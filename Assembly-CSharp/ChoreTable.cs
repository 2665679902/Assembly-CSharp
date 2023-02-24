using System;
using System.Collections.Generic;

// Token: 0x020003A7 RID: 935
public class ChoreTable
{
	// Token: 0x06001340 RID: 4928 RVA: 0x0006637A File Offset: 0x0006457A
	public ChoreTable(ChoreTable.Entry[] entries)
	{
		this.entries = entries;
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x0006638C File Offset: 0x0006458C
	public ref ChoreTable.Entry GetEntry<T>()
	{
		ref ChoreTable.Entry ptr = ref ChoreTable.InvalidEntry;
		for (int i = 0; i < this.entries.Length; i++)
		{
			if (this.entries[i].stateMachineDef is T)
			{
				ptr = ref this.entries[i];
				break;
			}
		}
		return ref ptr;
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000663DC File Offset: 0x000645DC
	public int GetChorePriority<StateMachineType>(ChoreConsumer chore_consumer)
	{
		for (int i = 0; i < this.entries.Length; i++)
		{
			ChoreTable.Entry entry = this.entries[i];
			if (entry.stateMachineDef.GetStateMachineType() == typeof(StateMachineType))
			{
				return entry.choreType.priority;
			}
		}
		Debug.LogError(chore_consumer.name + "'s chore table does not have an entry for: " + typeof(StateMachineType).Name);
		return -1;
	}

	// Token: 0x04000A6D RID: 2669
	private ChoreTable.Entry[] entries;

	// Token: 0x04000A6E RID: 2670
	public static ChoreTable.Entry InvalidEntry;

	// Token: 0x02000FB2 RID: 4018
	public class Builder
	{
		// Token: 0x06007046 RID: 28742 RVA: 0x002A67F7 File Offset: 0x002A49F7
		public ChoreTable.Builder PushInterruptGroup()
		{
			this.interruptGroupId++;
			return this;
		}

		// Token: 0x06007047 RID: 28743 RVA: 0x002A6808 File Offset: 0x002A4A08
		public ChoreTable.Builder PopInterruptGroup()
		{
			DebugUtil.Assert(this.interruptGroupId > 0);
			this.interruptGroupId--;
			return this;
		}

		// Token: 0x06007048 RID: 28744 RVA: 0x002A6828 File Offset: 0x002A4A28
		public ChoreTable.Builder Add(StateMachine.BaseDef def, bool condition = true, int forcePriority = -1)
		{
			if (condition)
			{
				ChoreTable.Builder.Info info = new ChoreTable.Builder.Info
				{
					interruptGroupId = this.interruptGroupId,
					forcePriority = forcePriority,
					def = def
				};
				this.infos.Add(info);
			}
			return this;
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x002A686C File Offset: 0x002A4A6C
		public ChoreTable CreateTable()
		{
			DebugUtil.Assert(this.interruptGroupId == 0);
			ChoreTable.Entry[] array = new ChoreTable.Entry[this.infos.Count];
			Stack<int> stack = new Stack<int>();
			int num = 10000;
			for (int i = 0; i < this.infos.Count; i++)
			{
				int num2 = ((this.infos[i].forcePriority != -1) ? this.infos[i].forcePriority : (num - 100));
				num = num2;
				int num3 = 10000 - i * 100;
				int num4 = this.infos[i].interruptGroupId;
				if (num4 != 0)
				{
					if (stack.Count != num4)
					{
						stack.Push(num3);
					}
					else
					{
						num3 = stack.Peek();
					}
				}
				else if (stack.Count > 0)
				{
					stack.Pop();
				}
				array[i] = new ChoreTable.Entry(this.infos[i].def, num2, num3);
			}
			return new ChoreTable(array);
		}

		// Token: 0x04005541 RID: 21825
		private int interruptGroupId;

		// Token: 0x04005542 RID: 21826
		private List<ChoreTable.Builder.Info> infos = new List<ChoreTable.Builder.Info>();

		// Token: 0x04005543 RID: 21827
		private const int INVALID_PRIORITY = -1;

		// Token: 0x02001ED9 RID: 7897
		private struct Info
		{
			// Token: 0x04008A39 RID: 35385
			public int interruptGroupId;

			// Token: 0x04008A3A RID: 35386
			public int forcePriority;

			// Token: 0x04008A3B RID: 35387
			public StateMachine.BaseDef def;
		}
	}

	// Token: 0x02000FB3 RID: 4019
	public class ChoreTableChore<StateMachineType, StateMachineInstanceType> : Chore<StateMachineInstanceType> where StateMachineInstanceType : StateMachine.Instance
	{
		// Token: 0x0600704B RID: 28747 RVA: 0x002A697C File Offset: 0x002A4B7C
		public ChoreTableChore(StateMachine.BaseDef state_machine_def, ChoreType chore_type, KPrefabID prefab_id)
			: base(chore_type, prefab_id, prefab_id.GetComponent<ChoreProvider>(), true, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
		{
			this.showAvailabilityInHoverText = false;
			base.smi = state_machine_def.CreateSMI(this) as StateMachineInstanceType;
		}
	}

	// Token: 0x02000FB4 RID: 4020
	public struct Entry
	{
		// Token: 0x0600704C RID: 28748 RVA: 0x002A69C4 File Offset: 0x002A4BC4
		public Entry(StateMachine.BaseDef state_machine_def, int priority, int interrupt_priority)
		{
			Type stateMachineInstanceType = Singleton<StateMachineManager>.Instance.CreateStateMachine(state_machine_def.GetStateMachineType()).GetStateMachineInstanceType();
			Type[] array = new Type[]
			{
				state_machine_def.GetStateMachineType(),
				stateMachineInstanceType
			};
			this.choreClassType = typeof(ChoreTable.ChoreTableChore<, >).MakeGenericType(array);
			this.choreType = new ChoreType(state_machine_def.ToString(), null, new string[0], "", "", "", "", new Tag[0], priority, priority);
			this.choreType.interruptPriority = interrupt_priority;
			this.stateMachineDef = state_machine_def;
		}

		// Token: 0x04005544 RID: 21828
		public Type choreClassType;

		// Token: 0x04005545 RID: 21829
		public ChoreType choreType;

		// Token: 0x04005546 RID: 21830
		public StateMachine.BaseDef stateMachineDef;
	}

	// Token: 0x02000FB5 RID: 4021
	public class Instance
	{
		// Token: 0x0600704D RID: 28749 RVA: 0x002A6A58 File Offset: 0x002A4C58
		public static void ResetParameters()
		{
			for (int i = 0; i < ChoreTable.Instance.parameters.Length; i++)
			{
				ChoreTable.Instance.parameters[i] = null;
			}
		}

		// Token: 0x0600704E RID: 28750 RVA: 0x002A6A80 File Offset: 0x002A4C80
		public Instance(ChoreTable chore_table, KPrefabID prefab_id)
		{
			this.prefabId = prefab_id;
			this.entries = ListPool<ChoreTable.Instance.Entry, ChoreTable.Instance>.Allocate();
			for (int i = 0; i < chore_table.entries.Length; i++)
			{
				this.entries.Add(new ChoreTable.Instance.Entry(chore_table.entries[i], prefab_id));
			}
		}

		// Token: 0x0600704F RID: 28751 RVA: 0x002A6AD8 File Offset: 0x002A4CD8
		~Instance()
		{
			this.OnCleanUp(this.prefabId);
		}

		// Token: 0x06007050 RID: 28752 RVA: 0x002A6B0C File Offset: 0x002A4D0C
		public void OnCleanUp(KPrefabID prefab_id)
		{
			if (this.entries == null)
			{
				return;
			}
			for (int i = 0; i < this.entries.Count; i++)
			{
				this.entries[i].OnCleanUp(prefab_id);
			}
			this.entries.Recycle();
			this.entries = null;
		}

		// Token: 0x04005547 RID: 21831
		private static object[] parameters = new object[3];

		// Token: 0x04005548 RID: 21832
		private KPrefabID prefabId;

		// Token: 0x04005549 RID: 21833
		private ListPool<ChoreTable.Instance.Entry, ChoreTable.Instance>.PooledList entries;

		// Token: 0x02001EDA RID: 7898
		private struct Entry
		{
			// Token: 0x06009D26 RID: 40230 RVA: 0x0033B7A0 File Offset: 0x003399A0
			public Entry(ChoreTable.Entry chore_table_entry, KPrefabID prefab_id)
			{
				ChoreTable.Instance.parameters[0] = chore_table_entry.stateMachineDef;
				ChoreTable.Instance.parameters[1] = chore_table_entry.choreType;
				ChoreTable.Instance.parameters[2] = prefab_id;
				this.chore = (Chore)Activator.CreateInstance(chore_table_entry.choreClassType, ChoreTable.Instance.parameters);
				ChoreTable.Instance.parameters[0] = null;
				ChoreTable.Instance.parameters[1] = null;
				ChoreTable.Instance.parameters[2] = null;
			}

			// Token: 0x06009D27 RID: 40231 RVA: 0x0033B802 File Offset: 0x00339A02
			public void OnCleanUp(KPrefabID prefab_id)
			{
				if (this.chore != null)
				{
					this.chore.Cancel("ChoreTable.Instance.OnCleanUp");
					this.chore = null;
				}
			}

			// Token: 0x04008A3C RID: 35388
			public Chore chore;
		}
	}
}
