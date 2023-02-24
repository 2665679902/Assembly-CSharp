using System;
using System.Collections.Generic;

namespace TUNING
{
	// Token: 0x02000D1A RID: 3354
	public class BUILDINGS
	{
		// Token: 0x04004C4E RID: 19534
		public const float DEFAULT_STORAGE_CAPACITY = 2000f;

		// Token: 0x04004C4F RID: 19535
		public const float STANDARD_MANUAL_REFILL_LEVEL = 0.2f;

		// Token: 0x04004C50 RID: 19536
		public const float MASS_TEMPERATURE_SCALE = 0.2f;

		// Token: 0x04004C51 RID: 19537
		public const float AIRCONDITIONER_TEMPDELTA = -14f;

		// Token: 0x04004C52 RID: 19538
		public const float MAX_ENVIRONMENT_DELTA = -50f;

		// Token: 0x04004C53 RID: 19539
		public const float COMPOST_FLIP_TIME = 20f;

		// Token: 0x04004C54 RID: 19540
		public const int TUBE_LAUNCHER_MAX_CHARGES = 3;

		// Token: 0x04004C55 RID: 19541
		public const float TUBE_LAUNCHER_RECHARGE_TIME = 10f;

		// Token: 0x04004C56 RID: 19542
		public const float TUBE_LAUNCHER_WORK_TIME = 1f;

		// Token: 0x04004C57 RID: 19543
		public const float SMELTER_INGOT_INPUTKG = 500f;

		// Token: 0x04004C58 RID: 19544
		public const float SMELTER_INGOT_OUTPUTKG = 100f;

		// Token: 0x04004C59 RID: 19545
		public const float SMELTER_FABRICATIONTIME = 120f;

		// Token: 0x04004C5A RID: 19546
		public const float GEOREFINERY_SLAB_INPUTKG = 1000f;

		// Token: 0x04004C5B RID: 19547
		public const float GEOREFINERY_SLAB_OUTPUTKG = 200f;

		// Token: 0x04004C5C RID: 19548
		public const float GEOREFINERY_FABRICATIONTIME = 120f;

		// Token: 0x04004C5D RID: 19549
		public const float MASS_BURN_RATE_HYDROGENGENERATOR = 0.1f;

		// Token: 0x04004C5E RID: 19550
		public const float COOKER_FOOD_TEMPERATURE = 368.15f;

		// Token: 0x04004C5F RID: 19551
		public const float OVERHEAT_DAMAGE_INTERVAL = 7.5f;

		// Token: 0x04004C60 RID: 19552
		public const float MIN_BUILD_TEMPERATURE = 288.15f;

		// Token: 0x04004C61 RID: 19553
		public const float MAX_BUILD_TEMPERATURE = 318.15f;

		// Token: 0x04004C62 RID: 19554
		public const float MELTDOWN_TEMPERATURE = 533.15f;

		// Token: 0x04004C63 RID: 19555
		public const float REPAIR_FORCE_TEMPERATURE = 293.15f;

		// Token: 0x04004C64 RID: 19556
		public const int REPAIR_EFFECTIVENESS_BASE = 10;

