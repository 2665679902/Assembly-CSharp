using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class ChlorineGeyserConfig : IEntityConfig
{
	// Token: 0x0600060A RID: 1546 RVA: 0x00027239 File Offset: 0x00025439
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x00027240 File Offset: 0x00025440
	public GameObject CreatePrefab()
	{
		string text = "ChlorineGeyser";
		string text2 = STRINGS.CREATURES.SPECIES.CHLORINEGEYSER.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.CHLORINEGEYSER.DESC;
		float num = 2000f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("geyser_side_chlorine_kanim"), "inactive", Grid.SceneLayer.BuildingBack, 4, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.DeprecatedContent, false);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.IgneousRock, true);
		component.Temperature = 372.15f;
		gameObject.AddOrGet<Geyser>().outputOffset = new Vector2I(0, 1);
		GeyserConfigurator geyserConfigurator = gameObject.AddOrGet<GeyserConfigurator>();
		geyserConfigurator.presetType = "chlorine_gas";
		geyserConfigurator.presetMin = 0.35f;
		geyserConfigurator.presetMax = 0.65f;
		Studyable studyable = gameObject.AddOrGet<Studyable>();
		studyable.meterTrackerSymbol = "geotracker_target";
		studyable.meterAnim = "tracker";
		gameObject.AddOrGet<LoopingSounds>();
		SoundEventVolumeCache.instance.AddVolume("geyser_methane_kanim", "GeyserMethane_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_methane_kanim", "GeyserMethane_shake_LP", NOISE_POLLUTION.NOISY.TIER6);
		return gameObject;
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0002735F File Offset: 0x0002555F
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x00027361 File Offset: 0x00025561
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000419 RID: 1049
	public const string ID = "ChlorineGeyser";
}
