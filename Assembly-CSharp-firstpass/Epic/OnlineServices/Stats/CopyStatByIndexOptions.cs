using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005A3 RID: 1443
	public class CopyStatByIndexOptions
	{
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x0008338C File Offset: 0x0008158C
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x0008338F File Offset: 0x0008158F
		// (set) Token: 0x06003B41 RID: 15169 RVA: 0x00083397 File Offset: 0x00081597
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06003B42 RID: 15170 RVA: 0x000833A0 File Offset: 0x000815A0
		// (set) Token: 0x06003B43 RID: 15171 RVA: 0x000833A8 File Offset: 0x000815A8
		public uint StatIndex { get; set; }
	}
}
