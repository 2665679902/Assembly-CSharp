using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000674 RID: 1652
	public class JoinGameAcceptedCallbackInfo
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06004017 RID: 16407 RVA: 0x00088143 File Offset: 0x00086343
		// (set) Token: 0x06004018 RID: 16408 RVA: 0x0008814B File Offset: 0x0008634B
		public object ClientData { get; set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06004019 RID: 16409 RVA: 0x00088154 File Offset: 0x00086354
		// (set) Token: 0x0600401A RID: 16410 RVA: 0x0008815C File Offset: 0x0008635C
		public string JoinInfo { get; set; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x00088165 File Offset: 0x00086365
		// (set) Token: 0x0600401C RID: 16412 RVA: 0x0008816D File Offset: 0x0008636D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x00088176 File Offset: 0x00086376
		// (set) Token: 0x0600401E RID: 16414 RVA: 0x0008817E File Offset: 0x0008637E
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x00088187 File Offset: 0x00086387
		// (set) Token: 0x06004020 RID: 16416 RVA: 0x0008818F File Offset: 0x0008638F
		public ulong UiEventId { get; set; }
	}
}
