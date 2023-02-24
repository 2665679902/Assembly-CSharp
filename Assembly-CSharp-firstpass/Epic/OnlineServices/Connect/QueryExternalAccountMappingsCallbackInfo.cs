using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008CC RID: 2252
	public class QueryExternalAccountMappingsCallbackInfo
	{
		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004F13 RID: 20243 RVA: 0x0009709A File Offset: 0x0009529A
		// (set) Token: 0x06004F14 RID: 20244 RVA: 0x000970A2 File Offset: 0x000952A2
		public Result ResultCode { get; set; }

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x000970AB File Offset: 0x000952AB
		// (set) Token: 0x06004F16 RID: 20246 RVA: 0x000970B3 File Offset: 0x000952B3
		public object ClientData { get; set; }

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004F17 RID: 20247 RVA: 0x000970BC File Offset: 0x000952BC
		// (set) Token: 0x06004F18 RID: 20248 RVA: 0x000970C4 File Offset: 0x000952C4
		public ProductUserId LocalUserId { get; set; }
	}
}
