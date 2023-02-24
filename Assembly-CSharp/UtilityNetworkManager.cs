using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009C4 RID: 2500
public class UtilityNetworkManager<NetworkType, ItemType> : IUtilityNetworkMgr where NetworkType : UtilityNetwork, new() where ItemType : MonoBehaviour
{
	// Token: 0x1700057C RID: 1404
	// (get) Token: 0x06004A34 RID: 18996 RVA: 0x0019F382 File Offset: 0x0019D582
	public bool IsDirty
	{
		get
		{
			return this.dirty;
		}
	}

	// Token: 0x06004A35 RID: 18997 RVA: 0x0019F38C File Offset: 0x0019D58C
	public UtilityNetworkManager(int game_width, int game_height, int tile_layer)
	{
		this.tileLayer = tile_layer;
		this.networks = new List<UtilityNetwork>();
		this.Initialize(game_width, game_height);
	}

	// Token: 0x06004A36 RID: 18998 RVA: 0x0019F418 File Offset: 0x0019D618
	public void Initialize(int game_width, int game_height)
	{
		this.networks.Clear();
		this.physicalGrid = new UtilityNetworkGridNode[game_width * game_height];
		this.visualGrid = new UtilityNetworkGridNode[game_width * game_height];
		this.stashedVisualGrid = new UtilityNetworkGridNode[game_width * game_height];
		this.physicalNodes = new HashSet<int>();
		this.visualNodes = new HashSet<int>();
		this.visitedCells = new HashSet<int>();
		this.visitedVirtualKeys = new HashSet<object>();
		this.queuedVirtualKeys = new HashSet<object>();
		for (int i = 0; i < this.visualGrid.Length; i++)
		{
			this.visualGrid[i] = new UtilityNetworkGridNode
			{
				networkIdx = -1,
				connections = (UtilityConnections)0
			};
			this.physicalGrid[i] = new UtilityNetworkGridNode
			{
				networkIdx = -1,
				connections = (UtilityConnections)0
			};
		}
	}

	// Token: 0x06004A37 RID: 18999 RVA: 0x0019F4F0 File Offset: 0x0019D6F0
	public void Update()
	{
		if (this.dirty)
		{
			this.dirty = false;
			for (int i = 0; i < this.networks.Count; i++)
			{
				this.networks[i].Reset(this.physicalGrid);
			}
			this.networks.Clear();
			this.virtualKeyToNetworkIdx.Clear();
			this.RebuildNetworks(this.tileLayer, false);
			this.RebuildNetworks(this.tileLayer, true);
			if (this.onNetworksRebuilt != null)
			{
				this.onNetworksRebuilt(this.networks, this.GetNodes(true));
			}
		}
	}

	// Token: 0x06004A38 RID: 19000 RVA: 0x0019F58C File Offset: 0x0019D78C
	protected UtilityNetworkGridNode[] GetGrid(bool is_physical_building)
	{
		if (!is_physical_building)
		{
			return this.visualGrid;
		}
		return this.physicalGrid;
	}

	// Token: 0x06004A39 RID: 19001 RVA: 0x0019F59E File Offset: 0x0019D79E
	private HashSet<int> GetNodes(bool is_physical_building)
	{
		if (!is_physical_building)
		{
			return this.visualNodes;
		}
		return this.physicalNodes;
	}

