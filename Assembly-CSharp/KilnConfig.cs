﻿using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001DD RID: 477
public class KilnConfig : IBuildingConfig
{
	// Token: 0x06000965 RID: 2405 RVA: 0x00036874 File Offset: 0x00034A74
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Kiln";
		int num = 2;
		int num2 = 2;
		string text2 = "kiln_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER1, tier2, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.RequiresPowerInput = false;
		buildingDef.ExhaustKilowattsWhenActive = 16f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 1));
		return buildingDef;
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x000368FC File Offset: 0x00034AFC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.heatedTemperature = 353.15f;
		complexFabricator.duplicantOperated = false;
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		this.ConfgiureRecipes();
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00036970 File Offset: 0x00034B70
	private void ConfgiureRecipes()
	{
		Tag tag = SimHashes.Ceramic.CreateTag();
		Tag tag2 = SimHashes.Clay.CreateTag();
		Tag tag3 = SimHashes.Carbon.CreateTag();
		float num = 100f;
		float num2 = 25f;
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(tag2, num),
			new ComplexRecipe.RecipeElement(tag3, num2)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(tag, num, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		string text = ComplexRecipeManager.MakeObsoleteRecipeID("Kiln", tag);
		string text2 = ComplexRecipeManager.MakeRecipeID("Kiln", array, array2);
		ComplexRecipe complexRecipe = new ComplexRecipe(text2, array, array2);
		complexRecipe.time = 40f;
		complexRecipe.description = string.Format(STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(SimHashes.Clay).name, ElementLoader.FindElementByHash(SimHashes.Ceramic).name);
		complexRecipe.fabricators = new List<Tag> { TagManager.Create("Kiln") };
		complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
		ComplexRecipeManager.Get().AddObsoleteIDMapping(text, text2);
		Tag tag4 = SimHashes.RefinedCarbon.CreateTag();
		ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(tag3, num + num2)
		};
		ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(tag4, num, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		string text3 = ComplexRecipeManager.MakeObsoleteRecipeID("Kiln", tag4);
		string text4 = ComplexRecipeManager.MakeRecipeID("Kiln", array3, array4);
		ComplexRecipe complexRecipe2 = new ComplexRecipe(text4, array3, array4);
		complexRecipe2.time = 40f;
		complexRecipe2.description = string.Format(STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(SimHashes.Carbon).name, ElementLoader.FindElementByHash(SimHashes.RefinedCarbon).name);
		complexRecipe2.fabricators = new List<Tag> { TagManager.Create("Kiln") };
		complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
		ComplexRecipeManager.Get().AddObsoleteIDMapping(text3, text4);
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00036B3D File Offset: 0x00034D3D
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
		SymbolOverrideControllerUtil.AddToPrefab(go);
	}

	// Token: 0x040005DC RID: 1500
	public const string ID = "Kiln";

	// Token: 0x040005DD RID: 1501
	public const float INPUT_CLAY_PER_SECOND = 1f;

	// Token: 0x040005DE RID: 1502
	public const float CERAMIC_PER_SECOND = 1f;

	// Token: 0x040005DF RID: 1503
	public const float CO2_RATIO = 0.1f;

	// Token: 0x040005E0 RID: 1504
	public const float OUTPUT_TEMP = 353.15f;

	// Token: 0x040005E1 RID: 1505
	public const float REFILL_RATE = 2400f;

	// Token: 0x040005E2 RID: 1506
	public const float CERAMIC_STORAGE_AMOUNT = 2400f;

	// Token: 0x040005E3 RID: 1507
	public const float COAL_RATE = 0.1f;
}
