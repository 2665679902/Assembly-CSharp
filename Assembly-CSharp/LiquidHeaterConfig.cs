using System;
using TUNING;
using UnityEngine;

// Token: 0x020001F5 RID: 501
public class LiquidHeaterConfig : IBuildingConfig
{
	// Token: 0x060009E9 RID: 2537 RVA: 0x00039478 File Offset: 0x00037678
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LiquidHeater";
		int num = 4;
		int num2 = 1;
		string text2 = "boiler_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 3200f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.Floodable = false;
		buildingDef.EnergyConsumptionWhenActive = 960f;
		buildingDef.ExhaustKilowattsWhenActive = 4000f;
		buildingDef.SelfHeatKilowattsWhenActive = 64f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "SolidMetal";
		buildingDef.OverheatTemperature = 398.15f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(1, 0));
		return buildingDef;
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x00039520 File Offset: 0x00037720
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		SpaceHeater spaceHeater = go.AddOrGet<SpaceHeater>();
		spaceHeater.SetLiquidHeater();
		spaceHeater.targetTemperature = 358.15f;
		spaceHeater.minimumCellMass = 400f;
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0003954A File Offset: 0x0003774A
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x0400060A RID: 1546
	public const string ID = "LiquidHeater";

	// Token: 0x0400060B RID: 1547
	public const float CONSUMPTION_RATE = 1f;
}
