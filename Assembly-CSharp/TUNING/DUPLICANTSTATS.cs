using System;
using System.Collections.Generic;
using UnityEngine;

namespace TUNING
{
	// Token: 0x02000D24 RID: 3364
	public class DUPLICANTSTATS
	{
		// Token: 0x060067F6 RID: 26614 RVA: 0x00286800 File Offset: 0x00284A00
		public static DUPLICANTSTATS.TraitVal GetTraitVal(string id)
		{
			foreach (DUPLICANTSTATS.TraitVal traitVal in DUPLICANTSTATS.SPECIALTRAITS)
			{
				if (id == traitVal.id)
				{
					return traitVal;
				}
			}
			foreach (DUPLICANTSTATS.TraitVal traitVal2 in DUPLICANTSTATS.GOODTRAITS)
			{
				if (id == traitVal2.id)
				{
					return traitVal2;
				}
			}
			foreach (DUPLICANTSTATS.TraitVal traitVal3 in DUPLICANTSTATS.BADTRAITS)
			{
				if (id == traitVal3.id)
				{
					return traitVal3;
				}
			}
			foreach (DUPLICANTSTATS.TraitVal traitVal4 in DUPLICANTSTATS.CONGENITALTRAITS)
			{
				if (id == traitVal4.id)
				{
					return traitVal4;
				}
			}
			DebugUtil.Assert(true, "Could not find TraitVal with ID: " + id);
			return DUPLICANTSTATS.INVALID_TRAIT_VAL;
		}

		// Token: 0x04004C9E RID: 19614
		public const float DEFAULT_MASS = 30f;

		// Token: 0x04004C9F RID: 19615
		public const float PEE_FUSE_TIME = 120f;

		// Token: 0x04004CA0 RID: 19616
		public const float PEE_PER_FLOOR_PEE = 2f;

		// Token: 0x04004CA1 RID: 19617
		public const float PEE_PER_TOILET_PEE = 6.7f;

		// Token: 0x04004CA2 RID: 19618
		public const string PEE_DISEASE = "FoodPoisoning";

		// Token: 0x04004CA3 RID: 19619
		public const int DISEASE_PER_PEE = 100000;

		// Token: 0x04004CA4 RID: 19620
		public const int DISEASE_PER_VOMIT = 100000;

		// Token: 0x04004CA5 RID: 19621
		public const float KCAL2JOULES = 4184f;

		// Token: 0x04004CA6 RID: 19622
		public const float COOLING_EFFICIENCY = 0.08f;

		// Token: 0x04004CA7 RID: 19623
		public const float DUPLICANT_COOLING_KILOWATTS = 0.5578667f;

		// Token: 0x04004CA8 RID: 19624
		public const float WARMING_EFFICIENCY = 0.08f;

		// Token: 0x04004CA9 RID: 19625
		public const float DUPLICANT_WARMING_KILOWATTS = 0.5578667f;

		// Token: 0x04004CAA RID: 19626
		public const float HEAT_GENERATION_EFFICIENCY = 0.012f;

		// Token: 0x04004CAB RID: 19627
		public const float DUPLICANT_BASE_GENERATION_KILOWATTS = 0.08368001f;

		// Token: 0x04004CAC RID: 19628
		public const float STANDARD_STRESS_PENALTY = 0.016666668f;

		// Token: 0x04004CAD RID: 19629
		public const float STANDARD_STRESS_BONUS = -0.033333335f;

		// Token: 0x04004CAE RID: 19630
		public const float RANCHING_DURATION_MULTIPLIER_BONUS_PER_POINT = 0.1f;

		// Token: 0x04004CAF RID: 19631
		public const float FARMING_DURATION_MULTIPLIER_BONUS_PER_POINT = 0.1f;

		// Token: 0x04004CB0 RID: 19632
		public const float POWER_DURATION_MULTIPLIER_BONUS_PER_POINT = 0.025f;

		// Token: 0x04004CB1 RID: 19633
		public const float RANCHING_CAPTURABLE_MULTIPLIER_BONUS_PER_POINT = 0.05f;

		// Token: 0x04004CB2 RID: 19634
		public const float STRESS_BELOW_EXPECTATIONS_FOOD = 0.25f;

		// Token: 0x04004CB3 RID: 19635
		public const float STRESS_ABOVE_EXPECTATIONS_FOOD = -0.5f;

		// Token: 0x04004CB4 RID: 19636
		public const float STANDARD_STRESS_PENALTY_SECOND = 0.25f;

		// Token: 0x04004CB5 RID: 19637
		public const float STANDARD_STRESS_BONUS_SECOND = -0.5f;

