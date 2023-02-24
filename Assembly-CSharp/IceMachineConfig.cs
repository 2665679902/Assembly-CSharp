using System;
using TUNING;
using UnityEngine;

// Token: 0x020001C5 RID: 453
public class IceMachineConfig : IBuildingConfig
{
	// Token: 0x060008E4 RID: 2276 RVA: 0x00034C78 File Offset: 0x00032E78
	public override BuildingDef CreateBuildingDef()
	{
		string text = "IceMachine";
		int num = 2;
		int num2 = 3;
		string text2 = "freezerator_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = this.energyConsumption;
		buildingDef.ExhaustKilowattsWhenActive = 4f;
		buildingDef.SelfHeatKilowattsWhenActive = 12f;
		buildingDef.ViewMode = OverlayModes.Temperature.ID;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00034D00 File Offset: 0x00032F00
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		storage.showInUI = true;
		storage.capacityKg = 30f;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		storage2.showInUI = true;
		storage2.capacityKg = 150f;
		storage2.allowItemRemoval = true;
		storage2.ignoreSourcePriority = true;
		storage2.allowUIItemRemoval = true;
		go.AddOrGet<LoopingSounds>();
		Prioritizable.AddRef(go);
		IceMachine iceMachine = go.AddOrGet<IceMachine>();
		iceMachine.SetStorages(storage, storage2);
		iceMachine.targetTemperature = 253.15f;
		iceMachine.heatRemovalRate = 20f;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = GameTags.Water;
		manualDeliveryKG.capacity = 30f;
		manualDeliveryKG.refillMass = 6f;
		manualDeliveryKG.MinimumMass = 10f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00034DEC File Offset: 0x00032FEC
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000595 RID: 1429
	public const string ID = "IceMachine";

	// Token: 0x04000596 RID: 1430
	private const float WATER_STORAGE = 30f;

	// Token: 0x04000597 RID: 1431
	private const float ICE_STORAGE = 150f;

	// Token: 0x04000598 RID: 1432
	private const float WATER_INPUT_RATE = 0.5f;

	// Token: 0x04000599 RID: 1433
	private const float ICE_OUTPUT_RATE = 0.5f;

	// Token: 0x0400059A RID: 1434
	private const float ICE_PER_LOAD = 30f;

	// Token: 0x0400059B RID: 1435
	private const float TARGET_ICE_TEMP = 253.15f;

	// Token: 0x0400059C RID: 1436
	private const float KDTU_TRANSFER_RATE = 20f;

	// Token: 0x0400059D RID: 1437
	private const float THERMAL_CONSERVATION = 0.8f;

	// Token: 0x0400059E RID: 1438
	private float energyConsumption = 60f;
}
