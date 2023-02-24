using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200091A RID: 2330
	public class CopyAchievementDefinitionV2ByAchievementIdOptions
	{
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x0600510C RID: 20748 RVA: 0x0009921F File Offset: 0x0009741F
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600510D RID: 20749 RVA: 0x00099222 File Offset: 0x00097422
		// (set) Token: 0x0600510E RID: 20750 RVA: 0x0009922A File Offset: 0x0009742A
		public string AchievementId { get; set; }
	}
}
