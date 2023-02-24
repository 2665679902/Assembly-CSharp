using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CD8 RID: 3288
	public class NoFarmables : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006695 RID: 26261 RVA: 0x00276C28 File Offset: 0x00274E28
		public override bool Success()
		{
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				foreach (PlantablePlot plantablePlot in Components.PlantablePlots.GetItems(worldContainer.id))
				{
					if (plantablePlot.Occupant != null)
					{
						using (IEnumerator<Tag> enumerator3 = plantablePlot.possibleDepositObjectTags.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								if (enumerator3.Current != GameTags.DecorSeed)
								{
									return false;
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x00276D1C File Offset: 0x00274F1C
		public override bool Fail()
		{
			return !this.Success();
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x00276D27 File Offset: 0x00274F27
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x06006698 RID: 26264 RVA: 0x00276D29 File Offset: 0x00274F29
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.NO_FARM_TILES;
		}
	}
}
