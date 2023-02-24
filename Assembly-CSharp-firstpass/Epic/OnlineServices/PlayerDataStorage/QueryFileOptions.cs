using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C3 RID: 1731
	public class QueryFileOptions
	{
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x00089CC7 File Offset: 0x00087EC7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x00089CCA File Offset: 0x00087ECA
		// (set) Token: 0x060041EE RID: 16878 RVA: 0x00089CD2 File Offset: 0x00087ED2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060041EF RID: 16879 RVA: 0x00089CDB File Offset: 0x00087EDB
		// (set) Token: 0x060041F0 RID: 16880 RVA: 0x00089CE3 File Offset: 0x00087EE3
		public string Filename { get; set; }
	}
}
