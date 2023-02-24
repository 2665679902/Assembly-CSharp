using System;
using UnityEngine;

// Token: 0x020007F3 RID: 2035
[AddComponentMenu("KMonoBehaviour/scripts/LightShapePreview")]
public class LightShapePreview : KMonoBehaviour
{
	// Token: 0x06003AB0 RID: 15024 RVA: 0x00145028 File Offset: 0x00143228
	private void Update()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (num != this.previousCell)
		{
			this.previousCell = num;
			LightGridManager.DestroyPreview();
			LightGridManager.CreatePreview(Grid.OffsetCell(num, this.offset), this.radius, this.shape, this.lux);
		}
	}

	// Token: 0x06003AB1 RID: 15025 RVA: 0x0014507E File Offset: 0x0014327E
	protected override void OnCleanUp()
	{
		LightGridManager.DestroyPreview();
	}

	// Token: 0x0400267F RID: 9855
	public float radius;

	// Token: 0x04002680 RID: 9856
	public int lux;

	// Token: 0x04002681 RID: 9857
	public global::LightShape shape;

	// Token: 0x04002682 RID: 9858
	public CellOffset offset;

	// Token: 0x04002683 RID: 9859
	private int previousCell = -1;
}
