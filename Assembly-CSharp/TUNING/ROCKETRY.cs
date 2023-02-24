using System;
using UnityEngine;

namespace TUNING
{
	// Token: 0x02000D33 RID: 3379
	public class ROCKETRY
	{
		// Token: 0x0600680D RID: 26637 RVA: 0x002893DC File Offset: 0x002875DC
		public static float MassFromPenaltyPercentage(float penaltyPercentage = 0.5f)
		{
			return -(1f / Mathf.Pow(penaltyPercentage - 1f, 5f));
		}

		// Token: 0x0600680E RID: 26638 RVA: 0x002893F8 File Offset: 0x002875F8
		public static float CalculateMassWithPenalty(float realMass)
		{
			float num = Mathf.Pow(realMass / ROCKETRY.MASS_PENALTY_DIVISOR, ROCKETRY.MASS_PENALTY_EXPONENT);
			return Mathf.Max(realMass, num);
		}

		// Token: 0x04004D96 RID: 19862
		public static float MISSION_DURATION_SCALE = 1800f;

		// Token: 0x04004D97 RID: 19863
		public static float MASS_PENALTY_EXPONENT = 3.2f;

		// Token: 0x04004D98 RID: 19864
		public static float MASS_PENALTY_DIVISOR = 300f;

		// Token: 0x04004D99 RID: 19865
		public const float SELF_DESTRUCT_REFUND_FACTOR = 0.5f;

		// Token: 0x04004D9A RID: 19866
		public static float CARGO_CAPACITY_SCALE = 10f;

		// Token: 0x04004D9B RID: 19867
		public static float LIQUID_CARGO_BAY_CLUSTER_CAPACITY = 2700f;

		// Token: 0x04004D9C RID: 19868
		public static float SOLID_CARGO_BAY_CLUSTER_CAPACITY = 2700f;

		// Token: 0x04004D9D RID: 19869
		public static float GAS_CARGO_BAY_CLUSTER_CAPACITY = 1100f;

		// Token: 0x04004D9E RID: 19870
		public static Vector2I ROCKET_INTERIOR_SIZE = new Vector2I(32, 32);

		// Token: 0x02001BAA RID: 7082
		public class DESTINATION_RESEARCH
		{
			// Token: 0x04007D82 RID: 32130
			public static int EVERGREEN = 10;

			// Token: 0x04007D83 RID: 32131
			public static int BASIC = 50;

			// Token: 0x04007D84 RID: 32132
			public static int HIGH = 150;
		}

		// Token: 0x02001BAB RID: 7083
		public class DESTINATION_ANALYSIS
		{
			// Token: 0x04007D85 RID: 32133
			public static int DISCOVERED = 50;

			// Token: 0x04007D86 RID: 32134
			public static int COMPLETE = 100;

			// Token: 0x04007D87 RID: 32135
			public static float DEFAULT_CYCLES_PER_DISCOVERY = 0.5f;
		}

		// Token: 0x02001BAC RID: 7084
		public class DESTINATION_THRUST_COSTS
		{
			// Token: 0x04007D88 RID: 32136
			public static int LOW = 3;

			// Token: 0x04007D89 RID: 32137
			public static int MID = 5;

			// Token: 0x04007D8A RID: 32138
			public static int HIGH = 7;

			// Token: 0x04007D8B RID: 32139
			public static int VERY_HIGH = 9;
		}

		// Token: 0x02001BAD RID: 7085
		public class CLUSTER_FOW
		{
			// Token: 0x04007D8C RID: 32140
			public static float POINTS_TO_REVEAL = 100f;

			// Token: 0x04007D8D RID: 32141
			public static float DEFAULT_CYCLES_PER_REVEAL = 0.5f;
		}

		// Token: 0x02001BAE RID: 7086
		public class ENGINE_EFFICIENCY
		{
			// Token: 0x04007D8E RID: 32142
			public static float WEAK = 20f;

			// Token: 0x04007D8F RID: 32143
			public static float MEDIUM = 40f;

			// Token: 0x04007D90 RID: 32144
			public static float STRONG = 60f;

			// Token: 0x04007D91 RID: 32145
			public static float BOOSTER = 30f;
		}

		// Token: 0x02001BAF RID: 7087
		public class ROCKET_HEIGHT
		{
			// Token: 0x04007D92 RID: 32146
			public static int VERY_SHORT = 10;

