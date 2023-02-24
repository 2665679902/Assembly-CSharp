using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000690 RID: 1680
	public class SetPresenceCallbackInfo
	{
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x00088C87 File Offset: 0x00086E87
		// (set) Token: 0x060040C4 RID: 16580 RVA: 0x00088C8F File Offset: 0x00086E8F
		public Result ResultCode { get; set; }

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060040C5 RID: 16581 RVA: 0x00088C98 File Offset: 0x00086E98
		// (set) Token: 0x060040C6 RID: 16582 RVA: 0x00088CA0 File Offset: 0x00086EA0
		public object ClientData { get; set; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x00088CA9 File Offset: 0x00086EA9
		// (set) Token: 0x060040C8 RID: 16584 RVA: 0x00088CB1 File Offset: 0x00086EB1
		public EpicAccountId LocalUserId { get; set; }
	}
}
