using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000283 RID: 643
public class NuclearResearchCenterConfig : IBuildingConfig
{
	// Token: 0x06000CD7 RID: 3287 RVA: 0x0004798F File Offset: 0x00045B8F
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x00047998 File Offset: 0x00045B98
	public override BuildingDef CreateBuildingDef()
	{
		string text = "NuclearResearchCenter";
		int num = 5;
		int num2 = 3;
		string text2 = "material_research_centre_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(-2, 1);
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.Deprecated = !Sim.IsRadiationEnabled();
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.RadiationIDs, "NuclearResearchCenter");
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("HEP_STORAGE", new CellOffset(2, 2), STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_ACTIVE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_INACTIVE, false, false) };
		return buildingDef;
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x00047AA0 File Offset: 0x00045CA0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.capacity = 100f;
		highEnergyParticleStorage.PORT_ID = "HEP_STORAGE";
		highEnergyParticleStorage.showCapacityStatusItem = true;
		NuclearResearchCenterWorkable nuclearResearchCenterWorkable = go.AddOrGet<NuclearResearchCenterWorkable>();
		nuclearResearchCenterWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_material_research_centre_kanim") };
		nuclearResearchCenterWorkable.requiredSkillPerk = Db.Get().SkillPerks.AllowNuclearResearch.Id;
		NuclearResearchCenter nuclearResearchCenter = go.AddOrGet<NuclearResearchCenter>();
		nuclearResearchCenter.researchTypeID = "nuclear";
		nuclearResearchCenter.materialPerPoint = 10f;
		nuclearResearchCenter.timePerPoint = 100f;
		nuclearResearchCenter.inputMaterial = NuclearResearchCenterConfig.INPUT_MATERIAL;
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x00047B70 File Offset: 0x00045D70
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400077A RID: 1914
	public const string ID = "NuclearResearchCenter";

	// Token: 0x0400077B RID: 1915
	public const string PORT_ID = "HEP_STORAGE";

	// Token: 0x0400077C RID: 1916
	public const float BASE_TIME_PER_POINT = 100f;

	// Token: 0x0400077D RID: 1917
	public const float PARTICLES_PER_POINT = 10f;

	// Token: 0x0400077E RID: 1918
	public const float CAPACITY = 100f;

	// Token: 0x0400077F RID: 1919
	public static readonly Tag INPUT_MATERIAL = GameTags.HighEnergyParticle;
}
