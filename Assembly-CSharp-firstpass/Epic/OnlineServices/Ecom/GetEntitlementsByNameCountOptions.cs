using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200084C RID: 2124
	public class GetEntitlementsByNameCountOptions
	{
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06004C2A RID: 19498 RVA: 0x000946A3 File Offset: 0x000928A3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x000946A6 File Offset: 0x000928A6
		// (set) Token: 0x06004C2C RID: 19500 RVA: 0x000946AE File Offset: 0x000928AE
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x000946B7 File Offset: 0x000928B7
		// (set) Token: 0x06004C2E RID: 19502 RVA: 0x000946BF File Offset: 0x000928BF
		public string EntitlementName { get; set; }
	}
}
