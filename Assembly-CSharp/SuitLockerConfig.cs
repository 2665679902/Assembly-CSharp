using System;
using TUNING;
using UnityEngine;

// Token: 0x0200032F RID: 815
public class SuitLockerConfig : IBuildingConfig
{
	// Token: 0x06001044 RID: 4164 RVA: 0x000585FC File Offset: 0x000567FC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SuitLocker";
		int num = 1;
		int num2 = 3;
		string text2 = "changingarea_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float[] array = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
		};
		string[] array2 = refined_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.PreventIdleTraversalPastBuilding = true;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "SuitLocker");
		return buildingDef;
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x00058698 File Offset: 0x00056898
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<SuitLocker>().OutfitTags = new Tag[] { GameTags.AtmoSuit };
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.capacityKG = 200f;
		go.AddOrGet<AnimTileable>().tags = new Tag[]
		{
			new Tag("SuitLocker"),
			new Tag("SuitMarker")
		};
		go.AddOrGet<Storage>();
		Prioritizable.AddRef(go);
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x00058747 File Offset: 0x00056947
	public override void DoPostConfigureComplete(GameObject go)
	{
		SymbolOverrideControllerUtil.AddToPrefab(go);
	}

	// Token: 0x040008DE RID: 2270
	public const string ID = "SuitLocker";
}
