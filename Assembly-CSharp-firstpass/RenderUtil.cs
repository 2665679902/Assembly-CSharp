using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public static class RenderUtil
{
	// Token: 0x0600084D RID: 2125 RVA: 0x000217F4 File Offset: 0x0001F9F4
	public static void EnableRenderer(Transform node, bool is_enabled)
	{
		if (node != null)
		{
			Renderer component = node.GetComponent<Renderer>();
			if (component != null)
			{
				component.enabled = is_enabled;
			}
			for (int i = 0; i < node.childCount; i++)
			{
				RenderUtil.EnableRenderer(node.GetChild(i), is_enabled);
			}
		}
	}
}
