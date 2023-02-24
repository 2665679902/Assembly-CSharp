using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class PropGravitasDeskPodiumConfig : IEntityConfig
{
	// Token: 0x06000E0B RID: 3595 RVA: 0x0004CF64 File Offset: 0x0004B164
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x0004CF6C File Offset: 0x0004B16C
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasDeskPodium";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDESKPODIUM.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDESKPODIUM.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_desk_podium_kanim"), "off", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextDeskPodiumEntry));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x0004D011 File Offset: 0x0004B211
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x0004D028 File Offset: 0x0004B228
	public void OnSpawn(GameObject inst)
	{
	}
}
