using System;
using STRINGS;

// Token: 0x0200097E RID: 2430
public class ConditionNoExtraPassengers : ProcessCondition
{
	// Token: 0x0600481E RID: 18462 RVA: 0x001957F3 File Offset: 0x001939F3
	public ConditionNoExtraPassengers(PassengerRocketModule module)
	{
		this.module = module;
	}

	// Token: 0x0600481F RID: 18463 RVA: 0x00195802 File Offset: 0x00193A02
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (!this.module.CheckExtraPassengers())
		{
			return ProcessCondition.Status.Ready;
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x06004820 RID: 18464 RVA: 0x00195814 File Offset: 0x00193A14
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.NO_EXTRA_PASSENGERS.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.NO_EXTRA_PASSENGERS.FAILURE;
	}

	// Token: 0x06004821 RID: 18465 RVA: 0x0019582F File Offset: 0x00193A2F
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.NO_EXTRA_PASSENGERS.TOOLTIP.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.NO_EXTRA_PASSENGERS.TOOLTIP.FAILURE;
	}

	// Token: 0x06004822 RID: 18466 RVA: 0x0019584A File Offset: 0x00193A4A
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F85 RID: 12165
	private PassengerRocketModule module;
}
