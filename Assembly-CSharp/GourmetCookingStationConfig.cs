﻿using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001AD RID: 429
public class GourmetCookingStationConfig : IBuildingConfig
{
	// Token: 0x06000856 RID: 2134 RVA: 0x000312AC File Offset: 0x0002F4AC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GourmetCookingStation";
		int num = 3;
		int num2 = 3;
		string text2 = "cookstation_gourmet_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		BuildingTemplates.CreateElectricalBuildingDef(buildingDef);
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.ExhaustKilowattsWhenActive = 1f;
		buildingDef.SelfHeatKilowattsWhenActive = 8f;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
		buildingDef.PowerInputOffset = new CellOffset(1, 0);
		return buildingDef;
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x00031350 File Offset: 0x0002F550
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		GourmetCookingStation gourmetCookingStation = go.AddOrGet<GourmetCookingStation>();
		gourmetCookingStation.heatedTemperature = 368.15f;
		gourmetCookingStation.duplicantOperated = true;
		gourmetCookingStation.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGet<ComplexFabricatorWorkable>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, gourmetCookingStation);
		gourmetCookingStation.fuelTag = this.FUEL_TAG;
		gourmetCookingStation.outStorage.capacityKg = 10f;
		gourmetCookingStation.inStorage.SetDefaultStoredItemModifiers(GourmetCookingStationConfig.GourmetCookingStationStoredItemModifiers);
		gourmetCookingStation.buildStorage.SetDefaultStoredItemModifiers(GourmetCookingStationConfig.GourmetCookingStationStoredItemModifiers);
		gourmetCookingStation.outStorage.SetDefaultStoredItemModifiers(GourmetCookingStationConfig.GourmetCookingStationStoredItemModifiers);
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.capacityTag = this.FUEL_TAG;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.alwaysConsume = true;
		conduitConsumer.storage = gourmetCookingStation.inStorage;
		conduitConsumer.forceAlwaysSatisfied = true;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(this.FUEL_TAG, 0.1f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.025f, SimHashes.CarbonDioxide, 348.15f, false, false, 0f, 2f, 1f, byte.MaxValue, 0, true)
		};
		this.ConfigureRecipes();
		Prioritizable.AddRef(go);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.CookTop, false);
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x000314B5 File Offset: 0x0002F6B5
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<PoweredActiveStoppableController.Def>();
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			ComplexFabricatorWorkable component = game_object.GetComponent<ComplexFabricatorWorkable>();
			component.AttributeConverter = Db.Get().AttributeConverters.CookingSpeed;
			component.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
			component.SkillExperienceSkillGroup = Db.Get().SkillGroups.Cooking.Id;
			component.SkillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		};
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x000314E8 File Offset: 0x0002F6E8
	private void ConfigureRecipes()
	{
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("GrilledPrickleFruit", 2f),
			new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 2f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Salsa", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		SalsaConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array, array2), array, array2)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = ITEMS.FOOD.SALSA.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "GourmetCookingStation" },
			sortOrder = 300
		};
		ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("FriedMushroom", 1f),
			new ComplexRecipe.RecipeElement("Lettuce", 4f)
		};
		ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("MushroomWrap", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		MushroomWrapConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array3, array4), array3, array4)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = ITEMS.FOOD.MUSHROOMWRAP.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "GourmetCookingStation" },
			sortOrder = 400
		};
		ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("CookedMeat", 1f),
			new ComplexRecipe.RecipeElement("CookedFish", 1f)
		};
		ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("SurfAndTurf", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		SurfAndTurfConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array5, array6), array5, array6)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = ITEMS.FOOD.SURFANDTURF.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "GourmetCookingStation" },
			sortOrder = 500
		};
		ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("ColdWheatSeed", 10f),
			new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 1f)
		};
		ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("SpiceBread", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		SpiceBreadConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array7, array8), array7, array8)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = ITEMS.FOOD.SPICEBREAD.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "GourmetCookingStation" },
			sortOrder = 600
		};
		ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Tofu", 1f),
			new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 1f)
		};
		ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("SpicyTofu", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		SpicyTofuConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array9, array10), array9, array10)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = ITEMS.FOOD.SPICYTOFU.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "GourmetCookingStation" },
			sortOrder = 800
		};
		ComplexRecipe.RecipeElement[] array11 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(GingerConfig.ID, 4f),
			new ComplexRecipe.RecipeElement("BeanPlantSeed", 4f)
		};
		ComplexRecipe.RecipeElement[] array12 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Curry", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		SpicyTofuConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array11, array12), array11, array12)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = ITEMS.FOOD.CURRY.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "GourmetCookingStation" },
			sortOrder = 800
		};
		ComplexRecipe.RecipeElement[] array13 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("ColdWheatBread", 1f),
			new ComplexRecipe.RecipeElement("Lettuce", 1f),
			new ComplexRecipe.RecipeElement("CookedMeat", 1f)
		};
		ComplexRecipe.RecipeElement[] array14 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Burger", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		BurgerConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array13, array14), array13, array14)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = ITEMS.FOOD.BURGER.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "GourmetCookingStation" },
			sortOrder = 900
		};
		if (DlcManager.IsExpansion1Active())
		{
			ComplexRecipe.RecipeElement[] array15 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement("ColdWheatSeed", 3f),
				new ComplexRecipe.RecipeElement("WormSuperFruit", 4f),
				new ComplexRecipe.RecipeElement("GrilledPrickleFruit", 1f)
			};
			ComplexRecipe.RecipeElement[] array16 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement("BerryPie", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
			};
			BerryPieConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", array15, array16), array15, array16)
			{
				time = FOOD.RECIPES.STANDARD_COOK_TIME,
				description = ITEMS.FOOD.BERRYPIE.RECIPEDESC,
				nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
				fabricators = new List<Tag> { "GourmetCookingStation" },
				sortOrder = 900
			};
		}
	}

	// Token: 0x04000540 RID: 1344
	public const string ID = "GourmetCookingStation";

	// Token: 0x04000541 RID: 1345
	private const float FUEL_STORE_CAPACITY = 10f;

	// Token: 0x04000542 RID: 1346
	private const float FUEL_CONSUME_RATE = 0.1f;

	// Token: 0x04000543 RID: 1347
	private const float CO2_EMIT_RATE = 0.025f;

	// Token: 0x04000544 RID: 1348
	private Tag FUEL_TAG = new Tag("Methane");

	// Token: 0x04000545 RID: 1349
	private static readonly List<Storage.StoredItemModifier> GourmetCookingStationStoredItemModifiers = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Preserve,
		Storage.StoredItemModifier.Insulate,
		Storage.StoredItemModifier.Seal
	};
}
