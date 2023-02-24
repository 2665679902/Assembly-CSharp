using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000175 RID: 373
public class LettuceConfig : IEntityConfig
{
	// Token: 0x06000735 RID: 1845 RVA: 0x0002C950 File Offset: 0x0002AB50
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0002C958 File Offset: 0x0002AB58
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Lettuce", ITEMS.FOOD.LETTUCE.NAME, ITEMS.FOOD.LETTUCE.DESC, 1f, false, Assets.GetAnim("sea_lettuce_leaves_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.LETTUCE);
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0002C9BC File Offset: 0x0002ABBC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0002C9BE File Offset: 0x0002ABBE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004D1 RID: 1233
	public const string ID = "Lettuce";
}
