using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000749 RID: 1865
	public class JoinLobbyOptions
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x0008D79E File Offset: 0x0008B99E
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x0008D7A1 File Offset: 0x0008B9A1
		// (set) Token: 0x0600456A RID: 17770 RVA: 0x0008D7A9 File Offset: 0x0008B9A9
		public LobbyDetails LobbyDetailsHandle { get; set; }

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x0008D7B2 File Offset: 0x0008B9B2
		// (set) Token: 0x0600456C RID: 17772 RVA: 0x0008D7BA File Offset: 0x0008B9BA
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x0008D7C3 File Offset: 0x0008B9C3
		// (set) Token: 0x0600456E RID: 17774 RVA: 0x0008D7CB File Offset: 0x0008B9CB
		public bool PresenceEnabled { get; set; }
	}
}
