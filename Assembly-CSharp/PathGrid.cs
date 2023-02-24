using System;
using System.Collections.Generic;

// Token: 0x020003CA RID: 970
public class PathGrid
{
	// Token: 0x06001419 RID: 5145 RVA: 0x0006A6E2 File Offset: 0x000688E2
	public void SetGroupProber(IGroupProber group_prober)
	{
		this.groupProber = group_prober;
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x0006A6EC File Offset: 0x000688EC
	public PathGrid(int width_in_cells, int height_in_cells, bool apply_offset, NavType[] valid_nav_types)
	{
		this.applyOffset = apply_offset;
		this.widthInCells = width_in_cells;
		this.heightInCells = height_in_cells;
		this.ValidNavTypes = valid_nav_types;
		int num = 0;
		this.NavTypeTable = new int[11];
		for (int i = 0; i < this.NavTypeTable.Length; i++)
		{
			this.NavTypeTable[i] = -1;
			for (int j = 0; j < this.ValidNavTypes.Length; j++)
			{
				if (this.ValidNavTypes[j] == (NavType)i)
				{
					this.NavTypeTable[i] = num++;
					break;
				}
			}
		}
		DebugUtil.DevAssert(true, "Cell packs nav type into 4 bits!", null);
		this.Cells = new PathFinder.Cell[width_in_cells * height_in_cells * this.ValidNavTypes.Length];
		this.ProberCells = new PathGrid.ProberCell[width_in_cells * height_in_cells];
		this.serialNo = 0;
		this.previousSerialNo = -1;
		this.isUpdating = false;
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0006A7C6 File Offset: 0x000689C6
	public void OnCleanUp()
	{
		if (this.groupProber != null)
		{
			this.groupProber.ReleaseProber(this);
		}
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x0006A7DD File Offset: 0x000689DD
	public void ResetUpdate()
	{
		this.previousSerialNo = -1;
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x0006A7E8 File Offset: 0x000689E8
	public void BeginUpdate(int root_cell, bool isContinuation)
	{
		this.isUpdating = true;
		this.freshlyOccupiedCells.Clear();
		if (isContinuation)
		{
			return;
		}
		if (this.applyOffset)
		{
			Grid.CellToXY(root_cell, out this.rootX, out this.rootY);
			this.rootX -= this.widthInCells / 2;
			this.rootY -= this.heightInCells / 2;
		}
		this.serialNo += 1;
		if (this.groupProber != null)
		{
			this.groupProber.SetValidSerialNos(this, this.previousSerialNo, this.serialNo);
		}
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x0006A880 File Offset: 0x00068A80
	public void EndUpdate(bool isComplete)
	{
		this.isUpdating = false;
		if (this.groupProber != null)
		{
			this.groupProber.Occupy(this, this.serialNo, this.freshlyOccupiedCells);
		}
		if (!isComplete)
		{
			return;
		}
		if (this.groupProber != null)
		{
			this.groupProber.SetValidSerialNos(this, this.serialNo, this.serialNo);
		}
		this.previousSerialNo = this.serialNo;
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x0006A8E4 File Offset: 0x00068AE4
	private bool IsValidSerialNo(short serialNo)
	{
		return serialNo == this.serialNo || (!this.isUpdating && this.previousSerialNo != -1 && serialNo == this.previousSerialNo);
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x0006A90D File Offset: 0x00068B0D
	public PathFinder.Cell GetCell(PathFinder.PotentialPath potential_path, out bool is_cell_in_range)
	{
		return this.GetCell(potential_path.cell, potential_path.navType, out is_cell_in_range);
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x0006A924 File Offset: 0x00068B24
	public PathFinder.Cell GetCell(int cell, NavType nav_type, out bool is_cell_in_range)
	{
		int num = this.OffsetCell(cell);
		is_cell_in_range = -1 != num;
		if (!is_cell_in_range)
		{
			return PathGrid.InvalidCell;
		}
		PathFinder.Cell cell2 = this.Cells[num * this.ValidNavTypes.Length + this.NavTypeTable[(int)nav_type]];
		if (!this.IsValidSerialNo(cell2.queryId))
		{
			return PathGrid.InvalidCell;
		}
		return cell2;
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x0006A980 File Offset: 0x00068B80
	public void SetCell(PathFinder.PotentialPath potential_path, ref PathFinder.Cell cell_data)
	{
		int num = this.OffsetCell(potential_path.cell);
		if (-1 == num)
		{
			return;
		}
		cell_data.queryId = this.serialNo;
		int num2 = this.NavTypeTable[(int)potential_path.navType];
		int num3 = num * this.ValidNavTypes.Length + num2;
		this.Cells[num3] = cell_data;
		if (potential_path.navType != NavType.Tube)
		{
			PathGrid.ProberCell proberCell = this.ProberCells[num];
			if (cell_data.queryId != proberCell.queryId || cell_data.cost < proberCell.cost)
			{
				proberCell.queryId = cell_data.queryId;
				proberCell.cost = cell_data.cost;
				this.ProberCells[num] = proberCell;
				this.freshlyOccupiedCells.Add(potential_path.cell);
			}
		}
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x0006AA44 File Offset: 0x00068C44
	public int GetCostIgnoreProberOffset(int cell, CellOffset[] offsets)
	{
		int num = -1;
		foreach (CellOffset cellOffset in offsets)
		{
			int num2 = Grid.OffsetCell(cell, cellOffset);
			if (Grid.IsValidCell(num2))
			{
				PathGrid.ProberCell proberCell = this.ProberCells[num2];
				if (this.IsValidSerialNo(proberCell.queryId) && (num == -1 || proberCell.cost < num))
				{
					num = proberCell.cost;
				}
			}
		}
		return num;
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x0006AAB4 File Offset: 0x00068CB4
	public int GetCost(int cell)
	{
		int num = this.OffsetCell(cell);
		if (-1 == num)
		{
			return -1;
		}
		PathGrid.ProberCell proberCell = this.ProberCells[num];
		if (!this.IsValidSerialNo(proberCell.queryId))
		{
			return -1;
		}
		return proberCell.cost;
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x0006AAF4 File Offset: 0x00068CF4
	private int OffsetCell(int cell)
	{
		if (!this.applyOffset)
		{
			return cell;
		}
		int num;
		int num2;
		Grid.CellToXY(cell, out num, out num2);
		if (num < this.rootX || num >= this.rootX + this.widthInCells || num2 < this.rootY || num2 >= this.rootY + this.heightInCells)
		{
			return -1;
		}
		int num3 = num - this.rootX;
		return (num2 - this.rootY) * this.widthInCells + num3;
	}

	// Token: 0x04000B1C RID: 2844
	private PathFinder.Cell[] Cells;

	// Token: 0x04000B1D RID: 2845
	private PathGrid.ProberCell[] ProberCells;

	// Token: 0x04000B1E RID: 2846
	private List<int> freshlyOccupiedCells = new List<int>();

	// Token: 0x04000B1F RID: 2847
	private NavType[] ValidNavTypes;

	// Token: 0x04000B20 RID: 2848
	private int[] NavTypeTable;

	// Token: 0x04000B21 RID: 2849
	private int widthInCells;

	// Token: 0x04000B22 RID: 2850
	private int heightInCells;

	// Token: 0x04000B23 RID: 2851
	private bool applyOffset;

	// Token: 0x04000B24 RID: 2852
	private int rootX;

	// Token: 0x04000B25 RID: 2853
	private int rootY;

	// Token: 0x04000B26 RID: 2854
	private short serialNo;

	// Token: 0x04000B27 RID: 2855
	private short previousSerialNo;

	// Token: 0x04000B28 RID: 2856
	private bool isUpdating;

	// Token: 0x04000B29 RID: 2857
	private IGroupProber groupProber;

	// Token: 0x04000B2A RID: 2858
	public static readonly PathFinder.Cell InvalidCell = new PathFinder.Cell
	{
		cost = -1
	};

	// Token: 0x02000FF6 RID: 4086
	private struct ProberCell
	{
		// Token: 0x04005604 RID: 22020
		public int cost;

		// Token: 0x04005605 RID: 22021
		public short queryId;
	}
}
