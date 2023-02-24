using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200073F RID: 1855
	public class DestroyLobbyOptions
	{
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600452F RID: 17711 RVA: 0x0008D42A File Offset: 0x0008B62A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x0008D42D File Offset: 0x0008B62D
		// (set) Token: 0x06004531 RID: 17713 RVA: 0x0008D435 File Offset: 0x0008B635
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x0008D43E File Offset: 0x0008B63E
		// (set) Token: 0x06004533 RID: 17715 RVA: 0x0008D446 File Offset: 0x0008B646
		public string LobbyId { get; set; }
	}
}
