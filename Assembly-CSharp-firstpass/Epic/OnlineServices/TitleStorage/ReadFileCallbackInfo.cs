using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200059A RID: 1434
	public class ReadFileCallbackInfo
	{
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x00082C03 File Offset: 0x00080E03
		// (set) Token: 0x06003AE7 RID: 15079 RVA: 0x00082C0B File Offset: 0x00080E0B
		public Result ResultCode { get; set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x00082C14 File Offset: 0x00080E14
		// (set) Token: 0x06003AE9 RID: 15081 RVA: 0x00082C1C File Offset: 0x00080E1C
		public object ClientData { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x00082C25 File Offset: 0x00080E25
		// (set) Token: 0x06003AEB RID: 15083 RVA: 0x00082C2D File Offset: 0x00080E2D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x00082C36 File Offset: 0x00080E36
		// (set) Token: 0x06003AED RID: 15085 RVA: 0x00082C3E File Offset: 0x00080E3E
		public string Filename { get; set; }
	}
}
