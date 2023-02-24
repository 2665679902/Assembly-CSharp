using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;

namespace Database
{
	// Token: 0x02000C7A RID: 3194
	public class AttributeConverters : ResourceSet<AttributeConverter>
	{
		// Token: 0x06006523 RID: 25891 RVA: 0x0025DACC File Offset: 0x0025BCCC
		public AttributeConverter Create(string id, string name, string description, Klei.AI.Attribute attribute, float multiplier, float base_value, IAttributeFormatter formatter, string[] available_dlcs)
		{
			AttributeConverter attributeConverter = new AttributeConverter(id, name, description, multiplier, base_value, attribute, formatter);
			if (DlcManager.IsDlcListValidForCurrentContent(available_dlcs))
			{
				base.Add(attributeConverter);
				attribute.converters.Add(attributeConverter);
			}
			return attributeConverter;
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x0025DB0C File Offset: 0x0025BD0C
		public AttributeConverters()
		{
			ToPercentAttributeFormatter toPercentAttributeFormatter = new ToPercentAttributeFormatter(1f, GameUtil.TimeSlice.None);
			StandardAttributeFormatter standardAttributeFormatter = new StandardAttributeFormatter(GameUtil.UnitClass.Mass, GameUtil.TimeSlice.None);
			this.MovementSpeed = this.Create("MovementSpeed", "Movement Speed", DUPLICANTS.ATTRIBUTES.ATHLETICS.SPEEDMODIFIER, Db.Get().Attributes.Athletics, 0.1f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.ConstructionSpeed = this.Create("ConstructionSpeed", "Construction Speed", DUPLICANTS.ATTRIBUTES.CONSTRUCTION.SPEEDMODIFIER, Db.Get().Attributes.Construction, 0.25f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.DiggingSpeed = this.Create("DiggingSpeed", "Digging Speed", DUPLICANTS.ATTRIBUTES.DIGGING.SPEEDMODIFIER, Db.Get().Attributes.Digging, 0.25f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.MachinerySpeed = this.Create("MachinerySpeed", "Machinery Speed", DUPLICANTS.ATTRIBUTES.MACHINERY.SPEEDMODIFIER, Db.Get().Attributes.Machinery, 0.1f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.HarvestSpeed = this.Create("HarvestSpeed", "Harvest Speed", DUPLICANTS.ATTRIBUTES.BOTANIST.HARVEST_SPEED_MODIFIER, Db.Get().Attributes.Botanist, 0.05f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.PlantTendSpeed = this.Create("PlantTendSpeed", "Plant Tend Speed", DUPLICANTS.ATTRIBUTES.BOTANIST.TINKER_MODIFIER, Db.Get().Attributes.Botanist, 0.025f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.CompoundingSpeed = this.Create("CompoundingSpeed", "Compounding Speed", DUPLICANTS.ATTRIBUTES.CARING.FABRICATE_SPEEDMODIFIER, Db.Get().Attributes.Caring, 0.1f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.ResearchSpeed = this.Create("ResearchSpeed", "Research Speed", DUPLICANTS.ATTRIBUTES.LEARNING.RESEARCHSPEED, Db.Get().Attributes.Learning, 0.4f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.TrainingSpeed = this.Create("TrainingSpeed", "Training Speed", DUPLICANTS.ATTRIBUTES.LEARNING.SPEEDMODIFIER, Db.Get().Attributes.Learning, 0.1f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.CookingSpeed = this.Create("CookingSpeed", "Cooking Speed", DUPLICANTS.ATTRIBUTES.COOKING.SPEEDMODIFIER, Db.Get().Attributes.Cooking, 0.05f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.ArtSpeed = this.Create("ArtSpeed", "Art Speed", DUPLICANTS.ATTRIBUTES.ART.SPEEDMODIFIER, Db.Get().Attributes.Art, 0.1f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.DoctorSpeed = this.Create("DoctorSpeed", "Doctor Speed", DUPLICANTS.ATTRIBUTES.CARING.SPEEDMODIFIER, Db.Get().Attributes.Caring, 0.2f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.TidyingSpeed = this.Create("TidyingSpeed", "Tidying Speed", DUPLICANTS.ATTRIBUTES.STRENGTH.SPEEDMODIFIER, Db.Get().Attributes.Strength, 0.25f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.AttackDamage = this.Create("AttackDamage", "Attack Damage", DUPLICANTS.ATTRIBUTES.DIGGING.ATTACK_MODIFIER, Db.Get().Attributes.Digging, 0.05f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.PilotingSpeed = this.Create("PilotingSpeed", "Piloting Speed", DUPLICANTS.ATTRIBUTES.SPACENAVIGATION.SPEED_MODIFIER, Db.Get().Attributes.SpaceNavigation, 0.025f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_EXPANSION1_ONLY);
			this.ImmuneLevelBoost = this.Create("ImmuneLevelBoost", "Immune Level Boost", DUPLICANTS.ATTRIBUTES.IMMUNITY.BOOST_MODIFIER, Db.Get().Attributes.Immunity, 0.0016666667f, 0f, new ToPercentAttributeFormatter(100f, GameUtil.TimeSlice.PerCycle), DlcManager.AVAILABLE_ALL_VERSIONS);
			this.ToiletSpeed = this.Create("ToiletSpeed", "Toilet Speed", "", Db.Get().Attributes.ToiletEfficiency, 1f, -1f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.CarryAmountFromStrength = this.Create("CarryAmountFromStrength", "Carry Amount", DUPLICANTS.ATTRIBUTES.STRENGTH.CARRYMODIFIER, Db.Get().Attributes.Strength, 40f, 0f, standardAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.TemperatureInsulation = this.Create("TemperatureInsulation", "Temperature Insulation", DUPLICANTS.ATTRIBUTES.INSULATION.SPEEDMODIFIER, Db.Get().Attributes.Insulation, 0.1f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.SeedHarvestChance = this.Create("SeedHarvestChance", "Seed Harvest Chance", DUPLICANTS.ATTRIBUTES.BOTANIST.BONUS_SEEDS, Db.Get().Attributes.Botanist, 0.033f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.CapturableSpeed = this.Create("CapturableSpeed", "Capturable Speed", DUPLICANTS.ATTRIBUTES.RANCHING.CAPTURABLESPEED, Db.Get().Attributes.Ranching, 0.05f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.GeotuningSpeed = this.Create("GeotuningSpeed", "Geotuning Speed", DUPLICANTS.ATTRIBUTES.LEARNING.GEOTUNER_SPEED_MODIFIER, Db.Get().Attributes.Learning, 0.05f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.RanchingEffectDuration = this.Create("RanchingEffectDuration", "Ranching Effect Duration", DUPLICANTS.ATTRIBUTES.RANCHING.EFFECTMODIFIER, Db.Get().Attributes.Ranching, 0.1f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.FarmedEffectDuration = this.Create("FarmedEffectDuration", "Farmer's Touch Duration", DUPLICANTS.ATTRIBUTES.BOTANIST.TINKER_EFFECT_MODIFIER, Db.Get().Attributes.Botanist, 0.1f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
			this.PowerTinkerEffectDuration = this.Create("PowerTinkerEffectDuration", "Engie's Tune-Up Effect Duration", DUPLICANTS.ATTRIBUTES.MACHINERY.TINKER_EFFECT_MODIFIER, Db.Get().Attributes.Machinery, 0.025f, 0f, toPercentAttributeFormatter, DlcManager.AVAILABLE_ALL_VERSIONS);
		}

		// Token: 0x06006525 RID: 25893 RVA: 0x0025E160 File Offset: 0x0025C360
		public List<AttributeConverter> GetConvertersForAttribute(Klei.AI.Attribute attrib)
		{
			List<AttributeConverter> list = new List<AttributeConverter>();
			foreach (AttributeConverter attributeConverter in this.resources)
			{
				if (attributeConverter.attribute == attrib)
				{
					list.Add(attributeConverter);
				}
			}
			return list;
		}

		// Token: 0x0400461E RID: 17950
		public AttributeConverter MovementSpeed;

		// Token: 0x0400461F RID: 17951
		public AttributeConverter ConstructionSpeed;

		// Token: 0x04004620 RID: 17952
		public AttributeConverter DiggingSpeed;

		// Token: 0x04004621 RID: 17953
		public AttributeConverter MachinerySpeed;

		// Token: 0x04004622 RID: 17954
		public AttributeConverter HarvestSpeed;

		// Token: 0x04004623 RID: 17955
		public AttributeConverter PlantTendSpeed;

		// Token: 0x04004624 RID: 17956
		public AttributeConverter CompoundingSpeed;

		// Token: 0x04004625 RID: 17957
		public AttributeConverter ResearchSpeed;

		// Token: 0x04004626 RID: 17958
		public AttributeConverter TrainingSpeed;

		// Token: 0x04004627 RID: 17959
		public AttributeConverter CookingSpeed;

		// Token: 0x04004628 RID: 17960
		public AttributeConverter ArtSpeed;

		// Token: 0x04004629 RID: 17961
		public AttributeConverter DoctorSpeed;

		// Token: 0x0400462A RID: 17962
		public AttributeConverter TidyingSpeed;

		// Token: 0x0400462B RID: 17963
		public AttributeConverter AttackDamage;

		// Token: 0x0400462C RID: 17964
		public AttributeConverter PilotingSpeed;

		// Token: 0x0400462D RID: 17965
		public AttributeConverter ImmuneLevelBoost;

		// Token: 0x0400462E RID: 17966
		public AttributeConverter ToiletSpeed;

		// Token: 0x0400462F RID: 17967
		public AttributeConverter CarryAmountFromStrength;

		// Token: 0x04004630 RID: 17968
		public AttributeConverter TemperatureInsulation;

		// Token: 0x04004631 RID: 17969
		public AttributeConverter SeedHarvestChance;

		// Token: 0x04004632 RID: 17970
		public AttributeConverter RanchingEffectDuration;

		// Token: 0x04004633 RID: 17971
		public AttributeConverter FarmedEffectDuration;

		// Token: 0x04004634 RID: 17972
		public AttributeConverter PowerTinkerEffectDuration;

		// Token: 0x04004635 RID: 17973
		public AttributeConverter CapturableSpeed;

		// Token: 0x04004636 RID: 17974
		public AttributeConverter GeotuningSpeed;
	}
}
