using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000582 RID: 1410
	public class FileTransferProgressCallbackInfo
	{
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x0008272B File Offset: 0x0008092B
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x00082733 File Offset: 0x00080933
		public object ClientData { get; set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x0008273C File Offset: 0x0008093C
		// (set) Token: 0x06003A6C RID: 14956 RVA: 0x00082744 File Offset: 0x00080944
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x0008274D File Offset: 0x0008094D
		// (set) Token: 0x06003A6E RID: 14958 RVA: 0x00082755 File Offset: 0x00080955
		public string Filename { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x0008275E File Offset: 0x0008095E
		// (set) Token: 0x06003A70 RID: 14960 RVA: 0x00082766 File Offset: 0x00080966
		public uint BytesTransferred { get; set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x0008276F File Offset: 0x0008096F
		// (set) Token: 0x06003A72 RID: 14962 RVA: 0x00082777 File Offset: 0x00080977
		public uint TotalFileSizeBytes { get; set; }
	}
}
