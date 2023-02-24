using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000AF6 RID: 2806
public class CrewRationsEntry : CrewListEntry
{
	// Token: 0x0600563A RID: 22074 RVA: 0x001F3476 File Offset: 0x001F1676
	public override void Populate(MinionIdentity _identity)
	{
		base.Populate(_identity);
		this.rationMonitor = _identity.GetSMI<RationMonitor.Instance>();
		this.Refresh();
	}

	// Token: 0x0600563B RID: 22075 RVA: 0x001F3494 File Offset: 0x001F1694
	public override void Refresh()
	{
		base.Refresh();
		this.rationsEatenToday.text = GameUtil.GetFormattedCalories(this.rationMonitor.GetRationsAteToday(), GameUtil.TimeSlice.None, true);
		if (this.identity == null)
		{
			return;
		}
		foreach (AmountInstance amountInstance in this.identity.GetAmounts())
		{
			float min = amountInstance.GetMin();
			float max = amountInstance.GetMax();
			float num = max - min;
			string text = Mathf.RoundToInt((num - (max - amountInstance.value)) / num * 100f).ToString();
			if (amountInstance.amount == Db.Get().Amounts.Stress)
			{
				this.currentStressText.text = amountInstance.GetValueString();
				this.currentStressText.GetComponent<ToolTip>().toolTip = amountInstance.GetTooltip();
				this.stressTrendImage.SetValue(amountInstance);
			}
			else if (amountInstance.amount == Db.Get().Amounts.Calories)
			{
				this.currentCaloriesText.text = text + "%";
				this.currentCaloriesText.GetComponent<ToolTip>().toolTip = amountInstance.GetTooltip();
			}
			else if (amountInstance.amount == Db.Get().Amounts.HitPoints)
			{
				this.currentHealthText.text = text + "%";
				this.currentHealthText.GetComponent<ToolTip>().toolTip = amountInstance.GetTooltip();
			}
		}
	}

	// Token: 0x04003AAD RID: 15021
	public KButton incRationPerDayButton;

	// Token: 0x04003AAE RID: 15022
	public KButton decRationPerDayButton;

	// Token: 0x04003AAF RID: 15023
	public LocText rationPerDayText;

	// Token: 0x04003AB0 RID: 15024
	public LocText rationsEatenToday;

	// Token: 0x04003AB1 RID: 15025
	public LocText currentCaloriesText;

	// Token: 0x04003AB2 RID: 15026
	public LocText currentStressText;

	// Token: 0x04003AB3 RID: 15027
	public LocText currentHealthText;

	// Token: 0x04003AB4 RID: 15028
	public ValueTrendImageToggle stressTrendImage;

	// Token: 0x04003AB5 RID: 15029
	private RationMonitor.Instance rationMonitor;
}
