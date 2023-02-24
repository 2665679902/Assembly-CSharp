using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CCE RID: 3278
	public class EstablishColonies : VictoryColonyAchievementRequirement
	{
		// Token: 0x06006668 RID: 26216 RVA: 0x00275FB0 File Offset: 0x002741B0
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.ESTABLISH_COLONIES.Replace("{goalBaseCount}", EstablishColonies.BASE_COUNT.ToString()).Replace("{baseCount}", this.GetColonyCount().ToString()).Replace("{neededCount}", EstablishColonies.BASE_COUNT.ToString());
		}

		// Token: 0x06006669 RID: 26217 RVA: 0x00276007 File Offset: 0x00274207
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x00276015 File Offset: 0x00274215
		public override bool Success()
		{
			return this.GetColonyCount() >= EstablishColonies.BASE_COUNT;
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x00276027 File Offset: 0x00274227
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.REQUIREMENTS.SEVERAL_COLONIES;
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x00276034 File Offset: 0x00274234
		private int GetColonyCount()
		{
			int num = 0;
			for (int i = 0; i < Components.Telepads.Count; i++)
			{
				Activatable component = Components.Telepads[i].GetComponent<Activatable>();
				if (component == null || component.IsActivated)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04004AC9 RID: 19145
		public static int BASE_COUNT = 5;
	}
}
