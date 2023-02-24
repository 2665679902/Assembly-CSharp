using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class LiquidLimitValveConfig : IBuildingConfig
{
	// Token: 0x060009EF RID: 2543 RVA: 0x0003959E File Offset: 0x0003779E
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x000395A8 File Offset: 0x000377A8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LiquidLimitValve";
		int num = 1;
		int num2 = 2;
		string text2 = "limit_valve_liquid_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] array = new float[]
		{
			TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0[0],
			TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
		};
		string[] array2 = new string[] { "RefinedMetal", "Plastic" };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER0, tier, 0.2f);
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
		buildingDef.LogicInputPorts = new List<LogicPorts.Port>
		{
			new LogicPorts.Port(LimitValve.RESET_PORT_ID, new CellOffset(0, 1), STRINGS.BUILDINGS.PREFABS.LIQUIDLIMITVALVE.LOGIC_PORT_RESET, STRINGS.BUILDINGS.PREFABS.LIQUIDLIMITVALVE.RESET_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LIQUIDLIMITVALVE.RESET_PORT_INACTIVE, false, LogicPortSpriteType.ResetUpdate, true)
		};
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LimitValve.OUTPUT_PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.LIQUIDLIMITVALVE.LOGIC_PORT_OUTPUT, STRINGS.BUILDINGS.PREFABS.LIQUIDLIMITVALVE.OUTPUT_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.LIQUIDLIMITVALVE.OUTPUT_PORT_INACTIVE, false, false) };
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "LiquidLimitValve");
		return buildingDef;
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x00039714 File Offset: 0x00037914
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGetDef<PoweredActiveTransitionController.Def>();
		UnityEngine.Object.DestroyImmediate(go.GetComponent<BuildingEnabledButton>());
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<ConduitBridge>().type = ConduitType.Liquid;
		LimitValve limitValve = go.AddOrGet<LimitValve>();
		limitValve.conduitType = ConduitType.Liquid;
		limitValve.maxLimitKg = 500f;
		limitValve.Limit = 0f;
		limitValve.sliderRanges = LimitValveTuning.GetDefaultSlider();
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00039781 File Offset: 0x00037981
	public override void DoPostConfigureComplete(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
		go.GetComponent<RequireInputs>().SetRequirements(true, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x0400060E RID: 1550
	public const string ID = "LiquidLimitValve";

	// Token: 0x0400060F RID: 1551
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;
}
