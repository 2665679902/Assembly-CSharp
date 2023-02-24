using System;
using TUNING;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class CreatureDeliveryPointConfig : IBuildingConfig
{
	// Token: 0x06000182 RID: 386 RVA: 0x0000B59C File Offset: 0x0000979C
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("CreatureDeliveryPoint", 1, 3, "relocator_dropoff_kanim", 10, 10f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER1, MATERIALS.RAW_METALS, 1600f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		return buildingDef;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000B5F8 File Offset: 0x000097F8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.CreatureRelocator, false);
		Storage storage = go.AddOrGet<Storage>();
		storage.allowItemRemoval = false;
		storage.showDescriptor = true;
		storage.storageFilters = STORAGEFILTERS.BAGABLE_CREATURES;
		storage.workAnims = new HashedString[] { "place", "release" };
		storage.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_restrain_creature_kanim") };
		storage.workAnimPlayMode = KAnim.PlayMode.Once;
		storage.synchronizeAnims = false;
		storage.useGunForDelivery = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		go.AddOrGet<CreatureDeliveryPoint>();
		go.AddOrGet<TreeFilterable>();
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000B6AC File Offset: 0x000098AC
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<FixedCapturePoint.Def>();
	}

	// Token: 0x040000E1 RID: 225
	public const string ID = "CreatureDeliveryPoint";
}
