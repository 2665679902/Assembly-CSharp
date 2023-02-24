using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002BE RID: 702
public class PropFacilityPaintingConfig : IEntityConfig
{
	// Token: 0x06000DE8 RID: 3560 RVA: 0x0004C912 File Offset: 0x0004AB12
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x0004C91C File Offset: 0x0004AB1C
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityPainting";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYPAINTING.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYPAINTING.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_painting_kanim"), "off", Grid.SceneLayer.Building, 3, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x0004C9B0 File Offset: 0x0004ABB0
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

	// Token: 0x06000DEB RID: 3563 RVA: 0x0004CA08 File Offset: 0x0004AC08
	public void OnSpawn(GameObject inst)
	{
	}
}
