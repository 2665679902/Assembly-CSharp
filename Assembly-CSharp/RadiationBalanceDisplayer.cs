using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000A36 RID: 2614
public class RadiationBalanceDisplayer : StandardAmountDisplayer
{
	// Token: 0x06004F73 RID: 20339 RVA: 0x001C4FDE File Offset: 0x001C31DE
	public RadiationBalanceDisplayer()
		: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal)
	{
		this.formatter = new RadiationBalanceDisplayer.RadiationAttributeFormatter();
	}

	// Token: 0x06004F74 RID: 20340 RVA: 0x001C4FF5 File Offset: 0x001C31F5
	public override string GetValueString(Amount master, AmountInstance instance)
	{
		return base.GetValueString(master, instance) + UI.UNITSUFFIXES.RADIATION.RADS;
	}

	// Token: 0x06004F75 RID: 20341 RVA: 0x001C5010 File Offset: 0x001C3210
	public override string GetTooltip(Amount master, AmountInstance instance)
	{
		string text = "";
		if (instance.gameObject.GetSMI<RadiationMonitor.Instance>() != null)
		{
			int num = Grid.PosToCell(instance.gameObject);
			if (Grid.IsValidCell(num))
			{
				text += DUPLICANTS.STATS.RADIATIONBALANCE.TOOLTIP_CURRENT_BALANCE;
			}
			text += "\n\n";
			float num2 = Mathf.Clamp01(1f - Db.Get().Attributes.RadiationResistance.Lookup(instance.gameObject).GetTotalValue());
			text += string.Format(DUPLICANTS.STATS.RADIATIONBALANCE.CURRENT_EXPOSURE, Mathf.RoundToInt(Grid.Radiation[num] * num2));
			text += "\n";
			text += string.Format(DUPLICANTS.STATS.RADIATIONBALANCE.CURRENT_REJUVENATION, Mathf.RoundToInt(Db.Get().Attributes.RadiationRecovery.Lookup(instance.gameObject).GetTotalValue() * 600f));
		}
		return text;
	}

	// Token: 0x020018CA RID: 6346
	public class RadiationAttributeFormatter : StandardAttributeFormatter
	{
		// Token: 0x06008E72 RID: 36466 RVA: 0x0030D517 File Offset: 0x0030B717
		public RadiationAttributeFormatter()
			: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.PerCycle)
		{
		}
	}
}
