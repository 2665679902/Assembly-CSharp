using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x020003C5 RID: 965
public class PathFinder
{
	// Token: 0x060013FC RID: 5116 RVA: 0x00069D70 File Offset: 0x00067F70
	public static void Initialize()
	{
		NavType[] array = new NavType[11];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (NavType)i;
		}
		PathFinder.PathGrid = new PathGrid(Grid.WidthInCells, Grid.HeightInCells, false, array);
		for (int j = 0; j < Grid.CellCount; j++)
		{
			if (Grid.Visible[j] > 0 || Grid.Spawnable[j] > 0)
			{
				ListPool<int, PathFinder>.PooledList pooledList = ListPool<int, PathFinder>.Allocate();
				GameUtil.FloodFillConditional(j, PathFinder.allowPathfindingFloodFillCb, pooledList, null);
				Grid.AllowPathfinding[j] = true;
				pooledList.Recycle();
			}
		}
		Grid.OnReveal = (Action<int>)Delegate.Combine(Grid.OnReveal, new Action<int>(PathFinder.OnReveal));
	}

	// Token: 0x060013FD RID: 5117 RVA: 0x00069E17 File Offset: 0x00068017
	private static void OnReveal(int cell)
	{
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x00069E19 File Offset: 0x00068019
	public static void UpdatePath(NavGrid nav_grid, PathFinderAbilities abilities, PathFinder.PotentialPath potential_path, PathFinderQuery query, ref PathFinder.Path path)
	{
		PathFinder.Run(nav_grid, abilities, potential_path, query, ref path);
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x00069E28 File Offset: 0x00068028
	public static bool ValidatePath(NavGrid nav_grid, PathFinderAbilities abilities, ref PathFinder.Path path)
	{
		if (!path.IsValid())
		{
			return false;
		}
		for (int i = 0; i < path.nodes.Count; i++)
		{
			PathFinder.Path.Node node = path.nodes[i];
			if (i < path.nodes.Count - 1)
			{
				PathFinder.Path.Node node2 = path.nodes[i + 1];
				int num = node.cell * nav_grid.maxLinksPerCell;
				bool flag = false;
				NavGrid.Link link = nav_grid.Links[num];
				while (link.link != PathFinder.InvalidHandle)
				{
					if (link.link == node2.cell && node2.navType == link.endNavType && node.navType == link.startNavType)
					{
						PathFinder.PotentialPath potentialPath = new PathFinder.PotentialPath(node.cell, node.navType, PathFinder.PotentialPath.Flags.None);
						flag = abilities.TraversePath(ref potentialPath, node.cell, node.navType, 0, (int)link.transitionId, false);
						if (flag)
						{
							break;
						}
					}
					num++;
					link = nav_grid.Links[num];
				}
				if (!flag)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x00069F3C File Offset: 0x0006813C
	public static void Run(NavGrid nav_grid, PathFinderAbilities abilities, PathFinder.PotentialPath potential_path, PathFinderQuery query)
	{
		int invalidCell = PathFinder.InvalidCell;
		NavType navType = NavType.NumNavTypes;
		query.ClearResult();
		if (!Grid.IsValidCell(potential_path.cell))
		{
			return;
		}
		PathFinder.FindPaths(nav_grid, ref abilities, potential_path, query, PathFinder.Temp.Potentials, ref invalidCell, ref navType);
		if (invalidCell != PathFinder.InvalidCell)
		{
			bool flag = false;
			PathFinder.Cell cell = PathFinder.PathGrid.GetCell(invalidCell, navType, out flag);
			query.SetResult(invalidCell, cell.cost, navType);
		}
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x00069FA0 File Offset: 0x000681A0
	public static void Run(NavGrid nav_grid, PathFinderAbilities abilities, PathFinder.PotentialPath potential_path, PathFinderQuery query, ref PathFinder.Path path)
	{
		PathFinder.Run(nav_grid, abilities, potential_path, query);
		if (query.GetResultCell() != PathFinder.InvalidCell)
		{
			PathFinder.BuildResultPath(query.GetResultCell(), query.GetResultNavType(), ref path);
			return;
		}
		path.Clear();
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x00069FD4 File Offset: 0x000681D4
	private static void BuildResultPath(int path_cell, NavType path_nav_type, ref PathFinder.Path path)
	{
		if (path_cell != PathFinder.InvalidCell)
		{
			bool flag = false;
			PathFinder.Cell cell = PathFinder.PathGrid.GetCell(path_cell, path_nav_type, out flag);
			path.Clear();
			path.cost = cell.cost;
			while (path_cell != PathFinder.InvalidCell)
			{
				path.AddNode(new PathFinder.Path.Node
				{
					cell = path_cell,
					navType = cell.navType,
					transitionId = cell.transitionId
				});
				path_cell = cell.parent;
				if (path_cell != PathFinder.InvalidCell)
				{
					cell = PathFinder.PathGrid.GetCell(path_cell, cell.parentNavType, out flag);
				}
			}
			if (path.nodes != null)
			{
				for (int i = 0; i < path.nodes.Count / 2; i++)
				{
					PathFinder.Path.Node node = path.nodes[i];
					path.nodes[i] = path.nodes[path.nodes.Count - i - 1];
					path.nodes[path.nodes.Count - i - 1] = node;
				}
			}
		}
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x0006A0E0 File Offset: 0x000682E0
	private static void FindPaths(NavGrid nav_grid, ref PathFinderAbilities abilities, PathFinder.PotentialPath potential_path, PathFinderQuery query, PathFinder.PotentialList potentials, ref int result_cell, ref NavType result_nav_type)
	{
		potentials.Clear();
		PathFinder.PathGrid.ResetUpdate();
		PathFinder.PathGrid.BeginUpdate(potential_path.cell, false);
		bool flag;
		PathFinder.Cell cell = PathFinder.PathGrid.GetCell(potential_path, out flag);
		PathFinder.AddPotential(potential_path, Grid.InvalidCell, NavType.NumNavTypes, 0, 0, potentials, PathFinder.PathGrid, ref cell);
		int num = int.MaxValue;
		while (potentials.Count > 0)
		{
			KeyValuePair<int, PathFinder.PotentialPath> keyValuePair = potentials.Next();
			cell = PathFinder.PathGrid.GetCell(keyValuePair.Value, out flag);
			if (cell.cost == keyValuePair.Key)
			{
				if (cell.navType != NavType.Tube && query.IsMatch(keyValuePair.Value.cell, cell.parent, cell.cost) && cell.cost < num)
				{
					result_cell = keyValuePair.Value.cell;
					num = cell.cost;
					result_nav_type = cell.navType;
					break;
				}
				PathFinder.AddPotentials(nav_grid.potentialScratchPad, keyValuePair.Value, cell.cost, ref abilities, query, nav_grid.maxLinksPerCell, nav_grid.Links, potentials, PathFinder.PathGrid, cell.parent, cell.parentNavType);
			}
		}
		PathFinder.PathGrid.EndUpdate(true);
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x0006A21A File Offset: 0x0006841A
	public static void AddPotential(PathFinder.PotentialPath potential_path, int parent_cell, NavType parent_nav_type, int cost, byte transition_id, PathFinder.PotentialList potentials, PathGrid path_grid, ref PathFinder.Cell cell_data)
	{
		cell_data.cost = cost;
		cell_data.parent = parent_cell;
		cell_data.SetNavTypes(potential_path.navType, parent_nav_type);
		cell_data.transitionId = transition_id;
		potentials.Add(cost, potential_path);
		path_grid.SetCell(potential_path, ref cell_data);
	}

	// Token: 0x06001405 RID: 5125 RVA: 0x0006A256 File Offset: 0x00068456
	[Conditional("ENABLE_PATH_DETAILS")]
	private static void BeginDetailSample(string region_name)
	{
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x0006A258 File Offset: 0x00068458
	[Conditional("ENABLE_PATH_DETAILS")]
	private static void EndDetailSample(string region_name)
	{
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x0006A25C File Offset: 0x0006845C
	public static bool IsSubmerged(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		int num = Grid.CellAbove(cell);
		return (Grid.IsValidCell(num) && Grid.Element[num].IsLiquid) || (Grid.Element[cell].IsLiquid && Grid.IsValidCell(num) && Grid.Element[num].IsSolid);
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x0006A2BC File Offset: 0x000684BC
	public static void AddPotentials(PathFinder.PotentialScratchPad potential_scratch_pad, PathFinder.PotentialPath potential, int cost, ref PathFinderAbilities abilities, PathFinderQuery query, int max_links_per_cell, NavGrid.Link[] links, PathFinder.PotentialList potentials, PathGrid path_grid, int parent_cell, NavType parent_nav_type)
	{
		if (!Grid.IsValidCell(potential.cell))
		{
			return;
		}
		int num = 0;
		NavGrid.Link[] linksWithCorrectNavType = potential_scratch_pad.linksWithCorrectNavType;
		int num2 = potential.cell * max_links_per_cell;
		NavGrid.Link link = links[num2];
		for (int num3 = link.link; num3 != PathFinder.InvalidHandle; num3 = link.link)
		{
			if (link.startNavType == potential.navType && (parent_cell != num3 || parent_nav_type != link.startNavType))
			{
				linksWithCorrectNavType[num++] = link;
			}
			num2++;
			link = links[num2];
		}
		int num4 = 0;
		PathFinder.PotentialScratchPad.PathGridCellData[] linksInCellRange = potential_scratch_pad.linksInCellRange;
		for (int i = 0; i < num; i++)
		{
			NavGrid.Link link2 = linksWithCorrectNavType[i];
			int link3 = link2.link;
			bool flag = false;
			PathFinder.Cell cell = path_grid.GetCell(link3, link2.endNavType, out flag);
			if (flag)
			{
				int num5 = cost + (int)link2.cost;
				bool flag2 = cell.cost == -1;
				bool flag3 = num5 < cell.cost;
				if (flag2 || flag3)
				{
					linksInCellRange[num4++] = new PathFinder.PotentialScratchPad.PathGridCellData
					{
						pathGridCell = cell,
						link = link2
					};
				}
			}
		}
		for (int j = 0; j < num4; j++)
		{
			PathFinder.PotentialScratchPad.PathGridCellData pathGridCellData = linksInCellRange[j];
			int link4 = pathGridCellData.link.link;
			pathGridCellData.isSubmerged = PathFinder.IsSubmerged(link4);
			linksInCellRange[j] = pathGridCellData;
		}
		for (int k = 0; k < num4; k++)
		{
			PathFinder.PotentialScratchPad.PathGridCellData pathGridCellData2 = linksInCellRange[k];
			NavGrid.Link link5 = pathGridCellData2.link;
			int link6 = link5.link;
			PathFinder.Cell pathGridCell = pathGridCellData2.pathGridCell;
			int num6 = cost + (int)link5.cost;
			PathFinder.PotentialPath potentialPath = potential;
			potentialPath.cell = link6;
			potentialPath.navType = link5.endNavType;
			if (pathGridCellData2.isSubmerged)
			{
				int submergedPathCostPenalty = abilities.GetSubmergedPathCostPenalty(potentialPath, link5);
				num6 += submergedPathCostPenalty;
			}
			PathFinder.PotentialPath.Flags flags = potentialPath.flags;
			bool flag4 = abilities.TraversePath(ref potentialPath, potential.cell, potential.navType, num6, (int)link5.transitionId, pathGridCellData2.isSubmerged);
			PathFinder.PotentialPath.Flags flags2 = potentialPath.flags;
			if (flag4)
			{
				PathFinder.AddPotential(potentialPath, potential.cell, potential.navType, num6, link5.transitionId, potentials, path_grid, ref pathGridCell);
			}
		}
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x0006A507 File Offset: 0x00068707
	public static void DestroyStatics()
	{
		PathFinder.PathGrid.OnCleanUp();
		PathFinder.PathGrid = null;
		PathFinder.Temp.Potentials.Clear();
	}

	// Token: 0x04000AF9 RID: 2809
	public static int InvalidHandle = -1;

	// Token: 0x04000AFA RID: 2810
	public static int InvalidIdx = -1;

	// Token: 0x04000AFB RID: 2811
	public static int InvalidCell = -1;

	// Token: 0x04000AFC RID: 2812
	public static PathGrid PathGrid;

	// Token: 0x04000AFD RID: 2813
	private static readonly Func<int, bool> allowPathfindingFloodFillCb = delegate(int cell)
	{
		if (Grid.Solid[cell])
		{
			return false;
		}
		if (Grid.AllowPathfinding[cell])
		{
			return false;
		}
		Grid.AllowPathfinding[cell] = true;
		return true;
	};

	// Token: 0x02000FEF RID: 4079
	public struct Cell
	{
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060070E7 RID: 28903 RVA: 0x002A8218 File Offset: 0x002A6418
		public NavType navType
		{
			get
			{
				return (NavType)(this.navTypes & 15);
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060070E8 RID: 28904 RVA: 0x002A8224 File Offset: 0x002A6424
		public NavType parentNavType
		{
			get
			{
				return (NavType)(this.navTypes >> 4);
			}
		}

		// Token: 0x060070E9 RID: 28905 RVA: 0x002A8230 File Offset: 0x002A6430
		public void SetNavTypes(NavType type, NavType parent_type)
		{
			this.navTypes = (byte)(type | (parent_type << 4));
		}

		// Token: 0x040055F5 RID: 22005
		public int cost;

		// Token: 0x040055F6 RID: 22006
		public int parent;

		// Token: 0x040055F7 RID: 22007
		public short queryId;

		// Token: 0x040055F8 RID: 22008
		private byte navTypes;

		// Token: 0x040055F9 RID: 22009
		public byte transitionId;
	}

	// Token: 0x02000FF0 RID: 4080
	public struct PotentialPath
	{
		// Token: 0x060070EA RID: 28906 RVA: 0x002A824D File Offset: 0x002A644D
		public PotentialPath(int cell, NavType nav_type, PathFinder.PotentialPath.Flags flags)
		{
			this.cell = cell;
			this.navType = nav_type;
			this.flags = flags;
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x002A8264 File Offset: 0x002A6464
		public void SetFlags(PathFinder.PotentialPath.Flags new_flags)
		{
			this.flags |= new_flags;
		}

		// Token: 0x060070EC RID: 28908 RVA: 0x002A8274 File Offset: 0x002A6474
		public void ClearFlags(PathFinder.PotentialPath.Flags new_flags)
		{
			this.flags &= ~new_flags;
		}

		// Token: 0x060070ED RID: 28909 RVA: 0x002A8286 File Offset: 0x002A6486
		public bool HasFlag(PathFinder.PotentialPath.Flags flag)
		{
			return this.HasAnyFlag(flag);
		}

		// Token: 0x060070EE RID: 28910 RVA: 0x002A828F File Offset: 0x002A648F
		public bool HasAnyFlag(PathFinder.PotentialPath.Flags mask)
		{
			return (this.flags & mask) > PathFinder.PotentialPath.Flags.None;
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060070EF RID: 28911 RVA: 0x002A829C File Offset: 0x002A649C
		// (set) Token: 0x060070F0 RID: 28912 RVA: 0x002A82A4 File Offset: 0x002A64A4
		public PathFinder.PotentialPath.Flags flags { readonly get; private set; }

		// Token: 0x040055FA RID: 22010
		public int cell;

		// Token: 0x040055FB RID: 22011
		public NavType navType;

		// Token: 0x02001EDF RID: 7903
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x04008A55 RID: 35413
			None = 0,
			// Token: 0x04008A56 RID: 35414
			HasAtmoSuit = 1,
			// Token: 0x04008A57 RID: 35415
			HasJetPack = 2,
			// Token: 0x04008A58 RID: 35416
			HasOxygenMask = 4,
			// Token: 0x04008A59 RID: 35417
			PerformSuitChecks = 8
		}
	}

	// Token: 0x02000FF1 RID: 4081
	public struct Path
	{
		// Token: 0x060070F1 RID: 28913 RVA: 0x002A82AD File Offset: 0x002A64AD
		public void AddNode(PathFinder.Path.Node node)
		{
			if (this.nodes == null)
			{
				this.nodes = new List<PathFinder.Path.Node>();
			}
			this.nodes.Add(node);
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x002A82CE File Offset: 0x002A64CE
		public bool IsValid()
		{
			return this.nodes != null && this.nodes.Count > 1;
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x002A82E8 File Offset: 0x002A64E8
		public bool HasArrived()
		{
			return this.nodes != null && this.nodes.Count > 0;
		}

		// Token: 0x060070F4 RID: 28916 RVA: 0x002A8302 File Offset: 0x002A6502
		public void Clear()
		{
			this.cost = 0;
			if (this.nodes != null)
			{
				this.nodes.Clear();
			}
		}

		// Token: 0x040055FD RID: 22013
		public int cost;

		// Token: 0x040055FE RID: 22014
		public List<PathFinder.Path.Node> nodes;

		// Token: 0x02001EE0 RID: 7904
		public struct Node
		{
			// Token: 0x04008A5A RID: 35418
			public int cell;

			// Token: 0x04008A5B RID: 35419
			public NavType navType;

			// Token: 0x04008A5C RID: 35420
			public byte transitionId;
		}
	}

	// Token: 0x02000FF2 RID: 4082
	public class PotentialList
	{
		// Token: 0x060070F5 RID: 28917 RVA: 0x002A831E File Offset: 0x002A651E
		public KeyValuePair<int, PathFinder.PotentialPath> Next()
		{
			return this.queue.Dequeue();
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060070F6 RID: 28918 RVA: 0x002A832B File Offset: 0x002A652B
		public int Count
		{
			get
			{
				return this.queue.Count;
			}
		}

		// Token: 0x060070F7 RID: 28919 RVA: 0x002A8338 File Offset: 0x002A6538
		public void Add(int cost, PathFinder.PotentialPath path)
		{
			this.queue.Enqueue(cost, path);
		}

		// Token: 0x060070F8 RID: 28920 RVA: 0x002A8347 File Offset: 0x002A6547
		public void Clear()
		{
			this.queue.Clear();
		}

		// Token: 0x040055FF RID: 22015
		private PathFinder.PotentialList.HOTQueue<PathFinder.PotentialPath> queue = new PathFinder.PotentialList.HOTQueue<PathFinder.PotentialPath>();

		// Token: 0x02001EE1 RID: 7905
		public class PriorityQueue<TValue>
		{
			// Token: 0x06009D37 RID: 40247 RVA: 0x0033BCCD File Offset: 0x00339ECD
			public PriorityQueue()
			{
				this._baseHeap = new List<KeyValuePair<int, TValue>>();
			}

			// Token: 0x06009D38 RID: 40248 RVA: 0x0033BCE0 File Offset: 0x00339EE0
			public void Enqueue(int priority, TValue value)
			{
				this.Insert(priority, value);
			}

			// Token: 0x06009D39 RID: 40249 RVA: 0x0033BCEA File Offset: 0x00339EEA
			public KeyValuePair<int, TValue> Dequeue()
			{
				KeyValuePair<int, TValue> keyValuePair = this._baseHeap[0];
				this.DeleteRoot();
				return keyValuePair;
			}

			// Token: 0x06009D3A RID: 40250 RVA: 0x0033BCFE File Offset: 0x00339EFE
			public KeyValuePair<int, TValue> Peek()
			{
				if (this.Count > 0)
				{
					return this._baseHeap[0];
				}
				throw new InvalidOperationException("Priority queue is empty");
			}

			// Token: 0x06009D3B RID: 40251 RVA: 0x0033BD20 File Offset: 0x00339F20
			private void ExchangeElements(int pos1, int pos2)
			{
				KeyValuePair<int, TValue> keyValuePair = this._baseHeap[pos1];
				this._baseHeap[pos1] = this._baseHeap[pos2];
				this._baseHeap[pos2] = keyValuePair;
			}

			// Token: 0x06009D3C RID: 40252 RVA: 0x0033BD60 File Offset: 0x00339F60
			private void Insert(int priority, TValue value)
			{
				KeyValuePair<int, TValue> keyValuePair = new KeyValuePair<int, TValue>(priority, value);
				this._baseHeap.Add(keyValuePair);
				this.HeapifyFromEndToBeginning(this._baseHeap.Count - 1);
			}

			// Token: 0x06009D3D RID: 40253 RVA: 0x0033BD98 File Offset: 0x00339F98
			private int HeapifyFromEndToBeginning(int pos)
			{
				if (pos >= this._baseHeap.Count)
				{
					return -1;
				}
				while (pos > 0)
				{
					int num = (pos - 1) / 2;
					if (this._baseHeap[num].Key - this._baseHeap[pos].Key <= 0)
					{
						break;
					}
					this.ExchangeElements(num, pos);
					pos = num;
				}
				return pos;
			}

			// Token: 0x06009D3E RID: 40254 RVA: 0x0033BDF8 File Offset: 0x00339FF8
			private void DeleteRoot()
			{
				if (this._baseHeap.Count <= 1)
				{
					this._baseHeap.Clear();
					return;
				}
				this._baseHeap[0] = this._baseHeap[this._baseHeap.Count - 1];
				this._baseHeap.RemoveAt(this._baseHeap.Count - 1);
				this.HeapifyFromBeginningToEnd(0);
			}

			// Token: 0x06009D3F RID: 40255 RVA: 0x0033BE64 File Offset: 0x0033A064
			private void HeapifyFromBeginningToEnd(int pos)
			{
				int count = this._baseHeap.Count;
				if (pos >= count)
				{
					return;
				}
				for (;;)
				{
					int num = pos;
					int num2 = 2 * pos + 1;
					int num3 = 2 * pos + 2;
					if (num2 < count && this._baseHeap[num].Key - this._baseHeap[num2].Key > 0)
					{
						num = num2;
					}
					if (num3 < count && this._baseHeap[num].Key - this._baseHeap[num3].Key > 0)
					{
						num = num3;
					}
					if (num == pos)
					{
						break;
					}
					this.ExchangeElements(num, pos);
					pos = num;
				}
			}

			// Token: 0x06009D40 RID: 40256 RVA: 0x0033BF0C File Offset: 0x0033A10C
			public void Clear()
			{
				this._baseHeap.Clear();
			}

			// Token: 0x170009F7 RID: 2551
			// (get) Token: 0x06009D41 RID: 40257 RVA: 0x0033BF19 File Offset: 0x0033A119
			public int Count
			{
				get
				{
					return this._baseHeap.Count;
				}
			}

			// Token: 0x04008A5D RID: 35421
			private List<KeyValuePair<int, TValue>> _baseHeap;
		}

		// Token: 0x02001EE2 RID: 7906
		private class HOTQueue<TValue>
		{
			// Token: 0x06009D42 RID: 40258 RVA: 0x0033BF28 File Offset: 0x0033A128
			public KeyValuePair<int, TValue> Dequeue()
			{
				if (this.hotQueue.Count == 0)
				{
					PathFinder.PotentialList.PriorityQueue<TValue> priorityQueue = this.hotQueue;
					this.hotQueue = this.coldQueue;
					this.coldQueue = priorityQueue;
					this.hotThreshold = this.coldThreshold;
				}
				this.count--;
				return this.hotQueue.Dequeue();
			}

			// Token: 0x06009D43 RID: 40259 RVA: 0x0033BF84 File Offset: 0x0033A184
			public void Enqueue(int priority, TValue value)
			{
				if (priority <= this.hotThreshold)
				{
					this.hotQueue.Enqueue(priority, value);
				}
				else
				{
					this.coldQueue.Enqueue(priority, value);
					this.coldThreshold = Math.Max(this.coldThreshold, priority);
				}
				this.count++;
			}

			// Token: 0x06009D44 RID: 40260 RVA: 0x0033BFD8 File Offset: 0x0033A1D8
			public KeyValuePair<int, TValue> Peek()
			{
				if (this.hotQueue.Count == 0)
				{
					PathFinder.PotentialList.PriorityQueue<TValue> priorityQueue = this.hotQueue;
					this.hotQueue = this.coldQueue;
					this.coldQueue = priorityQueue;
					this.hotThreshold = this.coldThreshold;
				}
				return this.hotQueue.Peek();
			}

			// Token: 0x06009D45 RID: 40261 RVA: 0x0033C023 File Offset: 0x0033A223
			public void Clear()
			{
				this.count = 0;
				this.hotThreshold = int.MinValue;
				this.hotQueue.Clear();
				this.coldThreshold = int.MinValue;
				this.coldQueue.Clear();
			}

			// Token: 0x170009F8 RID: 2552
			// (get) Token: 0x06009D46 RID: 40262 RVA: 0x0033C058 File Offset: 0x0033A258
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x04008A5E RID: 35422
			private PathFinder.PotentialList.PriorityQueue<TValue> hotQueue = new PathFinder.PotentialList.PriorityQueue<TValue>();

			// Token: 0x04008A5F RID: 35423
			private PathFinder.PotentialList.PriorityQueue<TValue> coldQueue = new PathFinder.PotentialList.PriorityQueue<TValue>();

			// Token: 0x04008A60 RID: 35424
			private int hotThreshold = int.MinValue;

			// Token: 0x04008A61 RID: 35425
			private int coldThreshold = int.MinValue;

			// Token: 0x04008A62 RID: 35426
			private int count;
		}
	}

	// Token: 0x02000FF3 RID: 4083
	private class Temp
	{
		// Token: 0x04005600 RID: 22016
		public static PathFinder.PotentialList Potentials = new PathFinder.PotentialList();
	}

	// Token: 0x02000FF4 RID: 4084
	public class PotentialScratchPad
	{
		// Token: 0x060070FC RID: 28924 RVA: 0x002A837B File Offset: 0x002A657B
		public PotentialScratchPad(int max_links_per_cell)
		{
			this.linksWithCorrectNavType = new NavGrid.Link[max_links_per_cell];
			this.linksInCellRange = new PathFinder.PotentialScratchPad.PathGridCellData[max_links_per_cell];
		}

		// Token: 0x04005601 RID: 22017
		public NavGrid.Link[] linksWithCorrectNavType;

		// Token: 0x04005602 RID: 22018
		public PathFinder.PotentialScratchPad.PathGridCellData[] linksInCellRange;

		// Token: 0x02001EE3 RID: 7907
		public struct PathGridCellData
		{
			// Token: 0x04008A63 RID: 35427
			public PathFinder.Cell pathGridCell;

			// Token: 0x04008A64 RID: 35428
			public NavGrid.Link link;

			// Token: 0x04008A65 RID: 35429
			public bool isSubmerged;
		}
	}
}
