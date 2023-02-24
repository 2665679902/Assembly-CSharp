using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004B5 RID: 1205
[AddComponentMenu("KMonoBehaviour/scripts/Pathfinding")]
public class Pathfinding : KMonoBehaviour
{
	// Token: 0x06001B85 RID: 7045 RVA: 0x00092071 File Offset: 0x00090271
	public static void DestroyInstance()
	{
		Pathfinding.Instance = null;
		OffsetTableTracker.OnPathfindingInvalidated();
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x0009207E File Offset: 0x0009027E
	protected override void OnPrefabInit()
	{
		Pathfinding.Instance = this;
	}

	// Token: 0x06001B87 RID: 7047 RVA: 0x00092086 File Offset: 0x00090286
	public void AddNavGrid(NavGrid nav_grid)
	{
		this.NavGrids.Add(nav_grid);
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x00092094 File Offset: 0x00090294
	public NavGrid GetNavGrid(string id)
	{
		foreach (NavGrid navGrid in this.NavGrids)
		{
			if (navGrid.id == id)
			{
				return navGrid;
			}
		}
		global::Debug.LogError("Could not find nav grid: " + id);
		return null;
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x00092108 File Offset: 0x00090308
	public List<NavGrid> GetNavGrids()
	{
		return this.NavGrids;
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x00092110 File Offset: 0x00090310
	public void ResetNavGrids()
	{
		foreach (NavGrid navGrid in this.NavGrids)
		{
			navGrid.InitializeGraph();
		}
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x00092160 File Offset: 0x00090360
	public void FlushNavGridsOnLoad()
	{
		if (this.navGridsHaveBeenFlushedOnLoad)
		{
			return;
		}
		this.navGridsHaveBeenFlushedOnLoad = true;
		this.UpdateNavGrids(true);
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x0009217C File Offset: 0x0009037C
	public void UpdateNavGrids(bool update_all = false)
	{
		update_all = true;
		if (update_all)
		{
			using (List<NavGrid>.Enumerator enumerator = this.NavGrids.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NavGrid navGrid = enumerator.Current;
					navGrid.UpdateGraph();
				}
				return;
			}
		}
		foreach (NavGrid navGrid2 in this.NavGrids)
		{
			if (navGrid2.updateEveryFrame)
			{
				navGrid2.UpdateGraph();
			}
		}
		this.NavGrids[this.UpdateIdx].UpdateGraph();
		this.UpdateIdx = (this.UpdateIdx + 1) % this.NavGrids.Count;
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x0009224C File Offset: 0x0009044C
	public void RenderEveryTick()
	{
		foreach (NavGrid navGrid in this.NavGrids)
		{
			navGrid.DebugUpdate();
		}
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x0009229C File Offset: 0x0009049C
	public void AddDirtyNavGridCell(int cell)
	{
		foreach (NavGrid navGrid in this.NavGrids)
		{
			navGrid.AddDirtyCell(cell);
		}
	}

	// Token: 0x06001B8F RID: 7055 RVA: 0x000922F0 File Offset: 0x000904F0
	public void RefreshNavCell(int cell)
	{
		HashSet<int> hashSet = new HashSet<int>();
		hashSet.Add(cell);
		foreach (NavGrid navGrid in this.NavGrids)
		{
			navGrid.UpdateGraph(hashSet);
		}
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x00092350 File Offset: 0x00090550
	protected override void OnCleanUp()
	{
		this.NavGrids.Clear();
		OffsetTableTracker.OnPathfindingInvalidated();
	}

	// Token: 0x04000F5B RID: 3931
	private List<NavGrid> NavGrids = new List<NavGrid>();

	// Token: 0x04000F5C RID: 3932
	private int UpdateIdx;

	// Token: 0x04000F5D RID: 3933
	private bool navGridsHaveBeenFlushedOnLoad;

	// Token: 0x04000F5E RID: 3934
	public static Pathfinding Instance;
}
