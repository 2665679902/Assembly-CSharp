using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public class PropGravitasShelfConfig : IEntityConfig
{
	// Token: 0x06000E48 RID: 3656 RVA: 0x0004DA2E File Offset: 0x0004BC2E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x0004DA38 File Offset: 0x0004BC38
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasShelf";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASSHELF.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASSHELF.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_shelf_kanim"), "off", Grid.SceneLayer.Building, 2, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0004DACB File Offset: 0x0004BCCB
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x0004DAE2 File Offset: 0x0004BCE2
	public void OnSpawn(GameObject inst)
	{
	}
}
