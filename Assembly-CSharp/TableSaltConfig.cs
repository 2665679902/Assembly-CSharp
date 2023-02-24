using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200018A RID: 394
public class TableSaltConfig : IEntityConfig
{
	// Token: 0x060007A4 RID: 1956 RVA: 0x0002D6E1 File Offset: 0x0002B8E1
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0002D6E8 File Offset: 0x0002B8E8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(TableSaltConfig.ID, ITEMS.INDUSTRIAL_PRODUCTS.TABLE_SALT.NAME, ITEMS.INDUSTRIAL_PRODUCTS.TABLE_SALT.DESC, 1f, false, Assets.GetAnim("seed_saltPlant_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.45f, true, SORTORDER.BUILDINGELEMENTS + TableSaltTuning.SORTORDER, SimHashes.Salt, new List<Tag> { GameTags.Other });
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0002D762 File Offset: 0x0002B962
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x0002D764 File Offset: 0x0002B964
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004F6 RID: 1270
	public static string ID = "TableSalt";
}
