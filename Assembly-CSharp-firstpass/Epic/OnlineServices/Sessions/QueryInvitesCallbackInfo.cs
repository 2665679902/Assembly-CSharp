using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200060B RID: 1547
	public class QueryInvitesCallbackInfo
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x0008516B File Offset: 0x0008336B
		// (set) Token: 0x06003D79 RID: 15737 RVA: 0x00085173 File Offset: 0x00083373
		public Result ResultCode { get; set; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06003D7A RID: 15738 RVA: 0x0008517C File Offset: 0x0008337C
		// (set) Token: 0x06003D7B RID: 15739 RVA: 0x00085184 File Offset: 0x00083384
		public object ClientData { get; set; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x0008518D File Offset: 0x0008338D
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x00085195 File Offset: 0x00083395
		public ProductUserId LocalUserId { get; set; }
	}
}
