using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000552 RID: 1362
	public class QueryUserInfoCallbackInfo
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06003949 RID: 14665 RVA: 0x00081427 File Offset: 0x0007F627
		// (set) Token: 0x0600394A RID: 14666 RVA: 0x0008142F File Offset: 0x0007F62F
		public Result ResultCode { get; set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600394B RID: 14667 RVA: 0x00081438 File Offset: 0x0007F638
		// (set) Token: 0x0600394C RID: 14668 RVA: 0x00081440 File Offset: 0x0007F640
		public object ClientData { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x00081449 File Offset: 0x0007F649
		// (set) Token: 0x0600394E RID: 14670 RVA: 0x00081451 File Offset: 0x0007F651
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x0008145A File Offset: 0x0007F65A
		// (set) Token: 0x06003950 RID: 14672 RVA: 0x00081462 File Offset: 0x0007F662
		public EpicAccountId TargetUserId { get; set; }
	}
}
