using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class EggIncubatorConfig : IBuildingConfig
{
	// Token: 0x060001DC RID: 476 RVA: 0x0000D29C File Offset: 0x0000B49C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "EggIncubator";
		int num = 2;
		int num2 = 3;
		string text2 = "incubator_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.OverheatTemperature = 363.15f;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingFront;
		return buildingDef;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000D330 File Offset: 0x0000B530
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		BuildingTemplates.CreateDefaultStorage(go, false).SetDefaultStoredItemModifiers(EggIncubatorConfig.IncubatorStorage);
		EggIncubator eggIncubator = go.AddOrGet<EggIncubator>();
		eggIncubator.AddDepositTag(GameTags.Egg);
		eggIncubator.SetWorkTime(5f);
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000D364 File Offset: 0x0000B564
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000119 RID: 281
	public const string ID = "EggIncubator";

	// Token: 0x0400011A RID: 282
	public static readonly List<Storage.StoredItemModifier> IncubatorStorage = new List<Storage.StoredItemModifier> { Storage.StoredItemModifier.Preserve };
}
