using System;

namespace TUNING
{
	// Token: 0x02000D22 RID: 3362
	public class NOISE_POLLUTION
	{
		// Token: 0x04004C9B RID: 19611
		public static readonly EffectorValues NONE = new EffectorValues
		{
			amount = 0,
			radius = 0
		};

		// Token: 0x04004C9C RID: 19612
		public static readonly EffectorValues CONE_OF_SILENCE = new EffectorValues
		{
			amount = -120,
			radius = 5
		};

		// Token: 0x04004C9D RID: 19613
		public static float DUPLICANT_TIME_THRESHOLD = 3f;

		// Token: 0x02001B79 RID: 7033
		public class LENGTHS
		{
			// Token: 0x04007C0C RID: 31756
			public static float VERYSHORT = 0.25f;

			// Token: 0x04007C0D RID: 31757
			public static float SHORT = 0.5f;

			// Token: 0x04007C0E RID: 31758
			public static float NORMAL = 1f;

			// Token: 0x04007C0F RID: 31759
			public static float LONG = 1.5f;

			// Token: 0x04007C10 RID: 31760
			public static float VERYLONG = 2f;
		}

		// Token: 0x02001B7A RID: 7034
		public class NOISY
		{
			// Token: 0x04007C11 RID: 31761
			public static readonly EffectorValues TIER0 = new EffectorValues
			{
				amount = 45,
				radius = 10
			};

			// Token: 0x04007C12 RID: 31762
			public static readonly EffectorValues TIER1 = new EffectorValues
			{
				amount = 55,
				radius = 10
			};

			// Token: 0x04007C13 RID: 31763
			public static readonly EffectorValues TIER2 = new EffectorValues
			{
				amount = 65,
				radius = 10
			};

			// Token: 0x04007C14 RID: 31764
			public static readonly EffectorValues TIER3 = new EffectorValues
			{
				amount = 75,
				radius = 15
			};

			// Token: 0x04007C15 RID: 31765
			public static readonly EffectorValues TIER4 = new EffectorValues
			{
				amount = 90,
				radius = 15
			};

			// Token: 0x04007C16 RID: 31766
			public static readonly EffectorValues TIER5 = new EffectorValues
			{
				amount = 105,
				radius = 20
			};

			// Token: 0x04007C17 RID: 31767
			public static readonly EffectorValues TIER6 = new EffectorValues
			{
				amount = 125,
				radius = 20
			};
		}

		// Token: 0x02001B7B RID: 7035
		public class CREATURES
		{
			// Token: 0x04007C18 RID: 31768
			public static readonly EffectorValues TIER0 = new EffectorValues
			{
				amount = 30,
				radius = 5
			};

			// Token: 0x04007C19 RID: 31769
			public static readonly EffectorValues TIER1 = new EffectorValues
			{
				amount = 35,
				radius = 5
			};

			// Token: 0x04007C1A RID: 31770
			public static readonly EffectorValues TIER2 = new EffectorValues
			{
				amount = 45,
				radius = 5
			};

			// Token: 0x04007C1B RID: 31771
			public static readonly EffectorValues TIER3 = new EffectorValues
			{
				amount = 55,
				radius = 5
			};

			// Token: 0x04007C1C RID: 31772
			public static readonly EffectorValues TIER4 = new EffectorValues
			{
				amount = 65,
				radius = 5
			};

			// Token: 0x04007C1D RID: 31773
			public static readonly EffectorValues TIER5 = new EffectorValues
			{
				amount = 75,
				radius = 5
			};

			// Token: 0x04007C1E RID: 31774
			public static readonly EffectorValues TIER6 = new EffectorValues
			{
				amount = 90,
				radius = 10
			};

			// Token: 0x04007C1F RID: 31775
			public static readonly EffectorValues TIER7 = new EffectorValues
			{
				amount = 105,
				radius = 10
			};
		}

		// Token: 0x02001B7C RID: 7036
		public class DAMPEN
		{
			// Token: 0x04007C20 RID: 31776
			public static readonly EffectorValues TIER0 = new EffectorValues
			{
				amount = -5,
				radius = 1
			};

			// Token: 0x04007C21 RID: 31777
			public static readonly EffectorValues TIER1 = new EffectorValues
			{
				amount = -10,
				radius = 2
			};

			// Token: 0x04007C22 RID: 31778
			public static readonly EffectorValues TIER2 = new EffectorValues
			{
				amount = -15,
				radius = 3
			};

			// Token: 0x04007C23 RID: 31779
			public static readonly EffectorValues TIER3 = new EffectorValues
			{
				amount = -20,
				radius = 4
			};

			// Token: 0x04007C24 RID: 31780
			public static readonly EffectorValues TIER4 = new EffectorValues
			{
				amount = -20,
				radius = 5
			};

			// Token: 0x04007C25 RID: 31781
			public static readonly EffectorValues TIER5 = new EffectorValues
			{
				amount = -25,
				radius = 6
			};
		}
	}
}
