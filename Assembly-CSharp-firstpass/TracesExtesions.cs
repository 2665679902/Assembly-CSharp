using System;
using UnityEngine;

// Token: 0x0200010F RID: 271
public static class TracesExtesions
{
	// Token: 0x06000932 RID: 2354 RVA: 0x000248F8 File Offset: 0x00022AF8
	public static void DeleteObject(this GameObject go)
	{
		KMonoBehaviour component = go.GetComponent<KMonoBehaviour>();
		if (component != null)
		{
			component.Trigger(1502190696, go);
		}
		UnityEngine.Object.Destroy(go);
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x00024927 File Offset: 0x00022B27
	public static void DeleteObject(this Component cmp)
	{
		cmp.gameObject.DeleteObject();
	}
}
