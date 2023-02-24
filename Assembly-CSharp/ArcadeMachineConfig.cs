using System;
using TUNING;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class ArcadeMachineConfig : IBuildingConfig
{
	// Token: 0x06000071 RID: 113 RVA: 0x00004F40 File Offset: 0x00003140
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ArcadeMachine";
		int num = 3;
		int num2 = 3;
		string text2 = "arcade_cabinet_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 1200f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		return buildingDef;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00004FC8 File Offset: 0x000031C8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		go.AddOrGet<ArcadeMachine>();
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06000073 RID: 115 RVA: 0x0000501A File Offset: 0x0000321A
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000059 RID: 89
	public const string ID = "ArcadeMachine";

	// Token: 0x0400005A RID: 90
	public const string SPECIFIC_EFFECT = "PlayedArcade";

	// Token: 0x0400005B RID: 91
	public const string TRACKING_EFFECT = "RecentlyPlayedArcade";
}
