using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200016F RID: 367
public class FishMeatConfig : IEntityConfig
{
	// Token: 0x06000717 RID: 1815 RVA: 0x0002C658 File Offset: 0x0002A858
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0002C660 File Offset: 0x0002A860
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("FishMeat", ITEMS.FOOD.FISHMEAT.NAME, ITEMS.FOOD.FISHMEAT.DESC, 1f, false, Assets.GetAnim("pacufillet_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.FISH_MEAT);
		return gameObject;
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0002C6C6 File Offset: 0x0002A8C6
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x0002C6C8 File Offset: 0x0002A8C8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004C6 RID: 1222
	public const string ID = "FishMeat";
}
