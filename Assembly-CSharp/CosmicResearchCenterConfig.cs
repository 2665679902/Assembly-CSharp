using System;
using TUNING;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class CosmicResearchCenterConfig : IBuildingConfig
{
	// Token: 0x06000176 RID: 374 RVA: 0x0000AEA4 File Offset: 0x000090A4
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000AEAC File Offset: 0x000090AC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CosmicResearchCenter";
		int num = 4;
		int num2 = 4;
		string text2 = "research_space_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0000AF3C File Offset: 0x0000913C
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
		manualDeliveryKG.RequestedItemTag = CosmicResearchCenterConfig.INPUT_MATERIAL;
		manualDeliveryKG.refillMass = 3f;
		manualDeliveryKG.capacity = 300f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.ResearchFetch.IdHash;
		ResearchCenter researchCenter = go.AddOrGet<ResearchCenter>();
		researchCenter.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_research_space_kanim") };
		researchCenter.research_point_type_id = "space";
		researchCenter.inputMaterial = CosmicResearchCenterConfig.INPUT_MATERIAL;
		researchCenter.mass_per_point = 1f;
		researchCenter.requiredSkillPerk = Db.Get().SkillPerks.AllowInterstellarResearch.Id;
		researchCenter.workLayer = Grid.SceneLayer.BuildingFront;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(CosmicResearchCenterConfig.INPUT_MATERIAL, 0.02f, true)
		};
		elementConverter.showDescriptors = false;
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x06000179 RID: 377 RVA: 0x0000B06C File Offset: 0x0000926C
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040000DA RID: 218
	public const string ID = "CosmicResearchCenter";

	// Token: 0x040000DB RID: 219
	public const float BASE_SECONDS_PER_POINT = 50f;

	// Token: 0x040000DC RID: 220
	public const float MASS_PER_POINT = 1f;

	// Token: 0x040000DD RID: 221
	public const float BASE_MASS_PER_SECOND = 0.02f;

	// Token: 0x040000DE RID: 222
	public const float CAPACITY = 300f;

	// Token: 0x040000DF RID: 223
	public static readonly Tag INPUT_MATERIAL = ResearchDatabankConfig.TAG;
}
