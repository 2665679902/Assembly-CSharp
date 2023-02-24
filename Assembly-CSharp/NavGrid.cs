using System;
using System.Collections.Generic;
using HUSL;
using UnityEngine;

// Token: 0x020003BF RID: 959
public class NavGrid
{
	// Token: 0x1700007F RID: 127
	// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00068FB5 File Offset: 0x000671B5
	// (set) Token: 0x060013C5 RID: 5061 RVA: 0x00068FBD File Offset: 0x000671BD
	public NavTable NavTable { get; private set; }

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00068FC6 File Offset: 0x000671C6
	// (set) Token: 0x060013C7 RID: 5063 RVA: 0x00068FCE File Offset: 0x000671CE
	public NavGrid.Transition[] transitions { get; set; }

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00068FD7 File Offset: 0x000671D7
	// (set) Token: 0x060013C9 RID: 5065 RVA: 0x00068FDF File Offset: 0x000671DF
	public NavGrid.Transition[][] transitionsByNavType { get; private set; }

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x060013CA RID: 5066 RVA: 0x00068FE8 File Offset: 0x000671E8
	// (set) Token: 0x060013CB RID: 5067 RVA: 0x00068FF0 File Offset: 0x000671F0
	public int updateRangeX { get; private set; }

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x060013CC RID: 5068 RVA: 0x00068FF9 File Offset: 0x000671F9
	// (set) Token: 0x060013CD RID: 5069 RVA: 0x00069001 File Offset: 0x00067201
	public int updateRangeY { get; private set; }

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060013CE RID: 5070 RVA: 0x0006900A File Offset: 0x0006720A
	// (set) Token: 0x060013CF RID: 5071 RVA: 0x00069012 File Offset: 0x00067212
	public int maxLinksPerCell { get; private set; }

