using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000580 RID: 1408
	public class FileMetadata
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x0008261B File Offset: 0x0008081B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x0008261E File Offset: 0x0008081E
		// (set) Token: 0x06003A5A RID: 14938 RVA: 0x00082626 File Offset: 0x00080826
		public uint FileSizeBytes { get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x0008262F File Offset: 0x0008082F
		// (set) Token: 0x06003A5C RID: 14940 RVA: 0x00082637 File Offset: 0x00080837
		public string MD5Hash { get; set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x00082640 File Offset: 0x00080840
		// (set) Token: 0x06003A5E RID: 14942 RVA: 0x00082648 File Offset: 0x00080848
		public string Filename { get; set; }
	}
}
