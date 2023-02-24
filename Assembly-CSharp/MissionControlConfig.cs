using System;
using TUNING;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class MissionControlConfig : IBuildingConfig
{
	// Token: 0x06000C84 RID: 3204 RVA: 0x0004679F File Offset: 0x0004499F
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x000467A8 File Offset: 0x000449A8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MissionControl";
		int num = 3;
		int num2 = 3;
		string text2 = "mission_control_station_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 960f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.DefaultAnimState = "off";
		return buildingDef;
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00046844 File Offset: 0x00044A44
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		BuildingDef def = go.GetComponent<BuildingComplete>().Def;
		Prioritizable.AddRef(go);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGetDef<PoweredController.Def>();
		SkyVisibilityMonitor.Def def2 = go.AddOrGetDef<SkyVisibilityMonitor.Def>();
		def2.ScanRadius = 1;
		def2.ScanOriginOffset = new CellOffset(0, def.HeightInCells);
		go.AddOrGetDef<MissionControl.Def>();
		MissionControlWorkable missionControlWorkable = go.AddOrGet<MissionControlWorkable>();
		missionControlWorkable.requiredSkillPerk = Db.Get().SkillPerks.CanMissionControl.Id;
		missionControlWorkable.workLayer = Grid.SceneLayer.BuildingUse;
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x000468D3 File Offset: 0x00044AD3
	public override void DoPostConfigureComplete(GameObject go)
	{
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Laboratory.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
	}

	// Token: 0x04000747 RID: 1863
	public const string ID = "MissionControl";

	// Token: 0x04000748 RID: 1864
	public const float EFFECT_DURATION = 600f;

	// Token: 0x04000749 RID: 1865
	public const float SPEED_MULTIPLIER = 1.2f;
}
