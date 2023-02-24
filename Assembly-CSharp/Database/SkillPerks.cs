using System;
using STRINGS;
using TUNING;

namespace Database
{
	// Token: 0x02000CFE RID: 3326
	public class SkillPerks : ResourceSet<SkillPerk>
	{
		// Token: 0x06006722 RID: 26402 RVA: 0x0027A4B4 File Offset: 0x002786B4
		public SkillPerks(ResourceSet parent)
			: base("SkillPerks", parent)
		{
			this.IncreaseDigSpeedSmall = base.Add(new SkillAttributePerk("IncreaseDigSpeedSmall", Db.Get().Attributes.Digging.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_MINER.NAME));
			this.IncreaseDigSpeedMedium = base.Add(new SkillAttributePerk("IncreaseDigSpeedMedium", Db.Get().Attributes.Digging.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.MINER.NAME));
			this.IncreaseDigSpeedLarge = base.Add(new SkillAttributePerk("IncreaseDigSpeedLarge", Db.Get().Attributes.Digging.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_MINER.NAME));
			this.CanDigVeryFirm = base.Add(new SimpleSkillPerk("CanDigVeryFirm", UI.ROLES_SCREEN.PERKS.CAN_DIG_VERY_FIRM.DESCRIPTION));
			this.CanDigNearlyImpenetrable = base.Add(new SimpleSkillPerk("CanDigAbyssalite", UI.ROLES_SCREEN.PERKS.CAN_DIG_NEARLY_IMPENETRABLE.DESCRIPTION));
			this.CanDigSuperDuperHard = base.Add(new SimpleSkillPerk("CanDigDiamondAndObsidan", UI.ROLES_SCREEN.PERKS.CAN_DIG_SUPER_SUPER_HARD.DESCRIPTION));
			this.CanDigRadioactiveMaterials = base.Add(new SimpleSkillPerk("CanDigCorium", UI.ROLES_SCREEN.PERKS.CAN_DIG_RADIOACTIVE_MATERIALS.DESCRIPTION));
			this.CanDigUnobtanium = base.Add(new SimpleSkillPerk("CanDigUnobtanium", UI.ROLES_SCREEN.PERKS.CAN_DIG_UNOBTANIUM.DESCRIPTION));
			this.IncreaseConstructionSmall = base.Add(new SkillAttributePerk("IncreaseConstructionSmall", Db.Get().Attributes.Construction.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_BUILDER.NAME));
			this.IncreaseConstructionMedium = base.Add(new SkillAttributePerk("IncreaseConstructionMedium", Db.Get().Attributes.Construction.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.BUILDER.NAME));
			this.IncreaseConstructionLarge = base.Add(new SkillAttributePerk("IncreaseConstructionLarge", Db.Get().Attributes.Construction.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_BUILDER.NAME));
			this.IncreaseConstructionMechatronics = base.Add(new SkillAttributePerk("IncreaseConstructionMechatronics", Db.Get().Attributes.Construction.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.NAME));
			this.CanDemolish = base.Add(new SimpleSkillPerk("CanDemonlish", UI.ROLES_SCREEN.PERKS.CAN_DEMOLISH.DESCRIPTION));
			this.IncreaseLearningSmall = base.Add(new SkillAttributePerk("IncreaseLearningSmall", Db.Get().Attributes.Learning.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_RESEARCHER.NAME));
			this.IncreaseLearningMedium = base.Add(new SkillAttributePerk("IncreaseLearningMedium", Db.Get().Attributes.Learning.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.RESEARCHER.NAME));
			this.IncreaseLearningLarge = base.Add(new SkillAttributePerk("IncreaseLearningLarge", Db.Get().Attributes.Learning.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_RESEARCHER.NAME));
			this.IncreaseLearningLargeSpace = base.Add(new SkillAttributePerk("IncreaseLearningLargeSpace", Db.Get().Attributes.Learning.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SPACE_RESEARCHER.NAME));
			this.IncreaseBotanySmall = base.Add(new SkillAttributePerk("IncreaseBotanySmall", Db.Get().Attributes.Botanist.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_FARMER.NAME));
			this.IncreaseBotanyMedium = base.Add(new SkillAttributePerk("IncreaseBotanyMedium", Db.Get().Attributes.Botanist.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.FARMER.NAME));
			this.IncreaseBotanyLarge = base.Add(new SkillAttributePerk("IncreaseBotanyLarge", Db.Get().Attributes.Botanist.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_FARMER.NAME));
			this.CanFarmTinker = base.Add(new SimpleSkillPerk("CanFarmTinker", UI.ROLES_SCREEN.PERKS.CAN_FARM_TINKER.DESCRIPTION));
			this.CanIdentifyMutantSeeds = base.Add(new SimpleSkillPerk("CanIdentifyMutantSeeds", UI.ROLES_SCREEN.PERKS.CAN_IDENTIFY_MUTANT_SEEDS.DESCRIPTION));
			this.IncreaseRanchingSmall = base.Add(new SkillAttributePerk("IncreaseRanchingSmall", Db.Get().Attributes.Ranching.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.RANCHER.NAME));
			this.IncreaseRanchingMedium = base.Add(new SkillAttributePerk("IncreaseRanchingMedium", Db.Get().Attributes.Ranching.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.SENIOR_RANCHER.NAME));
			this.CanWrangleCreatures = base.Add(new SimpleSkillPerk("CanWrangleCreatures", UI.ROLES_SCREEN.PERKS.CAN_WRANGLE_CREATURES.DESCRIPTION));
			this.CanUseRanchStation = base.Add(new SimpleSkillPerk("CanUseRanchStation", UI.ROLES_SCREEN.PERKS.CAN_USE_RANCH_STATION.DESCRIPTION));
			this.IncreaseAthleticsSmall = base.Add(new SkillAttributePerk("IncreaseAthleticsSmall", Db.Get().Attributes.Athletics.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.HAULER.NAME));
			this.IncreaseAthleticsMedium = base.Add(new SkillAttributePerk("IncreaseAthletics", Db.Get().Attributes.Athletics.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.SUIT_EXPERT.NAME));
			this.IncreaseAthleticsLarge = base.Add(new SkillAttributePerk("IncreaseAthleticsLarge", Db.Get().Attributes.Athletics.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.SUIT_DURABILITY.NAME));
			this.IncreaseStrengthGofer = base.Add(new SkillAttributePerk("IncreaseStrengthGofer", Db.Get().Attributes.Strength.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.HAULER.NAME));
			this.IncreaseStrengthCourier = base.Add(new SkillAttributePerk("IncreaseStrengthCourier", Db.Get().Attributes.Strength.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.MATERIALS_MANAGER.NAME));
			this.IncreaseStrengthGroundskeeper = base.Add(new SkillAttributePerk("IncreaseStrengthGroundskeeper", Db.Get().Attributes.Strength.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.HANDYMAN.NAME));
			this.IncreaseStrengthPlumber = base.Add(new SkillAttributePerk("IncreaseStrengthPlumber", Db.Get().Attributes.Strength.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.PLUMBER.NAME));
			this.IncreaseCarryAmountSmall = base.Add(new SkillAttributePerk("IncreaseCarryAmountSmall", Db.Get().Attributes.CarryAmount.Id, 400f, DUPLICANTS.ROLES.HAULER.NAME));
			this.IncreaseCarryAmountMedium = base.Add(new SkillAttributePerk("IncreaseCarryAmountMedium", Db.Get().Attributes.CarryAmount.Id, 800f, DUPLICANTS.ROLES.MATERIALS_MANAGER.NAME));
			this.IncreaseArtSmall = base.Add(new SkillAttributePerk("IncreaseArtSmall", Db.Get().Attributes.Art.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_ARTIST.NAME));
			this.IncreaseArtMedium = base.Add(new SkillAttributePerk("IncreaseArt", Db.Get().Attributes.Art.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.ARTIST.NAME));
			this.IncreaseArtLarge = base.Add(new SkillAttributePerk("IncreaseArtLarge", Db.Get().Attributes.Art.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.MASTER_ARTIST.NAME));
			this.CanArt = base.Add(new SimpleSkillPerk("CanArt", UI.ROLES_SCREEN.PERKS.CAN_ART.DESCRIPTION));
			this.CanArtUgly = base.Add(new SimpleSkillPerk("CanArtUgly", UI.ROLES_SCREEN.PERKS.CAN_ART_UGLY.DESCRIPTION));
			this.CanArtOkay = base.Add(new SimpleSkillPerk("CanArtOkay", UI.ROLES_SCREEN.PERKS.CAN_ART_OKAY.DESCRIPTION));
			this.CanArtGreat = base.Add(new SimpleSkillPerk("CanArtGreat", UI.ROLES_SCREEN.PERKS.CAN_ART_GREAT.DESCRIPTION));
			this.CanStudyArtifact = base.Add(new SimpleSkillPerk("CanStudyArtifact", UI.ROLES_SCREEN.PERKS.CAN_STUDY_ARTIFACTS.DESCRIPTION));
			this.CanClothingAlteration = base.Add(new SimpleSkillPerk("CanClothingAlteration", UI.ROLES_SCREEN.PERKS.CAN_CLOTHING_ALTERATION.DESCRIPTION));
			this.IncreaseMachinerySmall = base.Add(new SkillAttributePerk("IncreaseMachinerySmall", Db.Get().Attributes.Machinery.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.MACHINE_TECHNICIAN.NAME));
			this.IncreaseMachineryMedium = base.Add(new SkillAttributePerk("IncreaseMachineryMedium", Db.Get().Attributes.Machinery.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.POWER_TECHNICIAN.NAME));
			this.IncreaseMachineryLarge = base.Add(new SkillAttributePerk("IncreaseMachineryLarge", Db.Get().Attributes.Machinery.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.NAME));
			this.ConveyorBuild = base.Add(new SimpleSkillPerk("ConveyorBuild", UI.ROLES_SCREEN.PERKS.CONVEYOR_BUILD.DESCRIPTION));
			this.CanPowerTinker = base.Add(new SimpleSkillPerk("CanPowerTinker", UI.ROLES_SCREEN.PERKS.CAN_POWER_TINKER.DESCRIPTION));
			this.CanElectricGrill = base.Add(new SimpleSkillPerk("CanElectricGrill", UI.ROLES_SCREEN.PERKS.CAN_ELECTRIC_GRILL.DESCRIPTION));
			this.IncreaseCookingSmall = base.Add(new SkillAttributePerk("IncreaseCookingSmall", Db.Get().Attributes.Cooking.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_COOK.NAME));
			this.IncreaseCookingMedium = base.Add(new SkillAttributePerk("IncreaseCookingMedium", Db.Get().Attributes.Cooking.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.COOK.NAME));
			this.CanSpiceGrinder = base.Add(new SimpleSkillPerk("CanSpiceGrinder ", UI.ROLES_SCREEN.PERKS.CAN_SPICE_GRINDER.DESCRIPTION));
			this.IncreaseCaringSmall = base.Add(new SkillAttributePerk("IncreaseCaringSmall", Db.Get().Attributes.Caring.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_MEDIC.NAME));
			this.IncreaseCaringMedium = base.Add(new SkillAttributePerk("IncreaseCaringMedium", Db.Get().Attributes.Caring.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.MEDIC.NAME));
			this.IncreaseCaringLarge = base.Add(new SkillAttributePerk("IncreaseCaringLarge", Db.Get().Attributes.Caring.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_MEDIC.NAME));
			this.CanCompound = base.Add(new SimpleSkillPerk("CanCompound", UI.ROLES_SCREEN.PERKS.CAN_COMPOUND.DESCRIPTION));
			this.CanDoctor = base.Add(new SimpleSkillPerk("CanDoctor", UI.ROLES_SCREEN.PERKS.CAN_DOCTOR.DESCRIPTION));
			this.CanAdvancedMedicine = base.Add(new SimpleSkillPerk("CanAdvancedMedicine", UI.ROLES_SCREEN.PERKS.CAN_ADVANCED_MEDICINE.DESCRIPTION));
			this.ExosuitExpertise = base.Add(new SimpleSkillPerk("ExosuitExpertise", UI.ROLES_SCREEN.PERKS.EXOSUIT_EXPERTISE.DESCRIPTION));
			this.ExosuitDurability = base.Add(new SimpleSkillPerk("ExosuitDurability", UI.ROLES_SCREEN.PERKS.EXOSUIT_DURABILITY.DESCRIPTION));
			this.AllowAdvancedResearch = base.Add(new SimpleSkillPerk("AllowAdvancedResearch", UI.ROLES_SCREEN.PERKS.ADVANCED_RESEARCH.DESCRIPTION));
			this.AllowInterstellarResearch = base.Add(new SimpleSkillPerk("AllowInterStellarResearch", UI.ROLES_SCREEN.PERKS.INTERSTELLAR_RESEARCH.DESCRIPTION));
			this.AllowNuclearResearch = base.Add(new SimpleSkillPerk("AllowNuclearResearch", UI.ROLES_SCREEN.PERKS.NUCLEAR_RESEARCH.DESCRIPTION));
			this.AllowOrbitalResearch = base.Add(new SimpleSkillPerk("AllowOrbitalResearch", UI.ROLES_SCREEN.PERKS.ORBITAL_RESEARCH.DESCRIPTION));
			this.AllowGeyserTuning = base.Add(new SimpleSkillPerk("AllowGeyserTuning", UI.ROLES_SCREEN.PERKS.GEYSER_TUNING.DESCRIPTION));
			this.CanStudyWorldObjects = base.Add(new SimpleSkillPerk("CanStudyWorldObjects", UI.ROLES_SCREEN.PERKS.CAN_STUDY_WORLD_OBJECTS.DESCRIPTION));
			this.CanUseClusterTelescope = base.Add(new SimpleSkillPerk("CanUseClusterTelescope", UI.ROLES_SCREEN.PERKS.CAN_USE_CLUSTER_TELESCOPE.DESCRIPTION));
			this.CanDoPlumbing = base.Add(new SimpleSkillPerk("CanDoPlumbing", UI.ROLES_SCREEN.PERKS.CAN_DO_PLUMBING.DESCRIPTION));
			this.CanUseRockets = base.Add(new SimpleSkillPerk("CanUseRockets", UI.ROLES_SCREEN.PERKS.CAN_USE_ROCKETS.DESCRIPTION));
			this.FasterSpaceFlight = base.Add(new SkillAttributePerk("FasterSpaceFlight", Db.Get().Attributes.SpaceNavigation.Id, 0.1f, DUPLICANTS.ROLES.ASTRONAUT.NAME));
			this.CanTrainToBeAstronaut = base.Add(new SimpleSkillPerk("CanTrainToBeAstronaut", UI.ROLES_SCREEN.PERKS.CAN_DO_ASTRONAUT_TRAINING.DESCRIPTION));
			this.CanMissionControl = base.Add(new SimpleSkillPerk("CanMissionControl", UI.ROLES_SCREEN.PERKS.CAN_MISSION_CONTROL.DESCRIPTION));
			this.CanUseRocketControlStation = base.Add(new SimpleSkillPerk("CanUseRocketControlStation", UI.ROLES_SCREEN.PERKS.CAN_PILOT_ROCKET.DESCRIPTION));
			this.IncreaseRocketSpeedSmall = base.Add(new SkillAttributePerk("IncreaseRocketSpeedSmall", Db.Get().Attributes.SpaceNavigation.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.ROCKETPILOT.NAME));
		}

