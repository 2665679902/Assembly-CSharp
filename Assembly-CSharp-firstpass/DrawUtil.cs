using System;
using UnityEngine;

// Token: 0x02000096 RID: 150
public static class DrawUtil
{
	// Token: 0x060005CA RID: 1482 RVA: 0x0001B180 File Offset: 0x00019380
	public static void MultiColourGnomon(Vector2 pos, float size, float time = 0f)
	{
		size *= 0.5f;
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x0001B18B File Offset: 0x0001938B
	public static void Gnomon(Vector3 pos, float size)
	{
		size *= 0.5f;
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0001B196 File Offset: 0x00019396
	public static void Gnomon(Vector3 pos, float size, Color color, float time = 0f)
	{
		size *= 0.5f;
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0001B1A4 File Offset: 0x000193A4
	public static void Arrow(Vector3 start, Vector3 end, float size, Color color, float time = 0f)
	{
		Vector3 vector = end - start;
		if (vector.sqrMagnitude < 0.001f)
		{
			return;
		}
		Quaternion.LookRotation(vector, Vector3.up);
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0001B1D4 File Offset: 0x000193D4
	public static void Circle(Vector3 pos, float radius)
	{
		DrawUtil.Circle(pos, radius, Color.white, null, 0f);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0001B1FC File Offset: 0x000193FC
	public static void Circle(Vector3 pos, float radius, Color color, Vector3? normal = null, float time = 0f)
	{
		Vector3 vector = normal ?? Vector3.up;
		int num = 40;
		if (DrawUtil.circlePointCache == null)
		{
			float num2 = 6.2831855f / (float)num;
			DrawUtil.circlePointCache = new Vector3[num];
			for (int i = 0; i < num; i++)
			{
				DrawUtil.circlePointCache[i] = new Vector3(Mathf.Cos(num2 * (float)i), Mathf.Sin(num2 * (float)i), 0f);
			}
		}
		Quaternion.FromToRotation(Vector3.forward, vector);
		for (int j = 0; j < num - 1; j++)
		{
		}
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0001B299 File Offset: 0x00019499
	public static void Sphere(Vector3 pos, float radius)
	{
		DrawUtil.Sphere(pos, radius, Color.white, 0f);
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0001B2AC File Offset: 0x000194AC
	public static void Box(Vector3 pos, Color color, float size = 1f, float time = 1f)
	{
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0001B2AE File Offset: 0x000194AE
	public static void Sphere(Vector3 pos, float radius, Color color, float time = 0f)
	{
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0001B2B0 File Offset: 0x000194B0
	public static void Cell(int gridWidth, int cell, Color color, float inset = 0f, float time = 0f)
	{
		DrawUtil.CellXY(cell % gridWidth, cell / gridWidth, color, inset, time);
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0001B2C1 File Offset: 0x000194C1
	public static void CellXY(int x, int y, Color color, float inset = 0f, float time = 0f)
	{
		new Vector2((float)x, (float)y);
	}

	// Token: 0x04000589 RID: 1417
	private static Vector3[] sphere_verts = new Vector3[]
	{
		new Vector3(-1f, 0f, 0f),
		new Vector3(-0.7071f, -0.7071f, 0f),
		new Vector3(0f, -1f, 0f),
		new Vector3(0.7071f, -0.7071f, 0f),
		new Vector3(-0.7071f, 0.7071f, 0f),
		new Vector3(0f, 1f, 0f),
		new Vector3(0.7071f, 0.7071f, 0f),
		new Vector3(-0.7071f, 0f, -0.7071f),
		new Vector3(0f, 0f, -1f),
		new Vector3(0.7071f, 0f, -0.7071f),
		new Vector3(-0.7071f, 0f, 0.7071f),
		new Vector3(0f, 0f, 1f),
		new Vector3(0.7071f, 0f, 0.7071f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, -0.7071f, -0.7071f),
		new Vector3(0f, 0.7071f, -0.7071f),
		new Vector3(0f, 0.7071f, 0.7071f),
		new Vector3(0f, -0.7071f, 0.7071f)
	};

	// Token: 0x0400058A RID: 1418
	private static Vector3[] circlePointCache;
}
