using System;
using TUNING;
using UnityEngine;

// Token: 0x02000293 RID: 659
public class OuthouseConfig : IBuildingConfig
{
	// Token: 0x06000D20 RID: 3360 RVA: 0x00048BF4 File Offset: 0x00046DF4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Outhouse";
		int num = 2;
		int num2 = 3;
		string text2 = "outhouse_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER4, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.ExhaustKilowattsWhenActive = 0.25f;
		buildingDef.DiseaseCellVisName = "FoodPoisoning";
		buildingDef.AudioCategory = "Metal";
		SoundEventVolumeCache.instance.AddVolume("outhouse_kanim", "Latrine_door_open", NOISE_POLLUTION.NOISY.TIER1);
		SoundEventVolumeCache.instance.AddVolume("outhouse_kanim", "Latrine_door_close", NOISE_POLLUTION.NOISY.TIER1);
		return buildingDef;
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x00048C94 File Offset: 0x00046E94
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ToiletType, false);
		Toilet toilet = go.AddOrGet<Toilet>();
		toilet.maxFlushes = 15;
		toilet.dirtUsedPerFlush = 13f;
		toilet.solidWastePerUse = new Toilet.SpawnInfo(SimHashes.ToxicSand, 6.7f, 0f);
		toilet.solidWasteTemperature = 310.15f;
		toilet.diseaseId = "FoodPoisoning";
		toilet.diseasePerFlush = 100000;
		toilet.diseaseOnDupePerFlush = 100000;
		KAnimFile[] array = new KAnimFile[] { Assets.GetAnim("anim_interacts_outhouse_kanim") };
		ToiletWorkableUse toiletWorkableUse = go.AddOrGet<ToiletWorkableUse>();
		toiletWorkableUse.overrideAnims = array;
		toiletWorkableUse.workLayer = Grid.SceneLayer.BuildingFront;
		ToiletWorkableClean toiletWorkableClean = go.AddOrGet<ToiletWorkableClean>();
		toiletWorkableClean.workTime = 90f;
		toiletWorkableClean.overrideAnims = array;
		toiletWorkableClean.workLayer = Grid.SceneLayer.BuildingFront;
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.showInUI = true;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = new Tag("Dirt");
		manualDeliveryKG.capacity = 200f;
		manualDeliveryKG.refillMass = 0.01f;
		manualDeliveryKG.MinimumMass = 200f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		manualDeliveryKG.operationalRequirement = Operational.State.Functional;
		Ownable ownable = go.AddOrGet<Ownable>();
		ownable.slotID = Db.Get().AssignableSlots.Toilet.Id;
		ownable.canBePublic = true;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x00048E0D File Offset: 0x0004700D
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400079D RID: 1949
	public const string ID = "Outhouse";

	// Token: 0x0400079E RID: 1950
	private const int USES_PER_REFILL = 15;

	// Token: 0x0400079F RID: 1951
	private const float DIRT_PER_REFILL = 200f;

	// Token: 0x040007A0 RID: 1952
	private const float DIRT_PER_USE = 13f;
}
