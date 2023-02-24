using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D8 RID: 2264
	public class UnlinkAccountCallbackInfo
	{
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004F67 RID: 20327 RVA: 0x000975FF File Offset: 0x000957FF
		// (set) Token: 0x06004F68 RID: 20328 RVA: 0x00097607 File Offset: 0x00095807
		public Result ResultCode { get; set; }

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004F69 RID: 20329 RVA: 0x00097610 File Offset: 0x00095810
		// (set) Token: 0x06004F6A RID: 20330 RVA: 0x00097618 File Offset: 0x00095818
		public object ClientData { get; set; }

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004F6B RID: 20331 RVA: 0x00097621 File Offset: 0x00095821
		// (set) Token: 0x06004F6C RID: 20332 RVA: 0x00097629 File Offset: 0x00095829
		public ProductUserId LocalUserId { get; set; }
	}
}
