using System;
using Klei.AI;

// Token: 0x02000A35 RID: 2613
public class CaloriesDisplayer : StandardAmountDisplayer
{
	// Token: 0x06004F72 RID: 20338 RVA: 0x001C4FC7 File Offset: 0x001C31C7
	public CaloriesDisplayer()
		: base(GameUtil.UnitClass.Calories, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal)
	{
		this.formatter = new CaloriesDisplayer.CaloriesAttributeFormatter();
	}

	// Token: 0x020018C9 RID: 6345
	public class CaloriesAttributeFormatter : StandardAttributeFormatter
	{
		// Token: 0x06008E70 RID: 36464 RVA: 0x0030D4E8 File Offset: 0x0030B6E8
		public CaloriesAttributeFormatter()
			: base(GameUtil.UnitClass.Calories, GameUtil.TimeSlice.PerCycle)
		{
		}

		// Token: 0x06008E71 RID: 36465 RVA: 0x0030D4F2 File Offset: 0x0030B6F2
		public override string GetFormattedModifier(AttributeModifier modifier)
		{
			if (modifier.IsMultiplier)
			{
				return GameUtil.GetFormattedPercent(-modifier.Value * 100f, GameUtil.TimeSlice.None);
			}
			return base.GetFormattedModifier(modifier);
		}
	}
}
