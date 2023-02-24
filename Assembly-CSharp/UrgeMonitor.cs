using System;
using Klei.AI;

// Token: 0x02000852 RID: 2130
public class UrgeMonitor : GameStateMachine<UrgeMonitor, UrgeMonitor.Instance>
{
	// Token: 0x06003D40 RID: 15680 RVA: 0x00156648 File Offset: 0x00154848
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.Transition(this.hasurge, (UrgeMonitor.Instance smi) => smi.HasUrge(), UpdateRate.SIM_200ms);
		this.hasurge.Transition(this.satisfied, (UrgeMonitor.Instance smi) => !smi.HasUrge(), UpdateRate.SIM_200ms).ToggleUrge((UrgeMonitor.Instance smi) => smi.GetUrge());
	}

	// Token: 0x0400281D RID: 10269
	public GameStateMachine<UrgeMonitor, UrgeMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x0400281E RID: 10270
	public GameStateMachine<UrgeMonitor, UrgeMonitor.Instance, IStateMachineTarget, object>.State hasurge;

	// Token: 0x02001603 RID: 5635
	public new class Instance : GameStateMachine<UrgeMonitor, UrgeMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008661 RID: 34401 RVA: 0x002EEF44 File Offset: 0x002ED144
		public Instance(IStateMachineTarget master, Urge urge, Amount amount, ScheduleBlockType schedule_block, float in_schedule_threshold, float out_of_schedule_threshold, bool is_threshold_minimum)
			: base(master)
		{
			this.urge = urge;
			this.scheduleBlock = schedule_block;
			this.schedulable = base.GetComponent<Schedulable>();
			this.amountInstance = base.gameObject.GetAmounts().Get(amount);
			this.isThresholdMinimum = is_threshold_minimum;
			this.inScheduleThreshold = in_schedule_threshold;
			this.outOfScheduleThreshold = out_of_schedule_threshold;
		}

		// Token: 0x06008662 RID: 34402 RVA: 0x002EEFA2 File Offset: 0x002ED1A2
		private float GetThreshold()
		{
			if (this.schedulable.IsAllowed(this.scheduleBlock))
			{
				return this.inScheduleThreshold;
			}
			return this.outOfScheduleThreshold;
		}

		// Token: 0x06008663 RID: 34403 RVA: 0x002EEFC4 File Offset: 0x002ED1C4
		public Urge GetUrge()
		{
			return this.urge;
		}

		// Token: 0x06008664 RID: 34404 RVA: 0x002EEFCC File Offset: 0x002ED1CC
		public bool HasUrge()
		{
			if (this.isThresholdMinimum)
			{
				return this.amountInstance.value >= this.GetThreshold();
			}
			return this.amountInstance.value <= this.GetThreshold();
		}

		// Token: 0x040068AC RID: 26796
		private AmountInstance amountInstance;

		// Token: 0x040068AD RID: 26797
		private Urge urge;

		// Token: 0x040068AE RID: 26798
		private ScheduleBlockType scheduleBlock;

		// Token: 0x040068AF RID: 26799
		private Schedulable schedulable;

		// Token: 0x040068B0 RID: 26800
		private float inScheduleThreshold;

		// Token: 0x040068B1 RID: 26801
		private float outOfScheduleThreshold;

		// Token: 0x040068B2 RID: 26802
		private bool isThresholdMinimum;
	}
}
