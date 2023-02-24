using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000668 RID: 1640
	public class CopyPresenceOptions
	{
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x00087B1F File Offset: 0x00085D1F
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x00087B22 File Offset: 0x00085D22
		// (set) Token: 0x06003FB7 RID: 16311 RVA: 0x00087B2A File Offset: 0x00085D2A
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x00087B33 File Offset: 0x00085D33
		// (set) Token: 0x06003FB9 RID: 16313 RVA: 0x00087B3B File Offset: 0x00085D3B
		public EpicAccountId TargetUserId { get; set; }
	}
}
