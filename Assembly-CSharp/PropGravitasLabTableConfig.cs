using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class PropGravitasLabTableConfig : IEntityConfig
{
	// Token: 0x06000E2F RID: 3631 RVA: 0x0004D57E File Offset: 0x0004B77E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x0004D588 File Offset: 0x0004B788
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasLabTable";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASLABTABLE.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASLABTABLE.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_lab_table_kanim"), "off", Grid.SceneLayer.Building, 3, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0004D621 File Offset: 0x0004B821
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0004D638 File Offset: 0x0004B838
	public void OnSpawn(GameObject inst)
	{
	}
}