		// Token: 0x04004B37 RID: 19255
		public SkillPerk IncreaseDigSpeedSmall;

		// Token: 0x04004B38 RID: 19256
		public SkillPerk IncreaseDigSpeedMedium;

		// Token: 0x04004B39 RID: 19257
		public SkillPerk IncreaseDigSpeedLarge;

		// Token: 0x04004B3A RID: 19258
		public SkillPerk CanDigVeryFirm;

		// Token: 0x04004B3B RID: 19259
		public SkillPerk CanDigNearlyImpenetrable;

		// Token: 0x04004B3C RID: 19260
		public SkillPerk CanDigSuperDuperHard;

		// Token: 0x04004B3D RID: 19261
		public SkillPerk CanDigRadioactiveMaterials;

		// Token: 0x04004B3E RID: 19262
		public SkillPerk CanDigUnobtanium;

		// Token: 0x04004B3F RID: 19263
		public SkillPerk IncreaseConstructionSmall;

		// Token: 0x04004B40 RID: 19264
		public SkillPerk IncreaseConstructionMedium;

		// Token: 0x04004B41 RID: 19265
		public SkillPerk IncreaseConstructionLarge;

		// Token: 0x04004B42 RID: 19266
		public SkillPerk IncreaseConstructionMechatronics;

