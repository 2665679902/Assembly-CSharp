using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using STRINGS;

namespace TUNING
{
	// Token: 0x02000D2B RID: 3371
	public class CREATURES
	{
		// Token: 0x04004D2B RID: 19755
		public const float WILD_GROWTH_RATE_MODIFIER = 0.25f;

		// Token: 0x04004D2C RID: 19756
		public const int DEFAULT_PROBING_RADIUS = 32;

		// Token: 0x04004D2D RID: 19757
		public const float FERTILITY_TIME_BY_LIFESPAN = 0.6f;

		// Token: 0x04004D2E RID: 19758
		public const float INCUBATION_TIME_BY_LIFESPAN = 0.2f;

		// Token: 0x04004D2F RID: 19759
		public const float INCUBATOR_INCUBATION_MULTIPLIER = 4f;

		// Token: 0x04004D30 RID: 19760
		public const float WILD_CALORIE_BURN_RATIO = 0.25f;

		// Token: 0x04004D31 RID: 19761
		public const float HUG_INCUBATION_MULTIPLIER = 1f;

		// Token: 0x04004D32 RID: 19762
		public const float VIABILITY_LOSS_RATE = -0.016666668f;

		// Token: 0x04004D33 RID: 19763
		public const float STATERPILLAR_POWER_CHARGE_LOSS_RATE = -0.055555556f;

		// Token: 0x02001B98 RID: 7064
		public class HITPOINTS
		{
			// Token: 0x04007CF0 RID: 31984
			public const float TIER0 = 5f;

			// Token: 0x04007CF1 RID: 31985
			public const float TIER1 = 25f;

			// Token: 0x04007CF2 RID: 31986
			public const float TIER2 = 50f;

			// Token: 0x04007CF3 RID: 31987
			public const float TIER3 = 100f;

			// Token: 0x04007CF4 RID: 31988
			public const float TIER4 = 150f;

			// Token: 0x04007CF5 RID: 31989
			public const float TIER5 = 200f;

			// Token: 0x04007CF6 RID: 31990
			public const float TIER6 = 400f;
		}

		// Token: 0x02001B99 RID: 7065
		public class MASS_KG
		{
			// Token: 0x04007CF7 RID: 31991
			public const float TIER0 = 5f;

			// Token: 0x04007CF8 RID: 31992
			public const float TIER1 = 25f;

			// Token: 0x04007CF9 RID: 31993
			public const float TIER2 = 50f;

			// Token: 0x04007CFA RID: 31994
			public const float TIER3 = 100f;

			// Token: 0x04007CFB RID: 31995
			public const float TIER4 = 200f;

			// Token: 0x04007CFC RID: 31996
			public const float TIER5 = 400f;
		}

		// Token: 0x02001B9A RID: 7066
		public class TEMPERATURE
		{
			// Token: 0x04007CFD RID: 31997
			public static float FREEZING_10 = 173f;

			// Token: 0x04007CFE RID: 31998
			public static float FREEZING_9 = 183f;

			// Token: 0x04007CFF RID: 31999
			public static float FREEZING_3 = 243f;

			// Token: 0x04007D00 RID: 32000
			public static float FREEZING_2 = 253f;

			// Token: 0x04007D01 RID: 32001
			public static float FREEZING_1 = 263f;

			// Token: 0x04007D02 RID: 32002
			public static float FREEZING = 273f;

			// Token: 0x04007D03 RID: 32003
			public static float COOL = 283f;

			// Token: 0x04007D04 RID: 32004
			public static float MODERATE = 293f;

			// Token: 0x04007D05 RID: 32005
			public static float HOT = 303f;

			// Token: 0x04007D06 RID: 32006
			public static float HOT_1 = 313f;

			// Token: 0x04007D07 RID: 32007
			public static float HOT_2 = 323f;

			// Token: 0x04007D08 RID: 32008
			public static float HOT_3 = 333f;
		}

		// Token: 0x02001B9B RID: 7067
		public class LIFESPAN
		{
			// Token: 0x04007D09 RID: 32009
			public const float TIER0 = 5f;

			// Token: 0x04007D0A RID: 32010
			public const float TIER1 = 25f;

			// Token: 0x04007D0B RID: 32011
			public const float TIER2 = 75f;

