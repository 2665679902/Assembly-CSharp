using System;
using UnityEngine.UI;

// Token: 0x02000B3E RID: 2878
public class NonDrawingGraphic : Graphic
{
	// Token: 0x0600591C RID: 22812 RVA: 0x00204499 File Offset: 0x00202699
	public override void SetMaterialDirty()
	{
	}

	// Token: 0x0600591D RID: 22813 RVA: 0x0020449B File Offset: 0x0020269B
	public override void SetVerticesDirty()
	{
	}

	// Token: 0x0600591E RID: 22814 RVA: 0x0020449D File Offset: 0x0020269D
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}
}
