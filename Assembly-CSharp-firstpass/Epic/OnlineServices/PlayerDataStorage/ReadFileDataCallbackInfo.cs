using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C7 RID: 1735
	public class ReadFileDataCallbackInfo
	{
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06004207 RID: 16903 RVA: 0x00089E72 File Offset: 0x00088072
		// (set) Token: 0x06004208 RID: 16904 RVA: 0x00089E7A File Offset: 0x0008807A
		public object ClientData { get; set; }

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06004209 RID: 16905 RVA: 0x00089E83 File Offset: 0x00088083
		// (set) Token: 0x0600420A RID: 16906 RVA: 0x00089E8B File Offset: 0x0008808B
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x00089E94 File Offset: 0x00088094
		// (set) Token: 0x0600420C RID: 16908 RVA: 0x00089E9C File Offset: 0x0008809C
		public string Filename { get; set; }

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600420D RID: 16909 RVA: 0x00089EA5 File Offset: 0x000880A5
		// (set) Token: 0x0600420E RID: 16910 RVA: 0x00089EAD File Offset: 0x000880AD
		public uint TotalFileSizeBytes { get; set; }

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x00089EB6 File Offset: 0x000880B6
		// (set) Token: 0x06004210 RID: 16912 RVA: 0x00089EBE File Offset: 0x000880BE
		public bool IsLastChunk { get; set; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x00089EC7 File Offset: 0x000880C7
		// (set) Token: 0x06004212 RID: 16914 RVA: 0x00089ECF File Offset: 0x000880CF
		public byte[] DataChunk { get; set; }
	}
}
