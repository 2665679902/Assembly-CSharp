using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000081 RID: 129
public static class AxialUtil
{
	// Token: 0x0600051C RID: 1308 RVA: 0x00018D84 File Offset: 0x00016F84
	public static List<AxialI> GetRing(AxialI center, int radius)
	{
		if (radius < 0)
		{
			global::Debug.LogError(string.Format("Negative radius specified: {0}", radius));
		}
		if (radius == 0)
		{
			return new List<AxialI> { center };
		}
		List<AxialI> list = new List<AxialI>();
		for (int i = 0; i < AxialI.DIRECTIONS.Count; i++)
		{
			AxialI axialI = center + AxialI.DIRECTIONS[i] * radius;
			AxialI axialI2 = AxialI.CLOCKWISE[i];
			for (int j = 0; j < radius; j++)
			{
				list.Add(axialI + axialI2 * j);
			}
		}
		return list;
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00018E20 File Offset: 0x00017020
	public static List<AxialI> GetRings(AxialI center, int minRadius, int maxRadius)
	{
		List<AxialI> list = new List<AxialI>();
		for (int i = minRadius; i <= maxRadius; i++)
		{
			list.AddRange(AxialUtil.GetRing(center, i));
		}
		return list;
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00018E4D File Offset: 0x0001704D
	public static List<AxialI> GetAllPointsWithinRadius(AxialI center, int radius)
	{
		return AxialUtil.GetRings(center, 0, radius);
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00018E58 File Offset: 0x00017058
	public static int GetDistance(AxialI a, AxialI b)
	{
		Vector3I vector3I = a.ToCube();
		Vector3I vector3I2 = b.ToCube();
		return (Math.Abs(vector3I.x - vector3I2.x) + Math.Abs(vector3I.y - vector3I2.y) + Math.Abs(vector3I.z - vector3I2.z)) / 2;
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00018EAF File Offset: 0x000170AF
	public static bool IsAdjacent(this AxialI a, AxialI b)
	{
		return AxialUtil.GetDistance(a, b) == 1;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00018EBB File Offset: 0x000170BB
	public static bool IsWithinRadius(this AxialI a, AxialI center, int radius)
	{
		return AxialUtil.GetDistance(a, center) <= radius;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00018ECA File Offset: 0x000170CA
	public static Vector3 AxialToWorld(float r, float q)
	{
		return new Vector3(Mathf.Sqrt(3f) * r + Mathf.Sqrt(3f) / 2f * q, -1.5f * q, 0f);
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00018EFC File Offset: 0x000170FC
	public static IEnumerable<AxialI> SpiralOut(AxialI startLocation, int maximumRings)
	{
		int num;
		for (int ring = 0; ring < maximumRings; ring = num + 1)
		{
			for (int i = 0; i < AxialI.DIRECTIONS.Count; i = num + 1)
			{
				AxialI vertex = startLocation + AxialI.DIRECTIONS[i] * ring;
				AxialI increment = AxialI.CLOCKWISE[i];
				for (int j = 0; j < ring; j = num + 1)
				{
					yield return vertex + increment * j;
					num = j;
				}
				num = i;
			}
			num = ring;
		}
		yield break;
	}
}
