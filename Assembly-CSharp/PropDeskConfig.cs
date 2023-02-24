using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B2 RID: 690
public class PropDeskConfig : IEntityConfig
{
	// Token: 0x06000DAC RID: 3500 RVA: 0x0004BCB6 File Offset: 0x00049EB6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x0004BCC0 File Offset: 0x00049EC0
	public GameObject CreatePrefab()
	{
		string text = "PropDesk";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPDESK.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPDESK.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("setpiece_desk_kanim"), "off", Grid.SceneLayer.Building, 3, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextEmail));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x0004BD65 File Offset: 0x00049F65
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x0004BD7C File Offset: 0x00049F7C
	public void OnSpawn(GameObject inst)
	{
	}
}
