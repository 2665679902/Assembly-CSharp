using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000932 RID: 2354
	public class OnAchievementsUnlockedCallbackInfo
	{
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060051CF RID: 20943 RVA: 0x00099DE7 File Offset: 0x00097FE7
		// (set) Token: 0x060051D0 RID: 20944 RVA: 0x00099DEF File Offset: 0x00097FEF
		public object ClientData { get; set; }

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060051D1 RID: 20945 RVA: 0x00099DF8 File Offset: 0x00097FF8
		// (set) Token: 0x060051D2 RID: 20946 RVA: 0x00099E00 File Offset: 0x00098000
		public ProductUserId UserId { get; set; }

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060051D3 RID: 20947 RVA: 0x00099E09 File Offset: 0x00098009
		// (set) Token: 0x060051D4 RID: 20948 RVA: 0x00099E11 File Offset: 0x00098011
		public string[] AchievementIds { get; set; }
	}
}
