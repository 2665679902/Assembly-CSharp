using System;
using TUNING;

// Token: 0x02000849 RID: 2121
public class StressBehaviourMonitor : GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance>
{
	// Token: 0x06003D1F RID: 15647 RVA: 0x001557E8 File Offset: 0x001539E8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.satisfied.EventTransition(GameHashes.Stressed, this.stressed, (StressBehaviourMonitor.Instance smi) => smi.gameObject.GetSMI<StressMonitor.Instance>() != null && smi.gameObject.GetSMI<StressMonitor.Instance>().IsStressed());
		this.stressed.DefaultState(this.stressed.tierOne).ToggleExpression(Db.Get().Expressions.Unhappy, null).ToggleAnims((StressBehaviourMonitor.Instance smi) => smi.tierOneLocoAnim)
			.Transition(this.satisfied, (StressBehaviourMonitor.Instance smi) => smi.gameObject.GetSMI<StressMonitor.Instance>() != null && !smi.gameObject.GetSMI<StressMonitor.Instance>().IsStressed(), UpdateRate.SIM_200ms);
		this.stressed.tierOne.DefaultState(this.stressed.tierOne.actingOut).EventTransition(GameHashes.StressedHadEnough, this.stressed.tierTwo, null);
		this.stressed.tierOne.actingOut.ToggleChore((StressBehaviourMonitor.Instance smi) => smi.CreateTierOneStressChore(), this.stressed.tierOne.reprieve);
		this.stressed.tierOne.reprieve.ScheduleGoTo(30f, this.stressed.tierOne.actingOut);
		this.stressed.tierTwo.DefaultState(this.stressed.tierTwo.actingOut).Update(delegate(StressBehaviourMonitor.Instance smi, float dt)
		{
			smi.sm.timeInTierTwoStressResponse.Set(smi.sm.timeInTierTwoStressResponse.Get(smi) + dt, smi, false);
		}, UpdateRate.SIM_200ms, false).Exit("ResetStress", delegate(StressBehaviourMonitor.Instance smi)
		{
			Db.Get().Amounts.Stress.Lookup(smi.gameObject).SetValue(STRESS.ACTING_OUT_RESET);
		});
		this.stressed.tierTwo.actingOut.ToggleChore((StressBehaviourMonitor.Instance smi) => smi.CreateTierTwoStressChore(), this.stressed.tierTwo.reprieve);
		this.stressed.tierTwo.reprieve.ToggleChore((StressBehaviourMonitor.Instance smi) => new StressIdleChore(smi.master), null).Enter(delegate(StressBehaviourMonitor.Instance smi)
		{
			if (smi.sm.timeInTierTwoStressResponse.Get(smi) >= 150f)
			{
				smi.sm.timeInTierTwoStressResponse.Set(0f, smi, false);
				smi.GoTo(this.stressed);
			}
		}).ScheduleGoTo((StressBehaviourMonitor.Instance smi) => smi.tierTwoReprieveDuration, this.stressed.tierTwo);
	}

	// Token: 0x040027FF RID: 10239
	public StateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.FloatParameter timeInTierTwoStressResponse;

	// Token: 0x04002800 RID: 10240
	public GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x04002801 RID: 10241
	public StressBehaviourMonitor.StressedState stressed;

	// Token: 0x020015EA RID: 5610
	public class StressedState : GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006847 RID: 26695
		public StressBehaviourMonitor.TierOneStates tierOne;

		// Token: 0x04006848 RID: 26696
		public StressBehaviourMonitor.TierTwoStates tierTwo;
	}

	// Token: 0x020015EB RID: 5611
	public class TierOneStates : GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006849 RID: 26697
		public GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.State actingOut;

		// Token: 0x0400684A RID: 26698
		public GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.State reprieve;
	}

	// Token: 0x020015EC RID: 5612
	public class TierTwoStates : GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400684B RID: 26699
		public GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.State actingOut;

		// Token: 0x0400684C RID: 26700
		public GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.State reprieve;
	}

	// Token: 0x020015ED RID: 5613
	public new class Instance : GameStateMachine<StressBehaviourMonitor, StressBehaviourMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060085E5 RID: 34277 RVA: 0x002ED683 File Offset: 0x002EB883
		public Instance(IStateMachineTarget master, Func<ChoreProvider, Chore> tier_one_stress_chore_creator, Func<ChoreProvider, Chore> tier_two_stress_chore_creator, string tier_one_loco_anim, float tier_two_reprieve_duration = 3f)
			: base(master)
		{
			this.tierOneLocoAnim = tier_one_loco_anim;
			this.tierTwoReprieveDuration = tier_two_reprieve_duration;
			this.tierOneStressChoreCreator = tier_one_stress_chore_creator;
			this.tierTwoStressChoreCreator = tier_two_stress_chore_creator;
		}

		// Token: 0x060085E6 RID: 34278 RVA: 0x002ED6B5 File Offset: 0x002EB8B5
		public Chore CreateTierOneStressChore()
		{
			return this.tierOneStressChoreCreator(base.GetComponent<ChoreProvider>());
		}

		// Token: 0x060085E7 RID: 34279 RVA: 0x002ED6C8 File Offset: 0x002EB8C8
		public Chore CreateTierTwoStressChore()
		{
			return this.tierTwoStressChoreCreator(base.GetComponent<ChoreProvider>());
		}

		// Token: 0x0400684D RID: 26701
		public Func<ChoreProvider, Chore> tierOneStressChoreCreator;

		// Token: 0x0400684E RID: 26702
		public Func<ChoreProvider, Chore> tierTwoStressChoreCreator;

		// Token: 0x0400684F RID: 26703
		public string tierOneLocoAnim = "";

		// Token: 0x04006850 RID: 26704
		public float tierTwoReprieveDuration;
	}
}
