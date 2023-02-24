using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200054A RID: 1354
	public class QueryUserInfoByDisplayNameCallbackInfo
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x00080FEB File Offset: 0x0007F1EB
		// (set) Token: 0x06003907 RID: 14599 RVA: 0x00080FF3 File Offset: 0x0007F1F3
		public Result ResultCode { get; set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06003908 RID: 14600 RVA: 0x00080FFC File Offset: 0x0007F1FC
		// (set) Token: 0x06003909 RID: 14601 RVA: 0x00081004 File Offset: 0x0007F204
		public object ClientData { get; set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x0008100D File Offset: 0x0007F20D
		// (set) Token: 0x0600390B RID: 14603 RVA: 0x00081015 File Offset: 0x0007F215
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x0008101E File Offset: 0x0007F21E
		// (set) Token: 0x0600390D RID: 14605 RVA: 0x00081026 File Offset: 0x0007F226
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x0008102F File Offset: 0x0007F22F
		// (set) Token: 0x0600390F RID: 14607 RVA: 0x00081037 File Offset: 0x0007F237
		public string DisplayName { get; set; }
	}
}
