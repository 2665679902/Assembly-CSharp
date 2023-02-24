using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000745 RID: 1861
	public class JoinLobbyAcceptedCallbackInfo
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06004552 RID: 17746 RVA: 0x0008D63F File Offset: 0x0008B83F
		// (set) Token: 0x06004553 RID: 17747 RVA: 0x0008D647 File Offset: 0x0008B847
		public object ClientData { get; set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06004554 RID: 17748 RVA: 0x0008D650 File Offset: 0x0008B850
		// (set) Token: 0x06004555 RID: 17749 RVA: 0x0008D658 File Offset: 0x0008B858
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x0008D661 File Offset: 0x0008B861
		// (set) Token: 0x06004557 RID: 17751 RVA: 0x0008D669 File Offset: 0x0008B869
		public ulong UiEventId { get; set; }
	}
}
