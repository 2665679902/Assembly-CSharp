using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200091C RID: 2332
	public class CopyAchievementDefinitionV2ByIndexOptions
	{
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06005115 RID: 20757 RVA: 0x000992A3 File Offset: 0x000974A3
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06005116 RID: 20758 RVA: 0x000992A6 File Offset: 0x000974A6
		// (set) Token: 0x06005117 RID: 20759 RVA: 0x000992AE File Offset: 0x000974AE
		public uint AchievementIndex { get; set; }
	}
}
