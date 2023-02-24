using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000873 RID: 2163
	public class QueryOffersOptions
	{
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x000950B6 File Offset: 0x000932B6
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06004D00 RID: 19712 RVA: 0x000950B9 File Offset: 0x000932B9
		// (set) Token: 0x06004D01 RID: 19713 RVA: 0x000950C1 File Offset: 0x000932C1
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06004D02 RID: 19714 RVA: 0x000950CA File Offset: 0x000932CA
		// (set) Token: 0x06004D03 RID: 19715 RVA: 0x000950D2 File Offset: 0x000932D2
		public string OverrideCatalogNamespace { get; set; }
	}
}
