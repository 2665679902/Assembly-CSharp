using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000556 RID: 1366
	public class UserInfoData
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06003964 RID: 14692 RVA: 0x000815D3 File Offset: 0x0007F7D3
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06003965 RID: 14693 RVA: 0x000815D6 File Offset: 0x0007F7D6
		// (set) Token: 0x06003966 RID: 14694 RVA: 0x000815DE File Offset: 0x0007F7DE
		public EpicAccountId UserId { get; set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x000815E7 File Offset: 0x0007F7E7
		// (set) Token: 0x06003968 RID: 14696 RVA: 0x000815EF File Offset: 0x0007F7EF
		public string Country { get; set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x000815F8 File Offset: 0x0007F7F8
		// (set) Token: 0x0600396A RID: 14698 RVA: 0x00081600 File Offset: 0x0007F800
		public string DisplayName { get; set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600396B RID: 14699 RVA: 0x00081609 File Offset: 0x0007F809
		// (set) Token: 0x0600396C RID: 14700 RVA: 0x00081611 File Offset: 0x0007F811
		public string PreferredLanguage { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600396D RID: 14701 RVA: 0x0008161A File Offset: 0x0007F81A
		// (set) Token: 0x0600396E RID: 14702 RVA: 0x00081622 File Offset: 0x0007F822
		public string Nickname { get; set; }
	}
}
