using System;
using STRINGS;

// Token: 0x02000984 RID: 2436
public class ConditionSufficientFood : ProcessCondition
{
	// Token: 0x0600483C RID: 18492 RVA: 0x00195C9B File Offset: 0x00193E9B
	public ConditionSufficientFood(CommandModule module)
	{
		this.module = module;
	}

	// Token: 0x0600483D RID: 18493 RVA: 0x00195CAA File Offset: 0x00193EAA
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.module.storage.GetAmountAvailable(GameTags.Edible) <= 1f)
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x0600483E RID: 18494 RVA: 0x00195CCD File Offset: 0x00193ECD
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.HASFOOD.NAME;
		}
		return UI.STARMAP.NOFOOD.NAME;
	}

	// Token: 0x0600483F RID: 18495 RVA: 0x00195CE8 File Offset: 0x00193EE8
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.HASFOOD.TOOLTIP;
		}
		return UI.STARMAP.NOFOOD.TOOLTIP;
	}

	// Token: 0x06004840 RID: 18496 RVA: 0x00195D03 File Offset: 0x00193F03
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F8B RID: 12171
	private CommandModule module;
}
