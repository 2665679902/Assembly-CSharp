﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200034D RID: 845
public class WaterPurifierConfig : IBuildingConfig
{
	// Token: 0x06001102 RID: 4354 RVA: 0x0005C054 File Offset: 0x0005A254
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WaterPurifier";
		int num = 4;
		int num2 = 3;
		string text2 = "waterpurifier_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(-1, 0));
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.PowerInputOffset = new CellOffset(2, 0);
		buildingDef.UtilityInputOffset = new CellOffset(-1, 2);
		buildingDef.UtilityOutputOffset = new CellOffset(2, 2);
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "WaterPurifier");
		return buildingDef;
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0005C138 File Offset: 0x0005A338
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		go.AddOrGet<WaterPurifier>();
		Prioritizable.AddRef(go);
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(new Tag("Filter"), 1f, true),
			new ElementConverter.ConsumedElement(new Tag("DirtyWater"), 5f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(5f, SimHashes.Water, 0f, false, true, 0f, 0.5f, 0.75f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.2f, SimHashes.ToxicSand, 0f, false, true, 0f, 0.5f, 0.25f, byte.MaxValue, 0, true)
		};
		ElementDropper elementDropper = go.AddComponent<ElementDropper>();
		elementDropper.emitMass = 10f;
		elementDropper.emitTag = new Tag("ToxicSand");
		elementDropper.emitOffset = new Vector3(0f, 1f, 0f);
		ManualDeliveryKG manualDeliveryKG = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = new Tag("Filter");
		manualDeliveryKG.capacity = 1200f;
		manualDeliveryKG.refillMass = 300f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 10f;
		conduitConsumer.capacityKG = 20f;
		conduitConsumer.capacityTag = GameTags.AnyWater;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.invertElementFilter = true;
		conduitDispenser.elementFilter = new SimHashes[] { SimHashes.DirtyWater };
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0005C31B File Offset: 0x0005A51B
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x04000935 RID: 2357
	public const string ID = "WaterPurifier";

	// Token: 0x04000936 RID: 2358
	private const float FILTER_INPUT_RATE = 1f;

	// Token: 0x04000937 RID: 2359
	private const float DIRTY_WATER_INPUT_RATE = 5f;

	// Token: 0x04000938 RID: 2360
	private const float FILTER_CAPACITY = 1200f;

	// Token: 0x04000939 RID: 2361
	private const float USED_FILTER_OUTPUT_RATE = 0.2f;

	// Token: 0x0400093A RID: 2362
	private const float CLEAN_WATER_OUTPUT_RATE = 5f;

	// Token: 0x0400093B RID: 2363
	private const float TARGET_OUTPUT_TEMPERATURE = 313.15f;
}
