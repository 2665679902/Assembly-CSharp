using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C1 RID: 1729
	public class QueryFileListOptions
	{
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060041E3 RID: 16867 RVA: 0x00089C42 File Offset: 0x00087E42
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x00089C45 File Offset: 0x00087E45
		// (set) Token: 0x060041E5 RID: 16869 RVA: 0x00089C4D File Offset: 0x00087E4D
		public ProductUserId LocalUserId { get; set; }
	}
}
