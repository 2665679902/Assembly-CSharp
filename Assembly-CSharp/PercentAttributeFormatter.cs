using System;
using Klei.AI;

// Token: 0x02000A41 RID: 2625
public class PercentAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x06004F9D RID: 20381 RVA: 0x001C59CA File Offset: 0x001C3BCA
	public PercentAttributeFormatter()
		: base(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.None)
	{
	}

	// Token: 0x06004F9E RID: 20382 RVA: 0x001C59D4 File Offset: 0x001C3BD4
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.GetFormattedValue(instance.GetTotalDisplayValue(), base.DeltaTimeSlice);
	}

	// Token: 0x06004F9F RID: 20383 RVA: 0x001C59E8 File Offset: 0x001C3BE8
	public override string GetFormattedModifier(AttributeModifier modifier)
	{
		return this.GetFormattedValue(modifier.Value, base.DeltaTimeSlice);
	}

	// Token: 0x06004FA0 RID: 20384 RVA: 0x001C59FC File Offset: 0x001C3BFC
	public override string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice)
	{
		return GameUtil.GetFormattedPercent(value * 100f, timeSlice);
	}
}
