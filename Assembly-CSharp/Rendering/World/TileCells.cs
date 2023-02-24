using System;

namespace Rendering.World
{
	// Token: 0x02000C4F RID: 3151
	public struct TileCells
	{
		// Token: 0x06006432 RID: 25650 RVA: 0x002588C8 File Offset: 0x00256AC8
		public TileCells(int tile_x, int tile_y)
		{
			int num = Grid.WidthInCells - 1;
			int num2 = Grid.HeightInCells - 1;
			this.Cell0 = Grid.XYToCell(Math.Min(Math.Max(tile_x - 1, 0), num), Math.Min(Math.Max(tile_y - 1, 0), num2));
			this.Cell1 = Grid.XYToCell(Math.Min(tile_x, num), Math.Min(Math.Max(tile_y - 1, 0), num2));
			this.Cell2 = Grid.XYToCell(Math.Min(Math.Max(tile_x - 1, 0), num), Math.Min(tile_y, num2));
			this.Cell3 = Grid.XYToCell(Math.Min(tile_x, num), Math.Min(tile_y, num2));
		}

		// Token: 0x04004579 RID: 17785
		public int Cell0;

		// Token: 0x0400457A RID: 17786
		public int Cell1;

		// Token: 0x0400457B RID: 17787
		public int Cell2;

		// Token: 0x0400457C RID: 17788
		public int Cell3;
	}
}
