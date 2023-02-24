using System;
using TUNING;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class WashSinkConfig : IBuildingConfig
{
	// Token: 0x060010FA RID: 4346 RVA: 0x0005BD64 File Offset: 0x00059F64
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WashSink";
		int num = 2;
		int num2 = 3;
		string text2 = "wash_sink_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, tier2, 0.2f);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(1, 1);
		return buildingDef;
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x0005BDE8 File Offset: 0x00059FE8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.WashStation, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.AdvancedWashStation, false);
		HandSanitizer handSanitizer = go.AddOrGet<HandSanitizer>();
		handSanitizer.massConsumedPerUse = 5f;
		handSanitizer.consumedElement = SimHashes.Water;
		handSanitizer.outputElement = SimHashes.DirtyWater;
		handSanitizer.diseaseRemovalCount = 120000;
		handSanitizer.maxUses = 2;
		handSanitizer.dirtyMeterOffset = Meter.Offset.Behind;
		go.AddOrGet<DirectionControl>();
		HandSanitizer.Work work = go.AddOrGet<HandSanitizer.Work>();
		work.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_washbasin_kanim") };
		work.workTime = 5f;
		work.trackUses = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.invertElementFilter = true;
		conduitDispenser.elementFilter = new SimHashes[] { SimHashes.Water };
		Storage storage = go.AddOrGet<Storage>();
		storage.doDiseaseTransfer = false;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<RequireOutputs>().ignoreFullPipe = true;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0005BF1F File Offset: 0x0005A11F
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400092F RID: 2351
	public const string ID = "WashSink";

	// Token: 0x04000930 RID: 2352
	public const int DISEASE_REMOVAL_COUNT = 120000;

	// Token: 0x04000931 RID: 2353
	public const float WATER_PER_USE = 5f;

	// Token: 0x04000932 RID: 2354
	public const int USES_PER_FLUSH = 2;

	// Token: 0x04000933 RID: 2355
	public const float WORK_TIME = 5f;
}
