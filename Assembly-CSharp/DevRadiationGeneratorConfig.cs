using System;
using TUNING;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class DevRadiationGeneratorConfig : IBuildingConfig
{
	// Token: 0x060001BD RID: 445 RVA: 0x0000C738 File Offset: 0x0000A938
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DevRadiationGenerator";
		int num = 1;
		int num2 = 1;
		string text2 = "dev_generator_kanim";
		int num3 = 100;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.DebugOnly = true;
		return buildingDef;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.DevBuilding);
		RadiationEmitter radiationEmitter = go.AddOrGet<RadiationEmitter>();
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.radiusProportionalToRads = false;
		radiationEmitter.emitRadiusX = 12;
		radiationEmitter.emitRadiusY = 12;
		radiationEmitter.emitRads = 2400f / ((float)radiationEmitter.emitRadiusX / 6f);
		go.AddOrGet<DevRadiationEmitter>();
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000C811 File Offset: 0x0000AA11
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x04000108 RID: 264
	public const string ID = "DevRadiationGenerator";
}
