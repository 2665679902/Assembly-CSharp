﻿using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class ArtifactCargoBayConfig : IBuildingConfig
{
	// Token: 0x0600007A RID: 122 RVA: 0x00005147 File Offset: 0x00003347
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00005150 File Offset: 0x00003350
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ArtifactCargoBay";
		int num = 3;
		int num2 = 1;
		string text2 = "artifact_transport_module_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] hollow_TIER = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER1;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.Invincible = true;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000051FC File Offset: 0x000033FC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 1), GameTags.Rocket, null)
		};
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00005260 File Offset: 0x00003460
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MAJOR, 0f, 0f);
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>(new Storage.StoredItemModifier[]
		{
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Preserve
		}));
		Prioritizable.AddRef(go);
		ArtifactModule artifactModule = go.AddOrGet<ArtifactModule>();
		artifactModule.AddDepositTag(GameTags.PedestalDisplayable);
		artifactModule.occupyingObjectRelativePosition = new Vector3(0f, 0.5f, -1f);
		go.AddOrGet<DecorProvider>();
		go.AddOrGet<ItemPedestal>();
		go.AddOrGetDef<ArtifactHarvestModule.Def>();
	}

	// Token: 0x0400005E RID: 94
	public const string ID = "ArtifactCargoBay";
}
