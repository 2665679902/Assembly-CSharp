using System;
using TUNING;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public class LeadSuitLockerConfig : IBuildingConfig
{
	// Token: 0x0600098E RID: 2446 RVA: 0x000373E5 File Offset: 0x000355E5
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x000373EC File Offset: 0x000355EC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LeadSuitLocker";
		int num = 2;
		int num2 = 4;
		string text2 = "changingarea_radiation_kanim";
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
		buildingDef.UtilityInputOffset = new CellOffset(0, 2);
		buildingDef.Deprecated = !Sim.IsRadiationEnabled();
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "LeadSuitLocker");
		return buildingDef;
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x00037494 File Offset: 0x00035694
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<SuitLocker>().OutfitTags = new Tag[] { GameTags.LeadSuit };
		go.AddOrGet<LeadSuitLocker>();
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.capacityKG = 80f;
		go.AddOrGet<AnimTileable>().tags = new Tag[]
		{
			new Tag("LeadSuitLocker"),
			new Tag("LeadSuitMarker")
		};
		go.AddOrGet<Storage>();
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0003754A File Offset: 0x0003574A
	public override void DoPostConfigureComplete(GameObject go)
	{
		SymbolOverrideControllerUtil.AddToPrefab(go);
	}

	// Token: 0x040005EF RID: 1519
	public const string ID = "LeadSuitLocker";
}
