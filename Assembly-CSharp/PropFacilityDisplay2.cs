using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B9 RID: 697
public class PropFacilityDisplay2 : IEntityConfig
{
	// Token: 0x06000DCF RID: 3535 RVA: 0x0004C3A2 File Offset: 0x0004A5A2
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x0004C3AC File Offset: 0x0004A5AC
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityDisplay2";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY2.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY2.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_display2_kanim"), "off", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("display_prop2", UI.USERMENUACTIONS.READLORE.SEARCH_DISPLAY));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0004C45C File Offset: 0x0004A65C
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

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0004C4B4 File Offset: 0x0004A6B4
	public void OnSpawn(GameObject inst)
	{
	}
}
