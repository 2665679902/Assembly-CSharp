using System;
using UnityEngine;

// Token: 0x02000491 RID: 1169
public static class KSelectableExtensions
{
	// Token: 0x06001A46 RID: 6726 RVA: 0x0008C10F File Offset: 0x0008A30F
	public static string GetProperName(this Component cmp)
	{
		if (cmp != null && cmp.gameObject != null)
		{
			return cmp.gameObject.GetProperName();
		}
		return "";
	}

	// Token: 0x06001A47 RID: 6727 RVA: 0x0008C13C File Offset: 0x0008A33C
	public static string GetProperName(this GameObject go)
	{
		if (go != null)
		{
			KSelectable component = go.GetComponent<KSelectable>();
			if (component != null)
			{
				return component.GetName();
			}
		}
		return "";
	}

	// Token: 0x06001A48 RID: 6728 RVA: 0x0008C16E File Offset: 0x0008A36E
	public static string GetProperName(this KSelectable cmp)
	{
		if (cmp != null)
		{
			return cmp.GetName();
		}
		return "";
	}
}
