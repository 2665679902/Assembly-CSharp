using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000920 RID: 2336
	public class CopyPlayerAchievementByIndexOptions
	{
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x0600512B RID: 20779 RVA: 0x000993EF File Offset: 0x000975EF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x0600512C RID: 20780 RVA: 0x000993F2 File Offset: 0x000975F2
		// (set) Token: 0x0600512D RID: 20781 RVA: 0x000993FA File Offset: 0x000975FA
		public ProductUserId UserId { get; set; }

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x00099403 File Offset: 0x00097603
		// (set) Token: 0x0600512F RID: 20783 RVA: 0x0009940B File Offset: 0x0009760B
		public uint AchievementIndex { get; set; }
	}
}
