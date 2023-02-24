using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000993 RID: 2451
[SkipSaveFileSerialization]
public class Stinky : StateMachineComponent<Stinky.StatesInstance>
{
	// Token: 0x06004889 RID: 18569 RVA: 0x00196B61 File Offset: 0x00194D61
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x0600488A RID: 18570 RVA: 0x00196B70 File Offset: 0x00194D70
	private void Emit(object data)
	{
		GameObject gameObject = (GameObject)data;
		Components.Cmps<MinionIdentity> liveMinionIdentities = Components.LiveMinionIdentities;
		Vector2 vector = gameObject.transform.GetPosition();
		for (int i = 0; i < liveMinionIdentities.Count; i++)
		{
			MinionIdentity minionIdentity = liveMinionIdentities[i];
			if (minionIdentity.gameObject != gameObject.gameObject)
			{
				Vector2 vector2 = minionIdentity.transform.GetPosition();
				if (Vector2.SqrMagnitude(vector - vector2) <= 2.25f)
				{
					minionIdentity.Trigger(508119890, Strings.Get("STRINGS.DUPLICANTS.DISEASES.PUTRIDODOUR.CRINGE_EFFECT").String);
					minionIdentity.GetComponent<Effects>().Add("SmelledStinky", true);
					minionIdentity.gameObject.GetSMI<ThoughtGraph.Instance>().AddThought(Db.Get().Thoughts.PutridOdour);
				}
			}
		}
		int num = Grid.PosToCell(gameObject.transform.GetPosition());
		float value = Db.Get().Amounts.Temperature.Lookup(this).value;
		SimMessages.AddRemoveSubstance(num, SimHashes.ContaminatedOxygen, CellEventLogger.Instance.ElementConsumerSimUpdate, 0.0025000002f, value, byte.MaxValue, 0, true, -1);
		GameObject gameObject2 = gameObject;
		bool flag = SoundEvent.ObjectIsSelectedAndVisible(gameObject2);
		Vector3 vector3 = gameObject2.GetComponent<Transform>().GetPosition();
		float num2 = 1f;
		if (flag)
		{
			vector3 = SoundEvent.AudioHighlightListenerPosition(vector3);
			num2 = SoundEvent.GetVolume(flag);
		}
		else
		{
			vector3.z = 0f;
		}
		KFMOD.PlayOneShot(GlobalAssets.GetSound("Dupe_Flatulence", false), vector3, num2);
	}

	// Token: 0x04002FB8 RID: 12216
	private const float EmitMass = 0.0025000002f;

	// Token: 0x04002FB9 RID: 12217
	private const SimHashes EmitElement = SimHashes.ContaminatedOxygen;

	// Token: 0x04002FBA RID: 12218
	private const float EmissionRadius = 1.5f;

	// Token: 0x04002FBB RID: 12219
	private const float MaxDistanceSq = 2.25f;

	// Token: 0x04002FBC RID: 12220
	private KBatchedAnimController stinkyController;

	// Token: 0x04002FBD RID: 12221
	private static readonly HashedString[] WorkLoopAnims = new HashedString[] { "working_pre", "working_loop", "working_pst" };

	// Token: 0x0200178D RID: 6029
	public class StatesInstance : GameStateMachine<Stinky.States, Stinky.StatesInstance, Stinky, object>.GameInstance
	{
		// Token: 0x06008B3E RID: 35646 RVA: 0x002FF103 File Offset: 0x002FD303
		public StatesInstance(Stinky master)
			: base(master)
		{
		}
	}

	// Token: 0x0200178E RID: 6030
	public class States : GameStateMachine<Stinky.States, Stinky.StatesInstance, Stinky>
	{
		// Token: 0x06008B3F RID: 35647 RVA: 0x002FF10C File Offset: 0x002FD30C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.TagTransition(GameTags.Dead, null, false).Enter(delegate(Stinky.StatesInstance smi)
			{
				KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("odor_fx_kanim", smi.master.gameObject.transform.GetPosition(), smi.master.gameObject.transform, true, Grid.SceneLayer.Front, false);
				kbatchedAnimController.Play(Stinky.WorkLoopAnims, KAnim.PlayMode.Once);
				smi.master.stinkyController = kbatchedAnimController;
			}).Update("StinkyFX", delegate(Stinky.StatesInstance smi, float dt)
			{
				if (smi.master.stinkyController != null)
				{
					smi.master.stinkyController.Play(Stinky.WorkLoopAnims, KAnim.PlayMode.Once);
				}
			}, UpdateRate.SIM_4000ms, false);
			this.idle.Enter("ScheduleNextFart", delegate(Stinky.StatesInstance smi)
			{
				smi.ScheduleGoTo(this.GetNewInterval(), this.emit);
			});
			this.emit.Enter("Fart", delegate(Stinky.StatesInstance smi)
			{
				smi.master.Emit(smi.master.gameObject);
			}).ToggleExpression(Db.Get().Expressions.Relief, null).ScheduleGoTo(3f, this.idle);
		}

		// Token: 0x06008B40 RID: 35648 RVA: 0x002FF1F5 File Offset: 0x002FD3F5
		private float GetNewInterval()
		{
			return Mathf.Min(Mathf.Max(Util.GaussianRandom(TRAITS.STINKY_EMIT_INTERVAL_MAX - TRAITS.STINKY_EMIT_INTERVAL_MIN, 1f), TRAITS.STINKY_EMIT_INTERVAL_MIN), TRAITS.STINKY_EMIT_INTERVAL_MAX);
		}

		// Token: 0x04006D63 RID: 28003
		public GameStateMachine<Stinky.States, Stinky.StatesInstance, Stinky, object>.State idle;

		// Token: 0x04006D64 RID: 28004
		public GameStateMachine<Stinky.States, Stinky.StatesInstance, Stinky, object>.State emit;
	}
}
