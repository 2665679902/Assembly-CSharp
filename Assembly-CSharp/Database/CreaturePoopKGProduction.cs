using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CE9 RID: 3305
	public class CreaturePoopKGProduction : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066DB RID: 26331 RVA: 0x00277F01 File Offset: 0x00276101
		public CreaturePoopKGProduction(Tag poopElement, float amountToPoop)
		{
			this.poopElement = poopElement;
			this.amountToPoop = amountToPoop;
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x00277F18 File Offset: 0x00276118
		public override bool Success()
		{
			return Game.Instance.savedInfo.creaturePoopAmount.ContainsKey(this.poopElement) && Game.Instance.savedInfo.creaturePoopAmount[this.poopElement] >= this.amountToPoop;
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x00277F68 File Offset: 0x00276168
		public void Deserialize(IReader reader)
		{
			this.amountToPoop = reader.ReadSingle();
			string text = reader.ReadKleiString();
			this.poopElement = new Tag(text);
		}

		// Token: 0x060066DE RID: 26334 RVA: 0x00277F94 File Offset: 0x00276194
		public override string GetProgress(bool complete)
		{
			float num = 0f;
			Game.Instance.savedInfo.creaturePoopAmount.TryGetValue(this.poopElement, out num);
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.POOP_PRODUCTION, GameUtil.GetFormattedMass(complete ? this.amountToPoop : num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"), GameUtil.GetFormattedMass(this.amountToPoop, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
		}

		// Token: 0x04004AEB RID: 19179
		private Tag poopElement;

		// Token: 0x04004AEC RID: 19180
		private float amountToPoop;
	}
}
