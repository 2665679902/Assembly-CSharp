using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E7 RID: 1511
	public class IsUserInSessionOptions
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x00084E27 File Offset: 0x00083027
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x00084E2A File Offset: 0x0008302A
		// (set) Token: 0x06003CDD RID: 15581 RVA: 0x00084E32 File Offset: 0x00083032
		public string SessionName { get; set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x00084E3B File Offset: 0x0008303B
		// (set) Token: 0x06003CDF RID: 15583 RVA: 0x00084E43 File Offset: 0x00083043
		public ProductUserId TargetUserId { get; set; }
	}
}
