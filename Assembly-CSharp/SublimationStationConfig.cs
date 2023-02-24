using System;
using TUNING;
using UnityEngine;

// Token: 0x0200032C RID: 812
public class SublimationStationConfig : IBuildingConfig
{
	// Token: 0x06001032 RID: 4146 RVA: 0x00057805 File Offset: 0x00055A05
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x0005780C File Offset: 0x00055A0C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SublimationStation";
		int num = 2;
		int num2 = 1;
		string text2 = "sublimation_station_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.ViewMode = OverlayModes.Oxygen.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.Breakable = true;
		return buildingDef;
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x000578AC File Offset: 0x00055AAC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		CellOffset cellOffset = new CellOffset(0, 0);
		Electrolyzer electrolyzer = go.AddOrGet<Electrolyzer>();
		electrolyzer.maxMass = 1.8f;
		electrolyzer.hasMeter = false;
		electrolyzer.emissionOffset = cellOffset;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 600f;
		storage.showInUI = true;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(SimHashes.ToxicSand.CreateTag(), 1f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.66f, SimHashes.ContaminatedOxygen, 303.15f, false, false, (float)cellOffset.x, (float)cellOffset.y, 1f, byte.MaxValue, 0, true)
		};
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = SimHashes.ToxicSand.CreateTag();
		manualDeliveryKG.capacity = 600f;
		manualDeliveryKG.refillMass = 240f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x000579BD File Offset: 0x00055BBD
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x040008D4 RID: 2260
	public const string ID = "SublimationStation";

	// Token: 0x040008D5 RID: 2261
	private const float DIRT_CONSUME_RATE = 1f;

	// Token: 0x040008D6 RID: 2262
	private const float DIRT_STORAGE = 600f;

	// Token: 0x040008D7 RID: 2263
	private const float OXYGEN_GENERATION_RATE = 0.66f;

	// Token: 0x040008D8 RID: 2264
	private const float OXYGEN_TEMPERATURE = 303.15f;
}
