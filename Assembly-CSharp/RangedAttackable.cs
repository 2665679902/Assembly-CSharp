using System;
using UnityEngine;

// Token: 0x020004BC RID: 1212
public class RangedAttackable : AttackableBase
{
	// Token: 0x06001BF9 RID: 7161 RVA: 0x0009495F File Offset: 0x00092B5F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06001BFA RID: 7162 RVA: 0x00094967 File Offset: 0x00092B67
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.preferUnreservedCell = true;
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x00094981 File Offset: 0x00092B81
	public new int GetCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x06001BFC RID: 7164 RVA: 0x0009498C File Offset: 0x00092B8C
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0f, 0.5f, 0.5f, 0.15f);
		foreach (CellOffset cellOffset in base.GetOffsets())
		{
			Gizmos.DrawCube(new Vector3(0.5f, 0.5f, 0f) + Grid.CellToPos(Grid.OffsetCell(Grid.PosToCell(base.gameObject), cellOffset)), Vector3.one);
		}
	}
}
