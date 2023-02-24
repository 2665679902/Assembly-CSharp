using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007E5 RID: 2021
	public class LeaderboardRecord
	{
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060048EA RID: 18666 RVA: 0x00090E13 File Offset: 0x0008F013
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x00090E16 File Offset: 0x0008F016
		// (set) Token: 0x060048EC RID: 18668 RVA: 0x00090E1E File Offset: 0x0008F01E
		public ProductUserId UserId { get; set; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x00090E27 File Offset: 0x0008F027
		// (set) Token: 0x060048EE RID: 18670 RVA: 0x00090E2F File Offset: 0x0008F02F
		public uint Rank { get; set; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x00090E38 File Offset: 0x0008F038
		// (set) Token: 0x060048F0 RID: 18672 RVA: 0x00090E40 File Offset: 0x0008F040
		public int Score { get; set; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x00090E49 File Offset: 0x0008F049
		// (set) Token: 0x060048F2 RID: 18674 RVA: 0x00090E51 File Offset: 0x0008F051
		public string UserDisplayName { get; set; }
	}
}
