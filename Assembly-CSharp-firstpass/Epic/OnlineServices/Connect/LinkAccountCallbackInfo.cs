using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008AC RID: 2220
	public class LinkAccountCallbackInfo
	{
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004E7A RID: 20090 RVA: 0x00096C7B File Offset: 0x00094E7B
		// (set) Token: 0x06004E7B RID: 20091 RVA: 0x00096C83 File Offset: 0x00094E83
		public Result ResultCode { get; set; }

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004E7C RID: 20092 RVA: 0x00096C8C File Offset: 0x00094E8C
		// (set) Token: 0x06004E7D RID: 20093 RVA: 0x00096C94 File Offset: 0x00094E94
		public object ClientData { get; set; }

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004E7E RID: 20094 RVA: 0x00096C9D File Offset: 0x00094E9D
		// (set) Token: 0x06004E7F RID: 20095 RVA: 0x00096CA5 File Offset: 0x00094EA5
		public ProductUserId LocalUserId { get; set; }
	}
}
