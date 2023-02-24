using System;
using TUNING;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class AdvancedApothecaryConfig : IBuildingConfig
{
	// Token: 0x0600004A RID: 74 RVA: 0x00003DA4 File Offset: 0x00001FA4
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003DAC File Offset: 0x00001FAC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "AdvancedApothecary";
		int num = 3;
		int num2 = 3;
		string text2 = "medicine_nuclear_kanim";
		int num3 = 250;
		float num4 = 240f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(0, 2);
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003E48 File Offset: 0x00002048
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.capacity = 400f;
		highEnergyParticleStorage.showCapacityStatusItem = true;
		go.AddOrGet<HighEnergyParticlePort>().requireOperational = false;
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		AdvancedApothecary advancedApothecary = go.AddOrGet<AdvancedApothecary>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, advancedApothecary);
		go.AddOrGet<ComplexFabricatorWorkable>();
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		ActiveParticleConsumer.Def def = go.AddOrGetDef<ActiveParticleConsumer.Def>();
		def.activeConsumptionRate = 1f;
		def.minParticlesForOperational = 1f;
		def.meterSymbolName = null;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003EDD File Offset: 0x000020DD
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x04000038 RID: 56
	public const string ID = "AdvancedApothecary";

	// Token: 0x04000039 RID: 57
	public const float PARTICLE_CAPACITY = 400f;
}
