using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C2 RID: 1986
	public class RejectInviteCallbackInfo
	{
		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x000902A7 File Offset: 0x0008E4A7
		// (set) Token: 0x0600482C RID: 18476 RVA: 0x000902AF File Offset: 0x0008E4AF
		public Result ResultCode { get; set; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600482D RID: 18477 RVA: 0x000902B8 File Offset: 0x0008E4B8
		// (set) Token: 0x0600482E RID: 18478 RVA: 0x000902C0 File Offset: 0x0008E4C0
		public object ClientData { get; set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600482F RID: 18479 RVA: 0x000902C9 File Offset: 0x0008E4C9
		// (set) Token: 0x06004830 RID: 18480 RVA: 0x000902D1 File Offset: 0x0008E4D1
		public string InviteId { get; set; }
	}
}
