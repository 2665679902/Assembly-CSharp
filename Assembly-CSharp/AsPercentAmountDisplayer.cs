using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000A34 RID: 2612
public class AsPercentAmountDisplayer : IAmountDisplayer
{
	// Token: 0x170005CD RID: 1485
	// (get) Token: 0x06004F66 RID: 20326 RVA: 0x001C4DAB File Offset: 0x001C2FAB
	public IAttributeFormatter Formatter
	{
		get
		{
			return this.formatter;
		}
	}

	// Token: 0x170005CE RID: 1486
	// (get) Token: 0x06004F67 RID: 20327 RVA: 0x001C4DB3 File Offset: 0x001C2FB3
	// (set) Token: 0x06004F68 RID: 20328 RVA: 0x001C4DC0 File Offset: 0x001C2FC0
	public GameUtil.TimeSlice DeltaTimeSlice
	{
		get
		{
			return this.formatter.DeltaTimeSlice;
		}
		set
		{
			this.formatter.DeltaTimeSlice = value;
		}
	}

	// Token: 0x06004F69 RID: 20329 RVA: 0x001C4DCE File Offset: 0x001C2FCE
	public AsPercentAmountDisplayer(GameUtil.TimeSlice deltaTimeSlice)
	{
		this.formatter = new StandardAttributeFormatter(GameUtil.UnitClass.Percent, deltaTimeSlice);
	}

	// Token: 0x06004F6A RID: 20330 RVA: 0x001C4DE3 File Offset: 0x001C2FE3
	public string GetValueString(Amount master, AmountInstance instance)
	{
		return this.formatter.GetFormattedValue(this.ToPercent(instance.value, instance), GameUtil.TimeSlice.None);
	}

	// Token: 0x06004F6B RID: 20331 RVA: 0x001C4DFE File Offset: 0x001C2FFE
	public virtual string GetDescription(Amount master, AmountInstance instance)
	{
		return string.Format("{0}: {1}", master.Name, this.formatter.GetFormattedValue(this.ToPercent(instance.value, instance), GameUtil.TimeSlice.None));
	}

	// Token: 0x06004F6C RID: 20332 RVA: 0x001C4E29 File Offset: 0x001C3029
	public virtual string GetTooltipDescription(Amount master, AmountInstance instance)
	{
		return string.Format(master.description, this.formatter.GetFormattedValue(instance.value, GameUtil.TimeSlice.None));
	}

	// Token: 0x06004F6D RID: 20333 RVA: 0x001C4E48 File Offset: 0x001C3048
	public virtual string GetTooltip(Amount master, AmountInstance instance)
	{
		string text = string.Format(master.description, this.formatter.GetFormattedValue(instance.value, GameUtil.TimeSlice.None));
		text += "\n\n";
		if (this.formatter.DeltaTimeSlice == GameUtil.TimeSlice.PerCycle)
		{
			text += string.Format(UI.CHANGEPERCYCLE, this.formatter.GetFormattedValue(this.ToPercent(instance.deltaAttribute.GetTotalDisplayValue(), instance), GameUtil.TimeSlice.PerCycle));
		}
		else
		{
			text += string.Format(UI.CHANGEPERSECOND, this.formatter.GetFormattedValue(this.ToPercent(instance.deltaAttribute.GetTotalDisplayValue(), instance), GameUtil.TimeSlice.PerSecond));
		}
		for (int num = 0; num != instance.deltaAttribute.Modifiers.Count; num++)
		{
			AttributeModifier attributeModifier = instance.deltaAttribute.Modifiers[num];
			float modifierContribution = instance.deltaAttribute.GetModifierContribution(attributeModifier);
			text = text + "\n" + string.Format(UI.MODIFIER_ITEM_TEMPLATE, attributeModifier.GetDescription(), this.formatter.GetFormattedValue(this.ToPercent(modifierContribution, instance), this.formatter.DeltaTimeSlice));
		}
		return text;
	}

	// Token: 0x06004F6E RID: 20334 RVA: 0x001C4F71 File Offset: 0x001C3171
	public string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.formatter.GetFormattedAttribute(instance);
	}

	// Token: 0x06004F6F RID: 20335 RVA: 0x001C4F7F File Offset: 0x001C317F
	public string GetFormattedModifier(AttributeModifier modifier)
	{
		if (modifier.IsMultiplier)
		{
			return GameUtil.GetFormattedPercent(modifier.Value * 100f, GameUtil.TimeSlice.None);
		}
		return this.formatter.GetFormattedModifier(modifier);
	}

	// Token: 0x06004F70 RID: 20336 RVA: 0x001C4FA8 File Offset: 0x001C31A8
	public string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice)
	{
		return this.formatter.GetFormattedValue(value, timeSlice);
	}

	// Token: 0x06004F71 RID: 20337 RVA: 0x001C4FB7 File Offset: 0x001C31B7
	protected float ToPercent(float value, AmountInstance instance)
	{
		return 100f * value / instance.GetMax();
	}

	// Token: 0x0400354A RID: 13642
	protected StandardAttributeFormatter formatter;
}