			// Token: 0x04007D0C RID: 32012
			public const float TIER3 = 100f;

			// Token: 0x04007D0D RID: 32013
			public const float TIER4 = 150f;

			// Token: 0x04007D0E RID: 32014
			public const float TIER5 = 200f;

			// Token: 0x04007D0F RID: 32015
			public const float TIER6 = 400f;
		}

		// Token: 0x02001B9C RID: 7068
		public class CONVERSION_EFFICIENCY
		{
			// Token: 0x04007D10 RID: 32016
			public static float BAD_2 = 0.1f;

			// Token: 0x04007D11 RID: 32017
			public static float BAD_1 = 0.25f;

			// Token: 0x04007D12 RID: 32018
			public static float NORMAL = 0.5f;

			// Token: 0x04007D13 RID: 32019
			public static float GOOD_1 = 0.75f;

			// Token: 0x04007D14 RID: 32020
			public static float GOOD_2 = 0.95f;

			// Token: 0x04007D15 RID: 32021
			public static float GOOD_3 = 1f;
		}

		// Token: 0x02001B9D RID: 7069
		public class SPACE_REQUIREMENTS
		{
			// Token: 0x04007D16 RID: 32022
			public static int TIER1 = 4;

			// Token: 0x04007D17 RID: 32023
			public static int TIER2 = 8;

			// Token: 0x04007D18 RID: 32024
			public static int TIER3 = 12;

			// Token: 0x04007D19 RID: 32025
			public static int TIER4 = 16;
		}

