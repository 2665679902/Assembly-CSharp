using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class PropFacilityWallDegreeConfig : IEntityConfig
{
	// Token: 0x06000DF7 RID: 3575 RVA: 0x0004CC12 File Offset: 0x0004AE12
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x0004CC1C File Offset: 0x0004AE1C
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityWallDegree";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYWALLDEGREE.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYWALLDEGREE.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_degree_kanim"), "off", Grid.SceneLayer.Building, 2, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x0004CCB0 File Offset: 0x0004AEB0
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

	// Token: 0x06000DFA RID: 3578 RVA: 0x0004CD08 File Offset: 0x0004AF08
	public void OnSpawn(GameObject inst)
	{
	}
}
