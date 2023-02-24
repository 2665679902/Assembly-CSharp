using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class MeatConfig : IEntityConfig
{
	// Token: 0x0600073A RID: 1850 RVA: 0x0002C9C8 File Offset: 0x0002ABC8
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0002C9D0 File Offset: 0x0002ABD0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("Meat", ITEMS.FOOD.MEAT.NAME, ITEMS.FOOD.MEAT.DESC, 1f, false, Assets.GetAnim("creaturemeat_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.MEAT);
		return gameObject;
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0002CA36 File Offset: 0x0002AC36
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0002CA38 File Offset: 0x0002AC38
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004D2 RID: 1234
	public const string ID = "Meat";
}
