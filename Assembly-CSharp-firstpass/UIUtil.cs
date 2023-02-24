using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
public static class UIUtil
{
	// Token: 0x0600094A RID: 2378 RVA: 0x00024E6A File Offset: 0x0002306A
	public static float worldHeight(this RectTransform rt)
	{
		rt.GetWorldCorners(UIUtil.corners);
		return UIUtil.corners[2].y - UIUtil.corners[0].y;
	}

	// Token: 0x0400068F RID: 1679
	public static Vector3[] corners = new Vector3[4];
}
