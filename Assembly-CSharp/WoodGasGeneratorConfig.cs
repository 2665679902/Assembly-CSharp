﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class WoodGasGeneratorConfig : IBuildingConfig
{
	// Token: 0x0600112D RID: 4397 RVA: 0x0005C920 File Offset: 0x0005AB20
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WoodGasGenerator";
		int num = 2;
		int num2 = 2;
		string text2 = "generatorwood_kanim";
		int num3 = 100;
		float num4 = 120f;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] array = all_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.GeneratorWattageRating = 300f;
		buildingDef.GeneratorBaseCapacity = 20000f;
		buildingDef.ExhaustKilowattsWhenActive = 8f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(1, 1));
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(0, 0);
		return buildingDef;
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x0005C9D0 File Offset: 0x0005ABD0
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<LoopingSounds>();
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.showInUI = true;
		float num = 720f;
		go.AddOrGet<LoopingSounds>();
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = WoodLogConfig.TAG;
		manualDeliveryKG.capacity = 360f;
		manualDeliveryKG.refillMass = 180f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		EnergyGenerator energyGenerator = go.AddOrGet<EnergyGenerator>();
		energyGenerator.powerDistributionOrder = 8;
		energyGenerator.hasMeter = true;
		energyGenerator.formula = EnergyGenerator.CreateSimpleFormula(WoodLogConfig.TAG, 1.2f, num, SimHashes.CarbonDioxide, 0.17f, false, new CellOffset(0, 1), 383.15f);
		Tinkerable.MakePowerTinkerable(go);
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x04000944 RID: 2372
	public const string ID = "WoodGasGenerator";

	// Token: 0x04000945 RID: 2373
	private const float BRANCHES_PER_GENERATOR = 8f;

	// Token: 0x04000946 RID: 2374
	public const float CONSUMPTION_RATE = 1.2f;

	// Token: 0x04000947 RID: 2375
	private const float WOOD_PER_REFILL = 360f;

	// Token: 0x04000948 RID: 2376
	private const SimHashes EXHAUST_ELEMENT_GAS = SimHashes.CarbonDioxide;

	// Token: 0x04000949 RID: 2377
	private const SimHashes EXHAUST_ELEMENT_GAS2 = SimHashes.Syngas;

	// Token: 0x0400094A RID: 2378
	public const float CO2_EXHAUST_RATE = 0.17f;

	// Token: 0x0400094B RID: 2379
	private const int WIDTH = 2;

	// Token: 0x0400094C RID: 2380
	private const int HEIGHT = 2;
}
