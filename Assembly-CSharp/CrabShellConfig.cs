using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public class CrabShellConfig : IEntityConfig
{
	// Token: 0x060008F8 RID: 2296 RVA: 0x00035077 File Offset: 0x00033277
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00035080 File Offset: 0x00033280
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("CrabShell", ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME, ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.DESC, 10f, true, Assets.GetAnim("crabshells_large_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.Organics
		});
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<SimpleMassStatusItem>();
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		return gameObject;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00035109 File Offset: 0x00033309
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0003510B File Offset: 0x0003330B
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005A7 RID: 1447
	public const string ID = "CrabShell";

	// Token: 0x040005A8 RID: 1448
	public static readonly Tag TAG = TagManager.Create("CrabShell");

	// Token: 0x040005A9 RID: 1449
	public const float MASS = 10f;
}
