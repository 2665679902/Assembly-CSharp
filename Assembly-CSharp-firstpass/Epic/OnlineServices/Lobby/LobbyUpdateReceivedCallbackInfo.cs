using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000798 RID: 1944
	public class LobbyUpdateReceivedCallbackInfo
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06004773 RID: 18291 RVA: 0x0008FF3B File Offset: 0x0008E13B
		// (set) Token: 0x06004774 RID: 18292 RVA: 0x0008FF43 File Offset: 0x0008E143
		public object ClientData { get; set; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06004775 RID: 18293 RVA: 0x0008FF4C File Offset: 0x0008E14C
		// (set) Token: 0x06004776 RID: 18294 RVA: 0x0008FF54 File Offset: 0x0008E154
		public string LobbyId { get; set; }
	}
}
