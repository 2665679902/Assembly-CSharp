using System;
using System.Collections.Generic;

// Token: 0x020003C0 RID: 960
public class NavGridUpdater
{
	// Token: 0x060013E3 RID: 5091 RVA: 0x0006979A File Offset: 0x0006799A
	public static void InitializeNavGrid(NavTable nav_table, NavTableValidator[] validators, CellOffset[] bounding_offsets, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type)
	{
		NavGridUpdater.MarkValidCells(nav_table, validators, bounding_offsets);
		NavGridUpdater.CreateLinks(nav_table, max_links_per_cell, links, transitions_by_nav_type, new Dictionary<int, int>());
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x000697B4 File Offset: 0x000679B4
	public static void UpdateNavGrid(NavTable nav_table, NavTableValidator[] validators, CellOffset[] bounding_offsets, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions, HashSet<int> dirty_nav_cells)
	{
		NavGridUpdater.UpdateValidCells(dirty_nav_cells, nav_table, validators, bounding_offsets);
		NavGridUpdater.UpdateLinks(dirty_nav_cells, nav_table, max_links_per_cell, links, transitions_by_nav_type, teleport_transitions);
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x000697D0 File Offset: 0x000679D0
	private static void UpdateValidCells(HashSet<int> dirty_solid_cells, NavTable nav_table, NavTableValidator[] validators, CellOffset[] bounding_offsets)
	{
		foreach (int num in dirty_solid_cells)
		{
			for (int i = 0; i < validators.Length; i++)
			{
				validators[i].UpdateCell(num, nav_table, bounding_offsets);
			}
		}
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x00069834 File Offset: 0x00067A34
	private static void CreateLinksForCell(int cell, NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
	{
		NavGridUpdater.CreateLinks(cell, nav_table, max_links_per_cell, links, transitions_by_nav_type, teleport_transitions);
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x00069844 File Offset: 0x00067A44
	private static void UpdateLinks(HashSet<int> dirty_nav_cells, NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
	{
		foreach (int num in dirty_nav_cells)
		{
			NavGridUpdater.CreateLinksForCell(num, nav_table, max_links_per_cell, links, transitions_by_nav_type, teleport_transitions);
		}
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x00069898 File Offset: 0x00067A98
	private static void CreateLinks(NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
	{
		WorkItemCollection<NavGridUpdater.CreateLinkWorkItem, object> workItemCollection = new WorkItemCollection<NavGridUpdater.CreateLinkWorkItem, object>();
		workItemCollection.Reset(null);
		for (int i = 0; i < Grid.HeightInCells; i++)
		{
			workItemCollection.Add(new NavGridUpdater.CreateLinkWorkItem(Grid.OffsetCell(0, new CellOffset(0, i)), nav_table, max_links_per_cell, links, transitions_by_nav_type, teleport_transitions));
		}
		GlobalJobManager.Run(workItemCollection);
	}

	// Token: 0x060013E9 RID: 5097 RVA: 0x000698E8 File Offset: 0x00067AE8
	private static void CreateLinks(int cell, NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
	{
		int num = cell * max_links_per_cell;
		int num2 = 0;
		for (int i = 0; i < 11; i++)
		{
			NavType navType = (NavType)i;
			NavGrid.Transition[] array = transitions_by_nav_type[i];
			if (array != null && nav_table.IsValid(cell, navType))
			{
				NavGrid.Transition[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					NavGrid.Transition transition;
					if ((transition = array2[j]).start == NavType.Teleport && teleport_transitions.ContainsKey(cell))
					{
						int num3;
						int num4;
						Grid.CellToXY(cell, out num3, out num4);
						int num5 = teleport_transitions[cell];
						int num6;
						int num7;
						Grid.CellToXY(teleport_transitions[cell], out num6, out num7);
						transition.x = num6 - num3;
						transition.y = num7 - num4;
					}
					int num8 = transition.IsValid(cell, nav_table);
					if (num8 != Grid.InvalidCell)
					{
						links[num] = new NavGrid.Link(num8, transition.start, transition.end, transition.id, transition.cost);
						num++;
						num2++;
					}
				}
			}
		}
		if (num2 >= max_links_per_cell)
		{
			Debug.LogError("Out of nav links. Need to increase maxLinksPerCell:" + max_links_per_cell.ToString());
		}
		links[num].link = Grid.InvalidCell;
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x00069A14 File Offset: 0x00067C14
	private static void MarkValidCells(NavTable nav_table, NavTableValidator[] validators, CellOffset[] bounding_offsets)
	{
		WorkItemCollection<NavGridUpdater.MarkValidCellWorkItem, object> workItemCollection = new WorkItemCollection<NavGridUpdater.MarkValidCellWorkItem, object>();
		workItemCollection.Reset(null);
		for (int i = 0; i < Grid.HeightInCells; i++)
		{
			workItemCollection.Add(new NavGridUpdater.MarkValidCellWorkItem(Grid.OffsetCell(0, new CellOffset(0, i)), nav_table, bounding_offsets, validators));
		}
		GlobalJobManager.Run(workItemCollection);
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x00069A5F File Offset: 0x00067C5F
	public static void DebugDrawPath(int start_cell, int end_cell)
	{
		Grid.CellToPosCCF(start_cell, Grid.SceneLayer.Move);
		Grid.CellToPosCCF(end_cell, Grid.SceneLayer.Move);
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x00069A74 File Offset: 0x00067C74
	public static void DebugDrawPath(PathFinder.Path path)
	{
		if (path.nodes != null)
		{
			for (int i = 0; i < path.nodes.Count - 1; i++)
			{
				NavGridUpdater.DebugDrawPath(path.nodes[i].cell, path.nodes[i + 1].cell);
			}
		}
	}

	// Token: 0x04000AF2 RID: 2802
	public static int InvalidHandle = -1;

	// Token: 0x04000AF3 RID: 2803
	public static int InvalidIdx = -1;

	// Token: 0x04000AF4 RID: 2804
	public static int InvalidCell = -1;

	// Token: 0x02000FED RID: 4077
	private struct CreateLinkWorkItem : IWorkItem<object>
	{
		// Token: 0x060070E3 RID: 28899 RVA: 0x002A812E File Offset: 0x002A632E
		public CreateLinkWorkItem(int start_cell, NavTable nav_table, int max_links_per_cell, NavGrid.Link[] links, NavGrid.Transition[][] transitions_by_nav_type, Dictionary<int, int> teleport_transitions)
		{
			this.startCell = start_cell;
			this.navTable = nav_table;
			this.maxLinksPerCell = max_links_per_cell;
			this.links = links;
			this.transitionsByNavType = transitions_by_nav_type;
			this.teleportTransitions = teleport_transitions;
		}

		// Token: 0x060070E4 RID: 28900 RVA: 0x002A8160 File Offset: 0x002A6360
		public void Run(object shared_data)
		{
			for (int i = 0; i < Grid.WidthInCells; i++)
			{
				NavGridUpdater.CreateLinksForCell(this.startCell + i, this.navTable, this.maxLinksPerCell, this.links, this.transitionsByNavType, this.teleportTransitions);
			}
		}

		// Token: 0x040055EB RID: 21995
		private int startCell;

		// Token: 0x040055EC RID: 21996
		private NavTable navTable;

		// Token: 0x040055ED RID: 21997
		private int maxLinksPerCell;

		// Token: 0x040055EE RID: 21998
		private NavGrid.Link[] links;

		// Token: 0x040055EF RID: 21999
		private NavGrid.Transition[][] transitionsByNavType;

		// Token: 0x040055F0 RID: 22000
		private Dictionary<int, int> teleportTransitions;
	}

	// Token: 0x02000FEE RID: 4078
	private struct MarkValidCellWorkItem : IWorkItem<object>
	{
		// Token: 0x060070E5 RID: 28901 RVA: 0x002A81A8 File Offset: 0x002A63A8
		public MarkValidCellWorkItem(int start_cell, NavTable nav_table, CellOffset[] bounding_offsets, NavTableValidator[] validators)
		{
			this.startCell = start_cell;
			this.navTable = nav_table;
			this.boundingOffsets = bounding_offsets;
			this.validators = validators;
		}

		// Token: 0x060070E6 RID: 28902 RVA: 0x002A81C8 File Offset: 0x002A63C8
		public void Run(object shared_data)
		{
			for (int i = 0; i < Grid.WidthInCells; i++)
			{
				int num = this.startCell + i;
				NavTableValidator[] array = this.validators;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].UpdateCell(num, this.navTable, this.boundingOffsets);
				}
			}
		}

		// Token: 0x040055F1 RID: 22001
		private NavTable navTable;

		// Token: 0x040055F2 RID: 22002
		private CellOffset[] boundingOffsets;

		// Token: 0x040055F3 RID: 22003
		private NavTableValidator[] validators;

		// Token: 0x040055F4 RID: 22004
		private int startCell;
	}
}
