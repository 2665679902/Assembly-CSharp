using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007EC RID: 2028
	public class OnQueryLeaderboardDefinitionsCompleteCallbackInfo
	{
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x0009144C File Offset: 0x0008F64C
		// (set) Token: 0x06004935 RID: 18741 RVA: 0x00091454 File Offset: 0x0008F654
		public Result ResultCode { get; set; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06004936 RID: 18742 RVA: 0x0009145D File Offset: 0x0008F65D
		// (set) Token: 0x06004937 RID: 18743 RVA: 0x00091465 File Offset: 0x0008F665
		public object ClientData { get; set; }
	}
}
