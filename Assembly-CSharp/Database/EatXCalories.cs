using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CDA RID: 3290
	public class EatXCalories : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x0600669E RID: 26270 RVA: 0x00276E25 File Offset: 0x00275025
		public EatXCalories(int numCalories)
		{
			this.numCalories = numCalories;
		}

		// Token: 0x0600669F RID: 26271 RVA: 0x00276E34 File Offset: 0x00275034
		public override bool Success()
		{
			return RationTracker.Get().GetCaloriesConsumed() / 1000f > (float)this.numCalories;
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x00276E4F File Offset: 0x0027504F
		public void Deserialize(IReader reader)
		{
			this.numCalories = reader.ReadInt32();
		}

		// Token: 0x060066A1 RID: 26273 RVA: 0x00276E60 File Offset: 0x00275060
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CONSUME_CALORIES, GameUtil.GetFormattedCalories(complete ? ((float)this.numCalories * 1000f) : RationTracker.Get().GetCaloriesConsumed(), GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories((float)this.numCalories * 1000f, GameUtil.TimeSlice.None, true));
		}

		// Token: 0x04004ADC RID: 19164
		private int numCalories;
	}
}
