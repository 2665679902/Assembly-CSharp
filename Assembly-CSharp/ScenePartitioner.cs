using System;
using System.Collections.Generic;

// Token: 0x02000905 RID: 2309
public class ScenePartitioner : ISim1000ms
{
	// Token: 0x0600433B RID: 17211 RVA: 0x0017BE38 File Offset: 0x0017A038
	public ScenePartitioner(int node_size, int layer_count, int scene_width, int scene_height)
	{
		this.nodeSize = node_size;
		int num = scene_width / node_size;
		int num2 = scene_height / node_size;
		this.nodes = new ScenePartitioner.ScenePartitionerNode[layer_count, num2, num];
		for (int i = 0; i < this.nodes.GetLength(0); i++)
		{
			for (int j = 0; j < this.nodes.GetLength(1); j++)
			{
				for (int k = 0; k < this.nodes.GetLength(2); k++)
				{
					this.nodes[i, j, k].entries = new HashSet<ScenePartitionerEntry>();
				}
			}
		}
		SimAndRenderScheduler.instance.Add(this, false);
	}

	// Token: 0x0600433C RID: 17212 RVA: 0x0017BEF8 File Offset: 0x0017A0F8
	public void FreeResources()
	{
		for (int i = 0; i < this.nodes.GetLength(0); i++)
		{
			for (int j = 0; j < this.nodes.GetLength(1); j++)
			{
				for (int k = 0; k < this.nodes.GetLength(2); k++)
				{
					foreach (ScenePartitionerEntry scenePartitionerEntry in this.nodes[i, j, k].entries)
					{
						if (scenePartitionerEntry != null)
						{
							scenePartitionerEntry.partitioner = null;
							scenePartitionerEntry.obj = null;
						}
					}
					this.nodes[i, j, k].entries.Clear();
				}
			}
		}
		this.nodes = null;
	}

	// Token: 0x0600433D RID: 17213 RVA: 0x0017BFD8 File Offset: 0x0017A1D8
	[Obsolete]
	public ScenePartitionerLayer CreateMask(HashedString name)
	{
		foreach (ScenePartitionerLayer scenePartitionerLayer in this.layers)
		{
			if (scenePartitionerLayer.name == name)
			{
				return scenePartitionerLayer;
			}
		}
		ScenePartitionerLayer scenePartitionerLayer2 = new ScenePartitionerLayer(name, this.layers.Count);
		this.layers.Add(scenePartitionerLayer2);
		DebugUtil.Assert(this.layers.Count <= this.nodes.GetLength(0));
		return scenePartitionerLayer2;
	}

	// Token: 0x0600433E RID: 17214 RVA: 0x0017C078 File Offset: 0x0017A278
	public ScenePartitionerLayer CreateMask(string name)
	{
		foreach (ScenePartitionerLayer scenePartitionerLayer in this.layers)
		{
			if (scenePartitionerLayer.name == name)
			{
				return scenePartitionerLayer;
			}
		}
		HashCache.Get().Add(name);
		ScenePartitionerLayer scenePartitionerLayer2 = new ScenePartitionerLayer(name, this.layers.Count);
		this.layers.Add(scenePartitionerLayer2);
		DebugUtil.Assert(this.layers.Count <= this.nodes.GetLength(0));
		return scenePartitionerLayer2;
	}

	// Token: 0x0600433F RID: 17215 RVA: 0x0017C130 File Offset: 0x0017A330
	private int ClampNodeX(int x)
	{
		return Math.Min(Math.Max(x, 0), this.nodes.GetLength(2) - 1);
	}

	// Token: 0x06004340 RID: 17216 RVA: 0x0017C14C File Offset: 0x0017A34C
	private int ClampNodeY(int y)
	{
		return Math.Min(Math.Max(y, 0), this.nodes.GetLength(1) - 1);
	}

	// Token: 0x06004341 RID: 17217 RVA: 0x0017C168 File Offset: 0x0017A368
	private Extents GetNodeExtents(int x, int y, int width, int height)
	{
		Extents extents = default(Extents);
		extents.x = this.ClampNodeX(x / this.nodeSize);
		extents.y = this.ClampNodeY(y / this.nodeSize);
		extents.width = 1 + this.ClampNodeX((x + width) / this.nodeSize) - extents.x;
		extents.height = 1 + this.ClampNodeY((y + height) / this.nodeSize) - extents.y;
		return extents;
	}

	// Token: 0x06004342 RID: 17218 RVA: 0x0017C1E9 File Offset: 0x0017A3E9
	private Extents GetNodeExtents(ScenePartitionerEntry entry)
	{
		return this.GetNodeExtents(entry.x, entry.y, entry.width, entry.height);
	}