		// Token: 0x04004C65 RID: 19557
		public static Dictionary<string, string> PLANSUBCATEGORYSORTING = new Dictionary<string, string>
		{
			{ "Ladder", "ladders" },
			{ "FirePole", "ladders" },
			{ "LadderFast", "ladders" },
			{ "Tile", "tiles" },
			{ "GasPermeableMembrane", "tiles" },
			{ "MeshTile", "tiles" },
			{ "InsulationTile", "tiles" },
			{ "PlasticTile", "tiles" },
			{ "MetalTile", "tiles" },
			{ "GlassTile", "tiles" },
			{ "BunkerTile", "tiles" },
			{ "ExteriorWall", "tiles" },
			{ "CarpetTile", "tiles" },
			{ "ExobaseHeadquarters", "printingpods" },
			{ "Door", "doors" },
			{ "ManualPressureDoor", "doors" },
			{ "PressureDoor", "doors" },
			{ "BunkerDoor", "doors" },
			{ "StorageLocker", "storage" },
			{ "StorageLockerSmart", "storage" },
			{ "LiquidReservoir", "storage" },
			{ "GasReservoir", "storage" },
			{ "ObjectDispenser", "storage" },
			{ "TravelTube", "transport" },
			{ "TravelTubeEntrance", "transport" },
			{ "TravelTubeWallBridge", "transport" },
			{ "MineralDeoxidizer", "producers" },
			{ "SublimationStation", "producers" },
			{ "Electrolyzer", "producers" },
			{ "RustDeoxidizer", "producers" },
			{ "AirFilter", "scrubbers" },
			{ "CO2Scrubber", "scrubbers" },
			{ "AlgaeHabitat", "scrubbers" },
			{ "DevGenerator", "generators" },
			{ "ManualGenerator", "generators" },
			{ "Generator", "generators" },
			{ "WoodGasGenerator", "generators" },
			{ "HydrogenGenerator", "generators" },
			{ "MethaneGenerator", "generators" },
			{ "PetroleumGenerator", "generators" },
			{ "SteamTurbine", "generators" },
			{ "SteamTurbine2", "generators" },
			{ "SolarPanel", "generators" },
			{ "Wire", "wires" },
			{ "WireBridge", "wires" },
			{ "HighWattageWire", "wires" },
			{ "WireBridgeHighWattage", "wires" },
			{ "WireRefined", "wires" },
			{ "WireRefinedBridge", "wires" },
			{ "WireRefinedHighWattage", "wires" },
			{ "WireRefinedBridgeHighWattage", "wires" },
			{ "Battery", "batteries" },
			{ "BatteryMedium", "batteries" },
			{ "BatterySmart", "batteries" },
			{ "PowerTransformerSmall", "powercontrol" },
			{ "PowerTransformer", "powercontrol" },
			{
				SwitchConfig.ID,
				"switches"
			},
			{
				LogicPowerRelayConfig.ID,
				"switches"
			},
			{
				TemperatureControlledSwitchConfig.ID,
				"switches"
			},
			{
				PressureSwitchLiquidConfig.ID,
				"switches"
			},
			{
				PressureSwitchGasConfig.ID,
				"switches"
			},
			{ "MicrobeMusher", "cooking" },
			{ "CookingStation", "cooking" },
			{ "GourmetCookingStation", "cooking" },
			{ "SpiceGrinder", "cooking" },
			{ "PlanterBox", "farming" },
			{ "FarmTile", "farming" },
			{ "HydroponicFarm", "farming" },
			{ "RationBox", "storage" },
			{ "Refrigerator", "storage" },
			{ "CreatureDeliveryPoint", "ranching" },
			{ "FishDeliveryPoint", "ranching" },
			{ "CreatureFeeder", "ranching" },
			{ "FishFeeder", "ranching" },
			{ "EggIncubator", "ranching" },
			{ "EggCracker", "ranching" },
			{ "CreatureTrap", "ranching" },
			{ "FishTrap", "ranching" },
			{ "AirborneCreatureLure", "ranching" },
			{ "FlyingCreatureBait", "ranching" },
			{ "Outhouse", "washroom" },
			{ "FlushToilet", "washroom" },
			{ "WallToilet", "washroom" },
			{
				ShowerConfig.ID,
				"washroom"
			},
			{ "LiquidConduit", "pipes" },
			{ "InsulatedLiquidConduit", "pipes" },
			{ "LiquidConduitRadiant", "pipes" },
			{ "LiquidConduitBridge", "pipes" },
			{ "ContactConductivePipeBridge", "pipes" },
			{ "LiquidVent", "pipes" },
			{ "LiquidPump", "pumps" },
			{ "LiquidMiniPump", "pumps" },
			{ "LiquidPumpingStation", "pumps" },
			{ "DevPumpLiquid", "pumps" },
			{ "BottleEmptier", "valves" },
			{ "LiquidFilter", "valves" },
			{ "LiquidConduitPreferentialFlow", "valves" },
			{ "LiquidConduitOverflow", "valves" },
			{ "LiquidValve", "valves" },
			{ "LiquidLogicValve", "valves" },
			{ "LiquidLimitValve", "valves" },
			{
				LiquidConduitElementSensorConfig.ID,
				"sensors"
			},
			{
				LiquidConduitDiseaseSensorConfig.ID,
				"sensors"
			},
			{
				LiquidConduitTemperatureSensorConfig.ID,
				"sensors"
			},
			{ "ModularLaunchpadPortLiquid", "buildmenuports" },
			{ "ModularLaunchpadPortLiquidUnloader", "buildmenuports" },
			{ "GasConduit", "pipes" },
			{ "InsulatedGasConduit", "pipes" },
			{ "GasConduitRadiant", "pipes" },
			{ "GasConduitBridge", "pipes" },
			{ "GasVent", "pipes" },
			{ "GasVentHighPressure", "pipes" },
			{ "GasPump", "pumps" },
			{ "GasMiniPump", "pumps" },
			{ "DevPumpGas", "pumps" },
			{ "GasBottler", "valves" },
			{ "BottleEmptierGas", "valves" },
			{ "GasFilter", "valves" },
			{ "GasConduitPreferentialFlow", "valves" },
			{ "GasConduitOverflow", "valves" },
			{ "GasValve", "valves" },
			{ "GasLogicValve", "valves" },
			{ "GasLimitValve", "valves" },
			{
				GasConduitElementSensorConfig.ID,
				"sensors"
			},
			{
				GasConduitDiseaseSensorConfig.ID,
				"sensors"
			},
			{
				GasConduitTemperatureSensorConfig.ID,
				"sensors"
			},
			{ "ModularLaunchpadPortGas", "buildmenuports" },
			{ "ModularLaunchpadPortGasUnloader", "buildmenuports" },
			{ "Compost", "materials" },
			{ "WaterPurifier", "materials" },
			{ "Desalinator", "materials" },
			{ "FertilizerMaker", "materials" },
			{ "AlgaeDistillery", "materials" },
			{ "EthanolDistillery", "materials" },
			{ "RockCrusher", "materials" },
			{ "Kiln", "materials" },
			{ "SludgePress", "materials" },
			{ "MetalRefinery", "materials" },
			{ "GlassForge", "materials" },
			{ "OilRefinery", "oil" },
			{ "Polymerizer", "oil" },
			{ "OxyliteRefinery", "advanced" },
			{ "SupermaterialRefinery", "advanced" },
			{ "DiamondPress", "advanced" },
			{ "WashBasin", "hygiene" },
			{ "WashSink", "hygiene" },
			{ "HandSanitizer", "hygiene" },
			{ "DecontaminationShower", "hygiene" },
			{ "Apothecary", "medical" },
			{ "DoctorStation", "medical" },
			{ "AdvancedDoctorStation", "medical" },
			{ "MedicalCot", "medical" },
			{ "DevLifeSupport", "medical" },
			{ "MassageTable", "wellness" },
			{ "Grave", "wellness" },
			{ "Bed", "beds" },
			{ "LuxuryBed", "beds" },
			{
				LadderBedConfig.ID,
				"beds"
			},
			{ "FloorLamp", "lights" },
			{ "CeilingLight", "lights" },
			{ "SunLamp", "lights" },
			{ "DiningTable", "dining" },
			{ "WaterCooler", "recreation" },
			{ "Phonobox", "recreation" },
			{ "ArcadeMachine", "recreation" },
			{ "EspressoMachine", "recreation" },
			{ "HotTub", "recreation" },
			{ "MechanicalSurfboard", "recreation" },
			{ "Sauna", "recreation" },
			{ "Juicer", "recreation" },
			{ "SodaFountain", "recreation" },
			{ "BeachChair", "recreation" },
			{ "VerticalWindTunnel", "recreation" },
			{ "Telephone", "recreation" },
			{ "FlowerVase", "decor" },
			{ "FlowerVaseWall", "decor" },
			{ "FlowerVaseHanging", "decor" },
			{ "FlowerVaseHangingFancy", "decor" },
			{
				PixelPackConfig.ID,
				"decor"
			},
			{ "SmallSculpture", "decor" },
			{ "Sculpture", "decor" },
			{ "IceSculpture", "decor" },
			{ "MarbleSculpture", "decor" },
			{ "MetalSculpture", "decor" },
			{ "CrownMoulding", "decor" },
			{ "CornerMoulding", "decor" },
			{ "Canvas", "decor" },
			{ "CanvasWide", "decor" },
			{ "CanvasTall", "decor" },
			{ "ItemPedestal", "decor" },
			{ "ParkSign", "decor" },
			{ "MonumentBottom", "decor" },
			{ "MonumentMiddle", "decor" },
			{ "MonumentTop", "decor" },
			{ "ResearchCenter", "research" },
			{ "AdvancedResearchCenter", "research" },
			{ "GeoTuner", "research" },
			{ "NuclearResearchCenter", "research" },
			{ "OrbitalResearchCenter", "research" },
			{ "CosmicResearchCenter", "research" },
			{ "DLC1CosmicResearchCenter", "research" },
			{ "ArtifactAnalysisStation", "archaeology" },
			{ "AstronautTrainingCenter", "exploration" },
			{ "PowerControlStation", "industrialstation" },
			{ "ResetSkillsStation", "industrialstation" },
			{ "RoleStation", "workstations" },
			{ "RanchStation", "ranching" },
			{ "ShearingStation", "ranching" },
			{ "FarmStation", "farming" },
			{ "GeneticAnalysisStation", "farming" },
			{ "CraftingTable", "manufacturing" },
			{ "ClothingFabricator", "manufacturing" },
			{ "ClothingAlterationStation", "manufacturing" },
			{ "SuitFabricator", "manufacturing" },
			{ "OxygenMaskMarker", "equipment" },
			{ "OxygenMaskLocker", "equipment" },
			{ "SuitMarker", "equipment" },
			{ "SuitLocker", "equipment" },
			{ "JetSuitMarker", "equipment" },
			{ "JetSuitLocker", "equipment" },
			{ "LeadSuitMarker", "equipment" },
			{ "LeadSuitLocker", "equipment" },
			{ "SpaceHeater", "temperature" },
			{ "LiquidHeater", "temperature" },
			{ "LiquidConditioner", "temperature" },
			{ "LiquidCooledFan", "temperature" },
			{ "IceCooledFan", "temperature" },
			{ "IceMachine", "temperature" },
			{ "AirConditioner", "temperature" },
			{ "ThermalBlock", "temperature" },
			{ "OreScrubber", "sanitation" },
			{ "OilWellCap", "oil" },
			{ "SweepBotStation", "sanitation" },
			{ "LogicWire", "wires" },
			{ "LogicWireBridge", "wires" },
			{ "LogicRibbon", "wires" },
			{ "LogicRibbonBridge", "wires" },
			{
				LogicRibbonReaderConfig.ID,
				"wires"
			},
			{
				LogicRibbonWriterConfig.ID,
				"wires"
			},
			{ "LogicDuplicantSensor", "sensors" },
			{
				LogicPressureSensorGasConfig.ID,
				"sensors"
			},
			{
				LogicPressureSensorLiquidConfig.ID,
				"sensors"
			},
			{
				LogicTemperatureSensorConfig.ID,
				"sensors"
			},
			{
				LogicWattageSensorConfig.ID,
				"sensors"
			},
			{
				LogicTimeOfDaySensorConfig.ID,
				"sensors"
			},
			{
				LogicTimerSensorConfig.ID,
				"sensors"
			},
			{
				LogicDiseaseSensorConfig.ID,
				"sensors"
			},
			{
				LogicElementSensorGasConfig.ID,
				"sensors"
			},
			{
				LogicElementSensorLiquidConfig.ID,
				"sensors"
			},
			{
				LogicCritterCountSensorConfig.ID,
				"sensors"
			},
			{
				LogicRadiationSensorConfig.ID,
				"sensors"
			},
			{
				LogicHEPSensorConfig.ID,
				"sensors"
			},
			{
				CometDetectorConfig.ID,
				"sensors"
			},
			{
				LogicCounterConfig.ID,
				"logicmanager"
			},
			{ "Checkpoint", "logicmanager" },
			{
				LogicAlarmConfig.ID,
				"logicmanager"
			},
			{
				LogicHammerConfig.ID,
				"logicaudio"
			},
			{
				LogicSwitchConfig.ID,
				"switches"
			},
			{ "FloorSwitch", "switches" },
			{ "LogicGateNOT", "logicgates" },
			{ "LogicGateAND", "logicgates" },
			{ "LogicGateOR", "logicgates" },
			{ "LogicGateBUFFER", "logicgates" },
			{ "LogicGateFILTER", "logicgates" },
			{ "LogicGateXOR", "logicgates" },
			{
				LogicMemoryConfig.ID,
				"logicgates"
			},
			{ "LogicGateMultiplexer", "logicgates" },
			{ "LogicGateDemultiplexer", "logicgates" },
			{ "LogicInterasteroidSender", "transmissions" },
			{ "LogicInterasteroidReceiver", "transmissions" },
			{ "SolidConduit", "conveyancestructures" },
			{ "SolidConduitBridge", "conveyancestructures" },
			{ "SolidConduitInbox", "conveyancestructures" },
			{ "SolidConduitOutbox", "conveyancestructures" },
			{ "SolidFilter", "conveyancestructures" },
			{ "SolidVent", "conveyancestructures" },
			{ "DevPumpSolid", "pumps" },
			{ "SolidLogicValve", "valves" },
			{ "SolidLimitValve", "valves" },
			{
				SolidConduitDiseaseSensorConfig.ID,
				"sensors"
			},
			{
				SolidConduitElementSensorConfig.ID,
				"sensors"
			},
			{
				SolidConduitTemperatureSensorConfig.ID,
				"sensors"
			},
			{ "AutoMiner", "automated" },
			{ "SolidTransferArm", "automated" },
			{ "ModularLaunchpadPortSolid", "buildmenuports" },
			{ "ModularLaunchpadPortSolidUnloader", "buildmenuports" },
			{ "Telescope", "telescopes" },
			{ "ClusterTelescope", "telescopes" },
			{ "ClusterTelescopeEnclosed", "telescopes" },
			{ "LaunchPad", "rocketstructures" },
			{ "Gantry", "rocketstructures" },
			{ "RailGun", "fittings" },
			{ "RailGunPayloadOpener", "fittings" },
			{ "LandingBeacon", "rocketnav" },
			{ "SteamEngine", "engines" },
			{ "KeroseneEngine", "engines" },
			{ "HydrogenEngine", "engines" },
			{ "SolidBooster", "engines" },
			{ "LiquidFuelTank", "fuel and oxidizer" },
			{ "OxidizerTank", "fuel and oxidizer" },
			{ "OxidizerTankLiquid", "fuel and oxidizer" },
			{ "CargoBay", "cargo" },
			{ "GasCargoBay", "cargo" },
			{ "LiquidCargoBay", "cargo" },
			{ "SpecialCargoBay", "utility" },
			{ "CommandModule", "command" },
			{
				RocketControlStationConfig.ID,
				"rocketnav"
			},
			{
				LogicClusterLocationSensorConfig.ID,
				"rocketnav"
			},
			{ "MissionControl", "rocketnav" },
			{ "MissionControlCluster", "rocketnav" },
			{ "TouristModule", "utility" },
			{ "ResearchModule", "utility" },
			{ "RocketInteriorPowerPlug", "fittings" },
			{ "RocketInteriorLiquidInput", "fittings" },
			{ "RocketInteriorLiquidOutput", "fittings" },
			{ "RocketInteriorGasInput", "fittings" },
			{ "RocketInteriorGasOutput", "fittings" },
			{ "RocketInteriorSolidInput", "fittings" },
			{ "RocketInteriorSolidOutput", "fittings" },
			{ "ManualHighEnergyParticleSpawner", "producers" },
			{ "HighEnergyParticleSpawner", "producers" },
			{ "HighEnergyParticleRedirector", "transmissions" },
			{ "HEPBattery", "batteries" },
			{ "HEPBridgeTile", "transmissions" },
			{ "NuclearReactor", "producers" },
			{ "UraniumCentrifuge", "producers" },
			{ "RadiationLight", "producers" },
			{ "DevRadiationGenerator", "producers" }
		};

