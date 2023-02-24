using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000538 RID: 1336
	public class CopyExternalUserInfoByAccountIdOptions
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x00080A1E File Offset: 0x0007EC1E
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x00080A21 File Offset: 0x0007EC21
		// (set) Token: 0x06003892 RID: 14482 RVA: 0x00080A29 File Offset: 0x0007EC29
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06003893 RID: 14483 RVA: 0x00080A32 File Offset: 0x0007EC32
		// (set) Token: 0x06003894 RID: 14484 RVA: 0x00080A3A File Offset: 0x0007EC3A
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06003895 RID: 14485 RVA: 0x00080A43 File Offset: 0x0007EC43
		// (set) Token: 0x06003896 RID: 14486 RVA: 0x00080A4B File Offset: 0x0007EC4B
		public string AccountId { get; set; }
	}
}
