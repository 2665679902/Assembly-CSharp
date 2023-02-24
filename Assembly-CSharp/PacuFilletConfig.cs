using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017A RID: 378
public class PacuFilletConfig : IEntityConfig
{
	// Token: 0x06000751 RID: 1873 RVA: 0x0002CD78 File Offset: 0x0002AF78
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0002CD80 File Offset: 0x0002AF80
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("PacuFillet", ITEMS.FOOD.MEAT.NAME, ITEMS.FOOD.MEAT.DESC, 1f, false, Assets.GetAnim("pacufillet_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.FISH_MEAT);
		return gameObject;
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0002CDE6 File Offset: 0x0002AFE6
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0002CDE8 File Offset: 0x0002AFE8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004DA RID: 1242
	public const string ID = "PacuFillet";
}
