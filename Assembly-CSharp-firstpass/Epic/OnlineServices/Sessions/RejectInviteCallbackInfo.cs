using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000613 RID: 1555
	public class RejectInviteCallbackInfo
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x000853FB File Offset: 0x000835FB
		// (set) Token: 0x06003DA2 RID: 15778 RVA: 0x00085403 File Offset: 0x00083603
		public Result ResultCode { get; set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x0008540C File Offset: 0x0008360C
		// (set) Token: 0x06003DA4 RID: 15780 RVA: 0x00085414 File Offset: 0x00083614
		public object ClientData { get; set; }
	}
}