			// Token: 0x04007D93 RID: 32147
			public static int SHORT = 16;

			// Token: 0x04007D94 RID: 32148
			public static int MEDIUM = 20;

			// Token: 0x04007D95 RID: 32149
			public static int TALL = 25;

			// Token: 0x04007D96 RID: 32150
			public static int VERY_TALL = 35;

			// Token: 0x04007D97 RID: 32151
			public static int MAX_MODULE_STACK_HEIGHT = ROCKETRY.ROCKET_HEIGHT.VERY_TALL - 5;
		}

		// Token: 0x02001BB0 RID: 7088
		public class OXIDIZER_EFFICIENCY
		{
			// Token: 0x04007D98 RID: 32152
			public static float VERY_LOW = 0.334f;

			// Token: 0x04007D99 RID: 32153
			public static float LOW = 1f;

			// Token: 0x04007D9A RID: 32154
			public static float HIGH = 1.33f;
		}

		// Token: 0x02001BB1 RID: 7089
		public class DLC1_OXIDIZER_EFFICIENCY
		{
			// Token: 0x04007D9B RID: 32155
			public static float VERY_LOW = 1f;

			// Token: 0x04007D9C RID: 32156
			public static float LOW = 2f;

			// Token: 0x04007D9D RID: 32157
			public static float HIGH = 4f;
		}

		// Token: 0x02001BB2 RID: 7090
		public class CARGO_CONTAINER_MASS
		{
			// Token: 0x04007D9E RID: 32158
			public static float STATIC_MASS = 1000f;

			// Token: 0x04007D9F RID: 32159
			public static float PAYLOAD_MASS = 1000f;
		}

		// Token: 0x02001BB3 RID: 7091
		public class BURDEN
		{
			// Token: 0x04007DA0 RID: 32160
			public static int INSIGNIFICANT = 1;

			// Token: 0x04007DA1 RID: 32161
			public static int MINOR = 2;

			// Token: 0x04007DA2 RID: 32162
			public static int MINOR_PLUS = 3;

			// Token: 0x04007DA3 RID: 32163
			public static int MODERATE = 4;

			// Token: 0x04007DA4 RID: 32164
			public static int MODERATE_PLUS = 5;

			// Token: 0x04007DA5 RID: 32165
			public static int MAJOR = 6;

			// Token: 0x04007DA6 RID: 32166
			public static int MAJOR_PLUS = 7;

			// Token: 0x04007DA7 RID: 32167
			public static int MEGA = 9;

			// Token: 0x04007DA8 RID: 32168
			public static int MONUMENTAL = 15;
		}

		// Token: 0x02001BB4 RID: 7092
		public class ENGINE_POWER
		{
			// Token: 0x04007DA9 RID: 32169
			public static int EARLY_WEAK = 16;

			// Token: 0x04007DAA RID: 32170
			public static int EARLY_STRONG = 23;

			// Token: 0x04007DAB RID: 32171
			public static int MID_VERY_STRONG = 48;

			// Token: 0x04007DAC RID: 32172
			public static int MID_STRONG = 31;

			// Token: 0x04007DAD RID: 32173
			public static int MID_WEAK = 27;

			// Token: 0x04007DAE RID: 32174
			public static int LATE_STRONG = 34;

			// Token: 0x04007DAF RID: 32175
			public static int LATE_VERY_STRONG = 55;
		}

		// Token: 0x02001BB5 RID: 7093
		public class FUEL_COST_PER_DISTANCE
		{
			// Token: 0x04007DB0 RID: 32176
			public static float VERY_LOW = 0.033333335f;

			// Token: 0x04007DB1 RID: 32177
			public static float LOW = 0.0375f;

			// Token: 0x04007DB2 RID: 32178
			public static float MEDIUM = 0.075f;

			// Token: 0x04007DB3 RID: 32179
			public static float HIGH = 0.09375f;

			// Token: 0x04007DB4 RID: 32180
			public static float VERY_HIGH = 0.15f;

			// Token: 0x04007DB5 RID: 32181
			public static float GAS_VERY_LOW = 0.025f;

			// Token: 0x04007DB6 RID: 32182
			public static float GAS_LOW = 0.027777778f;

			// Token: 0x04007DB7 RID: 32183
			public static float GAS_HIGH = 0.041666668f;

			// Token: 0x04007DB8 RID: 32184
			public static float PARTICLES = 0.33333334f;
		}
	}
}
