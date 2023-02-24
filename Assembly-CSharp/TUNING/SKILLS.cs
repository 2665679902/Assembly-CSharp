using System;

namespace TUNING
{
	// Token: 0x02000D1F RID: 3359
	public class SKILLS
	{
		// Token: 0x04004C89 RID: 19593
		public static int TARGET_SKILLS_EARNED = 15;

		// Token: 0x04004C8A RID: 19594
		public static int TARGET_SKILLS_CYCLE = 250;

		// Token: 0x04004C8B RID: 19595
		public static float EXPERIENCE_LEVEL_POWER = 1.44f;

		// Token: 0x04004C8C RID: 19596
		public static float PASSIVE_EXPERIENCE_PORTION = 0.5f;

		// Token: 0x04004C8D RID: 19597
		public static float ACTIVE_EXPERIENCE_PORTION = 0.6f;

		// Token: 0x04004C8E RID: 19598
		public static float FULL_EXPERIENCE = 1f;

		// Token: 0x04004C8F RID: 19599
		public static float ALL_DAY_EXPERIENCE = SKILLS.FULL_EXPERIENCE / 0.9f;

		// Token: 0x04004C90 RID: 19600
		public static float MOST_DAY_EXPERIENCE = SKILLS.FULL_EXPERIENCE / 0.75f;

		// Token: 0x04004C91 RID: 19601
		public static float PART_DAY_EXPERIENCE = SKILLS.FULL_EXPERIENCE / 0.5f;

		// Token: 0x04004C92 RID: 19602
		public static float BARELY_EVER_EXPERIENCE = SKILLS.FULL_EXPERIENCE / 0.25f;

		// Token: 0x04004C93 RID: 19603
		public static float APTITUDE_EXPERIENCE_MULTIPLIER = 0.5f;

		// Token: 0x04004C94 RID: 19604
		public static int[] SKILL_TIER_MORALE_COST = new int[] { 1, 2, 3, 4, 5, 6, 7 };
	}
}
