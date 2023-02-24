using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200082A RID: 2090
	public class CheckoutCallbackInfo
	{
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x00092EDB File Offset: 0x000910DB
		// (set) Token: 0x06004AF6 RID: 19190 RVA: 0x00092EE3 File Offset: 0x000910E3
		public Result ResultCode { get; set; }

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06004AF7 RID: 19191 RVA: 0x00092EEC File Offset: 0x000910EC
		// (set) Token: 0x06004AF8 RID: 19192 RVA: 0x00092EF4 File Offset: 0x000910F4
		public object ClientData { get; set; }

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06004AF9 RID: 19193 RVA: 0x00092EFD File Offset: 0x000910FD
		// (set) Token: 0x06004AFA RID: 19194 RVA: 0x00092F05 File Offset: 0x00091105
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06004AFB RID: 19195 RVA: 0x00092F0E File Offset: 0x0009110E
		// (set) Token: 0x06004AFC RID: 19196 RVA: 0x00092F16 File Offset: 0x00091116
		public string TransactionId { get; set; }
	}
}
