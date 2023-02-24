using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000871 RID: 2161
	public class QueryOffersCallbackInfo
	{
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x00095007 File Offset: 0x00093207
		// (set) Token: 0x06004CF5 RID: 19701 RVA: 0x0009500F File Offset: 0x0009320F
		public Result ResultCode { get; set; }

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004CF6 RID: 19702 RVA: 0x00095018 File Offset: 0x00093218
		// (set) Token: 0x06004CF7 RID: 19703 RVA: 0x00095020 File Offset: 0x00093220
		public object ClientData { get; set; }

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004CF8 RID: 19704 RVA: 0x00095029 File Offset: 0x00093229
		// (set) Token: 0x06004CF9 RID: 19705 RVA: 0x00095031 File Offset: 0x00093231
		public EpicAccountId LocalUserId { get; set; }
	}
}
