using System;

// Token: 0x020003D8 RID: 984
public class SafetyChecker
{
	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06001459 RID: 5209 RVA: 0x0006B8A7 File Offset: 0x00069AA7
	// (set) Token: 0x0600145A RID: 5210 RVA: 0x0006B8AF File Offset: 0x00069AAF
	public SafetyChecker.Condition[] conditions { get; private set; }

	// Token: 0x0600145B RID: 5211 RVA: 0x0006B8B8 File Offset: 0x00069AB8
	public SafetyChecker(SafetyChecker.Condition[] conditions)
	{
		this.conditions = conditions;
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x0006B8C8 File Offset: 0x00069AC8
	public int GetSafetyConditions(int cell, int cost, SafetyChecker.Context context, out bool all_conditions_met)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.conditions.Length; i++)
		{
			SafetyChecker.Condition condition = this.conditions[i];
			if (condition.callback(cell, cost, context))
			{
				num |= condition.mask;
				num2++;
			}
		}
		all_conditions_met = num2 == this.conditions.Length;
		return num;
	}

	// Token: 0x02000FF8 RID: 4088
	public struct Condition
	{
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x0600710C RID: 28940 RVA: 0x002A850A File Offset: 0x002A670A
		// (set) Token: 0x0600710D RID: 28941 RVA: 0x002A8512 File Offset: 0x002A6712
		public SafetyChecker.Condition.Callback callback { readonly get; private set; }

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x0600710E RID: 28942 RVA: 0x002A851B File Offset: 0x002A671B
		// (set) Token: 0x0600710F RID: 28943 RVA: 0x002A8523 File Offset: 0x002A6723
		public int mask { readonly get; private set; }

		// Token: 0x06007110 RID: 28944 RVA: 0x002A852C File Offset: 0x002A672C
		public Condition(string id, int condition_mask, SafetyChecker.Condition.Callback condition_callback)
		{
			this = default(SafetyChecker.Condition);
			this.callback = condition_callback;
			this.mask = condition_mask;
		}

		// Token: 0x02001EE4 RID: 7908
		// (Invoke) Token: 0x06009D49 RID: 40265
		public delegate bool Callback(int cell, int cost, SafetyChecker.Context context);
	}

	// Token: 0x02000FF9 RID: 4089
	public struct Context
	{
		// Token: 0x06007111 RID: 28945 RVA: 0x002A8544 File Offset: 0x002A6744
		public Context(KMonoBehaviour cmp)
		{
			this.cell = Grid.PosToCell(cmp);
			this.navigator = cmp.GetComponent<Navigator>();
			this.oxygenBreather = cmp.GetComponent<OxygenBreather>();
			this.minionBrain = cmp.GetComponent<MinionBrain>();
			this.temperatureTransferer = cmp.GetComponent<SimTemperatureTransfer>();
			this.primaryElement = cmp.GetComponent<PrimaryElement>();
		}

		// Token: 0x04005613 RID: 22035
		public Navigator navigator;

		// Token: 0x04005614 RID: 22036
		public OxygenBreather oxygenBreather;

		// Token: 0x04005615 RID: 22037
		public SimTemperatureTransfer temperatureTransferer;

		// Token: 0x04005616 RID: 22038
		public PrimaryElement primaryElement;

		// Token: 0x04005617 RID: 22039
		public MinionBrain minionBrain;

		// Token: 0x04005618 RID: 22040
		public int cell;
	}
}
