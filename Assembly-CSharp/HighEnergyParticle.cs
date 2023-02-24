using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007A9 RID: 1961
[SerializationConfig(MemberSerialization.OptIn)]
public class HighEnergyParticle : StateMachineComponent<HighEnergyParticle.StatesInstance>
{
	// Token: 0x06003773 RID: 14195 RVA: 0x001343AB File Offset: 0x001325AB
	protected override void OnPrefabInit()
	{
		this.loopingSounds = base.gameObject.GetComponent<LoopingSounds>();
		this.flyingSound = GlobalAssets.GetSound("Radbolt_travel_LP", false);
		base.OnPrefabInit();
	}

	// Token: 0x06003774 RID: 14196 RVA: 0x001343D8 File Offset: 0x001325D8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.HighEnergyParticles.Add(this);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.HighEnergyParticleCount, base.gameObject);
		this.emitter.SetEmitting(false);
		this.emitter.Refresh();
		this.SetDirection(this.direction);
		base.gameObject.layer = LayerMask.NameToLayer("PlaceWithDepth");
		this.StartLoopingSound();
		base.smi.StartSM();
	}

	// Token: 0x06003775 RID: 14197 RVA: 0x00134460 File Offset: 0x00132660
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.StopLoopingSound();
		Components.HighEnergyParticles.Remove(this);
		if (this.capturedBy != null && this.capturedBy.currentParticle == this)
		{
			this.capturedBy.currentParticle = null;
		}
	}

	// Token: 0x06003776 RID: 14198 RVA: 0x001344B4 File Offset: 0x001326B4
	public void SetDirection(EightDirection direction)
	{
		this.direction = direction;
		float angle = EightDirectionUtil.GetAngle(direction);
		base.smi.master.transform.rotation = Quaternion.Euler(0f, 0f, angle);
	}

	// Token: 0x06003777 RID: 14199 RVA: 0x001344F4 File Offset: 0x001326F4
	public void Collide(HighEnergyParticle.CollisionType collisionType)
	{
		this.collision = collisionType;
		GameObject gameObject = new GameObject("HEPcollideFX");
		gameObject.SetActive(false);
		gameObject.transform.SetPosition(Grid.CellToPosCCC(Grid.PosToCell(base.smi.master.transform.position), Grid.SceneLayer.FXFront));
		KBatchedAnimController fxAnim = gameObject.AddComponent<KBatchedAnimController>();
		fxAnim.AnimFiles = new KAnimFile[] { Assets.GetAnim("hep_impact_kanim") };
		fxAnim.initialAnim = "graze";
		gameObject.SetActive(true);
		switch (collisionType)
		{
		case HighEnergyParticle.CollisionType.Captured:
			fxAnim.Play("full", KAnim.PlayMode.Once, 1f, 0f);
			break;
		case HighEnergyParticle.CollisionType.CaptureAndRelease:
			fxAnim.Play("partial", KAnim.PlayMode.Once, 1f, 0f);
			break;
		case HighEnergyParticle.CollisionType.PassThrough:
			fxAnim.Play("graze", KAnim.PlayMode.Once, 1f, 0f);
			break;
		}
		fxAnim.onAnimComplete += delegate(HashedString arg)
		{
			Util.KDestroyGameObject(fxAnim);
		};
		if (collisionType == HighEnergyParticle.CollisionType.PassThrough)
		{
			this.collision = HighEnergyParticle.CollisionType.None;
			return;
		}
		base.smi.sm.destroySignal.Trigger(base.smi);
	}

	// Token: 0x06003778 RID: 14200 RVA: 0x0013464F File Offset: 0x0013284F
	public void DestroyNow()
	{
		base.smi.sm.destroySimpleSignal.Trigger(base.smi);
	}

	// Token: 0x06003779 RID: 14201 RVA: 0x0013466C File Offset: 0x0013286C
	private void Capture(HighEnergyParticlePort input)
	{
		if (input.currentParticle != null)
		{
			DebugUtil.LogArgs(new object[] { "Particle was backed up and caused an explosion!" });
			base.smi.sm.destroySignal.Trigger(base.smi);
			return;
		}
		this.capturedBy = input;
		input.currentParticle = this;
		input.Capture(this);
		if (input.currentParticle == this)
		{
			input.currentParticle = null;
			this.capturedBy = null;
			this.Collide(HighEnergyParticle.CollisionType.Captured);
			return;
		}
		this.capturedBy = null;
		this.Collide(HighEnergyParticle.CollisionType.CaptureAndRelease);
	}

	// Token: 0x0600377A RID: 14202 RVA: 0x001346FD File Offset: 0x001328FD
	public void Uncapture()
	{
		if (this.capturedBy != null)
		{
			this.capturedBy.currentParticle = null;
		}
		this.capturedBy = null;
	}

	// Token: 0x0600377B RID: 14203 RVA: 0x00134720 File Offset: 0x00132920
	public void CheckCollision()
	{
		if (this.collision != HighEnergyParticle.CollisionType.None)
		{
			return;
		}
		int num = Grid.PosToCell(base.smi.master.transform.GetPosition());
		GameObject gameObject = Grid.Objects[num, 1];
		if (gameObject != null)
		{
			gameObject.GetComponent<Operational>();
			HighEnergyParticlePort component = gameObject.GetComponent<HighEnergyParticlePort>();
			if (component != null)
			{
				Vector2 vector = Grid.CellToPosCCC(component.GetHighEnergyParticleInputPortPosition(), Grid.SceneLayer.NoLayer);
				if (base.GetComponent<KCircleCollider2D>().Intersects(vector))
				{
					if (component.InputActive() && component.AllowCapture(this))
					{
						this.Capture(component);
						return;
					}
					this.Collide(HighEnergyParticle.CollisionType.PassThrough);
				}
			}
		}
		KCircleCollider2D component2 = base.GetComponent<KCircleCollider2D>();
		int num2 = 0;
		int num3 = 0;
		Grid.CellToXY(num, out num2, out num3);
		ListPool<ScenePartitionerEntry, HighEnergyParticle>.PooledList pooledList = ListPool<ScenePartitionerEntry, HighEnergyParticle>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(num2 - 1, num3 - 1, 3, 3, GameScenePartitioner.Instance.collisionLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			KCollider2D kcollider2D = scenePartitionerEntry.obj as KCollider2D;
			HighEnergyParticle component3 = kcollider2D.gameObject.GetComponent<HighEnergyParticle>();
			if (!(component3 == null) && !(component3 == this) && component3.isCollideable)
			{
				bool flag = component2.Intersects(component3.transform.position);
				bool flag2 = kcollider2D.Intersects(base.transform.position);
				if (flag && flag2)
				{
					this.payload += component3.payload;
					component3.DestroyNow();
					this.Collide(HighEnergyParticle.CollisionType.HighEnergyParticle);
					return;
				}
			}
		}
		pooledList.Recycle();
		GameObject gameObject2 = Grid.Objects[num, 3];
		if (gameObject2 != null)
		{
			ObjectLayerListItem objectLayerListItem = gameObject2.GetComponent<Pickupable>().objectLayerListItem;
			while (objectLayerListItem != null)
			{
				GameObject gameObject3 = objectLayerListItem.gameObject;
				objectLayerListItem = objectLayerListItem.nextItem;
				if (!(gameObject3 == null))
				{
					KPrefabID component4 = gameObject3.GetComponent<KPrefabID>();
					Health component5 = gameObject2.GetComponent<Health>();
					if (component5 != null && component4 != null && component4.HasTag(GameTags.Creature) && !component5.IsDefeated())
					{
						component5.Damage(20f);
						this.Collide(HighEnergyParticle.CollisionType.Creature);
						return;
					}
				}
			}
		}
		GameObject gameObject4 = Grid.Objects[num, 0];
		if (gameObject4 != null)
		{
			Health component6 = gameObject4.GetComponent<Health>();
			if (component6 != null && !component6.IsDefeated() && !gameObject4.HasTag(GameTags.Dead) && !gameObject4.HasTag(GameTags.Dying))
			{
				component6.Damage(20f);
				WoundMonitor.Instance smi = gameObject4.GetSMI<WoundMonitor.Instance>();
				if (smi != null && !component6.IsDefeated())
				{
					smi.PlayKnockedOverImpactAnimation();
				}
				gameObject4.GetComponent<PrimaryElement>().AddDisease(Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id), Mathf.FloorToInt(this.payload * 0.5f / 0.01f), "HEPImpact");
				this.Collide(HighEnergyParticle.CollisionType.Minion);
				return;
			}
		}
		if (Grid.IsSolidCell(num))
		{
			GameObject gameObject5 = Grid.Objects[num, 9];
			if (gameObject5 == null || !gameObject5.HasTag(GameTags.HEPPassThrough) || this.capturedBy == null || this.capturedBy.gameObject != gameObject5)
			{
				this.Collide(HighEnergyParticle.CollisionType.Solid);
			}
			return;
		}
	}

	// Token: 0x0600377C RID: 14204 RVA: 0x00134AB4 File Offset: 0x00132CB4
	public void MovingUpdate(float dt)
	{
		if (this.collision != HighEnergyParticle.CollisionType.None)
		{
			return;
		}
		Vector3 position = base.transform.GetPosition();
		int num = Grid.PosToCell(position);
		Vector3 vector = position + EightDirectionUtil.GetNormal(this.direction) * this.speed * dt;
		int num2 = Grid.PosToCell(vector);
		SaveGame.Instance.GetComponent<ColonyAchievementTracker>().radBoltTravelDistance += this.speed * dt;
		this.loopingSounds.UpdateVelocity(this.flyingSound, vector - position);
		if (!Grid.IsValidCell(num2))
		{
			base.smi.sm.destroySimpleSignal.Trigger(base.smi);
			return;
		}
		if (num != num2)
		{
			this.payload -= 0.1f;
			byte index = Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id);
			int num3 = Mathf.FloorToInt(5f);
			SimMessages.ModifyDiseaseOnCell(num2, index, num3);
		}
		if (this.payload <= 0f)
		{
			base.smi.sm.destroySimpleSignal.Trigger(base.smi);
		}
		base.transform.SetPosition(vector);
	}

	// Token: 0x0600377D RID: 14205 RVA: 0x00134BF1 File Offset: 0x00132DF1
	private void StartLoopingSound()
	{
		this.loopingSounds.StartSound(this.flyingSound);
	}

	// Token: 0x0600377E RID: 14206 RVA: 0x00134C05 File Offset: 0x00132E05
	private void StopLoopingSound()
	{
		this.loopingSounds.StopSound(this.flyingSound);
	}

	// Token: 0x0400251C RID: 9500
	[Serialize]
	private EightDirection direction;

	// Token: 0x0400251D RID: 9501
	[Serialize]
	public float speed;

	// Token: 0x0400251E RID: 9502
	[Serialize]
	public float payload;

	// Token: 0x0400251F RID: 9503
	[MyCmpReq]
	private RadiationEmitter emitter;

	// Token: 0x04002520 RID: 9504
	[Serialize]
	public float perCellFalloff;

	// Token: 0x04002521 RID: 9505
	[Serialize]
	public HighEnergyParticle.CollisionType collision;

	// Token: 0x04002522 RID: 9506
	[Serialize]
	public HighEnergyParticlePort capturedBy;

	// Token: 0x04002523 RID: 9507
	public short emitRadius;

	// Token: 0x04002524 RID: 9508
	public float emitRate;

	// Token: 0x04002525 RID: 9509
	public float emitSpeed;

	// Token: 0x04002526 RID: 9510
	private LoopingSounds loopingSounds;

	// Token: 0x04002527 RID: 9511
	public string flyingSound;

	// Token: 0x04002528 RID: 9512
	public bool isCollideable;

	// Token: 0x0200150B RID: 5387
	public enum CollisionType
	{
		// Token: 0x04006572 RID: 25970
		None,
		// Token: 0x04006573 RID: 25971
		Solid,
		// Token: 0x04006574 RID: 25972
		Creature,
		// Token: 0x04006575 RID: 25973
		Minion,
		// Token: 0x04006576 RID: 25974
		Captured,
		// Token: 0x04006577 RID: 25975
		HighEnergyParticle,
		// Token: 0x04006578 RID: 25976
		CaptureAndRelease,
		// Token: 0x04006579 RID: 25977
		PassThrough
	}

	// Token: 0x0200150C RID: 5388
	public class StatesInstance : GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.GameInstance
	{
		// Token: 0x06008280 RID: 33408 RVA: 0x002E4BC6 File Offset: 0x002E2DC6
		public StatesInstance(HighEnergyParticle smi)
			: base(smi)
		{
		}
	}

	// Token: 0x0200150D RID: 5389
	public class States : GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle>
	{
		// Token: 0x06008281 RID: 33409 RVA: 0x002E4BD0 File Offset: 0x002E2DD0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.ready.pre;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.ready.OnSignal(this.destroySimpleSignal, this.destroying.instant).OnSignal(this.destroySignal, this.destroying.explode, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.Creature).OnSignal(this.destroySignal, this.destroying.explode, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.Minion)
				.OnSignal(this.destroySignal, this.destroying.explode, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.Solid)
				.OnSignal(this.destroySignal, this.destroying.blackhole, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.HighEnergyParticle)
				.OnSignal(this.destroySignal, this.destroying.captured, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.Captured)
				.OnSignal(this.destroySignal, this.catchAndRelease, (HighEnergyParticle.StatesInstance smi) => smi.master.collision == HighEnergyParticle.CollisionType.CaptureAndRelease)
				.Enter(delegate(HighEnergyParticle.StatesInstance smi)
				{
					smi.master.emitter.SetEmitting(true);
					smi.master.isCollideable = true;
				})
				.Update(delegate(HighEnergyParticle.StatesInstance smi, float dt)
				{
					smi.master.MovingUpdate(dt);
					smi.master.CheckCollision();
				}, UpdateRate.SIM_EVERY_TICK, false);
			this.ready.pre.PlayAnim("travel_pre").OnAnimQueueComplete(this.ready.moving);
			this.ready.moving.PlayAnim("travel_loop", KAnim.PlayMode.Loop);
			this.catchAndRelease.Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				smi.master.collision = HighEnergyParticle.CollisionType.None;
			}).PlayAnim("explode", KAnim.PlayMode.Once).OnAnimQueueComplete(this.ready.pre);
			this.destroying.Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				smi.master.isCollideable = false;
				smi.master.StopLoopingSound();
			});
			this.destroying.instant.Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				UnityEngine.Object.Destroy(smi.master.gameObject);
			});
			this.destroying.explode.PlayAnim("explode").Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				this.EmitRemainingPayload(smi);
			});
			this.destroying.blackhole.PlayAnim("collision").Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				this.EmitRemainingPayload(smi);
			});
			this.destroying.captured.PlayAnim("travel_pst").OnAnimQueueComplete(this.destroying.instant).Enter(delegate(HighEnergyParticle.StatesInstance smi)
			{
				smi.master.emitter.SetEmitting(false);
			});
		}

		// Token: 0x06008282 RID: 33410 RVA: 0x002E4F08 File Offset: 0x002E3108
		private void EmitRemainingPayload(HighEnergyParticle.StatesInstance smi)
		{
			smi.master.GetComponent<KBatchedAnimController>().GetCurrentAnim();
			smi.master.emitter.emitRadiusX = 6;
			smi.master.emitter.emitRadiusY = 6;
			smi.master.emitter.emitRads = smi.master.payload * 0.5f * 600f / 9f;
			smi.master.emitter.Refresh();
			SimMessages.AddRemoveSubstance(Grid.PosToCell(smi.master.gameObject), SimHashes.Fallout, CellEventLogger.Instance.ElementEmitted, smi.master.payload * 0.001f, 5000f, Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id), Mathf.FloorToInt(smi.master.payload * 0.5f / 0.01f), true, -1);
			smi.Schedule(1f, delegate(object obj)
			{
				UnityEngine.Object.Destroy(smi.master.gameObject);
			}, null);
		}

		// Token: 0x0400657A RID: 25978
		public HighEnergyParticle.States.ReadyStates ready;

		// Token: 0x0400657B RID: 25979
		public HighEnergyParticle.States.DestructionStates destroying;

		// Token: 0x0400657C RID: 25980
		public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State catchAndRelease;

		// Token: 0x0400657D RID: 25981
		public StateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.Signal destroySignal;

		// Token: 0x0400657E RID: 25982
		public StateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.Signal destroySimpleSignal;

		// Token: 0x02002065 RID: 8293
		public class ReadyStates : GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State
		{
			// Token: 0x04009057 RID: 36951
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State pre;

			// Token: 0x04009058 RID: 36952
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State moving;
		}

		// Token: 0x02002066 RID: 8294
		public class DestructionStates : GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State
		{
			// Token: 0x04009059 RID: 36953
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State instant;

			// Token: 0x0400905A RID: 36954
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State explode;

			// Token: 0x0400905B RID: 36955
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State captured;

			// Token: 0x0400905C RID: 36956
			public GameStateMachine<HighEnergyParticle.States, HighEnergyParticle.StatesInstance, HighEnergyParticle, object>.State blackhole;
		}
	}
}
