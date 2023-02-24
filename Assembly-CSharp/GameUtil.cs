using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000788 RID: 1928
public static class GameUtil
{
	// Token: 0x0600354F RID: 13647 RVA: 0x00126794 File Offset: 0x00124994
	public static string GetTemperatureUnitSuffix()
	{
		GameUtil.TemperatureUnit temperatureUnit = GameUtil.temperatureUnit;
		string text;
		if (temperatureUnit != GameUtil.TemperatureUnit.Celsius)
		{
			if (temperatureUnit != GameUtil.TemperatureUnit.Fahrenheit)
			{
				text = UI.UNITSUFFIXES.TEMPERATURE.KELVIN;
			}
			else
			{
				text = UI.UNITSUFFIXES.TEMPERATURE.FAHRENHEIT;
			}
		}
		else
		{
			text = UI.UNITSUFFIXES.TEMPERATURE.CELSIUS;
		}
		return text;
	}

	// Token: 0x06003550 RID: 13648 RVA: 0x001267D6 File Offset: 0x001249D6
	private static string AddTemperatureUnitSuffix(string text)
	{
		return text + GameUtil.GetTemperatureUnitSuffix();
	}

	// Token: 0x06003551 RID: 13649 RVA: 0x001267E3 File Offset: 0x001249E3
	public static float GetTemperatureConvertedFromKelvin(float temperature, GameUtil.TemperatureUnit targetUnit)
	{
		if (targetUnit == GameUtil.TemperatureUnit.Celsius)
		{
			return temperature - 273.15f;
		}
		if (targetUnit != GameUtil.TemperatureUnit.Fahrenheit)
		{
			return temperature;
		}
		return temperature * 1.8f - 459.67f;
	}

	// Token: 0x06003552 RID: 13650 RVA: 0x00126808 File Offset: 0x00124A08
	public static float GetConvertedTemperature(float temperature, bool roundOutput = false)
	{
		GameUtil.TemperatureUnit temperatureUnit = GameUtil.temperatureUnit;
		if (temperatureUnit != GameUtil.TemperatureUnit.Celsius)
		{
			if (temperatureUnit != GameUtil.TemperatureUnit.Fahrenheit)
			{
				if (!roundOutput)
				{
					return temperature;
				}
				return Mathf.Round(temperature);
			}
			else
			{
				float num = temperature * 1.8f - 459.67f;
				if (!roundOutput)
				{
					return num;
				}
				return Mathf.Round(num);
			}
		}
		else
		{
			float num = temperature - 273.15f;
			if (!roundOutput)
			{
				return num;
			}
			return Mathf.Round(num);
		}
	}

	// Token: 0x06003553 RID: 13651 RVA: 0x00126863 File Offset: 0x00124A63
	public static float GetTemperatureConvertedToKelvin(float temperature, GameUtil.TemperatureUnit fromUnit)
	{
		if (fromUnit == GameUtil.TemperatureUnit.Celsius)
		{
			return temperature + 273.15f;
		}
		if (fromUnit != GameUtil.TemperatureUnit.Fahrenheit)
		{
			return temperature;
		}
		return (temperature + 459.67f) * 5f / 9f;
	}

	// Token: 0x06003554 RID: 13652 RVA: 0x0012688C File Offset: 0x00124A8C
	public static float GetTemperatureConvertedToKelvin(float temperature)
	{
		GameUtil.TemperatureUnit temperatureUnit = GameUtil.temperatureUnit;
		if (temperatureUnit == GameUtil.TemperatureUnit.Celsius)
		{
			return temperature + 273.15f;
		}
		if (temperatureUnit != GameUtil.TemperatureUnit.Fahrenheit)
		{
			return temperature;
		}
		return (temperature + 459.67f) * 5f / 9f;
	}

	// Token: 0x06003555 RID: 13653 RVA: 0x001268C8 File Offset: 0x00124AC8
	private static float GetConvertedTemperatureDelta(float kelvin_delta)
	{
		switch (GameUtil.temperatureUnit)
		{
		case GameUtil.TemperatureUnit.Celsius:
			return kelvin_delta;
		case GameUtil.TemperatureUnit.Fahrenheit:
			return kelvin_delta * 1.8f;
		case GameUtil.TemperatureUnit.Kelvin:
			return kelvin_delta;
		default:
			return kelvin_delta;
		}
	}

	// Token: 0x06003556 RID: 13654 RVA: 0x001268FC File Offset: 0x00124AFC
	public static float ApplyTimeSlice(float val, GameUtil.TimeSlice timeSlice)
	{
		if (timeSlice == GameUtil.TimeSlice.PerCycle)
		{
			return val * 600f;
		}
		return val;
	}

	// Token: 0x06003557 RID: 13655 RVA: 0x0012690B File Offset: 0x00124B0B
	public static float ApplyTimeSlice(int val, GameUtil.TimeSlice timeSlice)
	{
		if (timeSlice == GameUtil.TimeSlice.PerCycle)
		{
			return (float)val * 600f;
		}
		return (float)val;
	}

	// Token: 0x06003558 RID: 13656 RVA: 0x0012691C File Offset: 0x00124B1C
	public static string AddTimeSliceText(string text, GameUtil.TimeSlice timeSlice)
	{
		switch (timeSlice)
		{
		case GameUtil.TimeSlice.PerSecond:
			return text + UI.UNITSUFFIXES.PERSECOND;
		case GameUtil.TimeSlice.PerCycle:
			return text + UI.UNITSUFFIXES.PERCYCLE;
		}
		return text;
	}

	// Token: 0x06003559 RID: 13657 RVA: 0x00126959 File Offset: 0x00124B59
	public static string AddPositiveSign(string text, bool positive)
	{
		if (positive)
		{
			return string.Format(UI.POSITIVE_FORMAT, text);
		}
		return text;
	}

	// Token: 0x0600355A RID: 13658 RVA: 0x00126970 File Offset: 0x00124B70
	public static float AttributeSkillToAlpha(AttributeInstance attributeInstance)
	{
		return Mathf.Min(attributeInstance.GetTotalValue() / 10f, 1f);
	}

	// Token: 0x0600355B RID: 13659 RVA: 0x00126988 File Offset: 0x00124B88
	public static float AttributeSkillToAlpha(float attributeSkill)
	{
		return Mathf.Min(attributeSkill / 10f, 1f);
	}

	// Token: 0x0600355C RID: 13660 RVA: 0x0012699B File Offset: 0x00124B9B
	public static float AptitudeToAlpha(float aptitude)
	{
		return Mathf.Min(aptitude / 10f, 1f);
	}

	// Token: 0x0600355D RID: 13661 RVA: 0x001269AE File Offset: 0x00124BAE
	public static float GetThermalEnergy(PrimaryElement pe)
	{
		return pe.Temperature * pe.Mass * pe.Element.specificHeatCapacity;
	}

	// Token: 0x0600355E RID: 13662 RVA: 0x001269C9 File Offset: 0x00124BC9
	public static float CalculateTemperatureChange(float shc, float mass, float kilowatts)
	{
		return kilowatts / (shc * mass);
	}

	// Token: 0x0600355F RID: 13663 RVA: 0x001269D0 File Offset: 0x00124BD0
	public static void DeltaThermalEnergy(PrimaryElement pe, float kilowatts, float targetTemperature)
	{
		float num = GameUtil.CalculateTemperatureChange(pe.Element.specificHeatCapacity, pe.Mass, kilowatts);
		float num2 = pe.Temperature + num;
		if (targetTemperature > pe.Temperature)
		{
			num2 = Mathf.Clamp(num2, pe.Temperature, targetTemperature);
		}
		else
		{
			num2 = Mathf.Clamp(num2, targetTemperature, pe.Temperature);
		}
		pe.Temperature = num2;
	}

	// Token: 0x06003560 RID: 13664 RVA: 0x00126A2C File Offset: 0x00124C2C
	public static BindingEntry ActionToBinding(global::Action action)
	{
		foreach (BindingEntry bindingEntry in GameInputMapping.KeyBindings)
		{
			if (bindingEntry.mAction == action)
			{
				return bindingEntry;
			}
		}
		throw new ArgumentException(action.ToString() + " is not bound in GameInputBindings");
	}

	// Token: 0x06003561 RID: 13665 RVA: 0x00126A7C File Offset: 0x00124C7C
	public static string GetIdentityDescriptor(GameObject go, GameUtil.IdentityDescriptorTense tense = GameUtil.IdentityDescriptorTense.Normal)
	{
		if (go.GetComponent<MinionIdentity>())
		{
			switch (tense)
			{
			case GameUtil.IdentityDescriptorTense.Normal:
				return DUPLICANTS.STATS.SUBJECTS.DUPLICANT;
			case GameUtil.IdentityDescriptorTense.Possessive:
				return DUPLICANTS.STATS.SUBJECTS.DUPLICANT_POSSESSIVE;
			case GameUtil.IdentityDescriptorTense.Plural:
				return DUPLICANTS.STATS.SUBJECTS.DUPLICANT_PLURAL;
			}
		}
		else if (go.GetComponent<CreatureBrain>())
		{
			switch (tense)
			{
			case GameUtil.IdentityDescriptorTense.Normal:
				return DUPLICANTS.STATS.SUBJECTS.CREATURE;
			case GameUtil.IdentityDescriptorTense.Possessive:
				return DUPLICANTS.STATS.SUBJECTS.CREATURE_POSSESSIVE;
			case GameUtil.IdentityDescriptorTense.Plural:
				return DUPLICANTS.STATS.SUBJECTS.CREATURE_PLURAL;
			}
		}
		else
		{
			switch (tense)
			{
			case GameUtil.IdentityDescriptorTense.Normal:
				return DUPLICANTS.STATS.SUBJECTS.PLANT;
			case GameUtil.IdentityDescriptorTense.Possessive:
				return DUPLICANTS.STATS.SUBJECTS.PLANT_POSESSIVE;
			case GameUtil.IdentityDescriptorTense.Plural:
				return DUPLICANTS.STATS.SUBJECTS.PLANT_PLURAL;
			}
		}
		return "";
	}

	// Token: 0x06003562 RID: 13666 RVA: 0x00126B4A File Offset: 0x00124D4A
	public static float GetEnergyInPrimaryElement(PrimaryElement element)
	{
		return 0.001f * (element.Temperature * (element.Mass * 1000f * element.Element.specificHeatCapacity));
	}

	// Token: 0x06003563 RID: 13667 RVA: 0x00126B74 File Offset: 0x00124D74
	public static float EnergyToTemperatureDelta(float kilojoules, PrimaryElement element)
	{
		global::Debug.Assert(element.Mass > 0f);
		float num = Mathf.Max(GameUtil.GetEnergyInPrimaryElement(element) - kilojoules, 1f);
		float temperature = element.Temperature;
		return num / (0.001f * (element.Mass * (element.Element.specificHeatCapacity * 1000f))) - temperature;
	}

	// Token: 0x06003564 RID: 13668 RVA: 0x00126BCD File Offset: 0x00124DCD
	public static float CalculateEnergyDeltaForElement(PrimaryElement element, float startTemp, float endTemp)
	{
		return GameUtil.CalculateEnergyDeltaForElementChange(element.Mass, element.Element.specificHeatCapacity, startTemp, endTemp);
	}

	// Token: 0x06003565 RID: 13669 RVA: 0x00126BE7 File Offset: 0x00124DE7
	public static float CalculateEnergyDeltaForElementChange(float mass, float shc, float startTemp, float endTemp)
	{
		return (endTemp - startTemp) * mass * shc;
	}

	// Token: 0x06003566 RID: 13670 RVA: 0x00126BF0 File Offset: 0x00124DF0
	public static float GetFinalTemperature(float t1, float m1, float t2, float m2)
	{
		float num = m1 + m2;
		float num2 = (t1 * m1 + t2 * m2) / num;
		float num3 = Mathf.Min(t1, t2);
		float num4 = Mathf.Max(t1, t2);
		num2 = Mathf.Clamp(num2, num3, num4);
		if (float.IsNaN(num2) || float.IsInfinity(num2))
		{
			global::Debug.LogError(string.Format("Calculated an invalid temperature: t1={0}, m1={1}, t2={2}, m2={3}, min_temp={4}, max_temp={5}", new object[] { t1, m1, t2, m2, num3, num4 }));
		}
		return num2;
	}

	// Token: 0x06003567 RID: 13671 RVA: 0x00126C80 File Offset: 0x00124E80
	public static void ForceConduction(PrimaryElement a, PrimaryElement b, float dt)
	{
		float num = a.Temperature * a.Element.specificHeatCapacity * a.Mass;
		float num2 = b.Temperature * b.Element.specificHeatCapacity * b.Mass;
		float num3 = Math.Min(a.Element.thermalConductivity, b.Element.thermalConductivity);
		float num4 = Math.Min(a.Mass, b.Mass);
		float num5 = (b.Temperature - a.Temperature) * (num3 * num4) * dt;
		float num6 = (num + num2) / (a.Element.specificHeatCapacity * a.Mass + b.Element.specificHeatCapacity * b.Mass);
		float num7 = Math.Abs((num6 - a.Temperature) * a.Element.specificHeatCapacity * a.Mass);
		float num8 = Math.Abs((num6 - b.Temperature) * b.Element.specificHeatCapacity * b.Mass);
		float num9 = Math.Min(num7, num8);
		num5 = Math.Min(num5, num9);
		num5 = Math.Max(num5, -num9);
		a.Temperature = (num + num5) / a.Element.specificHeatCapacity / a.Mass;
		b.Temperature = (num2 - num5) / b.Element.specificHeatCapacity / b.Mass;
	}

	// Token: 0x06003568 RID: 13672 RVA: 0x00126DCC File Offset: 0x00124FCC
	public static string FloatToString(float f, string format = null)
	{
		if (float.IsPositiveInfinity(f))
		{
			return UI.POS_INFINITY;
		}
		if (float.IsNegativeInfinity(f))
		{
			return UI.NEG_INFINITY;
		}
		return f.ToString(format);
	}

	// Token: 0x06003569 RID: 13673 RVA: 0x00126DFC File Offset: 0x00124FFC
	public static string GetFloatWithDecimalPoint(float f)
	{
		string text;
		if (f == 0f)
		{
			text = "0";
		}
		else if (Mathf.Abs(f) < 1f)
		{
			text = "#,##0.#";
		}
		else
		{
			text = "#,###.#";
		}
		return GameUtil.FloatToString(f, text);
	}

	// Token: 0x0600356A RID: 13674 RVA: 0x00126E44 File Offset: 0x00125044
	public static string GetStandardFloat(float f)
	{
		string text;
		if (f == 0f)
		{
			text = "0";
		}
		else if (Mathf.Abs(f) < 1f)
		{
			text = "#,##0.#";
		}
		else if (Mathf.Abs(f) < 10f)
		{
			text = "#,###.#";
		}
		else
		{
			text = "#,###";
		}
		return GameUtil.FloatToString(f, text);
	}

	// Token: 0x0600356B RID: 13675 RVA: 0x00126EA0 File Offset: 0x001250A0
	public static string GetStandardPercentageFloat(float f, bool allowHundredths = false)
	{
		string text;
		if (Mathf.Abs(f) == 0f)
		{
			text = "0";
		}
		else if (Mathf.Abs(f) < 0.1f && allowHundredths)
		{
			text = "##0.##";
		}
		else if (Mathf.Abs(f) < 1f)
		{
			text = "##0.#";
		}
		else
		{
			text = "##0";
		}
		return GameUtil.FloatToString(f, text);
	}

	// Token: 0x0600356C RID: 13676 RVA: 0x00126F04 File Offset: 0x00125104
	public static string GetUnitFormattedName(GameObject go, bool upperName = false)
	{
		KPrefabID component = go.GetComponent<KPrefabID>();
		if (component != null && Assets.IsTagCountable(component.PrefabTag))
		{
			PrimaryElement component2 = go.GetComponent<PrimaryElement>();
			return GameUtil.GetUnitFormattedName(go.GetProperName(), component2.Units, upperName);
		}
		if (!upperName)
		{
			return go.GetProperName();
		}
		return StringFormatter.ToUpper(go.GetProperName());
	}

