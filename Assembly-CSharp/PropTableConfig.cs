using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class PropTableConfig : IEntityConfig
{
	// Token: 0x06000E74 RID: 3700 RVA: 0x0004E500 File Offset: 0x0004C700
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0004E508 File Offset: 0x0004C708
	public GameObject CreatePrefab()
	{
		string text = "PropTable";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPTABLE.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPTABLE.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("table_breakroom_kanim"), "off", Grid.SceneLayer.Building, 3, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x0004E59B File Offset: 0x0004C79B
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0004E5B2 File Offset: 0x0004C7B2
	public void OnSpawn(GameObject inst)
	{
	}
}
