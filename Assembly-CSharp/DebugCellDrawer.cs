using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006F9 RID: 1785
[AddComponentMenu("KMonoBehaviour/scripts/DebugCellDrawer")]
public class DebugCellDrawer : KMonoBehaviour
{
	// Token: 0x060030C3 RID: 12483 RVA: 0x00102414 File Offset: 0x00100614
	private void Update()
	{
		for (int i = 0; i < this.cells.Count; i++)
		{
			if (this.cells[i] != PathFinder.InvalidCell)
			{
				DebugExtension.DebugPoint(Grid.CellToPosCCF(this.cells[i], Grid.SceneLayer.Background), 1f, 0f, true);
			}
		}
	}

	// Token: 0x04001D6A RID: 7530
	public List<int> cells;
}
