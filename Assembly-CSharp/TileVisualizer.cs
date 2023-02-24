using System;
using UnityEngine;

// Token: 0x020004DD RID: 1245
public class TileVisualizer
{
	// Token: 0x06001D82 RID: 7554 RVA: 0x0009D6FC File Offset: 0x0009B8FC
	private static void RefreshCellInternal(int cell, ObjectLayer tile_layer)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		if (!Grid.IsValidCell(cell))
		{
			return;
		}
		GameObject gameObject = Grid.Objects[cell, (int)tile_layer];
		if (gameObject != null)
		{
			World.Instance.blockTileRenderer.Rebuild(tile_layer, cell);
			KAnimGraphTileVisualizer componentInChildren = gameObject.GetComponentInChildren<KAnimGraphTileVisualizer>();
			if (componentInChildren != null)
			{
				componentInChildren.Refresh();
				return;
			}
		}
	}

	// Token: 0x06001D83 RID: 7555 RVA: 0x0009D758 File Offset: 0x0009B958
	private static void RefreshCell(int cell, ObjectLayer tile_layer)
	{
		if (tile_layer == ObjectLayer.NumLayers)
		{
			return;
		}
		TileVisualizer.RefreshCellInternal(cell, tile_layer);
		TileVisualizer.RefreshCellInternal(Grid.CellAbove(cell), tile_layer);
		TileVisualizer.RefreshCellInternal(Grid.CellBelow(cell), tile_layer);
		TileVisualizer.RefreshCellInternal(Grid.CellLeft(cell), tile_layer);
		TileVisualizer.RefreshCellInternal(Grid.CellRight(cell), tile_layer);
	}

	// Token: 0x06001D84 RID: 7556 RVA: 0x0009D797 File Offset: 0x0009B997
	public static void RefreshCell(int cell, ObjectLayer tile_layer, ObjectLayer replacement_layer)
	{
		TileVisualizer.RefreshCell(cell, tile_layer);
		TileVisualizer.RefreshCell(cell, replacement_layer);
	}
}
