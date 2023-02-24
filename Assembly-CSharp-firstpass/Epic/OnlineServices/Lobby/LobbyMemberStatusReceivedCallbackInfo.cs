using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000771 RID: 1905
	public class LobbyMemberStatusReceivedCallbackInfo
	{
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x0008F172 File Offset: 0x0008D372
		// (set) Token: 0x0600469E RID: 18078 RVA: 0x0008F17A File Offset: 0x0008D37A
		public object ClientData { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x0008F183 File Offset: 0x0008D383
		// (set) Token: 0x060046A0 RID: 18080 RVA: 0x0008F18B File Offset: 0x0008D38B
		public string LobbyId { get; set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x0008F194 File Offset: 0x0008D394
		// (set) Token: 0x060046A2 RID: 18082 RVA: 0x0008F19C File Offset: 0x0008D39C
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x0008F1A5 File Offset: 0x0008D3A5
		// (set) Token: 0x060046A4 RID: 18084 RVA: 0x0008F1AD File Offset: 0x0008D3AD
		public LobbyMemberStatus CurrentStatus { get; set; }
	}
}
