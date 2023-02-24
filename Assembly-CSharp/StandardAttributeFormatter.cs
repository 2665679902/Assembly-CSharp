using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000A3B RID: 2619
public class StandardAttributeFormatter : IAttributeFormatter
{
	// Token: 0x170005D0 RID: 1488
	// (get) Token: 0x06004F84 RID: 20356 RVA: 0x001C54CC File Offset: 0x001C36CC
	// (set) Token: 0x06004F85 RID: 20357 RVA: 0x001C54D4 File Offset: 0x001C36D4
	public GameUtil.TimeSlice DeltaTimeSlice { get; set; }

	// Token: 0x06004F86 RID: 20358 RVA: 0x001C54DD File Offset: 0x001C36DD
	public StandardAttributeFormatter(GameUtil.UnitClass unitClass, GameUtil.TimeSlice deltaTimeSlice)
	{
		this.unitClass = unitClass;
		this.DeltaTimeSlice = deltaTimeSlice;
	}

	// Token: 0x06004F87 RID: 20359 RVA: 0x001C54F3 File Offset: 0x001C36F3
	public virtual string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.GetFormattedValue(instance.GetTotalDisplayValue(), GameUtil.TimeSlice.None);
	}

	// Token: 0x06004F88 RID: 20360 RVA: 0x001C5502 File Offset: 0x001C3702
	public virtual string GetFormattedModifier(AttributeModifier modifier)
	{
		return this.GetFormattedValue(modifier.Value, this.DeltaTimeSlice);
	}

	// Token: 0x06004F89 RID: 20361 RVA: 0x001C5518 File Offset: 0x001C3718
	public virtual string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		switch (this.unitClass)
		{
		case GameUtil.UnitClass.SimpleInteger:
			return GameUtil.GetFormattedInt(value, timeSlice);
		case GameUtil.UnitClass.Temperature:
			return GameUtil.GetFormattedTemperature(value, timeSlice, (timeSlice == GameUtil.TimeSlice.None) ? GameUtil.TemperatureInterpretation.Absolute : GameUtil.TemperatureInterpretation.Relative, true, false);
		case GameUtil.UnitClass.Mass:
			return GameUtil.GetFormattedMass(value, timeSlice, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
		case GameUtil.UnitClass.Calories:
			return GameUtil.GetFormattedCalories(value, timeSlice, true);
		case GameUtil.UnitClass.Percent:
			return GameUtil.GetFormattedPercent(value, timeSlice);
		case GameUtil.UnitClass.Distance:
			return GameUtil.GetFormattedDistance(value);
		case GameUtil.UnitClass.Disease:
			return GameUtil.GetFormattedDiseaseAmount(Mathf.RoundToInt(value), GameUtil.TimeSlice.None);
		case GameUtil.UnitClass.Radiation:
			return GameUtil.GetFormattedRads(value, timeSlice);
		case GameUtil.UnitClass.Energy:
			return GameUtil.GetFormattedJoules(value, "F1", timeSlice);
		case GameUtil.UnitClass.Power:
			return GameUtil.GetFormattedWattage(value, GameUtil.WattageFormatterUnit.Automatic, true);
		case GameUtil.UnitClass.Lux:
			return GameUtil.GetFormattedLux(Mathf.FloorToInt(value));
		case GameUtil.UnitClass.Time:
			return GameUtil.GetFormattedCycles(value, "F1", false);
		case GameUtil.UnitClass.Seconds:
			return GameUtil.GetFormattedTime(value, "F0");
		case GameUtil.UnitClass.Cycles:
			return GameUtil.GetFormattedCycles(value * 600f, "F1", false);
		}
		return GameUtil.GetFormattedSimple(value, timeSlice, null);
	}

	// Token: 0x06004F8A RID: 20362 RVA: 0x001C561E File Offset: 0x001C381E
	public virtual string GetTooltipDescription(Klei.AI.Attribute master)
	{
		return master.Description;
	}

	// Token: 0x06004F8B RID: 20363 RVA: 0x001C5628 File Offset: 0x001C3828
	public virtual string GetTooltip(Klei.AI.Attribute master, AttributeInstance instance)
	{
		List<AttributeModifier> list = new List<AttributeModifier>();
		for (int i = 0; i < instance.Modifiers.Count; i++)
		{
			list.Add(instance.Modifiers[i]);
		}
		return this.GetTooltip(master, list, instance.GetComponent<AttributeConverters>());
	}

	// Token: 0x06004F8C RID: 20364 RVA: 0x001C5674 File Offset: 0x001C3874
	public string GetTooltip(Klei.AI.Attribute master, List<AttributeModifier> modifiers, AttributeConverters converters)
	{
		string text = this.GetTooltipDescription(master);
		text += string.Format(DUPLICANTS.ATTRIBUTES.TOTAL_VALUE, this.GetFormattedValue(AttributeInstance.GetTotalDisplayValue(master, modifiers), GameUtil.TimeSlice.None), master.Name);
		if (master.BaseValue != 0f)
		{
			text += string.Format(DUPLICANTS.ATTRIBUTES.BASE_VALUE, master.BaseValue);
		}
		List<AttributeModifier> list = new List<AttributeModifier>(modifiers);
		list.Sort((AttributeModifier p1, AttributeModifier p2) => p2.Value.CompareTo(p1.Value));
		for (int num = 0; num != list.Count; num++)
		{
			AttributeModifier attributeModifier = list[num];
			string formattedString = attributeModifier.GetFormattedString();
			if (formattedString != null)
			{
				text += string.Format(DUPLICANTS.ATTRIBUTES.MODIFIER_ENTRY, attributeModifier.GetDescription(), formattedString);
			}
		}
		string text2 = "";
		if (converters != null && master.converters.Count > 0)
		{
			foreach (AttributeConverterInstance attributeConverterInstance in converters.converters)
			{
				if (attributeConverterInstance.converter.attribute == master)
				{
					string text3 = attributeConverterInstance.DescriptionFromAttribute(attributeConverterInstance.Evaluate(), attributeConverterInstance.gameObject);
					if (text3 != null)
					{
						text2 = text2 + "\n" + text3;
					}
				}
			}
		}
		if (text2.Length > 0)
		{
			text = text + "\n" + text2;
		}
		return text;
	}

	// Token: 0x0400354B RID: 13643
	public GameUtil.UnitClass unitClass;
}
