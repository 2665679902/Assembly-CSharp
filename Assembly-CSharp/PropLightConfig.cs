using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class PropLightConfig : IEntityConfig
{
	// Token: 0x06000E52 RID: 3666 RVA: 0x0004DBFD File Offset: 0x0004BDFD
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x0004DC04 File Offset: 0x0004BE04
	public GameObject CreatePrefab()
	{
		string text = "PropLight";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPLIGHT.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPLIGHT.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("setpiece_light_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x0004DCAE File Offset: 0x0004BEAE
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x0004DCB0 File Offset: 0x0004BEB0
	public void OnSpawn(GameObject inst)
	{
	}
}
