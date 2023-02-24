using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class PropGravitasFloorRobotConfig : IEntityConfig
{
	// Token: 0x06000E1B RID: 3611 RVA: 0x0004D260 File Offset: 0x0004B460
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x0004D268 File Offset: 0x0004B468
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasFloorRobot";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASFLOORROBOT.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPGRAVITASFLOORROBOT.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_floor_robot_kanim"), "off", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x0004D301 File Offset: 0x0004B501
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x0004D318 File Offset: 0x0004B518
	public void OnSpawn(GameObject inst)
	{
	}
}