		// Token: 0x04004B43 RID: 19267
		public SkillPerk CanDemolish;

		// Token: 0x04004B44 RID: 19268
		public SkillPerk IncreaseLearningSmall;

		// Token: 0x04004B45 RID: 19269
		public SkillPerk IncreaseLearningMedium;

		// Token: 0x04004B46 RID: 19270
		public SkillPerk IncreaseLearningLarge;

		// Token: 0x04004B47 RID: 19271
		public SkillPerk IncreaseLearningLargeSpace;

		// Token: 0x04004B48 RID: 19272
		public SkillPerk IncreaseBotanySmall;

		// Token: 0x04004B49 RID: 19273
		public SkillPerk IncreaseBotanyMedium;

		// Token: 0x04004B4A RID: 19274
		public SkillPerk IncreaseBotanyLarge;

		// Token: 0x04004B4B RID: 19275
		public SkillPerk CanFarmTinker;

		// Token: 0x04004B4C RID: 19276
		public SkillPerk CanIdentifyMutantSeeds;

		// Token: 0x04004B4D RID: 19277
		public SkillPerk CanWrangleCreatures;

		// Token: 0x04004B4E RID: 19278
		public SkillPerk CanUseRanchStation;

		// Token: 0x04004B4F RID: 19279
		public SkillPerk IncreaseRanchingSmall;

