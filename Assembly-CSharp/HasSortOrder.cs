using System;
using UnityEngine;

// Token: 0x02000AB8 RID: 2744
[AddComponentMenu("KMonoBehaviour/scripts/HasSortOrder")]
public class HasSortOrder : KMonoBehaviour, IHasSortOrder
{
	// Token: 0x17000637 RID: 1591
	// (get) Token: 0x060053F0 RID: 21488 RVA: 0x001E826D File Offset: 0x001E646D
	// (set) Token: 0x060053F1 RID: 21489 RVA: 0x001E8275 File Offset: 0x001E6475
	public int sortOrder { get; set; }
}
