using System;

namespace TUNING
{
	// Token: 0x02000D2C RID: 3372
	public class RADIATION
	{
		// Token: 0x04004D34 RID: 19764
		public const float GERM_RAD_SCALE = 0.01f;

		// Token: 0x04004D35 RID: 19765
		public const float STANDARD_DAILY_RECOVERY = 100f;

		// Token: 0x04004D36 RID: 19766
		public const float EXTRA_VOMIT_RECOVERY = 20f;

		// Token: 0x04004D37 RID: 19767
		public const float REACT_THRESHOLD = 133f;

		// Token: 0x02001B9F RID: 7071
		public class STANDARD_EMITTER
		{
			// Token: 0x04007D1B RID: 32027
			public const float STEADY_PULSE_RATE = 0.2f;

			// Token: 0x04007D1C RID: 32028
			public const float DOUBLE_SPEED_PULSE_RATE = 0.1f;

			// Token: 0x04007D1D RID: 32029
			public const float RADIUS_SCALE = 1f;
		}

		// Token: 0x02001BA0 RID: 7072
		public class RADIATION_PER_SECOND
		{
			// Token: 0x04007D1E RID: 32030
			public const float TRIVIAL = 60f;

			// Token: 0x04007D1F RID: 32031
			public const float VERY_LOW = 120f;

			// Token: 0x04007D20 RID: 32032
			public const float LOW = 240f;

			// Token: 0x04007D21 RID: 32033
			public const float MODERATE = 600f;

			// Token: 0x04007D22 RID: 32034
			public const float HIGH = 1800f;

			// Token: 0x04007D23 RID: 32035
			public const float VERY_HIGH = 4800f;

			// Token: 0x04007D24 RID: 32036
			public const int EXTREME = 9600;
		}

		// Token: 0x02001BA1 RID: 7073
		public class RADIATION_CONSTANT_RADS_PER_CYCLE
		{
			// Token: 0x04007D25 RID: 32037
			public const float LESS_THAN_TRIVIAL = 60f;

			// Token: 0x04007D26 RID: 32038
			public const float TRIVIAL = 120f;

			// Token: 0x04007D27 RID: 32039
			public const float VERY_LOW = 240f;

			// Token: 0x04007D28 RID: 32040
			public const float LOW = 480f;

			// Token: 0x04007D29 RID: 32041
			public const float MODERATE = 1200f;

			// Token: 0x04007D2A RID: 32042
			public const float MODERATE_PLUS = 2400f;

			// Token: 0x04007D2B RID: 32043
			public const float HIGH = 3600f;

			// Token: 0x04007D2C RID: 32044
			public const float VERY_HIGH = 8400f;

			// Token: 0x04007D2D RID: 32045
			public const int EXTREME = 16800;
		}
	}
}
