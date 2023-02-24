using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200084E RID: 2126
	public class GetEntitlementsCountOptions
	{
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x0009476B File Offset: 0x0009296B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06004C38 RID: 19512 RVA: 0x0009476E File Offset: 0x0009296E
		// (set) Token: 0x06004C39 RID: 19513 RVA: 0x00094776 File Offset: 0x00092976
		public EpicAccountId LocalUserId { get; set; }
	}
}
