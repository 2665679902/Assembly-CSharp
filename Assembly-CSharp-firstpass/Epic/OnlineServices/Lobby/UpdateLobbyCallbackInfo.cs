using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007CA RID: 1994
	public class UpdateLobbyCallbackInfo
	{
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600485F RID: 18527 RVA: 0x000905DB File Offset: 0x0008E7DB
		// (set) Token: 0x06004860 RID: 18528 RVA: 0x000905E3 File Offset: 0x0008E7E3
		public Result ResultCode { get; set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06004861 RID: 18529 RVA: 0x000905EC File Offset: 0x0008E7EC
		// (set) Token: 0x06004862 RID: 18530 RVA: 0x000905F4 File Offset: 0x0008E7F4
		public object ClientData { get; set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x000905FD File Offset: 0x0008E7FD
		// (set) Token: 0x06004864 RID: 18532 RVA: 0x00090605 File Offset: 0x0008E805
		public string LobbyId { get; set; }
	}
}