	// Token: 0x0600356D RID: 13677 RVA: 0x00126F5D File Offset: 0x0012515D
	public static string GetUnitFormattedName(string name, float count, bool upperName = false)
	{
		if (upperName)
		{
			name = name.ToUpper();
		}
		return StringFormatter.Replace(UI.NAME_WITH_UNITS, "{0}", name).Replace("{1}", string.Format("{0:0.##}", count));
	}

	// Token: 0x0600356E RID: 13678 RVA: 0x00126F9C File Offset: 0x0012519C
	public static string GetFormattedUnits(float units, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool displaySuffix = true, string floatFormatOverride = "")
	{
		string text = ((units == 1f) ? UI.UNITSUFFIXES.UNIT : UI.UNITSUFFIXES.UNITS);
		units = GameUtil.ApplyTimeSlice(units, timeSlice);
		string text2 = GameUtil.GetStandardFloat(units);
		if (!floatFormatOverride.IsNullOrWhiteSpace())
		{
			text2 = string.Format(floatFormatOverride, units);
		}
		if (displaySuffix)
		{
			text2 += text;
		}
		return GameUtil.AddTimeSliceText(text2, timeSlice);
	}

	// Token: 0x0600356F RID: 13679 RVA: 0x00126FFC File Offset: 0x001251FC
	public static string GetFormattedRocketRange(float range, GameUtil.TimeSlice timeSlice, bool displaySuffix = true)
	{
		if (timeSlice == GameUtil.TimeSlice.PerCycle)
		{
			return range.ToString("N1") + (displaySuffix ? (" " + UI.CLUSTERMAP.TILES_PER_CYCLE) : "");
		}
		float num = range / 600f;
		if (Mathf.Approximately(num, Mathf.Round(num)))
		{
			num = Mathf.Round(num);
		}
		return Mathf.Floor(num).ToString() + (displaySuffix ? (" " + UI.CLUSTERMAP.TILES) : "");
	}

	// Token: 0x06003570 RID: 13680 RVA: 0x0012708B File Offset: 0x0012528B
	public static string ApplyBoldString(string source)
	{
		return "<b>" + source + "</b>";
	}

	// Token: 0x06003571 RID: 13681 RVA: 0x001270A0 File Offset: 0x001252A0
	public static float GetRoundedTemperatureInKelvin(float kelvin)
	{
		float num = 0f;
		switch (GameUtil.temperatureUnit)
		{
		case GameUtil.TemperatureUnit.Celsius:
			num = GameUtil.GetTemperatureConvertedToKelvin(Mathf.Round(GameUtil.GetConvertedTemperature(Mathf.Round(kelvin), true)));
			break;
		case GameUtil.TemperatureUnit.Fahrenheit:
			num = GameUtil.GetTemperatureConvertedToKelvin((float)Mathf.RoundToInt(GameUtil.GetTemperatureConvertedFromKelvin(kelvin, GameUtil.TemperatureUnit.Fahrenheit)), GameUtil.TemperatureUnit.Fahrenheit);
			break;
		case GameUtil.TemperatureUnit.Kelvin:
			num = (float)Mathf.RoundToInt(kelvin);
			break;
		}
		return num;
	}

	// Token: 0x06003572 RID: 13682 RVA: 0x00127108 File Offset: 0x00125308
	public static string GetFormattedTemperature(float temp, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation interpretation = GameUtil.TemperatureInterpretation.Absolute, bool displayUnits = true, bool roundInDestinationFormat = false)
	{
		if (interpretation != GameUtil.TemperatureInterpretation.Absolute)
		{
			if (interpretation != GameUtil.TemperatureInterpretation.Relative)
			{
			}
			temp = GameUtil.GetConvertedTemperatureDelta(temp);
		}
		else
		{
			temp = GameUtil.GetConvertedTemperature(temp, roundInDestinationFormat);
		}
		temp = GameUtil.ApplyTimeSlice(temp, timeSlice);
		string text;
		if (Mathf.Abs(temp) < 0.1f)
		{
			text = GameUtil.FloatToString(temp, "##0.####");
		}
		else
		{
			text = GameUtil.FloatToString(temp, "##0.#");
		}
		if (displayUnits)
		{
			text = GameUtil.AddTemperatureUnitSuffix(text);
		}
		return GameUtil.AddTimeSliceText(text, timeSlice);
	}

	// Token: 0x06003573 RID: 13683 RVA: 0x0012717C File Offset: 0x0012537C
	public static string GetFormattedCaloriesForItem(Tag tag, float amount, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool forceKcal = true)
	{
		EdiblesManager.FoodInfo foodInfo = EdiblesManager.GetFoodInfo(tag.Name);
		return GameUtil.GetFormattedCalories((foodInfo != null) ? (foodInfo.CaloriesPerUnit * amount) : (-1f), timeSlice, forceKcal);
	}

	// Token: 0x06003574 RID: 13684 RVA: 0x001271B0 File Offset: 0x001253B0
	public static string GetFormattedCalories(float calories, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool forceKcal = true)
	{
		string text = UI.UNITSUFFIXES.CALORIES.CALORIE;
		if (Mathf.Abs(calories) >= 1000f || forceKcal)
		{
			calories /= 1000f;
			text = UI.UNITSUFFIXES.CALORIES.KILOCALORIE;
		}
		calories = GameUtil.ApplyTimeSlice(calories, timeSlice);
		return GameUtil.AddTimeSliceText(GameUtil.GetStandardFloat(calories) + text, timeSlice);
	}

	// Token: 0x06003575 RID: 13685 RVA: 0x0012720B File Offset: 0x0012540B
	public static string GetFormattedPlantGrowth(float percent, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		percent = GameUtil.ApplyTimeSlice(percent, timeSlice);
		return GameUtil.AddTimeSliceText(GameUtil.GetStandardPercentageFloat(percent, true) + UI.UNITSUFFIXES.PERCENT + " " + UI.UNITSUFFIXES.GROWTH, timeSlice);
	}

	// Token: 0x06003576 RID: 13686 RVA: 0x00127241 File Offset: 0x00125441
	public static string GetFormattedPercent(float percent, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		percent = GameUtil.ApplyTimeSlice(percent, timeSlice);
		return GameUtil.AddTimeSliceText(GameUtil.GetStandardPercentageFloat(percent, true) + UI.UNITSUFFIXES.PERCENT, timeSlice);
	}

	// Token: 0x06003577 RID: 13687 RVA: 0x00127268 File Offset: 0x00125468
	public static string GetFormattedRoundedJoules(float joules)
	{
		if (Mathf.Abs(joules) > 1000f)
		{
			return GameUtil.FloatToString(joules / 1000f, "F1") + UI.UNITSUFFIXES.ELECTRICAL.KILOJOULE;
		}
		return GameUtil.FloatToString(joules, "F1") + UI.UNITSUFFIXES.ELECTRICAL.JOULE;
	}

	// Token: 0x06003578 RID: 13688 RVA: 0x001272C0 File Offset: 0x001254C0
	public static string GetFormattedJoules(float joules, string floatFormat = "F1", GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		if (timeSlice == GameUtil.TimeSlice.PerSecond)
		{
			return GameUtil.GetFormattedWattage(joules, GameUtil.WattageFormatterUnit.Automatic, true);
		}
		joules = GameUtil.ApplyTimeSlice(joules, timeSlice);
		string text;
		if (Math.Abs(joules) > 1000000f)
		{
			text = GameUtil.FloatToString(joules / 1000000f, floatFormat) + UI.UNITSUFFIXES.ELECTRICAL.MEGAJOULE;
		}
		else if (Mathf.Abs(joules) > 1000f)
		{
			text = GameUtil.FloatToString(joules / 1000f, floatFormat) + UI.UNITSUFFIXES.ELECTRICAL.KILOJOULE;
		}
		else
		{
			text = GameUtil.FloatToString(joules, floatFormat) + UI.UNITSUFFIXES.ELECTRICAL.JOULE;
		}
		return GameUtil.AddTimeSliceText(text, timeSlice);
	}

	// Token: 0x06003579 RID: 13689 RVA: 0x00127359 File Offset: 0x00125559
	public static string GetFormattedRads(float rads, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		rads = GameUtil.ApplyTimeSlice(rads, timeSlice);
		return GameUtil.AddTimeSliceText(GameUtil.GetStandardFloat(rads) + UI.UNITSUFFIXES.RADIATION.RADS, timeSlice);
	}

	// Token: 0x0600357A RID: 13690 RVA: 0x00127380 File Offset: 0x00125580
	public static string GetFormattedHighEnergyParticles(float units, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, bool displayUnits = true)
	{
		string text = ((units == 1f) ? UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLE : UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES);
		units = GameUtil.ApplyTimeSlice(units, timeSlice);
		return GameUtil.AddTimeSliceText(displayUnits ? (GameUtil.GetFloatWithDecimalPoint(units) + text) : GameUtil.GetFloatWithDecimalPoint(units), timeSlice);
	}

	// Token: 0x0600357B RID: 13691 RVA: 0x001273D0 File Offset: 0x001255D0
	public static string GetFormattedWattage(float watts, GameUtil.WattageFormatterUnit unit = GameUtil.WattageFormatterUnit.Automatic, bool displayUnits = true)
	{
		LocString locString = "";
		switch (unit)
		{
		case GameUtil.WattageFormatterUnit.Watts:
			locString = UI.UNITSUFFIXES.ELECTRICAL.WATT;
			break;
		case GameUtil.WattageFormatterUnit.Kilowatts:
			watts /= 1000f;
			locString = UI.UNITSUFFIXES.ELECTRICAL.KILOWATT;
			break;
		case GameUtil.WattageFormatterUnit.Automatic:
			if (Mathf.Abs(watts) > 1000f)
			{
				watts /= 1000f;
				locString = UI.UNITSUFFIXES.ELECTRICAL.KILOWATT;
			}
			else
			{
				locString = UI.UNITSUFFIXES.ELECTRICAL.WATT;
			}
			break;
		}
		if (displayUnits)
		{
			return GameUtil.FloatToString(watts, "###0.##") + locString;
		}
		return GameUtil.FloatToString(watts, "###0.##");
	}

	// Token: 0x0600357C RID: 13692 RVA: 0x00127460 File Offset: 0x00125660
	public static string GetFormattedHeatEnergy(float dtu, GameUtil.HeatEnergyFormatterUnit unit = GameUtil.HeatEnergyFormatterUnit.Automatic)
	{
		LocString locString = "";
		string text;
		switch (unit)
		{
		case GameUtil.HeatEnergyFormatterUnit.DTU_S:
			locString = UI.UNITSUFFIXES.HEAT.DTU;
			text = "###0.";
			break;
		case GameUtil.HeatEnergyFormatterUnit.KDTU_S:
			dtu /= 1000f;
			locString = UI.UNITSUFFIXES.HEAT.KDTU;
			text = "###0.##";
			break;
		default:
			if (Mathf.Abs(dtu) > 1000f)
			{
				dtu /= 1000f;
				locString = UI.UNITSUFFIXES.HEAT.KDTU;
				text = "###0.##";
			}
			else
			{
				locString = UI.UNITSUFFIXES.HEAT.DTU;
				text = "###0.";
			}
			break;
		}
		return GameUtil.FloatToString(dtu, text) + locString;
	}

	// Token: 0x0600357D RID: 13693 RVA: 0x001274F4 File Offset: 0x001256F4
	public static string GetFormattedHeatEnergyRate(float dtu_s, GameUtil.HeatEnergyFormatterUnit unit = GameUtil.HeatEnergyFormatterUnit.Automatic)
	{
		LocString locString = "";
		switch (unit)
		{
		case GameUtil.HeatEnergyFormatterUnit.DTU_S:
			locString = UI.UNITSUFFIXES.HEAT.DTU_S;
			break;
		case GameUtil.HeatEnergyFormatterUnit.KDTU_S:
			dtu_s /= 1000f;
			locString = UI.UNITSUFFIXES.HEAT.KDTU_S;
			break;
		case GameUtil.HeatEnergyFormatterUnit.Automatic:
			if (Mathf.Abs(dtu_s) > 1000f)
			{
				dtu_s /= 1000f;
				locString = UI.UNITSUFFIXES.HEAT.KDTU_S;
			}
			else
			{
				locString = UI.UNITSUFFIXES.HEAT.DTU_S;
			}
			break;
		}
		return GameUtil.FloatToString(dtu_s, "###0.##") + locString;
	}

	// Token: 0x0600357E RID: 13694 RVA: 0x00127573 File Offset: 0x00125773
	public static string GetFormattedInt(float num, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		num = GameUtil.ApplyTimeSlice(num, timeSlice);
		return GameUtil.AddTimeSliceText(GameUtil.FloatToString(num, "F0"), timeSlice);
	}

	// Token: 0x0600357F RID: 13695 RVA: 0x00127590 File Offset: 0x00125790
	public static string GetFormattedSimple(float num, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, string formatString = null)
	{
		num = GameUtil.ApplyTimeSlice(num, timeSlice);
		string text;
		if (formatString != null)
		{
			text = GameUtil.FloatToString(num, formatString);
		}
		else if (num == 0f)
		{
			text = "0";
		}
		else if (Mathf.Abs(num) < 1f)
		{
			text = GameUtil.FloatToString(num, "#,##0.##");
		}
		else if (Mathf.Abs(num) < 10f)
		{
			text = GameUtil.FloatToString(num, "#,###.##");
		}
		else
		{
			text = GameUtil.FloatToString(num, "#,###.##");
		}
		return GameUtil.AddTimeSliceText(text, timeSlice);
	}

	// Token: 0x06003580 RID: 13696 RVA: 0x00127612 File Offset: 0x00125812
	public static string GetFormattedLux(int lux)
	{
		return lux.ToString() + UI.UNITSUFFIXES.LIGHT.LUX;
	}

