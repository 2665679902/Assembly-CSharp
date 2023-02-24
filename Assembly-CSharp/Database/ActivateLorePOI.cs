using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CE4 RID: 3300
	public class ActivateLorePOI : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066C7 RID: 26311 RVA: 0x00277AD2 File Offset: 0x00275CD2
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x060066C8 RID: 26312 RVA: 0x00277AD4 File Offset: 0x00275CD4
		public override bool Success()
		{
			foreach (BuildingComplete buildingComplete in Components.TemplateBuildings.Items)
			{
				if (!(buildingComplete == null))
				{
					Unsealable component = buildingComplete.GetComponent<Unsealable>();
					if (component != null && component.unsealed)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060066C9 RID: 26313 RVA: 0x00277B4C File Offset: 0x00275D4C
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.INVESTIGATE_A_POI;
		}
	}
}
