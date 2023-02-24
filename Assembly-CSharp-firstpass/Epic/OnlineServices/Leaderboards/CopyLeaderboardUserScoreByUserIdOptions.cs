using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007DA RID: 2010
	public class CopyLeaderboardUserScoreByUserIdOptions
	{
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060048B1 RID: 18609 RVA: 0x00090AAF File Offset: 0x0008ECAF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060048B2 RID: 18610 RVA: 0x00090AB2 File Offset: 0x0008ECB2
		// (set) Token: 0x060048B3 RID: 18611 RVA: 0x00090ABA File Offset: 0x0008ECBA
		public ProductUserId UserId { get; set; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060048B4 RID: 18612 RVA: 0x00090AC3 File Offset: 0x0008ECC3
		// (set) Token: 0x060048B5 RID: 18613 RVA: 0x00090ACB File Offset: 0x0008ECCB
		public string StatName { get; set; }
	}
}
