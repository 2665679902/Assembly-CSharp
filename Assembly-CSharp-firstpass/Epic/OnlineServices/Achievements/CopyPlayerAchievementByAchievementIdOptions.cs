using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200091E RID: 2334
	public class CopyPlayerAchievementByAchievementIdOptions
	{
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600511E RID: 20766 RVA: 0x00099327 File Offset: 0x00097527
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600511F RID: 20767 RVA: 0x0009932A File Offset: 0x0009752A
		// (set) Token: 0x06005120 RID: 20768 RVA: 0x00099332 File Offset: 0x00097532
		public ProductUserId UserId { get; set; }

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06005121 RID: 20769 RVA: 0x0009933B File Offset: 0x0009753B
		// (set) Token: 0x06005122 RID: 20770 RVA: 0x00099343 File Offset: 0x00097543
		public string AchievementId { get; set; }
	}
}
