using System;
using UnityEngine;

// Token: 0x020007ED RID: 2029
public static class KPrefabIDExtensions
{
	// Token: 0x06003A7B RID: 14971 RVA: 0x0014443E File Offset: 0x0014263E
	public static Tag PrefabID(this Component cmp)
	{
		return cmp.GetComponent<KPrefabID>().PrefabID();
	}

	// Token: 0x06003A7C RID: 14972 RVA: 0x0014444B File Offset: 0x0014264B
	public static Tag PrefabID(this GameObject go)
	{
		return go.GetComponent<KPrefabID>().PrefabID();
	}

	// Token: 0x06003A7D RID: 14973 RVA: 0x00144458 File Offset: 0x00142658
	public static Tag PrefabID(this StateMachine.Instance smi)
	{
		return smi.GetComponent<KPrefabID>().PrefabID();
	}

	// Token: 0x06003A7E RID: 14974 RVA: 0x00144465 File Offset: 0x00142665
	public static bool IsPrefabID(this Component cmp, Tag id)
	{
		return cmp.GetComponent<KPrefabID>().IsPrefabID(id);
	}

	// Token: 0x06003A7F RID: 14975 RVA: 0x00144473 File Offset: 0x00142673
	public static bool IsPrefabID(this GameObject go, Tag id)
	{
		return go.GetComponent<KPrefabID>().IsPrefabID(id);
	}

	// Token: 0x06003A80 RID: 14976 RVA: 0x00144481 File Offset: 0x00142681
	public static bool HasTag(this Component cmp, Tag tag)
	{
		return cmp.GetComponent<KPrefabID>().HasTag(tag);
	}

	// Token: 0x06003A81 RID: 14977 RVA: 0x0014448F File Offset: 0x0014268F
	public static bool HasTag(this GameObject go, Tag tag)
	{
		return go.GetComponent<KPrefabID>().HasTag(tag);
	}

	// Token: 0x06003A82 RID: 14978 RVA: 0x0014449D File Offset: 0x0014269D
	public static bool HasAnyTags(this Component cmp, Tag[] tags)
	{
		return cmp.GetComponent<KPrefabID>().HasAnyTags(tags);
	}

	// Token: 0x06003A83 RID: 14979 RVA: 0x001444AB File Offset: 0x001426AB
	public static bool HasAnyTags(this GameObject go, Tag[] tags)
	{
		return go.GetComponent<KPrefabID>().HasAnyTags(tags);
	}

	// Token: 0x06003A84 RID: 14980 RVA: 0x001444B9 File Offset: 0x001426B9
	public static bool HasAllTags(this Component cmp, Tag[] tags)
	{
		return cmp.GetComponent<KPrefabID>().HasAllTags(tags);
	}

	// Token: 0x06003A85 RID: 14981 RVA: 0x001444C7 File Offset: 0x001426C7
	public static bool HasAllTags(this GameObject go, Tag[] tags)
	{
		return go.GetComponent<KPrefabID>().HasAllTags(tags);
	}

	// Token: 0x06003A86 RID: 14982 RVA: 0x001444D5 File Offset: 0x001426D5
	public static void AddTag(this GameObject go, Tag tag)
	{
		go.GetComponent<KPrefabID>().AddTag(tag, false);
	}

	// Token: 0x06003A87 RID: 14983 RVA: 0x001444E4 File Offset: 0x001426E4
	public static void AddTag(this Component cmp, Tag tag)
	{
		cmp.GetComponent<KPrefabID>().AddTag(tag, false);
	}

	// Token: 0x06003A88 RID: 14984 RVA: 0x001444F3 File Offset: 0x001426F3
	public static void RemoveTag(this GameObject go, Tag tag)
	{
		go.GetComponent<KPrefabID>().RemoveTag(tag);
	}

	// Token: 0x06003A89 RID: 14985 RVA: 0x00144501 File Offset: 0x00142701
	public static void RemoveTag(this Component cmp, Tag tag)
	{
		cmp.GetComponent<KPrefabID>().RemoveTag(tag);
	}
}
