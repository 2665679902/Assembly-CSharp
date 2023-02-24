using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B7 RID: 695
public class PropFacilityCouchConfig : IEntityConfig
{
	// Token: 0x06000DC5 RID: 3525 RVA: 0x0004C186 File Offset: 0x0004A386
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x0004C190 File Offset: 0x0004A390
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityCouch";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYCOUCH.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYCOUCH.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_couch_kanim"), "off", Grid.SceneLayer.Building, 4, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0004C224 File Offset: 0x0004A424
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

	// Token: 0x06000DC8 RID: 3528 RVA: 0x0004C27C File Offset: 0x0004A47C
	public void OnSpawn(GameObject inst)
	{
	}
}
