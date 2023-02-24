using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C6 RID: 1990
	public class SendInviteCallbackInfo
	{
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06004843 RID: 18499 RVA: 0x0009041F File Offset: 0x0008E61F
		// (set) Token: 0x06004844 RID: 18500 RVA: 0x00090427 File Offset: 0x0008E627
		public Result ResultCode { get; set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06004845 RID: 18501 RVA: 0x00090430 File Offset: 0x0008E630
		// (set) Token: 0x06004846 RID: 18502 RVA: 0x00090438 File Offset: 0x0008E638
		public object ClientData { get; set; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06004847 RID: 18503 RVA: 0x00090441 File Offset: 0x0008E641
		// (set) Token: 0x06004848 RID: 18504 RVA: 0x00090449 File Offset: 0x0008E649
		public string LobbyId { get; set; }
	}
}
