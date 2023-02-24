using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007CC RID: 1996
	public class UpdateLobbyModificationOptions
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x0009068A File Offset: 0x0008E88A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x0009068D File Offset: 0x0008E88D
		// (set) Token: 0x0600486C RID: 18540 RVA: 0x00090695 File Offset: 0x0008E895
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600486D RID: 18541 RVA: 0x0009069E File Offset: 0x0008E89E
		// (set) Token: 0x0600486E RID: 18542 RVA: 0x000906A6 File Offset: 0x0008E8A6
		public string LobbyId { get; set; }
	}
}
