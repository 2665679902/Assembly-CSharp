using System;
using TUNING;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class HighEnergyParticleSpawnerConfig : IBuildingConfig
{
	// Token: 0x060008C1 RID: 2241 RVA: 0x00033EF8 File Offset: 0x000320F8
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00033F00 File Offset: 0x00032100
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HighEnergyParticleSpawner";
		int num = 1;
		int num2 = 2;
		string text2 = "radiation_collector_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UseHighEnergyParticleOutputPort = true;
		buildingDef.HighEnergyParticleOutputOffset = new CellOffset(0, 1);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 1f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.RadiationIDs, "HighEnergyParticleSpawner");
		buildingDef.Deprecated = !Sim.IsRadiationEnabled();
		return buildingDef;
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x00033FD8 File Offset: 0x000321D8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		Prioritizable.AddRef(go);
		go.AddOrGet<HighEnergyParticleStorage>().capacity = 500f;
		go.AddOrGet<LoopingSounds>();
		HighEnergyParticleSpawner highEnergyParticleSpawner = go.AddOrGet<HighEnergyParticleSpawner>();
		highEnergyParticleSpawner.minLaunchInterval = 2f;
		highEnergyParticleSpawner.radiationSampleRate = 0.2f;
		highEnergyParticleSpawner.minSlider = 50;
		highEnergyParticleSpawner.maxSlider = 500;
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00034045 File Offset: 0x00032245
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400057F RID: 1407
	public const string ID = "HighEnergyParticleSpawner";

	// Token: 0x04000580 RID: 1408
	public const float MIN_LAUNCH_INTERVAL = 2f;

	// Token: 0x04000581 RID: 1409
	public const float RADIATION_SAMPLE_RATE = 0.2f;

	// Token: 0x04000582 RID: 1410
	public const float HEP_PER_RAD = 0.1f;

	// Token: 0x04000583 RID: 1411
	public const int MIN_SLIDER = 50;

	// Token: 0x04000584 RID: 1412
	public const int MAX_SLIDER = 500;

	// Token: 0x04000585 RID: 1413
	public const float DISABLED_CONSUMPTION_RATE = 1f;
}
