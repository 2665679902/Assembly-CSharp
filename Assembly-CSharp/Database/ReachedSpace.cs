using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CCA RID: 3274
	public class ReachedSpace : VictoryColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006651 RID: 26193 RVA: 0x00275C8A File Offset: 0x00273E8A
		public ReachedSpace(SpaceDestinationType destinationType = null)
		{
			this.destinationType = destinationType;
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x00275C99 File Offset: 0x00273E99
		public override string Name()
		{
			if (this.destinationType != null)
			{
				return string.Format(COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.REQUIREMENTS.REACHED_SPACE_DESTINATION, this.destinationType.Name);
			}
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.REACH_SPACE_ANY_DESTINATION;
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x00275CC8 File Offset: 0x00273EC8
		public override string Description()
		{
			if (this.destinationType != null)
			{
				return string.Format(COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.REQUIREMENTS.REACHED_SPACE_DESTINATION_DESCRIPTION, this.destinationType.Name);
			}
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.REACH_SPACE_ANY_DESTINATION_DESCRIPTION;
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x00275CF8 File Offset: 0x00273EF8
		public override bool Success()
		{
			foreach (Spacecraft spacecraft in SpacecraftManager.instance.GetSpacecraft())
			{
				if (spacecraft.state != Spacecraft.MissionState.Grounded && spacecraft.state != Spacecraft.MissionState.Destroyed)
				{
					SpaceDestination destination = SpacecraftManager.instance.GetDestination(SpacecraftManager.instance.savedSpacecraftDestinations[spacecraft.id]);
					if (this.destinationType == null || destination.GetDestinationType() == this.destinationType)
					{
						if (this.destinationType == Db.Get().SpaceDestinationTypes.Wormhole)
						{
							Game.Instance.unlocks.Unlock("temporaltear", true);
						}
						return true;
					}
				}
			}
			return SpacecraftManager.instance.hasVisitedWormHole;
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x00275DD4 File Offset: 0x00273FD4
		public void Deserialize(IReader reader)
		{
			if (reader.ReadByte() <= 0)
			{
				string text = reader.ReadKleiString();
				this.destinationType = Db.Get().SpaceDestinationTypes.Get(text);
			}
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x00275E09 File Offset: 0x00274009
		public override string GetProgress(bool completed)
		{
			if (this.destinationType == null)
			{
				return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.LAUNCHED_ROCKET;
			}
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.LAUNCHED_ROCKET_TO_WORMHOLE;
		}

		// Token: 0x04004AC6 RID: 19142
		private SpaceDestinationType destinationType;
	}
}
