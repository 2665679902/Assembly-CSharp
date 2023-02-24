using System;
using System.Collections.Generic;
using ProcGen;
using UnityEngine;

namespace ProcGenGame
{
	// Token: 0x02000C41 RID: 3137
	public class Border : Path
	{
		// Token: 0x06006344 RID: 25412 RVA: 0x0024DFF8 File Offset: 0x0024C1F8
		public Border(Neighbors neighbors, Vector2 e0, Vector2 e1)
		{
			this.neighbors = neighbors;
			Vector2 vector = e1 - e0;
			Vector2 normalized = new Vector2(-vector.y, vector.x).normalized;
			Vector2 vector2 = e0 + vector / 2f + normalized;
			if (neighbors.n0.poly.Contains(vector2))
			{
				base.AddSegment(e0, e1);
				return;
			}
			base.AddSegment(e1, e0);
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x0024E074 File Offset: 0x0024C274
		private void SetCell(int gridCell, float defaultTemperature, TerrainCell.SetValuesFunction SetValues, SeededRandom rnd)
		{
			WeightedSimHash weightedSimHash = WeightedRandom.Choose<WeightedSimHash>(this.element, rnd);
			TerrainCell.ElementOverride elementOverride = TerrainCell.GetElementOverride(weightedSimHash.element, weightedSimHash.overrides);
			if (!elementOverride.overrideTemperature)
			{
				elementOverride.pdelement.temperature = defaultTemperature;
			}
			SetValues(gridCell, elementOverride.element, elementOverride.pdelement, elementOverride.dc);
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x0024E0D0 File Offset: 0x0024C2D0
		public void ConvertToMap(Chunk world, TerrainCell.SetValuesFunction SetValues, float neighbour0Temperature, float neighbour1Temperature, float midTemp, SeededRandom rnd, int snapLastCells)
		{
			for (int i = 0; i < this.pathElements.Count; i++)
			{
				Vector2 vector = this.pathElements[i].e1 - this.pathElements[i].e0;
				Vector2 normalized = new Vector2(-vector.y, vector.x).normalized;
				List<Vector2I> line = ProcGen.Util.GetLine(this.pathElements[i].e0, this.pathElements[i].e1);
				for (int j = 0; j < line.Count; j++)
				{
					int num = Grid.XYToCell(line[j].x, line[j].y);
					if (Grid.IsValidCell(num))
					{
						this.SetCell(num, midTemp, SetValues, rnd);
					}
					for (float num2 = 0.5f; num2 <= this.width; num2 += 1f)
					{
						float num3 = Mathf.Clamp01((num2 - 0.5f) / (this.width - 0.5f));
						if (num2 + (float)snapLastCells > this.width)
						{
							num3 = 1f;
						}
						Vector2 vector2 = line[j] + normalized * num2;
						float num4 = midTemp + (neighbour0Temperature - midTemp) * num3;
						num = Grid.XYToCell((int)vector2.x, (int)vector2.y);
						if (Grid.IsValidCell(num))
						{
							this.SetCell(num, num4, SetValues, rnd);
						}
						Vector2 vector3 = line[j] - normalized * num2;
						float num5 = midTemp + (neighbour1Temperature - midTemp) * num3;
						num = Grid.XYToCell((int)vector3.x, (int)vector3.y);
						if (Grid.IsValidCell(num))
						{
							this.SetCell(num, num5, SetValues, rnd);
						}
					}
				}
			}
		}

		// Token: 0x040044E3 RID: 17635
		public Neighbors neighbors;

		// Token: 0x040044E4 RID: 17636
		public List<WeightedSimHash> element;

		// Token: 0x040044E5 RID: 17637
		public float width;
	}
}
