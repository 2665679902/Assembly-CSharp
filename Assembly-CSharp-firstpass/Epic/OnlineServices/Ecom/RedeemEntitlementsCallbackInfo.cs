using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200087D RID: 2173
	public class RedeemEntitlementsCallbackInfo
	{
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004D4A RID: 19786 RVA: 0x00095597 File Offset: 0x00093797
		// (set) Token: 0x06004D4B RID: 19787 RVA: 0x0009559F File Offset: 0x0009379F
		public Result ResultCode { get; set; }

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004D4C RID: 19788 RVA: 0x000955A8 File Offset: 0x000937A8
		// (set) Token: 0x06004D4D RID: 19789 RVA: 0x000955B0 File Offset: 0x000937B0
		public object ClientData { get; set; }

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004D4E RID: 19790 RVA: 0x000955B9 File Offset: 0x000937B9
		// (set) Token: 0x06004D4F RID: 19791 RVA: 0x000955C1 File Offset: 0x000937C1
		public EpicAccountId LocalUserId { get; set; }
	}
}
