using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000CEB RID: 3307
	public class DupesVsSolidTransferArmFetch : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066E3 RID: 26339 RVA: 0x00278026 File Offset: 0x00276226
		public DupesVsSolidTransferArmFetch(float percentage, int numCycles)
		{
			this.percentage = percentage;
			this.numCycles = numCycles;
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x0027803C File Offset: 0x0027623C
		public override bool Success()
		{
			Dictionary<int, int> fetchDupeChoreDeliveries = SaveGame.Instance.GetComponent<ColonyAchievementTracker>().fetchDupeChoreDeliveries;
			Dictionary<int, int> fetchAutomatedChoreDeliveries = SaveGame.Instance.GetComponent<ColonyAchievementTracker>().fetchAutomatedChoreDeliveries;
			int num = 0;
			this.currentCycleCount = 0;
			for (int i = GameClock.Instance.GetCycle() - 1; i >= GameClock.Instance.GetCycle() - this.numCycles; i--)
			{
				if (fetchAutomatedChoreDeliveries.ContainsKey(i))
				{
					if (fetchDupeChoreDeliveries.ContainsKey(i) && (float)fetchDupeChoreDeliveries[i] >= (float)fetchAutomatedChoreDeliveries[i] * this.percentage)
					{
						break;
					}
					num++;
				}
				else if (fetchDupeChoreDeliveries.ContainsKey(i))
				{
					num = 0;
					break;
				}
			}
			this.currentCycleCount = Math.Max(this.currentCycleCount, num);
			return num >= this.numCycles;
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x002780F5 File Offset: 0x002762F5
		public void Deserialize(IReader reader)
		{
			this.numCycles = reader.ReadInt32();
			this.percentage = reader.ReadSingle();
		}

		// Token: 0x04004AED RID: 19181
		public float percentage;

		// Token: 0x04004AEE RID: 19182
		public int numCycles;

		// Token: 0x04004AEF RID: 19183
		public int currentCycleCount;

		// Token: 0x04004AF0 RID: 19184
		public bool armsOutPerformingDupesThisCycle;
	}
}