	// Token: 0x06004343 RID: 17219 RVA: 0x0017C20C File Offset: 0x0017A40C
	private void Insert(ScenePartitionerEntry entry)
	{
		if (entry.obj == null)
		{
			Debug.LogWarning("Trying to put null go into scene partitioner");
			return;
		}
		Extents nodeExtents = this.GetNodeExtents(entry);
		if (nodeExtents.x + nodeExtents.width > this.nodes.GetLength(2))
		{
			Debug.LogError(string.Concat(new string[]
			{
				entry.obj.ToString(),
				" x/w ",
				nodeExtents.x.ToString(),
				"/",
				nodeExtents.width.ToString(),
				" < ",
				this.nodes.GetLength(2).ToString()
			}));
		}
		if (nodeExtents.y + nodeExtents.height > this.nodes.GetLength(1))
		{
			Debug.LogError(string.Concat(new string[]
			{
				entry.obj.ToString(),
				" y/h ",
				nodeExtents.y.ToString(),
				"/",
				nodeExtents.height.ToString(),
				" < ",
				this.nodes.GetLength(1).ToString()
			}));
		}
		int layer = entry.layer;
		for (int i = nodeExtents.y; i < nodeExtents.y + nodeExtents.height; i++)
		{
			for (int j = nodeExtents.x; j < nodeExtents.x + nodeExtents.width; j++)
			{
				if (!this.nodes[layer, i, j].dirty)
				{
					this.nodes[layer, i, j].dirty = true;
					this.dirtyNodes.Add(new ScenePartitioner.DirtyNode
					{
						layer = layer,
						x = j,
						y = i
					});
				}
				this.nodes[layer, i, j].entries.Add(entry);
			}
		}
	}

	// Token: 0x06004344 RID: 17220 RVA: 0x0017C404 File Offset: 0x0017A604
	private void Widthdraw(ScenePartitionerEntry entry)
	{
		Extents nodeExtents = this.GetNodeExtents(entry);
		if (nodeExtents.x + nodeExtents.width > this.nodes.GetLength(2))
		{
			Debug.LogError(string.Concat(new string[]
			{
				" x/w ",
				nodeExtents.x.ToString(),
				"/",
				nodeExtents.width.ToString(),
				" < ",
				this.nodes.GetLength(2).ToString()
			}));
		}
		if (nodeExtents.y + nodeExtents.height > this.nodes.GetLength(1))
		{
			Debug.LogError(string.Concat(new string[]
			{
				" y/h ",
				nodeExtents.y.ToString(),
				"/",
				nodeExtents.height.ToString(),
				" < ",
				this.nodes.GetLength(1).ToString()
			}));
		}
		int layer = entry.layer;
		for (int i = nodeExtents.y; i < nodeExtents.y + nodeExtents.height; i++)
		{
			for (int j = nodeExtents.x; j < nodeExtents.x + nodeExtents.width; j++)
			{
				if (this.nodes[layer, i, j].entries.Remove(entry) && !this.nodes[layer, i, j].dirty)
				{
					this.nodes[layer, i, j].dirty = true;
					this.dirtyNodes.Add(new ScenePartitioner.DirtyNode
					{
						layer = layer,
						x = j,
						y = i
					});
				}
			}
		}
	}

	// Token: 0x06004345 RID: 17221 RVA: 0x0017C5CC File Offset: 0x0017A7CC
	public ScenePartitionerEntry Add(ScenePartitionerEntry entry)
	{
		this.Insert(entry);
		return entry;
	}

	// Token: 0x06004346 RID: 17222 RVA: 0x0017C5D6 File Offset: 0x0017A7D6
	public void UpdatePosition(int x, int y, ScenePartitionerEntry entry)
	{
		this.Widthdraw(entry);
		entry.x = x;
		entry.y = y;
		this.Insert(entry);
	}

	// Token: 0x06004347 RID: 17223 RVA: 0x0017C5F4 File Offset: 0x0017A7F4
	public void UpdatePosition(Extents e, ScenePartitionerEntry entry)
	{
		this.Widthdraw(entry);
		entry.x = e.x;
		entry.y = e.y;
		entry.width = e.width;
		entry.height = e.height;
		this.Insert(entry);
	}

