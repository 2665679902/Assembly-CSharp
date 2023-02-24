using System;

namespace TUNING
{
	// Token: 0x02000D25 RID: 3365
	public class DISEASE
	{
		// Token: 0x04004CE7 RID: 19687
		public const int COUNT_SCALER = 1000;

		// Token: 0x04004CE8 RID: 19688
		public const int GENERIC_EMIT_COUNT = 100000;

		// Token: 0x04004CE9 RID: 19689
		public const float GENERIC_EMIT_INTERVAL = 5f;

		// Token: 0x04004CEA RID: 19690
		public const float GENERIC_INFECTION_RADIUS = 1.5f;

		// Token: 0x04004CEB RID: 19691
		public const float GENERIC_INFECTION_INTERVAL = 5f;

		// Token: 0x04004CEC RID: 19692
		public const float STINKY_EMIT_MASS = 0.0025000002f;

		// Token: 0x04004CED RID: 19693
		public const float STINKY_EMIT_INTERVAL = 2.5f;

		// Token: 0x04004CEE RID: 19694
		public const float STORAGE_TRANSFER_RATE = 0.05f;

		// Token: 0x04004CEF RID: 19695
		public const float WORKABLE_TRANSFER_RATE = 0.33f;

		// Token: 0x04004CF0 RID: 19696
		public const float LADDER_TRANSFER_RATE = 0.005f;

		// Token: 0x04004CF1 RID: 19697
		public const float INTERNAL_GERM_DEATH_MULTIPLIER = -0.00066666666f;

		// Token: 0x04004CF2 RID: 19698
		public const float INTERNAL_GERM_DEATH_ADDEND = -0.8333333f;

		// Token: 0x04004CF3 RID: 19699
		public const float MINIMUM_IMMUNE_DAMAGE = 0.00016666666f;

		// Token: 0x02001B8E RID: 7054
		public class DURATION
		{
			// Token: 0x04007C8D RID: 31885
			public const float LONG = 10800f;

			// Token: 0x04007C8E RID: 31886
			public const float LONGISH = 4620f;

			// Token: 0x04007C8F RID: 31887
			public const float NORMAL = 2220f;

			// Token: 0x04007C90 RID: 31888
			public const float SHORT = 1020f;

			// Token: 0x04007C91 RID: 31889
			public const float TEMPORARY = 180f;

			// Token: 0x04007C92 RID: 31890
			public const float VERY_BRIEF = 60f;
		}

		// Token: 0x02001B8F RID: 7055
		public class IMMUNE_ATTACK_STRENGTH_PERCENT
		{
			// Token: 0x04007C93 RID: 31891
			public const float SLOW_3 = 0.00025f;

			// Token: 0x04007C94 RID: 31892
			public const float SLOW_2 = 0.0005f;

			// Token: 0x04007C95 RID: 31893
			public const float SLOW_1 = 0.00125f;

			// Token: 0x04007C96 RID: 31894
			public const float NORMAL = 0.005f;

			// Token: 0x04007C97 RID: 31895
			public const float FAST_1 = 0.0125f;

			// Token: 0x04007C98 RID: 31896
			public const float FAST_2 = 0.05f;

			// Token: 0x04007C99 RID: 31897
			public const float FAST_3 = 0.125f;
		}

		// Token: 0x02001B90 RID: 7056
		public class RADIATION_KILL_RATE
		{
			// Token: 0x04007C9A RID: 31898
			public const float NO_EFFECT = 0f;

			// Token: 0x04007C9B RID: 31899
			public const float SLOW = 1f;

			// Token: 0x04007C9C RID: 31900
			public const float NORMAL = 2.5f;

			// Token: 0x04007C9D RID: 31901
			public const float FAST = 5f;
		}

		// Token: 0x02001B91 RID: 7057
		public static class GROWTH_FACTOR
		{
			// Token: 0x04007C9E RID: 31902
			public const float NONE = float.PositiveInfinity;

			// Token: 0x04007C9F RID: 31903
			public const float DEATH_1 = 12000f;

			// Token: 0x04007CA0 RID: 31904
			public const float DEATH_2 = 6000f;

			// Token: 0x04007CA1 RID: 31905
			public const float DEATH_3 = 3000f;

			// Token: 0x04007CA2 RID: 31906
			public const float DEATH_4 = 1200f;

			// Token: 0x04007CA3 RID: 31907
			public const float DEATH_5 = 300f;

			// Token: 0x04007CA4 RID: 31908
			public const float DEATH_MAX = 10f;

			// Token: 0x04007CA5 RID: 31909
			public const float DEATH_INSTANT = 0f;

			// Token: 0x04007CA6 RID: 31910
			public const float GROWTH_1 = -12000f;

			// Token: 0x04007CA7 RID: 31911
			public const float GROWTH_2 = -6000f;

			// Token: 0x04007CA8 RID: 31912
			public const float GROWTH_3 = -3000f;

			// Token: 0x04007CA9 RID: 31913
			public const float GROWTH_4 = -1200f;

			// Token: 0x04007CAA RID: 31914
			public const float GROWTH_5 = -600f;

			// Token: 0x04007CAB RID: 31915
			public const float GROWTH_6 = -300f;

			// Token: 0x04007CAC RID: 31916
			public const float GROWTH_7 = -150f;
		}

		// Token: 0x02001B92 RID: 7058
		public static class UNDERPOPULATION_DEATH_RATE
		{
			// Token: 0x04007CAD RID: 31917
			public const float NONE = 0f;

			// Token: 0x04007CAE RID: 31918
			private const float BASE_NUM_TO_KILL = 400f;

			// Token: 0x04007CAF RID: 31919
			public const float SLOW = 0.6666667f;

			// Token: 0x04007CB0 RID: 31920
			public const float FAST = 2.6666667f;
		}
	}
}
