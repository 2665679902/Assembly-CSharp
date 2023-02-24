using System;

namespace TUNING
{
	// Token: 0x02000D23 RID: 3363
	public class ROBOTS
	{
		// Token: 0x02001B7D RID: 7037
		public class SCOUTBOT
		{
			// Token: 0x04007C26 RID: 31782
			public static readonly float DIGGING = 1f;

			// Token: 0x04007C27 RID: 31783
			public static readonly float CONSTRUCTION = 1f;

			// Token: 0x04007C28 RID: 31784
			public static readonly float ATHLETICS = 1f;

			// Token: 0x04007C29 RID: 31785
			public static readonly float HIT_POINTS = 100f;

			// Token: 0x04007C2A RID: 31786
			public static readonly float BATTERY_DEPLETION_RATE = 30f;

			// Token: 0x04007C2B RID: 31787
			public static readonly float BATTERY_CAPACITY = ROBOTS.SCOUTBOT.BATTERY_DEPLETION_RATE * 10f * 600f;
		}
	}
}
