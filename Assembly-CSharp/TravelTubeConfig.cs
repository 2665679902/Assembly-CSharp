using System;
using TUNING;
using UnityEngine;

// Token: 0x0200033E RID: 830
public class TravelTubeConfig : IBuildingConfig
{
	// Token: 0x0600108D RID: 4237 RVA: 0x00059FAC File Offset: 0x000581AC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "TravelTube";
		int num = 1;
		int num2 = 1;
		string text2 = "travel_tube_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, plastics, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.TileLayer = ObjectLayer.TravelTubeTile;
		buildingDef.ReplacementLayer = ObjectLayer.ReplacementTravelTube;
		buildingDef.AudioCategory = "Plastic";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = 0f;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.isKAnimTile = true;
		buildingDef.isUtility = true;
		buildingDef.DragBuild = true;
		return buildingDef;
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x0005A076 File Offset: 0x00058276
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<TravelTube>();
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x0005A09A File Offset: 0x0005829A
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		KAnimGraphTileVisualizer kanimGraphTileVisualizer = go.AddComponent<KAnimGraphTileVisualizer>();
		kanimGraphTileVisualizer.connectionSource = KAnimGraphTileVisualizer.ConnectionSource.Tube;
		kanimGraphTileVisualizer.isPhysicalBuilding = false;
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0005A0AF File Offset: 0x000582AF
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<Building>().Def.BuildingUnderConstruction.GetComponent<Constructable>().isDiggingRequired = false;
		KAnimGraphTileVisualizer kanimGraphTileVisualizer = go.AddComponent<KAnimGraphTileVisualizer>();
		kanimGraphTileVisualizer.connectionSource = KAnimGraphTileVisualizer.ConnectionSource.Tube;
		kanimGraphTileVisualizer.isPhysicalBuilding = true;
	}

	// Token: 0x04000901 RID: 2305
	public const string ID = "TravelTube";
}
