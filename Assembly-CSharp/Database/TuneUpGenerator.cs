using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CE7 RID: 3303
	public class TuneUpGenerator : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066D3 RID: 26323 RVA: 0x00277C5F File Offset: 0x00275E5F
		public TuneUpGenerator(float numChoreseToComplete)
		{
			this.numChoreseToComplete = numChoreseToComplete;
		}

		// Token: 0x060066D4 RID: 26324 RVA: 0x00277C70 File Offset: 0x00275E70
		public override bool Success()
		{
			float num = 0f;
			ReportManager.ReportEntry entry = ReportManager.Instance.TodaysReport.GetEntry(ReportManager.ReportType.ChoreStatus);
			for (int i = 0; i < entry.contextEntries.Count; i++)
			{
				ReportManager.ReportEntry reportEntry = entry.contextEntries[i];
				if (reportEntry.context == Db.Get().ChoreTypes.PowerTinker.Name)
				{
					num += reportEntry.Negative;
				}
			}
			string name = Db.Get().ChoreTypes.PowerTinker.Name;
			int count = ReportManager.Instance.reports.Count;
			for (int j = 0; j < count; j++)
			{
				ReportManager.ReportEntry entry2 = ReportManager.Instance.reports[j].GetEntry(ReportManager.ReportType.ChoreStatus);
				int count2 = entry2.contextEntries.Count;
				for (int k = 0; k < count2; k++)
				{
					ReportManager.ReportEntry reportEntry2 = entry2.contextEntries[k];
					if (reportEntry2.context == name)
					{
						num += reportEntry2.Negative;
					}
				}
			}
			this.choresCompleted = Math.Abs(num);
			return Math.Abs(num) >= this.numChoreseToComplete;
		}

		// Token: 0x060066D5 RID: 26325 RVA: 0x00277D9C File Offset: 0x00275F9C
		public void Deserialize(IReader reader)
		{
			this.numChoreseToComplete = reader.ReadSingle();
		}

		// Token: 0x060066D6 RID: 26326 RVA: 0x00277DAC File Offset: 0x00275FAC
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CHORES_OF_TYPE, complete ? this.numChoreseToComplete : this.choresCompleted, this.numChoreseToComplete, Db.Get().ChoreTypes.PowerTinker.Name);
		}

		// Token: 0x04004AE7 RID: 19175
		private float numChoreseToComplete;

		// Token: 0x04004AE8 RID: 19176
		private float choresCompleted;
	}
}
