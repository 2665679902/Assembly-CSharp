using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200086D RID: 2157
	public class QueryEntitlementsCallbackInfo
	{
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004CD8 RID: 19672 RVA: 0x00094E33 File Offset: 0x00093033
		// (set) Token: 0x06004CD9 RID: 19673 RVA: 0x00094E3B File Offset: 0x0009303B
		public Result ResultCode { get; set; }

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06004CDA RID: 19674 RVA: 0x00094E44 File Offset: 0x00093044
		// (set) Token: 0x06004CDB RID: 19675 RVA: 0x00094E4C File Offset: 0x0009304C
		public object ClientData { get; set; }

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x00094E55 File Offset: 0x00093055
		// (set) Token: 0x06004CDD RID: 19677 RVA: 0x00094E5D File Offset: 0x0009305D
		public EpicAccountId LocalUserId { get; set; }
	}
}
