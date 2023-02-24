using System;
using TUNING;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class DiningTableConfig : IBuildingConfig
{
	// Token: 0x060001C6 RID: 454 RVA: 0x0000CA9C File Offset: 0x0000AC9C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DiningTable";
		int num = 1;
		int num2 = 1;
		string text2 = "diningtable_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.WorkTime = 20f;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000CAFF File Offset: 0x0000ACFF
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.MessTable, false);
		go.AddOrGet<MessStation>();
		go.AddOrGet<AnimTileable>();
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000CB30 File Offset: 0x0000AD30
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KAnimControllerBase>().initialAnim = "off";
		go.AddOrGet<Ownable>().slotID = Db.Get().AssignableSlots.MessStation.Id;
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.showInUI = true;
		storage.capacityKg = TableSaltTuning.SALTSHAKERSTORAGEMASS;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = TableSaltConfig.ID.ToTag();
		manualDeliveryKG.capacity = TableSaltTuning.SALTSHAKERSTORAGEMASS;
		manualDeliveryKG.refillMass = TableSaltTuning.CONSUMABLE_RATE;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FoodFetch.IdHash;
		manualDeliveryKG.ShowStatusItem = false;
	}

	// Token: 0x0400010E RID: 270
	public const string ID = "DiningTable";
}
