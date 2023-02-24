using System;
using Klei.AI;

// Token: 0x02000A40 RID: 2624
public class ToPercentAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x06004F99 RID: 20377 RVA: 0x001C5970 File Offset: 0x001C3B70
	public ToPercentAttributeFormatter(float max, GameUtil.TimeSlice deltaTimeSlice = GameUtil.TimeSlice.None)
		: base(GameUtil.UnitClass.Percent, deltaTimeSlice)
	{
		this.max = max;
	}

	// Token: 0x06004F9A RID: 20378 RVA: 0x001C598C File Offset: 0x001C3B8C
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.GetFormattedValue(instance.GetTotalDisplayValue(), base.DeltaTimeSlice);
	}

	// Token: 0x06004F9B RID: 20379 RVA: 0x001C59A0 File Offset: 0x001C3BA0
	public override string GetFormattedModifier(AttributeModifier modifier)
	{
		return this.GetFormattedValue(modifier.Value, base.DeltaTimeSlice);
	}

	// Token: 0x06004F9C RID: 20380 RVA: 0x001C59B4 File Offset: 0x001C3BB4
	public override string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice)
	{
		return GameUtil.GetFormattedPercent(value / this.max * 100f, timeSlice);
	}

	// Token: 0x0400354D RID: 13645
	public float max = 1f;
}
