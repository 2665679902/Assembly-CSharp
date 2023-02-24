using System;

namespace TUNING
{
	// Token: 0x02000D30 RID: 3376
	public class EQUIPMENT
	{
		// Token: 0x02001BA2 RID: 7074
		public class TOYS
		{
			// Token: 0x04007D2E RID: 32046
			public static string SLOT = "Toy";

			// Token: 0x04007D2F RID: 32047
			public static float BALLOON_MASS = 1f;
		}

		// Token: 0x02001BA3 RID: 7075
		public class ATTRIBUTE_MOD_IDS
		{
			// Token: 0x04007D30 RID: 32048
			public static string DECOR = "Decor";

			// Token: 0x04007D31 RID: 32049
			public static string INSULATION = "Insulation";

			// Token: 0x04007D32 RID: 32050
			public static string ATHLETICS = "Athletics";

			// Token: 0x04007D33 RID: 32051
			public static string DIGGING = "Digging";

			// Token: 0x04007D34 RID: 32052
			public static string MAX_UNDERWATER_TRAVELCOST = "MaxUnderwaterTravelCost";

			// Token: 0x04007D35 RID: 32053
			public static string THERMAL_CONDUCTIVITY_BARRIER = "ThermalConductivityBarrier";
		}

		// Token: 0x02001BA4 RID: 7076
		public class TOOLS
		{
			// Token: 0x04007D36 RID: 32054
			public static string TOOLSLOT = "Multitool";

			// Token: 0x04007D37 RID: 32055
			public static string TOOLFABRICATOR = "MultitoolWorkbench";

			// Token: 0x04007D38 RID: 32056
			public static string TOOL_ANIM = "constructor_gun_kanim";
		}

		// Token: 0x02001BA5 RID: 7077
		public class CLOTHING
		{
			// Token: 0x04007D39 RID: 32057
			public static string SLOT = "Outfit";
		}

		// Token: 0x02001BA6 RID: 7078
		public class SUITS
		{
			// Token: 0x04007D3A RID: 32058
			public static string SLOT = "Suit";

			// Token: 0x04007D3B RID: 32059
			public static string FABRICATOR = "SuitFabricator";

			// Token: 0x04007D3C RID: 32060
			public static string ANIM = "clothing_kanim";

			// Token: 0x04007D3D RID: 32061
			public static string SNAPON = "snapTo_neck";

			// Token: 0x04007D3E RID: 32062
			public static float SUIT_DURABILITY_SKILL_BONUS = 0.25f;

			// Token: 0x04007D3F RID: 32063
			public static int OXYMASK_FABTIME = 20;

			// Token: 0x04007D40 RID: 32064
			public static int ATMOSUIT_FABTIME = 40;

			// Token: 0x04007D41 RID: 32065
			public static int ATMOSUIT_INSULATION = 50;

			// Token: 0x04007D42 RID: 32066
			public static int ATMOSUIT_ATHLETICS = -6;

			// Token: 0x04007D43 RID: 32067
			public static float ATMOSUIT_THERMAL_CONDUCTIVITY_BARRIER = 0.2f;

			// Token: 0x04007D44 RID: 32068
			public static int ATMOSUIT_DIGGING = 10;

			// Token: 0x04007D45 RID: 32069
			public static int ATMOSUIT_CONSTRUCTION = 10;

			// Token: 0x04007D46 RID: 32070
			public static float ATMOSUIT_BLADDER = -0.18333334f;

			// Token: 0x04007D47 RID: 32071
			public static int ATMOSUIT_MASS = 200;

			// Token: 0x04007D48 RID: 32072
			public static int ATMOSUIT_SCALDING = 1000;

			// Token: 0x04007D49 RID: 32073
			public static float ATMOSUIT_DECAY = -0.1f;

			// Token: 0x04007D4A RID: 32074
			public static float LEADSUIT_THERMAL_CONDUCTIVITY_BARRIER = 0.3f;

			// Token: 0x04007D4B RID: 32075
			public static int LEADSUIT_SCALDING = 1000;

			// Token: 0x04007D4C RID: 32076
			public static int LEADSUIT_INSULATION = 50;

			// Token: 0x04007D4D RID: 32077
			public static int LEADSUIT_STRENGTH = 10;

			// Token: 0x04007D4E RID: 32078
			public static int LEADSUIT_ATHLETICS = -8;

			// Token: 0x04007D4F RID: 32079
			public static float LEADSUIT_RADIATION_SHIELDING = 0.66f;

			// Token: 0x04007D50 RID: 32080
			public static int AQUASUIT_FABTIME = EQUIPMENT.SUITS.ATMOSUIT_FABTIME;

			// Token: 0x04007D51 RID: 32081
			public static int AQUASUIT_INSULATION = 0;

			// Token: 0x04007D52 RID: 32082
			public static int AQUASUIT_ATHLETICS = EQUIPMENT.SUITS.ATMOSUIT_ATHLETICS;

			// Token: 0x04007D53 RID: 32083
			public static int AQUASUIT_MASS = EQUIPMENT.SUITS.ATMOSUIT_MASS;

