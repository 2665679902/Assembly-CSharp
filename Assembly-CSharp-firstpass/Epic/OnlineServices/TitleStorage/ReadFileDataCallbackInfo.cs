using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200059C RID: 1436
	public class ReadFileDataCallbackInfo
	{
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x00082CE6 File Offset: 0x00080EE6
		// (set) Token: 0x06003AF5 RID: 15093 RVA: 0x00082CEE File Offset: 0x00080EEE
		public object ClientData { get; set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x00082CF7 File Offset: 0x00080EF7
		// (set) Token: 0x06003AF7 RID: 15095 RVA: 0x00082CFF File Offset: 0x00080EFF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x00082D08 File Offset: 0x00080F08
		// (set) Token: 0x06003AF9 RID: 15097 RVA: 0x00082D10 File Offset: 0x00080F10
		public string Filename { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x00082D19 File Offset: 0x00080F19
		// (set) Token: 0x06003AFB RID: 15099 RVA: 0x00082D21 File Offset: 0x00080F21
		public uint TotalFileSizeBytes { get; set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x00082D2A File Offset: 0x00080F2A
		// (set) Token: 0x06003AFD RID: 15101 RVA: 0x00082D32 File Offset: 0x00080F32
		public bool IsLastChunk { get; set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x00082D3B File Offset: 0x00080F3B
		// (set) Token: 0x06003AFF RID: 15103 RVA: 0x00082D43 File Offset: 0x00080F43
		public byte[] DataChunk { get; set; }
	}
}
