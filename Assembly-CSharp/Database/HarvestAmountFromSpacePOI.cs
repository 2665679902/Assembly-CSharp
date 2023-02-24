using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CF3 RID: 3315
	public class HarvestAmountFromSpacePOI : ColonyAchievementRequirement
	{
		// Token: 0x060066FE RID: 26366 RVA: 0x0027853D File Offset: 0x0027673D
		public HarvestAmountFromSpacePOI(float amountToHarvest)
		{
			this.amountToHarvest = amountToHarvest;
		}

		// Token: 0x060066FF RID: 26367 RVA: 0x0027854C File Offset: 0x0027674C
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.MINE_SPACE_POI, SaveGame.Instance.GetComponent<ColonyAchievementTracker>().totalMaterialsHarvestFromPOI, this.amountToHarvest);
		}

		// Token: 0x06006700 RID: 26368 RVA: 0x0027857C File Offset: 0x0027677C
		public override bool Success()
		{
			return SaveGame.Instance.GetComponent<ColonyAchievementTracker>().totalMaterialsHarvestFromPOI > this.amountToHarvest;
		}

		// Token: 0x04004AF4 RID: 19188
		private float amountToHarvest;
	}
}
