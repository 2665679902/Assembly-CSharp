using System;
using System.Collections.Generic;

namespace TUNING
{
	// Token: 0x02000D28 RID: 3368
	public class FOOD
	{
		// Token: 0x04004D05 RID: 19717
		public const float EATING_SECONDS_PER_CALORIE = 2E-05f;

		// Token: 0x04004D06 RID: 19718
		public const float FOOD_CALORIES_PER_CYCLE = 1000000f;

		// Token: 0x04004D07 RID: 19719
		public const int FOOD_AMOUNT_INGREDIENT_ONLY = 0;

		// Token: 0x04004D08 RID: 19720
		public const float KCAL_SMALL_PORTION = 600000f;

		// Token: 0x04004D09 RID: 19721
		public const float KCAL_BONUS_COOKING_LOW = 250000f;

		// Token: 0x04004D0A RID: 19722
		public const float KCAL_BASIC_PORTION = 800000f;

		// Token: 0x04004D0B RID: 19723
		public const float KCAL_PREPARED_FOOD = 4000000f;

		// Token: 0x04004D0C RID: 19724
		public const float KCAL_BONUS_COOKING_BASIC = 400000f;

		// Token: 0x04004D0D RID: 19725
		public const float DEFAULT_PRESERVE_TEMPERATURE = 255.15f;

		// Token: 0x04004D0E RID: 19726
		public const float DEFAULT_ROT_TEMPERATURE = 277.15f;

		// Token: 0x04004D0F RID: 19727
		public const float HIGH_PRESERVE_TEMPERATURE = 283.15f;

		// Token: 0x04004D10 RID: 19728
		public const float HIGH_ROT_TEMPERATURE = 308.15f;

		// Token: 0x04004D11 RID: 19729
		public const float EGG_COOK_TEMPERATURE = 344.15f;

		// Token: 0x04004D12 RID: 19730
		public const float DEFAULT_MASS = 1f;

		// Token: 0x04004D13 RID: 19731
		public const float DEFAULT_SPICE_MASS = 1f;

		// Token: 0x04004D14 RID: 19732
		public const float ROT_TO_ELEMENT_TIME = 600f;

		// Token: 0x04004D15 RID: 19733
		public const int MUSH_BAR_SPAWN_GERMS = 1000;

		// Token: 0x04004D16 RID: 19734
		public const float IDEAL_TEMPERATURE_TOLERANCE = 10f;

		// Token: 0x04004D17 RID: 19735
		public const int FOOD_QUALITY_AWFUL = -1;

		// Token: 0x04004D18 RID: 19736
		public const int FOOD_QUALITY_TERRIBLE = 0;

		// Token: 0x04004D19 RID: 19737
		public const int FOOD_QUALITY_MEDIOCRE = 1;

		// Token: 0x04004D1A RID: 19738
		public const int FOOD_QUALITY_GOOD = 2;

		// Token: 0x04004D1B RID: 19739
		public const int FOOD_QUALITY_GREAT = 3;

		// Token: 0x04004D1C RID: 19740
		public const int FOOD_QUALITY_AMAZING = 4;

		// Token: 0x04004D1D RID: 19741
		public const int FOOD_QUALITY_WONDERFUL = 5;

		// Token: 0x04004D1E RID: 19742
		public const int FOOD_QUALITY_MORE_WONDERFUL = 6;

		// Token: 0x02001B93 RID: 7059
		public class SPOIL_TIME
		{
			// Token: 0x04007CB1 RID: 31921
			public const float DEFAULT = 4800f;

			// Token: 0x04007CB2 RID: 31922
			public const float QUICK = 2400f;

			// Token: 0x04007CB3 RID: 31923
			public const float SLOW = 9600f;

			// Token: 0x04007CB4 RID: 31924
			public const float VERYSLOW = 19200f;
		}

		// Token: 0x02001B94 RID: 7060
		public class FOOD_TYPES
		{
			// Token: 0x04007CB5 RID: 31925
			public static readonly EdiblesManager.FoodInfo FIELDRATION = new EdiblesManager.FoodInfo("FieldRation", "", 800000f, -1, 255.15f, 277.15f, 19200f, false);

