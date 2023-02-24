using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008A6 RID: 2214
	public class GetExternalAccountMappingsOptions
	{
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004E4F RID: 20047 RVA: 0x000969D7 File Offset: 0x00094BD7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004E50 RID: 20048 RVA: 0x000969DA File Offset: 0x00094BDA
		// (set) Token: 0x06004E51 RID: 20049 RVA: 0x000969E2 File Offset: 0x00094BE2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004E52 RID: 20050 RVA: 0x000969EB File Offset: 0x00094BEB
		// (set) Token: 0x06004E53 RID: 20051 RVA: 0x000969F3 File Offset: 0x00094BF3
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x000969FC File Offset: 0x00094BFC
		// (set) Token: 0x06004E55 RID: 20053 RVA: 0x00096A04 File Offset: 0x00094C04
		public string TargetExternalUserId { get; set; }
	}
}