		// Token: 0x04004C66 RID: 19558
		public static List<PlanScreen.PlanInfo> PLANORDER = new List<PlanScreen.PlanInfo>
		{
			new PlanScreen.PlanInfo(new HashedString("Base"), false, new List<string>
			{
				"Ladder", "FirePole", "LadderFast", "Tile", "GasPermeableMembrane", "MeshTile", "InsulationTile", "PlasticTile", "MetalTile", "GlassTile",
				"BunkerTile", "CarpetTile", "ExteriorWall", "ExobaseHeadquarters", "Door", "ManualPressureDoor", "PressureDoor", "BunkerDoor", "StorageLocker", "StorageLockerSmart",
				"LiquidReservoir", "GasReservoir", "ObjectDispenser", "TravelTube", "TravelTubeEntrance", "TravelTubeWallBridge"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Oxygen"), false, new List<string> { "MineralDeoxidizer", "SublimationStation", "AlgaeHabitat", "AirFilter", "CO2Scrubber", "Electrolyzer", "RustDeoxidizer" }, ""),
			new PlanScreen.PlanInfo(new HashedString("Power"), false, new List<string>
			{
				"DevGenerator",
				"ManualGenerator",
				"Generator",
				"WoodGasGenerator",
				"HydrogenGenerator",
				"MethaneGenerator",
				"PetroleumGenerator",
				"SteamTurbine",
				"SteamTurbine2",
				"SolarPanel",
				"Wire",
				"WireBridge",
				"HighWattageWire",
				"WireBridgeHighWattage",
				"WireRefined",
				"WireRefinedBridge",
				"WireRefinedHighWattage",
				"WireRefinedBridgeHighWattage",
				"Battery",
				"BatteryMedium",
				"BatterySmart",
				"PowerTransformerSmall",
				"PowerTransformer",
				SwitchConfig.ID,
				LogicPowerRelayConfig.ID,
				TemperatureControlledSwitchConfig.ID,
				PressureSwitchLiquidConfig.ID,
				PressureSwitchGasConfig.ID
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Food"), false, new List<string>
			{
				"MicrobeMusher", "CookingStation", "GourmetCookingStation", "SpiceGrinder", "PlanterBox", "FarmTile", "HydroponicFarm", "RationBox", "Refrigerator", "CreatureDeliveryPoint",
				"FishDeliveryPoint", "CreatureFeeder", "FishFeeder", "EggIncubator", "EggCracker", "CreatureTrap", "FishTrap", "AirborneCreatureLure", "FlyingCreatureBait"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Plumbing"), false, new List<string>
			{
				"DevPumpLiquid",
				"Outhouse",
				"FlushToilet",
				"WallToilet",
				ShowerConfig.ID,
				"LiquidPumpingStation",
				"BottleEmptier",
				"LiquidConduit",
				"InsulatedLiquidConduit",
				"LiquidConduitRadiant",
				"LiquidConduitBridge",
				"LiquidConduitPreferentialFlow",
				"LiquidConduitOverflow",
				"LiquidPump",
				"LiquidMiniPump",
				"LiquidVent",
				"LiquidFilter",
				"LiquidValve",
				"LiquidLogicValve",
				"LiquidLimitValve",
				LiquidConduitElementSensorConfig.ID,
				LiquidConduitDiseaseSensorConfig.ID,
				LiquidConduitTemperatureSensorConfig.ID,
				"ModularLaunchpadPortLiquid",
				"ModularLaunchpadPortLiquidUnloader",
				"ContactConductivePipeBridge"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("HVAC"), false, new List<string>
			{
				"DevPumpGas",
				"GasConduit",
				"InsulatedGasConduit",
				"GasConduitRadiant",
				"GasConduitBridge",
				"GasConduitPreferentialFlow",
				"GasConduitOverflow",
				"GasPump",
				"GasMiniPump",
				"GasVent",
				"GasVentHighPressure",
				"GasFilter",
				"GasValve",
				"GasLogicValve",
				"GasLimitValve",
				"GasBottler",
				"BottleEmptierGas",
				"ModularLaunchpadPortGas",
				"ModularLaunchpadPortGasUnloader",
				GasConduitElementSensorConfig.ID,
				GasConduitDiseaseSensorConfig.ID,
				GasConduitTemperatureSensorConfig.ID
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Refining"), false, new List<string>
			{
				"Compost", "WaterPurifier", "Desalinator", "FertilizerMaker", "AlgaeDistillery", "EthanolDistillery", "RockCrusher", "Kiln", "SludgePress", "MetalRefinery",
				"GlassForge", "OilRefinery", "Polymerizer", "OxyliteRefinery", "SupermaterialRefinery", "DiamondPress"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Medical"), false, new List<string>
			{
				"DevLifeSupport", "WashBasin", "WashSink", "HandSanitizer", "DecontaminationShower", "Apothecary", "DoctorStation", "AdvancedDoctorStation", "MedicalCot", "MassageTable",
				"Grave"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Furniture"), false, new List<string>
			{
				"Bed",
				"LuxuryBed",
				LadderBedConfig.ID,
				"FloorLamp",
				"CeilingLight",
				"SunLamp",
				"DiningTable",
				"WaterCooler",
				"Phonobox",
				"ArcadeMachine",
				"EspressoMachine",
				"HotTub",
				"MechanicalSurfboard",
				"Sauna",
				"Juicer",
				"SodaFountain",
				"BeachChair",
				"VerticalWindTunnel",
				PixelPackConfig.ID,
				"Telephone",
				"FlowerVase",
				"FlowerVaseWall",
				"FlowerVaseHanging",
				"FlowerVaseHangingFancy",
				"SmallSculpture",
				"Sculpture",
				"IceSculpture",
				"MarbleSculpture",
				"MetalSculpture",
				"CrownMoulding",
				"CornerMoulding",
				"Canvas",
				"CanvasWide",
				"CanvasTall",
				"ItemPedestal",
				"MonumentBottom",
				"MonumentMiddle",
				"MonumentTop",
				"ParkSign"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Equipment"), false, new List<string>
			{
				"ResearchCenter", "AdvancedResearchCenter", "NuclearResearchCenter", "OrbitalResearchCenter", "CosmicResearchCenter", "DLC1CosmicResearchCenter", "Telescope", "GeoTuner", "PowerControlStation", "FarmStation",
				"GeneticAnalysisStation", "RanchStation", "ShearingStation", "RoleStation", "ResetSkillsStation", "ArtifactAnalysisStation", "CraftingTable", "ClothingFabricator", "ClothingAlterationStation", "SuitFabricator",
				"OxygenMaskMarker", "OxygenMaskLocker", "SuitMarker", "SuitLocker", "JetSuitMarker", "JetSuitLocker", "LeadSuitMarker", "LeadSuitLocker", "AstronautTrainingCenter"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Utilities"), true, new List<string>
			{
				"SpaceHeater", "LiquidHeater", "LiquidCooledFan", "IceCooledFan", "IceMachine", "AirConditioner", "LiquidConditioner", "OreScrubber", "OilWellCap", "ThermalBlock",
				"SweepBotStation"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Automation"), true, new List<string>
			{
				"LogicWire",
				"LogicWireBridge",
				"LogicRibbon",
				"LogicRibbonBridge",
				LogicSwitchConfig.ID,
				"LogicDuplicantSensor",
				LogicPressureSensorGasConfig.ID,
				LogicPressureSensorLiquidConfig.ID,
				LogicTemperatureSensorConfig.ID,
				LogicWattageSensorConfig.ID,
				LogicTimeOfDaySensorConfig.ID,
				LogicTimerSensorConfig.ID,
				LogicDiseaseSensorConfig.ID,
				LogicElementSensorGasConfig.ID,
				LogicElementSensorLiquidConfig.ID,
				LogicCritterCountSensorConfig.ID,
				LogicRadiationSensorConfig.ID,
				LogicHEPSensorConfig.ID,
				LogicCounterConfig.ID,
				LogicAlarmConfig.ID,
				LogicHammerConfig.ID,
				"LogicInterasteroidSender",
				"LogicInterasteroidReceiver",
				LogicRibbonReaderConfig.ID,
				LogicRibbonWriterConfig.ID,
				"FloorSwitch",
				"Checkpoint",
				CometDetectorConfig.ID,
				"LogicGateNOT",
				"LogicGateAND",
				"LogicGateOR",
				"LogicGateBUFFER",
				"LogicGateFILTER",
				"LogicGateXOR",
				LogicMemoryConfig.ID,
				"LogicGateMultiplexer",
				"LogicGateDemultiplexer"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Conveyance"), true, new List<string>
			{
				"DevPumpSolid",
				"SolidTransferArm",
				"SolidConduit",
				"SolidConduitBridge",
				"SolidConduitInbox",
				"SolidConduitOutbox",
				"SolidFilter",
				"SolidVent",
				"SolidLogicValve",
				"SolidLimitValve",
				SolidConduitDiseaseSensorConfig.ID,
				SolidConduitElementSensorConfig.ID,
				SolidConduitTemperatureSensorConfig.ID,
				"AutoMiner",
				"ModularLaunchpadPortSolid",
				"ModularLaunchpadPortSolidUnloader"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("Rocketry"), true, new List<string>
			{
				"ClusterTelescope",
				"ClusterTelescopeEnclosed",
				"MissionControl",
				"MissionControlCluster",
				"LaunchPad",
				"Gantry",
				"SteamEngine",
				"KeroseneEngine",
				"SolidBooster",
				"LiquidFuelTank",
				"OxidizerTank",
				"OxidizerTankLiquid",
				"CargoBay",
				"GasCargoBay",
				"LiquidCargoBay",
				"CommandModule",
				"TouristModule",
				"ResearchModule",
				"SpecialCargoBay",
				"HydrogenEngine",
				RocketControlStationConfig.ID,
				"RocketInteriorPowerPlug",
				"RocketInteriorLiquidInput",
				"RocketInteriorLiquidOutput",
				"RocketInteriorGasInput",
				"RocketInteriorGasOutput",
				"RocketInteriorSolidInput",
				"RocketInteriorSolidOutput",
				LogicClusterLocationSensorConfig.ID,
				"RailGun",
				"RailGunPayloadOpener",
				"LandingBeacon"
			}, ""),
			new PlanScreen.PlanInfo(new HashedString("HEP"), true, new List<string> { "RadiationLight", "ManualHighEnergyParticleSpawner", "NuclearReactor", "UraniumCentrifuge", "HighEnergyParticleSpawner", "HighEnergyParticleRedirector", "HEPBattery", "HEPBridgeTile", "DevRadiationGenerator" }, "EXPANSION1_ID")
		};

		// Token: 0x04004C67 RID: 19559
		public static List<Type> COMPONENT_DESCRIPTION_ORDER = new List<Type>
		{
			typeof(BottleEmptier),
			typeof(CookingStation),
			typeof(GourmetCookingStation),
			typeof(RoleStation),
			typeof(ResearchCenter),
			typeof(NuclearResearchCenter),
			typeof(LiquidCooledFan),
			typeof(HandSanitizer),
			typeof(HandSanitizer.Work),
			typeof(PlantAirConditioner),
			typeof(Clinic),
			typeof(BuildingElementEmitter),
			typeof(ElementConverter),
			typeof(ElementConsumer),
			typeof(PassiveElementConsumer),
			typeof(TinkerStation),
			typeof(EnergyConsumer),
			typeof(AirConditioner),
			typeof(Storage),
			typeof(Battery),
			typeof(AirFilter),
			typeof(FlushToilet),
			typeof(Toilet),
			typeof(EnergyGenerator),
			typeof(MassageTable),
			typeof(Shower),
			typeof(Ownable),
			typeof(PlantablePlot),
			typeof(RelaxationPoint),
			typeof(BuildingComplete),
			typeof(Building),
			typeof(BuildingPreview),
			typeof(BuildingUnderConstruction),
			typeof(Crop),
			typeof(Growing),
			typeof(Equippable),
			typeof(ColdBreather),
			typeof(ResearchPointObject),
			typeof(SuitTank),
			typeof(IlluminationVulnerable),
			typeof(TemperatureVulnerable),
			typeof(PressureVulnerable),
			typeof(SubmersionMonitor),
			typeof(BatterySmart),
			typeof(Compost),
			typeof(Refrigerator),
			typeof(Bed),
			typeof(OreScrubber),
			typeof(OreScrubber.Work),
			typeof(MinimumOperatingTemperature),
			typeof(RoomTracker),
			typeof(EnergyConsumerSelfSustaining),
			typeof(ArcadeMachine),
			typeof(Telescope),
			typeof(EspressoMachine),
			typeof(JetSuitTank),
			typeof(Phonobox),
			typeof(ArcadeMachine),
			typeof(BeachChair),
			typeof(Sauna),
			typeof(VerticalWindTunnel),
			typeof(HotTub),
			typeof(Juicer),
			typeof(SodaFountain),
			typeof(MechanicalSurfboard),
			typeof(BottleEmptier),
			typeof(AccessControl),
			typeof(GammaRayOven),
			typeof(Reactor),
			typeof(HighEnergyParticlePort),
			typeof(LeadSuitTank),
			typeof(ActiveParticleConsumer.Def),
			typeof(WaterCooler),
			typeof(Edible),
			typeof(PlantableSeed),
			typeof(SicknessTrigger),
			typeof(MedicinalPill),
			typeof(SeedProducer),
			typeof(Geyser),
			typeof(SpaceHeater),
			typeof(Overheatable),
			typeof(CreatureCalorieMonitor.Def),
			typeof(LureableMonitor.Def),
			typeof(CropSleepingMonitor.Def),
			typeof(FertilizationMonitor.Def),
			typeof(IrrigationMonitor.Def),
			typeof(ScaleGrowthMonitor.Def),
			typeof(TravelTubeEntrance.Work),
			typeof(ToiletWorkableUse),
			typeof(ReceptacleMonitor),
			typeof(Light2D),
			typeof(Ladder),
			typeof(SimCellOccupier),
			typeof(Vent),
			typeof(LogicPorts),
			typeof(Capturable),
			typeof(Trappable),
			typeof(SpaceArtifact),
			typeof(MessStation),
			typeof(PlantElementEmitter),
			typeof(Radiator),
			typeof(DecorProvider)
		};

		// Token: 0x02001B54 RID: 6996
		public class PHARMACY
		{
			// Token: 0x0200210E RID: 8462
			public class FABRICATIONTIME
			{
				// Token: 0x040092FB RID: 37627
				public const float TIER0 = 50f;

				// Token: 0x040092FC RID: 37628
				public const float TIER1 = 100f;

				// Token: 0x040092FD RID: 37629
				public const float TIER2 = 200f;
			}
		}

		// Token: 0x02001B55 RID: 6997
		public class NUCLEAR_REACTOR
		{
			// Token: 0x0200210F RID: 8463
			public class REACTOR_MASSES
			{
				// Token: 0x040092FE RID: 37630
				public const float MIN = 1f;

				// Token: 0x040092FF RID: 37631
				public const float MAX = 10f;
			}
		}

		// Token: 0x02001B56 RID: 6998
		public class OVERPRESSURE
		{
			// Token: 0x04007B39 RID: 31545
			public const float TIER0 = 1.8f;
		}

		// Token: 0x02001B57 RID: 6999
		public class OVERHEAT_TEMPERATURES
		{
			// Token: 0x04007B3A RID: 31546
			public const float LOW_3 = 10f;

			// Token: 0x04007B3B RID: 31547
			public const float LOW_2 = 328.15f;

			// Token: 0x04007B3C RID: 31548
			public const float LOW_1 = 338.15f;

			// Token: 0x04007B3D RID: 31549
			public const float NORMAL = 348.15f;

			// Token: 0x04007B3E RID: 31550
			public const float HIGH_1 = 363.15f;

			// Token: 0x04007B3F RID: 31551
			public const float HIGH_2 = 398.15f;

			// Token: 0x04007B40 RID: 31552
			public const float HIGH_3 = 1273.15f;

			// Token: 0x04007B41 RID: 31553
			public const float HIGH_4 = 2273.15f;
		}

		// Token: 0x02001B58 RID: 7000
		public class OVERHEAT_MATERIAL_MOD
		{
			// Token: 0x04007B42 RID: 31554
			public const float LOW_3 = -200f;

			// Token: 0x04007B43 RID: 31555
			public const float LOW_2 = -20f;

			// Token: 0x04007B44 RID: 31556
			public const float LOW_1 = -10f;

			// Token: 0x04007B45 RID: 31557
			public const float NORMAL = 0f;

			// Token: 0x04007B46 RID: 31558
			public const float HIGH_1 = 15f;

			// Token: 0x04007B47 RID: 31559
			public const float HIGH_2 = 50f;

			// Token: 0x04007B48 RID: 31560
			public const float HIGH_3 = 200f;

			// Token: 0x04007B49 RID: 31561
			public const float HIGH_4 = 500f;

			// Token: 0x04007B4A RID: 31562
			public const float HIGH_5 = 900f;
		}

		// Token: 0x02001B59 RID: 7001
		public class DECOR_MATERIAL_MOD
		{
			// Token: 0x04007B4B RID: 31563
			public const float NORMAL = 0f;

			// Token: 0x04007B4C RID: 31564
			public const float HIGH_1 = 0.1f;

			// Token: 0x04007B4D RID: 31565
			public const float HIGH_2 = 0.2f;

			// Token: 0x04007B4E RID: 31566
			public const float HIGH_3 = 0.5f;

			// Token: 0x04007B4F RID: 31567
			public const float HIGH_4 = 1f;
		}

		// Token: 0x02001B5A RID: 7002
		public class CONSTRUCTION_MASS_KG
		{
			// Token: 0x04007B50 RID: 31568
			public static readonly float[] TIER_TINY = new float[] { 5f };

			// Token: 0x04007B51 RID: 31569
			public static readonly float[] TIER0 = new float[] { 25f };

			// Token: 0x04007B52 RID: 31570
			public static readonly float[] TIER1 = new float[] { 50f };

			// Token: 0x04007B53 RID: 31571
			public static readonly float[] TIER2 = new float[] { 100f };

			// Token: 0x04007B54 RID: 31572
			public static readonly float[] TIER3 = new float[] { 200f };

			// Token: 0x04007B55 RID: 31573
			public static readonly float[] TIER4 = new float[] { 400f };

			// Token: 0x04007B56 RID: 31574
			public static readonly float[] TIER5 = new float[] { 800f };

			// Token: 0x04007B57 RID: 31575
			public static readonly float[] TIER6 = new float[] { 1200f };

			// Token: 0x04007B58 RID: 31576
			public static readonly float[] TIER7 = new float[] { 2000f };
		}

		// Token: 0x02001B5B RID: 7003
		public class ROCKETRY_MASS_KG
		{
			// Token: 0x04007B59 RID: 31577
			public static float[] COMMAND_MODULE_MASS = new float[] { 200f };

			// Token: 0x04007B5A RID: 31578
			public static float[] CARGO_MASS = new float[] { 1000f, 1000f };

			// Token: 0x04007B5B RID: 31579
			public static float[] CARGO_MASS_SMALL = new float[] { 400f, 400f };

			// Token: 0x04007B5C RID: 31580
			public static float[] FUEL_TANK_DRY_MASS = new float[] { 100f };

			// Token: 0x04007B5D RID: 31581
			public static float[] FUEL_TANK_WET_MASS = new float[] { 900f };

			// Token: 0x04007B5E RID: 31582
			public static float[] FUEL_TANK_WET_MASS_SMALL = new float[] { 300f };

			// Token: 0x04007B5F RID: 31583
			public static float[] FUEL_TANK_WET_MASS_GAS = new float[] { 100f };

			// Token: 0x04007B60 RID: 31584
			public static float[] FUEL_TANK_WET_MASS_GAS_LARGE = new float[] { 150f };

			// Token: 0x04007B61 RID: 31585
			public static float[] OXIDIZER_TANK_OXIDIZER_MASS = new float[] { 900f };

			// Token: 0x04007B62 RID: 31586
			public static float[] ENGINE_MASS_SMALL = new float[] { 200f };

			// Token: 0x04007B63 RID: 31587
			public static float[] ENGINE_MASS_LARGE = new float[] { 500f };

			// Token: 0x04007B64 RID: 31588
			public static float[] HOLLOW_TIER1 = new float[] { 200f, 100f };

			// Token: 0x04007B65 RID: 31589
			public static float[] HOLLOW_TIER2 = new float[] { 400f, 200f };

			// Token: 0x04007B66 RID: 31590
			public static float[] HOLLOW_TIER3 = new float[] { 800f, 400f };

			// Token: 0x04007B67 RID: 31591
			public static float[] DENSE_TIER0 = new float[] { 200f };

			// Token: 0x04007B68 RID: 31592
			public static float[] DENSE_TIER1 = new float[] { 500f };

			// Token: 0x04007B69 RID: 31593
			public static float[] DENSE_TIER2 = new float[] { 1000f };

			// Token: 0x04007B6A RID: 31594
			public static float[] DENSE_TIER3 = new float[] { 2000f };
		}

		// Token: 0x02001B5C RID: 7004
		public class ENERGY_CONSUMPTION_WHEN_ACTIVE
		{
			// Token: 0x04007B6B RID: 31595
			public const float TIER0 = 0f;

			// Token: 0x04007B6C RID: 31596
			public const float TIER1 = 5f;

			// Token: 0x04007B6D RID: 31597
			public const float TIER2 = 60f;

			// Token: 0x04007B6E RID: 31598
			public const float TIER3 = 120f;

			// Token: 0x04007B6F RID: 31599
			public const float TIER4 = 240f;

			// Token: 0x04007B70 RID: 31600
			public const float TIER5 = 480f;

			// Token: 0x04007B71 RID: 31601
			public const float TIER6 = 960f;

			// Token: 0x04007B72 RID: 31602
			public const float TIER7 = 1200f;

			// Token: 0x04007B73 RID: 31603
			public const float TIER8 = 1600f;
		}

		// Token: 0x02001B5D RID: 7005
		public class EXHAUST_ENERGY_ACTIVE
		{
			// Token: 0x04007B74 RID: 31604
			public const float TIER0 = 0f;

			// Token: 0x04007B75 RID: 31605
			public const float TIER1 = 0.125f;

			// Token: 0x04007B76 RID: 31606
			public const float TIER2 = 0.25f;

			// Token: 0x04007B77 RID: 31607
			public const float TIER3 = 0.5f;

			// Token: 0x04007B78 RID: 31608
			public const float TIER4 = 1f;

			// Token: 0x04007B79 RID: 31609
			public const float TIER5 = 2f;

			// Token: 0x04007B7A RID: 31610
			public const float TIER6 = 4f;

			// Token: 0x04007B7B RID: 31611
			public const float TIER7 = 8f;

			// Token: 0x04007B7C RID: 31612
			public const float TIER8 = 16f;
		}

		// Token: 0x02001B5E RID: 7006
		public class JOULES_LEAK_PER_CYCLE
		{
			// Token: 0x04007B7D RID: 31613
			public const float TIER0 = 400f;

			// Token: 0x04007B7E RID: 31614
			public const float TIER1 = 1000f;

			// Token: 0x04007B7F RID: 31615
			public const float TIER2 = 2000f;
		}

		// Token: 0x02001B5F RID: 7007
		public class SELF_HEAT_KILOWATTS
		{
			// Token: 0x04007B80 RID: 31616
			public const float TIER0 = 0f;

			// Token: 0x04007B81 RID: 31617
			public const float TIER1 = 0.5f;

			// Token: 0x04007B82 RID: 31618
			public const float TIER2 = 1f;

			// Token: 0x04007B83 RID: 31619
			public const float TIER3 = 2f;

			// Token: 0x04007B84 RID: 31620
			public const float TIER4 = 4f;

			// Token: 0x04007B85 RID: 31621
			public const float TIER5 = 8f;

			// Token: 0x04007B86 RID: 31622
			public const float TIER6 = 16f;

			// Token: 0x04007B87 RID: 31623
			public const float TIER7 = 32f;

			// Token: 0x04007B88 RID: 31624
			public const float TIER8 = 64f;

			// Token: 0x04007B89 RID: 31625
			public const float TIER_NUCLEAR = 16384f;
		}

		// Token: 0x02001B60 RID: 7008
		public class MELTING_POINT_KELVIN
		{
			// Token: 0x04007B8A RID: 31626
			public const float TIER0 = 800f;

			// Token: 0x04007B8B RID: 31627
			public const float TIER1 = 1600f;

			// Token: 0x04007B8C RID: 31628
			public const float TIER2 = 2400f;

			// Token: 0x04007B8D RID: 31629
			public const float TIER3 = 3200f;

			// Token: 0x04007B8E RID: 31630
			public const float TIER4 = 9999f;
		}

		// Token: 0x02001B61 RID: 7009
		public class CONSTRUCTION_TIME_SECONDS
		{
			// Token: 0x04007B8F RID: 31631
			public const float TIER0 = 3f;

			// Token: 0x04007B90 RID: 31632
			public const float TIER1 = 10f;

			// Token: 0x04007B91 RID: 31633
			public const float TIER2 = 30f;

			// Token: 0x04007B92 RID: 31634
			public const float TIER3 = 60f;

			// Token: 0x04007B93 RID: 31635
			public const float TIER4 = 120f;

			// Token: 0x04007B94 RID: 31636
			public const float TIER5 = 240f;

			// Token: 0x04007B95 RID: 31637
			public const float TIER6 = 480f;
		}

		// Token: 0x02001B62 RID: 7010
		public class HITPOINTS
		{
			// Token: 0x04007B96 RID: 31638
			public const int TIER0 = 10;

			// Token: 0x04007B97 RID: 31639
			public const int TIER1 = 30;

			// Token: 0x04007B98 RID: 31640
			public const int TIER2 = 100;

			// Token: 0x04007B99 RID: 31641
			public const int TIER3 = 250;

			// Token: 0x04007B9A RID: 31642
			public const int TIER4 = 1000;
		}

		// Token: 0x02001B63 RID: 7011
		public class DAMAGE_SOURCES
		{
			// Token: 0x04007B9B RID: 31643
			public const int CONDUIT_CONTENTS_BOILED = 1;

			// Token: 0x04007B9C RID: 31644
			public const int CONDUIT_CONTENTS_FROZE = 1;

			// Token: 0x04007B9D RID: 31645
			public const int BAD_INPUT_ELEMENT = 1;

			// Token: 0x04007B9E RID: 31646
			public const int BUILDING_OVERHEATED = 1;

			// Token: 0x04007B9F RID: 31647
			public const int HIGH_LIQUID_PRESSURE = 10;

			// Token: 0x04007BA0 RID: 31648
			public const int MICROMETEORITE = 1;

			// Token: 0x04007BA1 RID: 31649
			public const int CORROSIVE_ELEMENT = 1;
		}

		// Token: 0x02001B64 RID: 7012
		public class RELOCATION_TIME_SECONDS
		{
			// Token: 0x04007BA2 RID: 31650
			public const float DECONSTRUCT = 4f;

			// Token: 0x04007BA3 RID: 31651
			public const float CONSTRUCT = 4f;
		}

		// Token: 0x02001B65 RID: 7013
		public class WORK_TIME_SECONDS
		{
			// Token: 0x04007BA4 RID: 31652
			public const float VERYSHORT_WORK_TIME = 5f;

			// Token: 0x04007BA5 RID: 31653
			public const float SHORT_WORK_TIME = 15f;

			// Token: 0x04007BA6 RID: 31654
			public const float MEDIUM_WORK_TIME = 30f;

			// Token: 0x04007BA7 RID: 31655
			public const float LONG_WORK_TIME = 90f;

			// Token: 0x04007BA8 RID: 31656
			public const float VERY_LONG_WORK_TIME = 150f;

			// Token: 0x04007BA9 RID: 31657
			public const float EXTENSIVE_WORK_TIME = 180f;
		}

		// Token: 0x02001B66 RID: 7014
		public class FABRICATION_TIME_SECONDS
		{
			// Token: 0x04007BAA RID: 31658
			public const float VERY_SHORT = 20f;

			// Token: 0x04007BAB RID: 31659
			public const float SHORT = 40f;

			// Token: 0x04007BAC RID: 31660
			public const float MODERATE = 80f;

			// Token: 0x04007BAD RID: 31661
			public const float LONG = 250f;
		}

		// Token: 0x02001B67 RID: 7015
		public class DECOR
		{
			// Token: 0x04007BAE RID: 31662
			public static readonly EffectorValues NONE = new EffectorValues
			{
				amount = 0,
				radius = 1
			};

			// Token: 0x02002110 RID: 8464
			public class BONUS
			{
				// Token: 0x04009300 RID: 37632
				public static readonly EffectorValues TIER0 = new EffectorValues
				{
					amount = 5,
					radius = 1
				};

				// Token: 0x04009301 RID: 37633
				public static readonly EffectorValues TIER1 = new EffectorValues
				{
					amount = 10,
					radius = 2
				};

				// Token: 0x04009302 RID: 37634
				public static readonly EffectorValues TIER2 = new EffectorValues
				{
					amount = 15,
					radius = 3
				};

				// Token: 0x04009303 RID: 37635
				public static readonly EffectorValues TIER3 = new EffectorValues
				{
					amount = 20,
					radius = 4
				};

				// Token: 0x04009304 RID: 37636
				public static readonly EffectorValues TIER4 = new EffectorValues
				{
					amount = 25,
					radius = 5
				};

				// Token: 0x04009305 RID: 37637
				public static readonly EffectorValues TIER5 = new EffectorValues
				{
					amount = 30,
					radius = 6
				};

				// Token: 0x02002DB9 RID: 11705
				public class MONUMENT
				{
					// Token: 0x0400BA6A RID: 47722
					public static readonly EffectorValues COMPLETE = new EffectorValues
					{
						amount = 40,
						radius = 10
					};

					// Token: 0x0400BA6B RID: 47723
					public static readonly EffectorValues INCOMPLETE = new EffectorValues
					{
						amount = 10,
						radius = 5
					};
				}
			}

			// Token: 0x02002111 RID: 8465
			public class PENALTY
			{
				// Token: 0x04009306 RID: 37638
				public static readonly EffectorValues TIER0 = new EffectorValues
				{
					amount = -5,
					radius = 1
				};

				// Token: 0x04009307 RID: 37639
				public static readonly EffectorValues TIER1 = new EffectorValues
				{
					amount = -10,
					radius = 2
				};

				// Token: 0x04009308 RID: 37640
				public static readonly EffectorValues TIER2 = new EffectorValues
				{
					amount = -15,
					radius = 3
				};

				// Token: 0x04009309 RID: 37641
				public static readonly EffectorValues TIER3 = new EffectorValues
				{
					amount = -20,
					radius = 4
				};

				// Token: 0x0400930A RID: 37642
				public static readonly EffectorValues TIER4 = new EffectorValues
				{
					amount = -20,
					radius = 5
				};

				// Token: 0x0400930B RID: 37643
				public static readonly EffectorValues TIER5 = new EffectorValues
				{
					amount = -25,
					radius = 6
				};
			}
		}

		// Token: 0x02001B68 RID: 7016
		public class MASS_KG
		{
			// Token: 0x04007BAF RID: 31663
			public const float TIER0 = 25f;

			// Token: 0x04007BB0 RID: 31664
			public const float TIER1 = 50f;

			// Token: 0x04007BB1 RID: 31665
			public const float TIER2 = 100f;

			// Token: 0x04007BB2 RID: 31666
			public const float TIER3 = 200f;

			// Token: 0x04007BB3 RID: 31667
			public const float TIER4 = 400f;

			// Token: 0x04007BB4 RID: 31668
			public const float TIER5 = 800f;

			// Token: 0x04007BB5 RID: 31669
			public const float TIER6 = 1200f;

			// Token: 0x04007BB6 RID: 31670
			public const float TIER7 = 2000f;
		}

		// Token: 0x02001B69 RID: 7017
		public class UPGRADES
		{
			// Token: 0x04007BB7 RID: 31671
			public const float BUILDTIME_TIER0 = 120f;

			// Token: 0x02002112 RID: 8466
			public class MATERIALTAGS
			{
				// Token: 0x0400930C RID: 37644
				public const string METAL = "Metal";

				// Token: 0x0400930D RID: 37645
				public const string REFINEDMETAL = "RefinedMetal";

				// Token: 0x0400930E RID: 37646
				public const string CARBON = "Carbon";
			}

			// Token: 0x02002113 RID: 8467
			public class MATERIALMASS
			{
				// Token: 0x0400930F RID: 37647
				public const int TIER0 = 100;

				// Token: 0x04009310 RID: 37648
				public const int TIER1 = 200;

				// Token: 0x04009311 RID: 37649
				public const int TIER2 = 400;

				// Token: 0x04009312 RID: 37650
				public const int TIER3 = 500;
			}

			// Token: 0x02002114 RID: 8468
			public class MODIFIERAMOUNTS
			{
				// Token: 0x04009313 RID: 37651
				public const float MANUALGENERATOR_ENERGYGENERATION = 1.2f;

				// Token: 0x04009314 RID: 37652
				public const float MANUALGENERATOR_CAPACITY = 2f;

				// Token: 0x04009315 RID: 37653
				public const float PROPANEGENERATOR_ENERGYGENERATION = 1.6f;

				// Token: 0x04009316 RID: 37654
				public const float PROPANEGENERATOR_HEATGENERATION = 1.6f;

				// Token: 0x04009317 RID: 37655
				public const float GENERATOR_HEATGENERATION = 0.8f;

				// Token: 0x04009318 RID: 37656
				public const float GENERATOR_ENERGYGENERATION = 1.3f;

				// Token: 0x04009319 RID: 37657
				public const float TURBINE_ENERGYGENERATION = 1.2f;

				// Token: 0x0400931A RID: 37658
				public const float TURBINE_CAPACITY = 1.2f;

				// Token: 0x0400931B RID: 37659
				public const float SUITRECHARGER_EXECUTIONTIME = 1.2f;

				// Token: 0x0400931C RID: 37660
				public const float SUITRECHARGER_HEATGENERATION = 1.2f;

				// Token: 0x0400931D RID: 37661
				public const float STORAGELOCKER_CAPACITY = 2f;

				// Token: 0x0400931E RID: 37662
				public const float SOLARPANEL_ENERGYGENERATION = 1.2f;

				// Token: 0x0400931F RID: 37663
				public const float SMELTER_HEATGENERATION = 0.7f;
			}
		}
	}
}
