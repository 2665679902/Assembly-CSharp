﻿using System;
using Database;
using TUNING;
using UnityEngine;

// Token: 0x0200027D RID: 637
public class MonumentMiddleConfig : IBuildingConfig
{
	// Token: 0x06000CB6 RID: 3254 RVA: 0x00046ED8 File Offset: 0x000450D8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MonumentMiddle";
		int num = 5;
		int num2 = 5;
		string text2 = "monument_mid_a_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] array = new float[] { 2500f, 2500f, 5000f };
		string[] array2 = new string[]
		{
			SimHashes.Ceramic.ToString(),
			SimHashes.Polypropylene.ToString(),
			SimHashes.Steel.ToString()
		};
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.MONUMENT.INCOMPLETE, tier, 0.2f);
		BuildingTemplates.CreateMonumentBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = "MonumentMiddle";
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = false;
		return buildingDef;
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00046FCC File Offset: 0x000451CC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), "MonumentTop", null)
		};
		go.AddOrGet<MonumentPart>().part = MonumentPartResource.Part.Middle;
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x00047030 File Offset: 0x00045230
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x00047032 File Offset: 0x00045232
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x00047034 File Offset: 0x00045234
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<KBatchedAnimController>().initialAnim = "option_a";
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			MonumentPart monumentPart = game_object.AddOrGet<MonumentPart>();
			monumentPart.part = MonumentPartResource.Part.Middle;
			monumentPart.stateUISymbol = "mid";
		};
	}

	// Token: 0x0400075A RID: 1882
	public const string ID = "MonumentMiddle";
}
