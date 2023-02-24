using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200088F RID: 2191
	public class CopyProductUserExternalAccountByAccountTypeOptions
	{
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06004DD4 RID: 19924 RVA: 0x00096273 File Offset: 0x00094473
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06004DD5 RID: 19925 RVA: 0x00096276 File Offset: 0x00094476
		// (set) Token: 0x06004DD6 RID: 19926 RVA: 0x0009627E File Offset: 0x0009447E
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06004DD7 RID: 19927 RVA: 0x00096287 File Offset: 0x00094487
		// (set) Token: 0x06004DD8 RID: 19928 RVA: 0x0009628F File Offset: 0x0009448F
		public ExternalAccountType AccountIdType { get; set; }
	}
}
