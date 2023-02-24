using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000A3E RID: 2622
public class QualityOfLifeAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x06004F94 RID: 20372 RVA: 0x001C585E File Offset: 0x001C3A5E
	public QualityOfLifeAttributeFormatter()
		: base(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None)
	{
	}

	// Token: 0x06004F95 RID: 20373 RVA: 0x001C5868 File Offset: 0x001C3A68
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(instance.gameObject);
		return string.Format(DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.DESC_FORMAT, this.GetFormattedValue(instance.GetTotalDisplayValue(), GameUtil.TimeSlice.None), this.GetFormattedValue(attributeInstance.GetTotalDisplayValue(), GameUtil.TimeSlice.None));
	}

	// Token: 0x06004F96 RID: 20374 RVA: 0x001C58BC File Offset: 0x001C3ABC
	public override string GetTooltip(Klei.AI.Attribute master, AttributeInstance instance)
	{
		string text = base.GetTooltip(master, instance);
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(instance.gameObject);
		text = text + "\n\n" + string.Format(DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.TOOLTIP_EXPECTATION, this.GetFormattedValue(attributeInstance.GetTotalDisplayValue(), GameUtil.TimeSlice.None));
		if (instance.GetTotalDisplayValue() - attributeInstance.GetTotalDisplayValue() >= 0f)
		{
			text = text + "\n\n" + DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.TOOLTIP_EXPECTATION_OVER;
		}
		else
		{
			text = text + "\n\n" + DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.TOOLTIP_EXPECTATION_UNDER;
		}
		return text;
	}
}
