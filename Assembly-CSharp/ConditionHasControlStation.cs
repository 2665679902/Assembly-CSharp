using System;
using STRINGS;

// Token: 0x02000979 RID: 2425
public class ConditionHasControlStation : ProcessCondition
{
	// Token: 0x06004804 RID: 18436 RVA: 0x00194FA5 File Offset: 0x001931A5
	public ConditionHasControlStation(RocketModuleCluster module)
	{
		this.module = module;
	}

	// Token: 0x06004805 RID: 18437 RVA: 0x00194FB4 File Offset: 0x001931B4
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (Components.RocketControlStations.GetWorldItems(this.module.CraftInterface.GetComponent<WorldContainer>().id, false).Count <= 0)
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x06004806 RID: 18438 RVA: 0x00194FE1 File Offset: 0x001931E1
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.STATUS.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.STATUS.FAILURE;
	}

	// Token: 0x06004807 RID: 18439 RVA: 0x00194FFC File Offset: 0x001931FC
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.TOOLTIP.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.TOOLTIP.FAILURE;
	}

	// Token: 0x06004808 RID: 18440 RVA: 0x00195017 File Offset: 0x00193217
	public override bool ShowInUI()
	{
		return this.EvaluateCondition() == ProcessCondition.Status.Failure;
	}

	// Token: 0x04002F7E RID: 12158
	private RocketModuleCluster module;
}
