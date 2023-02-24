using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000289 RID: 649
public class AlgaeConfig : IOreConfig
{
	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0004853B File Offset: 0x0004673B
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.Algae;
		}
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x00048542 File Offset: 0x00046742
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x00048549 File Offset: 0x00046749
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateSolidOreEntity(this.ElementID, new List<Tag> { GameTags.Life });
	}
}
