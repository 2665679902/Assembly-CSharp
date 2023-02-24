using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000542 RID: 1346
	public class GetExternalUserInfoCountOptions
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x00080F23 File Offset: 0x0007F123
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x00080F26 File Offset: 0x0007F126
		// (set) Token: 0x060038E3 RID: 14563 RVA: 0x00080F2E File Offset: 0x0007F12E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060038E4 RID: 14564 RVA: 0x00080F37 File Offset: 0x0007F137
		// (set) Token: 0x060038E5 RID: 14565 RVA: 0x00080F3F File Offset: 0x0007F13F
		public EpicAccountId TargetUserId { get; set; }
	}
}
