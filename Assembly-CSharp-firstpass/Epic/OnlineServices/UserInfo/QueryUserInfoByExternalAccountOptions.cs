using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000550 RID: 1360
	public class QueryUserInfoByExternalAccountOptions
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06003938 RID: 14648 RVA: 0x0008131A File Offset: 0x0007F51A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x0008131D File Offset: 0x0007F51D
		// (set) Token: 0x0600393A RID: 14650 RVA: 0x00081325 File Offset: 0x0007F525
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x0008132E File Offset: 0x0007F52E
		// (set) Token: 0x0600393C RID: 14652 RVA: 0x00081336 File Offset: 0x0007F536
		public string ExternalAccountId { get; set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600393D RID: 14653 RVA: 0x0008133F File Offset: 0x0007F53F
		// (set) Token: 0x0600393E RID: 14654 RVA: 0x00081347 File Offset: 0x0007F547
		public ExternalAccountType AccountType { get; set; }
	}
}
