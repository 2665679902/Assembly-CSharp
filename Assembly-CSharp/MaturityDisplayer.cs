using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000A38 RID: 2616
public class MaturityDisplayer : AsPercentAmountDisplayer
{
	// Token: 0x06004F78 RID: 20344 RVA: 0x001C51F9 File Offset: 0x001C33F9
	public MaturityDisplayer()
		: base(GameUtil.TimeSlice.PerCycle)
	{
		this.formatter = new MaturityDisplayer.MaturityAttributeFormatter();
	}

	// Token: 0x06004F79 RID: 20345 RVA: 0x001C5210 File Offset: 0x001C3410
	public override string GetTooltipDescription(Amount master, AmountInstance instance)
	{
		string text = base.GetTooltipDescription(master, instance);
		Growing component = instance.gameObject.GetComponent<Growing>();
		if (component.IsGrowing())
		{
			float num = (instance.GetMax() - instance.value) / instance.GetDelta();
			if (component != null && component.IsGrowing())
			{
				text += string.Format(CREATURES.STATS.MATURITY.TOOLTIP_GROWING_CROP, GameUtil.GetFormattedCycles(num, "F1", false), GameUtil.GetFormattedCycles(component.TimeUntilNextHarvest(), "F1", false));
			}
			else
			{
				text += string.Format(CREATURES.STATS.MATURITY.TOOLTIP_GROWING, GameUtil.GetFormattedCycles(num, "F1", false));
			}
		}
		else if (component.ReachedNextHarvest())
		{
			text += CREATURES.STATS.MATURITY.TOOLTIP_GROWN;
		}
		else
		{
			text += CREATURES.STATS.MATURITY.TOOLTIP_STALLED;
		}
		return text;
	}

	// Token: 0x06004F7A RID: 20346 RVA: 0x001C52E8 File Offset: 0x001C34E8
	public override string GetDescription(Amount master, AmountInstance instance)
	{
		Growing component = instance.gameObject.GetComponent<Growing>();
		if (component != null && component.IsGrowing())
		{
			return string.Format(CREATURES.STATS.MATURITY.AMOUNT_DESC_FMT, master.Name, this.formatter.GetFormattedValue(base.ToPercent(instance.value, instance), GameUtil.TimeSlice.None), GameUtil.GetFormattedCycles(component.TimeUntilNextHarvest(), "F1", false));
		}
		return base.GetDescription(master, instance);
	}

	// Token: 0x020018CC RID: 6348
	public class MaturityAttributeFormatter : StandardAttributeFormatter
	{
		// Token: 0x06008E74 RID: 36468 RVA: 0x0030D52B File Offset: 0x0030B72B
		public MaturityAttributeFormatter()
			: base(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.None)
		{
		}

		// Token: 0x06008E75 RID: 36469 RVA: 0x0030D538 File Offset: 0x0030B738
		public override string GetFormattedModifier(AttributeModifier modifier)
		{
			float num = modifier.Value;
			GameUtil.TimeSlice timeSlice = base.DeltaTimeSlice;
			if (modifier.IsMultiplier)
			{
				num *= 100f;
				timeSlice = GameUtil.TimeSlice.None;
			}
			return this.GetFormattedValue(num, timeSlice);
		}
	}
}
