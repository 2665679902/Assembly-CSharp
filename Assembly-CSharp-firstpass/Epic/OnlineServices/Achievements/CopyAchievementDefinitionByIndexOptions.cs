using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000918 RID: 2328
	public class CopyAchievementDefinitionByIndexOptions
	{
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06005103 RID: 20739 RVA: 0x0009919B File Offset: 0x0009739B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06005104 RID: 20740 RVA: 0x0009919E File Offset: 0x0009739E
		// (set) Token: 0x06005105 RID: 20741 RVA: 0x000991A6 File Offset: 0x000973A6
		public uint AchievementIndex { get; set; }
	}
}
