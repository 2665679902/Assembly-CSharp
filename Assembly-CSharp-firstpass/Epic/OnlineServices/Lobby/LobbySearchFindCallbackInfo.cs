using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000786 RID: 1926
	public class LobbySearchFindCallbackInfo
	{
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x0008FAD3 File Offset: 0x0008DCD3
		// (set) Token: 0x06004721 RID: 18209 RVA: 0x0008FADB File Offset: 0x0008DCDB
		public Result ResultCode { get; set; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06004722 RID: 18210 RVA: 0x0008FAE4 File Offset: 0x0008DCE4
		// (set) Token: 0x06004723 RID: 18211 RVA: 0x0008FAEC File Offset: 0x0008DCEC
		public object ClientData { get; set; }
	}
}
