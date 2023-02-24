using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002CB RID: 715
public class PropGravitasJar2Config : IEntityConfig
{
	// Token: 0x06000E2A RID: 3626 RVA: 0x0004D4AE File Offset: 0x0004B6AE
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x0004D4B8 File Offset: 0x0004B6B8
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasJar2";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASJAR2.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASJAR2.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_jar2_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextDimensionalLore));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x0004D55D File Offset: 0x0004B75D
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x0004D574 File Offset: 0x0004B774
	public void OnSpawn(GameObject inst)
	{
	}
}
