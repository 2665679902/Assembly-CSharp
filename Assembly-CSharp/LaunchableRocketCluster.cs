using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000953 RID: 2387
[SerializationConfig(MemberSerialization.OptIn)]
public class LaunchableRocketCluster : StateMachineComponent<LaunchableRocketCluster.StatesInstance>, ILaunchableRocket
{
	// Token: 0x17000533 RID: 1331
	// (get) Token: 0x06004678 RID: 18040 RVA: 0x0018CD18 File Offset: 0x0018AF18
	public IList<Ref<RocketModuleCluster>> parts
	{
		get
		{
			return base.GetComponent<RocketModuleCluster>().CraftInterface.ClusterModules;
		}
	}

	// Token: 0x17000534 RID: 1332
	// (get) Token: 0x06004679 RID: 18041 RVA: 0x0018CD2A File Offset: 0x0018AF2A
	// (set) Token: 0x0600467A RID: 18042 RVA: 0x0018CD32 File Offset: 0x0018AF32
	public bool isLanding { get; private set; }

	// Token: 0x17000535 RID: 1333
	// (get) Token: 0x0600467B RID: 18043 RVA: 0x0018CD3B File Offset: 0x0018AF3B
	// (set) Token: 0x0600467C RID: 18044 RVA: 0x0018CD43 File Offset: 0x0018AF43
	public float rocketSpeed { get; private set; }

	// Token: 0x17000536 RID: 1334
	// (get) Token: 0x0600467D RID: 18045 RVA: 0x0018CD4C File Offset: 0x0018AF4C
	public LaunchableRocketRegisterType registerType
	{
		get
		{
			return LaunchableRocketRegisterType.Clustercraft;
		}
	}

	// Token: 0x17000537 RID: 1335
	// (get) Token: 0x0600467E RID: 18046 RVA: 0x0018CD4F File Offset: 0x0018AF4F
	public GameObject LaunchableGameObject
	{
		get
		{
			return base.gameObject;
		}
	}

