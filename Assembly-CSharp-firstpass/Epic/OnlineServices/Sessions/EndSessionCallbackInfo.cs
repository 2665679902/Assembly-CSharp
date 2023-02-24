using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005DF RID: 1503
	public class EndSessionCallbackInfo
	{
		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x00084BDB File Offset: 0x00082DDB
		// (set) Token: 0x06003CB5 RID: 15541 RVA: 0x00084BE3 File Offset: 0x00082DE3
		public Result ResultCode { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x00084BEC File Offset: 0x00082DEC
		// (set) Token: 0x06003CB7 RID: 15543 RVA: 0x00084BF4 File Offset: 0x00082DF4
		public object ClientData { get; set; }
	}
}
