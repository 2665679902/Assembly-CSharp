using System;
using UnityEngine;

// Token: 0x020003D2 RID: 978
public class DrawNavGridQuery : PathFinderQuery
{
	// Token: 0x06001443 RID: 5187 RVA: 0x0006B01A File Offset: 0x0006921A
	public DrawNavGridQuery Reset(MinionBrain brain)
	{
		return this;
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x0006B020 File Offset: 0x00069220
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (parent_cell == Grid.InvalidCell || (int)Grid.WorldIdx[parent_cell] != ClusterManager.Instance.activeWorldId || (int)Grid.WorldIdx[cell] != ClusterManager.Instance.activeWorldId)
		{
			return false;
		}
		GL.Color(Color.white);
		GL.Vertex(Grid.CellToPosCCC(parent_cell, Grid.SceneLayer.Move));
		GL.Vertex(Grid.CellToPosCCC(cell, Grid.SceneLayer.Move));
		return false;
	}
}
