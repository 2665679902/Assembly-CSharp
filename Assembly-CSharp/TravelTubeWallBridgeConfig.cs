using System;
using TUNING;
using UnityEngine;

// Token: 0x02000340 RID: 832
public class TravelTubeWallBridgeConfig : IBuildingConfig
{
	// Token: 0x06001096 RID: 4246 RVA: 0x0005A1C0 File Offset: 0x000583C0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "TravelTubeWallBridge";
		int num = 1;
		int num2 = 1;
		string text2 = "tube_tile_bridge_kanim";
		int num3 = 100;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, plastics, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		BuildingTemplates.CreateFoundationTileDef(buildingDef);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.AudioCategory = "Plastic";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R90;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 2);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.ForegroundLayer = Grid.SceneLayer.TileMain;
		return buildingDef;
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x0005A27A File Offset: 0x0005847A
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
		simCellOccupier.doReplaceElement = true;
		simCellOccupier.movementSpeedMultiplier = DUPLICANTSTATS.MOVEMENT.PENALTY_3;
		simCellOccupier.notifyOnMelt = true;
		go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
		go.AddOrGet<TileTemperature>();
		go.AddOrGet<TravelTubeBridge>();
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x0005A2BA File Offset: 0x000584BA
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		this.AddNetworkLink(go).visualizeOnly = true;
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x0005A2D8 File Offset: 0x000584D8
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		this.AddNetworkLink(go).visualizeOnly = true;
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x0005A2F5 File Offset: 0x000584F5
	public override void DoPostConfigureComplete(GameObject go)
	{
		this.AddNetworkLink(go).visualizeOnly = false;
		go.AddOrGet<BuildingCellVisualizer>();
		go.AddOrGet<KPrefabID>().AddTag(GameTags.TravelTubeBridges, false);
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x0005A31C File Offset: 0x0005851C
	protected virtual TravelTubeUtilityNetworkLink AddNetworkLink(GameObject go)
	{
		TravelTubeUtilityNetworkLink travelTubeUtilityNetworkLink = go.AddOrGet<TravelTubeUtilityNetworkLink>();
		travelTubeUtilityNetworkLink.link1 = new CellOffset(-1, 0);
		travelTubeUtilityNetworkLink.link2 = new CellOffset(1, 0);
		return travelTubeUtilityNetworkLink;
	}

	// Token: 0x04000905 RID: 2309
	public const string ID = "TravelTubeWallBridge";
}
