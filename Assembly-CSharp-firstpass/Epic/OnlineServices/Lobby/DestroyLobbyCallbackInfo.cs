using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200073D RID: 1853
	public class DestroyLobbyCallbackInfo
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x0008D37B File Offset: 0x0008B57B
		// (set) Token: 0x06004525 RID: 17701 RVA: 0x0008D383 File Offset: 0x0008B583
		public Result ResultCode { get; set; }

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x0008D38C File Offset: 0x0008B58C
		// (set) Token: 0x06004527 RID: 17703 RVA: 0x0008D394 File Offset: 0x0008B594
		public object ClientData { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x0008D39D File Offset: 0x0008B59D
		// (set) Token: 0x06004529 RID: 17705 RVA: 0x0008D3A5 File Offset: 0x0008B5A5
		public string LobbyId { get; set; }
	}
}