	// Token: 0x06003581 RID: 13697 RVA: 0x0012762C File Offset: 0x0012582C
	public static string GetLightDescription(int lux)
	{
		if (lux == 0)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.NO_LIGHT;
		}
		if (lux < 100)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.VERY_LOW_LIGHT;
		}
		if (lux < 1000)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.LOW_LIGHT;
		}
		if (lux < 10000)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.MEDIUM_LIGHT;
		}
		if (lux < 50000)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.HIGH_LIGHT;
		}
		if (lux < 100000)
		{
			return UI.OVERLAYS.LIGHTING.RANGES.VERY_HIGH_LIGHT;
		}
		return UI.OVERLAYS.LIGHTING.RANGES.MAX_LIGHT;
	}

	// Token: 0x06003582 RID: 13698 RVA: 0x001276B0 File Offset: 0x001258B0
	public static string GetRadiationDescription(float radsPerCycle)
	{
		if (radsPerCycle == 0f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.NONE;
		}
		if (radsPerCycle < 100f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.VERY_LOW;
		}
		if (radsPerCycle < 200f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.LOW;
		}
		if (radsPerCycle < 400f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.MEDIUM;
		}
		if (radsPerCycle < 2000f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.HIGH;
		}
		if (radsPerCycle < 4000f)
		{
			return UI.OVERLAYS.RADIATION.RANGES.VERY_HIGH;
		}
		return UI.OVERLAYS.RADIATION.RANGES.MAX;
	}

	// Token: 0x06003583 RID: 13699 RVA: 0x0012773C File Offset: 0x0012593C
	public static string GetFormattedByTag(Tag tag, float amount, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		if (GameTags.DisplayAsCalories.Contains(tag))
		{
			return GameUtil.GetFormattedCaloriesForItem(tag, amount, timeSlice, true);
		}
		if (GameTags.DisplayAsUnits.Contains(tag))
		{
			return GameUtil.GetFormattedUnits(amount, timeSlice, true, "");
		}
		return GameUtil.GetFormattedMass(amount, timeSlice, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
	}

	// Token: 0x06003584 RID: 13700 RVA: 0x0012778C File Offset: 0x0012598C
	public static string GetFormattedFoodQuality(int quality)
	{
		if (GameUtil.adjectives == null)
		{
			GameUtil.adjectives = LocString.GetStrings(typeof(DUPLICANTS.NEEDS.FOOD_QUALITY.ADJECTIVES));
		}
		LocString locString = ((quality >= 0) ? DUPLICANTS.NEEDS.FOOD_QUALITY.ADJECTIVE_FORMAT_POSITIVE : DUPLICANTS.NEEDS.FOOD_QUALITY.ADJECTIVE_FORMAT_NEGATIVE);
		int num = quality - DUPLICANTS.NEEDS.FOOD_QUALITY.ADJECTIVE_INDEX_OFFSET;
		num = Mathf.Clamp(num, 0, GameUtil.adjectives.Length);
		return string.Format(locString, GameUtil.adjectives[num], GameUtil.AddPositiveSign(quality.ToString(), quality > 0));
	}

	// Token: 0x06003585 RID: 13701 RVA: 0x001277FC File Offset: 0x001259FC
	public static string GetFormattedBytes(ulong amount)
	{
		string[] array = new string[]
		{
			UI.UNITSUFFIXES.INFORMATION.BYTE,
			UI.UNITSUFFIXES.INFORMATION.KILOBYTE,
			UI.UNITSUFFIXES.INFORMATION.MEGABYTE,
			UI.UNITSUFFIXES.INFORMATION.GIGABYTE,
			UI.UNITSUFFIXES.INFORMATION.TERABYTE
		};
		int num = ((amount == 0UL) ? 0 : ((int)Math.Floor(Math.Floor(Math.Log(amount)) / Math.Log(1024.0))));
		double num2 = amount / Math.Pow(1024.0, (double)num);
		global::Debug.Assert(num >= 0 && num < array.Length);
		return string.Format("{0:F} {1}", num2, array[num]);
	}

	// Token: 0x06003586 RID: 13702 RVA: 0x001278B4 File Offset: 0x00125AB4
	public static string GetFormattedInfomation(float amount, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		amount = GameUtil.ApplyTimeSlice(amount, timeSlice);
		string text = "";
		if (amount < 1024f)
		{
			text = UI.UNITSUFFIXES.INFORMATION.KILOBYTE;
		}
		else if (amount < 1048576f)
		{
			amount /= 1000f;
			text = UI.UNITSUFFIXES.INFORMATION.MEGABYTE;
		}
		else if (amount < 1.0737418E+09f)
		{
			amount /= 1048576f;
			text = UI.UNITSUFFIXES.INFORMATION.GIGABYTE;
		}
		return GameUtil.AddTimeSliceText(amount.ToString() + text, timeSlice);
	}

	// Token: 0x06003587 RID: 13703 RVA: 0x00127934 File Offset: 0x00125B34
	public static LocString GetCurrentMassUnit(bool useSmallUnit = false)
	{
		LocString locString = null;
		GameUtil.MassUnit massUnit = GameUtil.massUnit;
		if (massUnit != GameUtil.MassUnit.Kilograms)
		{
			if (massUnit == GameUtil.MassUnit.Pounds)
			{
				locString = UI.UNITSUFFIXES.MASS.POUND;
			}
		}
		else if (useSmallUnit)
		{
			locString = UI.UNITSUFFIXES.MASS.GRAM;
		}
		else
		{
			locString = UI.UNITSUFFIXES.MASS.KILOGRAM;
		}
		return locString;
	}

	// Token: 0x06003588 RID: 13704 RVA: 0x0012796C File Offset: 0x00125B6C
	public static string GetFormattedMass(float mass, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None, GameUtil.MetricMassFormat massFormat = GameUtil.MetricMassFormat.UseThreshold, bool includeSuffix = true, string floatFormat = "{0:0.#}")
	{
		if (mass == -3.4028235E+38f)
		{
			return UI.CALCULATING;
		}
		mass = GameUtil.ApplyTimeSlice(mass, timeSlice);
		string text;
		if (GameUtil.massUnit == GameUtil.MassUnit.Kilograms)
		{
			text = UI.UNITSUFFIXES.MASS.TONNE;
			if (massFormat == GameUtil.MetricMassFormat.UseThreshold)
			{
				float num = Mathf.Abs(mass);
				if (0f < num)
				{
					if (num < 5E-06f)
					{
						text = UI.UNITSUFFIXES.MASS.MICROGRAM;
						mass = Mathf.Floor(mass * 1E+09f);
					}
					else if (num < 0.005f)
					{
						mass *= 1000000f;
						text = UI.UNITSUFFIXES.MASS.MILLIGRAM;
					}
					else if (Mathf.Abs(mass) < 5f)
					{
						mass *= 1000f;
						text = UI.UNITSUFFIXES.MASS.GRAM;
					}
					else if (Mathf.Abs(mass) < 5000f)
					{
						text = UI.UNITSUFFIXES.MASS.KILOGRAM;
					}
					else
					{
						mass /= 1000f;
						text = UI.UNITSUFFIXES.MASS.TONNE;
					}
				}
				else
				{
					text = UI.UNITSUFFIXES.MASS.KILOGRAM;
				}
			}
			else if (massFormat == GameUtil.MetricMassFormat.Kilogram)
			{
				text = UI.UNITSUFFIXES.MASS.KILOGRAM;
			}
			else if (massFormat == GameUtil.MetricMassFormat.Gram)
			{
				mass *= 1000f;
				text = UI.UNITSUFFIXES.MASS.GRAM;
			}
			else if (massFormat == GameUtil.MetricMassFormat.Tonne)
			{
				mass /= 1000f;
				text = UI.UNITSUFFIXES.MASS.TONNE;
			}
		}
		else
		{
			mass /= 2.2f;
			text = UI.UNITSUFFIXES.MASS.POUND;
			if (massFormat == GameUtil.MetricMassFormat.UseThreshold)
			{
				float num2 = Mathf.Abs(mass);
				if (num2 < 5f && num2 > 0.001f)
				{
					mass *= 256f;
					text = UI.UNITSUFFIXES.MASS.DRACHMA;
				}
				else
				{
					mass *= 7000f;
					text = UI.UNITSUFFIXES.MASS.GRAIN;
				}
			}
		}
		if (!includeSuffix)
		{
			text = "";
			timeSlice = GameUtil.TimeSlice.None;
		}
		return GameUtil.AddTimeSliceText(string.Format(floatFormat, mass) + text, timeSlice);
	}

	// Token: 0x06003589 RID: 13705 RVA: 0x00127B3A File Offset: 0x00125D3A
	public static string GetFormattedTime(float seconds, string floatFormat = "F0")
	{
		return string.Format(UI.FORMATSECONDS, seconds.ToString(floatFormat));
	}

	// Token: 0x0600358A RID: 13706 RVA: 0x00127B53 File Offset: 0x00125D53
	public static string GetFormattedEngineEfficiency(float amount)
	{
		return amount.ToString() + " km /" + UI.UNITSUFFIXES.MASS.KILOGRAM;
	}

	// Token: 0x0600358B RID: 13707 RVA: 0x00127B70 File Offset: 0x00125D70
	public static string GetFormattedDistance(float meters)
	{
		if (Mathf.Abs(meters) < 1f)
		{
			string text = (meters * 100f).ToString();
			string text2 = text.Substring(0, text.LastIndexOf('.') + Mathf.Min(3, text.Length - text.LastIndexOf('.')));
			if (text2 == "-0.0")
			{
				text2 = "0";
			}
			return text2 + " cm";
		}
		if (meters < 1000f)
		{
			return meters.ToString() + " m";
		}
		return Util.FormatOneDecimalPlace(meters / 1000f) + " km";
	}

	// Token: 0x0600358C RID: 13708 RVA: 0x00127C0F File Offset: 0x00125E0F
	public static string GetFormattedCycles(float seconds, string formatString = "F1", bool forceCycles = false)
	{
		if (forceCycles || Mathf.Abs(seconds) > 100f)
		{
			return string.Format(UI.FORMATDAY, GameUtil.FloatToString(seconds / 600f, formatString));
		}
		return GameUtil.GetFormattedTime(seconds, "F0");
	}

	// Token: 0x0600358D RID: 13709 RVA: 0x00127C49 File Offset: 0x00125E49
	public static float GetDisplaySHC(float shc)
	{
		if (GameUtil.temperatureUnit == GameUtil.TemperatureUnit.Fahrenheit)
		{
			shc /= 1.8f;
		}
		return shc;
	}

	// Token: 0x0600358E RID: 13710 RVA: 0x00127C5D File Offset: 0x00125E5D
	public static string GetSHCSuffix()
	{
		return string.Format("(DTU/g)/{0}", GameUtil.GetTemperatureUnitSuffix());
	}

	// Token: 0x0600358F RID: 13711 RVA: 0x00127C6E File Offset: 0x00125E6E
	public static string GetFormattedSHC(float shc)
	{
		shc = GameUtil.GetDisplaySHC(shc);
		return string.Format("{0} (DTU/g)/{1}", shc.ToString("0.000"), GameUtil.GetTemperatureUnitSuffix());
	}

	// Token: 0x06003590 RID: 13712 RVA: 0x00127C93 File Offset: 0x00125E93
	public static float GetDisplayThermalConductivity(float tc)
	{
		if (GameUtil.temperatureUnit == GameUtil.TemperatureUnit.Fahrenheit)
		{
			tc /= 1.8f;
		}
		return tc;
	}

	// Token: 0x06003591 RID: 13713 RVA: 0x00127CA7 File Offset: 0x00125EA7
	public static string GetThermalConductivitySuffix()
	{
		return string.Format("(DTU/(m*s))/{0}", GameUtil.GetTemperatureUnitSuffix());
	}

	// Token: 0x06003592 RID: 13714 RVA: 0x00127CB8 File Offset: 0x00125EB8
	public static string GetFormattedThermalConductivity(float tc)
	{
		tc = GameUtil.GetDisplayThermalConductivity(tc);
		return string.Format("{0} (DTU/(m*s))/{1}", tc.ToString("0.000"), GameUtil.GetTemperatureUnitSuffix());
	}

	// Token: 0x06003593 RID: 13715 RVA: 0x00127CDD File Offset: 0x00125EDD
	public static string GetElementNameByElementHash(SimHashes elementHash)
	{
		return ElementLoader.FindElementByHash(elementHash).tag.ProperName();
	}

	// Token: 0x06003594 RID: 13716 RVA: 0x00127CF0 File Offset: 0x00125EF0
	public static bool HasTrait(GameObject go, string traitName)
	{
		Traits component = go.GetComponent<Traits>();
		return !(component == null) && component.HasTrait(traitName);
	}

	// Token: 0x06003595 RID: 13717 RVA: 0x00127D18 File Offset: 0x00125F18
	public static HashSet<int> GetFloodFillCavity(int startCell, bool allowLiquid)
	{
		HashSet<int> hashSet = new HashSet<int>();
		if (allowLiquid)
		{
			hashSet = GameUtil.FloodCollectCells(startCell, (int cell) => !Grid.Solid[cell], 300, null, true);
		}
		else
		{
			hashSet = GameUtil.FloodCollectCells(startCell, (int cell) => Grid.Element[cell].IsVacuum || Grid.Element[cell].IsGas, 300, null, true);
		}
		return hashSet;
	}

	// Token: 0x06003596 RID: 13718 RVA: 0x00127D8C File Offset: 0x00125F8C
	public static float GetRadiationAbsorptionPercentage(int cell)
	{
		if (Grid.IsValidCell(cell))
		{
			return GameUtil.GetRadiationAbsorptionPercentage(Grid.Element[cell], Grid.Mass[cell], Grid.IsSolidCell(cell) && (Grid.Properties[cell] & 128) == 128);
		}
		return 0f;
	}

	// Token: 0x06003597 RID: 13719 RVA: 0x00127DE4 File Offset: 0x00125FE4
	public static float GetRadiationAbsorptionPercentage(Element elem, float mass, bool isConstructed)
	{
		float num = 2000f;
		float num2 = 0.3f;
		float num3 = 0.7f;
		float num4 = 0.8f;
		float num5;
		if (isConstructed)
		{
			num5 = elem.radiationAbsorptionFactor * num4;
		}
		else
		{
			num5 = elem.radiationAbsorptionFactor * num2 + mass / num * elem.radiationAbsorptionFactor * num3;
		}
		return Mathf.Clamp(num5, 0f, 1f);
	}

	// Token: 0x06003598 RID: 13720 RVA: 0x00127E48 File Offset: 0x00126048
	public static HashSet<int> CollectCellsBreadthFirst(int start_cell, Func<int, bool> test_func, int max_depth = 10)
	{
		HashSet<int> hashSet = new HashSet<int>();
		HashSet<int> hashSet2 = new HashSet<int>();
		HashSet<int> hashSet3 = new HashSet<int>();
		hashSet3.Add(start_cell);
		Vector2Int[] array = new Vector2Int[]
		{
			new Vector2Int(1, 0),
			new Vector2Int(-1, 0),
			new Vector2Int(0, 1),
			new Vector2Int(0, -1)
		};
		for (int i = 0; i < max_depth; i++)
		{
			List<int> list = new List<int>();
			foreach (int num in hashSet3)
			{
				foreach (Vector2Int vector2Int in array)
				{
					int num2 = Grid.OffsetCell(num, vector2Int.x, vector2Int.y);
					if (!hashSet2.Contains(num2) && !hashSet.Contains(num2))
					{
						if (Grid.IsValidCell(num2) && test_func(num2))
						{
							hashSet.Add(num2);
							list.Add(num2);
						}
						else
						{
							hashSet2.Add(num2);
						}
					}
				}
			}
			hashSet3.Clear();
			foreach (int num3 in list)
			{
				hashSet3.Add(num3);
			}
			list.Clear();
			if (hashSet3.Count == 0)
			{
				break;
			}
		}
		return hashSet;
	}

	// Token: 0x06003599 RID: 13721 RVA: 0x00127FE4 File Offset: 0x001261E4
	public static HashSet<int> FloodCollectCells(int start_cell, Func<int, bool> is_valid, int maxSize = 300, HashSet<int> AddInvalidCellsToSet = null, bool clearOversizedResults = true)
	{
		HashSet<int> hashSet = new HashSet<int>();
		HashSet<int> hashSet2 = new HashSet<int>();
		GameUtil.probeFromCell(start_cell, is_valid, hashSet, hashSet2, maxSize);
		if (AddInvalidCellsToSet != null)
		{
			AddInvalidCellsToSet.UnionWith(hashSet2);
			if (hashSet.Count > maxSize)
			{
				AddInvalidCellsToSet.UnionWith(hashSet);
			}
		}
		if (hashSet.Count > maxSize && clearOversizedResults)
		{
			hashSet.Clear();
		}
		return hashSet;
	}

	// Token: 0x0600359A RID: 13722 RVA: 0x00128038 File Offset: 0x00126238
	public static HashSet<int> FloodCollectCells(HashSet<int> results, int start_cell, Func<int, bool> is_valid, int maxSize = 300, HashSet<int> AddInvalidCellsToSet = null, bool clearOversizedResults = true)
	{
		HashSet<int> hashSet = new HashSet<int>();
		GameUtil.probeFromCell(start_cell, is_valid, results, hashSet, maxSize);
		if (AddInvalidCellsToSet != null)
		{
			AddInvalidCellsToSet.UnionWith(hashSet);
			if (results.Count > maxSize)
			{
				AddInvalidCellsToSet.UnionWith(results);
			}
		}
		if (results.Count > maxSize && clearOversizedResults)
		{
			results.Clear();
		}
		return results;
	}

	// Token: 0x0600359B RID: 13723 RVA: 0x00128088 File Offset: 0x00126288
	private static void probeFromCell(int start_cell, Func<int, bool> is_valid, HashSet<int> cells, HashSet<int> invalidCells, int maxSize = 300)
	{
		if (cells.Count > maxSize || !Grid.IsValidCell(start_cell) || invalidCells.Contains(start_cell) || cells.Contains(start_cell) || !is_valid(start_cell))
		{
			invalidCells.Add(start_cell);
			return;
		}
		cells.Add(start_cell);
		GameUtil.probeFromCell(Grid.CellLeft(start_cell), is_valid, cells, invalidCells, maxSize);
		GameUtil.probeFromCell(Grid.CellRight(start_cell), is_valid, cells, invalidCells, maxSize);
		GameUtil.probeFromCell(Grid.CellAbove(start_cell), is_valid, cells, invalidCells, maxSize);
		GameUtil.probeFromCell(Grid.CellBelow(start_cell), is_valid, cells, invalidCells, maxSize);
	}

	// Token: 0x0600359C RID: 13724 RVA: 0x00128113 File Offset: 0x00126313
	public static bool FloodFillCheck<ArgType>(Func<int, ArgType, bool> fn, ArgType arg, int start_cell, int max_depth, bool stop_at_solid, bool stop_at_liquid)
	{
		return GameUtil.FloodFillFind<ArgType>(fn, arg, start_cell, max_depth, stop_at_solid, stop_at_liquid) != -1;
	}

	// Token: 0x0600359D RID: 13725 RVA: 0x00128128 File Offset: 0x00126328
	public static int FloodFillFind<ArgType>(Func<int, ArgType, bool> fn, ArgType arg, int start_cell, int max_depth, bool stop_at_solid, bool stop_at_liquid)
	{
		GameUtil.FloodFillNext.Enqueue(new GameUtil.FloodFillInfo
		{
			cell = start_cell,
			depth = 0
		});
		int num = -1;
		while (GameUtil.FloodFillNext.Count > 0)
		{
			GameUtil.FloodFillInfo floodFillInfo = GameUtil.FloodFillNext.Dequeue();
			if (floodFillInfo.depth < max_depth && Grid.IsValidCell(floodFillInfo.cell))
			{
				Element element = Grid.Element[floodFillInfo.cell];
				if ((!stop_at_solid || !element.IsSolid) && (!stop_at_liquid || !element.IsLiquid) && !GameUtil.FloodFillVisited.Contains(floodFillInfo.cell))
				{
					GameUtil.FloodFillVisited.Add(floodFillInfo.cell);
					if (fn(floodFillInfo.cell, arg))
					{
						num = floodFillInfo.cell;
						break;
					}
					GameUtil.FloodFillNext.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellLeft(floodFillInfo.cell),
						depth = floodFillInfo.depth + 1
					});
					GameUtil.FloodFillNext.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellRight(floodFillInfo.cell),
						depth = floodFillInfo.depth + 1
					});
					GameUtil.FloodFillNext.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellAbove(floodFillInfo.cell),
						depth = floodFillInfo.depth + 1
					});
					GameUtil.FloodFillNext.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellBelow(floodFillInfo.cell),
						depth = floodFillInfo.depth + 1
					});
				}
			}
		}
		GameUtil.FloodFillVisited.Clear();
		GameUtil.FloodFillNext.Clear();
		return num;
	}

	// Token: 0x0600359E RID: 13726 RVA: 0x001282E8 File Offset: 0x001264E8
	public static void FloodFillConditional(int start_cell, Func<int, bool> condition, ICollection<int> visited_cells, ICollection<int> valid_cells = null)
	{
		GameUtil.FloodFillNext.Enqueue(new GameUtil.FloodFillInfo
		{
			cell = start_cell,
			depth = 0
		});
		GameUtil.FloodFillConditional(GameUtil.FloodFillNext, condition, visited_cells, valid_cells, 10000);
	}

	// Token: 0x0600359F RID: 13727 RVA: 0x0012832C File Offset: 0x0012652C
	public static void FloodFillConditional(Queue<GameUtil.FloodFillInfo> queue, Func<int, bool> condition, ICollection<int> visited_cells, ICollection<int> valid_cells = null, int max_depth = 10000)
	{
		while (queue.Count > 0)
		{
			GameUtil.FloodFillInfo floodFillInfo = queue.Dequeue();
			if (floodFillInfo.depth < max_depth && Grid.IsValidCell(floodFillInfo.cell) && !visited_cells.Contains(floodFillInfo.cell))
			{
				visited_cells.Add(floodFillInfo.cell);
				if (condition(floodFillInfo.cell))
				{
					if (valid_cells != null)
					{
						valid_cells.Add(floodFillInfo.cell);
					}
					int num = floodFillInfo.depth + 1;
					queue.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellLeft(floodFillInfo.cell),
						depth = num
					});
					queue.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellRight(floodFillInfo.cell),
						depth = num
					});
					queue.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellAbove(floodFillInfo.cell),
						depth = num
					});
					queue.Enqueue(new GameUtil.FloodFillInfo
					{
						cell = Grid.CellBelow(floodFillInfo.cell),
						depth = num
					});
				}
			}
		}
		queue.Clear();
	}

	// Token: 0x060035A0 RID: 13728 RVA: 0x00128460 File Offset: 0x00126660
	public static string GetHardnessString(Element element, bool addColor = true)
	{
		if (!element.IsSolid)
		{
			return ELEMENTS.HARDNESS.NA;
		}
		Color color = GameUtil.Hardness.firmColor;
		string text;
		if (element.hardness >= 255)
		{
			color = GameUtil.Hardness.ImpenetrableColor;
			text = string.Format(ELEMENTS.HARDNESS.IMPENETRABLE, element.hardness);
		}
		else if (element.hardness >= 150)
		{
			color = GameUtil.Hardness.nearlyImpenetrableColor;
			text = string.Format(ELEMENTS.HARDNESS.NEARLYIMPENETRABLE, element.hardness);
		}
		else if (element.hardness >= 50)
		{
			color = GameUtil.Hardness.veryFirmColor;
			text = string.Format(ELEMENTS.HARDNESS.VERYFIRM, element.hardness);
		}
		else if (element.hardness >= 25)
		{
			color = GameUtil.Hardness.firmColor;
			text = string.Format(ELEMENTS.HARDNESS.FIRM, element.hardness);
		}
		else if (element.hardness >= 10)
		{
			color = GameUtil.Hardness.softColor;
			text = string.Format(ELEMENTS.HARDNESS.SOFT, element.hardness);
		}
		else
		{
			color = GameUtil.Hardness.verySoftColor;
			text = string.Format(ELEMENTS.HARDNESS.VERYSOFT, element.hardness);
		}
		if (addColor)
		{
			text = string.Format("<color=#{0}>{1}</color>", color.ToHexString(), text);
		}
		return text;
	}

	// Token: 0x060035A1 RID: 13729 RVA: 0x001285B0 File Offset: 0x001267B0
	public static string GetGermResistanceModifierString(float modifier, bool addColor = true)
	{
		Color color = Color.black;
		string text = "";
		if (modifier > 0f)
		{
			if (modifier >= 5f)
			{
				color = GameUtil.GermResistanceValues.PositiveLargeColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.POSITIVE_LARGE, modifier);
			}
			else if (modifier >= 2f)
			{
				color = GameUtil.GermResistanceValues.PositiveMediumColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.POSITIVE_MEDIUM, modifier);
			}
			else if (modifier > 0f)
			{
				color = GameUtil.GermResistanceValues.PositiveSmallColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.POSITIVE_SMALL, modifier);
			}
		}
		else if (modifier < 0f)
		{
			if (modifier <= -5f)
			{
				color = GameUtil.GermResistanceValues.NegativeLargeColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.NEGATIVE_LARGE, modifier);
			}
			else if (modifier <= -2f)
			{
				color = GameUtil.GermResistanceValues.NegativeMediumColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.NEGATIVE_MEDIUM, modifier);
			}
			else if (modifier < 0f)
			{
				color = GameUtil.GermResistanceValues.NegativeSmallColor;
				text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.NEGATIVE_SMALL, modifier);
			}
		}
		else
		{
			addColor = false;
			text = string.Format(DUPLICANTS.ATTRIBUTES.GERMRESISTANCE.MODIFIER_DESCRIPTORS.NONE, modifier);
		}
		if (addColor)
		{
			text = string.Format("<color=#{0}>{1}</color>", color.ToHexString(), text);
		}
		return text;
	}

	// Token: 0x060035A2 RID: 13730 RVA: 0x001286F8 File Offset: 0x001268F8
	public static string GetThermalConductivityString(Element element, bool addColor = true, bool addValue = true)
	{
		Color color = GameUtil.ThermalConductivityValues.mediumConductivityColor;
		string text;
		if (element.thermalConductivity >= 50f)
		{
			color = GameUtil.ThermalConductivityValues.veryHighConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.VERY_HIGH_CONDUCTIVITY;
		}
		else if (element.thermalConductivity >= 10f)
		{
			color = GameUtil.ThermalConductivityValues.highConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.HIGH_CONDUCTIVITY;
		}
		else if (element.thermalConductivity >= 2f)
		{
			color = GameUtil.ThermalConductivityValues.mediumConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.MEDIUM_CONDUCTIVITY;
		}
		else if (element.thermalConductivity >= 1f)
		{
			color = GameUtil.ThermalConductivityValues.lowConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.LOW_CONDUCTIVITY;
		}
		else
		{
			color = GameUtil.ThermalConductivityValues.veryLowConductivityColor;
			text = UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.VERY_LOW_CONDUCTIVITY;
		}
		if (addColor)
		{
			text = string.Format("<color=#{0}>{1}</color>", color.ToHexString(), text);
		}
		if (addValue)
		{
			text = string.Format(UI.ELEMENTAL.THERMALCONDUCTIVITY.ADJECTIVES.VALUE_WITH_ADJECTIVE, element.thermalConductivity.ToString(), text);
		}
		return text;
	}

	// Token: 0x060035A3 RID: 13731 RVA: 0x001287D8 File Offset: 0x001269D8
	public static string GetBreathableString(Element element, float Mass)
	{
		if (!element.IsGas && !element.IsVacuum)
		{
			return "";
		}
		Color color = GameUtil.BreathableValues.positiveColor;
		SimHashes id = element.id;
		LocString locString;
		if (id != SimHashes.Oxygen)
		{
			if (id != SimHashes.ContaminatedOxygen)
			{
				color = GameUtil.BreathableValues.negativeColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND4;
			}
			else if (Mass >= SimDebugView.optimallyBreathable)
			{
				color = GameUtil.BreathableValues.positiveColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND1;
			}
			else if (Mass >= SimDebugView.minimumBreathable + (SimDebugView.optimallyBreathable - SimDebugView.minimumBreathable) / 2f)
			{
				color = GameUtil.BreathableValues.positiveColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND2;
			}
			else if (Mass >= SimDebugView.minimumBreathable)
			{
				color = GameUtil.BreathableValues.warningColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND3;
			}
			else
			{
				color = GameUtil.BreathableValues.negativeColor;
				locString = UI.OVERLAYS.OXYGEN.LEGEND4;
			}
		}
		else if (Mass >= SimDebugView.optimallyBreathable)
		{
			color = GameUtil.BreathableValues.positiveColor;
			locString = UI.OVERLAYS.OXYGEN.LEGEND1;
		}
		else if (Mass >= SimDebugView.minimumBreathable + (SimDebugView.optimallyBreathable - SimDebugView.minimumBreathable) / 2f)
		{
			color = GameUtil.BreathableValues.positiveColor;
			locString = UI.OVERLAYS.OXYGEN.LEGEND2;
		}
		else if (Mass >= SimDebugView.minimumBreathable)
		{
			color = GameUtil.BreathableValues.warningColor;
			locString = UI.OVERLAYS.OXYGEN.LEGEND3;
		}
		else
		{
			color = GameUtil.BreathableValues.negativeColor;
			locString = UI.OVERLAYS.OXYGEN.LEGEND4;
		}
		return string.Format(ELEMENTS.BREATHABLEDESC, color.ToHexString(), locString);
	}

	// Token: 0x060035A4 RID: 13732 RVA: 0x0012890C File Offset: 0x00126B0C
	public static string GetWireLoadColor(float load, float maxLoad, float potentialLoad)
	{
		Color color;
		if (load > maxLoad + POWER.FLOAT_FUDGE_FACTOR)
		{
			color = GameUtil.WireLoadValues.negativeColor;
		}
		else if (potentialLoad > maxLoad && load / maxLoad >= 0.75f)
		{
			color = GameUtil.WireLoadValues.warningColor;
		}
		else
		{
			color = Color.white;
		}
		return color.ToHexString();
	}

	// Token: 0x060035A5 RID: 13733 RVA: 0x0012894D File Offset: 0x00126B4D
	public static string GetHotkeyString(global::Action action)
	{
		if (KInputManager.currentControllerIsGamepad)
		{
			return UI.FormatAsHotkey(GameUtil.GetActionString(action));
		}
		return UI.FormatAsHotkey("[" + GameUtil.GetActionString(action) + "]");
	}

	// Token: 0x060035A6 RID: 13734 RVA: 0x0012897C File Offset: 0x00126B7C
	public static string ReplaceHotkeyString(string template, global::Action action)
	{
		return template.Replace("{Hotkey}", GameUtil.GetHotkeyString(action));
	}

	// Token: 0x060035A7 RID: 13735 RVA: 0x0012898F File Offset: 0x00126B8F
	public static string ReplaceHotkeyString(string template, global::Action action1, global::Action action2)
	{
		return template.Replace("{Hotkey}", GameUtil.GetHotkeyString(action1) + GameUtil.GetHotkeyString(action2));
	}

	// Token: 0x060035A8 RID: 13736 RVA: 0x001289B0 File Offset: 0x00126BB0
	public static string GetKeycodeLocalized(KKeyCode key_code)
	{
		string text = key_code.ToString();
		if (key_code <= KKeyCode.Slash)
		{
			if (key_code <= KKeyCode.Tab)
			{
				if (key_code == KKeyCode.None)
				{
					return text;
				}
				if (key_code == KKeyCode.Backspace)
				{
					return INPUT.BACKSPACE;
				}
				if (key_code == KKeyCode.Tab)
				{
					return INPUT.TAB;
				}
			}
			else if (key_code <= KKeyCode.Escape)
			{
				if (key_code == KKeyCode.Return)
				{
					return INPUT.ENTER;
				}
				if (key_code == KKeyCode.Escape)
				{
					return INPUT.ESCAPE;
				}
			}
			else
			{
				if (key_code == KKeyCode.Space)
				{
					return INPUT.SPACE;
				}
				switch (key_code)
				{
				case KKeyCode.Plus:
					return "+";
				case KKeyCode.Comma:
					return ",";
				case KKeyCode.Minus:
					return "-";
				case KKeyCode.Period:
					return INPUT.PERIOD;
				case KKeyCode.Slash:
					return "/";
				}
			}
		}
		else if (key_code <= KKeyCode.Insert)
		{
			switch (key_code)
			{
			case KKeyCode.Colon:
				return ":";
			case KKeyCode.Semicolon:
				return ";";
			case KKeyCode.Less:
				break;
			case KKeyCode.Equals:
				return "=";
			default:
				switch (key_code)
				{
				case KKeyCode.LeftBracket:
					return "[";
				case KKeyCode.Backslash:
					return "\\";
				case KKeyCode.RightBracket:
					return "]";
				case KKeyCode.Caret:
				case KKeyCode.Underscore:
					break;
				case KKeyCode.BackQuote:
					return INPUT.BACKQUOTE;
				default:
					switch (key_code)
					{
					case KKeyCode.Keypad0:
						return INPUT.NUM + " 0";
					case KKeyCode.Keypad1:
						return INPUT.NUM + " 1";
					case KKeyCode.Keypad2:
						return INPUT.NUM + " 2";
					case KKeyCode.Keypad3:
						return INPUT.NUM + " 3";
					case KKeyCode.Keypad4:
						return INPUT.NUM + " 4";
					case KKeyCode.Keypad5:
						return INPUT.NUM + " 5";
					case KKeyCode.Keypad6:
						return INPUT.NUM + " 6";
					case KKeyCode.Keypad7:
						return INPUT.NUM + " 7";
					case KKeyCode.Keypad8:
						return INPUT.NUM + " 8";
					case KKeyCode.Keypad9:
						return INPUT.NUM + " 9";
					case KKeyCode.KeypadPeriod:
						return INPUT.NUM + " " + INPUT.PERIOD;
					case KKeyCode.KeypadDivide:
						return INPUT.NUM + " /";
					case KKeyCode.KeypadMultiply:
						return INPUT.NUM + " *";
					case KKeyCode.KeypadMinus:
						return INPUT.NUM + " -";
					case KKeyCode.KeypadPlus:
						return INPUT.NUM + " +";
					case KKeyCode.KeypadEnter:
						return INPUT.NUM + " " + INPUT.ENTER;
					case KKeyCode.Insert:
						return INPUT.INSERT;
					}
					break;
				}
				break;
			}
		}
		else if (key_code <= KKeyCode.Mouse6)
		{
			switch (key_code)
			{
			case KKeyCode.RightShift:
				return INPUT.RIGHT_SHIFT;
			case KKeyCode.LeftShift:
				return INPUT.LEFT_SHIFT;
			case KKeyCode.RightControl:
				return INPUT.RIGHT_CTRL;
			case KKeyCode.LeftControl:
				return INPUT.LEFT_CTRL;
			case KKeyCode.RightAlt:
				return INPUT.RIGHT_ALT;
			case KKeyCode.LeftAlt:
				return INPUT.LEFT_ALT;
			default:
				switch (key_code)
				{
				case KKeyCode.Mouse0:
					return INPUT.MOUSE + " 0";
				case KKeyCode.Mouse1:
					return INPUT.MOUSE + " 1";
				case KKeyCode.Mouse2:
					return INPUT.MOUSE + " 2";
				case KKeyCode.Mouse3:
					return INPUT.MOUSE + " 3";
				case KKeyCode.Mouse4:
					return INPUT.MOUSE + " 4";
				case KKeyCode.Mouse5:
					return INPUT.MOUSE + " 5";
				case KKeyCode.Mouse6:
					return INPUT.MOUSE + " 6";
				}
				break;
			}
		}
		else
		{
			if (key_code == KKeyCode.MouseScrollDown)
			{
				return INPUT.MOUSE_SCROLL_DOWN;
			}
			if (key_code == KKeyCode.MouseScrollUp)
			{
				return INPUT.MOUSE_SCROLL_UP;
			}
		}
		if (KKeyCode.A <= key_code && key_code <= KKeyCode.Z)
		{
			text = ((char)(65 + (key_code - KKeyCode.A))).ToString();
		}
		else if (KKeyCode.Alpha0 <= key_code && key_code <= KKeyCode.Alpha9)
		{
			text = ((char)(48 + (key_code - KKeyCode.Alpha0))).ToString();
		}
		else if (KKeyCode.F1 <= key_code && key_code <= KKeyCode.F12)
		{
			text = "F" + (key_code - KKeyCode.F1 + 1).ToString();
		}
		else
		{
			global::Debug.LogWarning("Unable to find proper string for KKeyCode: " + key_code.ToString() + " using key_code.ToString()");
		}
		return text;
	}

	// Token: 0x060035A9 RID: 13737 RVA: 0x00128FB8 File Offset: 0x001271B8
	public static string GetActionString(global::Action action)
	{
		string text = "";
		if (action == global::Action.NumActions)
		{
			return text;
		}
		BindingEntry bindingEntry = GameUtil.ActionToBinding(action);
		KKeyCode mKeyCode = bindingEntry.mKeyCode;
		if (KInputManager.currentControllerIsGamepad)
		{
			return KInputManager.steamInputInterpreter.GetActionGlyph(action);
		}
		if (bindingEntry.mModifier == global::Modifier.None)
		{
			return GameUtil.GetKeycodeLocalized(mKeyCode).ToUpper();
		}
		string text2 = "";
		global::Modifier mModifier = bindingEntry.mModifier;
		switch (mModifier)
		{
		case global::Modifier.Alt:
			text2 = GameUtil.GetKeycodeLocalized(KKeyCode.LeftAlt).ToUpper();
			break;
		case global::Modifier.Ctrl:
			text2 = GameUtil.GetKeycodeLocalized(KKeyCode.LeftControl).ToUpper();
			break;
		case (global::Modifier)3:
			break;
		case global::Modifier.Shift:
			text2 = GameUtil.GetKeycodeLocalized(KKeyCode.LeftShift).ToUpper();
			break;
		default:
			if (mModifier != global::Modifier.CapsLock)
			{
				if (mModifier == global::Modifier.Backtick)
				{
					text2 = GameUtil.GetKeycodeLocalized(KKeyCode.BackQuote).ToUpper();
				}
			}
			else
			{
				text2 = GameUtil.GetKeycodeLocalized(KKeyCode.CapsLock).ToUpper();
			}
			break;
		}
		return text2 + " + " + GameUtil.GetKeycodeLocalized(mKeyCode).ToUpper();
	}

	// Token: 0x060035AA RID: 13738 RVA: 0x001290AC File Offset: 0x001272AC
	public static void CreateExplosion(Vector3 explosion_pos)
	{
		Vector2 vector = new Vector2(explosion_pos.x, explosion_pos.y);
		float num = 5f;
		float num2 = num * num;
		foreach (Health health in Components.Health.Items)
		{
			Vector3 position = health.transform.GetPosition();
			float sqrMagnitude = (new Vector2(position.x, position.y) - vector).sqrMagnitude;
			if (num2 >= sqrMagnitude && health != null)
			{
				health.Damage(health.maxHitPoints);
			}
		}
	}

	// Token: 0x060035AB RID: 13739 RVA: 0x00129164 File Offset: 0x00127364
	private static void GetNonSolidCells(int x, int y, List<int> cells, int min_x, int min_y, int max_x, int max_y)
	{
		int num = Grid.XYToCell(x, y);
		if (Grid.IsValidCell(num) && !Grid.Solid[num] && !Grid.DupePassable[num] && x >= min_x && x <= max_x && y >= min_y && y <= max_y && !cells.Contains(num))
		{
			cells.Add(num);
			GameUtil.GetNonSolidCells(x + 1, y, cells, min_x, min_y, max_x, max_y);
			GameUtil.GetNonSolidCells(x - 1, y, cells, min_x, min_y, max_x, max_y);
			GameUtil.GetNonSolidCells(x, y + 1, cells, min_x, min_y, max_x, max_y);
			GameUtil.GetNonSolidCells(x, y - 1, cells, min_x, min_y, max_x, max_y);
		}
	}

	// Token: 0x060035AC RID: 13740 RVA: 0x00129208 File Offset: 0x00127408
	public static void GetNonSolidCells(int cell, int radius, List<int> cells)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		GameUtil.GetNonSolidCells(num, num2, cells, num - radius, num2 - radius, num + radius, num2 + radius);
	}

	// Token: 0x060035AD RID: 13741 RVA: 0x00129238 File Offset: 0x00127438
	public static float GetMaxStressInActiveWorld()
	{
		if (Components.LiveMinionIdentities.Count <= 0)
		{
			return 0f;
		}
		float num = 0f;
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			if (!minionIdentity.IsNullOrDestroyed() && minionIdentity.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
			{
				AmountInstance amountInstance = Db.Get().Amounts.Stress.Lookup(minionIdentity);
				if (amountInstance != null)
				{
					num = Mathf.Max(num, amountInstance.value);
				}
			}
		}
		return num;
	}

	// Token: 0x060035AE RID: 13742 RVA: 0x001292E4 File Offset: 0x001274E4
	public static float GetAverageStressInActiveWorld()
	{
		if (Components.LiveMinionIdentities.Count <= 0)
		{
			return 0f;
		}
		float num = 0f;
		int num2 = 0;
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			if (!minionIdentity.IsNullOrDestroyed() && minionIdentity.GetMyWorldId() == ClusterManager.Instance.activeWorldId)
			{
				num += Db.Get().Amounts.Stress.Lookup(minionIdentity).value;
				num2++;
			}
		}
		return num / (float)num2;
	}

	// Token: 0x060035AF RID: 13743 RVA: 0x00129390 File Offset: 0x00127590
	public static string MigrateFMOD(FMODAsset asset)
	{
		if (asset == null)
		{
			return null;
		}
		if (asset.path == null)
		{
			return asset.name;
		}
		return asset.path;
	}

	// Token: 0x060035B0 RID: 13744 RVA: 0x001293B2 File Offset: 0x001275B2
	private static void SortGameObjectDescriptors(List<IGameObjectEffectDescriptor> descriptorList)
	{
		descriptorList.Sort(delegate(IGameObjectEffectDescriptor e1, IGameObjectEffectDescriptor e2)
		{
			int num = TUNING.BUILDINGS.COMPONENT_DESCRIPTION_ORDER.IndexOf(e1.GetType());
			int num2 = TUNING.BUILDINGS.COMPONENT_DESCRIPTION_ORDER.IndexOf(e2.GetType());
			return num.CompareTo(num2);
		});
	}

	// Token: 0x060035B1 RID: 13745 RVA: 0x001293DC File Offset: 0x001275DC
	public static void IndentListOfDescriptors(List<Descriptor> list, int indentCount = 1)
	{
		for (int i = 0; i < list.Count; i++)
		{
			Descriptor descriptor = list[i];
			for (int j = 0; j < indentCount; j++)
			{
				descriptor.IncreaseIndent();
			}
			list[i] = descriptor;
		}
	}

	// Token: 0x060035B2 RID: 13746 RVA: 0x00129420 File Offset: 0x00127620
	public static List<Descriptor> GetAllDescriptors(GameObject go, bool simpleInfoScreen = false)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<IGameObjectEffectDescriptor> list2 = new List<IGameObjectEffectDescriptor>(go.GetComponents<IGameObjectEffectDescriptor>());
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			list2.AddRange(component.GetDescriptors());
		}
		GameUtil.SortGameObjectDescriptors(list2);
		foreach (IGameObjectEffectDescriptor gameObjectEffectDescriptor in list2)
		{
			List<Descriptor> descriptors = gameObjectEffectDescriptor.GetDescriptors(go);
			if (descriptors != null)
			{
				foreach (Descriptor descriptor in descriptors)
				{
					if (!descriptor.onlyForSimpleInfoScreen || simpleInfoScreen)
					{
						list.Add(descriptor);
					}
				}
			}
		}
		KPrefabID component2 = go.GetComponent<KPrefabID>();
		if (component2 != null && component2.AdditionalRequirements != null)
		{
			foreach (Descriptor descriptor2 in component2.AdditionalRequirements)
			{
				if (!descriptor2.onlyForSimpleInfoScreen || simpleInfoScreen)
				{
					list.Add(descriptor2);
				}
			}
		}
		if (component2 != null && component2.AdditionalEffects != null)
		{
			foreach (Descriptor descriptor3 in component2.AdditionalEffects)
			{
				if (!descriptor3.onlyForSimpleInfoScreen || simpleInfoScreen)
				{
					list.Add(descriptor3);
				}
			}
		}
		return list;
	}

	// Token: 0x060035B3 RID: 13747 RVA: 0x001295C0 File Offset: 0x001277C0
	public static List<Descriptor> GetDetailDescriptors(List<Descriptor> descriptors)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Detail)
			{
				list.Add(descriptor);
			}
		}
		GameUtil.IndentListOfDescriptors(list, 1);
		return list;
	}

	// Token: 0x060035B4 RID: 13748 RVA: 0x00129628 File Offset: 0x00127828
	public static List<Descriptor> GetRequirementDescriptors(List<Descriptor> descriptors)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Requirement)
			{
				list.Add(descriptor);
			}
		}
		GameUtil.IndentListOfDescriptors(list, 1);
		return list;
	}

	// Token: 0x060035B5 RID: 13749 RVA: 0x0012968C File Offset: 0x0012788C
	public static List<Descriptor> GetEffectDescriptors(List<Descriptor> descriptors)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Effect || descriptor.type == Descriptor.DescriptorType.DiseaseSource)
			{
				list.Add(descriptor);
			}
		}
		GameUtil.IndentListOfDescriptors(list, 1);
		return list;
	}

	// Token: 0x060035B6 RID: 13750 RVA: 0x001296FC File Offset: 0x001278FC
	public static List<Descriptor> GetInformationDescriptors(List<Descriptor> descriptors)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Lifecycle)
			{
				list.Add(descriptor);
			}
		}
		GameUtil.IndentListOfDescriptors(list, 1);
		return list;
	}

	// Token: 0x060035B7 RID: 13751 RVA: 0x00129764 File Offset: 0x00127964
	public static List<Descriptor> GetCropOptimumConditionDescriptors(List<Descriptor> descriptors)
	{
		List<Descriptor> list = new List<Descriptor>();
		foreach (Descriptor descriptor in descriptors)
		{
			if (descriptor.type == Descriptor.DescriptorType.Lifecycle)
			{
				Descriptor descriptor2 = descriptor;
				descriptor2.text = "• " + descriptor2.text;
				list.Add(descriptor2);
			}
		}
		GameUtil.IndentListOfDescriptors(list, 1);
		return list;
	}

	// Token: 0x060035B8 RID: 13752 RVA: 0x001297E4 File Offset: 0x001279E4
	public static List<Descriptor> GetGameObjectRequirements(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<IGameObjectEffectDescriptor> list2 = new List<IGameObjectEffectDescriptor>(go.GetComponents<IGameObjectEffectDescriptor>());
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			list2.AddRange(component.GetDescriptors());
		}
		GameUtil.SortGameObjectDescriptors(list2);
		foreach (IGameObjectEffectDescriptor gameObjectEffectDescriptor in list2)
		{
			List<Descriptor> descriptors = gameObjectEffectDescriptor.GetDescriptors(go);
			if (descriptors != null)
			{
				foreach (Descriptor descriptor in descriptors)
				{
					if (descriptor.type == Descriptor.DescriptorType.Requirement)
					{
						list.Add(descriptor);
					}
				}
			}
		}
		KPrefabID component2 = go.GetComponent<KPrefabID>();
		if (component2.AdditionalRequirements != null)
		{
			list.AddRange(component2.AdditionalRequirements);
		}
		return list;
	}

	// Token: 0x060035B9 RID: 13753 RVA: 0x001298D4 File Offset: 0x00127AD4
	public static List<Descriptor> GetGameObjectEffects(GameObject go, bool simpleInfoScreen = false)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<IGameObjectEffectDescriptor> list2 = new List<IGameObjectEffectDescriptor>(go.GetComponents<IGameObjectEffectDescriptor>());
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			list2.AddRange(component.GetDescriptors());
		}
		GameUtil.SortGameObjectDescriptors(list2);
		foreach (IGameObjectEffectDescriptor gameObjectEffectDescriptor in list2)
		{
			List<Descriptor> descriptors = gameObjectEffectDescriptor.GetDescriptors(go);
			if (descriptors != null)
			{
				foreach (Descriptor descriptor in descriptors)
				{
					if ((!descriptor.onlyForSimpleInfoScreen || simpleInfoScreen) && (descriptor.type == Descriptor.DescriptorType.Effect || descriptor.type == Descriptor.DescriptorType.DiseaseSource))
					{
						list.Add(descriptor);
					}
				}
			}
		}
		KPrefabID component2 = go.GetComponent<KPrefabID>();
		if (component2 != null && component2.AdditionalEffects != null)
		{
			foreach (Descriptor descriptor2 in component2.AdditionalEffects)
			{
				if (!descriptor2.onlyForSimpleInfoScreen || simpleInfoScreen)
				{
					list.Add(descriptor2);
				}
			}
		}
		return list;
	}

	// Token: 0x060035BA RID: 13754 RVA: 0x00129A28 File Offset: 0x00127C28
	public static List<Descriptor> GetPlantRequirementDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<Descriptor> requirementDescriptors = GameUtil.GetRequirementDescriptors(GameUtil.GetAllDescriptors(go, false));
		if (requirementDescriptors.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.PLANTREQUIREMENTS, UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.PLANTREQUIREMENTS, Descriptor.DescriptorType.Requirement);
			list.Add(descriptor);
			list.AddRange(requirementDescriptors);
		}
		return list;
	}

	// Token: 0x060035BB RID: 13755 RVA: 0x00129A84 File Offset: 0x00127C84
	public static List<Descriptor> GetPlantLifeCycleDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		List<Descriptor> informationDescriptors = GameUtil.GetInformationDescriptors(GameUtil.GetAllDescriptors(go, false));
		if (informationDescriptors.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.LIFECYCLE, UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.PLANTLIFECYCLE, Descriptor.DescriptorType.Lifecycle);
			list.Add(descriptor);
			list.AddRange(informationDescriptors);
		}
		return list;
	}

	// Token: 0x060035BC RID: 13756 RVA: 0x00129AE0 File Offset: 0x00127CE0
	public static List<Descriptor> GetPlantEffectDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (go.GetComponent<Growing>() == null)
		{
			return list;
		}
		List<Descriptor> allDescriptors = GameUtil.GetAllDescriptors(go, false);
		List<Descriptor> list2 = new List<Descriptor>();
		list2.AddRange(GameUtil.GetEffectDescriptors(allDescriptors));
		if (list2.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.UISIDESCREENS.PLANTERSIDESCREEN.PLANTEFFECTS, UI.UISIDESCREENS.PLANTERSIDESCREEN.TOOLTIPS.PLANTEFFECTS, Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
			list.AddRange(list2);
		}
		return list;
	}

	// Token: 0x060035BD RID: 13757 RVA: 0x00129B5C File Offset: 0x00127D5C
	public static string GetGameObjectEffectsTooltipString(GameObject go)
	{
		string text = "";
		List<Descriptor> gameObjectEffects = GameUtil.GetGameObjectEffects(go, false);
		if (gameObjectEffects.Count > 0)
		{
			text = text + UI.BUILDINGEFFECTS.OPERATIONEFFECTS + "\n";
		}
		foreach (Descriptor descriptor in gameObjectEffects)
		{
			text = text + descriptor.IndentedText() + "\n";
		}
		return text;
	}

	// Token: 0x060035BE RID: 13758 RVA: 0x00129BE4 File Offset: 0x00127DE4
	public static List<Descriptor> GetEquipmentEffects(EquipmentDef def)
	{
		global::Debug.Assert(def != null);
		List<Descriptor> list = new List<Descriptor>();
		List<AttributeModifier> attributeModifiers = def.AttributeModifiers;
		if (attributeModifiers != null)
		{
			foreach (AttributeModifier attributeModifier in attributeModifiers)
			{
				string name = Db.Get().Attributes.Get(attributeModifier.AttributeId).Name;
				string formattedString = attributeModifier.GetFormattedString();
				string text = ((attributeModifier.Value >= 0f) ? "produced" : "consumed");
				string text2 = UI.GAMEOBJECTEFFECTS.EQUIPMENT_MODS.text.Replace("{Attribute}", name).Replace("{Style}", text).Replace("{Value}", formattedString);
				list.Add(new Descriptor(text2, text2, Descriptor.DescriptorType.Effect, false));
			}
		}
		return list;
	}

	// Token: 0x060035BF RID: 13759 RVA: 0x00129CD4 File Offset: 0x00127ED4
	public static string GetRecipeDescription(Recipe recipe)
	{
		string text = null;
		if (recipe != null)
		{
			text = recipe.recipeDescription;
		}
		if (text == null)
		{
			text = RESEARCH.TYPES.MISSINGRECIPEDESC;
			global::Debug.LogWarning("Missing recipeDescription");
		}
		return text;
	}

	// Token: 0x060035C0 RID: 13760 RVA: 0x00129D06 File Offset: 0x00127F06
	public static int GetCurrentCycle()
	{
		return GameClock.Instance.GetCycle() + 1;
	}

	// Token: 0x060035C1 RID: 13761 RVA: 0x00129D14 File Offset: 0x00127F14
	public static float GetCurrentTimeInCycles()
	{
		return GameClock.Instance.GetTimeInCycles() + 1f;
	}

	// Token: 0x060035C2 RID: 13762 RVA: 0x00129D28 File Offset: 0x00127F28
	public static GameObject GetActiveTelepad()
	{
		GameObject gameObject = GameUtil.GetTelepad(ClusterManager.Instance.activeWorldId);
		if (gameObject == null)
		{
			gameObject = GameUtil.GetTelepad(ClusterManager.Instance.GetStartWorld().id);
		}
		return gameObject;
	}

	// Token: 0x060035C3 RID: 13763 RVA: 0x00129D64 File Offset: 0x00127F64
	public static GameObject GetTelepad(int worldId)
	{
		if (Components.Telepads.Count > 0)
		{
			for (int i = 0; i < Components.Telepads.Count; i++)
			{
				if (Components.Telepads[i].GetMyWorldId() == worldId)
				{
					return Components.Telepads[i].gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x060035C4 RID: 13764 RVA: 0x00129DB8 File Offset: 0x00127FB8
	public static GameObject KInstantiate(GameObject original, Vector3 position, Grid.SceneLayer sceneLayer, string name = null, int gameLayer = 0)
	{
		return GameUtil.KInstantiate(original, position, sceneLayer, null, name, gameLayer);
	}

	// Token: 0x060035C5 RID: 13765 RVA: 0x00129DC6 File Offset: 0x00127FC6
	public static GameObject KInstantiate(GameObject original, Vector3 position, Grid.SceneLayer sceneLayer, GameObject parent, string name = null, int gameLayer = 0)
	{
		position.z = Grid.GetLayerZ(sceneLayer);
		return Util.KInstantiate(original, position, Quaternion.identity, parent, name, true, gameLayer);
	}

	// Token: 0x060035C6 RID: 13766 RVA: 0x00129DE7 File Offset: 0x00127FE7
	public static GameObject KInstantiate(GameObject original, Grid.SceneLayer sceneLayer, string name = null, int gameLayer = 0)
	{
		return GameUtil.KInstantiate(original, Vector3.zero, sceneLayer, name, gameLayer);
	}

	// Token: 0x060035C7 RID: 13767 RVA: 0x00129DF7 File Offset: 0x00127FF7
	public static GameObject KInstantiate(Component original, Grid.SceneLayer sceneLayer, string name = null, int gameLayer = 0)
	{
		return GameUtil.KInstantiate(original.gameObject, Vector3.zero, sceneLayer, name, gameLayer);
	}

	// Token: 0x060035C8 RID: 13768 RVA: 0x00129E0C File Offset: 0x0012800C
	public unsafe static void IsEmissionBlocked(int cell, out bool all_not_gaseous, out bool all_over_pressure)
	{
		int* ptr = stackalloc int[(UIntPtr)16];
		*ptr = Grid.CellBelow(cell);
		ptr[1] = Grid.CellLeft(cell);
		ptr[2] = Grid.CellRight(cell);
		ptr[3] = Grid.CellAbove(cell);
		all_not_gaseous = true;
		all_over_pressure = true;
		for (int i = 0; i < 4; i++)
		{
			int num = ptr[i];
			if (Grid.IsValidCell(num))
			{
				Element element = Grid.Element[num];
				all_not_gaseous = all_not_gaseous && !element.IsGas && !element.IsVacuum;
				all_over_pressure = all_over_pressure && ((!element.IsGas && !element.IsVacuum) || Grid.Mass[num] >= 1.8f);
			}
		}
	}

	// Token: 0x060035C9 RID: 13769 RVA: 0x00129EC4 File Offset: 0x001280C4
	public static float GetDecorAtCell(int cell)
	{
		float num = 0f;
		if (!Grid.Solid[cell])
		{
			num = Grid.Decor[cell];
			num += (float)DecorProvider.GetLightDecorBonus(cell);
		}
		return num;
	}

	// Token: 0x060035CA RID: 13770 RVA: 0x00129EF8 File Offset: 0x001280F8
	public static string GetUnitTypeMassOrUnit(GameObject go)
	{
		string text = UI.UNITSUFFIXES.UNITS;
		KPrefabID component = go.GetComponent<KPrefabID>();
		if (component != null)
		{
			text = (component.Tags.Contains(GameTags.Seed) ? UI.UNITSUFFIXES.UNITS : UI.UNITSUFFIXES.MASS.KILOGRAM);
		}
		return text;
	}

	// Token: 0x060035CB RID: 13771 RVA: 0x00129F48 File Offset: 0x00128148
	public static string GetKeywordStyle(Tag tag)
	{
		Element element = ElementLoader.GetElement(tag);
		string text;
		if (element != null)
		{
			text = GameUtil.GetKeywordStyle(element);
		}
		else if (GameUtil.foodTags.Contains(tag))
		{
			text = "food";
		}
		else if (GameUtil.solidTags.Contains(tag))
		{
			text = "solid";
		}
		else
		{
			text = null;
		}
		return text;
	}

	// Token: 0x060035CC RID: 13772 RVA: 0x00129F98 File Offset: 0x00128198
	public static string GetKeywordStyle(SimHashes hash)
	{
		Element element = ElementLoader.FindElementByHash(hash);
		if (element != null)
		{
			return GameUtil.GetKeywordStyle(element);
		}
		return null;
	}

	// Token: 0x060035CD RID: 13773 RVA: 0x00129FB8 File Offset: 0x001281B8
	public static string GetKeywordStyle(Element element)
	{
		if (element.id == SimHashes.Oxygen)
		{
			return "oxygen";
		}
		if (element.IsSolid)
		{
			return "solid";
		}
		if (element.IsLiquid)
		{
			return "liquid";
		}
		if (element.IsGas)
		{
			return "gas";
		}
		if (element.IsVacuum)
		{
			return "vacuum";
		}
		return null;
	}

	// Token: 0x060035CE RID: 13774 RVA: 0x0012A014 File Offset: 0x00128214
	public static string GetKeywordStyle(GameObject go)
	{
		string text = "";
		UnityEngine.Object component = go.GetComponent<Edible>();
		Equippable component2 = go.GetComponent<Equippable>();
		MedicinalPill component3 = go.GetComponent<MedicinalPill>();
		ResearchPointObject component4 = go.GetComponent<ResearchPointObject>();
		if (component != null)
		{
			text = "food";
		}
		else if (component2 != null)
		{
			text = "equipment";
		}
		else if (component3 != null)
		{
			text = "medicine";
		}
		else if (component4 != null)
		{
			text = "research";
		}
		return text;
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x0012A084 File Offset: 0x00128284
	public static Sprite GetBiomeSprite(string id)
	{
		string text = "biomeIcon" + char.ToUpper(id[0]).ToString() + id.Substring(1).ToLower();
		Sprite sprite = Assets.GetSprite(text);
		if (sprite != null)
		{
			return new global::Tuple<Sprite, Color>(sprite, Color.white).first;
		}
		global::Debug.LogWarning("Missing codex biome icon: " + text);
		return null;
	}

	// Token: 0x060035D0 RID: 13776 RVA: 0x0012A0F4 File Offset: 0x001282F4
	public static string GenerateRandomDuplicantName()
	{
		string text = "";
		string text2 = "";
		bool flag = UnityEngine.Random.Range(0f, 1f) >= 0.5f;
		List<string> list = new List<string>(LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.NAME.NB)));
		list.AddRange(flag ? LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.NAME.MALE)) : LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.NAME.FEMALE)));
		string random = list.GetRandom<string>();
		if (UnityEngine.Random.Range(0f, 1f) > 0.7f)
		{
			List<string> list2 = new List<string>(LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.PREFIX.NB)));
			list2.AddRange(flag ? LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.PREFIX.MALE)) : LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.PREFIX.FEMALE)));
			text = list2.GetRandom<string>();
		}
		if (!string.IsNullOrEmpty(text))
		{
			text += " ";
		}
		if (UnityEngine.Random.Range(0f, 1f) >= 0.9f)
		{
			List<string> list3 = new List<string>(LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.SUFFIX.NB)));
			list3.AddRange(flag ? LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.SUFFIX.MALE)) : LocString.GetStrings(typeof(NAMEGEN.DUPLICANT.SUFFIX.FEMALE)));
			text2 = list3.GetRandom<string>();
		}
		if (!string.IsNullOrEmpty(text2))
		{
			text2 = " " + text2;
		}
		return text + random + text2;
	}

	// Token: 0x060035D1 RID: 13777 RVA: 0x0012A25C File Offset: 0x0012845C
	public static string GenerateRandomLaunchPadName()
	{
		return NAMEGEN.LAUNCHPAD.FORMAT.Replace("{Name}", UnityEngine.Random.Range(1, 1000).ToString());
	}

	// Token: 0x060035D2 RID: 13778 RVA: 0x0012A28C File Offset: 0x0012848C
	public static string GenerateRandomRocketName()
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		int num = 1;
		int num2 = 2;
		int num3 = 4;
		string random = new List<string>(LocString.GetStrings(typeof(NAMEGEN.ROCKET.NOUN))).GetRandom<string>();
		int num4 = 0;
		if (UnityEngine.Random.value > 0.7f)
		{
			text = new List<string>(LocString.GetStrings(typeof(NAMEGEN.ROCKET.PREFIX))).GetRandom<string>();
			num4 |= num;
		}
		if (UnityEngine.Random.value > 0.5f)
		{
			text2 = new List<string>(LocString.GetStrings(typeof(NAMEGEN.ROCKET.ADJECTIVE))).GetRandom<string>();
			num4 |= num2;
		}
		if (UnityEngine.Random.value > 0.1f)
		{
			text3 = new List<string>(LocString.GetStrings(typeof(NAMEGEN.ROCKET.SUFFIX))).GetRandom<string>();
			num4 |= num3;
		}
		string text4;
		if (num4 == (num | num2 | num3))
		{
			text4 = NAMEGEN.ROCKET.FMT_PREFIX_ADJECTIVE_NOUN_SUFFIX;
		}
		else if (num4 == (num2 | num3))
		{
			text4 = NAMEGEN.ROCKET.FMT_ADJECTIVE_NOUN_SUFFIX;
		}
		else if (num4 == (num | num3))
		{
			text4 = NAMEGEN.ROCKET.FMT_PREFIX_NOUN_SUFFIX;
		}
		else if (num4 == num3)
		{
			text4 = NAMEGEN.ROCKET.FMT_NOUN_SUFFIX;
		}
		else if (num4 == (num | num2))
		{
			text4 = NAMEGEN.ROCKET.FMT_PREFIX_ADJECTIVE_NOUN;
		}
		else if (num4 == num)
		{
			text4 = NAMEGEN.ROCKET.FMT_PREFIX_NOUN;
		}
		else if (num4 == num2)
		{
			text4 = NAMEGEN.ROCKET.FMT_ADJECTIVE_NOUN;
		}
		else
		{
			text4 = NAMEGEN.ROCKET.FMT_NOUN;
		}
		DebugUtil.LogArgs(new object[]
		{
			"Rocket name bits:",
			Convert.ToString(num4, 2)
		});
		return text4.Replace("{Prefix}", text).Replace("{Adjective}", text2).Replace("{Noun}", random)
			.Replace("{Suffix}", text3);
	}

	// Token: 0x060035D3 RID: 13779 RVA: 0x0012A454 File Offset: 0x00128654
	public static string GenerateRandomWorldName(string[] nameTables)
	{
		if (nameTables == null)
		{
			global::Debug.LogWarning("No name tables provided to generate world name. Using GENERIC");
			nameTables = new string[] { "GENERIC" };
		}
		string text = "";
		foreach (string text2 in nameTables)
		{
			text += Strings.Get("STRINGS.NAMEGEN.WORLD.ROOTS." + text2.ToUpper());
		}
		string text3 = GameUtil.RandomValueFromSeparatedString(text, "\n");
		if (string.IsNullOrEmpty(text3))
		{
			text3 = GameUtil.RandomValueFromSeparatedString(Strings.Get(NAMEGEN.WORLD.ROOTS.GENERIC), "\n");
		}
		string text4 = GameUtil.RandomValueFromSeparatedString(NAMEGEN.WORLD.SUFFIXES.GENERICLIST, "\n");
		return text3 + text4;
	}

	// Token: 0x060035D4 RID: 13780 RVA: 0x0012A510 File Offset: 0x00128710
	public static float GetThermalComfort(int cell, float tolerance = -0.08368001f)
	{
		float num = 0f;
		Element element = ElementLoader.FindElementByHash(SimHashes.Creature);
		if (Grid.Element[cell].thermalConductivity != 0f)
		{
			num = SimUtil.CalculateEnergyFlowCreatures(cell, 310.15f, element.specificHeatCapacity, element.thermalConductivity, 1f, 0.0045f);
		}
		num -= tolerance;
		return num * 1000f;
	}

	// Token: 0x060035D5 RID: 13781 RVA: 0x0012A574 File Offset: 0x00128774
	public static string RandomValueFromSeparatedString(string source, string separator = "\n")
	{
		int num = 0;
		int num2 = 0;
		for (;;)
		{
			num = source.IndexOf(separator, num);
			if (num == -1)
			{
				break;
			}
			num += separator.Length;
			num2++;
		}
		if (num2 == 0)
		{
			return "";
		}
		int num3 = UnityEngine.Random.Range(0, num2);
		num = 0;
		for (int i = 0; i < num3; i++)
		{
			num = source.IndexOf(separator, num) + separator.Length;
		}
		int num4 = source.IndexOf(separator, num);
		return source.Substring(num, (num4 == -1) ? (source.Length - num) : (num4 - num));
	}

	// Token: 0x060035D6 RID: 13782 RVA: 0x0012A5F8 File Offset: 0x001287F8
	public static string GetFormattedDiseaseName(byte idx, bool color = false)
	{
		Disease disease = Db.Get().Diseases[(int)idx];
		if (color)
		{
			return string.Format(UI.OVERLAYS.DISEASE.DISEASE_NAME_FORMAT, disease.Name, GameUtil.ColourToHex(GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName)));
		}
		return string.Format(UI.OVERLAYS.DISEASE.DISEASE_NAME_FORMAT_NO_COLOR, disease.Name);
	}

	// Token: 0x060035D7 RID: 13783 RVA: 0x0012A660 File Offset: 0x00128860
	public static string GetFormattedDisease(byte idx, int units, bool color = false)
	{
		if (idx == 255 || units <= 0)
		{
			return UI.OVERLAYS.DISEASE.NO_DISEASE;
		}
		Disease disease = Db.Get().Diseases[(int)idx];
		if (color)
		{
			return string.Format(UI.OVERLAYS.DISEASE.DISEASE_FORMAT, disease.Name, GameUtil.GetFormattedDiseaseAmount(units, GameUtil.TimeSlice.None), GameUtil.ColourToHex(GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName)));
		}
		return string.Format(UI.OVERLAYS.DISEASE.DISEASE_FORMAT_NO_COLOR, disease.Name, GameUtil.GetFormattedDiseaseAmount(units, GameUtil.TimeSlice.None));
	}

	// Token: 0x060035D8 RID: 13784 RVA: 0x0012A6EB File Offset: 0x001288EB
	public static string GetFormattedDiseaseAmount(int units, GameUtil.TimeSlice timeSlice = GameUtil.TimeSlice.None)
	{
		GameUtil.ApplyTimeSlice(units, timeSlice);
		return GameUtil.AddTimeSliceText(units.ToString("#,##0") + UI.UNITSUFFIXES.DISEASE.UNITS, timeSlice);
	}

	// Token: 0x060035D9 RID: 13785 RVA: 0x0012A716 File Offset: 0x00128916
	public static string ColourizeString(Color32 colour, string str)
	{
		return string.Format("<color=#{0}>{1}</color>", GameUtil.ColourToHex(colour), str);
	}

	// Token: 0x060035DA RID: 13786 RVA: 0x0012A72C File Offset: 0x0012892C
	public static string ColourToHex(Color32 colour)
	{
		return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", new object[] { colour.r, colour.g, colour.b, colour.a });
	}

	// Token: 0x060035DB RID: 13787 RVA: 0x0012A784 File Offset: 0x00128984
	public static string GetFormattedDecor(float value, bool enforce_max = false)
	{
		string text = "";
		LocString locString = ((value > DecorMonitor.MAXIMUM_DECOR_VALUE && enforce_max) ? UI.OVERLAYS.DECOR.MAXIMUM_DECOR : UI.OVERLAYS.DECOR.VALUE);
		if (enforce_max)
		{
			value = Math.Min(value, DecorMonitor.MAXIMUM_DECOR_VALUE);
		}
		if (value > 0f)
		{
			text = "+";
		}
		else if (value >= 0f)
		{
			locString = UI.OVERLAYS.DECOR.VALUE_ZERO;
		}
		return string.Format(locString, text, value);
	}

	// Token: 0x060035DC RID: 13788 RVA: 0x0012A7F0 File Offset: 0x001289F0
	public static Color GetDecorColourFromValue(int decor)
	{
		Color color = Color.black;
		float num = (float)decor / 100f;
		if (num > 0f)
		{
			color = Color.Lerp(new Color(0.15f, 0f, 0f), new Color(0f, 1f, 0f), Mathf.Abs(num));
		}
		else
		{
			color = Color.Lerp(new Color(0.15f, 0f, 0f), new Color(1f, 0f, 0f), Mathf.Abs(num));
		}
		return color;
	}

	// Token: 0x060035DD RID: 13789 RVA: 0x0012A880 File Offset: 0x00128A80
	public static List<Descriptor> GetMaterialDescriptors(Element element)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (element.attributeModifiers.Count > 0)
		{
			foreach (AttributeModifier attributeModifier in element.attributeModifiers)
			{
				string text = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS." + attributeModifier.AttributeId.ToUpper())), attributeModifier.GetFormattedString());
				string text2 = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP." + attributeModifier.AttributeId.ToUpper())), attributeModifier.GetFormattedString());
				Descriptor descriptor = default(Descriptor);
				descriptor.SetupDescriptor(text, text2, Descriptor.DescriptorType.Effect);
				descriptor.IncreaseIndent();
				list.Add(descriptor);
			}
		}
		list.AddRange(GameUtil.GetSignificantMaterialPropertyDescriptors(element));
		return list;
	}

	// Token: 0x060035DE RID: 13790 RVA: 0x0012A97C File Offset: 0x00128B7C
	public static string GetMaterialTooltips(Element element)
	{
		string text = element.tag.ProperName();
		foreach (AttributeModifier attributeModifier in element.attributeModifiers)
		{
			string name = Db.Get().BuildingAttributes.Get(attributeModifier.AttributeId).Name;
			string formattedString = attributeModifier.GetFormattedString();
			text = text + "\n    • " + string.Format(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, name, formattedString);
		}
		text += GameUtil.GetSignificantMaterialPropertyTooltips(element);
		return text;
	}

	// Token: 0x060035DF RID: 13791 RVA: 0x0012AA24 File Offset: 0x00128C24
	public static string GetSignificantMaterialPropertyTooltips(Element element)
	{
		string text = "";
		List<Descriptor> significantMaterialPropertyDescriptors = GameUtil.GetSignificantMaterialPropertyDescriptors(element);
		if (significantMaterialPropertyDescriptors.Count > 0)
		{
			text += "\n";
			for (int i = 0; i < significantMaterialPropertyDescriptors.Count; i++)
			{
				text = text + "    • " + Util.StripTextFormatting(significantMaterialPropertyDescriptors[i].text) + "\n";
			}
		}
		return text;
	}

	// Token: 0x060035E0 RID: 13792 RVA: 0x0012AA88 File Offset: 0x00128C88
	public static List<Descriptor> GetSignificantMaterialPropertyDescriptors(Element element)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (element.thermalConductivity > 10f)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(ELEMENTS.MATERIAL_MODIFIERS.HIGH_THERMAL_CONDUCTIVITY, GameUtil.GetThermalConductivityString(element, false, false)), string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.HIGH_THERMAL_CONDUCTIVITY, element.name, element.thermalConductivity.ToString("0.#####")), Descriptor.DescriptorType.Effect);
			descriptor.IncreaseIndent();
			list.Add(descriptor);
		}
		if (element.thermalConductivity < 1f)
		{
			Descriptor descriptor2 = default(Descriptor);
			descriptor2.SetupDescriptor(string.Format(ELEMENTS.MATERIAL_MODIFIERS.LOW_THERMAL_CONDUCTIVITY, GameUtil.GetThermalConductivityString(element, false, false)), string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.LOW_THERMAL_CONDUCTIVITY, element.name, element.thermalConductivity.ToString("0.#####")), Descriptor.DescriptorType.Effect);
			descriptor2.IncreaseIndent();
			list.Add(descriptor2);
		}
		if (element.specificHeatCapacity <= 0.2f)
		{
			Descriptor descriptor3 = default(Descriptor);
			descriptor3.SetupDescriptor(ELEMENTS.MATERIAL_MODIFIERS.LOW_SPECIFIC_HEAT_CAPACITY, string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.LOW_SPECIFIC_HEAT_CAPACITY, element.name, element.specificHeatCapacity * 1f), Descriptor.DescriptorType.Effect);
			descriptor3.IncreaseIndent();
			list.Add(descriptor3);
		}
		if (element.specificHeatCapacity >= 1f)
		{
			Descriptor descriptor4 = default(Descriptor);
			descriptor4.SetupDescriptor(ELEMENTS.MATERIAL_MODIFIERS.HIGH_SPECIFIC_HEAT_CAPACITY, string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.HIGH_SPECIFIC_HEAT_CAPACITY, element.name, element.specificHeatCapacity * 1f), Descriptor.DescriptorType.Effect);
			descriptor4.IncreaseIndent();
			list.Add(descriptor4);
		}
		if (Sim.IsRadiationEnabled() && element.radiationAbsorptionFactor >= 0.8f)
		{
			Descriptor descriptor5 = default(Descriptor);
			descriptor5.SetupDescriptor(ELEMENTS.MATERIAL_MODIFIERS.EXCELLENT_RADIATION_SHIELD, string.Format(ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.EXCELLENT_RADIATION_SHIELD, element.name, element.radiationAbsorptionFactor), Descriptor.DescriptorType.Effect);
			descriptor5.IncreaseIndent();
			list.Add(descriptor5);
		}
		return list;
	}

	// Token: 0x060035E1 RID: 13793 RVA: 0x0012AC83 File Offset: 0x00128E83
	public static int NaturalBuildingCell(this KMonoBehaviour cmp)
	{
		return Grid.PosToCell(cmp.transform.GetPosition());
	}

	// Token: 0x060035E2 RID: 13794 RVA: 0x0012AC98 File Offset: 0x00128E98
	public static List<Descriptor> GetMaterialDescriptors(Tag tag)
	{
		List<Descriptor> list = new List<Descriptor>();
		Element element = ElementLoader.GetElement(tag);
		if (element != null)
		{
			if (element.attributeModifiers.Count > 0)
			{
				foreach (AttributeModifier attributeModifier in element.attributeModifiers)
				{
					string text = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS." + attributeModifier.AttributeId.ToUpper())), attributeModifier.GetFormattedString());
					string text2 = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP." + attributeModifier.AttributeId.ToUpper())), attributeModifier.GetFormattedString());
					Descriptor descriptor = default(Descriptor);
					descriptor.SetupDescriptor(text, text2, Descriptor.DescriptorType.Effect);
					descriptor.IncreaseIndent();
					list.Add(descriptor);
				}
			}
			list.AddRange(GameUtil.GetSignificantMaterialPropertyDescriptors(element));
		}
		else
		{
			GameObject gameObject = Assets.TryGetPrefab(tag);
			if (gameObject != null)
			{
				PrefabAttributeModifiers component = gameObject.GetComponent<PrefabAttributeModifiers>();
				if (component != null)
				{
					foreach (AttributeModifier attributeModifier2 in component.descriptors)
					{
						string text3 = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS." + attributeModifier2.AttributeId.ToUpper())), attributeModifier2.GetFormattedString());
						string text4 = string.Format(Strings.Get(new StringKey("STRINGS.ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP." + attributeModifier2.AttributeId.ToUpper())), attributeModifier2.GetFormattedString());
						Descriptor descriptor2 = default(Descriptor);
						descriptor2.SetupDescriptor(text3, text4, Descriptor.DescriptorType.Effect);
						descriptor2.IncreaseIndent();
						list.Add(descriptor2);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x060035E3 RID: 13795 RVA: 0x0012AEA0 File Offset: 0x001290A0
	public static string GetMaterialTooltips(Tag tag)
	{
		string text = tag.ProperName();
		Element element = ElementLoader.GetElement(tag);
		if (element != null)
		{
			foreach (AttributeModifier attributeModifier in element.attributeModifiers)
			{
				string name = Db.Get().BuildingAttributes.Get(attributeModifier.AttributeId).Name;
				string formattedString = attributeModifier.GetFormattedString();
				text = text + "\n    • " + string.Format(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, name, formattedString);
			}
			text += GameUtil.GetSignificantMaterialPropertyTooltips(element);
		}
		else
		{
			GameObject gameObject = Assets.TryGetPrefab(tag);
			if (gameObject != null)
			{
				PrefabAttributeModifiers component = gameObject.GetComponent<PrefabAttributeModifiers>();
				if (component != null)
				{
					foreach (AttributeModifier attributeModifier2 in component.descriptors)
					{
						string name2 = Db.Get().BuildingAttributes.Get(attributeModifier2.AttributeId).Name;
						string formattedString2 = attributeModifier2.GetFormattedString();
						text = text + "\n    • " + string.Format(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, name2, formattedString2);
					}
				}
			}
		}
		return text;
	}

	// Token: 0x060035E4 RID: 13796 RVA: 0x0012B000 File Offset: 0x00129200
	public static bool AreChoresUIMergeable(Chore.Precondition.Context choreA, Chore.Precondition.Context choreB)
	{
		if (choreA.chore.target.isNull || choreB.chore.target.isNull)
		{
			return false;
		}
		ChoreType choreType = choreB.chore.choreType;
		ChoreType choreType2 = choreA.chore.choreType;
		return (choreA.chore.choreType == choreB.chore.choreType && choreA.chore.target.GetComponent<KPrefabID>().PrefabTag == choreB.chore.target.GetComponent<KPrefabID>().PrefabTag) || (choreA.chore.choreType == Db.Get().ChoreTypes.Dig && choreB.chore.choreType == Db.Get().ChoreTypes.Dig) || (choreA.chore.choreType == Db.Get().ChoreTypes.Relax && choreB.chore.choreType == Db.Get().ChoreTypes.Relax) || ((choreType2 == Db.Get().ChoreTypes.ReturnSuitIdle || choreType2 == Db.Get().ChoreTypes.ReturnSuitUrgent) && (choreType == Db.Get().ChoreTypes.ReturnSuitIdle || choreType == Db.Get().ChoreTypes.ReturnSuitUrgent)) || (choreA.chore.target.gameObject == choreB.chore.target.gameObject && choreA.chore.choreType == choreB.chore.choreType);
	}

	// Token: 0x060035E5 RID: 13797 RVA: 0x0012B198 File Offset: 0x00129398
	public static string GetChoreName(Chore chore, object choreData)
	{
		string text = "";
		if (chore.choreType == Db.Get().ChoreTypes.Fetch || chore.choreType == Db.Get().ChoreTypes.MachineFetch || chore.choreType == Db.Get().ChoreTypes.FabricateFetch || chore.choreType == Db.Get().ChoreTypes.FetchCritical || chore.choreType == Db.Get().ChoreTypes.PowerFetch)
		{
			text = chore.GetReportName(chore.gameObject.GetProperName());
		}
		else if (chore.choreType == Db.Get().ChoreTypes.StorageFetch || chore.choreType == Db.Get().ChoreTypes.FoodFetch)
		{
			FetchChore fetchChore = chore as FetchChore;
			FetchAreaChore fetchAreaChore = chore as FetchAreaChore;
			if (fetchAreaChore != null)
			{
				GameObject getFetchTarget = fetchAreaChore.GetFetchTarget;
				KMonoBehaviour kmonoBehaviour = choreData as KMonoBehaviour;
				if (getFetchTarget != null)
				{
					text = chore.GetReportName(getFetchTarget.GetProperName());
				}
				else if (kmonoBehaviour != null)
				{
					text = chore.GetReportName(kmonoBehaviour.GetProperName());
				}
				else
				{
					text = chore.GetReportName(null);
				}
			}
			else if (fetchChore != null)
			{
				Pickupable fetchTarget = fetchChore.fetchTarget;
				KMonoBehaviour kmonoBehaviour2 = choreData as KMonoBehaviour;
				if (fetchTarget != null)
				{
					text = chore.GetReportName(fetchTarget.GetProperName());
				}
				else if (kmonoBehaviour2 != null)
				{
					text = chore.GetReportName(kmonoBehaviour2.GetProperName());
				}
				else
				{
					text = chore.GetReportName(null);
				}
			}
		}
		else
		{
			text = chore.GetReportName(null);
		}
		return text;
	}

	// Token: 0x060035E6 RID: 13798 RVA: 0x0012B31C File Offset: 0x0012951C
	public static string ChoreGroupsForChoreType(ChoreType choreType)
	{
		if (choreType.groups == null || choreType.groups.Length == 0)
		{
			return null;
		}
		string text = "";
		for (int i = 0; i < choreType.groups.Length; i++)
		{
			if (i != 0)
			{
				text += UI.UISIDESCREENS.MINIONTODOSIDESCREEN.CHORE_GROUP_SEPARATOR;
			}
			text += choreType.groups[i].Name;
		}
		return text;
	}

	// Token: 0x060035E7 RID: 13799 RVA: 0x0012B37E File Offset: 0x0012957E
	public static bool IsCapturingTimeLapse()
	{
		return Game.Instance != null && Game.Instance.timelapser != null && Game.Instance.timelapser.CapturingTimelapseScreenshot;
	}

	// Token: 0x060035E8 RID: 13800 RVA: 0x0012B3B0 File Offset: 0x001295B0
	public static ExposureType GetExposureTypeForDisease(Disease disease)
	{
		for (int i = 0; i < GERM_EXPOSURE.TYPES.Length; i++)
		{
			if (disease.id == GERM_EXPOSURE.TYPES[i].germ_id)
			{
				return GERM_EXPOSURE.TYPES[i];
			}
		}
		return null;
	}

	// Token: 0x060035E9 RID: 13801 RVA: 0x0012B3F8 File Offset: 0x001295F8
	public static Sickness GetSicknessForDisease(Disease disease)
	{
		int i = 0;
		while (i < GERM_EXPOSURE.TYPES.Length)
		{
			if (disease.id == GERM_EXPOSURE.TYPES[i].germ_id)
			{
				if (GERM_EXPOSURE.TYPES[i].sickness_id == null)
				{
					return null;
				}
				return Db.Get().Sicknesses.Get(GERM_EXPOSURE.TYPES[i].sickness_id);
			}
			else
			{
				i++;
			}
		}
		return null;
	}

	// Token: 0x060035EA RID: 13802 RVA: 0x0012B462 File Offset: 0x00129662
	public static void SubscribeToTags<T>(T target, EventSystem.IntraObjectHandler<T> handler, bool triggerImmediately) where T : KMonoBehaviour
	{
		if (triggerImmediately)
		{
			handler.Trigger(target.gameObject, new TagChangedEventData(Tag.Invalid, false));
		}
		target.Subscribe<T>(-1582839653, handler);
	}

	// Token: 0x060035EB RID: 13803 RVA: 0x0012B49A File Offset: 0x0012969A
	public static void UnsubscribeToTags<T>(T target, EventSystem.IntraObjectHandler<T> handler) where T : KMonoBehaviour
	{
		target.Unsubscribe<T>(-1582839653, handler, false);
	}

	// Token: 0x060035EC RID: 13804 RVA: 0x0012B4AE File Offset: 0x001296AE
	public static EventSystem.IntraObjectHandler<T> CreateHasTagHandler<T>(Tag tag, Action<T, object> callback) where T : KMonoBehaviour
	{
		return new EventSystem.IntraObjectHandler<T>(delegate(T component, object data)
		{
			TagChangedEventData tagChangedEventData = (TagChangedEventData)data;
			if (tagChangedEventData.tag == Tag.Invalid)
			{
				KPrefabID component2 = component.GetComponent<KPrefabID>();
				tagChangedEventData = new TagChangedEventData(tag, component2.HasTag(tag));
			}
			if (tagChangedEventData.tag == tag && tagChangedEventData.added)
			{
				callback(component, data);
			}
		});
	}

	// Token: 0x04002400 RID: 9216
	public static GameUtil.TemperatureUnit temperatureUnit;

	// Token: 0x04002401 RID: 9217
	public static GameUtil.MassUnit massUnit;

	// Token: 0x04002402 RID: 9218
	private static string[] adjectives;

	// Token: 0x04002403 RID: 9219
	[ThreadStatic]
	public static Queue<GameUtil.FloodFillInfo> FloodFillNext = new Queue<GameUtil.FloodFillInfo>();

	// Token: 0x04002404 RID: 9220
	[ThreadStatic]
	public static HashSet<int> FloodFillVisited = new HashSet<int>();

	// Token: 0x04002405 RID: 9221
	public static TagSet foodTags = new TagSet(new string[]
	{
		"BasicPlantFood",
		"MushBar",
		"ColdWheatSeed",
		"ColdWheatSeed",
		"SpiceNut",
		"PrickleFruit",
		"Meat",
		"Mushroom",
		"ColdWheat",
		GameTags.Compostable.Name
	});

	// Token: 0x04002406 RID: 9222
	public static TagSet solidTags = new TagSet(new string[] { "Filter", "Coal", "BasicFabric", "SwampLilyFlower", "RefinedMetal" });

	// Token: 0x020014A1 RID: 5281
	public enum UnitClass
	{
		// Token: 0x04006423 RID: 25635
		SimpleFloat,
		// Token: 0x04006424 RID: 25636
		SimpleInteger,
		// Token: 0x04006425 RID: 25637
		Temperature,
		// Token: 0x04006426 RID: 25638
		Mass,
		// Token: 0x04006427 RID: 25639
		Calories,
		// Token: 0x04006428 RID: 25640
		Percent,
		// Token: 0x04006429 RID: 25641
		Distance,
		// Token: 0x0400642A RID: 25642
		Disease,
		// Token: 0x0400642B RID: 25643
		Radiation,
		// Token: 0x0400642C RID: 25644
		Energy,
		// Token: 0x0400642D RID: 25645
		Power,
		// Token: 0x0400642E RID: 25646
		Lux,
		// Token: 0x0400642F RID: 25647
		Time,
		// Token: 0x04006430 RID: 25648
		Seconds,
		// Token: 0x04006431 RID: 25649
		Cycles
	}

	// Token: 0x020014A2 RID: 5282
	public enum TemperatureUnit
	{
		// Token: 0x04006433 RID: 25651
		Celsius,
		// Token: 0x04006434 RID: 25652
		Fahrenheit,
		// Token: 0x04006435 RID: 25653
		Kelvin
	}

	// Token: 0x020014A3 RID: 5283
	public enum MassUnit
	{
		// Token: 0x04006437 RID: 25655
		Kilograms,
		// Token: 0x04006438 RID: 25656
		Pounds
	}

	// Token: 0x020014A4 RID: 5284
	public enum MetricMassFormat
	{
		// Token: 0x0400643A RID: 25658
		UseThreshold,
		// Token: 0x0400643B RID: 25659
		Kilogram,
		// Token: 0x0400643C RID: 25660
		Gram,
		// Token: 0x0400643D RID: 25661
		Tonne
	}

	// Token: 0x020014A5 RID: 5285
	public enum TemperatureInterpretation
	{
		// Token: 0x0400643F RID: 25663
		Absolute,
		// Token: 0x04006440 RID: 25664
		Relative
	}

	// Token: 0x020014A6 RID: 5286
	public enum TimeSlice
	{
		// Token: 0x04006442 RID: 25666
		None,
		// Token: 0x04006443 RID: 25667
		ModifyOnly,
		// Token: 0x04006444 RID: 25668
		PerSecond,
		// Token: 0x04006445 RID: 25669
		PerCycle
	}

	// Token: 0x020014A7 RID: 5287
	public enum MeasureUnit
	{
		// Token: 0x04006447 RID: 25671
		mass,
		// Token: 0x04006448 RID: 25672
		kcal,
		// Token: 0x04006449 RID: 25673
		quantity
	}

	// Token: 0x020014A8 RID: 5288
	public enum IdentityDescriptorTense
	{
		// Token: 0x0400644B RID: 25675
		Normal,
		// Token: 0x0400644C RID: 25676
		Possessive,
		// Token: 0x0400644D RID: 25677
		Plural
	}

	// Token: 0x020014A9 RID: 5289
	public enum WattageFormatterUnit
	{
		// Token: 0x0400644F RID: 25679
		Watts,
		// Token: 0x04006450 RID: 25680
		Kilowatts,
		// Token: 0x04006451 RID: 25681
		Automatic
	}

	// Token: 0x020014AA RID: 5290
	public enum HeatEnergyFormatterUnit
	{
		// Token: 0x04006453 RID: 25683
		DTU_S,
		// Token: 0x04006454 RID: 25684
		KDTU_S,
		// Token: 0x04006455 RID: 25685
		Automatic
	}

	// Token: 0x020014AB RID: 5291
	public struct FloodFillInfo
	{
		// Token: 0x04006456 RID: 25686
		public int cell;

		// Token: 0x04006457 RID: 25687
		public int depth;
	}

	// Token: 0x020014AC RID: 5292
	public static class Hardness
	{
		// Token: 0x04006458 RID: 25688
		public const int VERY_SOFT = 0;

		// Token: 0x04006459 RID: 25689
		public const int SOFT = 10;

		// Token: 0x0400645A RID: 25690
		public const int FIRM = 25;

		// Token: 0x0400645B RID: 25691
		public const int VERY_FIRM = 50;

		// Token: 0x0400645C RID: 25692
		public const int NEARLY_IMPENETRABLE = 150;

		// Token: 0x0400645D RID: 25693
		public const int SUPER_DUPER_HARD = 200;

		// Token: 0x0400645E RID: 25694
		public const int RADIOACTIVE_MATERIALS = 251;

		// Token: 0x0400645F RID: 25695
		public const int IMPENETRABLE = 255;

		// Token: 0x04006460 RID: 25696
		public static Color ImpenetrableColor = new Color(0.83137256f, 0.28627452f, 0.28235295f);

		// Token: 0x04006461 RID: 25697
		public static Color nearlyImpenetrableColor = new Color(0.7411765f, 0.34901962f, 0.49803922f);

		// Token: 0x04006462 RID: 25698
		public static Color veryFirmColor = new Color(0.6392157f, 0.39215687f, 0.6039216f);

		// Token: 0x04006463 RID: 25699
		public static Color firmColor = new Color(0.5254902f, 0.41960785f, 0.64705884f);

		// Token: 0x04006464 RID: 25700
		public static Color softColor = new Color(0.42745098f, 0.48235294f, 0.75686276f);

		// Token: 0x04006465 RID: 25701
		public static Color verySoftColor = new Color(0.44313726f, 0.67058825f, 0.8117647f);
	}

	// Token: 0x020014AD RID: 5293
	public static class GermResistanceValues
	{
		// Token: 0x04006466 RID: 25702
		public const float MEDIUM = 2f;

		// Token: 0x04006467 RID: 25703
		public const float LARGE = 5f;

		// Token: 0x04006468 RID: 25704
		public static Color NegativeLargeColor = new Color(0.83137256f, 0.28627452f, 0.28235295f);

		// Token: 0x04006469 RID: 25705
		public static Color NegativeMediumColor = new Color(0.7411765f, 0.34901962f, 0.49803922f);

		// Token: 0x0400646A RID: 25706
		public static Color NegativeSmallColor = new Color(0.6392157f, 0.39215687f, 0.6039216f);

		// Token: 0x0400646B RID: 25707
		public static Color PositiveSmallColor = new Color(0.5254902f, 0.41960785f, 0.64705884f);

		// Token: 0x0400646C RID: 25708
		public static Color PositiveMediumColor = new Color(0.42745098f, 0.48235294f, 0.75686276f);

		// Token: 0x0400646D RID: 25709
		public static Color PositiveLargeColor = new Color(0.44313726f, 0.67058825f, 0.8117647f);
	}

	// Token: 0x020014AE RID: 5294
	public static class ThermalConductivityValues
	{
		// Token: 0x0400646E RID: 25710
		public const float VERY_HIGH = 50f;

		// Token: 0x0400646F RID: 25711
		public const float HIGH = 10f;

		// Token: 0x04006470 RID: 25712
		public const float MEDIUM = 2f;

		// Token: 0x04006471 RID: 25713
		public const float LOW = 1f;

		// Token: 0x04006472 RID: 25714
		public static Color veryLowConductivityColor = new Color(0.83137256f, 0.28627452f, 0.28235295f);

		// Token: 0x04006473 RID: 25715
		public static Color lowConductivityColor = new Color(0.7411765f, 0.34901962f, 0.49803922f);

		// Token: 0x04006474 RID: 25716
		public static Color mediumConductivityColor = new Color(0.6392157f, 0.39215687f, 0.6039216f);

		// Token: 0x04006475 RID: 25717
		public static Color highConductivityColor = new Color(0.5254902f, 0.41960785f, 0.64705884f);

		// Token: 0x04006476 RID: 25718
		public static Color veryHighConductivityColor = new Color(0.42745098f, 0.48235294f, 0.75686276f);
	}

	// Token: 0x020014AF RID: 5295
	public static class BreathableValues
	{
		// Token: 0x04006477 RID: 25719
		public static Color positiveColor = new Color(0.44313726f, 0.67058825f, 0.8117647f);

		// Token: 0x04006478 RID: 25720
		public static Color warningColor = new Color(0.6392157f, 0.39215687f, 0.6039216f);

		// Token: 0x04006479 RID: 25721
		public static Color negativeColor = new Color(0.83137256f, 0.28627452f, 0.28235295f);
	}

	// Token: 0x020014B0 RID: 5296
	public static class WireLoadValues
	{
		// Token: 0x0400647A RID: 25722
		public static Color warningColor = new Color(0.9843137f, 0.6901961f, 0.23137255f);

		// Token: 0x0400647B RID: 25723
		public static Color negativeColor = new Color(1f, 0.19215687f, 0.19215687f);
	}
}
