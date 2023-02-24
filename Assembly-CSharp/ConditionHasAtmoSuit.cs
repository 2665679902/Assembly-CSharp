using System;
using STRINGS;

// Token: 0x02000978 RID: 2424
public class ConditionHasAtmoSuit : ProcessCondition
{
	// Token: 0x060047FF RID: 18431 RVA: 0x00194ED0 File Offset: 0x001930D0
	public ConditionHasAtmoSuit(CommandModule module)
	{
		this.module = module;
		ManualDeliveryKG manualDeliveryKG = this.module.FindOrAdd<ManualDeliveryKG>();
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		manualDeliveryKG.SetStorage(module.storage);
		manualDeliveryKG.RequestedItemTag = GameTags.AtmoSuit;
		manualDeliveryKG.MinimumMass = 1f;
		manualDeliveryKG.refillMass = 0.1f;
		manualDeliveryKG.capacity = 1f;
	}

	// Token: 0x06004800 RID: 18432 RVA: 0x00194F46 File Offset: 0x00193146
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.module.storage.GetAmountAvailable(GameTags.AtmoSuit) < 1f)
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x06004801 RID: 18433 RVA: 0x00194F6C File Offset: 0x0019316C
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.HASSUIT.NAME;
		}
		return UI.STARMAP.NOSUIT.NAME;
	}

	// Token: 0x06004802 RID: 18434 RVA: 0x00194F87 File Offset: 0x00193187
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.HASSUIT.TOOLTIP;
		}
		return UI.STARMAP.NOSUIT.TOOLTIP;
	}

	// Token: 0x06004803 RID: 18435 RVA: 0x00194FA2 File Offset: 0x001931A2
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04002F7D RID: 12157
	private CommandModule module;
}
