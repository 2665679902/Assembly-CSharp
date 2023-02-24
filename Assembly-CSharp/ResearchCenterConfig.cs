using System;
using TUNING;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class ResearchCenterConfig : IBuildingConfig
{
	// Token: 0x06000EA5 RID: 3749 RVA: 0x0004F494 File Offset: 0x0004D694
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ResearchCenter";
		int num = 2;
		int num2 = 2;
		string text2 = "research_center_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x0004F524 File Offset: 0x0004D724
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 1000f;
		storage.showInUI = true;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = ResearchCenterConfig.INPUT_MATERIAL;
		manualDeliveryKG.refillMass = 150f;
		manualDeliveryKG.capacity = 750f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.ResearchFetch.IdHash;
		ResearchCenter researchCenter = go.AddOrGet<ResearchCenter>();
		researchCenter.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_research_center_kanim") };
		researchCenter.research_point_type_id = "basic";
		researchCenter.inputMaterial = ResearchCenterConfig.INPUT_MATERIAL;
		researchCenter.mass_per_point = 50f;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(ResearchCenterConfig.INPUT_MATERIAL, 1.1111112f, true)
		};
		elementConverter.showDescriptors = false;
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x0004F632 File Offset: 0x0004D832
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000816 RID: 2070
	public const float BASE_SECONDS_PER_POINT = 45f;

	// Token: 0x04000817 RID: 2071
	public const float MASS_PER_POINT = 50f;

	// Token: 0x04000818 RID: 2072
	public const float BASE_MASS_PER_SECOND = 1.1111112f;

	// Token: 0x04000819 RID: 2073
	public static readonly Tag INPUT_MATERIAL = GameTags.Dirt;

	// Token: 0x0400081A RID: 2074
	public const float CAPACITY = 750f;

	// Token: 0x0400081B RID: 2075
	public const string ID = "ResearchCenter";
}
