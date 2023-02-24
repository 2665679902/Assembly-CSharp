using System;
using System.Collections.Generic;

namespace Rendering.World
{
	// Token: 0x02000C51 RID: 3153
	public abstract class TileRenderer : KMonoBehaviour
	{
		// Token: 0x06006434 RID: 25652 RVA: 0x00258988 File Offset: 0x00256B88
		protected override void OnSpawn()
		{
			this.Masks = this.GetMasks();
			this.TileGridWidth = Grid.WidthInCells + 1;
			this.TileGridHeight = Grid.HeightInCells + 1;
			this.BrushGrid = new int[this.TileGridWidth * this.TileGridHeight * 4];
			for (int i = 0; i < this.BrushGrid.Length; i++)
			{
				this.BrushGrid[i] = -1;
			}
			this.TileGrid = new Tile[this.TileGridWidth * this.TileGridHeight];
			for (int j = 0; j < this.TileGrid.Length; j++)
			{
				int num = j % this.TileGridWidth;
				int num2 = j / this.TileGridWidth;
				this.TileGrid[j] = new Tile(j, num, num2, this.Masks.Length);
			}
			this.LoadBrushes();
			this.VisibleAreaUpdater = new VisibleAreaUpdater(new Action<int>(this.UpdateOutsideView), new Action<int>(this.UpdateInsideView), "TileRenderer");
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x00258A78 File Offset: 0x00256C78
		protected virtual Mask[] GetMasks()
		{
			return new Mask[]
			{
				new Mask(this.Atlas, 0, false, false, false, false),
				new Mask(this.Atlas, 2, false, false, true, false),
				new Mask(this.Atlas, 2, false, true, true, false),
				new Mask(this.Atlas, 1, false, false, true, false),
				new Mask(this.Atlas, 2, false, false, false, false),
				new Mask(this.Atlas, 1, true, false, false, false),
				new Mask(this.Atlas, 3, false, false, false, false),
				new Mask(this.Atlas, 4, false, false, true, false),
				new Mask(this.Atlas, 2, false, true, false, false),
				new Mask(this.Atlas, 3, true, false, false, false),
				new Mask(this.Atlas, 1, true, false, true, false),
				new Mask(this.Atlas, 4, false, true, true, false),
				new Mask(this.Atlas, 1, false, false, false, false),
				new Mask(this.Atlas, 4, false, false, false, false),
				new Mask(this.Atlas, 4, false, true, false, false),
				new Mask(this.Atlas, 0, false, false, false, true)
			};
		}

		// Token: 0x06006436 RID: 25654 RVA: 0x00258C04 File Offset: 0x00256E04
		private void UpdateInsideView(int cell)
		{
			foreach (int num in this.GetCellTiles(cell))
			{
				this.ClearTiles.Add(num);
				this.DirtyTiles.Add(num);
			}
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x00258C48 File Offset: 0x00256E48
		private void UpdateOutsideView(int cell)
		{
			foreach (int num in this.GetCellTiles(cell))
			{
				this.ClearTiles.Add(num);
			}
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x00258C7C File Offset: 0x00256E7C
		private int[] GetCellTiles(int cell)
		{
			int num = 0;
			int num2 = 0;
			Grid.CellToXY(cell, out num, out num2);
			this.CellTiles[0] = num2 * this.TileGridWidth + num;
			this.CellTiles[1] = num2 * this.TileGridWidth + (num + 1);
			this.CellTiles[2] = (num2 + 1) * this.TileGridWidth + num;
			this.CellTiles[3] = (num2 + 1) * this.TileGridWidth + (num + 1);
			return this.CellTiles;
		}

		// Token: 0x06006439 RID: 25657
		public abstract void LoadBrushes();

		// Token: 0x0600643A RID: 25658 RVA: 0x00258CED File Offset: 0x00256EED
		public void MarkDirty(int cell)
		{
			this.VisibleAreaUpdater.UpdateCell(cell);
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x00258CFC File Offset: 0x00256EFC
		private void LateUpdate()
		{
			foreach (int num in this.ClearTiles)
			{
				this.Clear(ref this.TileGrid[num], this.Brushes, this.BrushGrid);
			}
			this.ClearTiles.Clear();
			foreach (int num2 in this.DirtyTiles)
			{
				this.MarkDirty(ref this.TileGrid[num2], this.Brushes, this.BrushGrid);
			}
			this.DirtyTiles.Clear();
			this.VisibleAreaUpdater.Update();
			foreach (Brush brush in this.DirtyBrushes)
			{
				brush.Refresh();
			}
			this.DirtyBrushes.Clear();
			foreach (Brush brush2 in this.ActiveBrushes)
			{
				brush2.Render();
			}
		}

		// Token: 0x0600643C RID: 25660
		public abstract void MarkDirty(ref Tile tile, Brush[] brush_array, int[] brush_grid);

		// Token: 0x0600643D RID: 25661 RVA: 0x00258E6C File Offset: 0x0025706C
		public void Clear(ref Tile tile, Brush[] brush_array, int[] brush_grid)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = tile.Idx * 4 + i;
				if (brush_grid[num] != -1)
				{
					brush_array[brush_grid[num]].Remove(tile.Idx);
				}
			}
		}

		// Token: 0x04004580 RID: 17792
		private Tile[] TileGrid;

		// Token: 0x04004581 RID: 17793
		private int[] BrushGrid;

		// Token: 0x04004582 RID: 17794
		protected int TileGridWidth;

		// Token: 0x04004583 RID: 17795
		protected int TileGridHeight;

		// Token: 0x04004584 RID: 17796
		private int[] CellTiles = new int[4];

		// Token: 0x04004585 RID: 17797
		protected Brush[] Brushes;

		// Token: 0x04004586 RID: 17798
		protected Mask[] Masks;

		// Token: 0x04004587 RID: 17799
		protected List<Brush> DirtyBrushes = new List<Brush>();

		// Token: 0x04004588 RID: 17800
		protected List<Brush> ActiveBrushes = new List<Brush>();

		// Token: 0x04004589 RID: 17801
		private VisibleAreaUpdater VisibleAreaUpdater;

		// Token: 0x0400458A RID: 17802
		private HashSet<int> ClearTiles = new HashSet<int>();

		// Token: 0x0400458B RID: 17803
		private HashSet<int> DirtyTiles = new HashSet<int>();

		// Token: 0x0400458C RID: 17804
		public TextureAtlas Atlas;
	}
}
