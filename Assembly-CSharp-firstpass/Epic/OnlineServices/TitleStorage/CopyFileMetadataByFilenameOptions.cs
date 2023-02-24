using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200057A RID: 1402
	public class CopyFileMetadataByFilenameOptions
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06003A37 RID: 14903 RVA: 0x0008241F File Offset: 0x0008061F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x00082422 File Offset: 0x00080622
		// (set) Token: 0x06003A39 RID: 14905 RVA: 0x0008242A File Offset: 0x0008062A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06003A3A RID: 14906 RVA: 0x00082433 File Offset: 0x00080633
		// (set) Token: 0x06003A3B RID: 14907 RVA: 0x0008243B File Offset: 0x0008063B
		public string Filename { get; set; }
	}
}
