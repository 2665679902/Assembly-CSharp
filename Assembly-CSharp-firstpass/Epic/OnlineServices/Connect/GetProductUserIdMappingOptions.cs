using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008AA RID: 2218
	public class GetProductUserIdMappingOptions
	{
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004E69 RID: 20073 RVA: 0x00096B6B File Offset: 0x00094D6B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x00096B6E File Offset: 0x00094D6E
		// (set) Token: 0x06004E6B RID: 20075 RVA: 0x00096B76 File Offset: 0x00094D76
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x00096B7F File Offset: 0x00094D7F
		// (set) Token: 0x06004E6D RID: 20077 RVA: 0x00096B87 File Offset: 0x00094D87
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06004E6E RID: 20078 RVA: 0x00096B90 File Offset: 0x00094D90
		// (set) Token: 0x06004E6F RID: 20079 RVA: 0x00096B98 File Offset: 0x00094D98
		public ProductUserId TargetProductUserId { get; set; }
	}
}
