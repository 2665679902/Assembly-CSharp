using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class CrabWoodShellConfig : IEntityConfig
{
	// Token: 0x060008FE RID: 2302 RVA: 0x00035126 File Offset: 0x00033326
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00035130 File Offset: 0x00033330
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("CrabWoodShell", ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.VARIANT_WOOD.NAME, ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.VARIANT_WOOD.DESC, 100f, true, Assets.GetAnim("crabshells_large_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.Organics,
			GameTags.MoltShell
		});
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<SimpleMassStatusItem>().symbolPrefix = "wood_";
		SymbolOverrideControllerUtil.AddToPrefab(gameObject).ApplySymbolOverridesByAffix(Assets.GetAnim("crabshells_large_kanim"), "wood_", null, 0);
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		return gameObject;
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x000351EE File Offset: 0x000333EE
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x000351F0 File Offset: 0x000333F0
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005AA RID: 1450
	public const string ID = "CrabWoodShell";

	// Token: 0x040005AB RID: 1451
	public static readonly Tag TAG = TagManager.Create("CrabWoodShell");

	// Token: 0x040005AC RID: 1452
	public const float MASS = 100f;

	// Token: 0x040005AD RID: 1453
	public const string symbolPrefix = "wood_";
}
