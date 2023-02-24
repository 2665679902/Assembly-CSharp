using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C0 RID: 1984
	public class QueryInvitesOptions
	{
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x00090222 File Offset: 0x0008E422
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06004823 RID: 18467 RVA: 0x00090225 File Offset: 0x0008E425
		// (set) Token: 0x06004824 RID: 18468 RVA: 0x0009022D File Offset: 0x0008E42D
		public ProductUserId LocalUserId { get; set; }
	}
}
