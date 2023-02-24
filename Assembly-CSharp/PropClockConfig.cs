using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B1 RID: 689
public class PropClockConfig : IEntityConfig
{
	// Token: 0x06000DA7 RID: 3495 RVA: 0x0004BBF7 File Offset: 0x00049DF7
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x0004BC00 File Offset: 0x00049E00
	public GameObject CreatePrefab()
	{
		string text = "PropClock";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPCLOCK.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPCLOCK.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("clock_poi_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x0004BCAA File Offset: 0x00049EAA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0004BCAC File Offset: 0x00049EAC
	public void OnSpawn(GameObject inst)
	{
	}
}
