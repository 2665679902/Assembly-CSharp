using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200065E RID: 1630
	public class UpdateSessionCallbackInfo
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x000878B3 File Offset: 0x00085AB3
		// (set) Token: 0x06003F8C RID: 16268 RVA: 0x000878BB File Offset: 0x00085ABB
		public Result ResultCode { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000878C4 File Offset: 0x00085AC4
		// (set) Token: 0x06003F8E RID: 16270 RVA: 0x000878CC File Offset: 0x00085ACC
		public object ClientData { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x000878D5 File Offset: 0x00085AD5
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x000878DD File Offset: 0x00085ADD
		public string SessionName { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000878E6 File Offset: 0x00085AE6
		// (set) Token: 0x06003F92 RID: 16274 RVA: 0x000878EE File Offset: 0x00085AEE
		public string SessionId { get; set; }
	}
}
