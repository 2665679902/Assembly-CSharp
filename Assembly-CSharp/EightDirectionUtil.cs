using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000735 RID: 1845
[SerializationConfig(MemberSerialization.OptIn)]
public class EightDirectionUtil
{
	// Token: 0x06003281 RID: 12929 RVA: 0x00110CDC File Offset: 0x0010EEDC
	public static int GetDirectionIndex(EightDirection direction)
	{
		return (int)direction;
	}

	// Token: 0x06003282 RID: 12930 RVA: 0x00110CDF File Offset: 0x0010EEDF
	public static EightDirection AngleToDirection(int angle)
	{
		return (EightDirection)Mathf.Floor((float)angle / 45f);
	}

	// Token: 0x06003283 RID: 12931 RVA: 0x00110CEF File Offset: 0x0010EEEF
	public static Vector3 GetNormal(EightDirection direction)
	{
		return EightDirectionUtil.normals[EightDirectionUtil.GetDirectionIndex(direction)];
	}

	// Token: 0x06003284 RID: 12932 RVA: 0x00110D01 File Offset: 0x0010EF01
	public static float GetAngle(EightDirection direction)
	{
		return (float)(45 * EightDirectionUtil.GetDirectionIndex(direction));
	}

	// Token: 0x04001ED0 RID: 7888
	public static readonly Vector3[] normals = new Vector3[]
	{
		Vector3.up,
		(Vector3.up + Vector3.left).normalized,
		Vector3.left,
		(Vector3.down + Vector3.left).normalized,
		Vector3.down,
		(Vector3.down + Vector3.right).normalized,
		Vector3.right,
		(Vector3.up + Vector3.right).normalized
	};
}
