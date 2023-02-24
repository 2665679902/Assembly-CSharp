using System;
using TUNING;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class SpaceHeaterConfig : IBuildingConfig
{
	// Token: 0x06000FDF RID: 4063 RVA: 0x0005601C File Offset: 0x0005421C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SpaceHeater";
		int num = 2;
		int num2 = 2;
		string text2 = "spaceheater_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 2f;
		buildingDef.SelfHeatKilowattsWhenActive = 16f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(1, 0));
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.OverheatTemperature = 398.15f;
		return buildingDef;
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x000560BD File Offset: 0x000542BD
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<SpaceHeater>().targetTemperature = 343.15f;
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x000560D6 File Offset: 0x000542D6
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x040008A7 RID: 2215
	public const string ID = "SpaceHeater";
}
