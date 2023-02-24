using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200060D RID: 1549
	public class QueryInvitesOptions
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x0008521A File Offset: 0x0008341A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x0008521D File Offset: 0x0008341D
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x00085225 File Offset: 0x00083425
		public ProductUserId LocalUserId { get; set; }
	}
}
