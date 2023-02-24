using System;

namespace Rendering.World
{
	// Token: 0x02000C50 RID: 3152
	public struct Tile
	{
		// Token: 0x06006433 RID: 25651 RVA: 0x00258969 File Offset: 0x00256B69
		public Tile(int idx, int tile_x, int tile_y, int mask_count)
		{
			this.Idx = idx;
			this.TileCells = new TileCells(tile_x, tile_y);
			this.MaskCount = mask_count;
		}

		// Token: 0x0400457D RID: 17789
		public int Idx;

		// Token: 0x0400457E RID: 17790
		public TileCells TileCells;

		// Token: 0x0400457F RID: 17791
		public int MaskCount;
	}
}
