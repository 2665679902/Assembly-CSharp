using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class CraftingTableConfig : IBuildingConfig
{
	// Token: 0x0600017C RID: 380 RVA: 0x0000B082 File Offset: 0x00009282
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x0000B08C File Offset: 0x0000928C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CraftingTable";
		int num = 2;
		int num2 = 2;
		string text2 = "craftingStation_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PowerInputOffset = new CellOffset(1, 0);
		return buildingDef;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000B108 File Offset: 0x00009308
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<Prioritizable>();
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.heatedTemperature = 318.15f;
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGet<ComplexFabricatorWorkable>().overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_craftingstation_kanim") };
		Prioritizable.AddRef(go);
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		this.ConfigureRecipes();
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000B194 File Offset: 0x00009394
	private void ConfigureRecipes()
	{
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.Cuprite.CreateTag(), 50f, true)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Oxygen_Mask".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CraftingTable", array, array2), array, array2)
		{
			time = (float)TUNING.EQUIPMENT.SUITS.OXYMASK_FABTIME,
			description = STRINGS.EQUIPMENT.PREFABS.OXYGEN_MASK.RECIPE_DESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
			fabricators = new List<Tag> { "CraftingTable" },
			requiredTech = Db.Get().TechItems.oxygenMask.parentTechId
		};
		ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.AluminumOre.CreateTag(), 50f, true)
		};
		ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Oxygen_Mask".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CraftingTable", array3, array4), array3, array4)
		{
			time = (float)TUNING.EQUIPMENT.SUITS.OXYMASK_FABTIME,
			description = STRINGS.EQUIPMENT.PREFABS.OXYGEN_MASK.RECIPE_DESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
			fabricators = new List<Tag> { "CraftingTable" },
			requiredTech = Db.Get().TechItems.oxygenMask.parentTechId
		};
		ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.IronOre.CreateTag(), 50f, true)
		};
		ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Oxygen_Mask".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CraftingTable", array5, array6), array5, array6)
		{
			time = (float)TUNING.EQUIPMENT.SUITS.OXYMASK_FABTIME,
			description = STRINGS.EQUIPMENT.PREFABS.OXYGEN_MASK.RECIPE_DESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
			fabricators = new List<Tag> { "CraftingTable" },
			requiredTech = Db.Get().TechItems.oxygenMask.parentTechId
		};
		if (ElementLoader.FindElementByHash(SimHashes.Cobaltite) != null)
		{
			ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement(SimHashes.Cobaltite.CreateTag(), 50f, true)
			};
			ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement("Oxygen_Mask".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
			};
			AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CraftingTable", array7, array8), array7, array8)
			{
				time = (float)TUNING.EQUIPMENT.SUITS.OXYMASK_FABTIME,
				description = STRINGS.EQUIPMENT.PREFABS.OXYGEN_MASK.RECIPE_DESC,
				nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
				fabricators = new List<Tag> { "CraftingTable" },
				requiredTech = Db.Get().TechItems.oxygenMask.parentTechId
			};
		}
		ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Worn_Oxygen_Mask".ToTag(), 1f, true)
		};
		ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Oxygen_Mask".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CraftingTable", array9, array10), array9, array10)
		{
			time = (float)TUNING.EQUIPMENT.SUITS.OXYMASK_FABTIME,
			description = STRINGS.EQUIPMENT.PREFABS.OXYGEN_MASK.RECIPE_DESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
			fabricators = new List<Tag> { "CraftingTable" },
			requiredTech = Db.Get().TechItems.oxygenMask.parentTechId
		};
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000B530 File Offset: 0x00009730
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Suits, true);
		};
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			ComplexFabricatorWorkable component = game_object.GetComponent<ComplexFabricatorWorkable>();
			component.WorkerStatusItem = Db.Get().DuplicantStatusItems.Fabricating;
			component.AttributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
			component.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
			component.SkillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
			component.SkillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		};
	}

	// Token: 0x040000E0 RID: 224
	public const string ID = "CraftingTable";
}
