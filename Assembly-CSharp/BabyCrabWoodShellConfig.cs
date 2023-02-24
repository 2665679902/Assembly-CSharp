using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001C8 RID: 456
public class BabyCrabWoodShellConfig : IEntityConfig
{
	// Token: 0x060008F2 RID: 2290 RVA: 0x00034F92 File Offset: 0x00033192
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00034F9C File Offset: 0x0003319C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BabyCrabWoodShell", ITEMS.INDUSTRIAL_PRODUCTS.BABY_CRAB_SHELL.VARIANT_WOOD.NAME, ITEMS.INDUSTRIAL_PRODUCTS.BABY_CRAB_SHELL.VARIANT_WOOD.DESC, 10f, true, Assets.GetAnim("crabshells_small_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.Organics,
			GameTags.MoltShell
		});
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<SimpleMassStatusItem>().symbolPrefix = "wood_";
		SymbolOverrideControllerUtil.AddToPrefab(gameObject).ApplySymbolOverridesByAffix(Assets.GetAnim("crabshells_small_kanim"), "wood_", null, 0);
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		return gameObject;
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0003505A File Offset: 0x0003325A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0003505C File Offset: 0x0003325C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005A3 RID: 1443
	public const string ID = "BabyCrabWoodShell";

	// Token: 0x040005A4 RID: 1444
	public static readonly Tag TAG = TagManager.Create("BabyCrabWoodShell");

	// Token: 0x040005A5 RID: 1445
	public const float MASS = 10f;

	// Token: 0x040005A6 RID: 1446
	public const string symbolPrefix = "wood_";
}
