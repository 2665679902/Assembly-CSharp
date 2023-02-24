using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000944 RID: 2372
	public class PlayerAchievement
	{
		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06005229 RID: 21033 RVA: 0x0009A18A File Offset: 0x0009838A
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x0600522A RID: 21034 RVA: 0x0009A18D File Offset: 0x0009838D
		// (set) Token: 0x0600522B RID: 21035 RVA: 0x0009A195 File Offset: 0x00098395
		public string AchievementId { get; set; }

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x0600522C RID: 21036 RVA: 0x0009A19E File Offset: 0x0009839E
		// (set) Token: 0x0600522D RID: 21037 RVA: 0x0009A1A6 File Offset: 0x000983A6
		public double Progress { get; set; }

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x0600522E RID: 21038 RVA: 0x0009A1AF File Offset: 0x000983AF
		// (set) Token: 0x0600522F RID: 21039 RVA: 0x0009A1B7 File Offset: 0x000983B7
		public DateTimeOffset? UnlockTime { get; set; }

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06005230 RID: 21040 RVA: 0x0009A1C0 File Offset: 0x000983C0
		// (set) Token: 0x06005231 RID: 21041 RVA: 0x0009A1C8 File Offset: 0x000983C8
		public PlayerStatInfo[] StatInfo { get; set; }

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06005232 RID: 21042 RVA: 0x0009A1D1 File Offset: 0x000983D1
		// (set) Token: 0x06005233 RID: 21043 RVA: 0x0009A1D9 File Offset: 0x000983D9
		public string DisplayName { get; set; }

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06005234 RID: 21044 RVA: 0x0009A1E2 File Offset: 0x000983E2
		// (set) Token: 0x06005235 RID: 21045 RVA: 0x0009A1EA File Offset: 0x000983EA
		public string Description { get; set; }

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06005236 RID: 21046 RVA: 0x0009A1F3 File Offset: 0x000983F3
		// (set) Token: 0x06005237 RID: 21047 RVA: 0x0009A1FB File Offset: 0x000983FB
		public string IconURL { get; set; }

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06005238 RID: 21048 RVA: 0x0009A204 File Offset: 0x00098404
		// (set) Token: 0x06005239 RID: 21049 RVA: 0x0009A20C File Offset: 0x0009840C
		public string FlavorText { get; set; }
	}
}
