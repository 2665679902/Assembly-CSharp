using System;
using TUNING;
using UnityEngine;

// Token: 0x020001BA RID: 442
public class HandSanitizerConfig : IBuildingConfig
{
	// Token: 0x060008B0 RID: 2224 RVA: 0x00033774 File Offset: 0x00031974
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HandSanitizer";
		int num = 1;
		int num2 = 3;
		string text2 = "handsanitizer_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] array = new string[] { "Metal" };
		float[] array2 = new float[] { BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0] };
		string[] array3 = array;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array2, array3, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		SoundEventVolumeCache.instance.AddVolume("handsanitizer_kanim", "HandSanitizer_tongue_out", NOISE_POLLUTION.NOISY.TIER0);
		SoundEventVolumeCache.instance.AddVolume("handsanitizer_kanim", "HandSanitizer_tongue_in", NOISE_POLLUTION.NOISY.TIER0);
		SoundEventVolumeCache.instance.AddVolume("handsanitizer_kanim", "HandSanitizer_tongue_slurp", NOISE_POLLUTION.NOISY.TIER0);
		return buildingDef;
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0003381C File Offset: 0x00031A1C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.WashStation, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.AdvancedWashStation, false);
		HandSanitizer handSanitizer = go.AddOrGet<HandSanitizer>();
		handSanitizer.massConsumedPerUse = 0.07f;
		handSanitizer.consumedElement = SimHashes.BleachStone;
		handSanitizer.diseaseRemovalCount = 480000;
		HandSanitizer.Work work = go.AddOrGet<HandSanitizer.Work>();
		work.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_handsanitizer_kanim") };
		work.workTime = 1.8f;
		work.trackUses = true;
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		go.AddOrGet<DirectionControl>();
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = GameTagExtensions.Create(SimHashes.BleachStone);
		manualDeliveryKG.capacity = 15f;
		manualDeliveryKG.refillMass = 3f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		manualDeliveryKG.operationalRequirement = Operational.State.Functional;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0003391B File Offset: 0x00031B1B
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000574 RID: 1396
	public const string ID = "HandSanitizer";

	// Token: 0x04000575 RID: 1397
	private const float STORAGE_SIZE = 15f;

	// Token: 0x04000576 RID: 1398
	private const float MASS_PER_USE = 0.07f;

	// Token: 0x04000577 RID: 1399
	private const int DISEASE_REMOVAL_COUNT = 480000;

	// Token: 0x04000578 RID: 1400
	private const float WORK_TIME = 1.8f;

	// Token: 0x04000579 RID: 1401
	private const SimHashes CONSUMED_ELEMENT = SimHashes.BleachStone;
}
