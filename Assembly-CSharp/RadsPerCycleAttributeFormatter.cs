using System;
using Klei.AI;

// Token: 0x02000A3C RID: 2620
public class RadsPerCycleAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x06004F8D RID: 20365 RVA: 0x001C5800 File Offset: 0x001C3A00
	public RadsPerCycleAttributeFormatter()
		: base(GameUtil.UnitClass.Radiation, GameUtil.TimeSlice.PerCycle)
	{
	}

	// Token: 0x06004F8E RID: 20366 RVA: 0x001C580A File Offset: 0x001C3A0A
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.GetFormattedValue(instance.GetTotalDisplayValue(), GameUtil.TimeSlice.PerCycle);
	}

	// Token: 0x06004F8F RID: 20367 RVA: 0x001C5819 File Offset: 0x001C3A19
	public override string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice)
	{
		return base.GetFormattedValue(value / 600f, timeSlice);
	}
}
