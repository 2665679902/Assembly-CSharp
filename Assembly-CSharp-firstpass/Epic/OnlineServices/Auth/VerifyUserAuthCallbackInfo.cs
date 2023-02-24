using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200090D RID: 2317
	public class VerifyUserAuthCallbackInfo
	{
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060050B3 RID: 20659 RVA: 0x0009898F File Offset: 0x00096B8F
		// (set) Token: 0x060050B4 RID: 20660 RVA: 0x00098997 File Offset: 0x00096B97
		public Result ResultCode { get; set; }

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x060050B5 RID: 20661 RVA: 0x000989A0 File Offset: 0x00096BA0
		// (set) Token: 0x060050B6 RID: 20662 RVA: 0x000989A8 File Offset: 0x00096BA8
		public object ClientData { get; set; }
	}
}
