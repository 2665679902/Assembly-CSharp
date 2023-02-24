using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001CD RID: 461
public class FarmStationToolsConfig : IEntityConfig
{
	// Token: 0x06000910 RID: 2320 RVA: 0x00035355 File Offset: 0x00033555
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0003535C File Offset: 0x0003355C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("FarmStationTools", ITEMS.INDUSTRIAL_PRODUCTS.FARM_STATION_TOOLS.NAME, ITEMS.INDUSTRIAL_PRODUCTS.FARM_STATION_TOOLS.DESC, 5f, true, Assets.GetAnim("kit_planttender_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.MiscPickupable });
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x000353CC File Offset: 0x000335CC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x000353CE File Offset: 0x000335CE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005B7 RID: 1463
	public const string ID = "FarmStationTools";

	// Token: 0x040005B8 RID: 1464
	public static readonly Tag tag = TagManager.Create("FarmStationTools");

	// Token: 0x040005B9 RID: 1465
	public const float MASS = 5f;
}
