using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CED RID: 3309
	public class SentCraftIntoTemporalTear : VictoryColonyAchievementRequirement
	{
		// Token: 0x060066EA RID: 26346 RVA: 0x002782F8 File Offset: 0x002764F8
		public override string Name()
		{
			return string.Format(COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.REQUIREMENTS.REACHED_SPACE_DESTINATION, UI.SPACEDESTINATIONS.WORMHOLE.NAME);
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x0027830E File Offset: 0x0027650E
		public override string Description()
		{
			return string.Format(COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.REQUIREMENTS.REACHED_SPACE_DESTINATION_DESCRIPTION, UI.SPACEDESTINATIONS.WORMHOLE.NAME);
		}

		// Token: 0x060066EC RID: 26348 RVA: 0x00278324 File Offset: 0x00276524
		public override string GetProgress(bool completed)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.LAUNCHED_ROCKET_TO_WORMHOLE;
		}

		// Token: 0x060066ED RID: 26349 RVA: 0x00278330 File Offset: 0x00276530
		public override bool Success()
		{
			return ClusterManager.Instance.GetClusterPOIManager().HasTemporalTearConsumedCraft();
		}
	}
}
