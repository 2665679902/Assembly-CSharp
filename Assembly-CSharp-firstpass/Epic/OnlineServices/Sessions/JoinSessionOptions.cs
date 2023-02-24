using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005ED RID: 1517
	public class JoinSessionOptions
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x00085016 File Offset: 0x00083216
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x00085019 File Offset: 0x00083219
		// (set) Token: 0x06003CFD RID: 15613 RVA: 0x00085021 File Offset: 0x00083221
		public string SessionName { get; set; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06003CFE RID: 15614 RVA: 0x0008502A File Offset: 0x0008322A
		// (set) Token: 0x06003CFF RID: 15615 RVA: 0x00085032 File Offset: 0x00083232
		public SessionDetails SessionHandle { get; set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x0008503B File Offset: 0x0008323B
		// (set) Token: 0x06003D01 RID: 15617 RVA: 0x00085043 File Offset: 0x00083243
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06003D02 RID: 15618 RVA: 0x0008504C File Offset: 0x0008324C
		// (set) Token: 0x06003D03 RID: 15619 RVA: 0x00085054 File Offset: 0x00083254
		public bool PresenceEnabled { get; set; }
	}
}
