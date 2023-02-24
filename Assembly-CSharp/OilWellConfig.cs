using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class OilWellConfig : IEntityConfig
{
	// Token: 0x0600066D RID: 1645 RVA: 0x00029B4A File Offset: 0x00027D4A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x00029B54 File Offset: 0x00027D54
	public GameObject CreatePrefab()
	{
		string text = "OilWell";
		string text2 = STRINGS.CREATURES.SPECIES.OIL_WELL.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.OIL_WELL.DESC;
		float num = 2000f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("geyser_side_oil_kanim"), "off", Grid.SceneLayer.BuildingBack, 4, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.SedimentaryRock, true);
		component.Temperature = 372.15f;
		gameObject.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 0), GameTags.OilWell, null)
		};
		SoundEventVolumeCache.instance.AddVolume("geyser_side_methane_kanim", "GeyserMethane_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_side_methane_kanim", "GeyserMethane_erupt_LP", NOISE_POLLUTION.NOISY.TIER6);
		return gameObject;
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x00029C44 File Offset: 0x00027E44
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x00029C46 File Offset: 0x00027E46
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400046B RID: 1131
	public const string ID = "OilWell";
}
