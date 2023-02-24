using System;
using System.Collections.Generic;

// Token: 0x0200056C RID: 1388
public class BuildingInventory : KMonoBehaviour
{
	// Token: 0x06002190 RID: 8592 RVA: 0x000B6D0D File Offset: 0x000B4F0D
	public static void DestroyInstance()
	{
		BuildingInventory.Instance = null;
	}

	// Token: 0x06002191 RID: 8593 RVA: 0x000B6D15 File Offset: 0x000B4F15
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		BuildingInventory.Instance = this;
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x000B6D23 File Offset: 0x000B4F23
	public HashSet<BuildingComplete> GetBuildings(Tag tag)
	{
		return this.Buildings[tag];
	}

	// Token: 0x06002193 RID: 8595 RVA: 0x000B6D31 File Offset: 0x000B4F31
	public int BuildingCount(Tag tag)
	{
		if (!this.Buildings.ContainsKey(tag))
		{
			return 0;
		}
		return this.Buildings[tag].Count;
	}

	// Token: 0x06002194 RID: 8596 RVA: 0x000B6D54 File Offset: 0x000B4F54
	public int BuildingCountForWorld_BAD_PERF(Tag tag, int worldId)
	{
		if (!this.Buildings.ContainsKey(tag))
		{
			return 0;
		}
		int num = 0;
		using (HashSet<BuildingComplete>.Enumerator enumerator = this.Buildings[tag].GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetMyWorldId() == worldId)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06002195 RID: 8597 RVA: 0x000B6DC4 File Offset: 0x000B4FC4
	public void RegisterBuilding(BuildingComplete building)
	{
		Tag prefabTag = building.prefabid.PrefabTag;
		HashSet<BuildingComplete> hashSet;
		if (!this.Buildings.TryGetValue(prefabTag, out hashSet))
		{
			hashSet = new HashSet<BuildingComplete>();
			this.Buildings[prefabTag] = hashSet;
		}
		hashSet.Add(building);
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x000B6E08 File Offset: 0x000B5008
	public void UnregisterBuilding(BuildingComplete building)
	{
		Tag prefabTag = building.prefabid.PrefabTag;
		HashSet<BuildingComplete> hashSet;
		if (!this.Buildings.TryGetValue(prefabTag, out hashSet))
		{
			DebugUtil.DevLogError(string.Format("Unregistering building {0} before it was registered.", prefabTag));
			return;
		}
		DebugUtil.DevAssert(hashSet.Remove(building), string.Format("Building {0} was not found to be removed", prefabTag), null);
	}

	// Token: 0x0400134A RID: 4938
	public static BuildingInventory Instance;

	// Token: 0x0400134B RID: 4939
	private Dictionary<Tag, HashSet<BuildingComplete>> Buildings = new Dictionary<Tag, HashSet<BuildingComplete>>();
}
