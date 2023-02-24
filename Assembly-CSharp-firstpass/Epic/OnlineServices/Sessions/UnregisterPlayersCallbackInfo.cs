using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200065A RID: 1626
	public class UnregisterPlayersCallbackInfo
	{
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x00087757 File Offset: 0x00085957
		// (set) Token: 0x06003F77 RID: 16247 RVA: 0x0008775F File Offset: 0x0008595F
		public Result ResultCode { get; set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x00087768 File Offset: 0x00085968
		// (set) Token: 0x06003F79 RID: 16249 RVA: 0x00087770 File Offset: 0x00085970
		public object ClientData { get; set; }
	}
}
