using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200086F RID: 2159
	public class QueryEntitlementsOptions
	{
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004CE3 RID: 19683 RVA: 0x00094EE2 File Offset: 0x000930E2
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x00094EE5 File Offset: 0x000930E5
		// (set) Token: 0x06004CE5 RID: 19685 RVA: 0x00094EED File Offset: 0x000930ED
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004CE6 RID: 19686 RVA: 0x00094EF6 File Offset: 0x000930F6
		// (set) Token: 0x06004CE7 RID: 19687 RVA: 0x00094EFE File Offset: 0x000930FE
		public string[] EntitlementNames { get; set; }

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x00094F07 File Offset: 0x00093107
		// (set) Token: 0x06004CE9 RID: 19689 RVA: 0x00094F0F File Offset: 0x0009310F
		public bool IncludeRedeemed { get; set; }
	}
}
