using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200076E RID: 1902
	public class LobbyInviteReceivedCallbackInfo
	{
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x0008F08E File Offset: 0x0008D28E
		// (set) Token: 0x06004690 RID: 18064 RVA: 0x0008F096 File Offset: 0x0008D296
		public object ClientData { get; set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06004691 RID: 18065 RVA: 0x0008F09F File Offset: 0x0008D29F
		// (set) Token: 0x06004692 RID: 18066 RVA: 0x0008F0A7 File Offset: 0x0008D2A7
		public string InviteId { get; set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x0008F0B0 File Offset: 0x0008D2B0
		// (set) Token: 0x06004694 RID: 18068 RVA: 0x0008F0B8 File Offset: 0x0008D2B8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x0008F0C1 File Offset: 0x0008D2C1
		// (set) Token: 0x06004696 RID: 18070 RVA: 0x0008F0C9 File Offset: 0x0008D2C9
		public ProductUserId TargetUserId { get; set; }
	}
}