		// Token: 0x04004CB6 RID: 19638
		public const float RECOVER_BREATH_DELTA = 3f;

		// Token: 0x04004CB7 RID: 19639
		public const float TRAVEL_TIME_WARNING_THRESHOLD = 0.4f;

		// Token: 0x04004CB8 RID: 19640
		public static readonly string[] ALL_ATTRIBUTES = new string[]
		{
			"Strength", "Caring", "Construction", "Digging", "Machinery", "Learning", "Cooking", "Botanist", "Art", "Ranching",
			"Athletics", "SpaceNavigation"
		};

		// Token: 0x04004CB9 RID: 19641
		public static readonly string[] DISTRIBUTED_ATTRIBUTES = new string[] { "Strength", "Caring", "Construction", "Digging", "Machinery", "Learning", "Cooking", "Botanist", "Art", "Ranching" };

		// Token: 0x04004CBA RID: 19642
		public static readonly string[] ROLLED_ATTRIBUTES = new string[] { "Athletics" };

		// Token: 0x04004CBB RID: 19643
		public static readonly int[] APTITUDE_ATTRIBUTE_BONUSES = new int[] { 7, 3, 1 };

		// Token: 0x04004CBC RID: 19644
		public static int ROLLED_ATTRIBUTE_MAX = 5;

		// Token: 0x04004CBD RID: 19645
		public static float ROLLED_ATTRIBUTE_POWER = 4f;

		// Token: 0x04004CBE RID: 19646
		public static Dictionary<string, List<string>> ARCHETYPE_TRAIT_EXCLUSIONS = new Dictionary<string, List<string>>
		{
			{
				"Mining",
				new List<string> { "Anemic", "DiggingDown", "Narcolepsy" }
			},
			{
				"Building",
				new List<string> { "Anemic", "NoodleArms", "ConstructionDown", "DiggingDown", "Narcolepsy" }
			},
			{
				"Farming",
				new List<string> { "Anemic", "NoodleArms", "BotanistDown", "RanchingDown", "Narcolepsy" }
			},
			{
				"Ranching",
				new List<string> { "RanchingDown", "BotanistDown", "Narcolepsy" }
			},
			{
				"Cooking",
				new List<string> { "NoodleArms", "CookingDown" }
			},
			{
				"Art",
				new List<string> { "ArtDown", "DecorDown" }
			},
			{
				"Research",
				new List<string> { "SlowLearner" }
			},
			{
				"Suits",
				new List<string> { "Anemic", "NoodleArms" }
			},
			{
				"Hauling",
				new List<string> { "Anemic", "NoodleArms", "Narcolepsy" }
			},
			{
				"Technicals",
				new List<string> { "MachineryDown" }
			},
			{
				"MedicalAid",
				new List<string> { "CaringDown", "WeakImmuneSystem" }
			},
			{
				"Basekeeping",
				new List<string> { "Anemic", "NoodleArms" }
			},
			{
				"Rocketry",
				new List<string>()
			}
		};

		// Token: 0x04004CBF RID: 19647
		public static int RARITY_LEGENDARY = 5;

		// Token: 0x04004CC0 RID: 19648
		public static int RARITY_EPIC = 4;

		// Token: 0x04004CC1 RID: 19649
		public static int RARITY_RARE = 3;

		// Token: 0x04004CC2 RID: 19650
		public static int RARITY_UNCOMMON = 2;

		// Token: 0x04004CC3 RID: 19651
		public static int RARITY_COMMON = 1;

		// Token: 0x04004CC4 RID: 19652
		public static int NO_STATPOINT_BONUS = 0;

		// Token: 0x04004CC5 RID: 19653
		public static int TINY_STATPOINT_BONUS = 1;

		// Token: 0x04004CC6 RID: 19654
		public static int SMALL_STATPOINT_BONUS = 2;

		// Token: 0x04004CC7 RID: 19655
		public static int MEDIUM_STATPOINT_BONUS = 3;

		// Token: 0x04004CC8 RID: 19656
		public static int LARGE_STATPOINT_BONUS = 4;

		// Token: 0x04004CC9 RID: 19657
		public static int HUGE_STATPOINT_BONUS = 5;

		// Token: 0x04004CCA RID: 19658
		public static int COMMON = 1;

		// Token: 0x04004CCB RID: 19659
		public static int UNCOMMON = 2;

		// Token: 0x04004CCC RID: 19660
		public static int RARE = 3;

		// Token: 0x04004CCD RID: 19661
		public static int EPIC = 4;

		// Token: 0x04004CCE RID: 19662
		public static int LEGENDARY = 5;

