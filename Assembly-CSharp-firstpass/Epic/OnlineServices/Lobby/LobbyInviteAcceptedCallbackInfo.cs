using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200076C RID: 1900
	public class LobbyInviteAcceptedCallbackInfo
	{
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x0008EFAC File Offset: 0x0008D1AC
		// (set) Token: 0x06004682 RID: 18050 RVA: 0x0008EFB4 File Offset: 0x0008D1B4
		public object ClientData { get; set; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06004683 RID: 18051 RVA: 0x0008EFBD File Offset: 0x0008D1BD
		// (set) Token: 0x06004684 RID: 18052 RVA: 0x0008EFC5 File Offset: 0x0008D1C5
		public string InviteId { get; set; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06004685 RID: 18053 RVA: 0x0008EFCE File Offset: 0x0008D1CE
		// (set) Token: 0x06004686 RID: 18054 RVA: 0x0008EFD6 File Offset: 0x0008D1D6
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x0008EFDF File Offset: 0x0008D1DF
		// (set) Token: 0x06004688 RID: 18056 RVA: 0x0008EFE7 File Offset: 0x0008D1E7
		public ProductUserId TargetUserId { get; set; }
	}
}
