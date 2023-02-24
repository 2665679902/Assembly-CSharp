using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D2 RID: 2002
	public class CopyLeaderboardDefinitionByLeaderboardIdOptions
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x0009085B File Offset: 0x0008EA5B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600488A RID: 18570 RVA: 0x0009085E File Offset: 0x0008EA5E
		// (set) Token: 0x0600488B RID: 18571 RVA: 0x00090866 File Offset: 0x0008EA66
		public string LeaderboardId { get; set; }
	}
}
