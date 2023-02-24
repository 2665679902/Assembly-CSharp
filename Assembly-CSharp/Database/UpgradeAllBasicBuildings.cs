using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CE1 RID: 3297
	public class UpgradeAllBasicBuildings : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066BB RID: 26299 RVA: 0x00277768 File Offset: 0x00275968
		public UpgradeAllBasicBuildings(Tag basicBuilding, Tag upgradeBuilding)
		{
			this.basicBuilding = basicBuilding;
			this.upgradeBuilding = upgradeBuilding;
		}

		// Token: 0x060066BC RID: 26300 RVA: 0x00277780 File Offset: 0x00275980
		public override bool Success()
		{
			bool flag = false;
			foreach (IBasicBuilding basicBuilding in Components.BasicBuildings.Items)
			{
				KPrefabID component = basicBuilding.transform.GetComponent<KPrefabID>();
				if (component.HasTag(this.basicBuilding))
				{
					return false;
				}
				if (component.HasTag(this.upgradeBuilding))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x00277804 File Offset: 0x00275A04
		public void Deserialize(IReader reader)
		{
			string text = reader.ReadKleiString();
			this.basicBuilding = new Tag(text);
			string text2 = reader.ReadKleiString();
			this.upgradeBuilding = new Tag(text2);
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x00277838 File Offset: 0x00275A38
		public override string GetProgress(bool complete)
		{
			BuildingDef buildingDef = Assets.GetBuildingDef(this.basicBuilding.Name);
			BuildingDef buildingDef2 = Assets.GetBuildingDef(this.upgradeBuilding.Name);
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.UPGRADE_ALL_BUILDINGS, buildingDef.Name, buildingDef2.Name);
		}

		// Token: 0x04004AE4 RID: 19172
		private Tag basicBuilding;

		// Token: 0x04004AE5 RID: 19173
		private Tag upgradeBuilding;
	}
}
