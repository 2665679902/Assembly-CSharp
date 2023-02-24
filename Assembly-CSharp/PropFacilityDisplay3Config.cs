using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002BA RID: 698
public class PropFacilityDisplay3Config : IEntityConfig
{
	// Token: 0x06000DD4 RID: 3540 RVA: 0x0004C4BE File Offset: 0x0004A6BE
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x0004C4C8 File Offset: 0x0004A6C8
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityDisplay3";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY3.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY3.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_display3_kanim"), "off", Grid.SceneLayer.Building, 2, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("display_prop3", UI.USERMENUACTIONS.READLORE.SEARCH_DISPLAY));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x0004C578 File Offset: 0x0004A778
	public void OnPrefabInit(GameObject inst)
	{
		OccupyArea component = inst.GetComponent<OccupyArea>();
		component.objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		int num = Grid.PosToCell(inst);
		foreach (CellOffset cellOffset in component.OccupiedCellsOffsets)
		{
			Grid.GravitasFacility[Grid.OffsetCell(num, cellOffset)] = true;
		}
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x0004C5D0 File Offset: 0x0004A7D0
	public void OnSpawn(GameObject inst)
	{
	}
}
