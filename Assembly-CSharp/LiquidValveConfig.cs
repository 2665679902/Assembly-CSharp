using System;
using TUNING;
using UnityEngine;

// Token: 0x020001FD RID: 509
public class LiquidValveConfig : IBuildingConfig
{
	// Token: 0x06000A07 RID: 2567 RVA: 0x0003A028 File Offset: 0x00038228
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LiquidValve";
		int num = 1;
		int num2 = 2;
		string text2 = "valveliquid_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, tier2, 0.2f);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.Floodable = false;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "LiquidValve");
		return buildingDef;
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0003A0CC File Offset: 0x000382CC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		ValveBase valveBase = go.AddOrGet<ValveBase>();
		valveBase.conduitType = ConduitType.Liquid;
		valveBase.maxFlow = 10f;
		valveBase.animFlowRanges = new ValveBase.AnimRangeInfo[]
		{
			new ValveBase.AnimRangeInfo(3f, "lo"),
			new ValveBase.AnimRangeInfo(7f, "med"),
			new ValveBase.AnimRangeInfo(10f, "hi")
		};
		go.AddOrGet<Valve>();
		go.AddOrGet<Workable>().workTime = 5f;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0003A170 File Offset: 0x00038370
	public override void DoPostConfigureComplete(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<RequireInputs>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000619 RID: 1561
	public const string ID = "LiquidValve";

	// Token: 0x0400061A RID: 1562
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;
}
