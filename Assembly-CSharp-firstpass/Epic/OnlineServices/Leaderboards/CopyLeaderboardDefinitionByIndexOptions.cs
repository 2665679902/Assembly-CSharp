using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D0 RID: 2000
	public class CopyLeaderboardDefinitionByIndexOptions
	{
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06004880 RID: 18560 RVA: 0x000907D7 File Offset: 0x0008E9D7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06004881 RID: 18561 RVA: 0x000907DA File Offset: 0x0008E9DA
		// (set) Token: 0x06004882 RID: 18562 RVA: 0x000907E2 File Offset: 0x0008E9E2
		public uint LeaderboardIndex { get; set; }
	}
}
