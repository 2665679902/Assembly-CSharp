using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000699 RID: 1689
	public class CopyFileMetadataByFilenameOptions
	{
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x00088EC7 File Offset: 0x000870C7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x00088ECA File Offset: 0x000870CA
		// (set) Token: 0x060040F2 RID: 16626 RVA: 0x00088ED2 File Offset: 0x000870D2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x00088EDB File Offset: 0x000870DB
		// (set) Token: 0x060040F4 RID: 16628 RVA: 0x00088EE3 File Offset: 0x000870E3
		public string Filename { get; set; }
	}
}
