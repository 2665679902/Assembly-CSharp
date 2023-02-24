using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008F9 RID: 2297
	public class LogoutCallbackInfo
	{
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x0600502D RID: 20525 RVA: 0x00098416 File Offset: 0x00096616
		// (set) Token: 0x0600502E RID: 20526 RVA: 0x0009841E File Offset: 0x0009661E
		public Result ResultCode { get; set; }

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x00098427 File Offset: 0x00096627
		// (set) Token: 0x06005030 RID: 20528 RVA: 0x0009842F File Offset: 0x0009662F
		public object ClientData { get; set; }

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x00098438 File Offset: 0x00096638
		// (set) Token: 0x06005032 RID: 20530 RVA: 0x00098440 File Offset: 0x00096640
		public EpicAccountId LocalUserId { get; set; }
	}
}
