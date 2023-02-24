using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CE5 RID: 3301
	public class CritterTypeExists : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066CB RID: 26315 RVA: 0x00277B60 File Offset: 0x00275D60
		public CritterTypeExists(List<Tag> critterTypes)
		{
			this.critterTypes = critterTypes;
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x00277B7C File Offset: 0x00275D7C
		public override bool Success()
		{
			foreach (Capturable capturable in Components.Capturables.Items)
			{
				if (this.critterTypes.Contains(capturable.PrefabID()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060066CD RID: 26317 RVA: 0x00277BE8 File Offset: 0x00275DE8
		public void Deserialize(IReader reader)
		{
			int num = reader.ReadInt32();
			this.critterTypes = new List<Tag>(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.critterTypes.Add(new Tag(text));
			}
		}

		// Token: 0x060066CE RID: 26318 RVA: 0x00277C2C File Offset: 0x00275E2C
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.HATCH_A_MORPH;
		}

		// Token: 0x04004AE6 RID: 19174
		private List<Tag> critterTypes = new List<Tag>();
	}
}
