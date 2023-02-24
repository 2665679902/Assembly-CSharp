using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class PropGravitasJar1Config : IEntityConfig
{
	// Token: 0x06000E25 RID: 3621 RVA: 0x0004D3E0 File Offset: 0x0004B5E0
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0004D3E8 File Offset: 0x0004B5E8
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasJar1";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASJAR1.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASJAR1.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_jar1_kanim"), "off", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextDimensionalLore));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x0004D48D File Offset: 0x0004B68D
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0004D4A4 File Offset: 0x0004B6A4
	public void OnSpawn(GameObject inst)
	{
	}
}
