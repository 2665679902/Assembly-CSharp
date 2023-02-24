using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B6 RID: 694
public class PropFacilityChandelierConfig : IEntityConfig
{
	// Token: 0x06000DC0 RID: 3520 RVA: 0x0004C086 File Offset: 0x0004A286
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x0004C090 File Offset: 0x0004A290
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityChandelier";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHANDELIER.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHANDELIER.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_chandelier_kanim"), "off", Grid.SceneLayer.Building, 5, 7, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x0004C124 File Offset: 0x0004A324
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

	// Token: 0x06000DC3 RID: 3523 RVA: 0x0004C17C File Offset: 0x0004A37C
	public void OnSpawn(GameObject inst)
	{
	}
}
