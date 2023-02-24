using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200085C RID: 2140
	public class ItemOwnership
	{
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004C86 RID: 19590 RVA: 0x00094C17 File Offset: 0x00092E17
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x00094C1A File Offset: 0x00092E1A
		// (set) Token: 0x06004C88 RID: 19592 RVA: 0x00094C22 File Offset: 0x00092E22
		public string Id { get; set; }

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06004C89 RID: 19593 RVA: 0x00094C2B File Offset: 0x00092E2B
		// (set) Token: 0x06004C8A RID: 19594 RVA: 0x00094C33 File Offset: 0x00092E33
		public OwnershipStatus OwnershipStatus { get; set; }
	}
}
