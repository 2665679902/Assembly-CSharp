using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class PropTallPlantConfig : IEntityConfig
{
	// Token: 0x06000E79 RID: 3705 RVA: 0x0004E5BC File Offset: 0x0004C7BC
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x0004E5C4 File Offset: 0x0004C7C4
	public GameObject CreatePrefab()
	{
		string text = "PropTallPlant";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYTALLPLANT.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYTALLPLANT.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_tall_plant_kanim"), "off", Grid.SceneLayer.Building, 1, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Polypropylene, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x0004E657 File Offset: 0x0004C857
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x0004E66E File Offset: 0x0004C86E
	public void OnSpawn(GameObject inst)
	{
	}
}
