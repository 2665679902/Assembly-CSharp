using System;
using TUNING;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class DLC1CosmicResearchCenterConfig : IBuildingConfig
{
	// Token: 0x06000199 RID: 409 RVA: 0x0000BB40 File Offset: 0x00009D40
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000BB48 File Offset: 0x00009D48
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DLC1CosmicResearchCenter";
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

	// Token: 0x0600019B RID: 411 RVA: 0x0000BBD8 File Offset: 0x00009DD8
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
		manualDeliveryKG.RequestedItemTag = DLC1CosmicResearchCenterConfig.INPUT_MATERIAL;
		manualDeliveryKG.refillMass = 3f;
		manualDeliveryKG.capacity = 300f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.ResearchFetch.IdHash;
		ResearchCenter researchCenter = go.AddOrGet<ResearchCenter>();
		researchCenter.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_research_space_kanim") };
		researchCenter.research_point_type_id = "orbital";
		researchCenter.inputMaterial = DLC1CosmicResearchCenterConfig.INPUT_MATERIAL;
		researchCenter.mass_per_point = 1f;
		researchCenter.requiredSkillPerk = Db.Get().SkillPerks.AllowOrbitalResearch.Id;
		researchCenter.workLayer = Grid.SceneLayer.BuildingFront;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(DLC1CosmicResearchCenterConfig.INPUT_MATERIAL, 0.02f, true)
		};
		elementConverter.showDescriptors = false;
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000BD08 File Offset: 0x00009F08
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040000E7 RID: 231
	public const string ID = "DLC1CosmicResearchCenter";

	// Token: 0x040000E8 RID: 232
	public const float BASE_SECONDS_PER_POINT = 50f;

	// Token: 0x040000E9 RID: 233
	public const float MASS_PER_POINT = 1f;

	// Token: 0x040000EA RID: 234
	public const float BASE_MASS_PER_SECOND = 0.02f;

	// Token: 0x040000EB RID: 235
	public const float CAPACITY = 300f;

	// Token: 0x040000EC RID: 236
	public static readonly Tag INPUT_MATERIAL = OrbitalResearchDatabankConfig.TAG;
}
