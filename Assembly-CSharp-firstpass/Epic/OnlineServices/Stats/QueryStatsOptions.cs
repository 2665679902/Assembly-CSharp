using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005B5 RID: 1461
	public class QueryStatsOptions
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x00083956 File Offset: 0x00081B56
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x00083959 File Offset: 0x00081B59
		// (set) Token: 0x06003BAE RID: 15278 RVA: 0x00083961 File Offset: 0x00081B61
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06003BAF RID: 15279 RVA: 0x0008396A File Offset: 0x00081B6A
		// (set) Token: 0x06003BB0 RID: 15280 RVA: 0x00083972 File Offset: 0x00081B72
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x0008397B File Offset: 0x00081B7B
		// (set) Token: 0x06003BB2 RID: 15282 RVA: 0x00083983 File Offset: 0x00081B83
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x0008398C File Offset: 0x00081B8C
		// (set) Token: 0x06003BB4 RID: 15284 RVA: 0x00083994 File Offset: 0x00081B94
		public string[] StatNames { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x0008399D File Offset: 0x00081B9D
		// (set) Token: 0x06003BB6 RID: 15286 RVA: 0x000839A5 File Offset: 0x00081BA5
		public ProductUserId TargetUserId { get; set; }
	}
}
