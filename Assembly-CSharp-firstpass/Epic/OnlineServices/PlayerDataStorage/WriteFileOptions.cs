using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006D0 RID: 1744
	public class WriteFileOptions
	{
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600424F RID: 16975 RVA: 0x0008A2F2 File Offset: 0x000884F2
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x0008A2F5 File Offset: 0x000884F5
		// (set) Token: 0x06004251 RID: 16977 RVA: 0x0008A2FD File Offset: 0x000884FD
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06004252 RID: 16978 RVA: 0x0008A306 File Offset: 0x00088506
		// (set) Token: 0x06004253 RID: 16979 RVA: 0x0008A30E File Offset: 0x0008850E
		public string Filename { get; set; }

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x0008A317 File Offset: 0x00088517
		// (set) Token: 0x06004255 RID: 16981 RVA: 0x0008A31F File Offset: 0x0008851F
		public uint ChunkLengthBytes { get; set; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06004256 RID: 16982 RVA: 0x0008A328 File Offset: 0x00088528
		// (set) Token: 0x06004257 RID: 16983 RVA: 0x0008A330 File Offset: 0x00088530
		public OnWriteFileDataCallback WriteFileDataCallback { get; set; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06004258 RID: 16984 RVA: 0x0008A339 File Offset: 0x00088539
		// (set) Token: 0x06004259 RID: 16985 RVA: 0x0008A341 File Offset: 0x00088541
		public OnFileTransferProgressCallback FileTransferProgressCallback { get; set; }
	}
}
