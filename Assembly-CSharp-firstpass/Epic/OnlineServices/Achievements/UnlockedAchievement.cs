using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000950 RID: 2384
	public class UnlockedAchievement
	{
		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06005293 RID: 21139 RVA: 0x0009A86F File Offset: 0x00098A6F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06005294 RID: 21140 RVA: 0x0009A872 File Offset: 0x00098A72
		// (set) Token: 0x06005295 RID: 21141 RVA: 0x0009A87A File Offset: 0x00098A7A
		public string AchievementId { get; set; }

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x0009A883 File Offset: 0x00098A83
		// (set) Token: 0x06005297 RID: 21143 RVA: 0x0009A88B File Offset: 0x00098A8B
		public DateTimeOffset? UnlockTime { get; set; }
	}
}
