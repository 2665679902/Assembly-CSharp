using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200054C RID: 1356
	public class QueryUserInfoByDisplayNameOptions
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06003917 RID: 14615 RVA: 0x00081102 File Offset: 0x0007F302
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06003918 RID: 14616 RVA: 0x00081105 File Offset: 0x0007F305
		// (set) Token: 0x06003919 RID: 14617 RVA: 0x0008110D File Offset: 0x0007F30D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600391A RID: 14618 RVA: 0x00081116 File Offset: 0x0007F316
		// (set) Token: 0x0600391B RID: 14619 RVA: 0x0008111E File Offset: 0x0007F31E
		public string DisplayName { get; set; }
	}
}
