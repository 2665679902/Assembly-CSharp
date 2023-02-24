using System;
using TUNING;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class TilePOIConfig : IBuildingConfig
{
	// Token: 0x06001082 RID: 4226 RVA: 0x00059C90 File Offset: 0x00057E90
	public override BuildingDef CreateBuildingDef()
	{
		string id = TilePOIConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "floor_mesh_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_MINERALS = MATERIALS.ALL_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, all_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.Overheatable = false;
		buildingDef.Repairable = false;
		buildingDef.Replaceable = false;
		buildingDef.Invincible = true;
		buildingDef.IsFoundation = true;
		buildingDef.UseStructureTemperature = false;
		buildingDef.TileLayer = ObjectLayer.FoundationTile;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
		buildingDef.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
		buildingDef.isKAnimTile = true;
		buildingDef.DebugOnly = true;
		buildingDef.BlockTileAtlas = Assets.GetTextureAtlas("tiles_POI");
		buildingDef.BlockTilePlaceAtlas = Assets.GetTextureAtlas("tiles_POI");
		buildingDef.BlockTileMaterial = Assets.GetMaterial("tiles_solid");
		buildingDef.DecorBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_POI_tops_decor_info");
		buildingDef.DecorPlaceBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_POI_tops_decor_info");
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x00059DAF File Offset: 0x00057FAF
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<SimCellOccupier>().doReplaceElement = true;
		go.AddOrGet<TileTemperature>();
		go.AddOrGet<KAnimGridTileVisualizer>();
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x00059DE6 File Offset: 0x00057FE6
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.Bunker, false);
		go.AddComponent<SimTemperatureTransfer>();
		go.GetComponent<Deconstructable>().allowDeconstruction = true;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x00059E0C File Offset: 0x0005800C
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<KAnimGridTileVisualizer>();
	}

	// Token: 0x040008FF RID: 2303
	public static string ID = "TilePOI";
}
