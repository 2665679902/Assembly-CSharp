﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class ClusterTelescopeEnclosedConfig : IBuildingConfig
{
	// Token: 0x06000118 RID: 280 RVA: 0x00008762 File Offset: 0x00006962
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000876C File Offset: 0x0000696C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ClusterTelescopeEnclosed";
		int num = 4;
		int num2 = 6;
		string text2 = "telescope_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00008810 File Offset: 0x00006A10
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		go.AddOrGetDef<PoweredController.Def>();
		ClusterTelescope.Def def = go.AddOrGetDef<ClusterTelescope.Def>();
		def.clearScanCellRadius = 6;
		def.analyzeClusterRadius = 4;
		def.workableOverrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_telescope_kanim") };
		def.providesOxygen = true;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 1000f;
		storage.showInUI = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.forceAlwaysSatisfied = true;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x000088DD File Offset: 0x00006ADD
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000099 RID: 153
	public const string ID = "ClusterTelescopeEnclosed";
}
