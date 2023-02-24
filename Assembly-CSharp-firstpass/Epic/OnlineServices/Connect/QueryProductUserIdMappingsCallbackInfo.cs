using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D0 RID: 2256
	public class QueryProductUserIdMappingsCallbackInfo
	{
		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06004F2F RID: 20271 RVA: 0x0009726F File Offset: 0x0009546F
		// (set) Token: 0x06004F30 RID: 20272 RVA: 0x00097277 File Offset: 0x00095477
		public Result ResultCode { get; set; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06004F31 RID: 20273 RVA: 0x00097280 File Offset: 0x00095480
		// (set) Token: 0x06004F32 RID: 20274 RVA: 0x00097288 File Offset: 0x00095488
		public object ClientData { get; set; }

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x00097291 File Offset: 0x00095491
		// (set) Token: 0x06004F34 RID: 20276 RVA: 0x00097299 File Offset: 0x00095499
		public ProductUserId LocalUserId { get; set; }
	}
}
