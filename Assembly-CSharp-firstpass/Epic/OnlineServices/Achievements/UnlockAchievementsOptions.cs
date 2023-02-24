using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200094E RID: 2382
	public class UnlockAchievementsOptions
	{
		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06005286 RID: 21126 RVA: 0x0009A78F File Offset: 0x0009898F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06005287 RID: 21127 RVA: 0x0009A792 File Offset: 0x00098992
		// (set) Token: 0x06005288 RID: 21128 RVA: 0x0009A79A File Offset: 0x0009899A
		public ProductUserId UserId { get; set; }

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06005289 RID: 21129 RVA: 0x0009A7A3 File Offset: 0x000989A3
		// (set) Token: 0x0600528A RID: 21130 RVA: 0x0009A7AB File Offset: 0x000989AB
		public string[] AchievementIds { get; set; }
	}
}
