using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006CE RID: 1742
	public class WriteFileDataCallbackInfo
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x0008A20E File Offset: 0x0008840E
		// (set) Token: 0x06004242 RID: 16962 RVA: 0x0008A216 File Offset: 0x00088416
		public object ClientData { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x0008A21F File Offset: 0x0008841F
		// (set) Token: 0x06004244 RID: 16964 RVA: 0x0008A227 File Offset: 0x00088427
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06004245 RID: 16965 RVA: 0x0008A230 File Offset: 0x00088430
		// (set) Token: 0x06004246 RID: 16966 RVA: 0x0008A238 File Offset: 0x00088438
		public string Filename { get; set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x0008A241 File Offset: 0x00088441
		// (set) Token: 0x06004248 RID: 16968 RVA: 0x0008A249 File Offset: 0x00088449
		public uint DataBufferLengthBytes { get; set; }
	}
}
