using System;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CC7 RID: 3271
	public class BeforeCycleNumber : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006642 RID: 26178 RVA: 0x00275A44 File Offset: 0x00273C44
		public BeforeCycleNumber(int cycleNumber = 100)
		{
			this.cycleNumber = cycleNumber;
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x00275A53 File Offset: 0x00273C53
		public override bool Success()
		{
			return GameClock.Instance.GetCycle() + 1 <= this.cycleNumber;
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x00275A6C File Offset: 0x00273C6C
		public override bool Fail()
		{
			return !this.Success();
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x00275A77 File Offset: 0x00273C77
		public void Deserialize(IReader reader)
		{
			this.cycleNumber = reader.ReadInt32();
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x00275A85 File Offset: 0x00273C85
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.REMAINING_CYCLES, Mathf.Max(this.cycleNumber - GameClock.Instance.GetCycle(), 0), this.cycleNumber);
		}

		// Token: 0x04004AC3 RID: 19139
		private int cycleNumber;
	}
}