		// Token: 0x04004CCF RID: 19663
		public static global::Tuple<int, int> TRAITS_ONE_POSITIVE_ONE_NEGATIVE = new global::Tuple<int, int>(1, 1);

		// Token: 0x04004CD0 RID: 19664
		public static global::Tuple<int, int> TRAITS_TWO_POSITIVE_ONE_NEGATIVE = new global::Tuple<int, int>(2, 1);

		// Token: 0x04004CD1 RID: 19665
		public static global::Tuple<int, int> TRAITS_ONE_POSITIVE_TWO_NEGATIVE = new global::Tuple<int, int>(1, 2);

		// Token: 0x04004CD2 RID: 19666
		public static global::Tuple<int, int> TRAITS_TWO_POSITIVE_TWO_NEGATIVE = new global::Tuple<int, int>(2, 2);

		// Token: 0x04004CD3 RID: 19667
		public static global::Tuple<int, int> TRAITS_THREE_POSITIVE_ONE_NEGATIVE = new global::Tuple<int, int>(3, 1);

		// Token: 0x04004CD4 RID: 19668
		public static global::Tuple<int, int> TRAITS_ONE_POSITIVE_THREE_NEGATIVE = new global::Tuple<int, int>(1, 3);

		// Token: 0x04004CD5 RID: 19669
		public static int MIN_STAT_POINTS = 0;

		// Token: 0x04004CD6 RID: 19670
		public static int MAX_STAT_POINTS = 0;

		// Token: 0x04004CD7 RID: 19671
		public static int MAX_TRAITS = 4;

		// Token: 0x04004CD8 RID: 19672
		public static int APTITUDE_BONUS = 1;

		// Token: 0x04004CD9 RID: 19673
		public static List<int> RARITY_DECK = new List<int>
		{
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_RARE,
			DUPLICANTSTATS.RARITY_RARE,
			DUPLICANTSTATS.RARITY_RARE,
			DUPLICANTSTATS.RARITY_RARE,
			DUPLICANTSTATS.RARITY_EPIC,
			DUPLICANTSTATS.RARITY_EPIC,
			DUPLICANTSTATS.RARITY_LEGENDARY
		};

		// Token: 0x04004CDA RID: 19674
		public static List<int> rarityDeckActive = new List<int>(DUPLICANTSTATS.RARITY_DECK);

		// Token: 0x04004CDB RID: 19675
		public static List<global::Tuple<int, int>> POD_TRAIT_CONFIGURATIONS_DECK = new List<global::Tuple<int, int>>
		{
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_THREE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_THREE_NEGATIVE
		};

		// Token: 0x04004CDC RID: 19676
		public static List<global::Tuple<int, int>> podTraitConfigurationsActive = new List<global::Tuple<int, int>>(DUPLICANTSTATS.POD_TRAIT_CONFIGURATIONS_DECK);

		// Token: 0x04004CDD RID: 19677
		public static readonly List<string> CONTRACTEDTRAITS_HEALING = new List<string> { "IrritableBowel", "Aggressive", "SlowLearner", "WeakImmuneSystem", "Snorer", "CantDig" };

		// Token: 0x04004CDE RID: 19678
		public static readonly List<DUPLICANTSTATS.TraitVal> CONGENITALTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "None"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Joshua",
				mutuallyExclusiveTraits = new List<string> { "ScaredyCat", "Aggressive" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Ellie",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				mutuallyExclusiveTraits = new List<string> { "InteriorDecorator", "MouthBreather", "Uncultured" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Stinky",
				mutuallyExclusiveTraits = new List<string> { "Flatulence", "InteriorDecorator" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Liam",
				mutuallyExclusiveTraits = new List<string> { "Flatulence", "InteriorDecorator" }
			}
		};

		// Token: 0x04004CDF RID: 19679
		public static readonly DUPLICANTSTATS.TraitVal INVALID_TRAIT_VAL = new DUPLICANTSTATS.TraitVal
		{
			id = "INVALID"
		};

