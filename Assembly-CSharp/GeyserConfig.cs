using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000147 RID: 327
public class GeyserConfig : IEntityConfig
{
	// Token: 0x06000648 RID: 1608 RVA: 0x000286E0 File Offset: 0x000268E0
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x000286E8 File Offset: 0x000268E8
	public GameObject CreatePrefab()
	{
		string text = "Geyser";
		string text2 = STRINGS.CREATURES.SPECIES.GEYSER.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.GEYSER.DESC;
		float num = 2000f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("geyser_side_steam_kanim"), "inactive", Grid.SceneLayer.BuildingBack, 4, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.DeprecatedContent, false);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.IgneousRock, true);
		component.Temperature = 372.15f;
		gameObject.AddOrGet<Geyser>().outputOffset = new Vector2I(0, 1);
		gameObject.AddOrGet<UserNameable>();
		GeyserConfigurator geyserConfigurator = gameObject.AddOrGet<GeyserConfigurator>();
		geyserConfigurator.presetType = "steam";
		geyserConfigurator.presetMin = 0.5f;
		geyserConfigurator.presetMax = 0.75f;
		Studyable studyable = gameObject.AddOrGet<Studyable>();
		studyable.meterTrackerSymbol = "geotracker_target";
		studyable.meterAnim = "tracker";
		gameObject.AddOrGet<LoopingSounds>();
		SoundEventVolumeCache.instance.AddVolume("geyser_side_steam_kanim", "Geyser_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_side_steam_kanim", "Geyser_erupt_LP", NOISE_POLLUTION.NOISY.TIER6);
		return gameObject;
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0002880E File Offset: 0x00026A0E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00028810 File Offset: 0x00026A10
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000445 RID: 1093
	public const int GEOTUNERS_REQUIRED_FOR_MAJOR_TRACKER_ANIMATION = 5;

	// Token: 0x02000EC1 RID: 3777
	public enum TrackerMeterAnimNames
	{
		// Token: 0x04005237 RID: 21047
		tracker,
		// Token: 0x04005238 RID: 21048
		geotracker,
		// Token: 0x04005239 RID: 21049
		geotracker_minor,
		// Token: 0x0400523A RID: 21050
		geotracker_major
	}
}
