using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C4 RID: 708
public class PropGravitasDecorativeWindowConfig : IEntityConfig
{
	// Token: 0x06000E06 RID: 3590 RVA: 0x0004CEA6 File Offset: 0x0004B0A6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x0004CEB0 File Offset: 0x0004B0B0
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasDecorativeWindow";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDECORATIVEWINDOW.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDECORATIVEWINDOW.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER2;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_top_window_kanim"), "on", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Glass, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x0004CF43 File Offset: 0x0004B143
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0004CF5A File Offset: 0x0004B15A
	public void OnSpawn(GameObject inst)
	{
	}
}
