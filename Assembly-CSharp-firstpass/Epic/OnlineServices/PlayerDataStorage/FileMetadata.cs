using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006A3 RID: 1699
	public class FileMetadata
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06004131 RID: 16689 RVA: 0x000892C3 File Offset: 0x000874C3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x000892C6 File Offset: 0x000874C6
		// (set) Token: 0x06004133 RID: 16691 RVA: 0x000892CE File Offset: 0x000874CE
		public uint FileSizeBytes { get; set; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06004134 RID: 16692 RVA: 0x000892D7 File Offset: 0x000874D7
		// (set) Token: 0x06004135 RID: 16693 RVA: 0x000892DF File Offset: 0x000874DF
		public string MD5Hash { get; set; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06004136 RID: 16694 RVA: 0x000892E8 File Offset: 0x000874E8
		// (set) Token: 0x06004137 RID: 16695 RVA: 0x000892F0 File Offset: 0x000874F0
		public string Filename { get; set; }
	}
}
