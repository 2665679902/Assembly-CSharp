using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000832 RID: 2098
	public class CopyEntitlementByIndexOptions
	{
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06004B2A RID: 19242 RVA: 0x00093233 File Offset: 0x00091433
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06004B2B RID: 19243 RVA: 0x00093236 File Offset: 0x00091436
		// (set) Token: 0x06004B2C RID: 19244 RVA: 0x0009323E File Offset: 0x0009143E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06004B2D RID: 19245 RVA: 0x00093247 File Offset: 0x00091447
		// (set) Token: 0x06004B2E RID: 19246 RVA: 0x0009324F File Offset: 0x0009144F
		public uint EntitlementIndex { get; set; }
	}
}
