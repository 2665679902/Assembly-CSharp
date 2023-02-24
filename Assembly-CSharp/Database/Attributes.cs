using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000C7B RID: 3195
	public class Attributes : ResourceSet<Klei.AI.Attribute>
	{
		// Token: 0x06006526 RID: 25894 RVA: 0x0025E1C4 File Offset: 0x0025C3C4
		public Attributes(ResourceSet parent)
			: base("Attributes", parent)
		{
			this.Construction = base.Add(new Klei.AI.Attribute("Construction", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_construction", null));
			this.Construction.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Digging = base.Add(new Klei.AI.Attribute("Digging", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_excavation", null));
			this.Digging.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Machinery = base.Add(new Klei.AI.Attribute("Machinery", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_machinery", null));
			this.Machinery.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Athletics = base.Add(new Klei.AI.Attribute("Athletics", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_athletics", null));
			this.Athletics.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Learning = base.Add(new Klei.AI.Attribute("Learning", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_science", null));
			this.Learning.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Cooking = base.Add(new Klei.AI.Attribute("Cooking", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_cusine", null));
			this.Cooking.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Caring = base.Add(new Klei.AI.Attribute("Caring", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_medicine", null));
			this.Caring.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Strength = base.Add(new Klei.AI.Attribute("Strength", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_strength", null));
			this.Strength.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Art = base.Add(new Klei.AI.Attribute("Art", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_creativity", null));
			this.Art.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Botanist = base.Add(new Klei.AI.Attribute("Botanist", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_agriculture", null));
			this.Botanist.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Ranching = base.Add(new Klei.AI.Attribute("Ranching", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, "mod_husbandry", null));
			this.Ranching.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.PowerTinker = base.Add(new Klei.AI.Attribute("PowerTinker", true, Klei.AI.Attribute.Display.Normal, true, 0f, null, null, null, null));
			this.PowerTinker.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.FarmTinker = base.Add(new Klei.AI.Attribute("FarmTinker", true, Klei.AI.Attribute.Display.Normal, true, 0f, null, null, null, null));
			this.FarmTinker.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			if (DlcManager.IsExpansion1Active())
			{
				this.SpaceNavigation = base.Add(new Klei.AI.Attribute("SpaceNavigation", true, Klei.AI.Attribute.Display.Skill, true, 0f, null, null, null, null));
				this.SpaceNavigation.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			}
			else
			{
				this.SpaceNavigation = base.Add(new Klei.AI.Attribute("SpaceNavigation", true, Klei.AI.Attribute.Display.Normal, true, 0f, null, null, null, null));
				this.SpaceNavigation.SetFormatter(new PercentAttributeFormatter());
			}
			this.Immunity = base.Add(new Klei.AI.Attribute("Immunity", true, Klei.AI.Attribute.Display.Details, false, 0f, null, null, null, null));
			this.Immunity.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.ThermalConductivityBarrier = base.Add(new Klei.AI.Attribute("ThermalConductivityBarrier", false, Klei.AI.Attribute.Display.Details, false, 0f, null, null, null, null));
			this.ThermalConductivityBarrier.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Distance, GameUtil.TimeSlice.None));
			this.Insulation = base.Add(new Klei.AI.Attribute("Insulation", false, Klei.AI.Attribute.Display.General, true, 0f, null, null, null, null));
			this.Decor = base.Add(new Klei.AI.Attribute("Decor", false, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.Decor.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.FoodQuality = base.Add(new Klei.AI.Attribute("FoodQuality", false, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.FoodQuality.SetFormatter(new FoodQualityAttributeFormatter());
			this.ScaldingThreshold = base.Add(new Klei.AI.Attribute("ScaldingThreshold", false, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.ScaldingThreshold.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Temperature, GameUtil.TimeSlice.None));
			this.GeneratorOutput = base.Add(new Klei.AI.Attribute("GeneratorOutput", false, Klei.AI.Attribute.Display.General, false, 0f, null, null, null, null));
			this.GeneratorOutput.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.None));
			this.MachinerySpeed = base.Add(new Klei.AI.Attribute("MachinerySpeed", false, Klei.AI.Attribute.Display.General, false, 1f, null, null, null, null));
			this.MachinerySpeed.SetFormatter(new PercentAttributeFormatter());
			this.RadiationResistance = base.Add(new Klei.AI.Attribute("RadiationResistance", false, Klei.AI.Attribute.Display.Details, false, 0f, null, null, null, DlcManager.AVAILABLE_EXPANSION1_ONLY));
			this.RadiationResistance.SetFormatter(new PercentAttributeFormatter());
			this.RadiationRecovery = base.Add(new Klei.AI.Attribute("RadiationRecovery", false, Klei.AI.Attribute.Display.Details, false, 0f, null, null, null, DlcManager.AVAILABLE_EXPANSION1_ONLY));
			this.RadiationRecovery.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Radiation, GameUtil.TimeSlice.PerCycle));
			this.DecorExpectation = base.Add(new Klei.AI.Attribute("DecorExpectation", false, Klei.AI.Attribute.Display.Expectation, false, 0f, null, null, null, null));
			this.DecorExpectation.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.FoodExpectation = base.Add(new Klei.AI.Attribute("FoodExpectation", false, Klei.AI.Attribute.Display.Expectation, false, 0f, null, null, null, null));
			this.FoodExpectation.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.RoomTemperaturePreference = base.Add(new Klei.AI.Attribute("RoomTemperaturePreference", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.RoomTemperaturePreference.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Temperature, GameUtil.TimeSlice.None));
			this.QualityOfLifeExpectation = base.Add(new Klei.AI.Attribute("QualityOfLifeExpectation", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.QualityOfLifeExpectation.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.AirConsumptionRate = base.Add(new Klei.AI.Attribute("AirConsumptionRate", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.AirConsumptionRate.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Mass, GameUtil.TimeSlice.PerSecond));
			this.MaxUnderwaterTravelCost = base.Add(new Klei.AI.Attribute("MaxUnderwaterTravelCost", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.ToiletEfficiency = base.Add(new Klei.AI.Attribute("ToiletEfficiency", false, Klei.AI.Attribute.Display.Details, false, 0f, null, null, null, null));
			this.ToiletEfficiency.SetFormatter(new ToPercentAttributeFormatter(1f, GameUtil.TimeSlice.None));
			this.Sneezyness = base.Add(new Klei.AI.Attribute("Sneezyness", false, Klei.AI.Attribute.Display.Details, false, 0f, null, null, null, null));
			this.DiseaseCureSpeed = base.Add(new Klei.AI.Attribute("DiseaseCureSpeed", false, Klei.AI.Attribute.Display.Normal, false, 0f, null, null, null, null));
			this.DiseaseCureSpeed.BaseValue = 1f;
			this.DiseaseCureSpeed.SetFormatter(new ToPercentAttributeFormatter(1f, GameUtil.TimeSlice.None));
			this.DoctoredLevel = base.Add(new Klei.AI.Attribute("DoctoredLevel", false, Klei.AI.Attribute.Display.Never, false, 0f, null, null, null, null));
			this.CarryAmount = base.Add(new Klei.AI.Attribute("CarryAmount", false, Klei.AI.Attribute.Display.Details, false, 0f, null, null, null, null));
			this.CarryAmount.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.Mass, GameUtil.TimeSlice.None));
			this.QualityOfLife = base.Add(new Klei.AI.Attribute("QualityOfLife", false, Klei.AI.Attribute.Display.Details, false, 0f, "ui_icon_qualityoflife", "attribute_qualityoflife", "mod_morale", null));
			this.QualityOfLife.SetFormatter(new QualityOfLifeAttributeFormatter());
			this.GermResistance = base.Add(new Klei.AI.Attribute("GermResistance", false, Klei.AI.Attribute.Display.Details, false, 0f, "ui_icon_immunelevel", "attribute_immunelevel", "mod_germresistance", null));
			this.GermResistance.SetFormatter(new GermResistanceAttributeFormatter());
			this.LifeSupport = base.Add(new Klei.AI.Attribute("LifeSupport", true, Klei.AI.Attribute.Display.Never, false, 0f, null, null, null, null));
			this.LifeSupport.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
			this.Toggle = base.Add(new Klei.AI.Attribute("Toggle", true, Klei.AI.Attribute.Display.Never, false, 0f, null, null, null, null));
			this.Toggle.SetFormatter(new StandardAttributeFormatter(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None));
		}

		// Token: 0x04004637 RID: 17975
		public Klei.AI.Attribute Construction;

		// Token: 0x04004638 RID: 17976
		public Klei.AI.Attribute Digging;

		// Token: 0x04004639 RID: 17977
		public Klei.AI.Attribute Machinery;

		// Token: 0x0400463A RID: 17978
		public Klei.AI.Attribute Athletics;

		// Token: 0x0400463B RID: 17979
		public Klei.AI.Attribute Learning;

		// Token: 0x0400463C RID: 17980
		public Klei.AI.Attribute Cooking;

		// Token: 0x0400463D RID: 17981
		public Klei.AI.Attribute Caring;

		// Token: 0x0400463E RID: 17982
		public Klei.AI.Attribute Strength;

		// Token: 0x0400463F RID: 17983
		public Klei.AI.Attribute Art;

		// Token: 0x04004640 RID: 17984
		public Klei.AI.Attribute Botanist;

		// Token: 0x04004641 RID: 17985
		public Klei.AI.Attribute Ranching;

		// Token: 0x04004642 RID: 17986
		public Klei.AI.Attribute LifeSupport;

		// Token: 0x04004643 RID: 17987
		public Klei.AI.Attribute Toggle;

		// Token: 0x04004644 RID: 17988
		public Klei.AI.Attribute PowerTinker;

		// Token: 0x04004645 RID: 17989
		public Klei.AI.Attribute FarmTinker;

		// Token: 0x04004646 RID: 17990
		public Klei.AI.Attribute SpaceNavigation;

		// Token: 0x04004647 RID: 17991
		public Klei.AI.Attribute Immunity;

		// Token: 0x04004648 RID: 17992
		public Klei.AI.Attribute GermResistance;

		// Token: 0x04004649 RID: 17993
		public Klei.AI.Attribute Insulation;

		// Token: 0x0400464A RID: 17994
		public Klei.AI.Attribute ThermalConductivityBarrier;

		// Token: 0x0400464B RID: 17995
		public Klei.AI.Attribute Decor;

		// Token: 0x0400464C RID: 17996
		public Klei.AI.Attribute FoodQuality;

		// Token: 0x0400464D RID: 17997
		public Klei.AI.Attribute ScaldingThreshold;

		// Token: 0x0400464E RID: 17998
		public Klei.AI.Attribute GeneratorOutput;

		// Token: 0x0400464F RID: 17999
		public Klei.AI.Attribute MachinerySpeed;

		// Token: 0x04004650 RID: 18000
		public Klei.AI.Attribute RadiationResistance;

		// Token: 0x04004651 RID: 18001
		public Klei.AI.Attribute RadiationRecovery;

		// Token: 0x04004652 RID: 18002
		public Klei.AI.Attribute DecorExpectation;

		// Token: 0x04004653 RID: 18003
		public Klei.AI.Attribute FoodExpectation;

		// Token: 0x04004654 RID: 18004
		public Klei.AI.Attribute RoomTemperaturePreference;

		// Token: 0x04004655 RID: 18005
		public Klei.AI.Attribute QualityOfLifeExpectation;

		// Token: 0x04004656 RID: 18006
		public Klei.AI.Attribute AirConsumptionRate;

		// Token: 0x04004657 RID: 18007
		public Klei.AI.Attribute MaxUnderwaterTravelCost;

		// Token: 0x04004658 RID: 18008
		public Klei.AI.Attribute ToiletEfficiency;

		// Token: 0x04004659 RID: 18009
		public Klei.AI.Attribute Sneezyness;

		// Token: 0x0400465A RID: 18010
		public Klei.AI.Attribute DiseaseCureSpeed;

		// Token: 0x0400465B RID: 18011
		public Klei.AI.Attribute DoctoredLevel;

		// Token: 0x0400465C RID: 18012
		public Klei.AI.Attribute CarryAmount;

		// Token: 0x0400465D RID: 18013
		public Klei.AI.Attribute QualityOfLife;
	}
}
