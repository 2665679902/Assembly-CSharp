using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F8 RID: 504
public class LiquidLogicValveConfig : IBuildingConfig
{
	// Token: 0x060009F4 RID: 2548 RVA: 0x000397C0 File Offset: 0x000379C0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LiquidLogicValve";
		int num = 1;
		int num2 = 2;
		string text2 = "valveliquid_logic_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER0, tier2, 0.2f);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.Floodable = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 10f;
		buildingDef.PowerInputOffset = new CellOffset(0, 1);
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(LogicOperationalController.PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LIQUIDLOGICVALVE.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.LIQUIDLOGICVALVE.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LIQUIDLOGICVALVE.LOGIC_PORT_INACTIVE, true, false) };
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "LiquidLogicValve");
		return buildingDef;
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x000398C2 File Offset: 0x00037AC2
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<BuildingEnabledButton>());
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		OperationalValve operationalValve = go.AddOrGet<OperationalValve>();
		operationalValve.conduitType = ConduitType.Liquid;
		operationalValve.maxFlow = 10f;
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x000398FC File Offset: 0x00037AFC
	public override void DoPostConfigureComplete(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
		go.GetComponent<RequireInputs>().SetRequirements(true, false);
		go.AddOrGet<LogicOperationalController>().unNetworkedValue = 0;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000610 RID: 1552
	public const string ID = "LiquidLogicValve";

	// Token: 0x04000611 RID: 1553
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;
}
