using System;
using System.Collections.Generic;
using System.Linq;

namespace Database
{
	// Token: 0x02000CEC RID: 3308
	public class DupesCompleteChoreInExoSuitForCycles : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066E6 RID: 26342 RVA: 0x0027810F File Offset: 0x0027630F
		public DupesCompleteChoreInExoSuitForCycles(int numCycles)
		{
			this.numCycles = numCycles;
		}

		// Token: 0x060066E7 RID: 26343 RVA: 0x00278120 File Offset: 0x00276320
		public override bool Success()
		{
			Dictionary<int, List<int>> dupesCompleteChoresInSuits = SaveGame.Instance.GetComponent<ColonyAchievementTracker>().dupesCompleteChoresInSuits;
			Dictionary<int, float> dictionary = new Dictionary<int, float>();
			foreach (MinionIdentity minionIdentity in Components.MinionIdentities.Items)
			{
				dictionary.Add(minionIdentity.GetComponent<KPrefabID>().InstanceID, minionIdentity.arrivalTime);
			}
			int num = 0;
			int num2 = Math.Min(dupesCompleteChoresInSuits.Count, this.numCycles);
			for (int i = GameClock.Instance.GetCycle() - num2; i <= GameClock.Instance.GetCycle(); i++)
			{
				if (dupesCompleteChoresInSuits.ContainsKey(i))
				{
					List<int> list = dictionary.Keys.Except(dupesCompleteChoresInSuits[i]).ToList<int>();
					bool flag = true;
					foreach (int num3 in list)
					{
						if (dictionary[num3] < (float)i)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						num++;
					}
					else if (i != GameClock.Instance.GetCycle())
					{
						num = 0;
					}
					this.currentCycleStreak = num;
					if (num >= this.numCycles)
					{
						this.currentCycleStreak = this.numCycles;
						return true;
					}
				}
				else
				{
					this.currentCycleStreak = Math.Max(this.currentCycleStreak, num);
					num = 0;
				}
			}
			return false;
		}

		// Token: 0x060066E8 RID: 26344 RVA: 0x002782A0 File Offset: 0x002764A0
		public void Deserialize(IReader reader)
		{
			this.numCycles = reader.ReadInt32();
		}

		// Token: 0x060066E9 RID: 26345 RVA: 0x002782B0 File Offset: 0x002764B0
		public int GetNumberOfDupesForCycle(int cycle)
		{
			int num = 0;
			Dictionary<int, List<int>> dupesCompleteChoresInSuits = SaveGame.Instance.GetComponent<ColonyAchievementTracker>().dupesCompleteChoresInSuits;
			if (dupesCompleteChoresInSuits.ContainsKey(GameClock.Instance.GetCycle()))
			{
				num = dupesCompleteChoresInSuits[GameClock.Instance.GetCycle()].Count;
			}
			return num;
		}

		// Token: 0x04004AF1 RID: 19185
		public int currentCycleStreak;

		// Token: 0x04004AF2 RID: 19186
		public int numCycles;
	}
}
