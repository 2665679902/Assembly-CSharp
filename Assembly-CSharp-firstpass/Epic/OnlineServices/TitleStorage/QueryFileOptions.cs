using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000598 RID: 1432
	public class QueryFileOptions
	{
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x00082B3B File Offset: 0x00080D3B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x00082B3E File Offset: 0x00080D3E
		// (set) Token: 0x06003ADB RID: 15067 RVA: 0x00082B46 File Offset: 0x00080D46
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x00082B4F File Offset: 0x00080D4F
		// (set) Token: 0x06003ADD RID: 15069 RVA: 0x00082B57 File Offset: 0x00080D57
		public string Filename { get; set; }
	}
}
