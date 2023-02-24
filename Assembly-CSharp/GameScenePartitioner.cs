using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000904 RID: 2308
[AddComponentMenu("KMonoBehaviour/scripts/GameScenePartitioner")]
public class GameScenePartitioner : KMonoBehaviour
{
	// Token: 0x170004C8 RID: 1224
	// (get) Token: 0x0600431E RID: 17182 RVA: 0x0017B6E1 File Offset: 0x001798E1
	public static GameScenePartitioner Instance
	{
		get
		{
			global::Debug.Assert(GameScenePartitioner.instance != null);
			return GameScenePartitioner.instance;
		}
	}

	// Token: 0x0600431F RID: 17183 RVA: 0x0017B6F8 File Offset: 0x001798F8
	protected override void OnPrefabInit()
	{
		global::Debug.Assert(GameScenePartitioner.instance == null);
		GameScenePartitioner.instance = this;
		this.partitioner = new ScenePartitioner(16, 64, Grid.WidthInCells, Grid.HeightInCells);
		this.solidChangedLayer = this.partitioner.CreateMask("SolidChanged");
		this.liquidChangedLayer = this.partitioner.CreateMask("LiquidChanged");
		this.digDestroyedLayer = this.partitioner.CreateMask("DigDestroyed");
		this.fogOfWarChangedLayer = this.partitioner.CreateMask("FogOfWarChanged");
		this.decorProviderLayer = this.partitioner.CreateMask("DecorProviders");
		this.attackableEntitiesLayer = this.partitioner.CreateMask("FactionedEntities");
		this.fetchChoreLayer = this.partitioner.CreateMask("FetchChores");
		this.pickupablesLayer = this.partitioner.CreateMask("Pickupables");
		this.pickupablesChangedLayer = this.partitioner.CreateMask("PickupablesChanged");
		this.gasConduitsLayer = this.partitioner.CreateMask("GasConduit");
		this.liquidConduitsLayer = this.partitioner.CreateMask("LiquidConduit");
		this.solidConduitsLayer = this.partitioner.CreateMask("SolidConduit");
		this.noisePolluterLayer = this.partitioner.CreateMask("NoisePolluters");
		this.validNavCellChangedLayer = this.partitioner.CreateMask("validNavCellChangedLayer");
		this.dirtyNavCellUpdateLayer = this.partitioner.CreateMask("dirtyNavCellUpdateLayer");
		this.trapsLayer = this.partitioner.CreateMask("trapsLayer");
		this.floorSwitchActivatorLayer = this.partitioner.CreateMask("FloorSwitchActivatorLayer");
		this.floorSwitchActivatorChangedLayer = this.partitioner.CreateMask("FloorSwitchActivatorChangedLayer");
		this.collisionLayer = this.partitioner.CreateMask("Collision");
		this.lure = this.partitioner.CreateMask("Lure");
		this.plants = this.partitioner.CreateMask("Plants");
		this.industrialBuildings = this.partitioner.CreateMask("IndustrialBuildings");
		this.completeBuildings = this.partitioner.CreateMask("CompleteBuildings");
		this.prioritizableObjects = this.partitioner.CreateMask("PrioritizableObjects");
		this.contactConductiveLayer = this.partitioner.CreateMask("ContactConductiveLayer");
		this.objectLayers = new ScenePartitionerLayer[44];
		for (int i = 0; i < 44; i++)
		{
			ObjectLayer objectLayer = (ObjectLayer)i;
			this.objectLayers[i] = this.partitioner.CreateMask(objectLayer.ToString());
		}
	}

	// Token: 0x06004320 RID: 17184 RVA: 0x0017B998 File Offset: 0x00179B98
	protected override void OnForcedCleanUp()
	{
		GameScenePartitioner.instance = null;
		this.partitioner.FreeResources();
		this.partitioner = null;
		this.solidChangedLayer = null;
		this.liquidChangedLayer = null;
		this.digDestroyedLayer = null;
		this.fogOfWarChangedLayer = null;
		this.decorProviderLayer = null;
		this.attackableEntitiesLayer = null;
		this.fetchChoreLayer = null;
		this.pickupablesLayer = null;
		this.pickupablesChangedLayer = null;
		this.gasConduitsLayer = null;
		this.liquidConduitsLayer = null;
		this.solidConduitsLayer = null;
		this.noisePolluterLayer = null;
		this.validNavCellChangedLayer = null;
		this.dirtyNavCellUpdateLayer = null;
		this.trapsLayer = null;
		this.floorSwitchActivatorLayer = null;
		this.floorSwitchActivatorChangedLayer = null;
		this.contactConductiveLayer = null;
		this.objectLayers = null;
	}

