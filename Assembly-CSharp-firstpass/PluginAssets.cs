using System;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class PluginAssets : MonoBehaviour
{
	// Token: 0x06000842 RID: 2114 RVA: 0x00021635 File Offset: 0x0001F835
	private void Awake()
	{
		PluginAssets.Instance = this;
	}

	// Token: 0x04000633 RID: 1587
	public static PluginAssets Instance;

	// Token: 0x04000634 RID: 1588
	public TextStyleSetting defaultTextStyleSetting;
}
