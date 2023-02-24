using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class PowerStationToolsConfig : IEntityConfig
{
	// Token: 0x06000922 RID: 2338 RVA: 0x00035512 File Offset: 0x00033712
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0003551C File Offset: 0x0003371C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("PowerStationTools", ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.NAME, ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.DESC, 5f, true, Assets.GetAnim("kit_electrician_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.MiscPickupable });
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0003558C File Offset: 0x0003378C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0003558E File Offset: 0x0003378E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005C0 RID: 1472
	public const string ID = "PowerStationTools";

	// Token: 0x040005C1 RID: 1473
	public static readonly Tag tag = TagManager.Create("PowerStationTools");

	// Token: 0x040005C2 RID: 1474
	public const float MASS = 5f;
}
