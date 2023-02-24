using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class BasicForagePlantConfig : IEntityConfig
{
	// Token: 0x060005EC RID: 1516 RVA: 0x00026998 File Offset: 0x00024B98
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x000269A0 File Offset: 0x00024BA0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("BasicForagePlant", ITEMS.FOOD.BASICFORAGEPLANT.NAME, ITEMS.FOOD.BASICFORAGEPLANT.DESC, 1f, false, Assets.GetAnim("muckrootvegetable_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.BASICFORAGEPLANT);
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x00026A04 File Offset: 0x00024C04
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00026A06 File Offset: 0x00024C06
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000408 RID: 1032
	public const string ID = "BasicForagePlant";
}
