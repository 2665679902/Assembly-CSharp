using System;
using TUNING;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class MissionControlClusterConfig : IBuildingConfig
{
	// Token: 0x06000C7F RID: 3199 RVA: 0x0004663B File Offset: 0x0004483B
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x00046644 File Offset: 0x00044844
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MissionControlCluster";
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

	// Token: 0x06000C81 RID: 3201 RVA: 0x000466E0 File Offset: 0x000448E0
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
		go.AddOrGetDef<MissionControlCluster.Def>();
		MissionControlClusterWorkable missionControlClusterWorkable = go.AddOrGet<MissionControlClusterWorkable>();
		missionControlClusterWorkable.requiredSkillPerk = Db.Get().SkillPerks.CanMissionControl.Id;
		missionControlClusterWorkable.workLayer = Grid.SceneLayer.BuildingUse;
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0004676F File Offset: 0x0004496F
	public override void DoPostConfigureComplete(GameObject go)
	{
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Laboratory.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
	}

	// Token: 0x04000743 RID: 1859
	public const string ID = "MissionControlCluster";

	// Token: 0x04000744 RID: 1860
	public const int WORK_RANGE_RADIUS = 2;

	// Token: 0x04000745 RID: 1861
	public const float EFFECT_DURATION = 600f;

	// Token: 0x04000746 RID: 1862
	public const float SPEED_MULTIPLIER = 1.2f;
}
