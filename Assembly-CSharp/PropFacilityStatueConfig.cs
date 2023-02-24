using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002BF RID: 703
public class PropFacilityStatueConfig : IEntityConfig
{
	// Token: 0x06000DED RID: 3565 RVA: 0x0004CA12 File Offset: 0x0004AC12
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x0004CA1C File Offset: 0x0004AC1C
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityStatue";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYSTATUE.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYSTATUE.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_statue_kanim"), "off", Grid.SceneLayer.Building, 5, 9, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x0004CAB0 File Offset: 0x0004ACB0
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

	// Token: 0x06000DF0 RID: 3568 RVA: 0x0004CB08 File Offset: 0x0004AD08
	public void OnSpawn(GameObject inst)
	{
	}
}
