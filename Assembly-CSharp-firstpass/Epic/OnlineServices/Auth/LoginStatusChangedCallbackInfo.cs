using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008F7 RID: 2295
	public class LoginStatusChangedCallbackInfo
	{
		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x0600501F RID: 20511 RVA: 0x00098333 File Offset: 0x00096533
		// (set) Token: 0x06005020 RID: 20512 RVA: 0x0009833B File Offset: 0x0009653B
		public object ClientData { get; set; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06005021 RID: 20513 RVA: 0x00098344 File Offset: 0x00096544
		// (set) Token: 0x06005022 RID: 20514 RVA: 0x0009834C File Offset: 0x0009654C
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06005023 RID: 20515 RVA: 0x00098355 File Offset: 0x00096555
		// (set) Token: 0x06005024 RID: 20516 RVA: 0x0009835D File Offset: 0x0009655D
		public LoginStatus PrevStatus { get; set; }

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x00098366 File Offset: 0x00096566
		// (set) Token: 0x06005026 RID: 20518 RVA: 0x0009836E File Offset: 0x0009656E
		public LoginStatus CurrentStatus { get; set; }
	}
}
