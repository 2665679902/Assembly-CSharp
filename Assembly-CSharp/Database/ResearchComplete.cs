using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CD2 RID: 3282
	public class ResearchComplete : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x0600667C RID: 26236 RVA: 0x00276334 File Offset: 0x00274534
		public override bool Success()
		{
			using (List<Tech>.Enumerator enumerator = Db.Get().Techs.resources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsComplete())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600667D RID: 26237 RVA: 0x00276398 File Offset: 0x00274598
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x0027639C File Offset: 0x0027459C
		public override string GetProgress(bool complete)
		{
			if (complete)
			{
				return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.TECH_RESEARCHED, Db.Get().Techs.resources.Count, Db.Get().Techs.resources.Count);
			}
			int num = 0;
			using (List<Tech>.Enumerator enumerator = Db.Get().Techs.resources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsComplete())
					{
						num++;
					}
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.TECH_RESEARCHED, num, Db.Get().Techs.resources.Count);
		}
	}
}
