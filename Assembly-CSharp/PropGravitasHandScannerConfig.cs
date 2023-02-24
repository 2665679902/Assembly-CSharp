using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public class PropGravitasHandScannerConfig : IEntityConfig
{
	// Token: 0x06000E20 RID: 3616 RVA: 0x0004D322 File Offset: 0x0004B522
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0004D32C File Offset: 0x0004B52C
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasHandScanner";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASHANDSCANNER.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASHANDSCANNER.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_hand_scanner_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x0004D3BF File Offset: 0x0004B5BF
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x0004D3D6 File Offset: 0x0004B5D6
	public void OnSpawn(GameObject inst)
	{
	}
}
