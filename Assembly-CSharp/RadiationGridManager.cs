using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008CB RID: 2251
public static class RadiationGridManager
{
	// Token: 0x060040B6 RID: 16566 RVA: 0x0016A45C File Offset: 0x0016865C
	public static int CalculateFalloff(float falloffRate, int cell, int origin)
	{
		return Mathf.Max(1, Mathf.RoundToInt(falloffRate * (float)Mathf.Max(Grid.GetCellDistance(origin, cell), 1)));
	}

	// Token: 0x060040B7 RID: 16567 RVA: 0x0016A479 File Offset: 0x00168679
	public static void Initialise()
	{
		RadiationGridManager.emitters = new List<RadiationGridEmitter>();
	}

	// Token: 0x060040B8 RID: 16568 RVA: 0x0016A485 File Offset: 0x00168685
	public static void Shutdown()
	{
		RadiationGridManager.emitters.Clear();
	}

	// Token: 0x060040B9 RID: 16569 RVA: 0x0016A494 File Offset: 0x00168694
	public static void Refresh()
	{
		for (int i = 0; i < RadiationGridManager.emitters.Count; i++)
		{
			if (RadiationGridManager.emitters[i].enabled)
			{
				RadiationGridManager.emitters[i].Emit();
			}
		}
	}

	// Token: 0x04002B1D RID: 11037
	public const float STANDARD_MASS_FALLOFF = 1000000f;

	// Token: 0x04002B1E RID: 11038
	public const int RADIATION_LINGER_RATE = 4;

	// Token: 0x04002B1F RID: 11039
	public static List<RadiationGridEmitter> emitters = new List<RadiationGridEmitter>();

	// Token: 0x04002B20 RID: 11040
	public static List<global::Tuple<int, int>> previewLightCells = new List<global::Tuple<int, int>>();

	// Token: 0x04002B21 RID: 11041
	public static int[] previewLux;
}