	// Token: 0x06004348 RID: 17224 RVA: 0x0017C634 File Offset: 0x0017A834
	public void Remove(ScenePartitionerEntry entry)
	{
		Extents nodeExtents = this.GetNodeExtents(entry);
		if (nodeExtents.x + nodeExtents.width > this.nodes.GetLength(2))
		{
			Debug.LogError(string.Concat(new string[]
			{
				" x/w ",
				nodeExtents.x.ToString(),
				"/",
				nodeExtents.width.ToString(),
				" < ",
				this.nodes.GetLength(2).ToString()
			}));
		}
		if (nodeExtents.y + nodeExtents.height > this.nodes.GetLength(1))
		{
			Debug.LogError(string.Concat(new string[]
			{
				" y/h ",
				nodeExtents.y.ToString(),
				"/",
				nodeExtents.height.ToString(),
				" < ",
				this.nodes.GetLength(1).ToString()
			}));
		}
		int layer = entry.layer;
		for (int i = nodeExtents.y; i < nodeExtents.y + nodeExtents.height; i++)
		{
			for (int j = nodeExtents.x; j < nodeExtents.x + nodeExtents.width; j++)
			{
				if (!this.nodes[layer, i, j].dirty)
				{
					this.nodes[layer, i, j].dirty = true;
					this.dirtyNodes.Add(new ScenePartitioner.DirtyNode
					{
						layer = layer,
						x = j,
						y = i
					});
				}
			}
		}
		entry.obj = null;
	}

	// Token: 0x06004349 RID: 17225 RVA: 0x0017C7E4 File Offset: 0x0017A9E4
	public void Sim1000ms(float dt)
	{
		foreach (ScenePartitioner.DirtyNode dirtyNode in this.dirtyNodes)
		{
			this.nodes[dirtyNode.layer, dirtyNode.y, dirtyNode.x].entries.RemoveWhere(ScenePartitioner.removeCallback);
			this.nodes[dirtyNode.layer, dirtyNode.y, dirtyNode.x].dirty = false;
		}
		this.dirtyNodes.Clear();
	}

	// Token: 0x0600434A RID: 17226 RVA: 0x0017C88C File Offset: 0x0017AA8C
	public void TriggerEvent(List<int> cells, ScenePartitionerLayer layer, object event_data)
	{
		ListPool<ScenePartitionerEntry, ScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, ScenePartitioner>.Allocate();
		this.queryId++;
		for (int i = 0; i < cells.Count; i++)
		{
			int num = 0;
			int num2 = 0;
			Grid.CellToXY(cells[i], out num, out num2);
			this.GatherEntries(num, num2, 1, 1, layer, event_data, pooledList, this.queryId);
		}
		this.RunLayerGlobalEvent(cells, layer, event_data);
		this.RunEntries(pooledList, event_data);
		pooledList.Recycle();
	}

	// Token: 0x0600434B RID: 17227 RVA: 0x0017C8FC File Offset: 0x0017AAFC
	public void TriggerEvent(HashSet<int> cells, ScenePartitionerLayer layer, object event_data)
	{
		ListPool<ScenePartitionerEntry, ScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, ScenePartitioner>.Allocate();
		this.queryId++;
		foreach (int num in cells)
		{
			int num2 = 0;
			int num3 = 0;
			Grid.CellToXY(num, out num2, out num3);
			this.GatherEntries(num2, num3, 1, 1, layer, event_data, pooledList, this.queryId);
		}
		this.RunLayerGlobalEvent(cells, layer, event_data);
		this.RunEntries(pooledList, event_data);
		pooledList.Recycle();
	}

	// Token: 0x0600434C RID: 17228 RVA: 0x0017C990 File Offset: 0x0017AB90
	public void TriggerEvent(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data)
	{
		ListPool<ScenePartitionerEntry, ScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, ScenePartitioner>.Allocate();
		this.GatherEntries(x, y, width, height, layer, event_data, pooledList);
		this.RunLayerGlobalEvent(x, y, width, height, layer, event_data);
		this.RunEntries(pooledList, event_data);
		pooledList.Recycle();
	}

	// Token: 0x0600434D RID: 17229 RVA: 0x0017C9D4 File Offset: 0x0017ABD4
	private void RunLayerGlobalEvent(List<int> cells, ScenePartitionerLayer layer, object event_data)
	{
		if (layer.OnEvent != null)
		{
			for (int i = 0; i < cells.Count; i++)
			{
				layer.OnEvent(cells[i], event_data);
			}
		}
	}

	// Token: 0x0600434E RID: 17230 RVA: 0x0017CA10 File Offset: 0x0017AC10
	private void RunLayerGlobalEvent(HashSet<int> cells, ScenePartitionerLayer layer, object event_data)
	{
		if (layer.OnEvent != null)
		{
			foreach (int num in cells)
			{
				layer.OnEvent(num, event_data);
			}
		}
	}

	// Token: 0x0600434F RID: 17231 RVA: 0x0017CA6C File Offset: 0x0017AC6C
	private void RunLayerGlobalEvent(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data)
	{
		if (layer.OnEvent != null)
		{
			for (int i = y; i < y + height; i++)
			{
				for (int j = x; j < x + width; j++)
				{
					int num = Grid.XYToCell(j, i);
					if (Grid.IsValidCell(num))
					{
						layer.OnEvent(num, event_data);
					}
				}
			}
		}
	}

