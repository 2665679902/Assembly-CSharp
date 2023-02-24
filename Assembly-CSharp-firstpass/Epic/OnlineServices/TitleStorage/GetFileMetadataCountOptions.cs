using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000584 RID: 1412
	public class GetFileMetadataCountOptions
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x00082842 File Offset: 0x00080A42
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x00082845 File Offset: 0x00080A45
		// (set) Token: 0x06003A7C RID: 14972 RVA: 0x0008284D File Offset: 0x00080A4D
		public ProductUserId LocalUserId { get; set; }
	}
}
