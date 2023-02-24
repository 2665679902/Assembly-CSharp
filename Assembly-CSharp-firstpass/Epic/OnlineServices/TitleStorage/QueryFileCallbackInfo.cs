using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000592 RID: 1426
	public class QueryFileCallbackInfo
	{
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x000828C7 File Offset: 0x00080AC7
		// (set) Token: 0x06003AB4 RID: 15028 RVA: 0x000828CF File Offset: 0x00080ACF
		public Result ResultCode { get; set; }

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x000828D8 File Offset: 0x00080AD8
		// (set) Token: 0x06003AB6 RID: 15030 RVA: 0x000828E0 File Offset: 0x00080AE0
		public object ClientData { get; set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x000828E9 File Offset: 0x00080AE9
		// (set) Token: 0x06003AB8 RID: 15032 RVA: 0x000828F1 File Offset: 0x00080AF1
		public ProductUserId LocalUserId { get; set; }
	}
}
