using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class FriedMushBarConfig : IEntityConfig
{
	// Token: 0x0600071C RID: 1820 RVA: 0x0002C6D2 File Offset: 0x0002A8D2
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0002C6DC File Offset: 0x0002A8DC
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("FriedMushBar", ITEMS.FOOD.FRIEDMUSHBAR.NAME, ITEMS.FOOD.FRIEDMUSHBAR.DESC, 1f, false, Assets.GetAnim("mushbarfried_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FRIEDMUSHBAR);
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0002C740 File Offset: 0x0002A940
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0002C742 File Offset: 0x0002A942
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004C7 RID: 1223
	public const string ID = "FriedMushBar";

	// Token: 0x040004C8 RID: 1224
	public static ComplexRecipe recipe;
}
