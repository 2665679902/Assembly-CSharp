using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008B0 RID: 2224
	public class LoginCallbackInfo
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x00096DF3 File Offset: 0x00094FF3
		// (set) Token: 0x06004E93 RID: 20115 RVA: 0x00096DFB File Offset: 0x00094FFB
		public Result ResultCode { get; set; }

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x00096E04 File Offset: 0x00095004
		// (set) Token: 0x06004E95 RID: 20117 RVA: 0x00096E0C File Offset: 0x0009500C
		public object ClientData { get; set; }

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004E96 RID: 20118 RVA: 0x00096E15 File Offset: 0x00095015
		// (set) Token: 0x06004E97 RID: 20119 RVA: 0x00096E1D File Offset: 0x0009501D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x00096E26 File Offset: 0x00095026
		// (set) Token: 0x06004E99 RID: 20121 RVA: 0x00096E2E File Offset: 0x0009502E
		public ContinuanceToken ContinuanceToken { get; set; }
	}
}
