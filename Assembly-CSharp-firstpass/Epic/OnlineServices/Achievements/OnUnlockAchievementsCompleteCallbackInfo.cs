using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000942 RID: 2370
	public class OnUnlockAchievementsCompleteCallbackInfo
	{
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x0600521B RID: 21019 RVA: 0x0009A0A6 File Offset: 0x000982A6
		// (set) Token: 0x0600521C RID: 21020 RVA: 0x0009A0AE File Offset: 0x000982AE
		public Result ResultCode { get; set; }

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600521D RID: 21021 RVA: 0x0009A0B7 File Offset: 0x000982B7
		// (set) Token: 0x0600521E RID: 21022 RVA: 0x0009A0BF File Offset: 0x000982BF
		public object ClientData { get; set; }

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600521F RID: 21023 RVA: 0x0009A0C8 File Offset: 0x000982C8
		// (set) Token: 0x06005220 RID: 21024 RVA: 0x0009A0D0 File Offset: 0x000982D0
		public ProductUserId UserId { get; set; }

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06005221 RID: 21025 RVA: 0x0009A0D9 File Offset: 0x000982D9
		// (set) Token: 0x06005222 RID: 21026 RVA: 0x0009A0E1 File Offset: 0x000982E1
		public uint AchievementsCount { get; set; }
	}
}
