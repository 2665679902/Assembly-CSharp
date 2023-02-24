using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200019C RID: 412
public class GasLimitValveConfig : IBuildingConfig
{
	// Token: 0x06000804 RID: 2052 RVA: 0x0002EAFF File Offset: 0x0002CCFF
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x0002EB08 File Offset: 0x0002CD08
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GasLimitValve";
		int num = 1;
		int num2 = 2;
		string text2 = "limit_valve_gas_kanim";
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
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.OutputConduitType = ConduitType.Gas;
		buildingDef.Floodable = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 10f;
		buildingDef.PowerInputOffset = new CellOffset(0, 1);
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 1);
		buildingDef.LogicInputPorts = new List<LogicPorts.Port>
		{
			new LogicPorts.Port(LimitValve.RESET_PORT_ID, new CellOffset(0, 1), STRINGS.BUILDINGS.PREFABS.GASLIMITVALVE.LOGIC_PORT_RESET, STRINGS.BUILDINGS.PREFABS.GASLIMITVALVE.RESET_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.GASLIMITVALVE.RESET_PORT_INACTIVE, false, LogicPortSpriteType.ResetUpdate, true)
		};
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LimitValve.OUTPUT_PORT_ID, new CellOffset(0, 0), STRINGS.BUILDINGS.PREFABS.GASLIMITVALVE.LOGIC_PORT_OUTPUT, STRINGS.BUILDINGS.PREFABS.GASLIMITVALVE.OUTPUT_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.GASLIMITVALVE.OUTPUT_PORT_INACTIVE, false, false) };
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.GasVentIDs, "GasLimitValve");
		return buildingDef;
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x0002EC74 File Offset: 0x0002CE74
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGetDef<PoweredActiveTransitionController.Def>();
		UnityEngine.Object.DestroyImmediate(go.GetComponent<BuildingEnabledButton>());
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<ConduitBridge>().type = ConduitType.Gas;
		LimitValve limitValve = go.AddOrGet<LimitValve>();
		limitValve.conduitType = ConduitType.Gas;
		limitValve.maxLimitKg = 500f;
		limitValve.Limit = 0f;
		limitValve.sliderRanges = LimitValveTuning.GetDefaultSlider();
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x0002ECE1 File Offset: 0x0002CEE1
	public override void DoPostConfigureComplete(GameObject go)
	{
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
		UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
		go.GetComponent<RequireInputs>().SetRequirements(true, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x04000518 RID: 1304
	public const string ID = "GasLimitValve";

	// Token: 0x04000519 RID: 1305
	private const ConduitType CONDUIT_TYPE = ConduitType.Gas;
}
