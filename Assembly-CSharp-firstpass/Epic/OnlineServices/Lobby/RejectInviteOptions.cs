using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C4 RID: 1988
	public class RejectInviteOptions
	{
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06004836 RID: 18486 RVA: 0x00090356 File Offset: 0x0008E556
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06004837 RID: 18487 RVA: 0x00090359 File Offset: 0x0008E559
		// (set) Token: 0x06004838 RID: 18488 RVA: 0x00090361 File Offset: 0x0008E561
		public string InviteId { get; set; }

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x0009036A File Offset: 0x0008E56A
		// (set) Token: 0x0600483A RID: 18490 RVA: 0x00090372 File Offset: 0x0008E572
		public ProductUserId LocalUserId { get; set; }
	}
}