	// Token: 0x060013D0 RID: 5072 RVA: 0x0006901B File Offset: 0x0006721B
	public static NavType MirrorNavType(NavType nav_type)
	{
		if (nav_type == NavType.LeftWall)
		{
			return NavType.RightWall;
		}
		if (nav_type == NavType.RightWall)
		{
			return NavType.LeftWall;
		}
		return nav_type;
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x0006902C File Offset: 0x0006722C
	public NavGrid(string id, NavGrid.Transition[] transitions, NavGrid.NavTypeData[] nav_type_data, CellOffset[] bounding_offsets, NavTableValidator[] validators, int update_range_x, int update_range_y, int max_links_per_cell)
	{
		this.id = id;
		this.Validators = validators;
		this.navTypeData = nav_type_data;
		this.transitions = transitions;
		this.boundingOffsets = bounding_offsets;
		List<NavType> list = new List<NavType>();
		this.updateRangeX = update_range_x;
		this.updateRangeY = update_range_y;
		this.maxLinksPerCell = max_links_per_cell + 1;
		for (int i = 0; i < transitions.Length; i++)
		{
			DebugUtil.Assert(i >= 0 && i <= 255);
			transitions[i].id = (byte)i;
			if (!list.Contains(transitions[i].start))
			{
				list.Add(transitions[i].start);
			}
			if (!list.Contains(transitions[i].end))
			{
				list.Add(transitions[i].end);
			}
		}
		this.ValidNavTypes = list.ToArray();
		this.DebugViewLinkType = new bool[this.ValidNavTypes.Length];
		this.DebugViewValidCellsType = new bool[this.ValidNavTypes.Length];
		foreach (NavType navType in this.ValidNavTypes)
		{
			this.GetNavTypeData(navType);
		}
		this.Links = new NavGrid.Link[this.maxLinksPerCell * Grid.CellCount];
		this.NavTable = new NavTable(Grid.CellCount);
		this.transitions = transitions;
		this.transitionsByNavType = new NavGrid.Transition[11][];
		for (int k = 0; k < 11; k++)
		{
			List<NavGrid.Transition> list2 = new List<NavGrid.Transition>();
			NavType navType2 = (NavType)k;
			foreach (NavGrid.Transition transition in transitions)
			{
				if (transition.start == navType2)
				{
					list2.Add(transition);
				}
			}
			this.transitionsByNavType[k] = list2.ToArray();
		}
		foreach (NavTableValidator navTableValidator in validators)
		{
			navTableValidator.onDirty = (Action<int>)Delegate.Combine(navTableValidator.onDirty, new Action<int>(this.AddDirtyCell));
		}
		this.potentialScratchPad = new PathFinder.PotentialScratchPad(this.maxLinksPerCell);
		this.InitializeGraph();
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x0006926C File Offset: 0x0006746C
	public NavGrid.NavTypeData GetNavTypeData(NavType nav_type)
	{
		foreach (NavGrid.NavTypeData navTypeData in this.navTypeData)
		{
			if (navTypeData.navType == nav_type)
			{
				return navTypeData;
			}
		}
		throw new Exception("Missing nav type data for nav type:" + nav_type.ToString());
	}

	// Token: 0x060013D3 RID: 5075 RVA: 0x000692C0 File Offset: 0x000674C0
	public bool HasNavTypeData(NavType nav_type)
	{
		NavGrid.NavTypeData[] array = this.navTypeData;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].navType == nav_type)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x000692F4 File Offset: 0x000674F4
	public HashedString GetIdleAnim(NavType nav_type)
	{
		return this.GetNavTypeData(nav_type).idleAnim;
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x00069302 File Offset: 0x00067502
	public void InitializeGraph()
	{
		NavGridUpdater.InitializeNavGrid(this.NavTable, this.Validators, this.boundingOffsets, this.maxLinksPerCell, this.Links, this.transitionsByNavType);
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x00069330 File Offset: 0x00067530
	public void UpdateGraph()
	{
		foreach (int num in this.DirtyCells)
		{
			for (int i = -this.updateRangeY; i <= this.updateRangeY; i++)
			{
				for (int j = -this.updateRangeX; j <= this.updateRangeX; j++)
				{
					int num2 = Grid.OffsetCell(num, j, i);
					if (Grid.IsValidCell(num2))
					{
						this.ExpandedDirtyCells.Add(num2);
					}
				}
			}
		}
		this.UpdateGraph(this.ExpandedDirtyCells);
		this.DirtyCells = new HashSet<int>();
		this.ExpandedDirtyCells = new HashSet<int>();
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x000693F0 File Offset: 0x000675F0
	public void UpdateGraph(HashSet<int> dirty_nav_cells)
	{
		NavGridUpdater.UpdateNavGrid(this.NavTable, this.Validators, this.boundingOffsets, this.maxLinksPerCell, this.Links, this.transitionsByNavType, this.teleportTransitions, dirty_nav_cells);
		if (this.OnNavGridUpdateComplete != null)
		{
			this.OnNavGridUpdateComplete(dirty_nav_cells);
		}
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x00069441 File Offset: 0x00067641
	public static void DebugDrawPath(int start_cell, int end_cell)
	{
		Grid.CellToPosCCF(start_cell, Grid.SceneLayer.Move);
		Grid.CellToPosCCF(end_cell, Grid.SceneLayer.Move);
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x00069458 File Offset: 0x00067658
	public static void DebugDrawPath(PathFinder.Path path)
	{
		if (path.nodes != null)
		{
			for (int i = 0; i < path.nodes.Count - 1; i++)
			{
				NavGrid.DebugDrawPath(path.nodes[i].cell, path.nodes[i + 1].cell);
			}
		}
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x000694B0 File Offset: 0x000676B0
	private void DebugDrawValidCells()
	{
		Color white = Color.white;
		int cellCount = Grid.CellCount;
		for (int i = 0; i < cellCount; i++)
		{
			for (int j = 0; j < 11; j++)
			{
				NavType navType = (NavType)j;
				if (this.NavTable.IsValid(i, navType) && this.DrawNavTypeCell(navType, ref white))
				{
					DebugExtension.DebugPoint(NavTypeHelper.GetNavPos(i, navType), white, 1f, 0f, false);
				}
			}
		}
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x0006951C File Offset: 0x0006771C
	private void DebugDrawLinks()
	{
		Color white = Color.white;
		for (int i = 0; i < Grid.CellCount; i++)
		{
			int num = i * this.maxLinksPerCell;
			for (int num2 = this.Links[num].link; num2 != NavGrid.InvalidCell; num2 = this.Links[num].link)
			{
				NavTypeHelper.GetNavPos(i, this.Links[num].startNavType);
				if (this.DrawNavTypeLink(this.Links[num].startNavType, ref white) || this.DrawNavTypeLink(this.Links[num].endNavType, ref white))
				{
					NavTypeHelper.GetNavPos(num2, this.Links[num].endNavType);
				}
				num++;
			}
		}
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x000695EC File Offset: 0x000677EC
	private bool DrawNavTypeLink(NavType nav_type, ref Color color)
	{
		color = this.NavTypeColor(nav_type);
		if (this.DebugViewLinksAll)
		{
			return true;
		}
		for (int i = 0; i < this.ValidNavTypes.Length; i++)
		{
			if (this.ValidNavTypes[i] == nav_type)
			{
				return this.DebugViewLinkType[i];
			}
		}
		return false;
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x00069638 File Offset: 0x00067838
	private bool DrawNavTypeCell(NavType nav_type, ref Color color)
	{
		color = this.NavTypeColor(nav_type);
		if (this.DebugViewValidCellsAll)
		{
			return true;
		}
		for (int i = 0; i < this.ValidNavTypes.Length; i++)
		{
			if (this.ValidNavTypes[i] == nav_type)
			{
				return this.DebugViewValidCellsType[i];
			}
		}
		return false;
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x00069684 File Offset: 0x00067884
	public void DebugUpdate()
	{
		if (this.DebugViewValidCells)
		{
			this.DebugDrawValidCells();
		}
		if (this.DebugViewLinks)
		{
			this.DebugDrawLinks();
		}
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x000696A2 File Offset: 0x000678A2
	public void AddDirtyCell(int cell)
	{
		this.DirtyCells.Add(cell);
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x000696B4 File Offset: 0x000678B4
	public void Clear()
	{
		NavTableValidator[] validators = this.Validators;
		for (int i = 0; i < validators.Length; i++)
		{
			validators[i].Clear();
		}
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x000696E0 File Offset: 0x000678E0
	private Color NavTypeColor(NavType navType)
	{
		if (this.debugColorLookup == null)
		{
			this.debugColorLookup = new Color[11];
			for (int i = 0; i < 11; i++)
			{
				double num = (double)i / 11.0;
				IList<double> list = ColorConverter.HUSLToRGB(new double[]
				{
					num * 360.0,
					100.0,
					50.0
				});
				this.debugColorLookup[i] = new Color((float)list[0], (float)list[1], (float)list[2]);
			}
		}
		return this.debugColorLookup[(int)navType];
	}

	// Token: 0x04000AD5 RID: 2773
	public bool DebugViewAllPaths;

	// Token: 0x04000AD6 RID: 2774
	public bool DebugViewValidCells;

	// Token: 0x04000AD7 RID: 2775
	public bool[] DebugViewValidCellsType;

	// Token: 0x04000AD8 RID: 2776
	public bool DebugViewValidCellsAll;

	// Token: 0x04000AD9 RID: 2777
	public bool DebugViewLinks;

	// Token: 0x04000ADA RID: 2778
	public bool[] DebugViewLinkType;

	// Token: 0x04000ADB RID: 2779
	public bool DebugViewLinksAll;

	// Token: 0x04000ADC RID: 2780
	public static int InvalidHandle = -1;

	// Token: 0x04000ADD RID: 2781
	public static int InvalidIdx = -1;

	// Token: 0x04000ADE RID: 2782
	public static int InvalidCell = -1;

	// Token: 0x04000ADF RID: 2783
	public Dictionary<int, int> teleportTransitions = new Dictionary<int, int>();

	// Token: 0x04000AE0 RID: 2784
	public NavGrid.Link[] Links;

	// Token: 0x04000AE2 RID: 2786
	private HashSet<int> DirtyCells = new HashSet<int>();

	// Token: 0x04000AE3 RID: 2787
	private HashSet<int> ExpandedDirtyCells = new HashSet<int>();

	// Token: 0x04000AE4 RID: 2788
	private NavTableValidator[] Validators = new NavTableValidator[0];

	// Token: 0x04000AE5 RID: 2789
	private CellOffset[] boundingOffsets;

	// Token: 0x04000AE6 RID: 2790
	public string id;

	// Token: 0x04000AE7 RID: 2791
	public bool updateEveryFrame;

	// Token: 0x04000AE8 RID: 2792
	public PathFinder.PotentialScratchPad potentialScratchPad;

	// Token: 0x04000AE9 RID: 2793
	public Action<HashSet<int>> OnNavGridUpdateComplete;

	// Token: 0x04000AEC RID: 2796
	public NavType[] ValidNavTypes;

	// Token: 0x04000AED RID: 2797
	public NavGrid.NavTypeData[] navTypeData;

	// Token: 0x04000AF1 RID: 2801
	private Color[] debugColorLookup;

	// Token: 0x02000FEA RID: 4074
	public struct Link
	{
		// Token: 0x060070DF RID: 28895 RVA: 0x002A7A6C File Offset: 0x002A5C6C
		public Link(int link, NavType start_nav_type, NavType end_nav_type, byte transition_id, byte cost)
		{
			this.link = link;
			this.startNavType = start_nav_type;
			this.endNavType = end_nav_type;
			this.transitionId = transition_id;
			this.cost = cost;
		}

		// Token: 0x040055CF RID: 21967
		public int link;

		// Token: 0x040055D0 RID: 21968
		public NavType startNavType;

		// Token: 0x040055D1 RID: 21969
		public NavType endNavType;

		// Token: 0x040055D2 RID: 21970
		public byte transitionId;

		// Token: 0x040055D3 RID: 21971
		public byte cost;
	}

	// Token: 0x02000FEB RID: 4075
	public struct NavTypeData
	{
		// Token: 0x040055D4 RID: 21972
		public NavType navType;

		// Token: 0x040055D5 RID: 21973
		public Vector2 animControllerOffset;

		// Token: 0x040055D6 RID: 21974
		public bool flipX;

		// Token: 0x040055D7 RID: 21975
		public bool flipY;

		// Token: 0x040055D8 RID: 21976
		public float rotation;

		// Token: 0x040055D9 RID: 21977
		public HashedString idleAnim;
	}

	// Token: 0x02000FEC RID: 4076
	public struct Transition
	{
		// Token: 0x060070E0 RID: 28896 RVA: 0x002A7A94 File Offset: 0x002A5C94
		public override string ToString()
		{
			return string.Format("{0}: {1}->{2} ({3}); offset {4},{5}", new object[] { this.id, this.start, this.end, this.startAxis, this.x, this.y });
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x002A7B08 File Offset: 0x002A5D08
		public Transition(NavType start, NavType end, int x, int y, NavAxis start_axis, bool is_looping, bool loop_has_pre, bool is_escape, int cost, string anim, CellOffset[] void_offsets, CellOffset[] solid_offsets, NavOffset[] valid_nav_offsets, NavOffset[] invalid_nav_offsets, bool critter = false, float animSpeed = 1f)
		{
			DebugUtil.Assert(cost <= 255 && cost >= 0);
			this.id = byte.MaxValue;
			this.start = start;
			this.end = end;
			this.x = x;
			this.y = y;
			this.startAxis = start_axis;
			this.isLooping = is_looping;
			this.isEscape = is_escape;
			this.anim = anim;
			this.preAnim = "";
			this.cost = (byte)cost;
			if (string.IsNullOrEmpty(this.anim))
			{
				this.anim = string.Concat(new string[]
				{
					start.ToString().ToLower(),
					"_",
					end.ToString().ToLower(),
					"_",
					x.ToString(),
					"_",
					y.ToString()
				});
			}
			if (this.isLooping)
			{
				if (loop_has_pre)
				{
					this.preAnim = this.anim + "_pre";
				}
				this.anim += "_loop";
			}
			if (this.startAxis != NavAxis.NA)
			{
				this.anim += ((this.startAxis == NavAxis.X) ? "_x" : "_y");
			}
			this.voidOffsets = void_offsets;
			this.solidOffsets = solid_offsets;
			this.validNavOffsets = valid_nav_offsets;
			this.invalidNavOffsets = invalid_nav_offsets;
			this.isCritter = critter;
			this.animSpeed = animSpeed;
		}

		// Token: 0x060070E2 RID: 28898 RVA: 0x002A7C94 File Offset: 0x002A5E94
		public int IsValid(int cell, NavTable nav_table)
		{
			if (!Grid.IsCellOffsetValid(cell, this.x, this.y))
			{
				return Grid.InvalidCell;
			}
			int num = Grid.OffsetCell(cell, this.x, this.y);
			if (!nav_table.IsValid(num, this.end))
			{
				return Grid.InvalidCell;
			}
			Grid.BuildFlags buildFlags = Grid.BuildFlags.Solid | Grid.BuildFlags.DupeImpassable;
			if (this.isCritter)
			{
				buildFlags |= Grid.BuildFlags.CritterImpassable;
			}
			foreach (CellOffset cellOffset in this.voidOffsets)
			{
				int num2 = Grid.OffsetCell(cell, cellOffset.x, cellOffset.y);
				if (Grid.IsValidCell(num2) && (Grid.BuildMasks[num2] & buildFlags) != ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
				{
					if (this.isCritter)
					{
						return Grid.InvalidCell;
					}
					if ((Grid.BuildMasks[num2] & Grid.BuildFlags.DupePassable) == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
					{
						return Grid.InvalidCell;
					}
				}
			}
			foreach (CellOffset cellOffset2 in this.solidOffsets)
			{
				int num3 = Grid.OffsetCell(cell, cellOffset2.x, cellOffset2.y);
				if (Grid.IsValidCell(num3) && !Grid.Solid[num3])
				{
					return Grid.InvalidCell;
				}
			}
			foreach (NavOffset navOffset in this.validNavOffsets)
			{
				int num4 = Grid.OffsetCell(cell, navOffset.offset.x, navOffset.offset.y);
				if (!nav_table.IsValid(num4, navOffset.navType))
				{
					return Grid.InvalidCell;
				}
			}
			foreach (NavOffset navOffset2 in this.invalidNavOffsets)
			{
				int num5 = Grid.OffsetCell(cell, navOffset2.offset.x, navOffset2.offset.y);
				if (nav_table.IsValid(num5, navOffset2.navType))
				{
					return Grid.InvalidCell;
				}
			}
			if (this.start == NavType.Tube)
			{
				if (this.end == NavType.Tube)
				{
					GameObject gameObject = Grid.Objects[cell, 9];
					GameObject gameObject2 = Grid.Objects[num, 9];
					TravelTubeUtilityNetworkLink travelTubeUtilityNetworkLink = (gameObject ? gameObject.GetComponent<TravelTubeUtilityNetworkLink>() : null);
					TravelTubeUtilityNetworkLink travelTubeUtilityNetworkLink2 = (gameObject2 ? gameObject2.GetComponent<TravelTubeUtilityNetworkLink>() : null);
					if (travelTubeUtilityNetworkLink)
					{
						int num6;
						int num7;
						travelTubeUtilityNetworkLink.GetCells(out num6, out num7);
						if (num != num6 && num != num7)
						{
							return Grid.InvalidCell;
						}
						UtilityConnections utilityConnections = UtilityConnectionsExtensions.DirectionFromToCell(cell, num);
						if (utilityConnections == (UtilityConnections)0)
						{
							return Grid.InvalidCell;
						}
						if (Game.Instance.travelTubeSystem.GetConnections(num, false) != utilityConnections)
						{
							return Grid.InvalidCell;
						}
					}
					else if (travelTubeUtilityNetworkLink2)
					{
						int num8;
						int num9;
						travelTubeUtilityNetworkLink2.GetCells(out num8, out num9);
						if (cell != num8 && cell != num9)
						{
							return Grid.InvalidCell;
						}
						UtilityConnections utilityConnections2 = UtilityConnectionsExtensions.DirectionFromToCell(num, cell);
						if (utilityConnections2 == (UtilityConnections)0)
						{
							return Grid.InvalidCell;
						}
						if (Game.Instance.travelTubeSystem.GetConnections(cell, false) != utilityConnections2)
						{
							return Grid.InvalidCell;
						}
					}
					else
					{
						bool flag = this.startAxis == NavAxis.X;
						int num10 = cell;
						for (int j = 0; j < 2; j++)
						{
							if ((flag && j == 0) || (!flag && j == 1))
							{
								int num11 = ((this.x > 0) ? 1 : (-1));
								for (int k = 0; k < Mathf.Abs(this.x); k++)
								{
									UtilityConnections connections = Game.Instance.travelTubeSystem.GetConnections(num10, false);
									if (num11 > 0 && (connections & UtilityConnections.Right) == (UtilityConnections)0)
									{
										return Grid.InvalidCell;
									}
									if (num11 < 0 && (connections & UtilityConnections.Left) == (UtilityConnections)0)
									{
										return Grid.InvalidCell;
									}
									num10 = Grid.OffsetCell(num10, num11, 0);
								}
							}
							else
							{
								int num12 = ((this.y > 0) ? 1 : (-1));
								for (int l = 0; l < Mathf.Abs(this.y); l++)
								{
									UtilityConnections connections2 = Game.Instance.travelTubeSystem.GetConnections(num10, false);
									if (num12 > 0 && (connections2 & UtilityConnections.Up) == (UtilityConnections)0)
									{
										return Grid.InvalidCell;
									}
									if (num12 < 0 && (connections2 & UtilityConnections.Down) == (UtilityConnections)0)
									{
										return Grid.InvalidCell;
									}
									num10 = Grid.OffsetCell(num10, 0, num12);
								}
							}
						}
					}
				}
				else
				{
					UtilityConnections connections3 = Game.Instance.travelTubeSystem.GetConnections(cell, false);
					if (this.y > 0)
					{
						if (connections3 != UtilityConnections.Down)
						{
							return Grid.InvalidCell;
						}
					}
					else if (this.x > 0)
					{
						if (connections3 != UtilityConnections.Left)
						{
							return Grid.InvalidCell;
						}
					}
					else if (this.x < 0)
					{
						if (connections3 != UtilityConnections.Right)
						{
							return Grid.InvalidCell;
						}
					}
					else
					{
						if (this.y >= 0)
						{
							return Grid.InvalidCell;
						}
						if (connections3 != UtilityConnections.Up)
						{
							return Grid.InvalidCell;
						}
					}
				}
			}
			else if (this.start == NavType.Floor && this.end == NavType.Tube)
			{
				int num13 = Grid.OffsetCell(cell, this.x, this.y);
				if (Game.Instance.travelTubeSystem.GetConnections(num13, false) != UtilityConnections.Up)
				{
					return Grid.InvalidCell;
				}
			}
			return num;
		}

		// Token: 0x040055DA RID: 21978
		public NavType start;

		// Token: 0x040055DB RID: 21979
		public NavType end;

		// Token: 0x040055DC RID: 21980
		public NavAxis startAxis;

		// Token: 0x040055DD RID: 21981
		public int x;

		// Token: 0x040055DE RID: 21982
		public int y;

		// Token: 0x040055DF RID: 21983
		public byte id;

		// Token: 0x040055E0 RID: 21984
		public byte cost;

		// Token: 0x040055E1 RID: 21985
		public bool isLooping;

		// Token: 0x040055E2 RID: 21986
		public bool isEscape;

		// Token: 0x040055E3 RID: 21987
		public string preAnim;

		// Token: 0x040055E4 RID: 21988
		public string anim;

		// Token: 0x040055E5 RID: 21989
		public float animSpeed;

		// Token: 0x040055E6 RID: 21990
		public CellOffset[] voidOffsets;

		// Token: 0x040055E7 RID: 21991
		public CellOffset[] solidOffsets;

		// Token: 0x040055E8 RID: 21992
		public NavOffset[] validNavOffsets;

		// Token: 0x040055E9 RID: 21993
		public NavOffset[] invalidNavOffsets;

		// Token: 0x040055EA RID: 21994
		public bool isCritter;
	}
}
