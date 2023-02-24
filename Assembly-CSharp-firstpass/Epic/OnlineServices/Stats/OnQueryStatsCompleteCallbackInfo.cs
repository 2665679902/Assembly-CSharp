using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005B3 RID: 1459
	public class OnQueryStatsCompleteCallbackInfo
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x00083873 File Offset: 0x00081A73
		// (set) Token: 0x06003B9F RID: 15263 RVA: 0x0008387B File Offset: 0x00081A7B
		public Result ResultCode { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x00083884 File Offset: 0x00081A84
		// (set) Token: 0x06003BA1 RID: 15265 RVA: 0x0008388C File Offset: 0x00081A8C
		public object ClientData { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x00083895 File Offset: 0x00081A95
		// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x0008389D File Offset: 0x00081A9D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000838A6 File Offset: 0x00081AA6
		// (set) Token: 0x06003BA5 RID: 15269 RVA: 0x000838AE File Offset: 0x00081AAE
		public ProductUserId TargetUserId { get; set; }
	}
}
