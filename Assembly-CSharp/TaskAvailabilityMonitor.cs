using System;

// Token: 0x0200084C RID: 2124
public class TaskAvailabilityMonitor : GameStateMachine<TaskAvailabilityMonitor, TaskAvailabilityMonitor.Instance>
{
	// Token: 0x06003D26 RID: 15654 RVA: 0x00155CF0 File Offset: 0x00153EF0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.EventTransition(GameHashes.NewDay, (TaskAvailabilityMonitor.Instance smi) => GameClock.Instance, this.unavailable, (TaskAvailabilityMonitor.Instance smi) => GameClock.Instance.GetCycle() > 0);
		this.unavailable.Enter("RefreshStatusItem", delegate(TaskAvailabilityMonitor.Instance smi)
		{
			smi.RefreshStatusItem();
		}).EventHandler(GameHashes.ScheduleChanged, delegate(TaskAvailabilityMonitor.Instance smi)
		{
			smi.RefreshStatusItem();
		});
	}

	// Token: 0x04002808 RID: 10248
	public GameStateMachine<TaskAvailabilityMonitor, TaskAvailabilityMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x04002809 RID: 10249
	public GameStateMachine<TaskAvailabilityMonitor, TaskAvailabilityMonitor.Instance, IStateMachineTarget, object>.State unavailable;

	// Token: 0x020015F4 RID: 5620
	public new class Instance : GameStateMachine<TaskAvailabilityMonitor, TaskAvailabilityMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008608 RID: 34312 RVA: 0x002EDCC5 File Offset: 0x002EBEC5
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06008609 RID: 34313 RVA: 0x002EDCCE File Offset: 0x002EBECE
		public void RefreshStatusItem()
		{
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().DuplicantStatusItems.Idle, null);
		}
	}
}
