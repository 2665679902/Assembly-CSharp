using System;
using System.Collections.Generic;

// Token: 0x020003AA RID: 938
internal class ClearableManager
{
	// Token: 0x06001359 RID: 4953 RVA: 0x00066A9C File Offset: 0x00064C9C
	public HandleVector<int>.Handle RegisterClearable(Clearable clearable)
	{
		return this.markedClearables.Allocate(new ClearableManager.MarkedClearable
		{
			clearable = clearable,
			pickupable = clearable.GetComponent<Pickupable>(),
			prioritizable = clearable.GetComponent<Prioritizable>()
		});
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x00066ADF File Offset: 0x00064CDF
	public void UnregisterClearable(HandleVector<int>.Handle handle)
	{
		this.markedClearables.Free(handle);
	}

	// Token: 0x0600135B RID: 4955 RVA: 0x00066AF0 File Offset: 0x00064CF0
	public void CollectAndSortClearables(Navigator navigator)
	{
		this.sortedClearables.Clear();
		foreach (ClearableManager.MarkedClearable markedClearable in this.markedClearables.GetDataList())
		{
			int navigationCost = markedClearable.pickupable.GetNavigationCost(navigator, markedClearable.pickupable.cachedCell);
			if (navigationCost != -1)
			{
				this.sortedClearables.Add(new ClearableManager.SortedClearable
				{
					pickupable = markedClearable.pickupable,
					masterPriority = markedClearable.prioritizable.GetMasterPriority(),
					cost = navigationCost
				});
			}
		}
		this.sortedClearables.Sort(ClearableManager.SortedClearable.comparer);
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x00066BB4 File Offset: 0x00064DB4
	public void CollectChores(List<GlobalChoreProvider.Fetch> fetches, ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded, List<Chore.Precondition.Context> failed_contexts)
	{
		ChoreType transport = Db.Get().ChoreTypes.Transport;
		int personalPriority = consumer_state.consumer.GetPersonalPriority(transport);
		int num = (Game.Instance.advancedPersonalPriorities ? transport.explicitPriority : transport.priority);
		bool flag = false;
		for (int i = 0; i < this.sortedClearables.Count; i++)
		{
			ClearableManager.SortedClearable sortedClearable = this.sortedClearables[i];
			Pickupable pickupable = sortedClearable.pickupable;
			PrioritySetting masterPriority = sortedClearable.masterPriority;
			Chore.Precondition.Context context = default(Chore.Precondition.Context);
			context.personalPriority = personalPriority;
			KPrefabID kprefabID = pickupable.KPrefabID;
			int num2 = 0;
			while (fetches != null && num2 < fetches.Count)
			{
				GlobalChoreProvider.Fetch fetch = fetches[num2];
				if ((fetch.chore.criteria == FetchChore.MatchCriteria.MatchID && fetch.chore.tags.Contains(kprefabID.PrefabTag)) || (fetch.chore.criteria == FetchChore.MatchCriteria.MatchTags && kprefabID.HasTag(fetch.chore.tagsFirst)))
				{
					context.Set(fetch.chore, consumer_state, false, pickupable);
					context.choreTypeForPermission = transport;
					context.RunPreconditions();
					if (context.IsSuccess())
					{
						context.masterPriority = masterPriority;
						context.priority = num;
						context.interruptPriority = transport.interruptPriority;
						succeeded.Add(context);
						flag = true;
						break;
					}
				}
				num2++;
			}
			if (flag)
			{
				break;
			}
		}
	}

	// Token: 0x04000A78 RID: 2680
	private KCompactedVector<ClearableManager.MarkedClearable> markedClearables = new KCompactedVector<ClearableManager.MarkedClearable>(0);

	// Token: 0x04000A79 RID: 2681
	private List<ClearableManager.SortedClearable> sortedClearables = new List<ClearableManager.SortedClearable>();

	// Token: 0x02000FB9 RID: 4025
	private struct MarkedClearable
	{
		// Token: 0x04005554 RID: 21844
		public Clearable clearable;

		// Token: 0x04005555 RID: 21845
		public Pickupable pickupable;

		// Token: 0x04005556 RID: 21846
		public Prioritizable prioritizable;
	}

	// Token: 0x02000FBA RID: 4026
	private struct SortedClearable
	{
		// Token: 0x04005557 RID: 21847
		public Pickupable pickupable;

		// Token: 0x04005558 RID: 21848
		public PrioritySetting masterPriority;

		// Token: 0x04005559 RID: 21849
		public int cost;

		// Token: 0x0400555A RID: 21850
		public static ClearableManager.SortedClearable.Comparer comparer = new ClearableManager.SortedClearable.Comparer();

		// Token: 0x02001EDB RID: 7899
		public class Comparer : IComparer<ClearableManager.SortedClearable>
		{
			// Token: 0x06009D28 RID: 40232 RVA: 0x0033B824 File Offset: 0x00339A24
			public int Compare(ClearableManager.SortedClearable a, ClearableManager.SortedClearable b)
			{
				int num = b.masterPriority.priority_value - a.masterPriority.priority_value;
				if (num == 0)
				{
					return a.cost - b.cost;
				}
				return num;
			}
		}
	}
}
