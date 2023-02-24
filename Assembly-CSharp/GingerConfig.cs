using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000149 RID: 329
public class GingerConfig : IEntityConfig
{
	// Token: 0x06000653 RID: 1619 RVA: 0x0002954D File Offset: 0x0002774D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x00029554 File Offset: 0x00027754
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(GingerConfig.ID, ITEMS.INGREDIENTS.GINGER.NAME, ITEMS.INGREDIENTS.GINGER.DESC, 1f, true, Assets.GetAnim("ginger_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.RECTANGLE, 0.45f, 0.4f, true, TUNING.SORTORDER.BUILDINGELEMENTS + GingerConfig.SORTORDER, SimHashes.Creature, new List<Tag> { GameTags.IndustrialIngredient });
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x000295CE File Offset: 0x000277CE
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x000295D0 File Offset: 0x000277D0
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000460 RID: 1120
	public static string ID = "GingerConfig";

	// Token: 0x04000461 RID: 1121
	public static int SORTORDER = 1;
}
