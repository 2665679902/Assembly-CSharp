using System;
using STRINGS;

// Token: 0x02000987 RID: 2439
public class LoadingCompleteCondition : ProcessCondition
{
	// Token: 0x0600484B RID: 18507 RVA: 0x00195E9A File Offset: 0x0019409A
	public LoadingCompleteCondition(Storage target)
	{
		this.target = target;
		this.userControlledTarget = target.GetComponent<IUserControlledCapacity>();
	}

	// Token: 0x0600484C RID: 18508 RVA: 0x00195EB5 File Offset: 0x001940B5
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.userControlledTarget != null)
		{
			if (this.userControlledTarget.AmountStored < this.userControlledTarget.UserMaxCapacity)
			{
				return ProcessCondition.Status.Warning;
			}
			return ProcessCondition.Status.Ready;
		}
		else
		{
			if (!this.target.IsFull())
			{
				return ProcessCondition.Status.Warning;
			}
			return ProcessCondition.Status.Ready;
		}
	}

	// Token: 0x0600484D RID: 18509 RVA: 0x00195EEB File Offset: 0x001940EB
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.LOADING_COMPLETE.STATUS.READY : UI.STARMAP.LAUNCHCHECKLIST.LOADING_COMPLETE.STATUS.WARNING;
	}

	// Token: 0x0600484E RID: 18510 RVA: 0x00195F02 File Offset: 0x00194102
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.LOADING_COMPLETE.TOOLTIP.READY : UI.STARMAP.LAUNCHCHECKLIST.LOADING_COMPLETE.TOOLTIP.WARNING;
	}

	// Token: 0x0600484F RID: 18511 RVA: 0x00195F19 File Offset: 0x00194119
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F8E RID: 12174
	private Storage target;

	// Token: 0x04002F8F RID: 12175
	private IUserControlledCapacity userControlledTarget;
}
