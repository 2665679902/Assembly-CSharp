using System;

// Token: 0x020006E8 RID: 1768
public class RobotIdleMonitor : GameStateMachine<RobotIdleMonitor, RobotIdleMonitor.Instance>
{
	// Token: 0x0600301B RID: 12315 RVA: 0x000FE3C8 File Offset: 0x000FC5C8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.PlayAnim("idle_loop", KAnim.PlayMode.Loop).Transition(this.working, (RobotIdleMonitor.Instance smi) => !RobotIdleMonitor.CheckShouldIdle(smi), UpdateRate.SIM_200ms);
		this.working.Transition(this.idle, (RobotIdleMonitor.Instance smi) => RobotIdleMonitor.CheckShouldIdle(smi), UpdateRate.SIM_200ms);
	}

	// Token: 0x0600301C RID: 12316 RVA: 0x000FE44C File Offset: 0x000FC64C
	private static bool CheckShouldIdle(RobotIdleMonitor.Instance smi)
	{
		FallMonitor.Instance smi2 = smi.master.gameObject.GetSMI<FallMonitor.Instance>();
		return smi2 == null || (!smi.master.gameObject.GetComponent<ChoreConsumer>().choreDriver.HasChore() && smi2.GetCurrentState() == smi2.sm.standing);
	}

	// Token: 0x04001D03 RID: 7427
	public GameStateMachine<RobotIdleMonitor, RobotIdleMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x04001D04 RID: 7428
	public GameStateMachine<RobotIdleMonitor, RobotIdleMonitor.Instance, IStateMachineTarget, object>.State working;

	// Token: 0x020013FA RID: 5114
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020013FB RID: 5115
	public new class Instance : GameStateMachine<RobotIdleMonitor, RobotIdleMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007FBD RID: 32701 RVA: 0x002DDAD2 File Offset: 0x002DBCD2
		public Instance(IStateMachineTarget master, RobotIdleMonitor.Def def)
			: base(master)
		{
		}

		// Token: 0x0400622C RID: 25132
		public KBatchedAnimController eyes;
	}
}
