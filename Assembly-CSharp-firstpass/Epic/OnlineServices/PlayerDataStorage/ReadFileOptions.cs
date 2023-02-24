using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C9 RID: 1737
	public class ReadFileOptions
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x00089FC4 File Offset: 0x000881C4
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x00089FC7 File Offset: 0x000881C7
		// (set) Token: 0x0600421D RID: 16925 RVA: 0x00089FCF File Offset: 0x000881CF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x00089FD8 File Offset: 0x000881D8
		// (set) Token: 0x0600421F RID: 16927 RVA: 0x00089FE0 File Offset: 0x000881E0
		public string Filename { get; set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x00089FE9 File Offset: 0x000881E9
		// (set) Token: 0x06004221 RID: 16929 RVA: 0x00089FF1 File Offset: 0x000881F1
		public uint ReadChunkLengthBytes { get; set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x00089FFA File Offset: 0x000881FA
		// (set) Token: 0x06004223 RID: 16931 RVA: 0x0008A002 File Offset: 0x00088202
		public OnReadFileDataCallback ReadFileDataCallback { get; set; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x0008A00B File Offset: 0x0008820B
		// (set) Token: 0x06004225 RID: 16933 RVA: 0x0008A013 File Offset: 0x00088213
		public OnFileTransferProgressCallback FileTransferProgressCallback { get; set; }
	}
}
