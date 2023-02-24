using System;
using STRINGS;

// Token: 0x02000981 RID: 2433
public class ConditionPilotOnBoard : ProcessCondition
{
	// Token: 0x0600482D RID: 18477 RVA: 0x00195A17 File Offset: 0x00193C17
	public ConditionPilotOnBoard(PassengerRocketModule module)
	{
		this.module = module;
	}

	// Token: 0x0600482E RID: 18478 RVA: 0x00195A26 File Offset: 0x00193C26
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (!this.module.CheckPilotBoarded())
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x0600482F RID: 18479 RVA: 0x00195A38 File Offset: 0x00193C38
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.FAILURE;
	}

	// Token: 0x06004830 RID: 18480 RVA: 0x00195A53 File Offset: 0x00193C53
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.TOOLTIP.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.TOOLTIP.FAILURE;
	}

	// Token: 0x06004831 RID: 18481 RVA: 0x00195A6E File Offset: 0x00193C6E
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F88 RID: 12168
	private PassengerRocketModule module;
}
