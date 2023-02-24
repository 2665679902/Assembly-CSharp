using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class PropElevatorConfig : IEntityConfig
{
	// Token: 0x06000DB1 RID: 3505 RVA: 0x0004BD86 File Offset: 0x00049F86
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0004BD90 File Offset: 0x00049F90
	public GameObject CreatePrefab()
	{
		string text = "PropElevator";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPELEVATOR.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPELEVATOR.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_elevator_kanim"), "off", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0004BE24 File Offset: 0x0004A024
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

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0004BE7C File Offset: 0x0004A07C
	public void OnSpawn(GameObject inst)
	{
	}
}
