using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000615 RID: 1557
	public class RejectInviteOptions
	{
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x00085476 File Offset: 0x00083676
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06003DAA RID: 15786 RVA: 0x00085479 File Offset: 0x00083679
		// (set) Token: 0x06003DAB RID: 15787 RVA: 0x00085481 File Offset: 0x00083681
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x0008548A File Offset: 0x0008368A
		// (set) Token: 0x06003DAD RID: 15789 RVA: 0x00085492 File Offset: 0x00083692
		public string InviteId { get; set; }
	}
}
