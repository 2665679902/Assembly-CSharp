using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B5 RID: 693
public class PropFacilityChairFlipConfig : IEntityConfig
{
	// Token: 0x06000DBB RID: 3515 RVA: 0x0004BF86 File Offset: 0x0004A186
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0004BF90 File Offset: 0x0004A190
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityChairFlip";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHAIR.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHAIR.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_chairFlip_kanim"), "off", Grid.SceneLayer.Building, 2, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0004C024 File Offset: 0x0004A224
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

	// Token: 0x06000DBE RID: 3518 RVA: 0x0004C07C File Offset: 0x0004A27C
	public void OnSpawn(GameObject inst)
	{
	}
}
