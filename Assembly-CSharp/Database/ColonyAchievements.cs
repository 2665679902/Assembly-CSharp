﻿using System;
using System.Collections.Generic;
using FMODUnity;
using STRINGS;
using TUNING;

namespace Database
{
	// Token: 0x02000CF9 RID: 3321
	public class ColonyAchievements : ResourceSet<ColonyAchievement>
	{
		// Token: 0x06006710 RID: 26384 RVA: 0x002788D0 File Offset: 0x00276AD0
		public ColonyAchievements(ResourceSet parent)
			: base("ColonyAchievements", parent)
		{
			this.Thriving = base.Add(new ColonyAchievement("Thriving", "WINCONDITION_STAY", COLONY_ACHIEVEMENTS.THRIVING.NAME, COLONY_ACHIEVEMENTS.THRIVING.DESCRIPTION, true, new List<ColonyAchievementRequirement>
			{
				new CycleNumber(200),
				new MinimumMorale(16),
				new NumberOfDupes(12),
				new MonumentBuilt()
			}, COLONY_ACHIEVEMENTS.THRIVING.MESSAGE_TITLE, COLONY_ACHIEVEMENTS.THRIVING.MESSAGE_BODY, "victoryShorts/Stay", "victoryLoops/Stay_loop", new Action<KMonoBehaviour>(ThrivingSequence.Start), AudioMixerSnapshots.Get().VictoryNISGenericSnapshot, "home_sweet_home", null));
			this.ReachedDistantPlanet = (DlcManager.IsExpansion1Active() ? base.Add(new ColonyAchievement("ReachedDistantPlanet", "WINCONDITION_LEAVE", COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.NAME, COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.DESCRIPTION, true, new List<ColonyAchievementRequirement>
			{
				new EstablishColonies(),
				new OpenTemporalTear(),
				new SentCraftIntoTemporalTear()
			}, COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.MESSAGE_TITLE_DLC1, COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.MESSAGE_BODY_DLC1, "victoryShorts/Leave", "victoryLoops/Leave_loop", new Action<KMonoBehaviour>(EnterTemporalTearSequence.Start), AudioMixerSnapshots.Get().VictoryNISRocketSnapshot, "rocket", null)) : base.Add(new ColonyAchievement("ReachedDistantPlanet", "WINCONDITION_LEAVE", COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.NAME, COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.DESCRIPTION, true, new List<ColonyAchievementRequirement>
			{
				new ReachedSpace(Db.Get().SpaceDestinationTypes.Wormhole)
			}, COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.MESSAGE_TITLE, COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.MESSAGE_BODY, "victoryShorts/Leave", "victoryLoops/Leave_loop", new Action<KMonoBehaviour>(ReachedDistantPlanetSequence.Start), AudioMixerSnapshots.Get().VictoryNISRocketSnapshot, "rocket", null)));
			if (DlcManager.IsExpansion1Active())
			{
				this.CollectedArtifacts = new ColonyAchievement("CollectedArtifacts", "WINCONDITION_ARTIFACTS", COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.NAME, COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.DESCRIPTION, true, new List<ColonyAchievementRequirement>
				{
					new CollectedArtifacts(),
					new CollectedSpaceArtifacts()
				}, COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.MESSAGE_TITLE, COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.MESSAGE_BODY, "victoryShorts/Artifact", "victoryLoops/Artifact_loop", new Action<KMonoBehaviour>(ArtifactSequence.Start), AudioMixerSnapshots.Get().VictoryNISGenericSnapshot, "cosmic_archaeology", DlcManager.AVAILABLE_EXPANSION1_ONLY);
				base.Add(this.CollectedArtifacts);
			}
			this.Survived100Cycles = base.Add(new ColonyAchievement("Survived100Cycles", "SURVIVE_HUNDRED_CYCLES", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SURVIVE_HUNDRED_CYCLES, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SURVIVE_HUNDRED_CYCLES_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new CycleNumber(100)
			}, "", "", "", "", null, default(EventReference), "Turn_of_the_Century", null));
			this.ReachedSpace = (DlcManager.IsExpansion1Active() ? base.Add(new ColonyAchievement("ReachedSpace", "REACH_SPACE_ANY_DESTINATION", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.REACH_SPACE_ANY_DESTINATION, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.REACH_SPACE_ANY_DESTINATION_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new LaunchedCraft()
			}, "", "", "", "", null, default(EventReference), "space_race", null)) : base.Add(new ColonyAchievement("ReachedSpace", "REACH_SPACE_ANY_DESTINATION", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.REACH_SPACE_ANY_DESTINATION, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.REACH_SPACE_ANY_DESTINATION_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new ReachedSpace(null)
			}, "", "", "", "", null, default(EventReference), "space_race", null)));
			this.CompleteSkillBranch = base.Add(new ColonyAchievement("CompleteSkillBranch", "COMPLETED_SKILL_BRANCH", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.COMPLETED_SKILL_BRANCH, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.COMPLETED_SKILL_BRANCH_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new SkillBranchComplete(Db.Get().Skills.GetTerminalSkills())
			}, "", "", "", "", null, default(EventReference), "CompleteSkillBranch", null));
			this.CompleteResearchTree = base.Add(new ColonyAchievement("CompleteResearchTree", "COMPLETED_RESEARCH", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.COMPLETED_RESEARCH, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.COMPLETED_RESEARCH_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new ResearchComplete()
			}, "", "", "", "", null, default(EventReference), "honorary_doctorate", null));
			this.Clothe8Dupes = base.Add(new ColonyAchievement("Clothe8Dupes", "EQUIP_EIGHT_DUPES", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.EQUIP_N_DUPES, string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.EQUIP_N_DUPES_DESCRIPTION, 8), false, new List<ColonyAchievementRequirement>
			{
				new EquipNDupes(Db.Get().AssignableSlots.Outfit, 8)
			}, "", "", "", "", null, default(EventReference), "and_nowhere_to_go", null));
			this.TameAllBasicCritters = base.Add(new ColonyAchievement("TameAllBasicCritters", "TAME_BASIC_CRITTERS", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.TAME_BASIC_CRITTERS, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.TAME_BASIC_CRITTERS_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new CritterTypesWithTraits(new List<Tag> { "Drecko", "Hatch", "LightBug", "Mole", "Oilfloater", "Pacu", "Puft", "Moo", "Crab", "Squirrel" })
			}, "", "", "", "", null, default(EventReference), "Animal_friends", null));
			this.Build4NatureReserves = base.Add(new ColonyAchievement("Build4NatureReserves", "BUILD_NATURE_RESERVES", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BUILD_NATURE_RESERVES, string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BUILD_NATURE_RESERVES_DESCRIPTION, Db.Get().RoomTypes.NatureReserve.Name, 4), false, new List<ColonyAchievementRequirement>
			{
				new BuildNRoomTypes(Db.Get().RoomTypes.NatureReserve, 4)
			}, "", "", "", "", null, default(EventReference), "Some_Reservations", null));
			this.Minimum20LivingDupes = base.Add(new ColonyAchievement("Minimum20LivingDupes", "TWENTY_DUPES", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.TWENTY_DUPES, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.TWENTY_DUPES_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new NumberOfDupes(20)
			}, "", "", "", "", null, default(EventReference), "no_place_like_clone", null));
			this.TameAGassyMoo = base.Add(new ColonyAchievement("TameAGassyMoo", "TAME_GASSYMOO", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.TAME_GASSYMOO, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.TAME_GASSYMOO_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new CritterTypesWithTraits(new List<Tag> { "Moo" })
			}, "", "", "", "", null, default(EventReference), "moovin_on_up", null));
			this.CoolBuildingTo6K = base.Add(new ColonyAchievement("CoolBuildingTo6K", "SIXKELVIN_BUILDING", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SIXKELVIN_BUILDING, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SIXKELVIN_BUILDING_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new CoolBuildingToXKelvin(6)
			}, "", "", "", "", null, default(EventReference), "not_0k", null));
			this.EatkCalFromMeatByCycle100 = base.Add(new ColonyAchievement("EatkCalFromMeatByCycle100", "EAT_MEAT", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.EAT_MEAT, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.EAT_MEAT_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new BeforeCycleNumber(100),
				new EatXCaloriesFromY(400000, new List<string>
				{
					FOOD.FOOD_TYPES.MEAT.Id,
					FOOD.FOOD_TYPES.FISH_MEAT.Id,
					FOOD.FOOD_TYPES.COOKED_MEAT.Id,
					FOOD.FOOD_TYPES.COOKED_FISH.Id,
					FOOD.FOOD_TYPES.SURF_AND_TURF.Id,
					FOOD.FOOD_TYPES.BURGER.Id
				})
			}, "", "", "", "", null, default(EventReference), "Carnivore", null));
			this.NoFarmTilesAndKCal = base.Add(new ColonyAchievement("NoFarmTilesAndKCal", "NO_PLANTERBOX", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.NO_PLANTERBOX, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.NO_PLANTERBOX_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new NoFarmables(),
				new EatXCalories(400000)
			}, "", "", "", "", null, default(EventReference), "Locavore", null));
			this.Generate240000kJClean = base.Add(new ColonyAchievement("Generate240000kJClean", "CLEAN_ENERGY", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.CLEAN_ENERGY, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.CLEAN_ENERGY_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new ProduceXEngeryWithoutUsingYList(240000f, new List<Tag> { "MethaneGenerator", "PetroleumGenerator", "WoodGasGenerator", "Generator" })
			}, "", "", "", "", null, default(EventReference), "sustainably_sustaining", null));
			this.BuildOutsideStartBiome = base.Add(new ColonyAchievement("BuildOutsideStartBiome", "BUILD_OUTSIDE_BIOME", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BUILD_OUTSIDE_BIOME, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BUILD_OUTSIDE_BIOME_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new BuildOutsideStartBiome()
			}, "", "", "", "", null, default(EventReference), "build_outside", null));
			this.Travel10000InTubes = base.Add(new ColonyAchievement("Travel10000InTubes", "TUBE_TRAVEL_DISTANCE", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.TUBE_TRAVEL_DISTANCE, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.TUBE_TRAVEL_DISTANCE_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new TravelXUsingTransitTubes(NavType.Tube, 10000)
			}, "", "", "", "", null, default(EventReference), "Totally-Tubular", null));
			this.VarietyOfRooms = base.Add(new ColonyAchievement("VarietyOfRooms", "VARIETY_OF_ROOMS", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.VARIETY_OF_ROOMS, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.VARIETY_OF_ROOMS_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new BuildRoomType(Db.Get().RoomTypes.NatureReserve),
				new BuildRoomType(Db.Get().RoomTypes.Hospital),
				new BuildRoomType(Db.Get().RoomTypes.RecRoom),
				new BuildRoomType(Db.Get().RoomTypes.GreatHall),
				new BuildRoomType(Db.Get().RoomTypes.Bedroom),
				new BuildRoomType(Db.Get().RoomTypes.PlumbedBathroom),
				new BuildRoomType(Db.Get().RoomTypes.Farm),
				new BuildRoomType(Db.Get().RoomTypes.CreaturePen)
			}, "", "", "", "", null, default(EventReference), "Get-a-Room", null));
			this.SurviveOneYear = base.Add(new ColonyAchievement("SurviveOneYear", "SURVIVE_ONE_YEAR", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SURVIVE_ONE_YEAR, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SURVIVE_ONE_YEAR_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new FractionalCycleNumber(365.25f)
			}, "", "", "", "", null, default(EventReference), "One_year", null));
			this.ExploreOilBiome = base.Add(new ColonyAchievement("ExploreOilBiome", "EXPLORE_OIL_BIOME", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.EXPLORE_OIL_BIOME, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.EXPLORE_OIL_BIOME_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new ExploreOilFieldSubZone()
			}, "", "", "", "", null, default(EventReference), "enter_oil_biome", null));
			this.EatCookedFood = base.Add(new ColonyAchievement("EatCookedFood", "COOKED_FOOD", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.COOKED_FOOD, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.COOKED_FOOD_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new EatXKCalProducedByY(1, new List<Tag> { "GourmetCookingStation", "CookingStation" })
			}, "", "", "", "", null, default(EventReference), "its_not_raw", null));
			this.BasicPumping = base.Add(new ColonyAchievement("BasicPumping", "BASIC_PUMPING", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BASIC_PUMPING, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BASIC_PUMPING_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new VentXKG(SimHashes.Oxygen, 1000f)
			}, "", "", "", "", null, default(EventReference), "BasicPumping", null));
			this.BasicComforts = base.Add(new ColonyAchievement("BasicComforts", "BASIC_COMFORTS", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BASIC_COMFORTS, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BASIC_COMFORTS_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new AtLeastOneBuildingForEachDupe(new List<Tag> { "FlushToilet", "Outhouse" }),
				new AtLeastOneBuildingForEachDupe(new List<Tag> { "Bed", "LuxuryBed" })
			}, "", "", "", "", null, default(EventReference), "1bed_1toilet", null));
			this.PlumbedWashrooms = base.Add(new ColonyAchievement("PlumbedWashrooms", "PLUMBED_WASHROOMS", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.PLUMBED_WASHROOMS, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.PLUMBED_WASHROOMS_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new UpgradeAllBasicBuildings("Outhouse", "FlushToilet"),
				new UpgradeAllBasicBuildings("WashBasin", "WashSink")
			}, "", "", "", "", null, default(EventReference), "royal_flush", null));
			this.AutomateABuilding = base.Add(new ColonyAchievement("AutomateABuilding", "AUTOMATE_A_BUILDING", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.AUTOMATE_A_BUILDING, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.AUTOMATE_A_BUILDING_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new AutomateABuilding()
			}, "", "", "", "", null, default(EventReference), "red_light_green_light", null));
			this.MasterpiecePainting = base.Add(new ColonyAchievement("MasterpiecePainting", "MASTERPIECE_PAINTING", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.MASTERPIECE_PAINTING, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.MASTERPIECE_PAINTING_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new CreateMasterPainting()
			}, "", "", "", "", null, default(EventReference), "art_underground", null));
			this.InspectPOI = base.Add(new ColonyAchievement("InspectPOI", "INSPECT_POI", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.INSPECT_POI, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.INSPECT_POI_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new ActivateLorePOI()
			}, "", "", "", "", null, default(EventReference), "ghosts_of_gravitas", null));
			this.HatchACritter = base.Add(new ColonyAchievement("HatchACritter", "HATCH_A_CRITTER", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.HATCH_A_CRITTER, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.HATCH_A_CRITTER_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new CritterTypeExists(new List<Tag>
				{
					"DreckoPlasticBaby", "HatchHardBaby", "HatchMetalBaby", "HatchVeggieBaby", "LightBugBlackBaby", "LightBugBlueBaby", "LightBugCrystalBaby", "LightBugOrangeBaby", "LightBugPinkBaby", "LightBugPurpleBaby",
					"OilfloaterDecorBaby", "OilfloaterHighTempBaby", "PacuCleanerBaby", "PacuTropicalBaby", "PuftBleachstoneBaby", "PuftOxyliteBaby", "SquirrelHugBaby", "CrabWoodBaby", "CrabFreshWaterBaby", "MoleDelicacyBaby"
				})
			}, "", "", "", "", null, default(EventReference), "good_egg", null));
			this.CuredDisease = base.Add(new ColonyAchievement("CuredDisease", "CURED_DISEASE", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.CURED_DISEASE, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.CURED_DISEASE_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new CureDisease()
			}, "", "", "", "", null, default(EventReference), "medic", null));
			this.GeneratorTuneup = base.Add(new ColonyAchievement("GeneratorTuneup", "GENERATOR_TUNEUP", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.GENERATOR_TUNEUP, string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.GENERATOR_TUNEUP_DESCRIPTION, 100), false, new List<ColonyAchievementRequirement>
			{
				new TuneUpGenerator(100f)
			}, "", "", "", "", null, default(EventReference), "tune_up_for_what", null));
			this.ClearFOW = base.Add(new ColonyAchievement("ClearFOW", "CLEAR_FOW", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.CLEAR_FOW, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.CLEAR_FOW_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new RevealAsteriod(0.8f)
			}, "", "", "", "", null, default(EventReference), "pulling_back_the_veil", null));
			this.HatchRefinement = base.Add(new ColonyAchievement("HatchRefinement", "HATCH_REFINEMENT", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.HATCH_REFINEMENT, string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.HATCH_REFINEMENT_DESCRIPTION, GameUtil.GetFormattedMass(10000f, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}")), false, new List<ColonyAchievementRequirement>
			{
				new CreaturePoopKGProduction("HatchMetal", 10000f)
			}, "", "", "", "", null, default(EventReference), "down_the_hatch", null));
			this.BunkerDoorDefense = base.Add(new ColonyAchievement("BunkerDoorDefense", "BUNKER_DOOR_DEFENSE", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BUNKER_DOOR_DEFENSE, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.BUNKER_DOOR_DEFENSE_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new BlockedCometWithBunkerDoor()
			}, "", "", "", "", null, default(EventReference), "Immovable_Object", null));
			this.IdleDuplicants = base.Add(new ColonyAchievement("IdleDuplicants", "IDLE_DUPLICANTS", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.IDLE_DUPLICANTS, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.IDLE_DUPLICANTS_DESCRIPTION, false, new List<ColonyAchievementRequirement>
			{
				new DupesVsSolidTransferArmFetch(0.51f, 5)
			}, "", "", "", "", null, default(EventReference), "easy_livin", null));
			this.ExosuitCycles = base.Add(new ColonyAchievement("ExosuitCycles", "EXOSUIT_CYCLES", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.EXOSUIT_CYCLES, string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.EXOSUIT_CYCLES_DESCRIPTION, 10), false, new List<ColonyAchievementRequirement>
			{
				new DupesCompleteChoreInExoSuitForCycles(10)
			}, "", "", "", "", null, default(EventReference), "job_suitability", null));
			if (DlcManager.IsExpansion1Active())
			{
				this.FirstTeleport = base.Add(new ColonyAchievement("FirstTeleport", "FIRST_TELEPORT", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.FIRST_TELEPORT, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.FIRST_TELEPORT_DESCRIPTION, false, new List<ColonyAchievementRequirement>
				{
					new TeleportDuplicant(),
					new DefrostDuplicant()
				}, "", "", "", "", null, default(EventReference), "first_teleport_of_call", DlcManager.AVAILABLE_EXPANSION1_ONLY));
				this.SoftLaunch = base.Add(new ColonyAchievement("SoftLaunch", "SOFT_LAUNCH", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SOFT_LAUNCH, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SOFT_LAUNCH_DESCRIPTION, false, new List<ColonyAchievementRequirement>
				{
					new BuildALaunchPad()
				}, "", "", "", "", null, default(EventReference), "soft_launch", DlcManager.AVAILABLE_EXPANSION1_ONLY));
				this.GMOOK = base.Add(new ColonyAchievement("GMOOK", "GMO_OK", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.GMO_OK, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.GMO_OK_DESCRIPTION, false, new List<ColonyAchievementRequirement>
				{
					new AnalyzeSeed(BasicFabricMaterialPlantConfig.ID),
					new AnalyzeSeed("BasicSingleHarvestPlant"),
					new AnalyzeSeed("GasGrass"),
					new AnalyzeSeed("MushroomPlant"),
					new AnalyzeSeed("PrickleFlower"),
					new AnalyzeSeed("SaltPlant"),
					new AnalyzeSeed(SeaLettuceConfig.ID),
					new AnalyzeSeed("SpiceVine"),
					new AnalyzeSeed("SwampHarvestPlant"),
					new AnalyzeSeed(SwampLilyConfig.ID),
					new AnalyzeSeed("WormPlant"),
					new AnalyzeSeed("ColdWheat"),
					new AnalyzeSeed("BeanPlant")
				}, "", "", "", "", null, default(EventReference), "gmo_ok", DlcManager.AVAILABLE_EXPANSION1_ONLY));
				this.MineTheGap = base.Add(new ColonyAchievement("MineTheGap", "MINE_THE_GAP", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.MINE_THE_GAP, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.MINE_THE_GAP_DESCRIPTION, false, new List<ColonyAchievementRequirement>
				{
					new HarvestAmountFromSpacePOI(1000000f)
				}, "", "", "", "", null, default(EventReference), "mine_the_gap", DlcManager.AVAILABLE_EXPANSION1_ONLY));
				this.LandedOnAllWorlds = base.Add(new ColonyAchievement("LandedOnAllWorlds", "LANDED_ON_ALL_WORLDS", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.LAND_ON_ALL_WORLDS, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.LAND_ON_ALL_WORLDS_DESCRIPTION, false, new List<ColonyAchievementRequirement>
				{
					new LandOnAllWorlds()
				}, "", "", "", "", null, default(EventReference), "land_on_all_worlds", DlcManager.AVAILABLE_EXPANSION1_ONLY));
				this.RadicalTrip = base.Add(new ColonyAchievement("RadicalTrip", "RADICAL_TRIP", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.RADICAL_TRIP, string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.RADICAL_TRIP_DESCRIPTION, 10), false, new List<ColonyAchievementRequirement>
				{
					new RadBoltTravelDistance(10000)
				}, "", "", "", "", null, default(EventReference), "radical_trip", DlcManager.AVAILABLE_EXPANSION1_ONLY));
				this.SweeterThanHoney = base.Add(new ColonyAchievement("SweeterThanHoney", "SWEETER_THAN_HONEY", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SWEETER_THAN_HONEY, COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SWEETER_THAN_HONEY_DESCRIPTION, false, new List<ColonyAchievementRequirement>
				{
					new HarvestAHiveWithoutBeingStung()
				}, "", "", "", "", null, default(EventReference), "sweeter_than_honey", DlcManager.AVAILABLE_EXPANSION1_ONLY));
				this.SurviveInARocket = base.Add(new ColonyAchievement("SurviveInARocket", "SURVIVE_IN_A_ROCKET", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SURVIVE_IN_A_ROCKET, string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.SURVIVE_IN_A_ROCKET_DESCRIPTION, 10, 25), false, new List<ColonyAchievementRequirement>
				{
					new SurviveARocketWithMinimumMorale(25f, 10)
				}, "", "", "", "", null, default(EventReference), "survive_a_rocket", DlcManager.AVAILABLE_EXPANSION1_ONLY));
				this.RunAReactor = base.Add(new ColonyAchievement("RunAReactor", "REACTOR_USAGE", COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.REACTOR_USAGE, string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.REACTOR_USAGE_DESCRIPTION, 5), false, new List<ColonyAchievementRequirement>
				{
					new RunReactorForXDays(5)
				}, "", "", "", "", null, default(EventReference), "thats_rad", DlcManager.AVAILABLE_EXPANSION1_ONLY));
			}
		}

		// Token: 0x04004AF9 RID: 19193
		public ColonyAchievement Thriving;

		// Token: 0x04004AFA RID: 19194
		public ColonyAchievement ReachedDistantPlanet;

		// Token: 0x04004AFB RID: 19195
		public ColonyAchievement CollectedArtifacts;

		// Token: 0x04004AFC RID: 19196
		public ColonyAchievement Survived100Cycles;

		// Token: 0x04004AFD RID: 19197
		public ColonyAchievement ReachedSpace;

		// Token: 0x04004AFE RID: 19198
		public ColonyAchievement CompleteSkillBranch;

		// Token: 0x04004AFF RID: 19199
		public ColonyAchievement CompleteResearchTree;

		// Token: 0x04004B00 RID: 19200
		public ColonyAchievement Clothe8Dupes;

		// Token: 0x04004B01 RID: 19201
		public ColonyAchievement Build4NatureReserves;

		// Token: 0x04004B02 RID: 19202
		public ColonyAchievement Minimum20LivingDupes;

		// Token: 0x04004B03 RID: 19203
		public ColonyAchievement TameAGassyMoo;

		// Token: 0x04004B04 RID: 19204
		public ColonyAchievement CoolBuildingTo6K;

		// Token: 0x04004B05 RID: 19205
		public ColonyAchievement EatkCalFromMeatByCycle100;

		// Token: 0x04004B06 RID: 19206
		public ColonyAchievement NoFarmTilesAndKCal;

		// Token: 0x04004B07 RID: 19207
		public ColonyAchievement Generate240000kJClean;

		// Token: 0x04004B08 RID: 19208
		public ColonyAchievement BuildOutsideStartBiome;

		// Token: 0x04004B09 RID: 19209
		public ColonyAchievement Travel10000InTubes;

		// Token: 0x04004B0A RID: 19210
		public ColonyAchievement VarietyOfRooms;

		// Token: 0x04004B0B RID: 19211
		public ColonyAchievement TameAllBasicCritters;

		// Token: 0x04004B0C RID: 19212
		public ColonyAchievement SurviveOneYear;

		// Token: 0x04004B0D RID: 19213
		public ColonyAchievement ExploreOilBiome;

		// Token: 0x04004B0E RID: 19214
		public ColonyAchievement EatCookedFood;

		// Token: 0x04004B0F RID: 19215
		public ColonyAchievement BasicPumping;

		// Token: 0x04004B10 RID: 19216
		public ColonyAchievement BasicComforts;

		// Token: 0x04004B11 RID: 19217
		public ColonyAchievement PlumbedWashrooms;

		// Token: 0x04004B12 RID: 19218
		public ColonyAchievement AutomateABuilding;

		// Token: 0x04004B13 RID: 19219
		public ColonyAchievement MasterpiecePainting;

		// Token: 0x04004B14 RID: 19220
		public ColonyAchievement InspectPOI;

		// Token: 0x04004B15 RID: 19221
		public ColonyAchievement HatchACritter;

		// Token: 0x04004B16 RID: 19222
		public ColonyAchievement CuredDisease;

		// Token: 0x04004B17 RID: 19223
		public ColonyAchievement GeneratorTuneup;

		// Token: 0x04004B18 RID: 19224
		public ColonyAchievement ClearFOW;

		// Token: 0x04004B19 RID: 19225
		public ColonyAchievement HatchRefinement;

		// Token: 0x04004B1A RID: 19226
		public ColonyAchievement BunkerDoorDefense;

		// Token: 0x04004B1B RID: 19227
		public ColonyAchievement IdleDuplicants;

		// Token: 0x04004B1C RID: 19228
		public ColonyAchievement ExosuitCycles;

		// Token: 0x04004B1D RID: 19229
		public ColonyAchievement FirstTeleport;

		// Token: 0x04004B1E RID: 19230
		public ColonyAchievement SoftLaunch;

		// Token: 0x04004B1F RID: 19231
		public ColonyAchievement GMOOK;

		// Token: 0x04004B20 RID: 19232
		public ColonyAchievement MineTheGap;

		// Token: 0x04004B21 RID: 19233
		public ColonyAchievement LandedOnAllWorlds;

		// Token: 0x04004B22 RID: 19234
		public ColonyAchievement RadicalTrip;

		// Token: 0x04004B23 RID: 19235
		public ColonyAchievement SweeterThanHoney;

		// Token: 0x04004B24 RID: 19236
		public ColonyAchievement SurviveInARocket;

		// Token: 0x04004B25 RID: 19237
		public ColonyAchievement RunAReactor;
	}
}