		// Token: 0x04004CE0 RID: 19680
		public static readonly List<DUPLICANTSTATS.TraitVal> BADTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "CantResearch",
				statBonus = DUPLICANTSTATS.NO_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveAptitudes = new List<HashedString> { "Research" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CantDig",
				statBonus = DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveAptitudes = new List<HashedString> { "Mining" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CantCook",
				statBonus = DUPLICANTSTATS.NO_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = "",
				mutuallyExclusiveAptitudes = new List<HashedString> { "Cooking" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CantBuild",
				statBonus = DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveAptitudes = new List<HashedString> { "Building" },
				mutuallyExclusiveTraits = new List<string> { "GrantSkill_Engineering1" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Hemophobia",
				statBonus = DUPLICANTSTATS.NO_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = "",
				mutuallyExclusiveAptitudes = new List<HashedString> { "MedicalAid" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "ScaredyCat",
				statBonus = DUPLICANTSTATS.NO_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = "",
				mutuallyExclusiveAptitudes = new List<HashedString> { "Mining" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "ConstructionDown",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "ConstructionUp", "CantBuild" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "RanchingDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "RanchingUp" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CaringDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Hemophobia" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BotanistDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "ArtDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CookingDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Foodie", "CantCook" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "MachineryDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DiggingDown",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "MoleHands", "CantDig" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SlowLearner",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "FastLearner", "CantResearch" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "NoodleArms",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DecorDown",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Anemic",
				statBonus = DUPLICANTSTATS.HUGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Flatulence",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "IrritableBowel",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Snorer",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "MouthBreather",
				statBonus = DUPLICANTSTATS.HUGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SmallBladder",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CalorieBurner",
				statBonus = DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "WeakImmuneSystem",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Allergies",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "NightLight",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Narcolepsy",
				statBonus = DUPLICANTSTATS.HUGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = ""
			}
		};

		// Token: 0x04004CE1 RID: 19681
		public static readonly List<DUPLICANTSTATS.TraitVal> STRESSTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "Aggressive",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StressVomiter",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "UglyCrier",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BingeEater",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Banshee",
				dlcId = ""
			}
		};

		// Token: 0x04004CE2 RID: 19682
		public static readonly List<DUPLICANTSTATS.TraitVal> JOYTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "BalloonArtist",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SparkleStreaker",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StickerBomber",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SuperProductive",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "HappySinger",
				dlcId = ""
			}
		};

