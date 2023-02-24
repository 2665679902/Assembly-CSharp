using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007CD RID: 1997
public class FloodTool : InterfaceTool
{
	// Token: 0x0600392E RID: 14638 RVA: 0x0013CD6C File Offset: 0x0013AF6C
	public HashSet<int> Flood(int startCell)
	{
		HashSet<int> hashSet = new HashSet<int>();
		HashSet<int> hashSet2 = new HashSet<int>();
		GameUtil.FloodFillConditional(startCell, this.floodCriteria, hashSet, hashSet2);
		return hashSet2;
	}

	// Token: 0x0600392F RID: 14639 RVA: 0x0013CD94 File Offset: 0x0013AF94
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
		this.paintArea(this.Flood(Grid.PosToCell(cursor_pos)));
	}

	// Token: 0x06003930 RID: 14640 RVA: 0x0013CDB4 File Offset: 0x0013AFB4
	public override void OnMouseMove(Vector3 cursor_pos)
	{
		base.OnMouseMove(cursor_pos);
		this.mouseCell = Grid.PosToCell(cursor_pos);
	}

	// Token: 0x040025D0 RID: 9680
	public Func<int, bool> floodCriteria;

	// Token: 0x040025D1 RID: 9681
	public Action<HashSet<int>> paintArea;

	// Token: 0x040025D2 RID: 9682
	protected Color32 areaColour = new Color(0.5f, 0.7f, 0.5f, 0.2f);

	// Token: 0x040025D3 RID: 9683
	protected int mouseCell = -1;
}
