using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D4 RID: 2004
	public class CopyLeaderboardRecordByIndexOptions
	{
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x000908DF File Offset: 0x0008EADF
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06004893 RID: 18579 RVA: 0x000908E2 File Offset: 0x0008EAE2
		// (set) Token: 0x06004894 RID: 18580 RVA: 0x000908EA File Offset: 0x0008EAEA
		public uint LeaderboardRecordIndex { get; set; }
	}
}
