using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007F0 RID: 2032
	public class OnQueryLeaderboardRanksCompleteCallbackInfo
	{
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x000914C6 File Offset: 0x0008F6C6
		// (set) Token: 0x06004945 RID: 18757 RVA: 0x000914CE File Offset: 0x0008F6CE
		public Result ResultCode { get; set; }

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x000914D7 File Offset: 0x0008F6D7
		// (set) Token: 0x06004947 RID: 18759 RVA: 0x000914DF File Offset: 0x0008F6DF
		public object ClientData { get; set; }
	}
}
