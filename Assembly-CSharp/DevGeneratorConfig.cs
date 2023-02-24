using System;
using TUNING;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class DevGeneratorConfig : IBuildingConfig
{
	// Token: 0x060001A9 RID: 425 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DevGenerator";
		int num = 1;
		int num2 = 1;
		string text2 = "dev_generator_kanim";
		int num3 = 100;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.GeneratorWattageRating = 100000f;
		buildingDef.GeneratorBaseCapacity = 200000f;
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(0, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		buildingDef.Floodable = false;
		buildingDef.DebugOnly = true;
		return buildingDef;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000C153 File Offset: 0x0000A353
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.DevBuilding);
		DevGenerator devGenerator = go.AddOrGet<DevGenerator>();
		devGenerator.powerDistributionOrder = 9;
		devGenerator.wattageRating = 100000f;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000C178 File Offset: 0x0000A378
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x040000F9 RID: 249
	public const string ID = "DevGenerator";
}