		// Token: 0x04004B50 RID: 19280
		public SkillPerk IncreaseRanchingMedium;

		// Token: 0x04004B51 RID: 19281
		public SkillPerk IncreaseAthleticsSmall;

		// Token: 0x04004B52 RID: 19282
		public SkillPerk IncreaseAthleticsMedium;

		// Token: 0x04004B53 RID: 19283
		public SkillPerk IncreaseAthleticsLarge;

		// Token: 0x04004B54 RID: 19284
		public SkillPerk IncreaseStrengthSmall;

		// Token: 0x04004B55 RID: 19285
		public SkillPerk IncreaseStrengthMedium;

		// Token: 0x04004B56 RID: 19286
		public SkillPerk IncreaseStrengthGofer;

		// Token: 0x04004B57 RID: 19287
		public SkillPerk IncreaseStrengthCourier;

		// Token: 0x04004B58 RID: 19288
		public SkillPerk IncreaseStrengthGroundskeeper;

		// Token: 0x04004B59 RID: 19289
		public SkillPerk IncreaseStrengthPlumber;

		// Token: 0x04004B5A RID: 19290
		public SkillPerk IncreaseCarryAmountSmall;

		// Token: 0x04004B5B RID: 19291
		public SkillPerk IncreaseCarryAmountMedium;

