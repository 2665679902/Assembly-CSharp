using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001D2 RID: 466
public class WoodLogConfig : IEntityConfig
{
	// Token: 0x0600092E RID: 2350 RVA: 0x00035652 File Offset: 0x00033852
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x0003565C File Offset: 0x0003385C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("WoodLog", ITEMS.INDUSTRIAL_PRODUCTS.WOOD.NAME, ITEMS.INDUSTRIAL_PRODUCTS.WOOD.DESC, 1f, false, Assets.GetAnim("wood_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.Organics,
			GameTags.BuildingWood
		});
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<SimpleMassStatusItem>();
		return gameObject;
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x000356E9 File Offset: 0x000338E9
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x000356EB File Offset: 0x000338EB
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005C6 RID: 1478
	public const string ID = "WoodLog";

	// Token: 0x040005C7 RID: 1479
	public static readonly Tag TAG = TagManager.Create("WoodLog");
}
