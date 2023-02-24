using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200025B RID: 603
public class GeneShufflerRechargeConfig : IEntityConfig
{
	// Token: 0x06000BF1 RID: 3057 RVA: 0x00042F10 File Offset: 0x00041110
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00042F18 File Offset: 0x00041118
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateLooseEntity("GeneShufflerRecharge", ITEMS.INDUSTRIAL_PRODUCTS.GENE_SHUFFLER_RECHARGE.NAME, ITEMS.INDUSTRIAL_PRODUCTS.GENE_SHUFFLER_RECHARGE.DESC, 5f, true, Assets.GetAnim("vacillator_charge_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.IndustrialIngredient });
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x00042F81 File Offset: 0x00041181
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x00042F83 File Offset: 0x00041183
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006F0 RID: 1776
	public const string ID = "GeneShufflerRecharge";

	// Token: 0x040006F1 RID: 1777
	public static readonly Tag tag = TagManager.Create("GeneShufflerRecharge");

	// Token: 0x040006F2 RID: 1778
	public const float MASS = 5f;
}
