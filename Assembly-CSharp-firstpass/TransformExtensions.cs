using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
public static class TransformExtensions
{
	// Token: 0x06000934 RID: 2356 RVA: 0x00024934 File Offset: 0x00022B34
	public static Vector3 GetPosition(this Transform transform)
	{
		return transform.position;
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0002493C File Offset: 0x00022B3C
	public static Vector3 SetPosition(this Transform transform, Vector3 position)
	{
		transform.position = position;
		if (Singleton<CellChangeMonitor>.Instance != null)
		{
			Singleton<CellChangeMonitor>.Instance.MarkDirty(transform);
		}
		return position;
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00024958 File Offset: 0x00022B58
	public static Vector3 GetLocalPosition(this Transform transform)
	{
		return transform.localPosition;
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00024960 File Offset: 0x00022B60
	public static Vector3 SetLocalPosition(this Transform transform, Vector3 position)
	{
		transform.localPosition = position;
		if (Singleton<CellChangeMonitor>.Instance != null)
		{
			Singleton<CellChangeMonitor>.Instance.MarkDirty(transform);
		}
		return position;
	}
}
