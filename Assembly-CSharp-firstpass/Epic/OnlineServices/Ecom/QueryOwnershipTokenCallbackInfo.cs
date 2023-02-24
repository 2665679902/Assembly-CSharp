using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000879 RID: 2169
	public class QueryOwnershipTokenCallbackInfo
	{
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004D2B RID: 19755 RVA: 0x0009538F File Offset: 0x0009358F
		// (set) Token: 0x06004D2C RID: 19756 RVA: 0x00095397 File Offset: 0x00093597
		public Result ResultCode { get; set; }

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004D2D RID: 19757 RVA: 0x000953A0 File Offset: 0x000935A0
		// (set) Token: 0x06004D2E RID: 19758 RVA: 0x000953A8 File Offset: 0x000935A8
		public object ClientData { get; set; }

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004D2F RID: 19759 RVA: 0x000953B1 File Offset: 0x000935B1
		// (set) Token: 0x06004D30 RID: 19760 RVA: 0x000953B9 File Offset: 0x000935B9
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004D31 RID: 19761 RVA: 0x000953C2 File Offset: 0x000935C2
		// (set) Token: 0x06004D32 RID: 19762 RVA: 0x000953CA File Offset: 0x000935CA
		public string OwnershipToken { get; set; }
	}
}