			// Token: 0x04007CB6 RID: 31926
			public static readonly EdiblesManager.FoodInfo MUSHBAR = new EdiblesManager.FoodInfo("MushBar", "", 800000f, -1, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CB7 RID: 31927
			public static readonly EdiblesManager.FoodInfo BASICPLANTFOOD = new EdiblesManager.FoodInfo("BasicPlantFood", "", 600000f, -1, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CB8 RID: 31928
			public static readonly EdiblesManager.FoodInfo BASICFORAGEPLANT = new EdiblesManager.FoodInfo("BasicForagePlant", "", 800000f, -1, 255.15f, 277.15f, 4800f, false);

			// Token: 0x04007CB9 RID: 31929
			public static readonly EdiblesManager.FoodInfo FORESTFORAGEPLANT = new EdiblesManager.FoodInfo("ForestForagePlant", "", 6400000f, -1, 255.15f, 277.15f, 4800f, false);

			// Token: 0x04007CBA RID: 31930
			public static readonly EdiblesManager.FoodInfo SWAMPFORAGEPLANT = new EdiblesManager.FoodInfo("SwampForagePlant", "EXPANSION1_ID", 2400000f, -1, 255.15f, 277.15f, 4800f, false);

			// Token: 0x04007CBB RID: 31931
			public static readonly EdiblesManager.FoodInfo MUSHROOM = new EdiblesManager.FoodInfo(MushroomConfig.ID, "", 2400000f, 0, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CBC RID: 31932
			public static readonly EdiblesManager.FoodInfo LETTUCE = new EdiblesManager.FoodInfo("Lettuce", "", 400000f, 0, 255.15f, 277.15f, 2400f, true).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.AVAILABLE_EXPANSION1_ONLY);

			// Token: 0x04007CBD RID: 31933
			public static readonly EdiblesManager.FoodInfo MEAT = new EdiblesManager.FoodInfo("Meat", "", 1600000f, -1, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CBE RID: 31934
			public static readonly EdiblesManager.FoodInfo PLANTMEAT = new EdiblesManager.FoodInfo("PlantMeat", "EXPANSION1_ID", 1200000f, 1, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CBF RID: 31935
			public static readonly EdiblesManager.FoodInfo PRICKLEFRUIT = new EdiblesManager.FoodInfo(PrickleFruitConfig.ID, "", 1600000f, 0, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CC0 RID: 31936
			public static readonly EdiblesManager.FoodInfo SWAMPFRUIT = new EdiblesManager.FoodInfo(SwampFruitConfig.ID, "EXPANSION1_ID", 1840000f, 0, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CC1 RID: 31937
			public static readonly EdiblesManager.FoodInfo FISH_MEAT = new EdiblesManager.FoodInfo("FishMeat", "", 1000000f, 2, 255.15f, 277.15f, 2400f, true).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.AVAILABLE_EXPANSION1_ONLY);

			// Token: 0x04007CC2 RID: 31938
			public static readonly EdiblesManager.FoodInfo SHELLFISH_MEAT = new EdiblesManager.FoodInfo("ShellfishMeat", "", 1000000f, 2, 255.15f, 277.15f, 2400f, true).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.AVAILABLE_EXPANSION1_ONLY);

			// Token: 0x04007CC3 RID: 31939
			public static readonly EdiblesManager.FoodInfo WORMBASICFRUIT = new EdiblesManager.FoodInfo("WormBasicFruit", "EXPANSION1_ID", 800000f, 0, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CC4 RID: 31940
			public static readonly EdiblesManager.FoodInfo WORMSUPERFRUIT = new EdiblesManager.FoodInfo("WormSuperFruit", "EXPANSION1_ID", 250000f, 1, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CC5 RID: 31941
			public static readonly EdiblesManager.FoodInfo PICKLEDMEAL = new EdiblesManager.FoodInfo("PickledMeal", "", 1800000f, -1, 255.15f, 277.15f, 19200f, true);

			// Token: 0x04007CC6 RID: 31942
			public static readonly EdiblesManager.FoodInfo BASICPLANTBAR = new EdiblesManager.FoodInfo("BasicPlantBar", "", 1700000f, 0, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CC7 RID: 31943
			public static readonly EdiblesManager.FoodInfo FRIEDMUSHBAR = new EdiblesManager.FoodInfo("FriedMushBar", "", 1050000f, 0, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CC8 RID: 31944
			public static readonly EdiblesManager.FoodInfo GAMMAMUSH = new EdiblesManager.FoodInfo("GammaMush", "", 1050000f, 1, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CC9 RID: 31945
			public static readonly EdiblesManager.FoodInfo GRILLED_PRICKLEFRUIT = new EdiblesManager.FoodInfo("GrilledPrickleFruit", "", 2000000f, 1, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CCA RID: 31946
			public static readonly EdiblesManager.FoodInfo SWAMP_DELIGHTS = new EdiblesManager.FoodInfo("SwampDelights", "EXPANSION1_ID", 2240000f, 1, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CCB RID: 31947
			public static readonly EdiblesManager.FoodInfo FRIED_MUSHROOM = new EdiblesManager.FoodInfo("FriedMushroom", "", 2800000f, 1, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CCC RID: 31948
			public static readonly EdiblesManager.FoodInfo COLD_WHEAT_BREAD = new EdiblesManager.FoodInfo("ColdWheatBread", "", 1200000f, 2, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CCD RID: 31949
			public static readonly EdiblesManager.FoodInfo COOKED_EGG = new EdiblesManager.FoodInfo("CookedEgg", "", 2800000f, 2, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CCE RID: 31950
			public static readonly EdiblesManager.FoodInfo COOKED_FISH = new EdiblesManager.FoodInfo("CookedFish", "", 1600000f, 3, 255.15f, 277.15f, 2400f, true).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.AVAILABLE_EXPANSION1_ONLY);

			// Token: 0x04007CCF RID: 31951
			public static readonly EdiblesManager.FoodInfo COOKED_MEAT = new EdiblesManager.FoodInfo("CookedMeat", "", 4000000f, 3, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CD0 RID: 31952
			public static readonly EdiblesManager.FoodInfo WORMBASICFOOD = new EdiblesManager.FoodInfo("WormBasicFood", "EXPANSION1_ID", 1200000f, 1, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CD1 RID: 31953
			public static readonly EdiblesManager.FoodInfo WORMSUPERFOOD = new EdiblesManager.FoodInfo("WormSuperFood", "EXPANSION1_ID", 2400000f, 3, 255.15f, 277.15f, 19200f, true);

			// Token: 0x04007CD2 RID: 31954
			public static readonly EdiblesManager.FoodInfo FRUITCAKE = new EdiblesManager.FoodInfo("FruitCake", "", 4000000f, 3, 255.15f, 277.15f, 19200f, false);

			// Token: 0x04007CD3 RID: 31955
			public static readonly EdiblesManager.FoodInfo SALSA = new EdiblesManager.FoodInfo("Salsa", "", 4400000f, 4, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CD4 RID: 31956
			public static readonly EdiblesManager.FoodInfo SURF_AND_TURF = new EdiblesManager.FoodInfo("SurfAndTurf", "", 6000000f, 4, 255.15f, 277.15f, 2400f, true).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.AVAILABLE_EXPANSION1_ONLY);

			// Token: 0x04007CD5 RID: 31957
			public static readonly EdiblesManager.FoodInfo MUSHROOM_WRAP = new EdiblesManager.FoodInfo("MushroomWrap", "", 4800000f, 4, 255.15f, 277.15f, 2400f, true).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.AVAILABLE_EXPANSION1_ONLY);

			// Token: 0x04007CD6 RID: 31958
			public static readonly EdiblesManager.FoodInfo TOFU = new EdiblesManager.FoodInfo("Tofu", "", 3600000f, 2, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CD7 RID: 31959
			public static readonly EdiblesManager.FoodInfo SPICEBREAD = new EdiblesManager.FoodInfo("SpiceBread", "", 4000000f, 5, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CD8 RID: 31960
			public static readonly EdiblesManager.FoodInfo SPICY_TOFU = new EdiblesManager.FoodInfo("SpicyTofu", "", 4000000f, 5, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CD9 RID: 31961
			public static readonly EdiblesManager.FoodInfo CURRY = new EdiblesManager.FoodInfo("Curry", "", 5000000f, 4, 255.15f, 277.15f, 9600f, true).AddEffects(new List<string> { "HotStuff" }, DlcManager.AVAILABLE_ALL_VERSIONS);

			// Token: 0x04007CDA RID: 31962
			public static readonly EdiblesManager.FoodInfo BERRY_PIE = new EdiblesManager.FoodInfo("BerryPie", "EXPANSION1_ID", 4200000f, 5, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CDB RID: 31963
			public static readonly EdiblesManager.FoodInfo BURGER = new EdiblesManager.FoodInfo("Burger", "", 6000000f, 6, 255.15f, 277.15f, 2400f, true).AddEffects(new List<string> { "GoodEats" }, DlcManager.AVAILABLE_ALL_VERSIONS).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.AVAILABLE_EXPANSION1_ONLY);

			// Token: 0x04007CDC RID: 31964
			public static readonly EdiblesManager.FoodInfo BEAN = new EdiblesManager.FoodInfo("BeanPlantSeed", "", 0f, 3, 255.15f, 277.15f, 4800f, true);

			// Token: 0x04007CDD RID: 31965
			public static readonly EdiblesManager.FoodInfo SPICENUT = new EdiblesManager.FoodInfo(SpiceNutConfig.ID, "", 0f, 0, 255.15f, 277.15f, 2400f, true);

			// Token: 0x04007CDE RID: 31966
			public static readonly EdiblesManager.FoodInfo COLD_WHEAT_SEED = new EdiblesManager.FoodInfo("ColdWheatSeed", "", 0f, 0, 283.15f, 308.15f, 9600f, true);

			// Token: 0x04007CDF RID: 31967
			public static readonly EdiblesManager.FoodInfo RAWEGG = new EdiblesManager.FoodInfo("RawEgg", "", 0f, -1, 255.15f, 277.15f, 4800f, true);
		}

		// Token: 0x02001B95 RID: 7061
		public class RECIPES
		{
			// Token: 0x04007CE0 RID: 31968
			public static float SMALL_COOK_TIME = 30f;

			// Token: 0x04007CE1 RID: 31969
			public static float STANDARD_COOK_TIME = 50f;
		}
	}
}
