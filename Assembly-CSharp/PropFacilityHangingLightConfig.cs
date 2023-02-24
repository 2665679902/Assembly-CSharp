using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002BD RID: 701
public class PropFacilityHangingLightConfig : IEntityConfig
{
	// Token: 0x06000DE3 RID: 3555 RVA: 0x0004C812 File Offset: 0x0004AA12
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0004C81C File Offset: 0x0004AA1C
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityHangingLight";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYLAMP.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYLAMP.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_light_kanim"), "off", Grid.SceneLayer.Building, 1, 4, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x0004C8B0 File Offset: 0x0004AAB0
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

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0004C908 File Offset: 0x0004AB08
	public void OnSpawn(GameObject inst)
	{
	}
}
