using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000936 RID: 2358
	public class OnAchievementsUnlockedCallbackV2Info
	{
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x00099E9C File Offset: 0x0009809C
		// (set) Token: 0x060051E3 RID: 20963 RVA: 0x00099EA4 File Offset: 0x000980A4
		public object ClientData { get; set; }

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060051E4 RID: 20964 RVA: 0x00099EAD File Offset: 0x000980AD
		// (set) Token: 0x060051E5 RID: 20965 RVA: 0x00099EB5 File Offset: 0x000980B5
		public ProductUserId UserId { get; set; }

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060051E6 RID: 20966 RVA: 0x00099EBE File Offset: 0x000980BE
		// (set) Token: 0x060051E7 RID: 20967 RVA: 0x00099EC6 File Offset: 0x000980C6
		public string AchievementId { get; set; }

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060051E8 RID: 20968 RVA: 0x00099ECF File Offset: 0x000980CF
		// (set) Token: 0x060051E9 RID: 20969 RVA: 0x00099ED7 File Offset: 0x000980D7
		public DateTimeOffset? UnlockTime { get; set; }
	}
}
