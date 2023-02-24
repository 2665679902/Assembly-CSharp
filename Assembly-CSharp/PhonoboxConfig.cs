﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class PhonoboxConfig : IBuildingConfig
{
	// Token: 0x06000D5F RID: 3423 RVA: 0x0004A51C File Offset: 0x0004871C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Phonobox";
		int num = 5;
		int num2 = 3;
		string text2 = "jukebot_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 960f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		return buildingDef;
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0004A5A4 File Offset: 0x000487A4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		go.AddOrGet<Phonobox>();
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0004A5FD File Offset: 0x000487FD
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007C9 RID: 1993
	public const string ID = "Phonobox";
}
