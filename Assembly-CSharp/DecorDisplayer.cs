using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000A37 RID: 2615
public class DecorDisplayer : StandardAmountDisplayer
{
	// Token: 0x06004F76 RID: 20342 RVA: 0x001C510E File Offset: 0x001C330E
	public DecorDisplayer()
		: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal)
	{
		this.formatter = new DecorDisplayer.DecorAttributeFormatter();
	}

	// Token: 0x06004F77 RID: 20343 RVA: 0x001C5128 File Offset: 0x001C3328
	public override string GetTooltip(Amount master, AmountInstance instance)
	{
		string text = string.Format(LocText.ParseText(master.description), this.formatter.GetFormattedValue(instance.value, GameUtil.TimeSlice.None));
		int num = Grid.PosToCell(instance.gameObject);
		if (Grid.IsValidCell(num))
		{
			text += string.Format(DUPLICANTS.STATS.DECOR.TOOLTIP_CURRENT, GameUtil.GetDecorAtCell(num));
		}
		text += "\n";
		DecorMonitor.Instance smi = instance.gameObject.GetSMI<DecorMonitor.Instance>();
		if (smi != null)
		{
			text += string.Format(DUPLICANTS.STATS.DECOR.TOOLTIP_AVERAGE_TODAY, this.formatter.GetFormattedValue(smi.GetTodaysAverageDecor(), GameUtil.TimeSlice.None));
			text += string.Format(DUPLICANTS.STATS.DECOR.TOOLTIP_AVERAGE_YESTERDAY, this.formatter.GetFormattedValue(smi.GetYesterdaysAverageDecor(), GameUtil.TimeSlice.None));
		}
		return text;
	}

	// Token: 0x020018CB RID: 6347
	public class DecorAttributeFormatter : StandardAttributeFormatter
	{
		// Token: 0x06008E73 RID: 36467 RVA: 0x0030D521 File Offset: 0x0030B721
		public DecorAttributeFormatter()
			: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.PerCycle)
		{
		}
	}
}
