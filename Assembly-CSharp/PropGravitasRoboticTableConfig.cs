using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class PropGravitasRoboticTableConfig : IEntityConfig
{
	// Token: 0x06000E43 RID: 3651 RVA: 0x0004D96D File Offset: 0x0004BB6D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x0004D974 File Offset: 0x0004BB74
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasRobitcTable";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASROBTICTABLE.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASROBTICTABLE.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_robotic_table_kanim"), "off", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x0004DA0D File Offset: 0x0004BC0D
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x0004DA24 File Offset: 0x0004BC24
	public void OnSpawn(GameObject inst)
	{
	}
}
