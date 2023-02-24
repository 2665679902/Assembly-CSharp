using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200053A RID: 1338
	public class CopyExternalUserInfoByAccountTypeOptions
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x00080B2B File Offset: 0x0007ED2B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x00080B2E File Offset: 0x0007ED2E
		// (set) Token: 0x060038A3 RID: 14499 RVA: 0x00080B36 File Offset: 0x0007ED36
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x00080B3F File Offset: 0x0007ED3F
		// (set) Token: 0x060038A5 RID: 14501 RVA: 0x00080B47 File Offset: 0x0007ED47
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060038A6 RID: 14502 RVA: 0x00080B50 File Offset: 0x0007ED50
		// (set) Token: 0x060038A7 RID: 14503 RVA: 0x00080B58 File Offset: 0x0007ED58
		public ExternalAccountType AccountType { get; set; }
	}
}
