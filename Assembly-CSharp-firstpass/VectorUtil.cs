using System;
using UnityEngine;

// Token: 0x02000122 RID: 290
public static class VectorUtil
{
	// Token: 0x06000A25 RID: 2597 RVA: 0x0002704D File Offset: 0x0002524D
	public static bool Less(this Vector2 v, Vector2 other)
	{
		return v.x < other.x && v.y < other.y;
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x0002706D File Offset: 0x0002526D
	public static bool LessEqual(this Vector2 v, Vector2 other)
	{
		return v.x <= other.x && v.y <= other.y;
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x00027090 File Offset: 0x00025290
	public static bool Less(this Vector3 v, Vector3 other)
	{
		return v.x < other.x && v.y < other.y && v.z < other.z;
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x000270BE File Offset: 0x000252BE
	public static bool LessEqual(this Vector3 v, Vector3 other)
	{
		return v.x <= other.x && v.y <= other.y && v.z <= other.z;
	}
}
