using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002BC RID: 700
public class PropFacilityGlobeDroorsConfig : IEntityConfig
{
	// Token: 0x06000DDE RID: 3550 RVA: 0x0004C6F6 File Offset: 0x0004A8F6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0004C700 File Offset: 0x0004A900
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityGlobeDroors";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYGLOBEDROORS.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPFACILITYGLOBEDROORS.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_globe_kanim"), "off", Grid.SceneLayer.Building, 1, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("journal_newspaper", UI.USERMENUACTIONS.READLORE.SEARCH_CABINET));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x0004C7B0 File Offset: 0x0004A9B0
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

	// Token: 0x06000DE1 RID: 3553 RVA: 0x0004C808 File Offset: 0x0004AA08
	public void OnSpawn(GameObject inst)
	{
	}
}