		// Token: 0x04004B5C RID: 19292
		public SkillPerk IncreaseArtSmall;

		// Token: 0x04004B5D RID: 19293
		public SkillPerk IncreaseArtMedium;

		// Token: 0x04004B5E RID: 19294
		public SkillPerk IncreaseArtLarge;

		// Token: 0x04004B5F RID: 19295
		public SkillPerk CanArt;

		// Token: 0x04004B60 RID: 19296
		public SkillPerk CanArtUgly;

		// Token: 0x04004B61 RID: 19297
		public SkillPerk CanArtOkay;

		// Token: 0x04004B62 RID: 19298
		public SkillPerk CanArtGreat;

		// Token: 0x04004B63 RID: 19299
		public SkillPerk CanStudyArtifact;

		// Token: 0x04004B64 RID: 19300
		public SkillPerk CanClothingAlteration;

		// Token: 0x04004B65 RID: 19301
		public SkillPerk IncreaseMachinerySmall;

		// Token: 0x04004B66 RID: 19302
		public SkillPerk IncreaseMachineryMedium;

		// Token: 0x04004B67 RID: 19303
		public SkillPerk IncreaseMachineryLarge;

		// Token: 0x04004B68 RID: 19304
		public SkillPerk ConveyorBuild;

		// Token: 0x04004B69 RID: 19305
		public SkillPerk CanPowerTinker;

