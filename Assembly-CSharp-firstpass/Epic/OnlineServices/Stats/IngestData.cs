using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005A9 RID: 1449
	public class IngestData
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000835A3 File Offset: 0x000817A3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x000835A6 File Offset: 0x000817A6
		// (set) Token: 0x06003B64 RID: 15204 RVA: 0x000835AE File Offset: 0x000817AE
		public string StatName { get; set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x000835B7 File Offset: 0x000817B7
		// (set) Token: 0x06003B66 RID: 15206 RVA: 0x000835BF File Offset: 0x000817BF
		public int IngestAmount { get; set; }
	}
}
