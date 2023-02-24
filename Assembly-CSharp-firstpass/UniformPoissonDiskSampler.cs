using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class UniformPoissonDiskSampler
{
	// Token: 0x06000A67 RID: 2663 RVA: 0x00027E46 File Offset: 0x00026046
	public UniformPoissonDiskSampler(SeededRandom seed)
	{
		this.myRandom = seed;
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x00027E55 File Offset: 0x00026055
	public List<Vector2> SampleCircle(Vector2 center, float radius, float minimumDistance)
	{
		return this.SampleCircle(center, radius, minimumDistance, 30);
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x00027E62 File Offset: 0x00026062
	public List<Vector2> SampleCircle(Vector2 center, float radius, float minimumDistance, int pointsPerIteration)
	{
		return this.Sample(center - new Vector2(radius, radius), center + new Vector2(radius, radius), new float?(radius), minimumDistance, pointsPerIteration);
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00027E8D File Offset: 0x0002608D
	public List<Vector2> SampleRectangle(Vector2 topLeft, Vector2 lowerRight, float minimumDistance)
	{
		return this.SampleRectangle(topLeft, lowerRight, minimumDistance, 30);
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00027E9C File Offset: 0x0002609C
	public List<Vector2> SampleRectangle(Vector2 topLeft, Vector2 lowerRight, float minimumDistance, int pointsPerIteration)
	{
		return this.Sample(topLeft, lowerRight, null, minimumDistance, pointsPerIteration);
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00027EC0 File Offset: 0x000260C0
	private List<Vector2> Sample(Vector2 topLeft, Vector2 lowerRight, float? rejectionDistance, float minimumDistance, int pointsPerIteration)
	{
		UniformPoissonDiskSampler.Settings settings = new UniformPoissonDiskSampler.Settings
		{
			TopLeft = topLeft,
			LowerRight = lowerRight,
			Dimensions = lowerRight - topLeft,
			Center = (topLeft + lowerRight) / 2f,
			CellSize = minimumDistance / UniformPoissonDiskSampler.SquareRootTwo,
			MinimumDistance = minimumDistance,
			RejectionSqDistance = ((rejectionDistance == null) ? null : (rejectionDistance * rejectionDistance))
		};
		settings.GridWidth = (int)(settings.Dimensions.x / settings.CellSize) + 1;
		settings.GridHeight = (int)(settings.Dimensions.y / settings.CellSize) + 1;
		UniformPoissonDiskSampler.State state = new UniformPoissonDiskSampler.State
		{
			Grid = new Vector2?[settings.GridWidth, settings.GridHeight],
			ActivePoints = new List<Vector2>(),
			Points = new List<Vector2>()
		};
		this.AddFirstPoint(ref settings, ref state);
		while (state.ActivePoints.Count != 0)
		{
			int num = this.myRandom.RandomRange(0, state.ActivePoints.Count - 1);
			Vector2 vector = state.ActivePoints[num];
			bool flag = false;
			for (int i = 0; i < pointsPerIteration; i++)
			{
				flag |= this.AddNextPoint(vector, ref settings, ref state);
			}
			if (!flag)
			{
				state.ActivePoints.RemoveAt(num);
			}
		}
		return state.Points;
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0002806C File Offset: 0x0002626C
	private void AddFirstPoint(ref UniformPoissonDiskSampler.Settings settings, ref UniformPoissonDiskSampler.State state)
	{
		bool flag = false;
		while (!flag)
		{
			float num = Mathf.Min(settings.Dimensions.x, settings.Dimensions.y) / 2f;
			Vector2 vector = new Vector2(this.myRandom.RandomValue(), this.myRandom.RandomValue());
			Vector2 vector2 = settings.Center + vector * num;
			if (settings.RejectionSqDistance != null)
			{
				float num2 = Vector2.SqrMagnitude(settings.Center - vector2);
				float? rejectionSqDistance = settings.RejectionSqDistance;
				if ((num2 > rejectionSqDistance.GetValueOrDefault()) & (rejectionSqDistance != null))
				{
					continue;
				}
			}
			flag = true;
			Vector2 vector3 = UniformPoissonDiskSampler.Denormalize(vector2, settings.TopLeft, (double)settings.CellSize);
			state.Grid[(int)vector3.x, (int)vector3.y] = new Vector2?(vector2);
			state.ActivePoints.Add(vector2);
			state.Points.Add(vector2);
		}
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x00028164 File Offset: 0x00026364
	private bool AddNextPoint(Vector2 point, ref UniformPoissonDiskSampler.Settings settings, ref UniformPoissonDiskSampler.State state)
	{
		bool flag = false;
		Vector2 vector = this.GenerateRandomAround(point, settings.MinimumDistance);
		if (vector.x >= settings.TopLeft.x && vector.x < settings.LowerRight.x && vector.y > settings.TopLeft.y && vector.y < settings.LowerRight.y)
		{
			if (settings.RejectionSqDistance != null)
			{
				float num = Vector2.SqrMagnitude(settings.Center - vector);
				float? rejectionSqDistance = settings.RejectionSqDistance;
				if (!((num <= rejectionSqDistance.GetValueOrDefault()) & (rejectionSqDistance != null)))
				{
					return flag;
				}
			}
			Vector2 vector2 = UniformPoissonDiskSampler.Denormalize(vector, settings.TopLeft, (double)settings.CellSize);
			bool flag2 = false;
			int num2 = (int)Math.Max(0f, vector2.x - 2f);
			while ((float)num2 < Math.Min((float)settings.GridWidth, vector2.x + 3f) && !flag2)
			{
				int num3 = (int)Math.Max(0f, vector2.y - 2f);
				while ((float)num3 < Math.Min((float)settings.GridHeight, vector2.y + 3f) && !flag2)
				{
					if (state.Grid[num2, num3] != null && Vector2.Distance(state.Grid[num2, num3].Value, vector) < settings.MinimumDistance)
					{
						flag2 = true;
					}
					num3++;
				}
				num2++;
			}
			if (!flag2)
			{
				flag = true;
				state.ActivePoints.Add(vector);
				state.Points.Add(vector);
				state.Grid[(int)vector2.x, (int)vector2.y] = new Vector2?(vector);
			}
		}
		return flag;
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x00028334 File Offset: 0x00026534
	private Vector2 GenerateRandomAround(Vector2 center, float minimumDistance)
	{
		float num = this.myRandom.RandomValue();
		float num2 = minimumDistance + minimumDistance * num;
		num = this.myRandom.RandomValue();
		float num3 = 6.2831855f * num;
		float num4 = num2 * (float)Math.Sin((double)num3);
		float num5 = num2 * (float)Math.Cos((double)num3);
		return new Vector2(center.x + num4, center.y + num5);
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x00028392 File Offset: 0x00026592
	private static Vector2 Denormalize(Vector2 point, Vector2 origin, double cellSize)
	{
		return new Vector2((float)((int)((double)(point.x - origin.x) / cellSize)), (float)((int)((double)(point.y - origin.y) / cellSize)));
	}

	// Token: 0x040006D3 RID: 1747
	public const int DefaultPointsPerIteration = 30;

	// Token: 0x040006D4 RID: 1748
	private static readonly float SquareRootTwo = (float)Math.Sqrt(2.0);

	// Token: 0x040006D5 RID: 1749
	private SeededRandom myRandom;

	// Token: 0x02000A0E RID: 2574
	private struct Settings
	{
		// Token: 0x04002275 RID: 8821
		public Vector2 TopLeft;

		// Token: 0x04002276 RID: 8822
		public Vector2 LowerRight;

		// Token: 0x04002277 RID: 8823
		public Vector2 Center;

		// Token: 0x04002278 RID: 8824
		public Vector2 Dimensions;

		// Token: 0x04002279 RID: 8825
		public float? RejectionSqDistance;

		// Token: 0x0400227A RID: 8826
		public float MinimumDistance;

		// Token: 0x0400227B RID: 8827
		public float CellSize;

		// Token: 0x0400227C RID: 8828
		public int GridWidth;

		// Token: 0x0400227D RID: 8829
		public int GridHeight;
	}

	// Token: 0x02000A0F RID: 2575
	private struct State
	{
		// Token: 0x0400227E RID: 8830
		public Vector2?[,] Grid;

		// Token: 0x0400227F RID: 8831
		public List<Vector2> ActivePoints;

		// Token: 0x04002280 RID: 8832
		public List<Vector2> Points;
	}
}
