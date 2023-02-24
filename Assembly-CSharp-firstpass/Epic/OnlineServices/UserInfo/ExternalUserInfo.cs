using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000540 RID: 1344
	public class ExternalUserInfo
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x00080E13 File Offset: 0x0007F013
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060038D1 RID: 14545 RVA: 0x00080E16 File Offset: 0x0007F016
		// (set) Token: 0x060038D2 RID: 14546 RVA: 0x00080E1E File Offset: 0x0007F01E
		public ExternalAccountType AccountType { get; set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x00080E27 File Offset: 0x0007F027
		// (set) Token: 0x060038D4 RID: 14548 RVA: 0x00080E2F File Offset: 0x0007F02F
		public string AccountId { get; set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060038D5 RID: 14549 RVA: 0x00080E38 File Offset: 0x0007F038
		// (set) Token: 0x060038D6 RID: 14550 RVA: 0x00080E40 File Offset: 0x0007F040
		public string DisplayName { get; set; }
	}
}
