using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public class PropGravitasCeilingRobotConfig : IEntityConfig
{
	// Token: 0x06000DFC RID: 3580 RVA: 0x0004CD12 File Offset: 0x0004AF12
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x0004CD1C File Offset: 0x0004AF1C
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasCeilingRobot";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASCEILINGROBOT.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASCEILINGROBOT.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_ceiling_robot_kanim"), "off", Grid.SceneLayer.Building, 2, 4, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x0004CDAF File Offset: 0x0004AFAF
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x0004CDC6 File Offset: 0x0004AFC6
	public void OnSpawn(GameObject inst)
	{
	}
}
