using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008C4 RID: 2244
public static class LightGridManager
{
	// Token: 0x06004095 RID: 16533 RVA: 0x00168ED5 File Offset: 0x001670D5
	private static int CalculateFalloff(float falloffRate, int cell, int origin)
	{
		return Mathf.Max(1, Mathf.RoundToInt(falloffRate * (float)Mathf.Max(Grid.GetCellDistance(origin, cell), 1)));
	}

	// Token: 0x06004096 RID: 16534 RVA: 0x00168EF2 File Offset: 0x001670F2
	public static void Initialise()
	{
		LightGridManager.previewLux = new int[Grid.CellCount];
	}

	// Token: 0x06004097 RID: 16535 RVA: 0x00168F03 File Offset: 0x00167103
	public static void Shutdown()
	{
		LightGridManager.previewLux = null;
		LightGridManager.previewLightCells.Clear();
	}

	// Token: 0x06004098 RID: 16536 RVA: 0x00168F18 File Offset: 0x00167118
	public static void DestroyPreview()
	{
		foreach (global::Tuple<int, int> tuple in LightGridManager.previewLightCells)
		{
			LightGridManager.previewLux[tuple.first] = 0;
		}
		LightGridManager.previewLightCells.Clear();
	}

	// Token: 0x06004099 RID: 16537 RVA: 0x00168F7C File Offset: 0x0016717C
	public static void CreatePreview(int origin_cell, float radius, global::LightShape shape, int lux)
	{
		LightGridManager.previewLightCells.Clear();
		ListPool<int, LightGridManager.LightGridEmitter>.PooledList pooledList = ListPool<int, LightGridManager.LightGridEmitter>.Allocate();
		pooledList.Add(origin_cell);
		DiscreteShadowCaster.GetVisibleCells(origin_cell, pooledList, (int)radius, shape, true);
		foreach (int num in pooledList)
		{
			if (Grid.IsValidCell(num))
			{
				int num2 = lux / LightGridManager.CalculateFalloff(0.5f, num, origin_cell);
				LightGridManager.previewLightCells.Add(new global::Tuple<int, int>(num, num2));
				LightGridManager.previewLux[num] = num2;
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x04002A54 RID: 10836
	public const float DEFAULT_FALLOFF_RATE = 0.5f;

	// Token: 0x04002A55 RID: 10837
	public static List<global::Tuple<int, int>> previewLightCells = new List<global::Tuple<int, int>>();

	// Token: 0x04002A56 RID: 10838
	public static int[] previewLux;

	// Token: 0x02001690 RID: 5776
	public class LightGridEmitter
	{
		// Token: 0x060087F6 RID: 34806 RVA: 0x002F4588 File Offset: 0x002F2788
		public void UpdateLitCells()
		{
			DiscreteShadowCaster.GetVisibleCells(this.state.origin, this.litCells, (int)this.state.radius, this.state.shape, true);
		}

		// Token: 0x060087F7 RID: 34807 RVA: 0x002F45B8 File Offset: 0x002F27B8
		public void AddToGrid(bool update_lit_cells)
		{
			DebugUtil.DevAssert(!update_lit_cells || this.litCells.Count == 0, "adding an already added emitter", null);
			if (update_lit_cells)
			{
				this.UpdateLitCells();
			}
			foreach (int num in this.litCells)
			{
				if (Grid.IsValidCell(num))
				{
					int num2 = Mathf.Max(0, Grid.LightCount[num] + this.ComputeLux(num));
					Grid.LightCount[num] = num2;
					LightGridManager.previewLux[num] = num2;
				}
			}
		}

		// Token: 0x060087F8 RID: 34808 RVA: 0x002F465C File Offset: 0x002F285C
		public void RemoveFromGrid()
		{
			foreach (int num in this.litCells)
			{
				if (Grid.IsValidCell(num))
				{
					Grid.LightCount[num] = Mathf.Max(0, Grid.LightCount[num] - this.ComputeLux(num));
					LightGridManager.previewLux[num] = 0;
				}
			}
			this.litCells.Clear();
		}

		// Token: 0x060087F9 RID: 34809 RVA: 0x002F46E0 File Offset: 0x002F28E0
		public bool Refresh(LightGridManager.LightGridEmitter.State state, bool force = false)
		{
			if (!force && EqualityComparer<LightGridManager.LightGridEmitter.State>.Default.Equals(this.state, state))
			{
				return false;
			}
			this.RemoveFromGrid();
			this.state = state;
			this.AddToGrid(true);
			return true;
		}

		// Token: 0x060087FA RID: 34810 RVA: 0x002F470F File Offset: 0x002F290F
		private int ComputeLux(int cell)
		{
			return this.state.intensity / this.ComputeFalloff(cell);
		}

		// Token: 0x060087FB RID: 34811 RVA: 0x002F4724 File Offset: 0x002F2924
		private int ComputeFalloff(int cell)
		{
			return LightGridManager.CalculateFalloff(this.state.falloffRate, this.state.origin, cell);
		}

		// Token: 0x04006A2E RID: 27182
		private LightGridManager.LightGridEmitter.State state = LightGridManager.LightGridEmitter.State.DEFAULT;

		// Token: 0x04006A2F RID: 27183
		private List<int> litCells = new List<int>();

		// Token: 0x020020A8 RID: 8360
		[Serializable]
		public struct State : IEquatable<LightGridManager.LightGridEmitter.State>
		{
			// Token: 0x0600A499 RID: 42137 RVA: 0x003481F4 File Offset: 0x003463F4
			public bool Equals(LightGridManager.LightGridEmitter.State rhs)
			{
				return this.origin == rhs.origin && this.shape == rhs.shape && this.radius == rhs.radius && this.intensity == rhs.intensity && this.falloffRate == rhs.falloffRate && this.colour == rhs.colour;
			}

			// Token: 0x04009186 RID: 37254
			public int origin;

			// Token: 0x04009187 RID: 37255
			public global::LightShape shape;

			// Token: 0x04009188 RID: 37256
			public float radius;

			// Token: 0x04009189 RID: 37257
			public int intensity;

			// Token: 0x0400918A RID: 37258
			public float falloffRate;

			// Token: 0x0400918B RID: 37259
			public Color colour;

			// Token: 0x0400918C RID: 37260
			public static readonly LightGridManager.LightGridEmitter.State DEFAULT = new LightGridManager.LightGridEmitter.State
			{
				origin = Grid.InvalidCell,
				shape = global::LightShape.Circle,
				radius = 4f,
				intensity = 1,
				falloffRate = 0.5f,
				colour = Color.white
			};
		}
	}
}
