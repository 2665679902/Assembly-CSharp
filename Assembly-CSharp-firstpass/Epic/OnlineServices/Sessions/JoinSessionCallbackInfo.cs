using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005EB RID: 1515
	public class JoinSessionCallbackInfo
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06003CF3 RID: 15603 RVA: 0x00084F9E File Offset: 0x0008319E
		// (set) Token: 0x06003CF4 RID: 15604 RVA: 0x00084FA6 File Offset: 0x000831A6
		public Result ResultCode { get; set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06003CF5 RID: 15605 RVA: 0x00084FAF File Offset: 0x000831AF
		// (set) Token: 0x06003CF6 RID: 15606 RVA: 0x00084FB7 File Offset: 0x000831B7
		public object ClientData { get; set; }
	}
}
