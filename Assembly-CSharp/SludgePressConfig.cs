using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class SludgePressConfig : IBuildingConfig
{
	// Token: 0x06000F6E RID: 3950 RVA: 0x00053F0E File Offset: 0x0005210E
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00053F18 File Offset: 0x00052118
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SludgePress";
		int num = 4;
		int num2 = 3;
		string text2 = "sludge_press_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_MINERALS = MATERIALS.ALL_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_MINERALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER1, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x00053FB0 File Offset: 0x000521B0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		complexFabricator.duplicantOperated = true;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		ComplexFabricatorWorkable complexFabricatorWorkable = go.AddOrGet<ComplexFabricatorWorkable>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		complexFabricatorWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_sludge_press_kanim") };
		complexFabricatorWorkable.workingPstComplete = new HashedString[] { "working_pst_complete" };
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.alwaysDispense = true;
		conduitDispenser.elementFilter = null;
		conduitDispenser.storage = go.GetComponent<ComplexFabricator>().outStorage;
		this.AddRecipes(go);
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00054074 File Offset: 0x00052274
	private void AddRecipes(GameObject go)
	{
		float num = 150f;
		foreach (Element element in ElementLoader.elements.FindAll((Element e) => e.elementComposition != null))
		{
			ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement(element.tag, num)
			};
			ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[element.elementComposition.Length];
			for (int i = 0; i < element.elementComposition.Length; i++)
			{
				ElementLoader.ElementComposition elementComposition = element.elementComposition[i];
				Element element2 = ElementLoader.FindElementByName(elementComposition.elementID);
				bool isLiquid = element2.IsLiquid;
				array2[i] = new ComplexRecipe.RecipeElement(element2.tag, num * elementComposition.percentage, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, isLiquid);
			}
			string text = ComplexRecipeManager.MakeObsoleteRecipeID("SludgePress", element.tag);
			string text2 = ComplexRecipeManager.MakeRecipeID("SludgePress", array, array2);
			ComplexRecipe complexRecipe = new ComplexRecipe(text2, array, array2);
			complexRecipe.time = 20f;
			complexRecipe.description = string.Format(STRINGS.BUILDINGS.PREFABS.SLUDGEPRESS.RECIPE_DESCRIPTION, element.name);
			complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Composite;
			complexRecipe.fabricators = new List<Tag> { TagManager.Create("SludgePress") };
			ComplexRecipeManager.Get().AddObsoleteIDMapping(text, text2);
		}
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x000541F8 File Offset: 0x000523F8
	public override void DoPostConfigureComplete(GameObject go)
	{
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			ComplexFabricatorWorkable component = game_object.GetComponent<ComplexFabricatorWorkable>();
			component.WorkerStatusItem = Db.Get().DuplicantStatusItems.Processing;
			component.AttributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
			component.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
			component.SkillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
			component.SkillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		};
	}

	// Token: 0x04000880 RID: 2176
	public const string ID = "SludgePress";
}