		// Token: 0x04004CE3 RID: 19683
		public static readonly List<DUPLICANTSTATS.TraitVal> GENESHUFFLERTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "Regeneration",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DeeperDiversLungs",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SunnyDisposition",
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "RockCrusher",
				dlcId = ""
			}
		};

		// Token: 0x04004CE4 RID: 19684
		public static readonly List<DUPLICANTSTATS.TraitVal> SPECIALTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "AncientKnowledge",
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				dlcId = "EXPANSION1_ID",
				doNotGenerateTrait = true,
				mutuallyExclusiveTraits = new List<string>
				{
					"CantResearch", "CantBuild", "CantCook", "CantDig", "Hemophobia", "ScaredyCat", "Anemic", "SlowLearner", "NoodleArms", "ConstructionDown",
					"RanchingDown", "DiggingDown", "MachineryDown", "CookingDown", "ArtDown", "CaringDown", "BotanistDown"
				}
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Chatty",
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				dlcId = "",
				doNotGenerateTrait = true
			}
		};

		// Token: 0x04004CE5 RID: 19685
		public static readonly List<DUPLICANTSTATS.TraitVal> GOODTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "Twinkletoes",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Anemic" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StrongArm",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "NoodleArms" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Greasemonkey",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "MachineryDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DiversLung",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "MouthBreather" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "IronGut",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StrongImmuneSystem",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "WeakImmuneSystem" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "EarlyBird",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "NightOwl" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "NightOwl",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "EarlyBird" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "MoleHands",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "CantDig", "DiggingDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "FastLearner",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "SlowLearner", "CantResearch" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "InteriorDecorator",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Uncultured", "ArtDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Uncultured",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "InteriorDecorator" },
				mutuallyExclusiveAptitudes = new List<HashedString> { "Art" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SimpleTastes",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Foodie" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Foodie",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "SimpleTastes", "CantCook", "CookingDown" },
				mutuallyExclusiveAptitudes = new List<HashedString> { "Cooking" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BedsideManner",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Hemophobia", "CaringDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DecorUp",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "DecorDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Thriver",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GreenThumb",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "BotanistDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "ConstructionUp",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "ConstructionDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "RanchingUp",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "RanchingDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Loner",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "EXPANSION1_ID"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StarryEyed",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				dlcId = "EXPANSION1_ID"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GlowStick",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "EXPANSION1_ID"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "RadiationEater",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "EXPANSION1_ID"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Mining1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "CantDig" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Mining2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "CantDig" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Mining3",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "CantDig" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Farming2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Ranching1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Cooking1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "CantCook" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Arting1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Uncultured" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Arting2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Uncultured" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Arting3",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Uncultured" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Suits1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Technicals2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Engineering1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Basekeeping2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Anemic" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Medicine2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "Hemophobia" }
			}
		};

		// Token: 0x04004CE6 RID: 19686
		public static readonly List<DUPLICANTSTATS.TraitVal> NEEDTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "Claustrophobic",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "PrefersWarmer",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "PrefersColder" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "PrefersColder",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = "",
				mutuallyExclusiveTraits = new List<string> { "PrefersWarmer" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SensitiveFeet",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Fashionable",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Climacophobic",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SolitarySleeper",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				dlcId = ""
			}
		};

		// Token: 0x02001B7E RID: 7038
		public class BASESTATS
		{
			// Token: 0x04007C2C RID: 31788
			public const float STAMINA_USED_PER_SECOND = -0.11666667f;

			// Token: 0x04007C2D RID: 31789
			public const float MAX_CALORIES = 4000000f;

			// Token: 0x04007C2E RID: 31790
			public const float CALORIES_BURNED_PER_CYCLE = -1000000f;

			// Token: 0x04007C2F RID: 31791
			public const float CALORIES_BURNED_PER_SECOND = -1666.6666f;

			// Token: 0x04007C30 RID: 31792
			public const float GUESSTIMATE_CALORIES_PER_CYCLE = -1600000f;

			// Token: 0x04007C31 RID: 31793
			public const float GUESSTIMATE_CALORIES_BURNED_PER_SECOND = -1666.6666f;

			// Token: 0x04007C32 RID: 31794
			public const float OXYGEN_USED_PER_SECOND = 0.1f;

			// Token: 0x04007C33 RID: 31795
			public const float OXYGEN_TO_CO2_CONVERSION = 0.02f;

			// Token: 0x04007C34 RID: 31796
			public const float LOW_OXYGEN_THRESHOLD = 0.52f;

			// Token: 0x04007C35 RID: 31797
			public const float NO_OXYGEN_THRESHOLD = 0.05f;

			// Token: 0x04007C36 RID: 31798
			public const float MIN_CO2_TO_EMIT = 0.02f;

			// Token: 0x04007C37 RID: 31799
			public const float BLADDER_INCREASE_PER_SECOND = 0.16666667f;

			// Token: 0x04007C38 RID: 31800
			public const float DECOR_EXPECTATION = 0f;

			// Token: 0x04007C39 RID: 31801
			public const float FOOD_QUALITY_EXPECTATION = 0f;

			// Token: 0x04007C3A RID: 31802
			public const float RECREATION_EXPECTATION = 2f;

			// Token: 0x04007C3B RID: 31803
			public const float MAX_PROFESSION_DECOR_EXPECTATION = 75f;

			// Token: 0x04007C3C RID: 31804
			public const float MAX_PROFESSION_FOOD_EXPECTATION = 0f;

			// Token: 0x04007C3D RID: 31805
			public const int MAX_UNDERWATER_TRAVEL_COST = 8;

			// Token: 0x04007C3E RID: 31806
			public const float TOILET_EFFICIENCY = 1f;

			// Token: 0x04007C3F RID: 31807
			public const float ROOM_TEMPERATURE_PREFERENCE = 0f;

			// Token: 0x04007C40 RID: 31808
			public const int BUILDING_DAMAGE_ACTING_OUT = 100;

			// Token: 0x04007C41 RID: 31809
			public const float IMMUNE_LEVEL_MAX = 100f;

			// Token: 0x04007C42 RID: 31810
			public const float IMMUNE_LEVEL_RECOVERY = 0.025f;

			// Token: 0x04007C43 RID: 31811
			public const float CARRY_CAPACITY = 200f;

			// Token: 0x04007C44 RID: 31812
			public const float HIT_POINTS = 100f;

			// Token: 0x04007C45 RID: 31813
			public const float RADIATION_RESISTANCE = 0f;
		}

		// Token: 0x02001B7F RID: 7039
		public class RADIATION_DIFFICULTY_MODIFIERS
		{
			// Token: 0x04007C46 RID: 31814
			public static float HARDEST = 0.33f;

			// Token: 0x04007C47 RID: 31815
			public static float HARDER = 0.66f;

			// Token: 0x04007C48 RID: 31816
			public static float DEFAULT = 1f;

			// Token: 0x04007C49 RID: 31817
			public static float EASIER = 2f;

			// Token: 0x04007C4A RID: 31818
			public static float EASIEST = 100f;
		}

		// Token: 0x02001B80 RID: 7040
		public class RADIATION_EXPOSURE_LEVELS
		{
			// Token: 0x04007C4B RID: 31819
			public const float LOW = 100f;

			// Token: 0x04007C4C RID: 31820
			public const float MODERATE = 300f;

			// Token: 0x04007C4D RID: 31821
			public const float HIGH = 600f;

			// Token: 0x04007C4E RID: 31822
			public const float DEADLY = 900f;
		}

		// Token: 0x02001B81 RID: 7041
		public class CALORIES
		{
			// Token: 0x04007C4F RID: 31823
			public const float SATISFIED_THRESHOLD = 0.95f;

			// Token: 0x04007C50 RID: 31824
			public const float HUNGRY_THRESHOLD = 0.825f;

			// Token: 0x04007C51 RID: 31825
			public const float STARVING_THRESHOLD = 0.25f;
		}

		// Token: 0x02001B82 RID: 7042
		public class TEMPERATURE
		{
			// Token: 0x04007C52 RID: 31826
			public const float SKIN_THICKNESS = 0.002f;

			// Token: 0x04007C53 RID: 31827
			public const float SURFACE_AREA = 1f;

			// Token: 0x04007C54 RID: 31828
			public const float GROUND_TRANSFER_SCALE = 0f;

			// Token: 0x0200211A RID: 8474
			public class EXTERNAL
			{
				// Token: 0x0400933C RID: 37692
				public const float THRESHOLD_COLD = 283.15f;

				// Token: 0x0400933D RID: 37693
				public const float THRESHOLD_HOT = 306.15f;

				// Token: 0x0400933E RID: 37694
				public const float THRESHOLD_SCALDING = 345f;
			}

			// Token: 0x0200211B RID: 8475
			public class INTERNAL
			{
				// Token: 0x0400933F RID: 37695
				public const float IDEAL = 310.15f;

				// Token: 0x04009340 RID: 37696
				public const float THRESHOLD_HYPOTHERMIA = 308.15f;

				// Token: 0x04009341 RID: 37697
				public const float THRESHOLD_HYPERTHERMIA = 312.15f;

				// Token: 0x04009342 RID: 37698
				public const float THRESHOLD_FATAL_HOT = 320.15f;

				// Token: 0x04009343 RID: 37699
				public const float THRESHOLD_FATAL_COLD = 300.15f;
			}

			// Token: 0x0200211C RID: 8476
			public class CONDUCTIVITY_BARRIER_MODIFICATION
			{
				// Token: 0x04009344 RID: 37700
				public const float SKINNY = -0.005f;

				// Token: 0x04009345 RID: 37701
				public const float PUDGY = 0.005f;
			}
		}

		// Token: 0x02001B83 RID: 7043
		public class NOISE
		{
			// Token: 0x04007C55 RID: 31829
			public const int THRESHOLD_PEACEFUL = 0;

			// Token: 0x04007C56 RID: 31830
			public const int THRESHOLD_QUIET = 36;

			// Token: 0x04007C57 RID: 31831
			public const int THRESHOLD_TOSS_AND_TURN = 45;

			// Token: 0x04007C58 RID: 31832
			public const int THRESHOLD_WAKE_UP = 60;

			// Token: 0x04007C59 RID: 31833
			public const int THRESHOLD_MINOR_REACTION = 80;

			// Token: 0x04007C5A RID: 31834
			public const int THRESHOLD_MAJOR_REACTION = 106;

			// Token: 0x04007C5B RID: 31835
			public const int THRESHOLD_EXTREME_REACTION = 125;
		}

		// Token: 0x02001B84 RID: 7044
		public class BREATH
		{
			// Token: 0x04007C5C RID: 31836
			private const float BREATH_BAR_TOTAL_SECONDS = 110f;

			// Token: 0x04007C5D RID: 31837
			private const float RETREAT_AT_SECONDS = 80f;

			// Token: 0x04007C5E RID: 31838
			private const float SUFFOCATION_WARN_AT_SECONDS = 50f;

			// Token: 0x04007C5F RID: 31839
			public const float BREATH_BAR_TOTAL_AMOUNT = 100f;

			// Token: 0x04007C60 RID: 31840
			public const float RETREAT_AMOUNT = 72.72727f;

			// Token: 0x04007C61 RID: 31841
			public const float SUFFOCATE_AMOUNT = 45.454548f;

			// Token: 0x04007C62 RID: 31842
			public const float BREATH_RATE = 0.90909094f;
		}

		// Token: 0x02001B85 RID: 7045
		public class LIGHT
		{
			// Token: 0x04007C63 RID: 31843
			public const int LUX_SUNBURN = 72000;

			// Token: 0x04007C64 RID: 31844
			public const float SUNBURN_DELAY_TIME = 120f;

			// Token: 0x04007C65 RID: 31845
			public const int LUX_PLEASANT_LIGHT = 40000;

			// Token: 0x04007C66 RID: 31846
			public const float LIGHT_WORK_EFFICIENCY_BONUS = 0.15f;

			// Token: 0x04007C67 RID: 31847
			public const int NO_LIGHT = 0;

			// Token: 0x04007C68 RID: 31848
			public const int VERY_LOW_LIGHT = 1;

			// Token: 0x04007C69 RID: 31849
			public const int LOW_LIGHT = 100;

			// Token: 0x04007C6A RID: 31850
			public const int MEDIUM_LIGHT = 1000;

			// Token: 0x04007C6B RID: 31851
			public const int HIGH_LIGHT = 10000;

			// Token: 0x04007C6C RID: 31852
			public const int VERY_HIGH_LIGHT = 50000;

			// Token: 0x04007C6D RID: 31853
			public const int MAX_LIGHT = 100000;
		}

		// Token: 0x02001B86 RID: 7046
		public class MOVEMENT
		{
			// Token: 0x04007C6E RID: 31854
			public static float NEUTRAL = 1f;

			// Token: 0x04007C6F RID: 31855
			public static float BONUS_1 = 1.1f;

			// Token: 0x04007C70 RID: 31856
			public static float BONUS_2 = 1.25f;

			// Token: 0x04007C71 RID: 31857
			public static float BONUS_3 = 1.5f;

			// Token: 0x04007C72 RID: 31858
			public static float BONUS_4 = 1.75f;

			// Token: 0x04007C73 RID: 31859
			public static float PENALTY_1 = 0.9f;

			// Token: 0x04007C74 RID: 31860
			public static float PENALTY_2 = 0.75f;

			// Token: 0x04007C75 RID: 31861
			public static float PENALTY_3 = 0.5f;

			// Token: 0x04007C76 RID: 31862
			public static float PENALTY_4 = 0.25f;
		}

		// Token: 0x02001B87 RID: 7047
		public class QOL_STRESS
		{
			// Token: 0x04007C77 RID: 31863
			public const float ABOVE_EXPECTATIONS = -0.016666668f;

			// Token: 0x04007C78 RID: 31864
			public const float AT_EXPECTATIONS = -0.008333334f;

			// Token: 0x04007C79 RID: 31865
			public const float MIN_STRESS = -0.033333335f;

			// Token: 0x0200211D RID: 8477
			public class BELOW_EXPECTATIONS
			{
				// Token: 0x04009346 RID: 37702
				public const float EASY = 0.0033333334f;

				// Token: 0x04009347 RID: 37703
				public const float NEUTRAL = 0.004166667f;

				// Token: 0x04009348 RID: 37704
				public const float HARD = 0.008333334f;

				// Token: 0x04009349 RID: 37705
				public const float VERYHARD = 0.016666668f;
			}

			// Token: 0x0200211E RID: 8478
			public class MAX_STRESS
			{
				// Token: 0x0400934A RID: 37706
				public const float EASY = 0.016666668f;

				// Token: 0x0400934B RID: 37707
				public const float NEUTRAL = 0.041666668f;

				// Token: 0x0400934C RID: 37708
				public const float HARD = 0.05f;

				// Token: 0x0400934D RID: 37709
				public const float VERYHARD = 0.083333336f;
			}
		}

		// Token: 0x02001B88 RID: 7048
		public class COMBAT
		{
			// Token: 0x04007C7A RID: 31866
			public const Health.HealthState FLEE_THRESHOLD = Health.HealthState.Critical;

			// Token: 0x0200211F RID: 8479
			public class BASICWEAPON
			{
				// Token: 0x0400934E RID: 37710
				public const float ATTACKS_PER_SECOND = 2f;

				// Token: 0x0400934F RID: 37711
				public const float MIN_DAMAGE_PER_HIT = 1f;

				// Token: 0x04009350 RID: 37712
				public const float MAX_DAMAGE_PER_HIT = 1f;

				// Token: 0x04009351 RID: 37713
				public const AttackProperties.TargetType TARGET_TYPE = AttackProperties.TargetType.Single;

				// Token: 0x04009352 RID: 37714
				public const AttackProperties.DamageType DAMAGE_TYPE = AttackProperties.DamageType.Standard;

				// Token: 0x04009353 RID: 37715
				public const int MAX_HITS = 1;

				// Token: 0x04009354 RID: 37716
				public const float AREA_OF_EFFECT_RADIUS = 0f;
			}
		}

		// Token: 0x02001B89 RID: 7049
		public class CLOTHING
		{
			// Token: 0x02002120 RID: 8480
			public class DECOR_MODIFICATION
			{
				// Token: 0x04009355 RID: 37717
				public const int NEGATIVE_SIGNIFICANT = -30;

				// Token: 0x04009356 RID: 37718
				public const int NEGATIVE_MILD = -10;

				// Token: 0x04009357 RID: 37719
				public const int BASIC = -5;

				// Token: 0x04009358 RID: 37720
				public const int POSITIVE_MILD = 10;

				// Token: 0x04009359 RID: 37721
				public const int POSITIVE_SIGNIFICANT = 30;

				// Token: 0x0400935A RID: 37722
				public const int POSITIVE_MAJOR = 40;
			}

			// Token: 0x02002121 RID: 8481
			public class CONDUCTIVITY_BARRIER_MODIFICATION
			{
				// Token: 0x0400935B RID: 37723
				public const float THIN = 0.0005f;

				// Token: 0x0400935C RID: 37724
				public const float BASIC = 0.0025f;

				// Token: 0x0400935D RID: 37725
				public const float THICK = 0.01f;
			}

			// Token: 0x02002122 RID: 8482
			public class SWEAT_EFFICIENCY_MULTIPLIER
			{
				// Token: 0x0400935E RID: 37726
				public const float DIMINISH_SIGNIFICANT = -2.5f;

				// Token: 0x0400935F RID: 37727
				public const float DIMINISH_MILD = -1.25f;

				// Token: 0x04009360 RID: 37728
				public const float NEUTRAL = 0f;

				// Token: 0x04009361 RID: 37729
				public const float IMPROVE = 2f;
			}
		}

		// Token: 0x02001B8A RID: 7050
		public class DISTRIBUTIONS
		{
			// Token: 0x06009671 RID: 38513 RVA: 0x003232E3 File Offset: 0x003214E3
			public static int[] GetRandomDistribution()
			{
				return DUPLICANTSTATS.DISTRIBUTIONS.TYPES[UnityEngine.Random.Range(0, DUPLICANTSTATS.DISTRIBUTIONS.TYPES.Count)];
			}

			// Token: 0x04007C7B RID: 31867
			public static readonly List<int[]> TYPES = new List<int[]>
			{
				new int[] { 5, 4, 4, 3, 3, 2, 1 },
				new int[] { 5, 3, 2, 1 },
				new int[] { 5, 2, 2, 1 },
				new int[] { 5, 1 },
				new int[] { 5, 3, 1 },
				new int[] { 3, 3, 3, 3, 1 },
				new int[] { 4 },
				new int[] { 3 },
				new int[] { 2 },
				new int[] { 1 }
			};
		}

		// Token: 0x02001B8B RID: 7051
		public struct TraitVal
		{
			// Token: 0x04007C7C RID: 31868
			public string id;

			// Token: 0x04007C7D RID: 31869
			public int statBonus;

			// Token: 0x04007C7E RID: 31870
			public int impact;

			// Token: 0x04007C7F RID: 31871
			public int rarity;

			// Token: 0x04007C80 RID: 31872
			public string dlcId;

			// Token: 0x04007C81 RID: 31873
			public List<string> mutuallyExclusiveTraits;

			// Token: 0x04007C82 RID: 31874
			public List<HashedString> mutuallyExclusiveAptitudes;

			// Token: 0x04007C83 RID: 31875
			public bool doNotGenerateTrait;
		}

		// Token: 0x02001B8C RID: 7052
		public class ATTRIBUTE_LEVELING
		{
			// Token: 0x04007C84 RID: 31876
			public static int MAX_GAINED_ATTRIBUTE_LEVEL = 20;

			// Token: 0x04007C85 RID: 31877
			public static int TARGET_MAX_LEVEL_CYCLE = 400;

			// Token: 0x04007C86 RID: 31878
			public static float EXPERIENCE_LEVEL_POWER = 1.7f;

			// Token: 0x04007C87 RID: 31879
			public static float FULL_EXPERIENCE = 1f;

			// Token: 0x04007C88 RID: 31880
			public static float ALL_DAY_EXPERIENCE = DUPLICANTSTATS.ATTRIBUTE_LEVELING.FULL_EXPERIENCE / 0.8f;

			// Token: 0x04007C89 RID: 31881
			public static float MOST_DAY_EXPERIENCE = DUPLICANTSTATS.ATTRIBUTE_LEVELING.FULL_EXPERIENCE / 0.5f;

			// Token: 0x04007C8A RID: 31882
			public static float PART_DAY_EXPERIENCE = DUPLICANTSTATS.ATTRIBUTE_LEVELING.FULL_EXPERIENCE / 0.25f;

			// Token: 0x04007C8B RID: 31883
			public static float BARELY_EVER_EXPERIENCE = DUPLICANTSTATS.ATTRIBUTE_LEVELING.FULL_EXPERIENCE / 0.1f;
		}

		// Token: 0x02001B8D RID: 7053
		public class ROOM
		{
			// Token: 0x04007C8C RID: 31884
			public const float LABORATORY_RESEARCH_EFFICIENCY_BONUS = 0.1f;
		}
	}
}
