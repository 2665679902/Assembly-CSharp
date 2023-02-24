using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000A33 RID: 2611
public class StandardAmountDisplayer : IAmountDisplayer
{
	// Token: 0x170005CB RID: 1483
	// (get) Token: 0x06004F5C RID: 20316 RVA: 0x001C4B64 File Offset: 0x001C2D64
	public IAttributeFormatter Formatter
	{
		get
		{
			return this.formatter;
		}
	}

	// Token: 0x170005CC RID: 1484
	// (get) Token: 0x06004F5D RID: 20317 RVA: 0x001C4B6C File Offset: 0x001C2D6C
	// (set) Token: 0x06004F5E RID: 20318 RVA: 0x001C4B79 File Offset: 0x001C2D79
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

	// Token: 0x06004F5F RID: 20319 RVA: 0x001C4B87 File Offset: 0x001C2D87
	public StandardAmountDisplayer(GameUtil.UnitClass unitClass, GameUtil.TimeSlice deltaTimeSlice, StandardAttributeFormatter formatter = null, GameUtil.IdentityDescriptorTense tense = GameUtil.IdentityDescriptorTense.Normal)
	{
		this.tense = tense;
		if (formatter != null)
		{
			this.formatter = formatter;
			return;
		}
		this.formatter = new StandardAttributeFormatter(unitClass, deltaTimeSlice);
	}

	// Token: 0x06004F60 RID: 20320 RVA: 0x001C4BB0 File Offset: 0x001C2DB0
	public virtual string GetValueString(Amount master, AmountInstance instance)
	{
		if (!master.showMax)
		{
			return this.formatter.GetFormattedValue(instance.value, GameUtil.TimeSlice.None);
		}
		return string.Format("{0} / {1}", this.formatter.GetFormattedValue(instance.value, GameUtil.TimeSlice.None), this.formatter.GetFormattedValue(instance.GetMax(), GameUtil.TimeSlice.None));
	}

	// Token: 0x06004F61 RID: 20321 RVA: 0x001C4C06 File Offset: 0x001C2E06
	public virtual string GetDescription(Amount master, AmountInstance instance)
	{
		return string.Format("{0}: {1}", master.Name, this.GetValueString(master, instance));
	}

	// Token: 0x06004F62 RID: 20322 RVA: 0x001C4C20 File Offset: 0x001C2E20
	public virtual string GetTooltip(Amount master, AmountInstance instance)
	{
		string text = "";
		if (master.description.IndexOf("{1}") > -1)
		{
			text += string.Format(master.description, this.formatter.GetFormattedValue(instance.value, GameUtil.TimeSlice.None), GameUtil.GetIdentityDescriptor(instance.gameObject, this.tense));
		}
		else
		{
			text += string.Format(master.description, this.formatter.GetFormattedValue(instance.value, GameUtil.TimeSlice.None));
		}
		text += "\n\n";
		if (this.formatter.DeltaTimeSlice == GameUtil.TimeSlice.PerCycle)
		{
			text += string.Format(UI.CHANGEPERCYCLE, this.formatter.GetFormattedValue(instance.deltaAttribute.GetTotalDisplayValue(), GameUtil.TimeSlice.PerCycle));
		}
		else if (this.formatter.DeltaTimeSlice == GameUtil.TimeSlice.PerSecond)
		{
			text += string.Format(UI.CHANGEPERSECOND, this.formatter.GetFormattedValue(instance.deltaAttribute.GetTotalDisplayValue(), GameUtil.TimeSlice.PerSecond));
		}
		for (int num = 0; num != instance.deltaAttribute.Modifiers.Count; num++)
		{
			AttributeModifier attributeModifier = instance.deltaAttribute.Modifiers[num];
			text = text + "\n" + string.Format(UI.MODIFIER_ITEM_TEMPLATE, attributeModifier.GetDescription(), this.formatter.GetFormattedModifier(attributeModifier));
		}
		return text;
	}

	// Token: 0x06004F63 RID: 20323 RVA: 0x001C4D80 File Offset: 0x001C2F80
	public string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.formatter.GetFormattedAttribute(instance);
	}

	// Token: 0x06004F64 RID: 20324 RVA: 0x001C4D8E File Offset: 0x001C2F8E
	public string GetFormattedModifier(AttributeModifier modifier)
	{
		return this.formatter.GetFormattedModifier(modifier);
	}

	// Token: 0x06004F65 RID: 20325 RVA: 0x001C4D9C File Offset: 0x001C2F9C
	public string GetFormattedValue(float value, GameUtil.TimeSlice time_slice)
	{
		return this.formatter.GetFormattedValue(value, time_slice);
	}

	// Token: 0x04003548 RID: 13640
	protected StandardAttributeFormatter formatter;

	// Token: 0x04003549 RID: 13641
	public GameUtil.IdentityDescriptorTense tense;
}
