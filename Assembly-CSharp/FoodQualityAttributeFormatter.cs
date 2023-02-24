using System;
using Klei.AI;

// Token: 0x02000A3D RID: 2621
public class FoodQualityAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x06004F90 RID: 20368 RVA: 0x001C5829 File Offset: 0x001C3A29
	public FoodQualityAttributeFormatter()
		: base(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None)
	{
	}

	// Token: 0x06004F91 RID: 20369 RVA: 0x001C5833 File Offset: 0x001C3A33
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.GetFormattedValue(instance.GetTotalDisplayValue(), GameUtil.TimeSlice.None);
	}

	// Token: 0x06004F92 RID: 20370 RVA: 0x001C5842 File Offset: 0x001C3A42
	public override string GetFormattedModifier(AttributeModifier modifier)
	{
		return GameUtil.GetFormattedInt(modifier.Value, GameUtil.TimeSlice.None);
	}

	// Token: 0x06004F93 RID: 20371 RVA: 0x001C5850 File Offset: 0x001C3A50
	public override string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice)
	{
		return Util.StripTextFormatting(GameUtil.GetFormattedFoodQuality((int)value));
	}
}
