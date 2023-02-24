using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200014B RID: 331
public class MethaneGeyserConfig : IEntityConfig
{
	// Token: 0x0600065E RID: 1630 RVA: 0x00029781 File Offset: 0x00027981
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x00029788 File Offset: 0x00027988
	public GameObject CreatePrefab()
	{
		string text = "MethaneGeyser";
		string text2 = STRINGS.CREATURES.SPECIES.METHANEGEYSER.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.METHANEGEYSER.DESC;
		float num = 2000f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("geyser_side_methane_kanim"), "inactive", Grid.SceneLayer.BuildingBack, 4, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.DeprecatedContent, false);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.IgneousRock, true);
		component.Temperature = 372.15f;
		gameObject.AddOrGet<Geyser>().outputOffset = new Vector2I(0, 1);
		GeyserConfigurator geyserConfigurator = gameObject.AddOrGet<GeyserConfigurator>();
		geyserConfigurator.presetType = "methane";
		geyserConfigurator.presetMin = 0.35f;
		geyserConfigurator.presetMax = 0.65f;
		Studyable studyable = gameObject.AddOrGet<Studyable>();
		studyable.meterTrackerSymbol = "geotracker_target";
		studyable.meterAnim = "tracker";
		gameObject.AddOrGet<LoopingSounds>();
		SoundEventVolumeCache.instance.AddVolume("geyser_side_methane_kanim", "GeyserMethane_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_side_methane_kanim", "GeyserMethane_erupt_LP", NOISE_POLLUTION.NOISY.TIER6);
		return gameObject;
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x000298A7 File Offset: 0x00027AA7
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x000298A9 File Offset: 0x00027AA9
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000466 RID: 1126
	public const string ID = "MethaneGeyser";
}
