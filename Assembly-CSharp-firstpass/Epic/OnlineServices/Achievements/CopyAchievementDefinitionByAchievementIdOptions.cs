using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000916 RID: 2326
	public class CopyAchievementDefinitionByAchievementIdOptions
	{
		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x060050FA RID: 20730 RVA: 0x00099117 File Offset: 0x00097317
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x0009911A File Offset: 0x0009731A
		// (set) Token: 0x060050FC RID: 20732 RVA: 0x00099122 File Offset: 0x00097322
		public string AchievementId { get; set; }
	}
}
