using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C0 RID: 704
public class PropFacilityTableConfig : IEntityConfig
{
	// Token: 0x06000DF2 RID: 3570 RVA: 0x0004CB12 File Offset: 0x0004AD12
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x0004CB1C File Offset: 0x0004AD1C
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityTable";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYTABLE.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYTABLE.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_table_kanim"), "off", Grid.SceneLayer.Building, 4, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x0004CBB0 File Offset: 0x0004ADB0
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

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0004CC08 File Offset: 0x0004AE08
	public void OnSpawn(GameObject inst)
	{
	}
}
