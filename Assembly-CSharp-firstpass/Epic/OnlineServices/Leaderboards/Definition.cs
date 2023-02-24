using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007DC RID: 2012
	public class Definition
	{
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060048BE RID: 18622 RVA: 0x00090B77 File Offset: 0x0008ED77
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060048BF RID: 18623 RVA: 0x00090B7A File Offset: 0x0008ED7A
		// (set) Token: 0x060048C0 RID: 18624 RVA: 0x00090B82 File Offset: 0x0008ED82
		public string LeaderboardId { get; set; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060048C1 RID: 18625 RVA: 0x00090B8B File Offset: 0x0008ED8B
		// (set) Token: 0x060048C2 RID: 18626 RVA: 0x00090B93 File Offset: 0x0008ED93
		public string StatName { get; set; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060048C3 RID: 18627 RVA: 0x00090B9C File Offset: 0x0008ED9C
		// (set) Token: 0x060048C4 RID: 18628 RVA: 0x00090BA4 File Offset: 0x0008EDA4
		public LeaderboardAggregation Aggregation { get; set; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060048C5 RID: 18629 RVA: 0x00090BAD File Offset: 0x0008EDAD
		// (set) Token: 0x060048C6 RID: 18630 RVA: 0x00090BB5 File Offset: 0x0008EDB5
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060048C7 RID: 18631 RVA: 0x00090BBE File Offset: 0x0008EDBE
		// (set) Token: 0x060048C8 RID: 18632 RVA: 0x00090BC6 File Offset: 0x0008EDC6
		public DateTimeOffset? EndTime { get; set; }
	}
}
