using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002BB RID: 699
public class PropFacilityDisplay : IEntityConfig
{
	// Token: 0x06000DD9 RID: 3545 RVA: 0x0004C5DA File Offset: 0x0004A7DA
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0004C5E4 File Offset: 0x0004A7E4
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityDisplay";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY1.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY1.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_display1_kanim"), "off", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("display_prop1", UI.USERMENUACTIONS.READLORE.SEARCH_DISPLAY));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0004C694 File Offset: 0x0004A894
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

	// Token: 0x06000DDC RID: 3548 RVA: 0x0004C6EC File Offset: 0x0004A8EC
	public void OnSpawn(GameObject inst)
	{
	}
}
