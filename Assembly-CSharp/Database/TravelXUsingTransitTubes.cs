using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CDD RID: 3293
	public class TravelXUsingTransitTubes : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066AA RID: 26282 RVA: 0x002771BE File Offset: 0x002753BE
		public TravelXUsingTransitTubes(NavType navType, int distanceToTravel)
		{
			this.navType = navType;
			this.distanceToTravel = distanceToTravel;
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x002771D4 File Offset: 0x002753D4
		public override bool Success()
		{
			int num = 0;
			foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
			{
				Navigator component = minionIdentity.GetComponent<Navigator>();
				if (component != null && component.distanceTravelledByNavType.ContainsKey(this.navType))
				{
					num += component.distanceTravelledByNavType[this.navType];
				}
			}
			return num >= this.distanceToTravel;
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x00277268 File Offset: 0x00275468
		public void Deserialize(IReader reader)
		{
			byte b = reader.ReadByte();
			this.navType = (NavType)b;
			this.distanceToTravel = reader.ReadInt32();
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x00277290 File Offset: 0x00275490
		public override string GetProgress(bool complete)
		{
			int num = 0;
			foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
			{
				Navigator component = minionIdentity.GetComponent<Navigator>();
				if (component != null && component.distanceTravelledByNavType.ContainsKey(this.navType))
				{
					num += component.distanceTravelledByNavType[this.navType];
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.TRAVELED_IN_TUBES, complete ? this.distanceToTravel : num, this.distanceToTravel);
		}

		// Token: 0x04004ADF RID: 19167
		private int distanceToTravel;

		// Token: 0x04004AE0 RID: 19168
		private NavType navType;
	}
}
