using System;
using System.Collections.Generic;

// Token: 0x020003A9 RID: 937
public class GlobalChoreProvider : ChoreProvider, IRender200ms
{
	// Token: 0x06001349 RID: 4937 RVA: 0x00066537 File Offset: 0x00064737
	public static void DestroyInstance()
	{
		GlobalChoreProvider.Instance = null;
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x0006653F File Offset: 0x0006473F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		GlobalChoreProvider.Instance = this;
		this.clearableManager = new ClearableManager();
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x00066558 File Offset: 0x00064758
	protected override void OnWorldRemoved(object data)
	{
		int num = (int)data;
		int parentWorldId = ClusterManager.Instance.GetWorld(num).ParentWorldId;
		List<FetchChore> list;
		if (this.fetchMap.TryGetValue(parentWorldId, out list))
		{
			base.ClearWorldChores<FetchChore>(list, num);
		}
		base.OnWorldRemoved(data);
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x0006659C File Offset: 0x0006479C
	protected override void OnWorldParentChanged(object data)
	{
		WorldParentChangedEventArgs worldParentChangedEventArgs = data as WorldParentChangedEventArgs;
		if (worldParentChangedEventArgs == null || worldParentChangedEventArgs.lastParentId == (int)ClusterManager.INVALID_WORLD_IDX)
		{
			return;
		}
		base.OnWorldParentChanged(data);
		List<FetchChore> list;
		if (!this.fetchMap.TryGetValue(worldParentChangedEventArgs.lastParentId, out list))
		{
			return;
		}
		List<FetchChore> list2;
		if (!this.fetchMap.TryGetValue(worldParentChangedEventArgs.world.ParentWorldId, out list2))
		{
			list2 = (this.fetchMap[worldParentChangedEventArgs.world.ParentWorldId] = new List<FetchChore>());
		}
		base.TransferChores<FetchChore>(list, list2, worldParentChangedEventArgs.world.ParentWorldId);
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x00066628 File Offset: 0x00064828
	public override void AddChore(Chore chore)
	{
		FetchChore fetchChore = chore as FetchChore;
		if (fetchChore != null)
		{
			int myParentWorldId = fetchChore.gameObject.GetMyParentWorldId();
			List<FetchChore> list;
			if (!this.fetchMap.TryGetValue(myParentWorldId, out list))
			{
				list = (this.fetchMap[myParentWorldId] = new List<FetchChore>());
			}
			chore.provider = this;
			list.Add(fetchChore);
			return;
		}
		base.AddChore(chore);
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x00066684 File Offset: 0x00064884
	public override void RemoveChore(Chore chore)
	{
		FetchChore fetchChore = chore as FetchChore;
		if (fetchChore != null)
		{
			int myParentWorldId = fetchChore.gameObject.GetMyParentWorldId();
			List<FetchChore> list;
			if (this.fetchMap.TryGetValue(myParentWorldId, out list))
			{
				list.Remove(fetchChore);
			}
			chore.provider = null;
			return;
		}
		base.RemoveChore(chore);
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x000666D0 File Offset: 0x000648D0
	public void UpdateFetches(PathProber path_prober)
	{
		List<FetchChore> list = null;
		int myParentWorldId = path_prober.gameObject.GetMyParentWorldId();
		if (!this.fetchMap.TryGetValue(myParentWorldId, out list))
		{
			return;
		}
		this.fetches.Clear();
		Navigator component = path_prober.GetComponent<Navigator>();
		for (int i = list.Count - 1; i >= 0; i--)
		{
			FetchChore fetchChore = list[i];
			if (!(fetchChore.driver != null) && (!(fetchChore.automatable != null) || !fetchChore.automatable.GetAutomationOnly()))
			{
				if (fetchChore.provider == null)
				{
					fetchChore.Cancel("no provider");
					list[i] = list[list.Count - 1];
					list.RemoveAt(list.Count - 1);
				}
				else
				{
					Storage destination = fetchChore.destination;
					if (!(destination == null))
					{
						int navigationCost = component.GetNavigationCost(destination);
						if (navigationCost != -1)
						{
							this.fetches.Add(new GlobalChoreProvider.Fetch
							{
								chore = fetchChore,
								idsHash = fetchChore.tagsHash,
								cost = navigationCost,
								priority = fetchChore.masterPriority,
								category = destination.fetchCategory
							});
						}
					}
				}
			}
		}
		if (this.fetches.Count > 0)
		{
			this.fetches.Sort(GlobalChoreProvider.Comparer);
			int j = 1;
			int num = 0;
			while (j < this.fetches.Count)
			{
				if (!this.fetches[num].IsBetterThan(this.fetches[j]))
				{
					num++;
					this.fetches[num] = this.fetches[j];
				}
				j++;
			}
			this.fetches.RemoveRange(num + 1, this.fetches.Count - num - 1);
		}
		this.clearableManager.CollectAndSortClearables(component);
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x000668C4 File Offset: 0x00064AC4
	public override void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded, List<Chore.Precondition.Context> failed_contexts)
	{
		base.CollectChores(consumer_state, succeeded, failed_contexts);
		this.clearableManager.CollectChores(this.fetches, consumer_state, succeeded, failed_contexts);
		for (int i = 0; i < this.fetches.Count; i++)
		{
			this.fetches[i].chore.CollectChoresFromGlobalChoreProvider(consumer_state, succeeded, failed_contexts, false);
		}
	}

	// Token: 0x06001351 RID: 4945 RVA: 0x0006691E File Offset: 0x00064B1E
	public HandleVector<int>.Handle RegisterClearable(Clearable clearable)
	{
		return this.clearableManager.RegisterClearable(clearable);
	}

	// Token: 0x06001352 RID: 4946 RVA: 0x0006692C File Offset: 0x00064B2C
	public void UnregisterClearable(HandleVector<int>.Handle handle)
	{
		this.clearableManager.UnregisterClearable(handle);
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x0006693A File Offset: 0x00064B3A
	protected override void OnLoadLevel()
	{
		base.OnLoadLevel();
		GlobalChoreProvider.Instance = null;
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x00066948 File Offset: 0x00064B48
	public void Render200ms(float dt)
	{
		this.UpdateStorageFetchableBits();
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x00066950 File Offset: 0x00064B50
	private void UpdateStorageFetchableBits()
	{
		ChoreType storageFetch = Db.Get().ChoreTypes.StorageFetch;
		ChoreType foodFetch = Db.Get().ChoreTypes.FoodFetch;
		this.storageFetchableTags.Clear();
		List<int> worldIDsSorted = ClusterManager.Instance.GetWorldIDsSorted();
		for (int i = 0; i < worldIDsSorted.Count; i++)
		{
			List<FetchChore> list;
			if (this.fetchMap.TryGetValue(worldIDsSorted[i], out list))
			{
				for (int j = 0; j < list.Count; j++)
				{
					FetchChore fetchChore = list[j];
					if ((fetchChore.choreType == storageFetch || fetchChore.choreType == foodFetch) && fetchChore.destination)
					{
						int num = Grid.PosToCell(fetchChore.destination);
						if (MinionGroupProber.Get().IsReachable(num, fetchChore.destination.GetOffsets(num)))
						{
							this.storageFetchableTags.UnionWith(fetchChore.tags);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x00066A40 File Offset: 0x00064C40
	public bool ClearableHasDestination(Pickupable pickupable)
	{
		KPrefabID kprefabID = pickupable.KPrefabID;
		return this.storageFetchableTags.Contains(kprefabID.PrefabTag);
	}

	// Token: 0x04000A72 RID: 2674
	public static GlobalChoreProvider Instance;

	// Token: 0x04000A73 RID: 2675
	public Dictionary<int, List<FetchChore>> fetchMap = new Dictionary<int, List<FetchChore>>();

	// Token: 0x04000A74 RID: 2676
	public List<GlobalChoreProvider.Fetch> fetches = new List<GlobalChoreProvider.Fetch>();

	// Token: 0x04000A75 RID: 2677
	private static readonly GlobalChoreProvider.FetchComparer Comparer = new GlobalChoreProvider.FetchComparer();

	// Token: 0x04000A76 RID: 2678
	private ClearableManager clearableManager;

	// Token: 0x04000A77 RID: 2679
	private HashSet<Tag> storageFetchableTags = new HashSet<Tag>();

	// Token: 0x02000FB6 RID: 4022
	public struct Fetch
	{
		// Token: 0x06007052 RID: 28754 RVA: 0x002A6B6C File Offset: 0x002A4D6C
		public bool IsBetterThan(GlobalChoreProvider.Fetch fetch)
		{
			if (this.category != fetch.category)
			{
				return false;
			}
			if (this.idsHash != fetch.idsHash)
			{
				return false;
			}
			if (this.chore.choreType != fetch.chore.choreType)
			{
				return false;
			}
			if (this.priority.priority_class > fetch.priority.priority_class)
			{
				return true;
			}
			if (this.priority.priority_class == fetch.priority.priority_class)
			{
				if (this.priority.priority_value > fetch.priority.priority_value)
				{
					return true;
				}
				if (this.priority.priority_value == fetch.priority.priority_value)
				{
					return this.cost <= fetch.cost;
				}
			}
			return false;
		}

		// Token: 0x0400554A RID: 21834
		public FetchChore chore;

		// Token: 0x0400554B RID: 21835
		public int idsHash;

		// Token: 0x0400554C RID: 21836
		public int cost;

		// Token: 0x0400554D RID: 21837
		public PrioritySetting priority;

		// Token: 0x0400554E RID: 21838
		public Storage.FetchCategory category;
	}

	// Token: 0x02000FB7 RID: 4023
	private class FetchComparer : IComparer<GlobalChoreProvider.Fetch>
	{
		// Token: 0x06007053 RID: 28755 RVA: 0x002A6C2C File Offset: 0x002A4E2C
		public int Compare(GlobalChoreProvider.Fetch a, GlobalChoreProvider.Fetch b)
		{
			int num = b.priority.priority_class - a.priority.priority_class;
			if (num != 0)
			{
				return num;
			}
			int num2 = b.priority.priority_value - a.priority.priority_value;
			if (num2 != 0)
			{
				return num2;
			}
			return a.cost - b.cost;
		}
	}

	// Token: 0x02000FB8 RID: 4024
	private struct FindTopPriorityTask : IWorkItem<object>
	{
		// Token: 0x06007055 RID: 28757 RVA: 0x002A6C88 File Offset: 0x002A4E88
		public FindTopPriorityTask(int start, int end, List<Prioritizable> worldCollection)
		{
			this.start = start;
			this.end = end;
			this.worldCollection = worldCollection;
			this.found = false;
		}

		// Token: 0x06007056 RID: 28758 RVA: 0x002A6CA8 File Offset: 0x002A4EA8
		public void Run(object context)
		{
			if (GlobalChoreProvider.FindTopPriorityTask.abort)
			{
				return;
			}
			int num = this.start;
			while (num != this.end && this.worldCollection.Count > num)
			{
				if (!(this.worldCollection[num] == null) && this.worldCollection[num].IsTopPriority())
				{
					this.found = true;
					break;
				}
				num++;
			}
			if (this.found)
			{
				GlobalChoreProvider.FindTopPriorityTask.abort = true;
			}
		}

		// Token: 0x0400554F RID: 21839
		private int start;

		// Token: 0x04005550 RID: 21840
		private int end;

		// Token: 0x04005551 RID: 21841
		private List<Prioritizable> worldCollection;

		// Token: 0x04005552 RID: 21842
		public bool found;

		// Token: 0x04005553 RID: 21843
		public static bool abort;
	}
}
