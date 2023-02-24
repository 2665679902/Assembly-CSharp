using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CC8 RID: 3272
	public class FractionalCycleNumber : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006647 RID: 26183 RVA: 0x00275ABD File Offset: 0x00273CBD
		public FractionalCycleNumber(float fractionalCycleNumber)
		{
			this.fractionalCycleNumber = fractionalCycleNumber;
		}

		// Token: 0x06006648 RID: 26184 RVA: 0x00275ACC File Offset: 0x00273CCC
		public override bool Success()
		{
			int num = (int)this.fractionalCycleNumber;
			float num2 = this.fractionalCycleNumber - (float)num;
			return (float)(GameClock.Instance.GetCycle() + 1) > this.fractionalCycleNumber || (GameClock.Instance.GetCycle() + 1 == num && GameClock.Instance.GetCurrentCycleAsPercentage() >= num2);
		}

		// Token: 0x06006649 RID: 26185 RVA: 0x00275B23 File Offset: 0x00273D23
		public void Deserialize(IReader reader)
		{
			this.fractionalCycleNumber = reader.ReadSingle();
		}

		// Token: 0x0600664A RID: 26186 RVA: 0x00275B34 File Offset: 0x00273D34
		public override string GetProgress(bool complete)
		{
			float num = (float)GameClock.Instance.GetCycle() + GameClock.Instance.GetCurrentCycleAsPercentage();
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.FRACTIONAL_CYCLE, complete ? this.fractionalCycleNumber : num, this.fractionalCycleNumber);
		}

		// Token: 0x04004AC4 RID: 19140
		private float fractionalCycleNumber;
	}
}