	// Token: 0x0600467F RID: 18047 RVA: 0x0018CD57 File Offset: 0x0018AF57
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06004680 RID: 18048 RVA: 0x0018CD6C File Offset: 0x0018AF6C
	public List<GameObject> GetEngines()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (Ref<RocketModuleCluster> @ref in this.parts)
		{
			if (@ref.Get().GetComponent<RocketEngineCluster>())
			{
				list.Add(@ref.Get().gameObject);
			}
		}
		return list;
	}

	// Token: 0x06004681 RID: 18049 RVA: 0x0018CDDC File Offset: 0x0018AFDC
	private int GetRocketHeight()
	{
		int num = 0;
		foreach (Ref<RocketModuleCluster> @ref in this.parts)
		{
			num += @ref.Get().GetComponent<Building>().Def.HeightInCells;
		}
		return num;
	}

	// Token: 0x06004682 RID: 18050 RVA: 0x0018CE40 File Offset: 0x0018B040
	private float InitialFlightAnimOffsetForLanding()
	{
		int num = Grid.PosToCell(base.gameObject);
		return ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num]).maximumBounds.y - base.gameObject.transform.GetPosition().y + (float)this.GetRocketHeight() + 100f;
	}

	// Token: 0x04002E9F RID: 11935
	[Serialize]
	private int takeOffLocation;

	// Token: 0x04002EA2 RID: 11938
	private GameObject soundSpeakerObject;

	// Token: 0x02001749 RID: 5961
	public class StatesInstance : GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.GameInstance
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06008A4E RID: 35406 RVA: 0x002FBB44 File Offset: 0x002F9D44
		private float heightLaunchSpeedRatio
		{
			get
			{
				return Mathf.Pow((float)base.master.GetRocketHeight(), TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().heightSpeedPower) * TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().heightSpeedFactor;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06008A4F RID: 35407 RVA: 0x002FBB6C File Offset: 0x002F9D6C
		// (set) Token: 0x06008A50 RID: 35408 RVA: 0x002FBB7F File Offset: 0x002F9D7F
		public float DistanceAboveGround
		{
			get
			{
				return base.sm.distanceAboveGround.Get(this);
			}
			set
			{
				base.sm.distanceAboveGround.Set(value, this, false);
			}
		}

		// Token: 0x06008A51 RID: 35409 RVA: 0x002FBB95 File Offset: 0x002F9D95
		public StatesInstance(LaunchableRocketCluster master)
			: base(master)
		{
			this.takeoffAccelPowerInv = 1f / TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().takeoffAccelPower;
		}

		// Token: 0x06008A52 RID: 35410 RVA: 0x002FBBB4 File Offset: 0x002F9DB4
		public void SetMissionState(Spacecraft.MissionState state)
		{
			global::Debug.Assert(!DlcManager.FeatureClusterSpaceEnabled());
			SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(base.master.GetComponent<LaunchConditionManager>()).SetState(state);
		}

		// Token: 0x06008A53 RID: 35411 RVA: 0x002FBBDE File Offset: 0x002F9DDE
		public Spacecraft.MissionState GetMissionState()
		{
			global::Debug.Assert(!DlcManager.FeatureClusterSpaceEnabled());
			return SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(base.master.GetComponent<LaunchConditionManager>()).state;
		}

		// Token: 0x06008A54 RID: 35412 RVA: 0x002FBC07 File Offset: 0x002F9E07
		public bool IsGrounded()
		{
			return base.smi.master.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().Status == Clustercraft.CraftStatus.Grounded;
		}

		// Token: 0x06008A55 RID: 35413 RVA: 0x002FBC2C File Offset: 0x002F9E2C
		public bool IsNotSpaceBound()
		{
			Clustercraft component = base.smi.master.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>();
			return component.Status == Clustercraft.CraftStatus.Grounded || component.Status == Clustercraft.CraftStatus.Landing;
		}

		// Token: 0x06008A56 RID: 35414 RVA: 0x002FBC68 File Offset: 0x002F9E68
		public bool IsNotGroundBound()
		{
			Clustercraft component = base.smi.master.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>();
			return component.Status == Clustercraft.CraftStatus.Launching || component.Status == Clustercraft.CraftStatus.InFlight;
		}

		// Token: 0x06008A57 RID: 35415 RVA: 0x002FBCA4 File Offset: 0x002F9EA4
		public void SetupLaunch()
		{
			base.master.isLanding = false;
			base.master.rocketSpeed = 0f;
			base.sm.warmupTimeRemaining.Set(5f, this, false);
			base.sm.distanceAboveGround.Set(0f, this, false);
			if (base.master.soundSpeakerObject == null)
			{
				base.master.soundSpeakerObject = new GameObject("rocketSpeaker");
				base.master.soundSpeakerObject.transform.SetParent(base.master.gameObject.transform);
			}
			foreach (Ref<RocketModuleCluster> @ref in base.master.parts)
			{
				if (@ref != null)
				{
					base.master.takeOffLocation = Grid.PosToCell(base.master.gameObject);
					@ref.Get().Trigger(-1277991738, base.master.gameObject);
				}
			}
			CraftModuleInterface craftInterface = base.master.GetComponent<RocketModuleCluster>().CraftInterface;
			if (craftInterface != null)
			{
				craftInterface.Trigger(-1277991738, base.master.gameObject);
				WorldContainer component = craftInterface.GetComponent<WorldContainer>();
				List<MinionIdentity> worldItems = Components.MinionIdentities.GetWorldItems(component.id, false);
				MinionMigrationEventArgs minionMigrationEventArgs = new MinionMigrationEventArgs
				{
					prevWorldId = component.id,
					targetWorldId = component.id
				};
				foreach (MinionIdentity minionIdentity in worldItems)
				{
					minionMigrationEventArgs.minionId = minionIdentity;
					Game.Instance.Trigger(586301400, minionMigrationEventArgs);
				}
			}
			Game.Instance.Trigger(-1277991738, base.gameObject);
			this.constantVelocityPhase_maxSpeed = 0f;
		}

		// Token: 0x06008A58 RID: 35416 RVA: 0x002FBE9C File Offset: 0x002FA09C
		public void LaunchLoop(float dt)
		{
			base.master.isLanding = false;
			if (this.constantVelocityPhase_maxSpeed == 0f)
			{
				float num = Mathf.Pow((Mathf.Pow(TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().maxAccelerationDistance, this.takeoffAccelPowerInv) * this.heightLaunchSpeedRatio - 0.033f) / this.heightLaunchSpeedRatio, TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().takeoffAccelPower);
				this.constantVelocityPhase_maxSpeed = (TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().maxAccelerationDistance - num) / 0.033f;
			}
			if (base.sm.warmupTimeRemaining.Get(this) > 0f)
			{
				base.sm.warmupTimeRemaining.Delta(-dt, this);
			}
			else if (this.DistanceAboveGround < TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().maxAccelerationDistance)
			{
				float num2 = Mathf.Pow(this.DistanceAboveGround, this.takeoffAccelPowerInv) * this.heightLaunchSpeedRatio;
				num2 += dt;
				this.DistanceAboveGround = Mathf.Pow(num2 / this.heightLaunchSpeedRatio, TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().takeoffAccelPower);
				float num3 = Mathf.Pow((num2 - 0.033f) / this.heightLaunchSpeedRatio, TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().takeoffAccelPower);
				base.master.rocketSpeed = (this.DistanceAboveGround - num3) / 0.033f;
			}
			else
			{
				base.master.rocketSpeed = this.constantVelocityPhase_maxSpeed;
				this.DistanceAboveGround += base.master.rocketSpeed * dt;
			}
			this.UpdateSoundSpeakerObject();
			if (this.UpdatePartsAnimPositionsAndDamage(true) == 0)
			{
				base.smi.GoTo(base.sm.not_grounded.space);
			}
		}

		// Token: 0x06008A59 RID: 35417 RVA: 0x002FC020 File Offset: 0x002FA220
		public void FinalizeLaunch()
		{
			base.master.rocketSpeed = 0f;
			this.DistanceAboveGround = base.sm.distanceToSpace.Get(base.smi);
			foreach (Ref<RocketModuleCluster> @ref in base.master.parts)
			{
				if (@ref != null && !(@ref.Get() == null))
				{
					RocketModuleCluster rocketModuleCluster = @ref.Get();
					rocketModuleCluster.GetComponent<KBatchedAnimController>().Offset = Vector3.up * this.DistanceAboveGround;
					rocketModuleCluster.GetComponent<KBatchedAnimController>().enabled = false;
					rocketModuleCluster.GetComponent<RocketModule>().MoveToSpace();
				}
			}
			base.smi.master.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().SetCraftStatus(Clustercraft.CraftStatus.InFlight);
		}

		// Token: 0x06008A5A RID: 35418 RVA: 0x002FC100 File Offset: 0x002FA300
		public void SetupLanding()
		{
			float num = base.master.InitialFlightAnimOffsetForLanding();
			this.DistanceAboveGround = num;
			base.sm.warmupTimeRemaining.Set(2f, this, false);
			base.master.isLanding = true;
			base.master.rocketSpeed = 0f;
			this.constantVelocityPhase_maxSpeed = 0f;
		}

		// Token: 0x06008A5B RID: 35419 RVA: 0x002FC160 File Offset: 0x002FA360
		public void LandingLoop(float dt)
		{
			base.master.isLanding = true;
			if (this.constantVelocityPhase_maxSpeed == 0f)
			{
				float num = Mathf.Pow((Mathf.Pow(TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().maxAccelerationDistance, this.takeoffAccelPowerInv) * this.heightLaunchSpeedRatio - 0.033f) / this.heightLaunchSpeedRatio, TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().takeoffAccelPower);
				this.constantVelocityPhase_maxSpeed = (num - TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().maxAccelerationDistance) / 0.033f;
			}
			if (this.DistanceAboveGround > TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().maxAccelerationDistance)
			{
				base.master.rocketSpeed = this.constantVelocityPhase_maxSpeed;
				this.DistanceAboveGround += base.master.rocketSpeed * dt;
			}
			else if (this.DistanceAboveGround > 0.0025f)
			{
				float num2 = Mathf.Pow(this.DistanceAboveGround, this.takeoffAccelPowerInv) * this.heightLaunchSpeedRatio;
				num2 -= dt;
				this.DistanceAboveGround = Mathf.Pow(num2 / this.heightLaunchSpeedRatio, TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().takeoffAccelPower);
				float num3 = Mathf.Pow((num2 - 0.033f) / this.heightLaunchSpeedRatio, TuningData<LaunchableRocketCluster.StatesInstance.Tuning>.Get().takeoffAccelPower);
				base.master.rocketSpeed = (this.DistanceAboveGround - num3) / 0.033f;
			}
			else if (base.sm.warmupTimeRemaining.Get(this) > 0f)
			{
				base.sm.warmupTimeRemaining.Delta(-dt, this);
				this.DistanceAboveGround = 0f;
			}
			this.UpdateSoundSpeakerObject();
			this.UpdatePartsAnimPositionsAndDamage(true);
		}

		// Token: 0x06008A5C RID: 35420 RVA: 0x002FC2E0 File Offset: 0x002FA4E0
		public void FinalizeLanding()
		{
			base.GetComponent<KSelectable>().IsSelectable = true;
			base.master.rocketSpeed = 0f;
			this.DistanceAboveGround = 0f;
			foreach (Ref<RocketModuleCluster> @ref in base.smi.master.parts)
			{
				if (@ref != null && !(@ref.Get() == null))
				{
					@ref.Get().GetComponent<KBatchedAnimController>().Offset = Vector3.zero;
				}
			}
			base.smi.master.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().SetCraftStatus(Clustercraft.CraftStatus.Grounded);
		}

		// Token: 0x06008A5D RID: 35421 RVA: 0x002FC3A0 File Offset: 0x002FA5A0
		private void UpdateSoundSpeakerObject()
		{
			if (base.master.soundSpeakerObject == null)
			{
				base.master.soundSpeakerObject = new GameObject("rocketSpeaker");
				base.master.soundSpeakerObject.transform.SetParent(base.gameObject.transform);
			}
			base.master.soundSpeakerObject.transform.SetLocalPosition(this.DistanceAboveGround * Vector3.up);
		}

		// Token: 0x06008A5E RID: 35422 RVA: 0x002FC41C File Offset: 0x002FA61C
		public int UpdatePartsAnimPositionsAndDamage(bool doDamage = true)
		{
			int num = base.gameObject.GetMyWorldId();
			if (num == -1)
			{
				return 0;
			}
			LaunchPad currentPad = base.smi.master.GetComponent<RocketModuleCluster>().CraftInterface.CurrentPad;
			if (currentPad != null)
			{
				num = currentPad.GetMyWorldId();
			}
			int num2 = 0;
			foreach (Ref<RocketModuleCluster> @ref in base.master.parts)
			{
				if (@ref != null)
				{
					RocketModuleCluster rocketModuleCluster = @ref.Get();
					KBatchedAnimController component = rocketModuleCluster.GetComponent<KBatchedAnimController>();
					component.Offset = Vector3.up * this.DistanceAboveGround;
					Vector3 positionIncludingOffset = component.PositionIncludingOffset;
					int num3 = Grid.PosToCell(positionIncludingOffset);
					bool flag = Grid.IsValidCell(num3);
					bool flag2 = flag && (int)Grid.WorldIdx[num3] == num;
					if (component.enabled != flag2)
					{
						component.enabled = flag2;
					}
					if (doDamage && flag)
					{
						num2++;
						LaunchableRocketCluster.States.DoWorldDamage(rocketModuleCluster.gameObject, positionIncludingOffset, num);
					}
				}
			}
			return num2;
		}

		// Token: 0x04006CA3 RID: 27811
		private float takeoffAccelPowerInv;

		// Token: 0x04006CA4 RID: 27812
		private float constantVelocityPhase_maxSpeed;

		// Token: 0x020020C1 RID: 8385
		public class Tuning : TuningData<LaunchableRocketCluster.StatesInstance.Tuning>
		{
			// Token: 0x040091E4 RID: 37348
			public float takeoffAccelPower = 4f;

			// Token: 0x040091E5 RID: 37349
			public float maxAccelerationDistance = 25f;

			// Token: 0x040091E6 RID: 37350
			public float warmupTime = 5f;

			// Token: 0x040091E7 RID: 37351
			public float heightSpeedPower = 0.5f;

			// Token: 0x040091E8 RID: 37352
			public float heightSpeedFactor = 4f;

			// Token: 0x040091E9 RID: 37353
			public int maxAccelHeight = 40;
		}
	}

	// Token: 0x0200174A RID: 5962
	public class States : GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster>
	{
		// Token: 0x06008A5F RID: 35423 RVA: 0x002FC53C File Offset: 0x002FA73C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.grounded;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.grounded.EventTransition(GameHashes.DoLaunchRocket, this.not_grounded.launch_setup, null).EnterTransition(this.not_grounded.launch_loop, (LaunchableRocketCluster.StatesInstance smi) => smi.IsNotGroundBound()).Enter(delegate(LaunchableRocketCluster.StatesInstance smi)
			{
				smi.FinalizeLanding();
			});
			this.not_grounded.launch_setup.Enter(delegate(LaunchableRocketCluster.StatesInstance smi)
			{
				smi.SetupLaunch();
				this.distanceToSpace.Set((float)ConditionFlightPathIsClear.PadTopEdgeDistanceToCeilingEdge(smi.master.gameObject.GetComponent<RocketModuleCluster>().CraftInterface.CurrentPad.gameObject), smi, false);
				smi.GoTo(this.not_grounded.launch_loop);
			});
			this.not_grounded.launch_loop.EventTransition(GameHashes.DoReturnRocket, this.not_grounded.landing_setup, null).Enter(delegate(LaunchableRocketCluster.StatesInstance smi)
			{
				smi.UpdatePartsAnimPositionsAndDamage(false);
			}).Update(delegate(LaunchableRocketCluster.StatesInstance smi, float dt)
			{
				smi.LaunchLoop(dt);
			}, UpdateRate.SIM_EVERY_TICK, false)
				.ParamTransition<float>(this.distanceAboveGround, this.not_grounded.space, (LaunchableRocketCluster.StatesInstance smi, float p) => p >= this.distanceToSpace.Get(smi))
				.TriggerOnEnter(GameHashes.StartRocketLaunch, null)
				.Exit(delegate(LaunchableRocketCluster.StatesInstance smi)
				{
					smi.gameObject.GetMyWorld().RevealSurface();
				});
			this.not_grounded.space.EnterTransition(this.not_grounded.landing_setup, (LaunchableRocketCluster.StatesInstance smi) => smi.IsNotSpaceBound()).EventTransition(GameHashes.DoReturnRocket, this.not_grounded.landing_setup, null).Enter(delegate(LaunchableRocketCluster.StatesInstance smi)
			{
				smi.FinalizeLaunch();
			});
			this.not_grounded.landing_setup.Enter(delegate(LaunchableRocketCluster.StatesInstance smi)
			{
				smi.SetupLanding();
				smi.GoTo(this.not_grounded.landing_loop);
			});
			this.not_grounded.landing_loop.Enter(delegate(LaunchableRocketCluster.StatesInstance smi)
			{
				smi.UpdatePartsAnimPositionsAndDamage(false);
			}).Update(delegate(LaunchableRocketCluster.StatesInstance smi, float dt)
			{
				smi.LandingLoop(dt);
			}, UpdateRate.SIM_EVERY_TICK, false).ParamTransition<float>(this.distanceAboveGround, this.not_grounded.land, new StateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.Parameter<float>.Callback(this.IsFullyLanded<float>))
				.ParamTransition<float>(this.warmupTimeRemaining, this.not_grounded.land, new StateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.Parameter<float>.Callback(this.IsFullyLanded<float>));
			this.not_grounded.land.TriggerOnEnter(GameHashes.RocketTouchDown, null).Enter(delegate(LaunchableRocketCluster.StatesInstance smi)
			{
				foreach (Ref<RocketModuleCluster> @ref in smi.master.parts)
				{
					if (@ref != null && !(@ref.Get() == null))
					{
						@ref.Get().Trigger(-887025858, smi.gameObject);
					}
				}
				CraftModuleInterface craftInterface = smi.master.GetComponent<RocketModuleCluster>().CraftInterface;
				if (craftInterface != null)
				{
					craftInterface.Trigger(-887025858, smi.gameObject);
					WorldContainer component = craftInterface.GetComponent<WorldContainer>();
					List<MinionIdentity> worldItems = Components.MinionIdentities.GetWorldItems(component.id, false);
					MinionMigrationEventArgs minionMigrationEventArgs = new MinionMigrationEventArgs
					{
						prevWorldId = component.id,
						targetWorldId = component.id
					};
					foreach (MinionIdentity minionIdentity in worldItems)
					{
						minionMigrationEventArgs.minionId = minionIdentity;
						Game.Instance.Trigger(586301400, minionMigrationEventArgs);
					}
				}
				Game.Instance.Trigger(-887025858, smi.gameObject);
				smi.GoTo(this.grounded);
			});
		}

		// Token: 0x06008A60 RID: 35424 RVA: 0x002FC7F6 File Offset: 0x002FA9F6
		public bool IsFullyLanded<T>(LaunchableRocketCluster.StatesInstance smi, T p)
		{
			return this.distanceAboveGround.Get(smi) <= 0.0025f && this.warmupTimeRemaining.Get(smi) <= 0f;
		}

		// Token: 0x06008A61 RID: 35425 RVA: 0x002FC824 File Offset: 0x002FAA24
		public static void DoWorldDamage(GameObject part, Vector3 apparentPosition, int actualWorld)
		{
			OccupyArea component = part.GetComponent<OccupyArea>();
			component.UpdateOccupiedArea();
			foreach (CellOffset cellOffset in component.OccupiedCellsOffsets)
			{
				int num = Grid.OffsetCell(Grid.PosToCell(apparentPosition), cellOffset);
				if (Grid.IsValidCell(num) && Grid.WorldIdx[num] == Grid.WorldIdx[actualWorld])
				{
					if (Grid.Solid[num])
					{
						WorldDamage.Instance.ApplyDamage(num, 10000f, num, BUILDINGS.DAMAGESOURCES.ROCKET, UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.ROCKET);
					}
					else if (Grid.FakeFloor[num])
					{
						GameObject gameObject = Grid.Objects[num, 39];
						if (gameObject != null && gameObject.HasTag(GameTags.GantryExtended))
						{
							BuildingHP component2 = gameObject.GetComponent<BuildingHP>();
							if (component2 != null)
							{
								gameObject.Trigger(-794517298, new BuildingHP.DamageSourceInfo
								{
									damage = component2.MaxHitPoints,
									source = BUILDINGS.DAMAGESOURCES.ROCKET,
									popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.ROCKET
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x04006CA5 RID: 27813
		public StateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.FloatParameter warmupTimeRemaining;

		// Token: 0x04006CA6 RID: 27814
		public StateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.FloatParameter distanceAboveGround;

		// Token: 0x04006CA7 RID: 27815
		public StateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.FloatParameter distanceToSpace;

		// Token: 0x04006CA8 RID: 27816
		public GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.State grounded;

		// Token: 0x04006CA9 RID: 27817
		public LaunchableRocketCluster.States.NotGroundedStates not_grounded;

		// Token: 0x020020C2 RID: 8386
		public class NotGroundedStates : GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.State
		{
			// Token: 0x040091EA RID: 37354
			public GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.State launch_setup;

			// Token: 0x040091EB RID: 37355
			public GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.State launch_loop;

			// Token: 0x040091EC RID: 37356
			public GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.State space;

			// Token: 0x040091ED RID: 37357
			public GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.State landing_setup;

			// Token: 0x040091EE RID: 37358
			public GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.State landing_loop;

			// Token: 0x040091EF RID: 37359
			public GameStateMachine<LaunchableRocketCluster.States, LaunchableRocketCluster.StatesInstance, LaunchableRocketCluster, object>.State land;
		}
	}
}
