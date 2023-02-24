using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200094A RID: 2378
	public class QueryPlayerAchievementsOptions
	{
		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06005270 RID: 21104 RVA: 0x0009A643 File Offset: 0x00098843
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06005271 RID: 21105 RVA: 0x0009A646 File Offset: 0x00098846
		// (set) Token: 0x06005272 RID: 21106 RVA: 0x0009A64E File Offset: 0x0009884E
		public ProductUserId UserId { get; set; }
	}
}
