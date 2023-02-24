using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000596 RID: 1430
	public class QueryFileListOptions
	{
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x00082A5A File Offset: 0x00080C5A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x00082A5D File Offset: 0x00080C5D
		// (set) Token: 0x06003ACE RID: 15054 RVA: 0x00082A65 File Offset: 0x00080C65
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x00082A6E File Offset: 0x00080C6E
		// (set) Token: 0x06003AD0 RID: 15056 RVA: 0x00082A76 File Offset: 0x00080C76
		public string[] ListOfTags { get; set; }
	}
}
