using System;
using TUNING;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class SolarPanelConfig : IBuildingConfig
{
	// Token: 0x06000F81 RID: 3969 RVA: 0x00054704 File Offset: 0x00052904
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SolarPanel";
		int num = 7;
		int num2 = 3;
		string text2 = "solar_panel_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] glasses = MATERIALS.GLASSES;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, glasses, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.GeneratorWattageRating = 380f;
		buildingDef.GeneratorBaseCapacity = buildingDef.GeneratorWattageRating;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.BuildLocationRule = BuildLocationRule.Anywhere;
		buildingDef.HitPoints = 10;
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(0, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x000547BB File Offset: 0x000529BB
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<LoopingSounds>();
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x000547DC File Offset: 0x000529DC
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<Repairable>().expectedRepairTime = 52.5f;
		go.AddOrGet<SolarPanel>().powerDistributionOrder = 9;
		go.AddOrGetDef<PoweredActiveController.Def>();
		MakeBaseSolid.Def def = go.AddOrGetDef<MakeBaseSolid.Def>();
		def.occupyFoundationLayer = false;
		def.solidOffsets = new CellOffset[7];
		for (int i = 0; i < 7; i++)
		{
			def.solidOffsets[i] = new CellOffset(i - 3, 0);
		}
	}

	// Token: 0x04000885 RID: 2181
	public const string ID = "SolarPanel";

	// Token: 0x04000886 RID: 2182
	public const float WATTS_PER_LUX = 0.00053f;

	// Token: 0x04000887 RID: 2183
	public const float MAX_WATTS = 380f;

	// Token: 0x04000888 RID: 2184
	private const int WIDTH = 7;
}
