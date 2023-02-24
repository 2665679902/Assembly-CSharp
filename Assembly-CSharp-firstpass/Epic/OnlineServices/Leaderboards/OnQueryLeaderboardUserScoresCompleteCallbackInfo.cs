using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007F4 RID: 2036
	public class OnQueryLeaderboardUserScoresCompleteCallbackInfo
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x0009153E File Offset: 0x0008F73E
		// (set) Token: 0x06004955 RID: 18773 RVA: 0x00091546 File Offset: 0x0008F746
		public Result ResultCode { get; set; }

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x0009154F File Offset: 0x0008F74F
		// (set) Token: 0x06004957 RID: 18775 RVA: 0x00091557 File Offset: 0x0008F757
		public object ClientData { get; set; }
	}
}
