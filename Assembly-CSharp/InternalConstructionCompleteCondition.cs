using System;
using STRINGS;

// Token: 0x02000986 RID: 2438
public class InternalConstructionCompleteCondition : ProcessCondition
{
	// Token: 0x06004846 RID: 18502 RVA: 0x00195E3B File Offset: 0x0019403B
	public InternalConstructionCompleteCondition(BuildingInternalConstructor.Instance target)
	{
		this.target = target;
	}

	// Token: 0x06004847 RID: 18503 RVA: 0x00195E4A File Offset: 0x0019404A
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.target.IsRequestingConstruction() && !this.target.HasOutputInStorage())
		{
			return ProcessCondition.Status.Warning;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x06004848 RID: 18504 RVA: 0x00195E69 File Offset: 0x00194069
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.INTERNAL_CONSTRUCTION_COMPLETE.STATUS.READY : UI.STARMAP.LAUNCHCHECKLIST.INTERNAL_CONSTRUCTION_COMPLETE.STATUS.FAILURE;
	}

	// Token: 0x06004849 RID: 18505 RVA: 0x00195E80 File Offset: 0x00194080
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.INTERNAL_CONSTRUCTION_COMPLETE.TOOLTIP.READY : UI.STARMAP.LAUNCHCHECKLIST.INTERNAL_CONSTRUCTION_COMPLETE.TOOLTIP.FAILURE;
	}

	// Token: 0x0600484A RID: 18506 RVA: 0x00195E97 File Offset: 0x00194097
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F8D RID: 12173
	private BuildingInternalConstructor.Instance target;
}
