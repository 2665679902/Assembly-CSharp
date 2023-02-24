using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200059E RID: 1438
	public class ReadFileOptions
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x00082E38 File Offset: 0x00081038
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x00082E3B File Offset: 0x0008103B
		// (set) Token: 0x06003B0A RID: 15114 RVA: 0x00082E43 File Offset: 0x00081043
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x00082E4C File Offset: 0x0008104C
		// (set) Token: 0x06003B0C RID: 15116 RVA: 0x00082E54 File Offset: 0x00081054
		public string Filename { get; set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x00082E5D File Offset: 0x0008105D
		// (set) Token: 0x06003B0E RID: 15118 RVA: 0x00082E65 File Offset: 0x00081065
		public uint ReadChunkLengthBytes { get; set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x00082E6E File Offset: 0x0008106E
		// (set) Token: 0x06003B10 RID: 15120 RVA: 0x00082E76 File Offset: 0x00081076
		public OnReadFileDataCallback ReadFileDataCallback { get; set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x00082E7F File Offset: 0x0008107F
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x00082E87 File Offset: 0x00081087
		public OnFileTransferProgressCallback FileTransferProgressCallback { get; set; }
	}
}
