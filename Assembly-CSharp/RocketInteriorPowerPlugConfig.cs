using System;
using TUNING;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class RocketInteriorPowerPlugConfig : IBuildingConfig
{
	// Token: 0x06000F1F RID: 3871 RVA: 0x000526B4 File Offset: 0x000508B4
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x000526BC File Offset: 0x000508BC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RocketInteriorPowerPlug";
		int num = 1;
		int num2 = 1;
		string text2 = "rocket_floor_plug_kanim";
		int num3 = 30;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnRocketEnvelope;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.RequiresPowerOutput = true;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.WireIDs, "RocketInteriorPowerPlug");
		return buildingDef;
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x00052770 File Offset: 0x00050970
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		base.ConfigureBuildingTemplate(go, prefab_tag);
		go.GetComponent<KPrefabID>().AddTag(GameTags.RocketInteriorBuilding, false);
		go.AddComponent<RequireInputs>();
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x00052792 File Offset: 0x00050992
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<OperationalController.Def>();
		go.AddOrGet<WireUtilitySemiVirtualNetworkLink>().link1 = new CellOffset(0, 0);
	}

	// Token: 0x04000860 RID: 2144
	public const string ID = "RocketInteriorPowerPlug";
}
