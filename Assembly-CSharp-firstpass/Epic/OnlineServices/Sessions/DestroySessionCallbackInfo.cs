using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D9 RID: 1497
	public class DestroySessionCallbackInfo
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06003C9A RID: 15514 RVA: 0x00084A57 File Offset: 0x00082C57
		// (set) Token: 0x06003C9B RID: 15515 RVA: 0x00084A5F File Offset: 0x00082C5F
		public Result ResultCode { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06003C9C RID: 15516 RVA: 0x00084A68 File Offset: 0x00082C68
		// (set) Token: 0x06003C9D RID: 15517 RVA: 0x00084A70 File Offset: 0x00082C70
		public object ClientData { get; set; }
	}
}
