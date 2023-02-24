using System;
using TUNING;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class BeachChairConfig : IBuildingConfig
{
	// Token: 0x060000B2 RID: 178 RVA: 0x0000634C File Offset: 0x0000454C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BeachChair";
		int num = 2;
		int num2 = 3;
		string text2 = "beach_chair_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] array = new float[] { 400f, 2f };
		string[] array2 = new string[] { "BuildableRaw", "BuildingFiber" };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER4, none, 0.2f);
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		return buildingDef;
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x000063D0 File Offset: 0x000045D0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		go.AddOrGet<BeachChairWorkable>().basePriority = RELAXATION.PRIORITY.TIER4;
		BeachChair beachChair = go.AddOrGet<BeachChair>();
		beachChair.specificEffectUnlit = "BeachChairUnlit";
		beachChair.specificEffectLit = "BeachChairLit";
		beachChair.trackingEffect = "RecentlyBeachChair";
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGet<AnimTileable>();
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00006458 File Offset: 0x00004658
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000076 RID: 118
	public const string ID = "BeachChair";

	// Token: 0x04000077 RID: 119
	public const int TAN_LUX = 10000;

	// Token: 0x04000078 RID: 120
	private const float TANK_SIZE_KG = 20f;

	// Token: 0x04000079 RID: 121
	private const float SPILL_RATE_KG = 0.05f;
}
