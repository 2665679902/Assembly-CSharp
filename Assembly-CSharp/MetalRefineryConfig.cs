using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class MetalRefineryConfig : IBuildingConfig
{
	// Token: 0x06000B3E RID: 2878 RVA: 0x0003F748 File Offset: 0x0003D948
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MetalRefinery";
		int num = 3;
		int num2 = 4;
		string text2 = "metalrefinery_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] all_MINERALS = MATERIALS.ALL_MINERALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_MINERALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 1200f;
		buildingDef.SelfHeatKilowattsWhenActive = 16f;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.UtilityInputOffset = new CellOffset(-1, 1);
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		LiquidCooledRefinery liquidCooledRefinery = go.AddOrGet<LiquidCooledRefinery>();
		liquidCooledRefinery.duplicantOperated = true;
		liquidCooledRefinery.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		liquidCooledRefinery.keepExcessLiquids = true;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		Workable workable = go.AddOrGet<ComplexFabricatorWorkable>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, liquidCooledRefinery);
		liquidCooledRefinery.coolantTag = MetalRefineryConfig.COOLANT_TAG;
		liquidCooledRefinery.minCoolantMass = 400f;
		liquidCooledRefinery.outStorage.capacityKg = 2000f;
		liquidCooledRefinery.thermalFudge = 0.8f;
		liquidCooledRefinery.inStorage.SetDefaultStoredItemModifiers(MetalRefineryConfig.RefineryStoredItemModifiers);
		liquidCooledRefinery.buildStorage.SetDefaultStoredItemModifiers(MetalRefineryConfig.RefineryStoredItemModifiers);
		liquidCooledRefinery.outStorage.SetDefaultStoredItemModifiers(MetalRefineryConfig.RefineryStoredItemModifiers);
		liquidCooledRefinery.outputOffset = new Vector3(1f, 0.5f);
		workable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_metalrefinery_kanim") };
		go.AddOrGet<RequireOutputs>().ignoreFullPipe = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.capacityTag = GameTags.Liquid;
		conduitConsumer.capacityKG = 800f;
		conduitConsumer.storage = liquidCooledRefinery.inStorage;
		conduitConsumer.alwaysConsume = true;
		conduitConsumer.forceAlwaysSatisfied = true;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.storage = liquidCooledRefinery.outStorage;
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.elementFilter = null;
		conduitDispenser.alwaysDispense = true;
		foreach (Element element in ElementLoader.elements.FindAll((Element e) => e.IsSolid && e.HasTag(GameTags.Metal)))
		{
			if (!element.HasTag(GameTags.Noncrushable))
			{
				Element lowTempTransition = element.highTempTransition.lowTempTransition;
				if (lowTempTransition != element)
				{
					ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
					{
						new ComplexRecipe.RecipeElement(element.tag, 100f)
					};
					ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
					{
						new ComplexRecipe.RecipeElement(lowTempTransition.tag, 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
					};
					string text = ComplexRecipeManager.MakeObsoleteRecipeID("MetalRefinery", element.tag);
					string text2 = ComplexRecipeManager.MakeRecipeID("MetalRefinery", array, array2);
					ComplexRecipe complexRecipe = new ComplexRecipe(text2, array, array2);
					complexRecipe.time = 40f;
					complexRecipe.description = string.Format(STRINGS.BUILDINGS.PREFABS.METALREFINERY.RECIPE_DESCRIPTION, lowTempTransition.name, element.name);
					complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
					complexRecipe.fabricators = new List<Tag> { TagManager.Create("MetalRefinery") };
					ComplexRecipeManager.Get().AddObsoleteIDMapping(text, text2);
				}
			}
		}
		Element element2 = ElementLoader.FindElementByHash(SimHashes.Steel);
		ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Iron).tag, 70f),
			new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.RefinedCarbon).tag, 20f),
			new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Lime).tag, 10f)
		};
		ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Steel).tag, 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		string text3 = ComplexRecipeManager.MakeObsoleteRecipeID("MetalRefinery", element2.tag);
		string text4 = ComplexRecipeManager.MakeRecipeID("MetalRefinery", array3, array4);
		ComplexRecipe complexRecipe2 = new ComplexRecipe(text4, array3, array4);
		complexRecipe2.time = 40f;
		complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
		complexRecipe2.description = string.Format(STRINGS.BUILDINGS.PREFABS.METALREFINERY.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(SimHashes.Steel).name, ElementLoader.FindElementByHash(SimHashes.Iron).name);
		complexRecipe2.fabricators = new List<Tag> { TagManager.Create("MetalRefinery") };
		ComplexRecipeManager.Get().AddObsoleteIDMapping(text3, text4);
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x0003FBC0 File Offset: 0x0003DDC0
	public override void DoPostConfigureComplete(GameObject go)
	{
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddOrGetDef<PoweredActiveStoppableController.Def>();
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

	// Token: 0x040006AA RID: 1706
	public const string ID = "MetalRefinery";

	// Token: 0x040006AB RID: 1707
	private const float INPUT_KG = 100f;

	// Token: 0x040006AC RID: 1708
	private const float LIQUID_COOLED_HEAT_PORTION = 0.8f;

	// Token: 0x040006AD RID: 1709
	private static readonly Tag COOLANT_TAG = GameTags.Liquid;

	// Token: 0x040006AE RID: 1710
	private const float COOLANT_MASS = 400f;

	// Token: 0x040006AF RID: 1711
	private static readonly List<Storage.StoredItemModifier> RefineryStoredItemModifiers = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Preserve,
		Storage.StoredItemModifier.Insulate,
		Storage.StoredItemModifier.Seal
	};
}
