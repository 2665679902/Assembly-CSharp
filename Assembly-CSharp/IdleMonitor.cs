using System;

// Token: 0x02000832 RID: 2098
public class IdleMonitor : GameStateMachine<IdleMonitor, IdleMonitor.Instance>
{
	// Token: 0x06003C9E RID: 15518 RVA: 0x001522FC File Offset: 0x001504FC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.TagTransition(GameTags.Dying, this.stopped, false).ToggleRecurringChore(new Func<IdleMonitor.Instance, Chore>(this.CreateIdleChore), null);
		this.stopped.DoNothing();
	}

	// Token: 0x06003C9F RID: 15519 RVA: 0x0015233C File Offset: 0x0015053C
	private Chore CreateIdleChore(IdleMonitor.Instance smi)
	{
		return new IdleChore(smi.master);
	}

	// Token: 0x0400278D RID: 10125
	public GameStateMachine<IdleMonitor, IdleMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x0400278E RID: 10126
	public GameStateMachine<IdleMonitor, IdleMonitor.Instance, IStateMachineTarget, object>.State stopped;

	// Token: 0x020015B8 RID: 5560
	public new class Instance : GameStateMachine<IdleMonitor, IdleMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008516 RID: 34070 RVA: 0x002EBDA3 File Offset: 0x002E9FA3
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}
	}
}
