using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008E9 RID: 2281
	public class DeletePersistentAuthCallbackInfo
	{
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004FCE RID: 20430 RVA: 0x00097E1F File Offset: 0x0009601F
		// (set) Token: 0x06004FCF RID: 20431 RVA: 0x00097E27 File Offset: 0x00096027
		public Result ResultCode { get; set; }

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x00097E30 File Offset: 0x00096030
		// (set) Token: 0x06004FD1 RID: 20433 RVA: 0x00097E38 File Offset: 0x00096038
		public object ClientData { get; set; }
	}
}