	// Token: 0x06004A3A RID: 19002 RVA: 0x0019F5B0 File Offset: 0x0019D7B0
	public void ClearCell(int cell, bool is_physical_building)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		UtilityNetworkGridNode[] grid = this.GetGrid(is_physical_building);
		HashSet<int> nodes = this.GetNodes(is_physical_building);
		UtilityConnections connections = grid[cell].connections;
		grid[cell].connections = (UtilityConnections)0;
		Vector2I vector2I = Grid.CellToXY(cell);
		if (vector2I.x > 1 && (connections & UtilityConnections.Left) != (UtilityConnections)0)
		{
			UtilityNetworkGridNode[] array = grid;
			int num = Grid.CellLeft(cell);
			array[num].connections = array[num].connections & ~UtilityConnections.Right;
		}
		if (vector2I.x < Grid.WidthInCells - 1 && (connections & UtilityConnections.Right) != (UtilityConnections)0)
		{
			UtilityNetworkGridNode[] array2 = grid;
			int num2 = Grid.CellRight(cell);
			array2[num2].connections = array2[num2].connections & ~UtilityConnections.Left;
		}
		if (vector2I.y > 1 && (connections & UtilityConnections.Down) != (UtilityConnections)0)
		{
			UtilityNetworkGridNode[] array3 = grid;
			int num3 = Grid.CellBelow(cell);
			array3[num3].connections = array3[num3].connections & ~UtilityConnections.Up;
		}
		if (vector2I.y < Grid.HeightInCells - 1 && (connections & UtilityConnections.Up) != (UtilityConnections)0)
		{
			UtilityNetworkGridNode[] array4 = grid;
			int num4 = Grid.CellAbove(cell);
			array4[num4].connections = array4[num4].connections & ~UtilityConnections.Down;
		}
		nodes.Remove(cell);
		if (is_physical_building)
		{
			this.dirty = true;
			this.ClearCell(cell, false);
		}
	}

	// Token: 0x06004A3B RID: 19003 RVA: 0x0019F6B0 File Offset: 0x0019D8B0
	private void QueueCellForVisit(UtilityNetworkGridNode[] grid, int dest_cell, UtilityConnections direction)
	{
		if (!Grid.IsValidCell(dest_cell))
		{
			return;
		}
		if (this.visitedCells.Contains(dest_cell))
		{
			return;
		}
		if (direction != (UtilityConnections)0 && (grid[dest_cell].connections & direction.InverseDirection()) == (UtilityConnections)0)
		{
			return;
		}
		if (Grid.Objects[dest_cell, this.tileLayer] != null)
		{
			this.visitedCells.Add(dest_cell);
			this.queued.Enqueue(dest_cell);
		}
	}

	// Token: 0x06004A3C RID: 19004 RVA: 0x0019F720 File Offset: 0x0019D920
	public void ForceRebuildNetworks()
	{
		this.dirty = true;
	}

	// Token: 0x06004A3D RID: 19005 RVA: 0x0019F72C File Offset: 0x0019D92C
	public void AddToNetworks(int cell, object item, bool is_endpoint)
	{
		if (item != null)
		{
			if (is_endpoint)
			{
				if (this.endpoints.ContainsKey(cell))
				{
					global::Debug.LogWarning(string.Format("Cell {0} already has a utility network endpoint assigned. Adding {1} will stomp previous endpoint, destroying the object that's already there.", cell, item.ToString()));
					KMonoBehaviour kmonoBehaviour = this.endpoints[cell] as KMonoBehaviour;
					if (kmonoBehaviour != null)
					{
						Util.KDestroyGameObject(kmonoBehaviour);
					}
				}
				this.endpoints[cell] = item;
			}
			else
			{
				if (this.items.ContainsKey(cell))
				{
					global::Debug.LogWarning(string.Format("Cell {0} already has a utility network connector assigned. Adding {1} will stomp previous item, destroying the object that's already there.", cell, item.ToString()));
					KMonoBehaviour kmonoBehaviour2 = this.items[cell] as KMonoBehaviour;
					if (kmonoBehaviour2 != null)
					{
						Util.KDestroyGameObject(kmonoBehaviour2);
					}
				}
				this.items[cell] = item;
			}
		}
		this.dirty = true;
	}

	// Token: 0x06004A3E RID: 19006 RVA: 0x0019F7FC File Offset: 0x0019D9FC
	public void AddToVirtualNetworks(object key, object item, bool is_endpoint)
	{
		if (item != null)
		{
			if (is_endpoint)
			{
				if (!this.virtualEndpoints.ContainsKey(key))
				{
					this.virtualEndpoints[key] = new List<object>();
				}
				this.virtualEndpoints[key].Add(item);
			}
			else
			{
				if (!this.virtualItems.ContainsKey(key))
				{
					this.virtualItems[key] = new List<object>();
				}
				this.virtualItems[key].Add(item);
			}
		}
		this.dirty = true;
	}

	// Token: 0x06004A3F RID: 19007 RVA: 0x0019F87C File Offset: 0x0019DA7C
	private unsafe void Reconnect(int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		int* ptr = stackalloc int[(UIntPtr)16];
		int* ptr2 = stackalloc int[(UIntPtr)16];
		int* ptr3 = stackalloc int[(UIntPtr)16];
		int num = 0;
		if (vector2I.y < Grid.HeightInCells - 1)
		{
			ptr[num] = Grid.CellAbove(cell);
			ptr2[num] = 4;
			ptr3[num] = 8;
			num++;
		}
		if (vector2I.y > 1)
		{
			ptr[num] = Grid.CellBelow(cell);
			ptr2[num] = 8;
			ptr3[num] = 4;
			num++;
		}
		if (vector2I.x > 1)
		{
			ptr[num] = Grid.CellLeft(cell);
			ptr2[num] = 1;
			ptr3[num] = 2;
			num++;
		}
		if (vector2I.x < Grid.WidthInCells - 1)
		{
			ptr[num] = Grid.CellRight(cell);
			ptr2[num] = 2;
			ptr3[num] = 1;
			num++;
		}
		UtilityConnections connections = this.physicalGrid[cell].connections;
		UtilityConnections connections2 = this.visualGrid[cell].connections;
		for (int i = 0; i < num; i++)
		{
			int num2 = ptr[i];
			UtilityConnections utilityConnections = (UtilityConnections)ptr2[i];
			UtilityConnections utilityConnections2 = (UtilityConnections)ptr3[i];
			if ((connections & utilityConnections) != (UtilityConnections)0)
			{
				if (this.physicalNodes.Contains(num2))
				{
					UtilityNetworkGridNode[] array = this.physicalGrid;
					int num3 = num2;
					array[num3].connections = array[num3].connections | utilityConnections2;
				}
				if (this.visualNodes.Contains(num2))
				{
					UtilityNetworkGridNode[] array2 = this.visualGrid;
					int num4 = num2;
					array2[num4].connections = array2[num4].connections | utilityConnections2;
				}
			}
			else if ((connections2 & utilityConnections) != (UtilityConnections)0 && (this.physicalNodes.Contains(num2) || this.visualNodes.Contains(num2)))
			{
				UtilityNetworkGridNode[] array3 = this.visualGrid;
				int num5 = num2;
				array3[num5].connections = array3[num5].connections | utilityConnections2;
			}
		}
	}

	// Token: 0x06004A40 RID: 19008 RVA: 0x0019FA5C File Offset: 0x0019DC5C
	public void RemoveFromVirtualNetworks(object key, object item, bool is_endpoint)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.dirty = true;
		if (item != null)
		{
			if (is_endpoint)
			{
				this.virtualEndpoints[key].Remove(item);
				if (this.virtualEndpoints[key].Count == 0)
				{
					this.virtualEndpoints.Remove(key);
				}
			}
			else
			{
				this.virtualItems[key].Remove(item);
				if (this.virtualItems[key].Count == 0)
				{
					this.virtualItems.Remove(key);
				}
			}
			UtilityNetwork networkForVirtualKey = this.GetNetworkForVirtualKey(key);
			if (networkForVirtualKey != null)
			{
				networkForVirtualKey.RemoveItem(item);
			}
		}
	}

	// Token: 0x06004A41 RID: 19009 RVA: 0x0019FAF8 File Offset: 0x0019DCF8
	public void RemoveFromNetworks(int cell, object item, bool is_endpoint)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.dirty = true;
		if (item != null)
		{
			if (is_endpoint)
			{
				this.endpoints.Remove(cell);
				int networkIdx = this.physicalGrid[cell].networkIdx;
				if (networkIdx != -1)
				{
					this.networks[networkIdx].RemoveItem(item);
					return;
				}
			}
			else
			{
				int networkIdx2 = this.physicalGrid[cell].networkIdx;
				this.physicalGrid[cell].connections = (UtilityConnections)0;
				this.physicalGrid[cell].networkIdx = -1;
				this.items.Remove(cell);
				this.Disconnect(cell);
				object obj;
				if (this.endpoints.TryGetValue(cell, out obj) && networkIdx2 != -1)
				{
					this.networks[networkIdx2].DisconnectItem(obj);
				}
			}
		}
	}

	// Token: 0x06004A42 RID: 19010 RVA: 0x0019FBC8 File Offset: 0x0019DDC8
	private unsafe void Disconnect(int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		int num = 0;
		int* ptr = stackalloc int[(UIntPtr)16];
		int* ptr2 = stackalloc int[(UIntPtr)16];
		if (vector2I.y < Grid.HeightInCells - 1)
		{
			ptr[num] = Grid.CellAbove(cell);
			ptr2[num] = -9;
			num++;
		}
		if (vector2I.y > 1)
		{
			ptr[num] = Grid.CellBelow(cell);
			ptr2[num] = -5;
			num++;
		}
		if (vector2I.x > 1)
		{
			ptr[num] = Grid.CellLeft(cell);
			ptr2[num] = -3;
			num++;
		}
		if (vector2I.x < Grid.WidthInCells - 1)
		{
			ptr[num] = Grid.CellRight(cell);
			ptr2[num] = -2;
			num++;
		}
		for (int i = 0; i < num; i++)
		{
			int num2 = ptr[i];
			int num3 = ptr2[i];
			int num4 = (int)(this.physicalGrid[num2].connections & (UtilityConnections)num3);
			this.physicalGrid[num2].connections = (UtilityConnections)num4;
		}
	}

	// Token: 0x06004A43 RID: 19011 RVA: 0x0019FCD4 File Offset: 0x0019DED4
	private unsafe void RebuildNetworks(int layer, bool is_physical)
	{
		UtilityNetworkGridNode[] grid = this.GetGrid(is_physical);
		HashSet<int> nodes = this.GetNodes(is_physical);
		this.visitedCells.Clear();
		this.visitedVirtualKeys.Clear();
		this.queuedVirtualKeys.Clear();
		this.queued.Clear();
		int* ptr = stackalloc int[(UIntPtr)16];
		int* ptr2 = stackalloc int[(UIntPtr)16];
		foreach (int num in nodes)
		{
			UtilityNetworkGridNode utilityNetworkGridNode = grid[num];
			if (!this.visitedCells.Contains(num))
			{
				this.queued.Enqueue(num);
				this.visitedCells.Add(num);
				NetworkType networkType = new NetworkType();
				networkType.id = this.networks.Count;
				this.networks.Add(networkType);
				while (this.queued.Count > 0)
				{
					int num2 = this.queued.Dequeue();
					utilityNetworkGridNode = grid[num2];
					object obj = null;
					object obj2 = null;
					if (is_physical)
					{
						if (this.items.TryGetValue(num2, out obj))
						{
							if (obj is IDisconnectable && (obj as IDisconnectable).IsDisconnected())
							{
								continue;
							}
							if (obj != null)
							{
								networkType.AddItem(obj);
							}
						}
						if (this.endpoints.TryGetValue(num2, out obj2) && obj2 != null)
						{
							networkType.AddItem(obj2);
						}
					}
					grid[num2].networkIdx = networkType.id;
					if (obj != null && obj2 != null)
					{
						networkType.ConnectItem(obj2);
					}
					Vector2I vector2I = Grid.CellToXY(num2);
					int num3 = 0;
					if (vector2I.x >= 0)
					{
						ptr[num3] = Grid.CellLeft(num2);
						ptr2[num3] = 1;
						num3++;
					}
					if (vector2I.x < Grid.WidthInCells)
					{
						ptr[num3] = Grid.CellRight(num2);
						ptr2[num3] = 2;
						num3++;
					}
					if (vector2I.y >= 0)
					{
						ptr[num3] = Grid.CellBelow(num2);
						ptr2[num3] = 8;
						num3++;
					}
					if (vector2I.y < Grid.HeightInCells)
					{
						ptr[num3] = Grid.CellAbove(num2);
						ptr2[num3] = 4;
						num3++;
					}
					for (int i = 0; i < num3; i++)
					{
						int num4 = ptr2[i];
						if ((utilityNetworkGridNode.connections & (UtilityConnections)num4) != (UtilityConnections)0)
						{
							int num5 = ptr[i];
							this.QueueCellForVisit(grid, num5, (UtilityConnections)num4);
						}
					}
					int num6;
					if (this.links.TryGetValue(num2, out num6))
					{
						this.QueueCellForVisit(grid, num6, (UtilityConnections)0);
					}
					object obj3;
					if (this.semiVirtualLinks.TryGetValue(num2, out obj3) && !this.visitedVirtualKeys.Contains(obj3))
					{
						this.visitedVirtualKeys.Add(obj3);
						this.virtualKeyToNetworkIdx[obj3] = networkType.id;
						if (this.virtualItems.ContainsKey(obj3))
						{
							foreach (object obj4 in this.virtualItems[obj3])
							{
								networkType.AddItem(obj4);
								networkType.ConnectItem(obj4);
							}
						}
						if (this.virtualEndpoints.ContainsKey(obj3))
						{
							foreach (object obj5 in this.virtualEndpoints[obj3])
							{
								networkType.AddItem(obj5);
								networkType.ConnectItem(obj5);
							}
						}
						foreach (KeyValuePair<int, object> keyValuePair in this.semiVirtualLinks)
						{
							if (keyValuePair.Value == obj3)
							{
								this.QueueCellForVisit(grid, keyValuePair.Key, (UtilityConnections)0);
							}
						}
					}
				}
			}
		}
		foreach (KeyValuePair<object, List<object>> keyValuePair2 in this.virtualItems)
		{
			if (!this.visitedVirtualKeys.Contains(keyValuePair2.Key))
			{
				NetworkType networkType2 = new NetworkType();
				networkType2.id = this.networks.Count;
				this.visitedVirtualKeys.Add(keyValuePair2.Key);
				this.virtualKeyToNetworkIdx[keyValuePair2.Key] = networkType2.id;
				foreach (object obj6 in keyValuePair2.Value)
				{
					networkType2.AddItem(obj6);
					networkType2.ConnectItem(obj6);
				}
				foreach (object obj7 in this.virtualEndpoints[keyValuePair2.Key])
				{
					networkType2.AddItem(obj7);
					networkType2.ConnectItem(obj7);
				}
				this.networks.Add(networkType2);
			}
		}
		foreach (KeyValuePair<object, List<object>> keyValuePair3 in this.virtualEndpoints)
		{
			if (!this.visitedVirtualKeys.Contains(keyValuePair3.Key))
			{
				NetworkType networkType3 = new NetworkType();
				networkType3.id = this.networks.Count;
				this.visitedVirtualKeys.Add(keyValuePair3.Key);
				this.virtualKeyToNetworkIdx[keyValuePair3.Key] = networkType3.id;
				foreach (object obj8 in this.virtualEndpoints[keyValuePair3.Key])
				{
					networkType3.AddItem(obj8);
					networkType3.ConnectItem(obj8);
				}
				this.networks.Add(networkType3);
			}
		}
	}

	// Token: 0x06004A44 RID: 19012 RVA: 0x001A0424 File Offset: 0x0019E624
	public UtilityNetwork GetNetworkForVirtualKey(object key)
	{
		int num;
		if (this.virtualKeyToNetworkIdx.TryGetValue(key, out num))
		{
			return this.networks[num];
		}
		return null;
	}

	// Token: 0x06004A45 RID: 19013 RVA: 0x001A0450 File Offset: 0x0019E650
	public UtilityNetwork GetNetworkByID(int id)
	{
		UtilityNetwork utilityNetwork = null;
		if (0 <= id && id < this.networks.Count)
		{
			utilityNetwork = this.networks[id];
		}
		return utilityNetwork;
	}

	// Token: 0x06004A46 RID: 19014 RVA: 0x001A0480 File Offset: 0x0019E680
	public UtilityNetwork GetNetworkForCell(int cell)
	{
		UtilityNetwork utilityNetwork = null;
		if (Grid.IsValidCell(cell) && 0 <= this.physicalGrid[cell].networkIdx && this.physicalGrid[cell].networkIdx < this.networks.Count)
		{
			utilityNetwork = this.networks[this.physicalGrid[cell].networkIdx];
		}
		return utilityNetwork;
	}

	// Token: 0x06004A47 RID: 19015 RVA: 0x001A04E8 File Offset: 0x0019E6E8
	public UtilityNetwork GetNetworkForDirection(int cell, Direction direction)
	{
		cell = Grid.GetCellInDirection(cell, direction);
		if (!Grid.IsValidCell(cell))
		{
			return null;
		}
		UtilityNetworkGridNode utilityNetworkGridNode = this.GetGrid(true)[cell];
		UtilityNetwork utilityNetwork = null;
		if (utilityNetworkGridNode.networkIdx != -1 && utilityNetworkGridNode.networkIdx < this.networks.Count)
		{
			utilityNetwork = this.networks[utilityNetworkGridNode.networkIdx];
		}
		return utilityNetwork;
	}

	// Token: 0x06004A48 RID: 19016 RVA: 0x001A0548 File Offset: 0x0019E748
	private UtilityConnections GetNeighboursAsConnections(int cell, HashSet<int> nodes)
	{
		UtilityConnections utilityConnections = (UtilityConnections)0;
		Vector2I vector2I = Grid.CellToXY(cell);
		if (vector2I.x > 1 && nodes.Contains(Grid.CellLeft(cell)))
		{
			utilityConnections |= UtilityConnections.Left;
		}
		if (vector2I.x < Grid.WidthInCells - 1 && nodes.Contains(Grid.CellRight(cell)))
		{
			utilityConnections |= UtilityConnections.Right;
		}
		if (vector2I.y > 1 && nodes.Contains(Grid.CellBelow(cell)))
		{
			utilityConnections |= UtilityConnections.Down;
		}
		if (vector2I.y < Grid.HeightInCells - 1 && nodes.Contains(Grid.CellAbove(cell)))
		{
			utilityConnections |= UtilityConnections.Up;
		}
		return utilityConnections;
	}

	// Token: 0x06004A49 RID: 19017 RVA: 0x001A05D8 File Offset: 0x0019E7D8
	public virtual void SetConnections(UtilityConnections connections, int cell, bool is_physical_building)
	{
		HashSet<int> nodes = this.GetNodes(is_physical_building);
		nodes.Add(cell);
		this.visualGrid[cell].connections = connections;
		if (is_physical_building)
		{
			this.dirty = true;
			UtilityConnections utilityConnections = (is_physical_building ? (connections & this.GetNeighboursAsConnections(cell, nodes)) : connections);
			this.physicalGrid[cell].connections = utilityConnections;
		}
		this.Reconnect(cell);
	}

	// Token: 0x06004A4A RID: 19018 RVA: 0x001A063C File Offset: 0x0019E83C
	public UtilityConnections GetConnections(int cell, bool is_physical_building)
	{
		UtilityNetworkGridNode[] array = this.GetGrid(is_physical_building);
		UtilityConnections utilityConnections = array[cell].connections;
		if (!is_physical_building)
		{
			array = this.GetGrid(true);
			utilityConnections |= array[cell].connections;
		}
		return utilityConnections;
	}

	// Token: 0x06004A4B RID: 19019 RVA: 0x001A067C File Offset: 0x0019E87C
	public UtilityConnections GetDisplayConnections(int cell)
	{
		UtilityConnections utilityConnections = (UtilityConnections)0;
		UtilityNetworkGridNode[] array = this.GetGrid(false);
		UtilityConnections utilityConnections2 = utilityConnections | array[cell].connections;
		array = this.GetGrid(true);
		return utilityConnections2 | array[cell].connections;
	}

	// Token: 0x06004A4C RID: 19020 RVA: 0x001A06B4 File Offset: 0x0019E8B4
	public virtual bool CanAddConnection(UtilityConnections new_connection, int cell, bool is_physical_building, out string fail_reason)
	{
		fail_reason = null;
		return true;
	}

	// Token: 0x06004A4D RID: 19021 RVA: 0x001A06BC File Offset: 0x0019E8BC
	public void AddConnection(UtilityConnections new_connection, int cell, bool is_physical_building)
	{
		string text;
		if (this.CanAddConnection(new_connection, cell, is_physical_building, out text))
		{
			if (is_physical_building)
			{
				this.dirty = true;
			}
			UtilityNetworkGridNode[] grid = this.GetGrid(is_physical_building);
			UtilityConnections connections = grid[cell].connections;
			grid[cell].connections = connections | new_connection;
		}
	}

	// Token: 0x06004A4E RID: 19022 RVA: 0x001A0702 File Offset: 0x0019E902
	public void StashVisualGrids()
	{
		Array.Copy(this.visualGrid, this.stashedVisualGrid, this.visualGrid.Length);
	}

	// Token: 0x06004A4F RID: 19023 RVA: 0x001A071D File Offset: 0x0019E91D
	public void UnstashVisualGrids()
	{
		Array.Copy(this.stashedVisualGrid, this.visualGrid, this.visualGrid.Length);
	}

	// Token: 0x06004A50 RID: 19024 RVA: 0x001A0738 File Offset: 0x0019E938
	public string GetVisualizerString(int cell)
	{
		UtilityConnections displayConnections = this.GetDisplayConnections(cell);
		return this.GetVisualizerString(displayConnections);
	}

	// Token: 0x06004A51 RID: 19025 RVA: 0x001A0754 File Offset: 0x0019E954
	public string GetVisualizerString(UtilityConnections connections)
	{
		string text = "";
		if ((connections & UtilityConnections.Left) != (UtilityConnections)0)
		{
			text += "L";
		}
		if ((connections & UtilityConnections.Right) != (UtilityConnections)0)
		{
			text += "R";
		}
		if ((connections & UtilityConnections.Up) != (UtilityConnections)0)
		{
			text += "U";
		}
		if ((connections & UtilityConnections.Down) != (UtilityConnections)0)
		{
			text += "D";
		}
		if (text == "")
		{
			text = "None";
		}
		return text;
	}

	// Token: 0x06004A52 RID: 19026 RVA: 0x001A07C0 File Offset: 0x0019E9C0
	public object GetEndpoint(int cell)
	{
		object obj = null;
		this.endpoints.TryGetValue(cell, out obj);
		return obj;
	}

	// Token: 0x06004A53 RID: 19027 RVA: 0x001A07DF File Offset: 0x0019E9DF
	public void AddSemiVirtualLink(int cell1, object virtualKey)
	{
		global::Debug.Assert(virtualKey != null, "Can not use a null key for a virtual network");
		this.semiVirtualLinks[cell1] = virtualKey;
		this.dirty = true;
	}

	// Token: 0x06004A54 RID: 19028 RVA: 0x001A0803 File Offset: 0x0019EA03
	public void RemoveSemiVirtualLink(int cell1, object virtualKey)
	{
		global::Debug.Assert(virtualKey != null, "Can not use a null key for a virtual network");
		this.semiVirtualLinks.Remove(cell1);
		this.dirty = true;
	}

	// Token: 0x06004A55 RID: 19029 RVA: 0x001A0827 File Offset: 0x0019EA27
	public void AddLink(int cell1, int cell2)
	{
		this.links[cell1] = cell2;
		this.links[cell2] = cell1;
		this.dirty = true;
	}

	// Token: 0x06004A56 RID: 19030 RVA: 0x001A084A File Offset: 0x0019EA4A
	public void RemoveLink(int cell1, int cell2)
	{
		this.links.Remove(cell1);
		this.links.Remove(cell2);
		this.dirty = true;
	}

	// Token: 0x06004A57 RID: 19031 RVA: 0x001A086D File Offset: 0x0019EA6D
	public void AddNetworksRebuiltListener(Action<IList<UtilityNetwork>, ICollection<int>> listener)
	{
		this.onNetworksRebuilt = (Action<IList<UtilityNetwork>, ICollection<int>>)Delegate.Combine(this.onNetworksRebuilt, listener);
	}

	// Token: 0x06004A58 RID: 19032 RVA: 0x001A0886 File Offset: 0x0019EA86
	public void RemoveNetworksRebuiltListener(Action<IList<UtilityNetwork>, ICollection<int>> listener)
	{
		this.onNetworksRebuilt = (Action<IList<UtilityNetwork>, ICollection<int>>)Delegate.Remove(this.onNetworksRebuilt, listener);
	}

	// Token: 0x06004A59 RID: 19033 RVA: 0x001A089F File Offset: 0x0019EA9F
	public IList<UtilityNetwork> GetNetworks()
	{
		return this.networks;
	}

	// Token: 0x040030B2 RID: 12466
	private Dictionary<int, object> items = new Dictionary<int, object>();

	// Token: 0x040030B3 RID: 12467
	private Dictionary<int, object> endpoints = new Dictionary<int, object>();

	// Token: 0x040030B4 RID: 12468
	private Dictionary<object, List<object>> virtualItems = new Dictionary<object, List<object>>();

	// Token: 0x040030B5 RID: 12469
	private Dictionary<object, List<object>> virtualEndpoints = new Dictionary<object, List<object>>();

	// Token: 0x040030B6 RID: 12470
	private Dictionary<int, int> links = new Dictionary<int, int>();

	// Token: 0x040030B7 RID: 12471
	private Dictionary<int, object> semiVirtualLinks = new Dictionary<int, object>();

	// Token: 0x040030B8 RID: 12472
	private List<UtilityNetwork> networks;

	// Token: 0x040030B9 RID: 12473
	private Dictionary<object, int> virtualKeyToNetworkIdx = new Dictionary<object, int>();

	// Token: 0x040030BA RID: 12474
	private HashSet<int> visitedCells;

	// Token: 0x040030BB RID: 12475
	private HashSet<object> visitedVirtualKeys;

	// Token: 0x040030BC RID: 12476
	private HashSet<object> queuedVirtualKeys;

	// Token: 0x040030BD RID: 12477
	private Action<IList<UtilityNetwork>, ICollection<int>> onNetworksRebuilt;

	// Token: 0x040030BE RID: 12478
	private Queue<int> queued = new Queue<int>();

	// Token: 0x040030BF RID: 12479
	protected UtilityNetworkGridNode[] visualGrid;

	// Token: 0x040030C0 RID: 12480
	private UtilityNetworkGridNode[] stashedVisualGrid;

	// Token: 0x040030C1 RID: 12481
	protected UtilityNetworkGridNode[] physicalGrid;

	// Token: 0x040030C2 RID: 12482
	protected HashSet<int> physicalNodes;

	// Token: 0x040030C3 RID: 12483
	protected HashSet<int> visualNodes;

	// Token: 0x040030C4 RID: 12484
	private bool dirty;

	// Token: 0x040030C5 RID: 12485
	private int tileLayer = -1;
}
