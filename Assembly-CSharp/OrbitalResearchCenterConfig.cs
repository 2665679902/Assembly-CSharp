using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000288 RID: 648
public class OrbitalResearchCenterConfig : IBuildingConfig
{
	// Token: 0x06000CEF RID: 3311 RVA: 0x00048346 File Offset: 0x00046546
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x00048350 File Offset: 0x00046550
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OrbitalResearchCenter";
		int num = 2;
		int num2 = 3;
		string text2 = "orbital_research_station_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, plastics, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x000483E0 File Offset: 0x000465E0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.RocketInteriorBuilding, false);
		go.AddOrGet<InOrbitRequired>();
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<Prioritizable>();
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.heatedTemperature = 308.15f;
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGet<ComplexFabricatorWorkable>().overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_orbital_research_station_kanim") };
		Prioritizable.AddRef(go);
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		this.ConfigureRecipes();
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x00048484 File Offset: 0x00046684
	private void ConfigureRecipes()
	{
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(OrbitalResearchCenterConfig.INPUT_MATERIAL, 5f, true)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("OrbitalResearchDatabank".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("OrbitalResearchCenter", array, array2), array, array2)
		{
			time = 33f,
			description = ITEMS.INDUSTRIAL_PRODUCTS.ORBITAL_RESEARCH_DATABANK.RECIPE_DESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
			fabricators = new List<Tag> { "OrbitalResearchCenter" }
		};
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x00048520 File Offset: 0x00046720
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000794 RID: 1940
	public const string ID = "OrbitalResearchCenter";

	// Token: 0x04000795 RID: 1941
	public const float BASE_SECONDS_PER_POINT = 33f;

	// Token: 0x04000796 RID: 1942
	public const float MASS_PER_POINT = 5f;

	// Token: 0x04000797 RID: 1943
	public static readonly Tag INPUT_MATERIAL = SimHashes.Polypropylene.CreateTag();

	// Token: 0x04000798 RID: 1944
	public const float OUTPUT_TEMPERATURE = 308.15f;
}
