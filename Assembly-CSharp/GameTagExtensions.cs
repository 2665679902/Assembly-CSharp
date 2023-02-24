using System;
using UnityEngine;

// Token: 0x02000785 RID: 1925
public static class GameTagExtensions
{
	// Token: 0x06003545 RID: 13637 RVA: 0x0012551B File Offset: 0x0012371B
	public static GameObject Prefab(this Tag tag)
	{
		return Assets.GetPrefab(tag);
	}

	// Token: 0x06003546 RID: 13638 RVA: 0x00125523 File Offset: 0x00123723
	public static string ProperName(this Tag tag)
	{
		return TagManager.GetProperName(tag, false);
	}

	// Token: 0x06003547 RID: 13639 RVA: 0x0012552C File Offset: 0x0012372C
	public static string ProperNameStripLink(this Tag tag)
	{
		return TagManager.GetProperName(tag, true);
	}

	// Token: 0x06003548 RID: 13640 RVA: 0x00125535 File Offset: 0x00123735
	public static Tag Create(SimHashes id)
	{
		return TagManager.Create(id.ToString());
	}

	// Token: 0x06003549 RID: 13641 RVA: 0x00125549 File Offset: 0x00123749
	public static Tag CreateTag(this SimHashes id)
	{
		return TagManager.Create(id.ToString());
	}
}
