using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class DiamondPressConfig : IBuildingConfig
{
	// Token: 0x060001C1 RID: 449 RVA: 0x0000C822 File Offset: 0x0000AA22
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000C82C File Offset: 0x0000AA2C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DiamondPress";
		int num = 3;
		int num2 = 5;
		string text2 = "diamond_press_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.SelfHeatKilowattsWhenActive = 16f;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(0, 2);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("HEP_STORAGE", new CellOffset(0, 2), STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_ACTIVE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_INACTIVE, false, false) };
		return buildingDef;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000C90C File Offset: 0x0000AB0C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		complexFabricator.duplicantOperated = true;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.capacity = 2000f;
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.PORT_ID = "HEP_STORAGE";
		highEnergyParticleStorage.showCapacityStatusItem = true;
		Workable workable = go.AddOrGet<ComplexFabricatorWorkable>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		workable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_diamond_press_kanim") };
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.RefinedCarbon.CreateTag(), 100f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.Diamond.CreateTag(), 100f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		ComplexRecipe complexRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("DiamondPress", array, array2), array, array2, 1000, 0);
		complexRecipe.time = 80f;
		complexRecipe.description = string.Format(STRINGS.BUILDINGS.PREFABS.DIAMONDPRESS.REFINED_CARBON_RECIPE_DESCRIPTION, SimHashes.Diamond.CreateTag().ProperName(), SimHashes.RefinedCarbon.CreateTag().ProperName());
		complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
		complexRecipe.fabricators = new List<Tag> { TagManager.Create("DiamondPress") };
		Prioritizable.AddRef(go);
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000CA5E File Offset: 0x0000AC5E
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
			MeterController meter = new MeterController(component.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
			HighEnergyParticleStorage hepStorage = component.GetComponent<HighEnergyParticleStorage>();
			component.Subscribe(-1837862626, delegate(object data)
			{
				meter.SetPositionPercent(hepStorage.Particles / hepStorage.Capacity());
			});
			meter.SetPositionPercent(hepStorage.Particles / hepStorage.Capacity());
		};
	}

	// Token: 0x04000109 RID: 265
	public const string ID = "DiamondPress";

	// Token: 0x0400010A RID: 266
	public const string PORT_ID = "HEP_STORAGE";

	// Token: 0x0400010B RID: 267
	private const int HEP_PER_DIAMOND_KG = 10;

	// Token: 0x0400010C RID: 268
	private const int RECIPE_MASS_KG = 100;

	// Token: 0x0400010D RID: 269
	private const int HEP_STORAGE_CAPACITY = 2000;
}
