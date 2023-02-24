﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class ResearchModuleConfig : IBuildingConfig
{
	// Token: 0x06000EAA RID: 3754 RVA: 0x0004F648 File Offset: 0x0004D848
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x0004F650 File Offset: 0x0004D850
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ResearchModule";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_research_module_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] command_MODULE_MASS = BUILDINGS.ROCKETRY_MASS_KG.COMMAND_MODULE_MASS;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, command_MODULE_MASS, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x0004F700 File Offset: 0x0004D900
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<ResearchModule>();
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x0004F76B File Offset: 0x0004D96B
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_research_module_bg_kanim", false);
	}

	// Token: 0x0400081C RID: 2076
	public const string ID = "ResearchModule";
}
