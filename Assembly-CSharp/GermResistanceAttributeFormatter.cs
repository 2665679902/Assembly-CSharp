using System;
using Klei.AI;

// Token: 0x02000A3F RID: 2623
public class GermResistanceAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x06004F97 RID: 20375 RVA: 0x001C5958 File Offset: 0x001C3B58
	public GermResistanceAttributeFormatter()
		: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.None)
	{
	}

	// Token: 0x06004F98 RID: 20376 RVA: 0x001C5962 File Offset: 0x001C3B62
	public override string GetFormattedModifier(AttributeModifier modifier)
	{
		return GameUtil.GetGermResistanceModifierString(modifier.Value, false);
	}
}
