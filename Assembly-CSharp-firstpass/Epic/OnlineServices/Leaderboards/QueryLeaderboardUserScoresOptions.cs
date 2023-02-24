using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007FA RID: 2042
	public class QueryLeaderboardUserScoresOptions
	{
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06004972 RID: 18802 RVA: 0x00091703 File Offset: 0x0008F903
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06004973 RID: 18803 RVA: 0x00091706 File Offset: 0x0008F906
		// (set) Token: 0x06004974 RID: 18804 RVA: 0x0009170E File Offset: 0x0008F90E
		public ProductUserId[] UserIds { get; set; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06004975 RID: 18805 RVA: 0x00091717 File Offset: 0x0008F917
		// (set) Token: 0x06004976 RID: 18806 RVA: 0x0009171F File Offset: 0x0008F91F
		public UserScoresQueryStatInfo[] StatInfo { get; set; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06004977 RID: 18807 RVA: 0x00091728 File Offset: 0x0008F928
		// (set) Token: 0x06004978 RID: 18808 RVA: 0x00091730 File Offset: 0x0008F930
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06004979 RID: 18809 RVA: 0x00091739 File Offset: 0x0008F939
		// (set) Token: 0x0600497A RID: 18810 RVA: 0x00091741 File Offset: 0x0008F941
		public DateTimeOffset? EndTime { get; set; }
	}
}
