using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B8 RID: 696
public class PropFacilityDeskConfig : IEntityConfig
{
	// Token: 0x06000DCA RID: 3530 RVA: 0x0004C286 File Offset: 0x0004A486
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x0004C290 File Offset: 0x0004A490
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityDesk";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYDESK.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYDESK.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_desk_kanim"), "off", Grid.SceneLayer.Building, 4, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("journal_magazine", UI.USERMENUACTIONS.READLORE.SEARCH_STERNSDESK));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x0004C340 File Offset: 0x0004A540
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

	// Token: 0x06000DCD RID: 3533 RVA: 0x0004C398 File Offset: 0x0004A598
	public void OnSpawn(GameObject inst)
	{
	}
}
