using System;
using TUNING;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class TelephoneConfig : IBuildingConfig
{
	// Token: 0x0600105E RID: 4190 RVA: 0x000591BF File Offset: 0x000573BF
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x000591C8 File Offset: 0x000573C8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Telephone";
		int num = 1;
		int num2 = 2;
		string text2 = "telephone_kanim";
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
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		return buildingDef;
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x0005925C File Offset: 0x0005745C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		Telephone telephone = go.AddOrGet<Telephone>();
		telephone.babbleEffect = "TelephoneBabble";
		telephone.chatEffect = "TelephoneChat";
		telephone.longDistanceEffect = "TelephoneLongDistance";
		telephone.trackingEffect = "RecentlyTelephoned";
		go.AddOrGet<TelephoneCallerWorkable>().basePriority = RELAXATION.PRIORITY.TIER5;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x000592E8 File Offset: 0x000574E8
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040008E8 RID: 2280
	public const string ID = "Telephone";

	// Token: 0x040008E9 RID: 2281
	public const float ringTime = 15f;

	// Token: 0x040008EA RID: 2282
	public const float callTime = 25f;
}
