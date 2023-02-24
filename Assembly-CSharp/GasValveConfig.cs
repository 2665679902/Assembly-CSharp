using System;
using TUNING;
using UnityEngine;

// Token: 0x020001A2 RID: 418
public class GasValveConfig : IBuildingConfig
{
	// Token: 0x0600081D RID: 2077 RVA: 0x0002F4E8 File Offset: 0x0002D6E8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GasValve";
		int num = 1;
		int num2 = 2;
		string text2 = "valvegas_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, tier2, 0.2f);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.OutputConduitType = ConduitType.Gas;
		buildingDef.Floodable = false;
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.GasVentIDs, "GasValve");
		return buildingDef;
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0002F58C File Offset: 0x0002D78C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		ValveBase valveBase = go.AddOrGet<ValveBase>();
		valveBase.conduitType = ConduitType.Gas;
		valveBase.maxFlow = 1f;
		valveBase.animFlowRanges = new ValveBase.AnimRangeInfo[]
		{
			new ValveBase.AnimRangeInfo(0.25f, "lo"),
			new ValveBase.AnimRangeInfo(0.5f, "med"),
			new ValveBase.AnimRangeInfo(0.75f, "hi")
		};
		go.AddOrGet<Valve>();
		go.AddOrGet<Workable>().workTime = 5f;
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0002F630 File Offset: 0x0002D830
	public override void DoPostConfigureComplete(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<RequireInputs>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000524 RID: 1316
	public const string ID = "GasValve";

	// Token: 0x04000525 RID: 1317
	private const ConduitType CONDUIT_TYPE = ConduitType.Gas;
}
