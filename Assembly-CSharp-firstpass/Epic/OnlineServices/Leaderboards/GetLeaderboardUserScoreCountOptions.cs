using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007E2 RID: 2018
	public class GetLeaderboardUserScoreCountOptions
	{
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060048E1 RID: 18657 RVA: 0x00090D8F File Offset: 0x0008EF8F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060048E2 RID: 18658 RVA: 0x00090D92 File Offset: 0x0008EF92
		// (set) Token: 0x060048E3 RID: 18659 RVA: 0x00090D9A File Offset: 0x0008EF9A
		public string StatName { get; set; }
	}
}
