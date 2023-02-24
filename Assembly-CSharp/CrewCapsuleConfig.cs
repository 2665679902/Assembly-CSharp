﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class CrewCapsuleConfig : IBuildingConfig
{
	// Token: 0x06000191 RID: 401 RVA: 0x0000B948 File Offset: 0x00009B48
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CrewCapsule";
		int num = 5;
		int num2 = 19;
		string text2 = "rocket_small_steam_kanim";
		int num3 = 1000;
		float num4 = 480f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER7;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.UtilityInputOffset = new CellOffset(2, 6);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 10f;
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000BA03 File Offset: 0x00009C03
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<LaunchConditionManager>();
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000BA24 File Offset: 0x00009C24
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<Storage>();
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
	}

	// Token: 0x040000E5 RID: 229
	public const string ID = "CrewCapsule";
}
