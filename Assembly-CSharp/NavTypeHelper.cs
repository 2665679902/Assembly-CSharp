using System;
using UnityEngine;

// Token: 0x020003C4 RID: 964
public static class NavTypeHelper
{
	// Token: 0x060013FA RID: 5114 RVA: 0x00069C68 File Offset: 0x00067E68
	public static Vector3 GetNavPos(int cell, NavType nav_type)
	{
		Vector3 zero = Vector3.zero;
		switch (nav_type)
		{
		case NavType.Floor:
			return Grid.CellToPosCBC(cell, Grid.SceneLayer.Move);
		case NavType.LeftWall:
			return Grid.CellToPosLCC(cell, Grid.SceneLayer.Move);
		case NavType.RightWall:
			return Grid.CellToPosRCC(cell, Grid.SceneLayer.Move);
		case NavType.Ceiling:
			return Grid.CellToPosCTC(cell, Grid.SceneLayer.Move);
		case NavType.Ladder:
			return Grid.CellToPosCCC(cell, Grid.SceneLayer.Move);
		case NavType.Pole:
			return Grid.CellToPosCCC(cell, Grid.SceneLayer.Move);
		case NavType.Tube:
			return Grid.CellToPosCCC(cell, Grid.SceneLayer.Move);
		case NavType.Solid:
			return Grid.CellToPosCCC(cell, Grid.SceneLayer.Move);
		}
		return Grid.CellToPosCCC(cell, Grid.SceneLayer.Move);
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x00069D10 File Offset: 0x00067F10
	public static int GetAnchorCell(NavType nav_type, int cell)
	{
		int num = Grid.InvalidCell;
		if (Grid.IsValidCell(cell))
		{
			switch (nav_type)
			{
			case NavType.Floor:
				num = Grid.CellBelow(cell);
				break;
			case NavType.LeftWall:
				num = Grid.CellLeft(cell);
				break;
			case NavType.RightWall:
				num = Grid.CellRight(cell);
				break;
			case NavType.Ceiling:
				num = Grid.CellAbove(cell);
				break;
			default:
				if (nav_type == NavType.Solid)
				{
					num = cell;
				}
				break;
			}
		}
		return num;
	}
}
