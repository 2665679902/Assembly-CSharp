using System;
using Klei;
using ProcGen;
using STRINGS;

namespace Database
{
	// Token: 0x02000CDC RID: 3292
	public class BuildOutsideStartBiome : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066A7 RID: 26279 RVA: 0x002770C0 File Offset: 0x002752C0
		public override bool Success()
		{
			WorldDetailSave clusterDetailSave = SaveLoader.Instance.clusterDetailSave;
			foreach (BuildingComplete buildingComplete in Components.BuildingCompletes.Items)
			{
				if (!buildingComplete.GetComponent<KPrefabID>().HasTag(GameTags.TemplateBuilding))
				{
					for (int i = 0; i < clusterDetailSave.overworldCells.Count; i++)
					{
						WorldDetailSave.OverworldCell overworldCell = clusterDetailSave.overworldCells[i];
						if (overworldCell.tags != null && !overworldCell.tags.Contains(WorldGenTags.StartWorld) && overworldCell.poly.PointInPolygon(buildingComplete.transform.GetPosition()))
						{
							Game.Instance.unlocks.Unlock("buildoutsidestartingbiome", true);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060066A8 RID: 26280 RVA: 0x002771B0 File Offset: 0x002753B0
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x002771B2 File Offset: 0x002753B2
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILT_OUTSIDE_START;
		}
	}
}
