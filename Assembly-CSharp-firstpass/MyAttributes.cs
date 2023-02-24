using System;
using System.Collections.Generic;

// Token: 0x020000DC RID: 220
public static class MyAttributes
{
	// Token: 0x0600082A RID: 2090 RVA: 0x000212DE File Offset: 0x0001F4DE
	public static void Register(IAttributeManager mgr)
	{
		MyAttributes.s_attributeMgrs.Add(mgr);
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x000212EC File Offset: 0x0001F4EC
	public static void OnAwake(object obj, KMonoBehaviour cmp)
	{
		foreach (IAttributeManager attributeManager in MyAttributes.s_attributeMgrs)
		{
			attributeManager.OnAwake(obj, cmp);
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x00021340 File Offset: 0x0001F540
	public static void OnAwake(KMonoBehaviour c)
	{
		MyAttributes.OnAwake(c, c);
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0002134C File Offset: 0x0001F54C
	public static void OnStart(object obj, KMonoBehaviour cmp)
	{
		foreach (IAttributeManager attributeManager in MyAttributes.s_attributeMgrs)
		{
			attributeManager.OnStart(obj, cmp);
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000213A0 File Offset: 0x0001F5A0
	public static void OnStart(KMonoBehaviour c)
	{
		MyAttributes.OnStart(c, c);
	}

	// Token: 0x0400062A RID: 1578
	private static List<IAttributeManager> s_attributeMgrs = new List<IAttributeManager>();
}
