using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class BabyCrabShellConfig : IEntityConfig
{
	// Token: 0x060008EC RID: 2284 RVA: 0x00034EE2 File Offset: 0x000330E2
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00034EEC File Offset: 0x000330EC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BabyCrabShell", ITEMS.INDUSTRIAL_PRODUCTS.BABY_CRAB_SHELL.NAME, ITEMS.INDUSTRIAL_PRODUCTS.BABY_CRAB_SHELL.DESC, 5f, true, Assets.GetAnim("crabshells_small_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.Organics
		});
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<SimpleMassStatusItem>();
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		return gameObject;
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00034F75 File Offset: 0x00033175
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x00034F77 File Offset: 0x00033177
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005A0 RID: 1440
	public const string ID = "BabyCrabShell";

	// Token: 0x040005A1 RID: 1441
	public static readonly Tag TAG = TagManager.Create("BabyCrabShell");

	// Token: 0x040005A2 RID: 1442
	public const float MASS = 5f;
}
