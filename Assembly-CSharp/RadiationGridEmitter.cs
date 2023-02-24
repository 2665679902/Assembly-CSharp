using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008CC RID: 2252
[Serializable]
public class RadiationGridEmitter
{
	// Token: 0x060040BB RID: 16571 RVA: 0x0016A4F0 File Offset: 0x001686F0
	public RadiationGridEmitter(int originCell, int intensity)
	{
		this.originCell = originCell;
		this.intensity = intensity;
	}

	// Token: 0x060040BC RID: 16572 RVA: 0x0016A540 File Offset: 0x00168740
	public void Emit()
	{
		this.scanCells.Clear();
		Vector2 vector = Grid.CellToPosCCC(this.originCell, Grid.SceneLayer.Building);
		for (float num = (float)this.direction - (float)this.angle / 2f; num < (float)this.direction + (float)this.angle / 2f; num += (float)(this.angle / this.projectionCount))
		{
			float num2 = UnityEngine.Random.Range((float)(-(float)this.angle / this.projectionCount) / 2f, (float)(this.angle / this.projectionCount) / 2f);
			Vector2 vector2 = new Vector2(Mathf.Cos((num + num2) * 3.1415927f / 180f), Mathf.Sin((num + num2) * 3.1415927f / 180f));
			int num3 = 3;
			float num4 = (float)(this.intensity / 4);
			Vector2 vector3 = vector2;
			float num5 = 0f;
			while ((double)num4 > 0.01 && num5 < (float)RadiationGridEmitter.MAX_EMIT_DISTANCE)
			{
				num5 += 1f / (float)num3;
				int num6 = Grid.PosToCell(vector + vector3 * num5);
				if (!Grid.IsValidCell(num6))
				{
					break;
				}
				if (!this.scanCells.Contains(num6))
				{
					SimMessages.ModifyRadiationOnCell(num6, (float)Mathf.RoundToInt(num4), -1);
					this.scanCells.Add(num6);
				}
				num4 *= Mathf.Max(0f, 1f - Mathf.Pow(Grid.Mass[num6], 1.25f) * Grid.Element[num6].molarMass / 1000000f);
				num4 *= UnityEngine.Random.Range(0.96f, 0.98f);
			}
		}
	}

	// Token: 0x060040BD RID: 16573 RVA: 0x0016A6F3 File Offset: 0x001688F3
	private int CalculateFalloff(float falloffRate, int cell, int origin)
	{
		return Mathf.Max(1, Mathf.RoundToInt(falloffRate * (float)Mathf.Max(Grid.GetCellDistance(origin, cell), 1)));
	}

	// Token: 0x04002B22 RID: 11042
	private static int MAX_EMIT_DISTANCE = 128;

	// Token: 0x04002B23 RID: 11043
	public int originCell = -1;

	// Token: 0x04002B24 RID: 11044
	public int intensity = 1;

	// Token: 0x04002B25 RID: 11045
	public int projectionCount = 20;

	// Token: 0x04002B26 RID: 11046
	public int direction;

	// Token: 0x04002B27 RID: 11047
	public int angle = 360;

	// Token: 0x04002B28 RID: 11048
	public bool enabled;

	// Token: 0x04002B29 RID: 11049
	private HashSet<int> scanCells = new HashSet<int>();
}
