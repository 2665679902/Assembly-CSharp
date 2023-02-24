using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000229 RID: 553
public class ManualHighEnergyParticleSpawnerConfig : IBuildingConfig
{
	// Token: 0x06000AED RID: 2797 RVA: 0x0003D983 File Offset: 0x0003BB83
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0003D98C File Offset: 0x0003BB8C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ManualHighEnergyParticleSpawner";
		int num = 1;
		int num2 = 3;
		string text2 = "manual_radbolt_generator_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.UseHighEnergyParticleOutputPort = true;
		buildingDef.HighEnergyParticleOutputOffset = new CellOffset(0, 2);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.RadiationIDs, "ManualHighEnergyParticleSpawner");
		buildingDef.Deprecated = !Sim.IsRadiationEnabled();
		return buildingDef;
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x0003DA28 File Offset: 0x0003BC28
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		go.AddOrGet<HighEnergyParticleStorage>();
		go.AddOrGet<LoopingSounds>();
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		complexFabricator.duplicantOperated = true;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		ComplexFabricatorWorkable complexFabricatorWorkable = go.AddOrGet<ComplexFabricatorWorkable>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		complexFabricatorWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_manual_radbolt_generator_kanim") };
		complexFabricatorWorkable.workLayer = Grid.SceneLayer.BuildingUse;
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.UraniumOre.CreateTag(), 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(ManualHighEnergyParticleSpawnerConfig.WASTE_MATERIAL, 0.5f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		ComplexRecipe complexRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("ManualHighEnergyParticleSpawner", array, array2), array, array2, 0, 5);
		complexRecipe.time = 40f;
		complexRecipe.description = string.Format(STRINGS.BUILDINGS.PREFABS.MANUALHIGHENERGYPARTICLESPAWNER.RECIPE_DESCRIPTION, SimHashes.UraniumOre.CreateTag().ProperName(), ManualHighEnergyParticleSpawnerConfig.WASTE_MATERIAL.ProperName());
		complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.HEP;
		complexRecipe.fabricators = new List<Tag> { TagManager.Create("ManualHighEnergyParticleSpawner") };
		ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.EnrichedUranium.CreateTag(), 1f)
		};
		ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(ManualHighEnergyParticleSpawnerConfig.WASTE_MATERIAL, 0.8f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		ComplexRecipe complexRecipe2 = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("ManualHighEnergyParticleSpawner", array3, array4), array3, array4, 0, 25);
		complexRecipe2.time = 40f;
		complexRecipe2.description = string.Format(STRINGS.BUILDINGS.PREFABS.MANUALHIGHENERGYPARTICLESPAWNER.RECIPE_DESCRIPTION, SimHashes.EnrichedUranium.CreateTag().ProperName(), ManualHighEnergyParticleSpawnerConfig.WASTE_MATERIAL.ProperName());
		complexRecipe2.nameDisplay = ComplexRecipe.RecipeNameDisplay.HEP;
		complexRecipe2.fabricators = new List<Tag> { TagManager.Create("ManualHighEnergyParticleSpawner") };
		go.AddOrGet<ManualHighEnergyParticleSpawner>();
		RadiationEmitter radiationEmitter = go.AddComponent<RadiationEmitter>();
		radiationEmitter.emissionOffset = new Vector3(0f, 2f);
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.emitRadiusX = this.RAD_LIGHT_SIZE;
		radiationEmitter.emitRadiusY = this.RAD_LIGHT_SIZE;
		radiationEmitter.emitRads = 120f;
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0003DC55 File Offset: 0x0003BE55
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000664 RID: 1636
	public const string ID = "ManualHighEnergyParticleSpawner";

	// Token: 0x04000665 RID: 1637
	public const float MIN_LAUNCH_INTERVAL = 2f;

	// Token: 0x04000666 RID: 1638
	public const int MIN_SLIDER = 1;

	// Token: 0x04000667 RID: 1639
	public const int MAX_SLIDER = 100;

	// Token: 0x04000668 RID: 1640
	public const float RADBOLTS_PER_KG = 5f;

	// Token: 0x04000669 RID: 1641
	public const float MASS_PER_CRAFT = 1f;

	// Token: 0x0400066A RID: 1642
	public const float REFINED_BONUS = 5f;

	// Token: 0x0400066B RID: 1643
	public const int RADBOLTS_PER_CRAFT = 5;

	// Token: 0x0400066C RID: 1644
	public static readonly Tag WASTE_MATERIAL = SimHashes.DepletedUranium.CreateTag();

	// Token: 0x0400066D RID: 1645
	private const float ORE_FUEL_TO_WASTE_RATIO = 0.5f;

	// Token: 0x0400066E RID: 1646
	private const float REFINED_FUEL_TO_WASTE_RATIO = 0.8f;

	// Token: 0x0400066F RID: 1647
	private short RAD_LIGHT_SIZE = 3;
}
