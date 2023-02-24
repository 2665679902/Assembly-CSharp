using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CD1 RID: 3281
	public class CalorieSurplus : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006677 RID: 26231 RVA: 0x002762B0 File Offset: 0x002744B0
		public CalorieSurplus(float surplusAmount)
		{
			this.surplusAmount = (double)surplusAmount;
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x002762C0 File Offset: 0x002744C0
		public override bool Success()
		{
			return (double)(ClusterManager.Instance.CountAllRations() / 1000f) >= this.surplusAmount;
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x002762DE File Offset: 0x002744DE
		public override bool Fail()
		{
			return !this.Success();
		}

		// Token: 0x0600667A RID: 26234 RVA: 0x002762E9 File Offset: 0x002744E9
		public void Deserialize(IReader reader)
		{
			this.surplusAmount = reader.ReadDouble();
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x002762F7 File Offset: 0x002744F7
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CALORIE_SURPLUS, GameUtil.GetFormattedCalories(complete ? ((float)this.surplusAmount) : ClusterManager.Instance.CountAllRations(), GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories((float)this.surplusAmount, GameUtil.TimeSlice.None, true));
		}

		// Token: 0x04004ACD RID: 19149
		private double surplusAmount;
	}
}
