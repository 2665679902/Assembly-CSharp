using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007D6 RID: 2006
	public class CopyLeaderboardRecordByUserIdOptions
	{
		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600489B RID: 18587 RVA: 0x00090963 File Offset: 0x0008EB63
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600489C RID: 18588 RVA: 0x00090966 File Offset: 0x0008EB66
		// (set) Token: 0x0600489D RID: 18589 RVA: 0x0009096E File Offset: 0x0008EB6E
		public ProductUserId UserId { get; set; }
	}
}
