using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CC6 RID: 3270
	public class CycleNumber : VictoryColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x0600663C RID: 26172 RVA: 0x0027599E File Offset: 0x00273B9E
		public override string Name()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_CYCLE, this.cycleNumber);
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x002759BA File Offset: 0x00273BBA
		public override string Description()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_CYCLE_DESCRIPTION, this.cycleNumber);
		}

		// Token: 0x0600663E RID: 26174 RVA: 0x002759D6 File Offset: 0x00273BD6
		public CycleNumber(int cycleNumber = 100)
		{
			this.cycleNumber = cycleNumber;
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x002759E5 File Offset: 0x00273BE5
		public override bool Success()
		{
			return GameClock.Instance.GetCycle() + 1 >= this.cycleNumber;
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x002759FE File Offset: 0x00273BFE
		public void Deserialize(IReader reader)
		{
			this.cycleNumber = reader.ReadInt32();
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x00275A0C File Offset: 0x00273C0C
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CYCLE_NUMBER, complete ? this.cycleNumber : (GameClock.Instance.GetCycle() + 1), this.cycleNumber);
		}

		// Token: 0x04004AC2 RID: 19138
		private int cycleNumber;
	}
}
