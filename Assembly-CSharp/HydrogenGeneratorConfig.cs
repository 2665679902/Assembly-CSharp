﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class HydrogenGeneratorConfig : IBuildingConfig
{
	// Token: 0x060008D8 RID: 2264 RVA: 0x00034694 File Offset: 0x00032894
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HydrogenGenerator";
		int num = 4;
		int num2 = 3;
		string text2 = "generatormerc_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.GeneratorWattageRating = 800f;
		buildingDef.GeneratorBaseCapacity = 1000f;
		buildingDef.ExhaustKilowattsWhenActive = 2f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(1, 0);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(-1, 0));
		return buildingDef;
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00034758 File Offset: 0x00032958
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<Storage>();
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = GameTagExtensions.Create(SimHashes.Hydrogen);
		conduitConsumer.capacityKG = 2f;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		EnergyGenerator energyGenerator = go.AddOrGet<EnergyGenerator>();
		energyGenerator.formula = EnergyGenerator.CreateSimpleFormula(SimHashes.Hydrogen.CreateTag(), 0.1f, 2f, SimHashes.Void, 0f, true, default(CellOffset), 0f);
		energyGenerator.powerDistributionOrder = 8;
		energyGenerator.ignoreBatteryRefillPercent = true;
		energyGenerator.meterOffset = Meter.Offset.Behind;
		Tinkerable.MakePowerTinkerable(go);
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x0400058E RID: 1422
	public const string ID = "HydrogenGenerator";
}
