using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class ForestTreeBranchConfig : IEntityConfig
{
	// Token: 0x06000639 RID: 1593 RVA: 0x000281AF File Offset: 0x000263AF
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x000281B8 File Offset: 0x000263B8
	public GameObject CreatePrefab()
	{
		string text = "ForestTreeBranch";
		string text2 = STRINGS.CREATURES.SPECIES.WOOD_TREE.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.WOOD_TREE.DESC;
		float num = 8f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("tree_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 1, tier, default(EffectorValues), SimHashes.Creature, new List<Tag>(), 298.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 258.15f, 288.15f, 313.15f, 448.15f, null, true, 0f, 0.15f, "WoodLog", true, true, false, true, 12000f, 0f, 9800f, "ForestTreeBranchOriginal", STRINGS.CREATURES.SPECIES.WOOD_TREE.NAME);
		gameObject.AddOrGet<TreeBud>();
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<BudUprootedMonitor>();
		return gameObject;
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00028282 File Offset: 0x00026482
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x00028284 File Offset: 0x00026484
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400043A RID: 1082
	public const string ID = "ForestTreeBranch";

	// Token: 0x0400043B RID: 1083
	public const float WOOD_AMOUNT = 300f;
}
