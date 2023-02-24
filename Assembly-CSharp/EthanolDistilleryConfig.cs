using System;
using TUNING;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class EthanolDistilleryConfig : IBuildingConfig
{
	// Token: 0x06000256 RID: 598 RVA: 0x00010FE4 File Offset: 0x0000F1E4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "EthanolDistillery";
		int num = 4;
		int num2 = 3;
		string text2 = "ethanoldistillery_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, tier2, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.PowerInputOffset = new CellOffset(2, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(-1, 0);
		return buildingDef;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00011090 File Offset: 0x0000F290
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.elementFilter = new SimHashes[] { SimHashes.Ethanol };
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 1000f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.showInUI = true;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = WoodLogConfig.TAG;
		manualDeliveryKG.capacity = 600f;
		manualDeliveryKG.refillMass = 150f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(WoodLogConfig.TAG, 1f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.5f, SimHashes.Ethanol, 346.5f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.33333334f, SimHashes.ToxicSand, 366.5f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.16666667f, SimHashes.CarbonDioxide, 366.5f, false, false, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
		};
		AlgaeDistillery algaeDistillery = go.AddOrGet<AlgaeDistillery>();
		algaeDistillery.emitMass = 20f;
		algaeDistillery.emitTag = new Tag("ToxicSand");
		algaeDistillery.emitOffset = new Vector3(2f, 1f);
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00011243 File Offset: 0x0000F443
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00011245 File Offset: 0x0000F445
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00011247 File Offset: 0x0000F447
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x04000144 RID: 324
	public const string ID = "EthanolDistillery";

	// Token: 0x04000145 RID: 325
	public const float ORGANICS_CONSUME_PER_SECOND = 1f;

	// Token: 0x04000146 RID: 326
	public const float ORGANICS_STORAGE_AMOUNT = 600f;

	// Token: 0x04000147 RID: 327
	public const float ETHANOL_RATE = 0.5f;

	// Token: 0x04000148 RID: 328
	public const float SOLID_WASTE_RATE = 0.33333334f;

	// Token: 0x04000149 RID: 329
	public const float CO2_WASTE_RATE = 0.16666667f;

	// Token: 0x0400014A RID: 330
	public const float OUTPUT_TEMPERATURE = 346.5f;

	// Token: 0x0400014B RID: 331
	public const float WASTE_OUTPUT_TEMPERATURE = 366.5f;
}
