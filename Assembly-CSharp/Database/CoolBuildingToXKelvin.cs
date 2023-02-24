using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CD7 RID: 3287
	public class CoolBuildingToXKelvin : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06006691 RID: 26257 RVA: 0x00276BD0 File Offset: 0x00274DD0
		public CoolBuildingToXKelvin(int kelvinToCoolTo)
		{
			this.kelvinToCoolTo = kelvinToCoolTo;
		}

		// Token: 0x06006692 RID: 26258 RVA: 0x00276BDF File Offset: 0x00274DDF
		public override bool Success()
		{
			return BuildingComplete.MinKelvinSeen <= (float)this.kelvinToCoolTo;
		}

		// Token: 0x06006693 RID: 26259 RVA: 0x00276BF2 File Offset: 0x00274DF2
		public void Deserialize(IReader reader)
		{
			this.kelvinToCoolTo = reader.ReadInt32();
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x00276C00 File Offset: 0x00274E00
		public override string GetProgress(bool complete)
		{
			float minKelvinSeen = BuildingComplete.MinKelvinSeen;
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.KELVIN_COOLING, minKelvinSeen);
		}

		// Token: 0x04004AD9 RID: 19161
		private int kelvinToCoolTo;
	}
}
