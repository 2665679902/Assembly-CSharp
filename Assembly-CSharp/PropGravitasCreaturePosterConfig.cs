using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C3 RID: 707
public class PropGravitasCreaturePosterConfig : IEntityConfig
{
	// Token: 0x06000E01 RID: 3585 RVA: 0x0004CDD0 File Offset: 0x0004AFD0
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x0004CDD8 File Offset: 0x0004AFD8
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasCreaturePoster";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASCREATUREPOSTER.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASCREATUREPOSTER.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_poster_kanim"), "off", Grid.SceneLayer.Building, 2, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("storytrait_crittermanipulator_workiversary", UI.USERMENUACTIONS.READLORE.SEARCH_PROPGRAVITASCREATUREPOSTER));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x0004CE85 File Offset: 0x0004B085
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x0004CE9C File Offset: 0x0004B09C
	public void OnSpawn(GameObject inst)
	{
	}
}
