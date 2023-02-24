using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000877 RID: 2167
	public class QueryOwnershipOptions
	{
		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06004D1A RID: 19738 RVA: 0x00095268 File Offset: 0x00093468
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06004D1B RID: 19739 RVA: 0x0009526B File Offset: 0x0009346B
		// (set) Token: 0x06004D1C RID: 19740 RVA: 0x00095273 File Offset: 0x00093473
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06004D1D RID: 19741 RVA: 0x0009527C File Offset: 0x0009347C
		// (set) Token: 0x06004D1E RID: 19742 RVA: 0x00095284 File Offset: 0x00093484
		public string[] CatalogItemIds { get; set; }

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06004D1F RID: 19743 RVA: 0x0009528D File Offset: 0x0009348D
		// (set) Token: 0x06004D20 RID: 19744 RVA: 0x00095295 File Offset: 0x00093495
		public string CatalogNamespace { get; set; }
	}
}
