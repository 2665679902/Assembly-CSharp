using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000924 RID: 2340
	public class CopyUnlockedAchievementByIndexOptions
	{
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06005145 RID: 20805 RVA: 0x0009957F File Offset: 0x0009777F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x00099582 File Offset: 0x00097782
		// (set) Token: 0x06005147 RID: 20807 RVA: 0x0009958A File Offset: 0x0009778A
		public ProductUserId UserId { get; set; }

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x00099593 File Offset: 0x00097793
		// (set) Token: 0x06005149 RID: 20809 RVA: 0x0009959B File Offset: 0x0009779B
		public uint AchievementIndex { get; set; }
	}
}
