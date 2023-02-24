using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200054E RID: 1358
	public class QueryUserInfoByExternalAccountCallbackInfo
	{
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x000811CB File Offset: 0x0007F3CB
		// (set) Token: 0x06003925 RID: 14629 RVA: 0x000811D3 File Offset: 0x0007F3D3
		public Result ResultCode { get; set; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06003926 RID: 14630 RVA: 0x000811DC File Offset: 0x0007F3DC
		// (set) Token: 0x06003927 RID: 14631 RVA: 0x000811E4 File Offset: 0x0007F3E4
		public object ClientData { get; set; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06003928 RID: 14632 RVA: 0x000811ED File Offset: 0x0007F3ED
		// (set) Token: 0x06003929 RID: 14633 RVA: 0x000811F5 File Offset: 0x0007F3F5
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600392A RID: 14634 RVA: 0x000811FE File Offset: 0x0007F3FE
		// (set) Token: 0x0600392B RID: 14635 RVA: 0x00081206 File Offset: 0x0007F406
		public string ExternalAccountId { get; set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x0008120F File Offset: 0x0007F40F
		// (set) Token: 0x0600392D RID: 14637 RVA: 0x00081217 File Offset: 0x0007F417
		public ExternalAccountType AccountType { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600392E RID: 14638 RVA: 0x00081220 File Offset: 0x0007F420
		// (set) Token: 0x0600392F RID: 14639 RVA: 0x00081228 File Offset: 0x0007F428
		public EpicAccountId TargetUserId { get; set; }
	}
}
