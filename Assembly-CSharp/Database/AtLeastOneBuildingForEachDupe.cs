using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CE0 RID: 3296
	public class AtLeastOneBuildingForEachDupe : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066B6 RID: 26294 RVA: 0x0027755C File Offset: 0x0027575C
		public AtLeastOneBuildingForEachDupe(List<Tag> validBuildingTypes)
		{
			this.validBuildingTypes = validBuildingTypes;
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x00277578 File Offset: 0x00275778
		public override bool Success()
		{
			if (Components.LiveMinionIdentities.Items.Count <= 0)
			{
				return false;
			}
			int num = 0;
			foreach (IBasicBuilding basicBuilding in Components.BasicBuildings.Items)
			{
				Tag prefabTag = basicBuilding.transform.GetComponent<KPrefabID>().PrefabTag;
				if (this.validBuildingTypes.Contains(prefabTag))
				{
					num++;
					if (prefabTag == "FlushToilet" || prefabTag == "Outhouse")
					{
						return true;
					}
				}
			}
			return num >= Components.LiveMinionIdentities.Items.Count;
		}

		// Token: 0x060066B8 RID: 26296 RVA: 0x00277640 File Offset: 0x00275840
		public override bool Fail()
		{
			return false;
		}

		// Token: 0x060066B9 RID: 26297 RVA: 0x00277644 File Offset: 0x00275844
		public void Deserialize(IReader reader)
		{
			int num = reader.ReadInt32();
			this.validBuildingTypes = new List<Tag>(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.validBuildingTypes.Add(new Tag(text));
			}
		}

		// Token: 0x060066BA RID: 26298 RVA: 0x00277688 File Offset: 0x00275888
		public override string GetProgress(bool complete)
		{
			if (this.validBuildingTypes.Contains("FlushToilet"))
			{
				return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILT_ONE_TOILET;
			}
			if (complete)
			{
				return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILT_ONE_BED_PER_DUPLICANT;
			}
			int num = 0;
			foreach (IBasicBuilding basicBuilding in Components.BasicBuildings.Items)
			{
				KPrefabID component = basicBuilding.transform.GetComponent<KPrefabID>();
				if (this.validBuildingTypes.Contains(component.PrefabTag))
				{
					num++;
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILING_BEDS, complete ? Components.LiveMinionIdentities.Items.Count : num, Components.LiveMinionIdentities.Items.Count);
		}

		// Token: 0x04004AE3 RID: 19171
		private List<Tag> validBuildingTypes = new List<Tag>();
	}
}
