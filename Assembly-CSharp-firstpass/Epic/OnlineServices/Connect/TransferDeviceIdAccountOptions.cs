using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D6 RID: 2262
	public class TransferDeviceIdAccountOptions
	{
		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x000974F2 File Offset: 0x000956F2
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x000974F5 File Offset: 0x000956F5
		// (set) Token: 0x06004F58 RID: 20312 RVA: 0x000974FD File Offset: 0x000956FD
		public ProductUserId PrimaryLocalUserId { get; set; }

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004F59 RID: 20313 RVA: 0x00097506 File Offset: 0x00095706
		// (set) Token: 0x06004F5A RID: 20314 RVA: 0x0009750E File Offset: 0x0009570E
		public ProductUserId LocalDeviceUserId { get; set; }

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06004F5B RID: 20315 RVA: 0x00097517 File Offset: 0x00095717
		// (set) Token: 0x06004F5C RID: 20316 RVA: 0x0009751F File Offset: 0x0009571F
		public ProductUserId ProductUserIdToPreserve { get; set; }
	}
}
