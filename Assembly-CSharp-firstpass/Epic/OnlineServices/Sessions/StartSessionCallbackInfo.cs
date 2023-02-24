using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000656 RID: 1622
	public class StartSessionCallbackInfo
	{
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x00087658 File Offset: 0x00085858
		// (set) Token: 0x06003F66 RID: 16230 RVA: 0x00087660 File Offset: 0x00085860
		public Result ResultCode { get; set; }

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x00087669 File Offset: 0x00085869
		// (set) Token: 0x06003F68 RID: 16232 RVA: 0x00087671 File Offset: 0x00085871
		public object ClientData { get; set; }
	}
}
