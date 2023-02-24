using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class PlantMeatConfig : IEntityConfig
{
	// Token: 0x0600075B RID: 1883 RVA: 0x0002CE7D File Offset: 0x0002B07D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0002CE84 File Offset: 0x0002B084
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("PlantMeat", ITEMS.FOOD.PLANTMEAT.NAME, ITEMS.FOOD.PLANTMEAT.DESC, 1f, false, Assets.GetAnim("critter_trap_fruit_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.PLANTMEAT);
		return gameObject;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0002CEEA File Offset: 0x0002B0EA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0002CEEC File Offset: 0x0002B0EC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004DD RID: 1245
	public const string ID = "PlantMeat";
}