	// Token: 0x06004321 RID: 17185 RVA: 0x0017BA4C File Offset: 0x00179C4C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		NavGrid navGrid = Pathfinding.Instance.GetNavGrid("MinionNavGrid");
		navGrid.OnNavGridUpdateComplete = (Action<HashSet<int>>)Delegate.Combine(navGrid.OnNavGridUpdateComplete, new Action<HashSet<int>>(this.OnNavGridUpdateComplete));
		NavTable navTable = navGrid.NavTable;
		navTable.OnValidCellChanged = (Action<int, NavType>)Delegate.Combine(navTable.OnValidCellChanged, new Action<int, NavType>(this.OnValidNavCellChanged));
	}

	// Token: 0x06004322 RID: 17186 RVA: 0x0017BAB8 File Offset: 0x00179CB8
	public HandleVector<int>.Handle Add(string name, object obj, int x, int y, int width, int height, ScenePartitionerLayer layer, Action<object> event_callback)
	{
		ScenePartitionerEntry scenePartitionerEntry = new ScenePartitionerEntry(name, obj, x, y, width, height, layer, this.partitioner, event_callback);
		this.partitioner.Add(scenePartitionerEntry);
		return this.scenePartitionerEntries.Allocate(scenePartitionerEntry);
	}

	// Token: 0x06004323 RID: 17187 RVA: 0x0017BAF8 File Offset: 0x00179CF8
	public HandleVector<int>.Handle Add(string name, object obj, Extents extents, ScenePartitionerLayer layer, Action<object> event_callback)
	{
		return this.Add(name, obj, extents.x, extents.y, extents.width, extents.height, layer, event_callback);
	}

	// Token: 0x06004324 RID: 17188 RVA: 0x0017BB2C File Offset: 0x00179D2C
	public HandleVector<int>.Handle Add(string name, object obj, int cell, ScenePartitionerLayer layer, Action<object> event_callback)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		return this.Add(name, obj, num, num2, 1, 1, layer, event_callback);
	}

	// Token: 0x06004325 RID: 17189 RVA: 0x0017BB57 File Offset: 0x00179D57
	public void AddGlobalLayerListener(ScenePartitionerLayer layer, Action<int, object> action)
	{
		layer.OnEvent = (Action<int, object>)Delegate.Combine(layer.OnEvent, action);
	}

	// Token: 0x06004326 RID: 17190 RVA: 0x0017BB70 File Offset: 0x00179D70
	public void RemoveGlobalLayerListener(ScenePartitionerLayer layer, Action<int, object> action)
	{
		layer.OnEvent = (Action<int, object>)Delegate.Remove(layer.OnEvent, action);
	}

	// Token: 0x06004327 RID: 17191 RVA: 0x0017BB89 File Offset: 0x00179D89
	public void TriggerEvent(List<int> cells, ScenePartitionerLayer layer, object event_data)
	{
		this.partitioner.TriggerEvent(cells, layer, event_data);
	}

	// Token: 0x06004328 RID: 17192 RVA: 0x0017BB99 File Offset: 0x00179D99
	public void TriggerEvent(HashSet<int> cells, ScenePartitionerLayer layer, object event_data)
	{
		this.partitioner.TriggerEvent(cells, layer, event_data);
	}

	// Token: 0x06004329 RID: 17193 RVA: 0x0017BBA9 File Offset: 0x00179DA9
	public void TriggerEvent(Extents extents, ScenePartitionerLayer layer, object event_data)
	{
		this.partitioner.TriggerEvent(extents.x, extents.y, extents.width, extents.height, layer, event_data);
	}

	// Token: 0x0600432A RID: 17194 RVA: 0x0017BBD0 File Offset: 0x00179DD0
	public void TriggerEvent(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data)
	{
		this.partitioner.TriggerEvent(x, y, width, height, layer, event_data);
	}

	// Token: 0x0600432B RID: 17195 RVA: 0x0017BBE8 File Offset: 0x00179DE8
	public void TriggerEvent(int cell, ScenePartitionerLayer layer, object event_data)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		this.TriggerEvent(num, num2, 1, 1, layer, event_data);
	}

	// Token: 0x0600432C RID: 17196 RVA: 0x0017BC0F File Offset: 0x00179E0F
	public void GatherEntries(Extents extents, ScenePartitionerLayer layer, List<ScenePartitionerEntry> gathered_entries)
	{
		this.GatherEntries(extents.x, extents.y, extents.width, extents.height, layer, gathered_entries);
	}

	// Token: 0x0600432D RID: 17197 RVA: 0x0017BC31 File Offset: 0x00179E31
	public void GatherEntries(int x_bottomLeft, int y_bottomLeft, int width, int height, ScenePartitionerLayer layer, List<ScenePartitionerEntry> gathered_entries)
	{
		this.partitioner.GatherEntries(x_bottomLeft, y_bottomLeft, width, height, layer, null, gathered_entries);
	}

	// Token: 0x0600432E RID: 17198 RVA: 0x0017BC48 File Offset: 0x00179E48
	public void Iterate<IteratorType>(int x, int y, int width, int height, ScenePartitionerLayer layer, ref IteratorType iterator) where IteratorType : GameScenePartitioner.Iterator
	{
		ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(x, y, width, height, layer, pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			ScenePartitionerEntry scenePartitionerEntry = pooledList[i];
			iterator.Iterate(scenePartitionerEntry.obj);
		}
		pooledList.Recycle();
	}

	// Token: 0x0600432F RID: 17199 RVA: 0x0017BCA0 File Offset: 0x00179EA0
	public void Iterate<IteratorType>(int cell, int radius, ScenePartitionerLayer layer, ref IteratorType iterator) where IteratorType : GameScenePartitioner.Iterator
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		this.Iterate<IteratorType>(num - radius, num2 - radius, radius * 2, radius * 2, layer, ref iterator);
	}

	// Token: 0x06004330 RID: 17200 RVA: 0x0017BCD0 File Offset: 0x00179ED0
	private void OnValidNavCellChanged(int cell, NavType nav_type)
	{
		this.changedCells.Add(cell);
	}

	// Token: 0x06004331 RID: 17201 RVA: 0x0017BCE0 File Offset: 0x00179EE0
	private void OnNavGridUpdateComplete(HashSet<int> dirty_nav_cells)
	{
		if (dirty_nav_cells.Count > 0)
		{
			GameScenePartitioner.Instance.TriggerEvent(dirty_nav_cells, GameScenePartitioner.Instance.dirtyNavCellUpdateLayer, null);
		}
		if (this.changedCells.Count > 0)
		{
			GameScenePartitioner.Instance.TriggerEvent(this.changedCells, GameScenePartitioner.Instance.validNavCellChangedLayer, null);
			this.changedCells.Clear();
		}
	}

	// Token: 0x06004332 RID: 17202 RVA: 0x0017BD40 File Offset: 0x00179F40
	public void UpdatePosition(HandleVector<int>.Handle handle, int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		this.UpdatePosition(handle, vector2I.x, vector2I.y);
	}

	// Token: 0x06004333 RID: 17203 RVA: 0x0017BD67 File Offset: 0x00179F67
	public void UpdatePosition(HandleVector<int>.Handle handle, int x, int y)
	{
		if (!handle.IsValid())
		{
			return;
		}
		this.scenePartitionerEntries.GetData(handle).UpdatePosition(x, y);
	}

	// Token: 0x06004334 RID: 17204 RVA: 0x0017BD86 File Offset: 0x00179F86
	public void UpdatePosition(HandleVector<int>.Handle handle, Extents ext)
	{
		if (!handle.IsValid())
		{
			return;
		}
		this.scenePartitionerEntries.GetData(handle).UpdatePosition(ext);
	}

	// Token: 0x06004335 RID: 17205 RVA: 0x0017BDA4 File Offset: 0x00179FA4
	public void Free(ref HandleVector<int>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return;
		}
		this.scenePartitionerEntries.GetData(handle).Release();
		this.scenePartitionerEntries.Free(handle);
		handle.Clear();
	}

	// Token: 0x06004336 RID: 17206 RVA: 0x0017BDDD File Offset: 0x00179FDD
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.partitioner.Cleanup();
	}

	// Token: 0x06004337 RID: 17207 RVA: 0x0017BDF0 File Offset: 0x00179FF0
	public bool DoDebugLayersContainItemsOnCell(int cell)
	{
		return this.partitioner.DoDebugLayersContainItemsOnCell(cell);
	}

	// Token: 0x06004338 RID: 17208 RVA: 0x0017BDFE File Offset: 0x00179FFE
	public List<ScenePartitionerLayer> GetLayers()
	{
		return this.partitioner.layers;
	}

	// Token: 0x06004339 RID: 17209 RVA: 0x0017BE0B File Offset: 0x0017A00B
	public void SetToggledLayers(HashSet<ScenePartitionerLayer> toggled_layers)
	{
		this.partitioner.toggledLayers = toggled_layers;
	}

	// Token: 0x04002CBF RID: 11455
	public ScenePartitionerLayer solidChangedLayer;

	// Token: 0x04002CC0 RID: 11456
	public ScenePartitionerLayer liquidChangedLayer;

	// Token: 0x04002CC1 RID: 11457
	public ScenePartitionerLayer digDestroyedLayer;

	// Token: 0x04002CC2 RID: 11458
	public ScenePartitionerLayer fogOfWarChangedLayer;

	// Token: 0x04002CC3 RID: 11459
	public ScenePartitionerLayer decorProviderLayer;

	// Token: 0x04002CC4 RID: 11460
	public ScenePartitionerLayer attackableEntitiesLayer;

	// Token: 0x04002CC5 RID: 11461
	public ScenePartitionerLayer fetchChoreLayer;

	// Token: 0x04002CC6 RID: 11462
	public ScenePartitionerLayer pickupablesLayer;

	// Token: 0x04002CC7 RID: 11463
	public ScenePartitionerLayer pickupablesChangedLayer;

	// Token: 0x04002CC8 RID: 11464
	public ScenePartitionerLayer gasConduitsLayer;

	// Token: 0x04002CC9 RID: 11465
	public ScenePartitionerLayer liquidConduitsLayer;

	// Token: 0x04002CCA RID: 11466
	public ScenePartitionerLayer solidConduitsLayer;

	// Token: 0x04002CCB RID: 11467
	public ScenePartitionerLayer wiresLayer;

	// Token: 0x04002CCC RID: 11468
	public ScenePartitionerLayer[] objectLayers;

	// Token: 0x04002CCD RID: 11469
	public ScenePartitionerLayer noisePolluterLayer;

	// Token: 0x04002CCE RID: 11470
	public ScenePartitionerLayer validNavCellChangedLayer;

	// Token: 0x04002CCF RID: 11471
	public ScenePartitionerLayer dirtyNavCellUpdateLayer;

	// Token: 0x04002CD0 RID: 11472
	public ScenePartitionerLayer trapsLayer;

	// Token: 0x04002CD1 RID: 11473
	public ScenePartitionerLayer floorSwitchActivatorLayer;

	// Token: 0x04002CD2 RID: 11474
	public ScenePartitionerLayer floorSwitchActivatorChangedLayer;

	// Token: 0x04002CD3 RID: 11475
	public ScenePartitionerLayer collisionLayer;

	// Token: 0x04002CD4 RID: 11476
	public ScenePartitionerLayer lure;

	// Token: 0x04002CD5 RID: 11477
	public ScenePartitionerLayer plants;

	// Token: 0x04002CD6 RID: 11478
	public ScenePartitionerLayer industrialBuildings;

	// Token: 0x04002CD7 RID: 11479
	public ScenePartitionerLayer completeBuildings;

	// Token: 0x04002CD8 RID: 11480
	public ScenePartitionerLayer prioritizableObjects;

	// Token: 0x04002CD9 RID: 11481
	public ScenePartitionerLayer contactConductiveLayer;

	// Token: 0x04002CDA RID: 11482
	private ScenePartitioner partitioner;

	// Token: 0x04002CDB RID: 11483
	private static GameScenePartitioner instance;

	// Token: 0x04002CDC RID: 11484
	private KCompactedVector<ScenePartitionerEntry> scenePartitionerEntries = new KCompactedVector<ScenePartitionerEntry>(0);

	// Token: 0x04002CDD RID: 11485
	private List<int> changedCells = new List<int>();

	// Token: 0x020016E6 RID: 5862
	public interface Iterator
	{
		// Token: 0x06008906 RID: 35078
		void Iterate(object obj);

		// Token: 0x06008907 RID: 35079
		void Cleanup();
	}
}
