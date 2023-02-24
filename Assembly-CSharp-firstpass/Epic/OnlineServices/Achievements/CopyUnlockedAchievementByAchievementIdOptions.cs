using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000922 RID: 2338
	public class CopyUnlockedAchievementByAchievementIdOptions
	{
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06005138 RID: 20792 RVA: 0x000994B7 File Offset: 0x000976B7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06005139 RID: 20793 RVA: 0x000994BA File Offset: 0x000976BA
		// (set) Token: 0x0600513A RID: 20794 RVA: 0x000994C2 File Offset: 0x000976C2
		public ProductUserId UserId { get; set; }

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x0600513B RID: 20795 RVA: 0x000994CB File Offset: 0x000976CB
		// (set) Token: 0x0600513C RID: 20796 RVA: 0x000994D3 File Offset: 0x000976D3
		public string AchievementId { get; set; }
	}
}