		// Token: 0x02001B9E RID: 7070
		public class EGG_CHANCE_MODIFIERS
		{
			// Token: 0x06009689 RID: 38537 RVA: 0x00323DCF File Offset: 0x00321FCF
			private static System.Action CreateDietaryModifier(string id, Tag eggTag, HashSet<Tag> foodTags, float modifierPerCal)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = CREATURES.FERTILITY_MODIFIERS.DIET.NAME;
					string text2 = CREATURES.FERTILITY_MODIFIERS.DIET.DESC;
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text3 = text;
					string text4 = text2;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(string descStr)
						{
							string text5 = string.Join(", ", foodTags.Select((Tag t) => t.ProperName()).ToArray<string>());
							descStr = string.Format(descStr, text5);
							return descStr;
						});
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							inst.gameObject.Subscribe(-2038961714, delegate(object data)
							{
								CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = (CreatureCalorieMonitor.CaloriesConsumedEvent)data;
								if (foodTags.Contains(caloriesConsumedEvent.tag))
								{
									inst.AddBreedingChance(eggType, caloriesConsumedEvent.calories * modifierPerCal);
								}
							});
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text3, text4, func, fertilityModFn);
				};
			}

			// Token: 0x0600968A RID: 38538 RVA: 0x00323DFD File Offset: 0x00321FFD
			private static System.Action CreateDietaryModifier(string id, Tag eggTag, Tag foodTag, float modifierPerCal)
			{
				return CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier(id, eggTag, new HashSet<Tag> { foodTag }, modifierPerCal);
			}

			// Token: 0x0600968B RID: 38539 RVA: 0x00323E14 File Offset: 0x00322014
			private static System.Action CreateNearbyCreatureModifier(string id, Tag eggTag, Tag nearbyCreature, float modifierPerSecond, bool alsoInvert)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = ((modifierPerSecond < 0f) ? CREATURES.FERTILITY_MODIFIERS.NEARBY_CREATURE_NEG.NAME : CREATURES.FERTILITY_MODIFIERS.NEARBY_CREATURE.NAME);
					string text2 = ((modifierPerSecond < 0f) ? CREATURES.FERTILITY_MODIFIERS.NEARBY_CREATURE_NEG.DESC : CREATURES.FERTILITY_MODIFIERS.NEARBY_CREATURE.DESC);
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text3 = text;
					string text4 = text2;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (string descStr) => string.Format(descStr, nearbyCreature.ProperName()));
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							NearbyCreatureMonitor.Instance instance = inst.gameObject.GetSMI<NearbyCreatureMonitor.Instance>();
							if (instance == null)
							{
								instance = new NearbyCreatureMonitor.Instance(inst.master);
								instance.StartSM();
							}
							instance.OnUpdateNearbyCreatures += delegate(float dt, List<KPrefabID> creatures)
							{
								bool flag = false;
								using (List<KPrefabID>.Enumerator enumerator = creatures.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										if (enumerator.Current.PrefabTag == nearbyCreature)
										{
											flag = true;
											break;
										}
									}
								}
								if (flag)
								{
									inst.AddBreedingChance(eggType, dt * modifierPerSecond);
									return;
								}
								if (alsoInvert)
								{
									inst.AddBreedingChance(eggType, dt * -modifierPerSecond);
								}
							};
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text3, text4, func, fertilityModFn);
				};
			}

			// Token: 0x0600968C RID: 38540 RVA: 0x00323E4C File Offset: 0x0032204C
			private static System.Action CreateElementCreatureModifier(string id, Tag eggTag, Tag element, float modifierPerSecond, bool alsoInvert, bool checkSubstantialLiquid, string tooltipOverride = null)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = CREATURES.FERTILITY_MODIFIERS.LIVING_IN_ELEMENT.NAME;
					string text2 = CREATURES.FERTILITY_MODIFIERS.LIVING_IN_ELEMENT.DESC;
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text3 = text;
					string text4 = text2;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(string descStr)
						{
							if (tooltipOverride == null)
							{
								return string.Format(descStr, ElementLoader.GetElement(element).name);
							}
							return tooltipOverride;
						});
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							CritterElementMonitor.Instance instance = inst.gameObject.GetSMI<CritterElementMonitor.Instance>();
							if (instance == null)
							{
								instance = new CritterElementMonitor.Instance(inst.master);
								instance.StartSM();
							}
							instance.OnUpdateEggChances += delegate(float dt)
							{
								int num = Grid.PosToCell(inst);
								if (!Grid.IsValidCell(num))
								{
									return;
								}
								if (Grid.Element[num].HasTag(element) && (!checkSubstantialLiquid || Grid.IsSubstantialLiquid(num, 0.35f)))
								{
									inst.AddBreedingChance(eggType, dt * modifierPerSecond);
									return;
								}
								if (alsoInvert)
								{
									inst.AddBreedingChance(eggType, dt * -modifierPerSecond);
								}
							};
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text3, text4, func, fertilityModFn);
				};
			}

			// Token: 0x0600968D RID: 38541 RVA: 0x00323E9D File Offset: 0x0032209D
			private static System.Action CreateCropTendedModifier(string id, Tag eggTag, HashSet<Tag> cropTags, float modifierPerEvent)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = CREATURES.FERTILITY_MODIFIERS.CROPTENDING.NAME;
					string text2 = CREATURES.FERTILITY_MODIFIERS.CROPTENDING.DESC;
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text3 = text;
					string text4 = text2;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(string descStr)
						{
							string text5 = string.Join(", ", cropTags.Select((Tag t) => t.ProperName()).ToArray<string>());
							descStr = string.Format(descStr, text5);
							return descStr;
						});
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							inst.gameObject.Subscribe(90606262, delegate(object data)
							{
								CropTendingStates.CropTendingEventData cropTendingEventData = (CropTendingStates.CropTendingEventData)data;
								if (cropTags.Contains(cropTendingEventData.cropId))
								{
									inst.AddBreedingChance(eggType, modifierPerEvent);
								}
							});
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text3, text4, func, fertilityModFn);
				};
			}

			// Token: 0x0600968E RID: 38542 RVA: 0x00323ECB File Offset: 0x003220CB
			private static System.Action CreateTemperatureModifier(string id, Tag eggTag, float minTemp, float maxTemp, float modifierPerSecond, bool alsoInvert)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = CREATURES.FERTILITY_MODIFIERS.TEMPERATURE.NAME;
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text2 = text;
					string text3 = null;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (string src) => string.Format(CREATURES.FERTILITY_MODIFIERS.TEMPERATURE.DESC, GameUtil.GetFormattedTemperature(minTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetFormattedTemperature(maxTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)));
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							TemperatureVulnerable component = inst.master.GetComponent<TemperatureVulnerable>();
							if (component != null)
							{
								component.OnTemperature += delegate(float dt, float newTemp)
								{
									if (newTemp > minTemp && newTemp < maxTemp)
									{
										inst.AddBreedingChance(eggType, dt * modifierPerSecond);
										return;
									}
									if (alsoInvert)
									{
										inst.AddBreedingChance(eggType, dt * -modifierPerSecond);
									}
								};
								return;
							}
							DebugUtil.LogErrorArgs(new object[]
							{
								"Ack! Trying to add temperature modifier",
								id,
								"to",
								inst.master.name,
								"but it's not temperature vulnerable!"
							});
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text2, text3, func, fertilityModFn);
				};
			}

			// Token: 0x04007D1A RID: 32026
			public static List<System.Action> MODIFIER_CREATORS = new List<System.Action>
			{
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("HatchHard", "HatchHardEgg".ToTag(), SimHashes.SedimentaryRock.CreateTag(), 0.05f / HatchTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("HatchVeggie", "HatchVeggieEgg".ToTag(), SimHashes.Dirt.CreateTag(), 0.05f / HatchTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("HatchMetal", "HatchMetalEgg".ToTag(), HatchMetalConfig.METAL_ORE_TAGS, 0.05f / HatchTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateNearbyCreatureModifier("PuftAlphaBalance", "PuftAlphaEgg".ToTag(), "PuftAlpha".ToTag(), -0.00025f, true),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateNearbyCreatureModifier("PuftAlphaNearbyOxylite", "PuftOxyliteEgg".ToTag(), "PuftAlpha".ToTag(), 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateNearbyCreatureModifier("PuftAlphaNearbyBleachstone", "PuftBleachstoneEgg".ToTag(), "PuftAlpha".ToTag(), 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("OilFloaterHighTemp", "OilfloaterHighTempEgg".ToTag(), 373.15f, 523.15f, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("OilFloaterDecor", "OilfloaterDecorEgg".ToTag(), 293.15f, 333.15f, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugOrange", "LightBugOrangeEgg".ToTag(), "GrilledPrickleFruit".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugPurple", "LightBugPurpleEgg".ToTag(), "FriedMushroom".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugPink", "LightBugPinkEgg".ToTag(), "SpiceBread".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugBlue", "LightBugBlueEgg".ToTag(), "Salsa".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugBlack", "LightBugBlackEgg".ToTag(), SimHashes.Phosphorus.CreateTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugCrystal", "LightBugCrystalEgg".ToTag(), "CookedMeat".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("PacuTropical", "PacuTropicalEgg".ToTag(), 308.15f, 353.15f, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("PacuCleaner", "PacuCleanerEgg".ToTag(), 243.15f, 278.15f, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("DreckoPlastic", "DreckoPlasticEgg".ToTag(), "BasicSingleHarvestPlant".ToTag(), 0.025f / DreckoTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("SquirrelHug", "SquirrelHugEgg".ToTag(), BasicFabricMaterialPlantConfig.ID.ToTag(), 0.025f / SquirrelTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateCropTendedModifier("DivergentWorm", "DivergentWormEgg".ToTag(), new HashSet<Tag>
				{
					"WormPlant".ToTag(),
					"SuperWormPlant".ToTag()
				}, 0.05f / (float)DivergentTuning.TIMES_TENDED_PER_CYCLE_FOR_EVOLUTION),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateElementCreatureModifier("PokeLumber", "CrabWoodEgg".ToTag(), SimHashes.Ethanol.CreateTag(), 0.00025f, true, true, null),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateElementCreatureModifier("PokeFreshWater", "CrabFreshWaterEgg".ToTag(), SimHashes.Water.CreateTag(), 0.00025f, true, true, null),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("MoleDelicacy", "MoleDelicacyEgg".ToTag(), MoleDelicacyConfig.EGG_CHANCES_TEMPERATURE_MIN, MoleDelicacyConfig.EGG_CHANCES_TEMPERATURE_MAX, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateElementCreatureModifier("StaterpillarGas", "StaterpillarGasEgg".ToTag(), GameTags.Unbreathable, 0.00025f, true, false, CREATURES.FERTILITY_MODIFIERS.LIVING_IN_ELEMENT.UNBREATHABLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateElementCreatureModifier("StaterpillarLiquid", "StaterpillarLiquidEgg".ToTag(), GameTags.Liquid, 0.00025f, true, false, CREATURES.FERTILITY_MODIFIERS.LIVING_IN_ELEMENT.LIQUID)
			};
		}
	}
}
