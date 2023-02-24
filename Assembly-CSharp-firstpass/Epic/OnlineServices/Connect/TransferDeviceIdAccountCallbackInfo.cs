using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D4 RID: 2260
	public class TransferDeviceIdAccountCallbackInfo
	{
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x00097443 File Offset: 0x00095643
		// (set) Token: 0x06004F4C RID: 20300 RVA: 0x0009744B File Offset: 0x0009564B
		public Result ResultCode { get; set; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004F4D RID: 20301 RVA: 0x00097454 File Offset: 0x00095654
		// (set) Token: 0x06004F4E RID: 20302 RVA: 0x0009745C File Offset: 0x0009565C
		public object ClientData { get; set; }

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004F4F RID: 20303 RVA: 0x00097465 File Offset: 0x00095665
		// (set) Token: 0x06004F50 RID: 20304 RVA: 0x0009746D File Offset: 0x0009566D
		public ProductUserId LocalUserId { get; set; }
	}
}
