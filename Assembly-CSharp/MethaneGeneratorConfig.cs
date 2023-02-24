﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200023D RID: 573
public class MethaneGeneratorConfig : IBuildingConfig
{
	// Token: 0x06000B4D RID: 2893 RVA: 0x0003FEB4 File Offset: 0x0003E0B4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MethaneGenerator";
		int num = 4;
		int num2 = 3;
		string text2 = "generatormethane_kanim";
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
		buildingDef.SelfHeatKilowattsWhenActive = 8f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(2, 2);
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(0, 0);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.OutputConduitType = ConduitType.Gas;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		return buildingDef;
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x0003FF8C File Offset: 0x0003E18C
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<Storage>().capacityKg = 50f;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 0.90000004f;
		conduitConsumer.capacityTag = GameTags.CombustibleGas;
		conduitConsumer.capacityKG = 0.90000004f;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		EnergyGenerator energyGenerator = go.AddOrGet<EnergyGenerator>();
		energyGenerator.powerDistributionOrder = 8;
		energyGenerator.ignoreBatteryRefillPercent = true;
		energyGenerator.formula = new EnergyGenerator.Formula
		{
			inputs = new EnergyGenerator.InputItem[]
			{
				new EnergyGenerator.InputItem(GameTags.Methane, 0.09f, 0.90000004f)
			},
			outputs = new EnergyGenerator.OutputItem[]
			{
				new EnergyGenerator.OutputItem(SimHashes.DirtyWater, 0.0675f, false, new CellOffset(1, 1), 313.15f),
				new EnergyGenerator.OutputItem(SimHashes.CarbonDioxide, 0.0225f, true, new CellOffset(0, 2), 383.15f)
			}
		};
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Gas;
		conduitDispenser.invertElementFilter = true;
		conduitDispenser.elementFilter = new SimHashes[]
		{
			SimHashes.Methane,
			SimHashes.Syngas
		};
		Tinkerable.MakePowerTinkerable(go);
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x040006B3 RID: 1715
	public const string ID = "MethaneGenerator";

	// Token: 0x040006B4 RID: 1716
	public const float FUEL_CONSUMPTION_RATE = 0.09f;

	// Token: 0x040006B5 RID: 1717
	private const float CO2_RATIO = 0.25f;

	// Token: 0x040006B6 RID: 1718
	public const float WATER_OUTPUT_TEMPERATURE = 313.15f;

	// Token: 0x040006B7 RID: 1719
	private const int WIDTH = 4;

	// Token: 0x040006B8 RID: 1720
	private const int HEIGHT = 3;
}
