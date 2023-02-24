using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200074B RID: 1867
	public class KickMemberCallbackInfo
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x0008D8AB File Offset: 0x0008BAAB
		// (set) Token: 0x0600457A RID: 17786 RVA: 0x0008D8B3 File Offset: 0x0008BAB3
		public Result ResultCode { get; set; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x0008D8BC File Offset: 0x0008BABC
		// (set) Token: 0x0600457C RID: 17788 RVA: 0x0008D8C4 File Offset: 0x0008BAC4
		public object ClientData { get; set; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x0008D8CD File Offset: 0x0008BACD
		// (set) Token: 0x0600457E RID: 17790 RVA: 0x0008D8D5 File Offset: 0x0008BAD5
		public string LobbyId { get; set; }
	}
}
