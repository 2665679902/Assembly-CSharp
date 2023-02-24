using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003CB RID: 971
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/PathProber")]
public class PathProber : KMonoBehaviour
{
	// Token: 0x06001427 RID: 5159 RVA: 0x0006AB87 File Offset: 0x00068D87
	protected override void OnCleanUp()
	{
		if (this.PathGrid != null)
		{
			this.PathGrid.OnCleanUp();
		}
		base.OnCleanUp();
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x0006ABA2 File Offset: 0x00068DA2
	public void SetGroupProber(IGroupProber group_prober)
	{
		this.PathGrid.SetGroupProber(group_prober);
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x0006ABB0 File Offset: 0x00068DB0
	public void SetValidNavTypes(NavType[] nav_types, int max_probing_radius)
	{
		if (max_probing_radius != 0)
		{
			this.PathGrid = new PathGrid(max_probing_radius * 2, max_probing_radius * 2, true, nav_types);
			return;
		}
		this.PathGrid = new PathGrid(Grid.WidthInCells, Grid.HeightInCells, false, nav_types);
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x0006ABE0 File Offset: 0x00068DE0
	public int GetCost(int cell)
	{
		return this.PathGrid.GetCost(cell);
	}

	// Token: 0x0600142B RID: 5163 RVA: 0x0006ABEE File Offset: 0x00068DEE
	public int GetNavigationCostIgnoreProberOffset(int cell, CellOffset[] offsets)
	{
		return this.PathGrid.GetCostIgnoreProberOffset(cell, offsets);
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x0006ABFD File Offset: 0x00068DFD
	public PathGrid GetPathGrid()
	{
		return this.PathGrid;
	}

	// Token: 0x0600142D RID: 5165 RVA: 0x0006AC08 File Offset: 0x00068E08
	public void UpdateProbe(NavGrid nav_grid, int cell, NavType nav_type, PathFinderAbilities abilities, PathFinder.PotentialPath.Flags flags)
	{
		if (this.scratchPad == null)
		{
			this.scratchPad = new PathFinder.PotentialScratchPad(nav_grid.maxLinksPerCell);
		}
		bool flag = this.updateCount == -1;
		bool flag2 = this.Potentials.Count == 0 || flag;
		this.PathGrid.BeginUpdate(cell, !flag2);
		if (flag2)
		{
			this.updateCount = 0;
			bool flag3;
			PathFinder.Cell cell2 = this.PathGrid.GetCell(cell, nav_type, out flag3);
			PathFinder.AddPotential(new PathFinder.PotentialPath(cell, nav_type, flags), Grid.InvalidCell, NavType.NumNavTypes, 0, 0, this.Potentials, this.PathGrid, ref cell2);
		}
		int num = ((this.potentialCellsPerUpdate <= 0 || flag) ? int.MaxValue : this.potentialCellsPerUpdate);
		this.updateCount++;
		while (this.Potentials.Count > 0 && num > 0)
		{
			KeyValuePair<int, PathFinder.PotentialPath> keyValuePair = this.Potentials.Next();
			num--;
			bool flag3;
			PathFinder.Cell cell3 = this.PathGrid.GetCell(keyValuePair.Value, out flag3);
			if (cell3.cost == keyValuePair.Key)
			{
				PathFinder.AddPotentials(this.scratchPad, keyValuePair.Value, cell3.cost, ref abilities, null, nav_grid.maxLinksPerCell, nav_grid.Links, this.Potentials, this.PathGrid, cell3.parent, cell3.parentNavType);
			}
		}
		bool flag4 = this.Potentials.Count == 0;
		this.PathGrid.EndUpdate(flag4);
		if (flag4)
		{
			int num2 = this.updateCount;
		}
	}

	// Token: 0x04000B2B RID: 2859
	public const int InvalidHandle = -1;

	// Token: 0x04000B2C RID: 2860
	public const int InvalidIdx = -1;

	// Token: 0x04000B2D RID: 2861
	public const int InvalidCell = -1;

	// Token: 0x04000B2E RID: 2862
	public const int InvalidCost = -1;

	// Token: 0x04000B2F RID: 2863
	private PathGrid PathGrid;

	// Token: 0x04000B30 RID: 2864
	private PathFinder.PotentialList Potentials = new PathFinder.PotentialList();

	// Token: 0x04000B31 RID: 2865
	public int updateCount = -1;

	// Token: 0x04000B32 RID: 2866
	private const int updateCountThreshold = 25;

	// Token: 0x04000B33 RID: 2867
	private PathFinder.PotentialScratchPad scratchPad;

	// Token: 0x04000B34 RID: 2868
	public int potentialCellsPerUpdate = -1;
}
