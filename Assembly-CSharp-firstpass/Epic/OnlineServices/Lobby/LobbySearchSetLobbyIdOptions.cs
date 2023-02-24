using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000790 RID: 1936
	public class LobbySearchSetLobbyIdOptions
	{
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600474B RID: 18251 RVA: 0x0008FCDB File Offset: 0x0008DEDB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600474C RID: 18252 RVA: 0x0008FCDE File Offset: 0x0008DEDE
		// (set) Token: 0x0600474D RID: 18253 RVA: 0x0008FCE6 File Offset: 0x0008DEE6
		public string LobbyId { get; set; }
	}
}
