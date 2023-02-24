using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C6 RID: 710
public class PropGravitasDisplay4Config : IEntityConfig
{
	// Token: 0x06000E10 RID: 3600 RVA: 0x0004D032 File Offset: 0x0004B232
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x0004D03C File Offset: 0x0004B23C
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasDisplay4";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDISPLAY4.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDISPLAY4.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_display4_kanim"), "off", Grid.SceneLayer.Building, 1, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextDimensionalLore));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x0004D0E1 File Offset: 0x0004B2E1
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x0004D0F8 File Offset: 0x0004B2F8
	public void OnSpawn(GameObject inst)
	{
	}
}
