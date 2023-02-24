using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class FieldRationConfig : IEntityConfig
{
	// Token: 0x06000712 RID: 1810 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0002C5E8 File Offset: 0x0002A7E8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("FieldRation", ITEMS.FOOD.FIELDRATION.NAME, ITEMS.FOOD.FIELDRATION.DESC, 1f, false, Assets.GetAnim("fieldration_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FIELDRATION);
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0002C64C File Offset: 0x0002A84C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0002C64E File Offset: 0x0002A84E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004C5 RID: 1221
	public const string ID = "FieldRation";
}