			// Token: 0x04007D54 RID: 32084
			public static int AQUASUIT_UNDERWATER_TRAVELCOST = 6;

			// Token: 0x04007D55 RID: 32085
			public static int TEMPERATURESUIT_FABTIME = EQUIPMENT.SUITS.ATMOSUIT_FABTIME;

			// Token: 0x04007D56 RID: 32086
			public static float TEMPERATURESUIT_INSULATION = 0.2f;

			// Token: 0x04007D57 RID: 32087
			public static int TEMPERATURESUIT_ATHLETICS = EQUIPMENT.SUITS.ATMOSUIT_ATHLETICS;

			// Token: 0x04007D58 RID: 32088
			public static int TEMPERATURESUIT_MASS = EQUIPMENT.SUITS.ATMOSUIT_MASS;

			// Token: 0x04007D59 RID: 32089
			public const int OXYGEN_MASK_MASS = 15;

			// Token: 0x04007D5A RID: 32090
			public static int OXYGEN_MASK_ATHLETICS = -2;

			// Token: 0x04007D5B RID: 32091
			public static float OXYGEN_MASK_DECAY = -0.2f;

			// Token: 0x04007D5C RID: 32092
			public static float INDESTRUCTIBLE_DURABILITY_MOD = 0f;

			// Token: 0x04007D5D RID: 32093
			public static float REINFORCED_DURABILITY_MOD = 0.5f;

			// Token: 0x04007D5E RID: 32094
			public static float FLIMSY_DURABILITY_MOD = 1.5f;

			// Token: 0x04007D5F RID: 32095
			public static float THREADBARE_DURABILITY_MOD = 2f;

			// Token: 0x04007D60 RID: 32096
			public static float MINIMUM_USABLE_SUIT_CHARGE = 0.95f;
		}

		// Token: 0x02001BA7 RID: 7079
		public class VESTS
		{
			// Token: 0x04007D61 RID: 32097
			public static string SLOT = "Suit";

			// Token: 0x04007D62 RID: 32098
			public static string FABRICATOR = "ClothingFabricator";

			// Token: 0x04007D63 RID: 32099
			public static string SNAPON0 = "snapTo_body";

			// Token: 0x04007D64 RID: 32100
			public static string SNAPON1 = "snapTo_arm";

			// Token: 0x04007D65 RID: 32101
			public static string WARM_VEST_ANIM0 = "body_shirt_hot01_kanim";

			// Token: 0x04007D66 RID: 32102
			public static string WARM_VEST_ANIM1 = "body_shirt_hot02_kanim";

			// Token: 0x04007D67 RID: 32103
			public static string WARM_VEST_ICON0 = "shirt_hot01_kanim";

			// Token: 0x04007D68 RID: 32104
			public static string WARM_VEST_ICON1 = "shirt_hot02_kanim";

			// Token: 0x04007D69 RID: 32105
			public static float WARM_VEST_FABTIME = 180f;

			// Token: 0x04007D6A RID: 32106
			public static float WARM_VEST_INSULATION = 0.01f;

			// Token: 0x04007D6B RID: 32107
			public static int WARM_VEST_MASS = 4;

			// Token: 0x04007D6C RID: 32108
			public static string COOL_VEST_ANIM0 = "body_shirt_cold01_kanim";

			// Token: 0x04007D6D RID: 32109
			public static string COOL_VEST_ANIM1 = "body_shirt_cold02_kanim";

			// Token: 0x04007D6E RID: 32110
			public static string COOL_VEST_ICON0 = "shirt_cold01_kanim";

			// Token: 0x04007D6F RID: 32111
			public static string COOL_VEST_ICON1 = "shirt_cold02_kanim";

			// Token: 0x04007D70 RID: 32112
			public static float COOL_VEST_FABTIME = EQUIPMENT.VESTS.WARM_VEST_FABTIME;

			// Token: 0x04007D71 RID: 32113
			public static float COOL_VEST_INSULATION = 0.01f;

			// Token: 0x04007D72 RID: 32114
			public static int COOL_VEST_MASS = EQUIPMENT.VESTS.WARM_VEST_MASS;

			// Token: 0x04007D73 RID: 32115
			public static float FUNKY_VEST_FABTIME = EQUIPMENT.VESTS.WARM_VEST_FABTIME;

			// Token: 0x04007D74 RID: 32116
			public static float FUNKY_VEST_DECOR = 1f;

			// Token: 0x04007D75 RID: 32117
			public static int FUNKY_VEST_MASS = EQUIPMENT.VESTS.WARM_VEST_MASS;

			// Token: 0x04007D76 RID: 32118
			public static float CUSTOM_CLOTHING_FABTIME = 180f;

			// Token: 0x04007D77 RID: 32119
			public static float CUSTOM_ATMOSUIT_FABTIME = 15f;

			// Token: 0x04007D78 RID: 32120
			public static int CUSTOM_CLOTHING_MASS = EQUIPMENT.VESTS.WARM_VEST_MASS + 3;
		}
	}
}
