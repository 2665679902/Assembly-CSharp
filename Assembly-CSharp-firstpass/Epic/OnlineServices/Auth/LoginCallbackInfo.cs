using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008F2 RID: 2290
	public class LoginCallbackInfo
	{
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06004FFE RID: 20478 RVA: 0x0009810F File Offset: 0x0009630F
		// (set) Token: 0x06004FFF RID: 20479 RVA: 0x00098117 File Offset: 0x00096317
		public Result ResultCode { get; set; }

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06005000 RID: 20480 RVA: 0x00098120 File Offset: 0x00096320
		// (set) Token: 0x06005001 RID: 20481 RVA: 0x00098128 File Offset: 0x00096328
		public object ClientData { get; set; }

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06005002 RID: 20482 RVA: 0x00098131 File Offset: 0x00096331
		// (set) Token: 0x06005003 RID: 20483 RVA: 0x00098139 File Offset: 0x00096339
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06005004 RID: 20484 RVA: 0x00098142 File Offset: 0x00096342
		// (set) Token: 0x06005005 RID: 20485 RVA: 0x0009814A File Offset: 0x0009634A
		public PinGrantInfo PinGrantInfo { get; set; }

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06005006 RID: 20486 RVA: 0x00098153 File Offset: 0x00096353
		// (set) Token: 0x06005007 RID: 20487 RVA: 0x0009815B File Offset: 0x0009635B
		public ContinuanceToken ContinuanceToken { get; set; }

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06005008 RID: 20488 RVA: 0x00098164 File Offset: 0x00096364
		// (set) Token: 0x06005009 RID: 20489 RVA: 0x0009816C File Offset: 0x0009636C
		public AccountFeatureRestrictedInfo AccountFeatureRestrictedInfo { get; set; }
	}
}
