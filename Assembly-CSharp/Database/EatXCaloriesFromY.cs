using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CD9 RID: 3289
	public class EatXCaloriesFromY : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x0600669A RID: 26266 RVA: 0x00276D3D File Offset: 0x00274F3D
		public EatXCaloriesFromY(int numCalories, List<string> fromFoodType)
		{
			this.numCalories = numCalories;
			this.fromFoodType = fromFoodType;
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x00276D5E File Offset: 0x00274F5E
		public override bool Success()
		{
			return RationTracker.Get().GetCaloiresConsumedByFood(this.fromFoodType) / 1000f > (float)this.numCalories;
		}

		// Token: 0x0600669C RID: 26268 RVA: 0x00276D80 File Offset: 0x00274F80
		public void Deserialize(IReader reader)
		{
			this.numCalories = reader.ReadInt32();
			int num = reader.ReadInt32();
			this.fromFoodType = new List<string>(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.fromFoodType.Add(text);
			}
		}

		// Token: 0x0600669D RID: 26269 RVA: 0x00276DCC File Offset: 0x00274FCC
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CALORIES_FROM_MEAT, GameUtil.GetFormattedCalories(complete ? ((float)this.numCalories * 1000f) : RationTracker.Get().GetCaloiresConsumedByFood(this.fromFoodType), GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories((float)this.numCalories * 1000f, GameUtil.TimeSlice.None, true));
		}

		// Token: 0x04004ADA RID: 19162
		private int numCalories;

		// Token: 0x04004ADB RID: 19163
		private List<string> fromFoodType = new List<string>();
	}
}