	// Token: 0x06004350 RID: 17232 RVA: 0x0017CAC0 File Offset: 0x0017ACC0
	private void RunEntries(List<ScenePartitionerEntry> gathered_entries, object event_data)
	{
		for (int i = 0; i < gathered_entries.Count; i++)
		{
			ScenePartitionerEntry scenePartitionerEntry = gathered_entries[i];
			if (scenePartitionerEntry.obj != null && scenePartitionerEntry.eventCallback != null)
			{
				scenePartitionerEntry.eventCallback(event_data);
			}
		}
	}

	// Token: 0x06004351 RID: 17233 RVA: 0x0017CB04 File Offset: 0x0017AD04
	public void GatherEntries(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data, List<ScenePartitionerEntry> gathered_entries)
	{
		int num = this.queryId + 1;
		this.queryId = num;
		this.GatherEntries(x, y, width, height, layer, event_data, gathered_entries, num);
	}

	// Token: 0x06004352 RID: 17234 RVA: 0x0017CB34 File Offset: 0x0017AD34
	public void GatherEntries(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data, List<ScenePartitionerEntry> gathered_entries, int query_id)
	{
		Extents nodeExtents = this.GetNodeExtents(x, y, width, height);
		int num = Math.Min(nodeExtents.y + nodeExtents.height, this.nodes.GetLength(1));
		int num2 = Math.Max(nodeExtents.y, 0);
		int num3 = Math.Max(nodeExtents.x, 0);
		int num4 = Math.Min(nodeExtents.x + nodeExtents.width, this.nodes.GetLength(2));
		int layer2 = layer.layer;
		for (int i = num2; i < num; i++)
		{
			for (int j = num3; j < num4; j++)
			{
				ListPool<ScenePartitionerEntry, ScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, ScenePartitioner>.Allocate();
				foreach (ScenePartitionerEntry scenePartitionerEntry in this.nodes[layer2, i, j].entries)
				{
					if (scenePartitionerEntry != null && scenePartitionerEntry.queryId != this.queryId)
					{
						if (scenePartitionerEntry.obj == null)
						{
							pooledList.Add(scenePartitionerEntry);
						}
						else if (x + width - 1 >= scenePartitionerEntry.x && x <= scenePartitionerEntry.x + scenePartitionerEntry.width - 1 && y + height - 1 >= scenePartitionerEntry.y && y <= scenePartitionerEntry.y + scenePartitionerEntry.height - 1)
						{
							scenePartitionerEntry.queryId = this.queryId;
							gathered_entries.Add(scenePartitionerEntry);
						}
					}
				}
				this.nodes[layer2, i, j].entries.ExceptWith(pooledList);
				pooledList.Recycle();
			}
		}
	}

	// Token: 0x06004353 RID: 17235 RVA: 0x0017CCDC File Offset: 0x0017AEDC
	public void Cleanup()
	{
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06004354 RID: 17236 RVA: 0x0017CCEC File Offset: 0x0017AEEC
	public bool DoDebugLayersContainItemsOnCell(int cell)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		List<ScenePartitionerEntry> list = new List<ScenePartitionerEntry>();
		foreach (ScenePartitionerLayer scenePartitionerLayer in this.toggledLayers)
		{
			list.Clear();
			GameScenePartitioner.Instance.GatherEntries(num, num2, 1, 1, scenePartitionerLayer, list);
			if (list.Count > 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04002CDE RID: 11486
	public List<ScenePartitionerLayer> layers = new List<ScenePartitionerLayer>();

	// Token: 0x04002CDF RID: 11487
	private int nodeSize;

	// Token: 0x04002CE0 RID: 11488
	private List<ScenePartitioner.DirtyNode> dirtyNodes = new List<ScenePartitioner.DirtyNode>();

	// Token: 0x04002CE1 RID: 11489
	private ScenePartitioner.ScenePartitionerNode[,,] nodes;

	// Token: 0x04002CE2 RID: 11490
	private int queryId;

	// Token: 0x04002CE3 RID: 11491
	private static readonly Predicate<ScenePartitionerEntry> removeCallback = (ScenePartitionerEntry entry) => entry == null || entry.obj == null;

	// Token: 0x04002CE4 RID: 11492
	public HashSet<ScenePartitionerLayer> toggledLayers = new HashSet<ScenePartitionerLayer>();

	// Token: 0x020016E7 RID: 5863
	private struct ScenePartitionerNode
	{
		// Token: 0x04006B52 RID: 27474
		public HashSet<ScenePartitionerEntry> entries;

		// Token: 0x04006B53 RID: 27475
		public bool dirty;
	}

	// Token: 0x020016E8 RID: 5864
	private struct DirtyNode
	{
		// Token: 0x04006B54 RID: 27476
		public int layer;

		// Token: 0x04006B55 RID: 27477
		public int x;

		// Token: 0x04006B56 RID: 27478
		public int y;
	}
}
