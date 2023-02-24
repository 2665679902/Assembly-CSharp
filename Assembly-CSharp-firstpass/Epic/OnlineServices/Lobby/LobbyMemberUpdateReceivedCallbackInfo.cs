using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000773 RID: 1907
	public class LobbyMemberUpdateReceivedCallbackInfo
	{
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060046AB RID: 18091 RVA: 0x0008F256 File Offset: 0x0008D456
		// (set) Token: 0x060046AC RID: 18092 RVA: 0x0008F25E File Offset: 0x0008D45E
		public object ClientData { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060046AD RID: 18093 RVA: 0x0008F267 File Offset: 0x0008D467
		// (set) Token: 0x060046AE RID: 18094 RVA: 0x0008F26F File Offset: 0x0008D46F
		public string LobbyId { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060046AF RID: 18095 RVA: 0x0008F278 File Offset: 0x0008D478
		// (set) Token: 0x060046B0 RID: 18096 RVA: 0x0008F280 File Offset: 0x0008D480
		public ProductUserId TargetUserId { get; set; }
	}
}
