using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200074F RID: 1871
	public class LeaveLobbyCallbackInfo
	{
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06004595 RID: 17813 RVA: 0x0008DA67 File Offset: 0x0008BC67
		// (set) Token: 0x06004596 RID: 17814 RVA: 0x0008DA6F File Offset: 0x0008BC6F
		public Result ResultCode { get; set; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06004597 RID: 17815 RVA: 0x0008DA78 File Offset: 0x0008BC78
		// (set) Token: 0x06004598 RID: 17816 RVA: 0x0008DA80 File Offset: 0x0008BC80
		public object ClientData { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x0008DA89 File Offset: 0x0008BC89
		// (set) Token: 0x0600459A RID: 17818 RVA: 0x0008DA91 File Offset: 0x0008BC91
		public string LobbyId { get; set; }
	}
}
