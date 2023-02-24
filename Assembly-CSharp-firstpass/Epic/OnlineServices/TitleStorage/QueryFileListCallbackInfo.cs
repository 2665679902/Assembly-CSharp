using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000594 RID: 1428
	public class QueryFileListCallbackInfo
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x00082976 File Offset: 0x00080B76
		// (set) Token: 0x06003ABF RID: 15039 RVA: 0x0008297E File Offset: 0x00080B7E
		public Result ResultCode { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x00082987 File Offset: 0x00080B87
		// (set) Token: 0x06003AC1 RID: 15041 RVA: 0x0008298F File Offset: 0x00080B8F
		public object ClientData { get; set; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x00082998 File Offset: 0x00080B98
		// (set) Token: 0x06003AC3 RID: 15043 RVA: 0x000829A0 File Offset: 0x00080BA0
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000829A9 File Offset: 0x00080BA9
		// (set) Token: 0x06003AC5 RID: 15045 RVA: 0x000829B1 File Offset: 0x00080BB1
		public uint FileCount { get; set; }
	}
}
