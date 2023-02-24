using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007E7 RID: 2023
	public class LeaderboardUserScore
	{
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060048FF RID: 18687 RVA: 0x00090F67 File Offset: 0x0008F167
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06004900 RID: 18688 RVA: 0x00090F6A File Offset: 0x0008F16A
		// (set) Token: 0x06004901 RID: 18689 RVA: 0x00090F72 File Offset: 0x0008F172
		public ProductUserId UserId { get; set; }

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06004902 RID: 18690 RVA: 0x00090F7B File Offset: 0x0008F17B
		// (set) Token: 0x06004903 RID: 18691 RVA: 0x00090F83 File Offset: 0x0008F183
		public int Score { get; set; }
	}
}
