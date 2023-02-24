using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000578 RID: 1400
	public class CopyFileMetadataAtIndexOptions
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x00082354 File Offset: 0x00080554
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x00082357 File Offset: 0x00080557
		// (set) Token: 0x06003A2C RID: 14892 RVA: 0x0008235F File Offset: 0x0008055F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x00082368 File Offset: 0x00080568
		// (set) Token: 0x06003A2E RID: 14894 RVA: 0x00082370 File Offset: 0x00080570
		public uint Index { get; set; }
	}
}
