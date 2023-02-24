using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200067C RID: 1660
	public class PresenceChangedCallbackInfo
	{
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x0008825A File Offset: 0x0008645A
		// (set) Token: 0x06004041 RID: 16449 RVA: 0x00088262 File Offset: 0x00086462
		public object ClientData { get; set; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x0008826B File Offset: 0x0008646B
		// (set) Token: 0x06004043 RID: 16451 RVA: 0x00088273 File Offset: 0x00086473
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x0008827C File Offset: 0x0008647C
		// (set) Token: 0x06004045 RID: 16453 RVA: 0x00088284 File Offset: 0x00086484
		public EpicAccountId PresenceUserId { get; set; }
	}
}
