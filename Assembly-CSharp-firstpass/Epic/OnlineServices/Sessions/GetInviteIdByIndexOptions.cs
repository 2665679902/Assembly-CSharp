using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E5 RID: 1509
	public class GetInviteIdByIndexOptions
	{
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06003CCE RID: 15566 RVA: 0x00084D5F File Offset: 0x00082F5F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x00084D62 File Offset: 0x00082F62
		// (set) Token: 0x06003CD0 RID: 15568 RVA: 0x00084D6A File Offset: 0x00082F6A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x00084D73 File Offset: 0x00082F73
		// (set) Token: 0x06003CD2 RID: 15570 RVA: 0x00084D7B File Offset: 0x00082F7B
		public uint Index { get; set; }
	}
}
