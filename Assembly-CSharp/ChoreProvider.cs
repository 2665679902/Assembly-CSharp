using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A6 RID: 934
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/ChoreProvider")]
public class ChoreProvider : KMonoBehaviour
{
	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06001332 RID: 4914 RVA: 0x00065EA7 File Offset: 0x000640A7
	// (set) Token: 0x06001333 RID: 4915 RVA: 0x00065EAF File Offset: 0x000640AF
	public string Name { get; private set; }

	// Token: 0x06001334 RID: 4916 RVA: 0x00065EB8 File Offset: 0x000640B8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Game.Instance.Subscribe(880851192, new Action<object>(this.OnWorldParentChanged));
		Game.Instance.Subscribe(586301400, new Action<object>(this.OnMinionMigrated));
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x00065F05 File Offset: 0x00064105
	protected override void OnSpawn()
	{
		if (ClusterManager.Instance != null)
		{
			ClusterManager.Instance.Subscribe(-1078710002, new Action<object>(this.OnWorldRemoved));
		}
		base.OnSpawn();
		this.Name = base.name;
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x00065F44 File Offset: 0x00064144
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Game.Instance.Unsubscribe(880851192, new Action<object>(this.OnWorldParentChanged));
		Game.Instance.Unsubscribe(586301400, new Action<object>(this.OnMinionMigrated));
		if (ClusterManager.Instance != null)
		{
			ClusterManager.Instance.Unsubscribe(-1078710002, new Action<object>(this.OnWorldRemoved));
		}
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x00065FB8 File Offset: 0x000641B8
	protected virtual void OnWorldRemoved(object data)
	{
		int num = (int)data;
		int parentWorldId = ClusterManager.Instance.GetWorld(num).ParentWorldId;
		List<Chore> list;
		if (this.choreWorldMap.TryGetValue(parentWorldId, out list))
		{
			this.ClearWorldChores<Chore>(list, num);
		}
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x00065FF8 File Offset: 0x000641F8
	protected virtual void OnWorldParentChanged(object data)
	{
		WorldParentChangedEventArgs worldParentChangedEventArgs = data as WorldParentChangedEventArgs;
		List<Chore> list;
		if (worldParentChangedEventArgs == null || worldParentChangedEventArgs.lastParentId == (int)ClusterManager.INVALID_WORLD_IDX || worldParentChangedEventArgs.lastParentId == worldParentChangedEventArgs.world.ParentWorldId || !this.choreWorldMap.TryGetValue(worldParentChangedEventArgs.lastParentId, out list))
		{
			return;
		}
		List<Chore> list2;
		if (!this.choreWorldMap.TryGetValue(worldParentChangedEventArgs.world.ParentWorldId, out list2))
		{
			list2 = (this.choreWorldMap[worldParentChangedEventArgs.world.ParentWorldId] = new List<Chore>());
		}
		this.TransferChores<Chore>(list, list2, worldParentChangedEventArgs.world.ParentWorldId);
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x000660A0 File Offset: 0x000642A0
	protected virtual void OnMinionMigrated(object data)
	{
		MinionMigrationEventArgs minionMigrationEventArgs = data as MinionMigrationEventArgs;
		List<Chore> list;
		if (minionMigrationEventArgs == null || !(minionMigrationEventArgs.minionId.gameObject == base.gameObject) || minionMigrationEventArgs.prevWorldId == minionMigrationEventArgs.targetWorldId || !this.choreWorldMap.TryGetValue(minionMigrationEventArgs.prevWorldId, out list))
		{
			return;
		}
		List<Chore> list2;
		if (!this.choreWorldMap.TryGetValue(minionMigrationEventArgs.targetWorldId, out list2))
		{
			list2 = (this.choreWorldMap[minionMigrationEventArgs.targetWorldId] = new List<Chore>());
		}
		this.TransferChores<Chore>(list, list2, minionMigrationEventArgs.targetWorldId);
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x0006613C File Offset: 0x0006433C
	protected void TransferChores<T>(List<T> oldChores, List<T> newChores, int transferId) where T : Chore
	{
		int num = oldChores.Count - 1;
		for (int i = num; i >= 0; i--)
		{
			T t = oldChores[i];
			if (t.isNull)
			{
				DebugUtil.DevLogError(string.Concat(new string[]
				{
					"[",
					t.GetType().Name,
					"] ",
					t.GetReportName(null),
					" has no target"
				}));
			}
			else if (t.gameObject.GetMyParentWorldId() == transferId)
			{
				newChores.Add(t);
				oldChores[i] = oldChores[num];
				oldChores.RemoveAt(num--);
			}
		}
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x000661F8 File Offset: 0x000643F8
	protected void ClearWorldChores<T>(List<T> chores, int worldId) where T : Chore
	{
		int num = chores.Count - 1;
		for (int i = num; i >= 0; i--)
		{
			if (chores[i].gameObject.GetMyWorldId() == worldId)
			{
				chores[i] = chores[num];
				chores.RemoveAt(num--);
			}
		}
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x0006624C File Offset: 0x0006444C
	public virtual void AddChore(Chore chore)
	{
		chore.provider = this;
		List<Chore> list = null;
		int myParentWorldId = chore.gameObject.GetMyParentWorldId();
		if (!this.choreWorldMap.TryGetValue(myParentWorldId, out list))
		{
			list = (this.choreWorldMap[myParentWorldId] = new List<Chore>());
		}
		list.Add(chore);
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x00066298 File Offset: 0x00064498
	public virtual void RemoveChore(Chore chore)
	{
		if (chore == null)
		{
			return;
		}
		chore.provider = null;
		List<Chore> list = null;
		int myParentWorldId = chore.gameObject.GetMyParentWorldId();
		if (this.choreWorldMap.TryGetValue(myParentWorldId, out list))
		{
			list.Remove(chore);
		}
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x000662D8 File Offset: 0x000644D8
	public virtual void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded, List<Chore.Precondition.Context> failed_contexts)
	{
		List<Chore> list = null;
		int myParentWorldId = consumer_state.gameObject.GetMyParentWorldId();
		if (!this.choreWorldMap.TryGetValue(myParentWorldId, out list))
		{
			return;
		}
		for (int i = list.Count - 1; i >= 0; i--)
		{
			Chore chore = list[i];
			if (chore.provider == null)
			{
				chore.Cancel("no provider");
				list[i] = list[list.Count - 1];
				list.RemoveAt(list.Count - 1);
			}
			else
			{
				chore.CollectChores(consumer_state, succeeded, failed_contexts, false);
			}
		}
	}

	// Token: 0x04000A6C RID: 2668
	public Dictionary<int, List<Chore>> choreWorldMap = new Dictionary<int, List<Chore>>();
}
