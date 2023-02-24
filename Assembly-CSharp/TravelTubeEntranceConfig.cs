using System;
using TUNING;
using UnityEngine;

// Token: 0x0200033F RID: 831
public class TravelTubeEntranceConfig : IBuildingConfig
{
	// Token: 0x06001092 RID: 4242 RVA: 0x0005A0E8 File Offset: 0x000582E8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "TravelTubeEntrance";
		int num = 3;
		int num2 = 2;
		string text2 = "tube_launcher_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 960f;
		buildingDef.Entombable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PowerInputOffset = new CellOffset(1, 0);
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(1, 1));
		return buildingDef;
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x0005A178 File Offset: 0x00058378
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		TravelTubeEntrance travelTubeEntrance = go.AddOrGet<TravelTubeEntrance>();
		travelTubeEntrance.joulesPerLaunch = 10000f;
		travelTubeEntrance.jouleCapacity = 40000f;
		go.AddOrGet<TravelTubeEntrance.Work>();
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGet<EnergyConsumerSelfSustaining>();
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0005A1AA File Offset: 0x000583AA
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.NoWire;
	}

	// Token: 0x04000902 RID: 2306
	public const string ID = "TravelTubeEntrance";

	// Token: 0x04000903 RID: 2307
	private const float JOULES_PER_LAUNCH = 10000f;

	// Token: 0x04000904 RID: 2308
	private const float LAUNCHES_FROM_FULL_CHARGE = 4f;
}
