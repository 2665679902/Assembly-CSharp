using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000188 RID: 392
public class SwampLilyFlowerConfig : IEntityConfig
{
	// Token: 0x0600079C RID: 1948 RVA: 0x0002D610 File Offset: 0x0002B810
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0002D618 File Offset: 0x0002B818
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(SwampLilyFlowerConfig.ID, ITEMS.INGREDIENTS.SWAMPLILYFLOWER.NAME, ITEMS.INGREDIENTS.SWAMPLILYFLOWER.DESC, 1f, false, Assets.GetAnim("swamplilyflower_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.IndustrialIngredient });
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0002D68F File Offset: 0x0002B88F
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x0002D691 File Offset: 0x0002B891
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004F0 RID: 1264
	public static float SEEDS_PER_FRUIT = 1f;

	// Token: 0x040004F1 RID: 1265
	public static string ID = "SwampLilyFlower";
}
