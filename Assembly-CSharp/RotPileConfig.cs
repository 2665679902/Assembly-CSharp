using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class RotPileConfig : IEntityConfig
{
	// Token: 0x0600076C RID: 1900 RVA: 0x0002D11D File Offset: 0x0002B31D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0002D124 File Offset: 0x0002B324
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(RotPileConfig.ID, ITEMS.FOOD.ROTPILE.NAME, ITEMS.FOOD.ROTPILE.DESC, 1f, false, Assets.GetAnim("rotfood_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Organics, false);
		component.AddTag(GameTags.Compostable, false);
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<OccupyArea>();
		gameObject.AddOrGet<Modifiers>();
		gameObject.AddOrGet<RotPile>();
		gameObject.AddComponent<DecorProvider>().SetValues(DECOR.PENALTY.TIER2);
		return gameObject;
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0002D1C7 File Offset: 0x0002B3C7
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<DecorProvider>().overrideName = ITEMS.FOOD.ROTPILE.NAME;
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x0002D1DE File Offset: 0x0002B3DE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004E2 RID: 1250
	public static string ID = "RotPile";
}
