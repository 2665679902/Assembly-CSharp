using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B4 RID: 692
public class PropFacilityChairConfig : IEntityConfig
{
	// Token: 0x06000DB6 RID: 3510 RVA: 0x0004BE86 File Offset: 0x0004A086
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x0004BE90 File Offset: 0x0004A090
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityChair";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHAIR.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHAIR.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_chair_kanim"), "off", Grid.SceneLayer.Building, 2, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x0004BF24 File Offset: 0x0004A124
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

	// Token: 0x06000DB9 RID: 3513 RVA: 0x0004BF7C File Offset: 0x0004A17C
	public void OnSpawn(GameObject inst)
	{
	}
}
