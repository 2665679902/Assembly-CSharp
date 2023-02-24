using System;
using TUNING;
using UnityEngine;

// Token: 0x0200034A RID: 842
public class WashBasinConfig : IBuildingConfig
{
	// Token: 0x060010F6 RID: 4342 RVA: 0x0005BC04 File Offset: 0x00059E04
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WashBasin";
		int num = 2;
		int num2 = 3;
		string text2 = "wash_basin_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] array = raw_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		return BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, tier2, 0.2f);
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x0005BC4C File Offset: 0x00059E4C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.WashStation, false);
		HandSanitizer handSanitizer = go.AddOrGet<HandSanitizer>();
		handSanitizer.massConsumedPerUse = 5f;
		handSanitizer.consumedElement = SimHashes.Water;
		handSanitizer.outputElement = SimHashes.DirtyWater;
		handSanitizer.diseaseRemovalCount = 120000;
		handSanitizer.maxUses = 40;
		handSanitizer.dumpWhenFull = true;
		go.AddOrGet<DirectionControl>();
		HandSanitizer.Work work = go.AddOrGet<HandSanitizer.Work>();
		work.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_washbasin_kanim") };
		work.workTime = 5f;
		work.trackUses = true;
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = GameTagExtensions.Create(SimHashes.Water);
		manualDeliveryKG.MinimumMass = 5f;
		manualDeliveryKG.capacity = 200f;
		manualDeliveryKG.refillMass = 40f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		go.AddOrGet<LoopingSounds>();
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0005BD58 File Offset: 0x00059F58
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400092A RID: 2346
	public const string ID = "WashBasin";

	// Token: 0x0400092B RID: 2347
	public const int DISEASE_REMOVAL_COUNT = 120000;

	// Token: 0x0400092C RID: 2348
	public const float WATER_PER_USE = 5f;

	// Token: 0x0400092D RID: 2349
	public const int USES_PER_FLUSH = 40;

	// Token: 0x0400092E RID: 2350
	public const float WORK_TIME = 5f;
}
