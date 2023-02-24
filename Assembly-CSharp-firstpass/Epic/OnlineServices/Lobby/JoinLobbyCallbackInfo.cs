using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000747 RID: 1863
	public class JoinLobbyCallbackInfo
	{
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600455D RID: 17757 RVA: 0x0008D6EE File Offset: 0x0008B8EE
		// (set) Token: 0x0600455E RID: 17758 RVA: 0x0008D6F6 File Offset: 0x0008B8F6
		public Result ResultCode { get; set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x0008D6FF File Offset: 0x0008B8FF
		// (set) Token: 0x06004560 RID: 17760 RVA: 0x0008D707 File Offset: 0x0008B907
		public object ClientData { get; set; }

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x0008D710 File Offset: 0x0008B910
		// (set) Token: 0x06004562 RID: 17762 RVA: 0x0008D718 File Offset: 0x0008B918
		public string LobbyId { get; set; }
	}
}