		// Token: 0x04004B6A RID: 19306
		public SkillPerk CanElectricGrill;

		// Token: 0x04004B6B RID: 19307
		public SkillPerk IncreaseCookingSmall;

		// Token: 0x04004B6C RID: 19308
		public SkillPerk IncreaseCookingMedium;

		// Token: 0x04004B6D RID: 19309
		public SkillPerk CanSpiceGrinder;

		// Token: 0x04004B6E RID: 19310
		public SkillPerk IncreaseCaringSmall;

		// Token: 0x04004B6F RID: 19311
		public SkillPerk IncreaseCaringMedium;

		// Token: 0x04004B70 RID: 19312
		public SkillPerk IncreaseCaringLarge;

		// Token: 0x04004B71 RID: 19313
		public SkillPerk CanCompound;

		// Token: 0x04004B72 RID: 19314
		public SkillPerk CanDoctor;

		// Token: 0x04004B73 RID: 19315
		public SkillPerk CanAdvancedMedicine;

		// Token: 0x04004B74 RID: 19316
		public SkillPerk ExosuitExpertise;

		// Token: 0x04004B75 RID: 19317
		public SkillPerk ExosuitDurability;

		// Token: 0x04004B76 RID: 19318
		public SkillPerk AllowAdvancedResearch;

		// Token: 0x04004B77 RID: 19319
		public SkillPerk AllowInterstellarResearch;

		// Token: 0x04004B78 RID: 19320
		public SkillPerk AllowNuclearResearch;

		// Token: 0x04004B79 RID: 19321
		public SkillPerk AllowOrbitalResearch;

		// Token: 0x04004B7A RID: 19322
		public SkillPerk AllowGeyserTuning;

		// Token: 0x04004B7B RID: 19323
		public SkillPerk CanStudyWorldObjects;

		// Token: 0x04004B7C RID: 19324
		public SkillPerk CanUseClusterTelescope;

		// Token: 0x04004B7D RID: 19325
		public SkillPerk IncreaseRocketSpeedSmall;

		// Token: 0x04004B7E RID: 19326
		public SkillPerk CanMissionControl;

		// Token: 0x04004B7F RID: 19327
		public SkillPerk CanDoPlumbing;

		// Token: 0x04004B80 RID: 19328
		public SkillPerk CanUseRockets;

		// Token: 0x04004B81 RID: 19329
		public SkillPerk FasterSpaceFlight;

		// Token: 0x04004B82 RID: 19330
		public SkillPerk CanTrainToBeAstronaut;

		// Token: 0x04004B83 RID: 19331
		public SkillPerk CanUseRocketControlStation;
	}
}
