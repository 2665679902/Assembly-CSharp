using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000818 RID: 2072
	public class QueryFriendsCallbackInfo
	{
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06004A28 RID: 18984 RVA: 0x0009218E File Offset: 0x0009038E
		// (set) Token: 0x06004A29 RID: 18985 RVA: 0x00092196 File Offset: 0x00090396
		public Result ResultCode { get; set; }

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06004A2A RID: 18986 RVA: 0x0009219F File Offset: 0x0009039F
		// (set) Token: 0x06004A2B RID: 18987 RVA: 0x000921A7 File Offset: 0x000903A7
		public object ClientData { get; set; }

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06004A2C RID: 18988 RVA: 0x000921B0 File Offset: 0x000903B0
		// (set) Token: 0x06004A2D RID: 18989 RVA: 0x000921B8 File Offset: 0x000903B8
		public EpicAccountId LocalUserId { get; set; }
	}
}
