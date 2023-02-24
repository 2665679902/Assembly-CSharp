using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000C73 RID: 3187
	public class Amounts : ResourceSet<Amount>
	{
		// Token: 0x06006511 RID: 25873 RVA: 0x0025BBD0 File Offset: 0x00259DD0
		public void Load()
		{
			this.Stamina = this.CreateAmount("Stamina", 0f, 100f, false, Units.Flat, 0.35f, true, "STRINGS.DUPLICANTS", "ui_icon_stamina", "attribute_stamina", "mod_stamina");
			this.Stamina.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal));
			this.Calories = this.CreateAmount("Calories", 0f, 0f, false, Units.Flat, 4000f, true, "STRINGS.DUPLICANTS", "ui_icon_calories", "attribute_calories", "mod_calories");
			this.Calories.SetDisplayer(new CaloriesDisplayer());
			this.ExternalTemperature = this.CreateAmount("ExternalTemperature", 0f, 10000f, false, Units.Kelvin, 0.5f, true, "STRINGS.DUPLICANTS", null, null, null);
			this.ExternalTemperature.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Temperature, GameUtil.TimeSlice.PerSecond, null, GameUtil.IdentityDescriptorTense.Normal));
			this.Breath = this.CreateAmount("Breath", 0f, 100f, false, Units.Flat, 0.5f, true, "STRINGS.DUPLICANTS", "ui_icon_breath", null, "mod_breath");
			this.Breath.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.PerSecond, null, GameUtil.IdentityDescriptorTense.Normal));
			this.Stress = this.CreateAmount("Stress", 0f, 100f, false, Units.Flat, 0.5f, true, "STRINGS.DUPLICANTS", "ui_icon_stress", "attribute_stress", "mod_stress");
			this.Stress.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.Toxicity = this.CreateAmount("Toxicity", 0f, 100f, true, Units.Flat, 0.5f, true, "STRINGS.DUPLICANTS", null, null, null);
			this.Toxicity.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal));
			this.Bladder = this.CreateAmount("Bladder", 0f, 100f, false, Units.Flat, 0.5f, true, "STRINGS.DUPLICANTS", "ui_icon_bladder", null, "mod_bladder");
			this.Bladder.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal));
			this.Decor = this.CreateAmount("Decor", -1000f, 1000f, false, Units.Flat, 0.016666668f, true, "STRINGS.DUPLICANTS", "ui_icon_decor", null, "mod_decor");
			this.Decor.SetDisplayer(new DecorDisplayer());
			this.RadiationBalance = this.CreateAmount("RadiationBalance", 0f, 10000f, false, Units.Flat, 0.5f, true, "STRINGS.DUPLICANTS", "ui_icon_radiation", null, "mod_health");
			this.RadiationBalance.SetDisplayer(new RadiationBalanceDisplayer());
			this.Temperature = this.CreateAmount("Temperature", 0f, 10000f, false, Units.Kelvin, 0.5f, true, "STRINGS.DUPLICANTS", "ui_icon_temperature", null, null);
			this.Temperature.SetDisplayer(new DuplicantTemperatureDeltaAsEnergyAmountDisplayer(GameUtil.UnitClass.Temperature, GameUtil.TimeSlice.PerSecond));
			this.HitPoints = this.CreateAmount("HitPoints", 0f, 0f, true, Units.Flat, 0.1675f, true, "STRINGS.DUPLICANTS", "ui_icon_hitpoints", "attribute_hitpoints", "mod_health");
			this.HitPoints.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Possessive));
			this.AirPressure = this.CreateAmount("AirPressure", 0f, 1E+09f, false, Units.Flat, 0f, true, "STRINGS.CREATURES", null, null, null);
			this.AirPressure.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Mass, GameUtil.TimeSlice.PerSecond, null, GameUtil.IdentityDescriptorTense.Normal));
			this.Maturity = this.CreateAmount("Maturity", 0f, 0f, true, Units.Flat, 0.0009166667f, true, "STRINGS.CREATURES", "ui_icon_maturity", null, null);
			this.Maturity.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Cycles, GameUtil.TimeSlice.None, null, GameUtil.IdentityDescriptorTense.Normal));
			this.OldAge = this.CreateAmount("OldAge", 0f, 0f, false, Units.Flat, 0f, false, "STRINGS.CREATURES", null, null, null);
			this.Fertilization = this.CreateAmount("Fertilization", 0f, 100f, true, Units.Flat, 0.1675f, true, "STRINGS.CREATURES", null, null, null);
			this.Fertilization.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.PerSecond, null, GameUtil.IdentityDescriptorTense.Normal));
			this.Fertility = this.CreateAmount("Fertility", 0f, 100f, true, Units.Flat, 0.008375f, true, "STRINGS.CREATURES", "ui_icon_fertility", null, null);
			this.Fertility.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.Wildness = this.CreateAmount("Wildness", 0f, 100f, true, Units.Flat, 0.1675f, true, "STRINGS.CREATURES", "ui_icon_wildness", null, null);
			this.Wildness.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.Incubation = this.CreateAmount("Incubation", 0f, 100f, true, Units.Flat, 0.01675f, true, "STRINGS.CREATURES", "ui_icon_incubation", null, null);
			this.Incubation.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.Viability = this.CreateAmount("Viability", 0f, 100f, true, Units.Flat, 0.1675f, true, "STRINGS.CREATURES", "ui_icon_viability", null, null);
			this.Viability.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.PowerCharge = this.CreateAmount("PowerCharge", 0f, 100f, true, Units.Flat, 0.1675f, true, "STRINGS.CREATURES", null, null, null);
			this.PowerCharge.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.Age = this.CreateAmount("Age", 0f, 0f, true, Units.Flat, 0.1675f, true, "STRINGS.CREATURES", "ui_icon_age", null, null);
			this.Age.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal));
			this.Irrigation = this.CreateAmount("Irrigation", 0f, 1f, true, Units.Flat, 0.1675f, true, "STRINGS.CREATURES", null, null, null);
			this.Irrigation.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.PerSecond, null, GameUtil.IdentityDescriptorTense.Normal));
			this.ImmuneLevel = this.CreateAmount("ImmuneLevel", 0f, 100f, true, Units.Flat, 0.1675f, true, "STRINGS.DUPLICANTS", "ui_icon_immunelevel", "attribute_immunelevel", null);
			this.ImmuneLevel.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.Rot = this.CreateAmount("Rot", 0f, 0f, false, Units.Flat, 0f, true, "STRINGS.CREATURES", null, null, null);
			this.Rot.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.Illumination = this.CreateAmount("Illumination", 0f, 1f, false, Units.Flat, 0f, true, "STRINGS.CREATURES", null, null, null);
			this.Illumination.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.None, null, GameUtil.IdentityDescriptorTense.Normal));
			this.ScaleGrowth = this.CreateAmount("ScaleGrowth", 0f, 100f, true, Units.Flat, 0.1675f, true, "STRINGS.CREATURES", "ui_icon_scale_growth", null, null);
			this.ScaleGrowth.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.ElementGrowth = this.CreateAmount("ElementGrowth", 0f, 100f, true, Units.Flat, 0.1675f, true, "STRINGS.CREATURES", "ui_icon_scale_growth", null, null);
			this.ElementGrowth.SetDisplayer(new AsPercentAmountDisplayer(GameUtil.TimeSlice.PerCycle));
			this.InternalBattery = this.CreateAmount("InternalBattery", 0f, 0f, true, Units.Flat, 4000f, true, "STRINGS.ROBOTS", "ui_icon_battery", null, null);
			this.InternalBattery.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Energy, GameUtil.TimeSlice.PerSecond, null, GameUtil.IdentityDescriptorTense.Normal));
			this.InternalChemicalBattery = this.CreateAmount("InternalChemicalBattery", 0f, 0f, true, Units.Flat, 4000f, true, "STRINGS.ROBOTS", "ui_icon_battery", null, null);
			this.InternalChemicalBattery.SetDisplayer(new StandardAmountDisplayer(GameUtil.UnitClass.Energy, GameUtil.TimeSlice.PerSecond, null, GameUtil.IdentityDescriptorTense.Normal));
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x0025C348 File Offset: 0x0025A548
		public Amount CreateAmount(string id, float min, float max, bool show_max, Units units, float delta_threshold, bool show_in_ui, string string_root, string uiSprite = null, string thoughtSprite = null, string uiFullColourSprite = null)
		{
			string text = Strings.Get(string.Format("{1}.STATS.{0}.NAME", id.ToUpper(), string_root.ToUpper()));
			string text2 = Strings.Get(string.Format("{1}.STATS.{0}.TOOLTIP", id.ToUpper(), string_root.ToUpper()));
			Klei.AI.Attribute.Display display = (show_in_ui ? Klei.AI.Attribute.Display.Normal : Klei.AI.Attribute.Display.Never);
			string text3 = id + "Min";
			StringEntry stringEntry;
			string text4 = (Strings.TryGet(new StringKey(string.Format("{1}.ATTRIBUTES.{0}.NAME", text3.ToUpper(), string_root)), out stringEntry) ? stringEntry.String : ("Minimum" + text));
			StringEntry stringEntry2;
			string text5 = (Strings.TryGet(new StringKey(string.Format("{1}.ATTRIBUTES.{0}.DESC", text3.ToUpper(), string_root)), out stringEntry2) ? stringEntry2.String : ("Minimum" + text));
			Klei.AI.Attribute attribute = new Klei.AI.Attribute(id + "Min", text4, "", text5, min, display, false, null, null, uiFullColourSprite);
			string text6 = id + "Max";
			StringEntry stringEntry3;
			string text7 = (Strings.TryGet(new StringKey(string.Format("{1}.ATTRIBUTES.{0}.NAME", text6.ToUpper(), string_root)), out stringEntry3) ? stringEntry3.String : ("Maximum" + text));
			StringEntry stringEntry4;
			string text8 = (Strings.TryGet(new StringKey(string.Format("{1}.ATTRIBUTES.{0}.DESC", text6.ToUpper(), string_root)), out stringEntry4) ? stringEntry4.String : ("Maximum" + text));
			Klei.AI.Attribute attribute2 = new Klei.AI.Attribute(id + "Max", text7, "", text8, max, display, false, null, null, uiFullColourSprite);
			string text9 = id + "Delta";
			string text10 = Strings.Get(string.Format("{1}.ATTRIBUTES.{0}.NAME", text9.ToUpper(), string_root));
			string text11 = Strings.Get(string.Format("{1}.ATTRIBUTES.{0}.DESC", text9.ToUpper(), string_root));
			Klei.AI.Attribute attribute3 = new Klei.AI.Attribute(text9, text10, "", text11, 0f, Klei.AI.Attribute.Display.Normal, false, null, null, uiFullColourSprite);
			Amount amount = new Amount(id, text, text2, attribute, attribute2, attribute3, show_max, units, delta_threshold, show_in_ui, uiSprite, thoughtSprite);
			Db.Get().Attributes.Add(attribute);
			Db.Get().Attributes.Add(attribute2);
			Db.Get().Attributes.Add(attribute3);
			base.Add(amount);
			return amount;
		}

		// Token: 0x040045E7 RID: 17895
		public Amount Stamina;

		// Token: 0x040045E8 RID: 17896
		public Amount Calories;

		// Token: 0x040045E9 RID: 17897
		public Amount ImmuneLevel;

		// Token: 0x040045EA RID: 17898
		public Amount ExternalTemperature;

		// Token: 0x040045EB RID: 17899
		public Amount Breath;

		// Token: 0x040045EC RID: 17900
		public Amount Stress;

		// Token: 0x040045ED RID: 17901
		public Amount Toxicity;

		// Token: 0x040045EE RID: 17902
		public Amount Bladder;

		// Token: 0x040045EF RID: 17903
		public Amount Decor;

		// Token: 0x040045F0 RID: 17904
		public Amount RadiationBalance;

		// Token: 0x040045F1 RID: 17905
		public Amount Temperature;

		// Token: 0x040045F2 RID: 17906
		public Amount HitPoints;

		// Token: 0x040045F3 RID: 17907
		public Amount AirPressure;

		// Token: 0x040045F4 RID: 17908
		public Amount Maturity;

		// Token: 0x040045F5 RID: 17909
		public Amount OldAge;

		// Token: 0x040045F6 RID: 17910
		public Amount Age;

		// Token: 0x040045F7 RID: 17911
		public Amount Fertilization;

		// Token: 0x040045F8 RID: 17912
		public Amount Illumination;

		// Token: 0x040045F9 RID: 17913
		public Amount Irrigation;

		// Token: 0x040045FA RID: 17914
		public Amount CreatureCalories;

		// Token: 0x040045FB RID: 17915
		public Amount Fertility;

		// Token: 0x040045FC RID: 17916
		public Amount Viability;

		// Token: 0x040045FD RID: 17917
		public Amount PowerCharge;

		// Token: 0x040045FE RID: 17918
		public Amount Wildness;

		// Token: 0x040045FF RID: 17919
		public Amount Incubation;

		// Token: 0x04004600 RID: 17920
		public Amount ScaleGrowth;

		// Token: 0x04004601 RID: 17921
		public Amount ElementGrowth;

		// Token: 0x04004602 RID: 17922
		public Amount InternalBattery;

		// Token: 0x04004603 RID: 17923
		public Amount InternalChemicalBattery;

		// Token: 0x04004604 RID: 17924
		public Amount Rot;
	}
}
