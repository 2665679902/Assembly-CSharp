using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CC5 RID: 3269
	public class NumberOfDupes : VictoryColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006636 RID: 26166 RVA: 0x002758F2 File Offset: 0x00273AF2
		public override string Name()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_DUPLICANTS, this.numDupes);
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x0027590E File Offset: 0x00273B0E
		public override string Description()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_DUPLICANTS_DESCRIPTION, this.numDupes);
		}

		// Token: 0x06006638 RID: 26168 RVA: 0x0027592A File Offset: 0x00273B2A
		public NumberOfDupes(int num)
		{
			this.numDupes = num;
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x00275939 File Offset: 0x00273B39
		public override bool Success()
		{
			return Components.LiveMinionIdentities.Items.Count >= this.numDupes;
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x00275955 File Offset: 0x00273B55
		public void Deserialize(IReader reader)
		{
			this.numDupes = reader.ReadInt32();
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x00275963 File Offset: 0x00273B63
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.POPULATION, complete ? this.numDupes : Components.LiveMinionIdentities.Items.Count, this.numDupes);
		}

		// Token: 0x04004AC1 RID: 19137
		private int numDupes;
	}
}
