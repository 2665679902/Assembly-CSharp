using System;
using TUNING;
using UnityEngine;

// Token: 0x0200019F RID: 415
public class GasPermeableMembraneConfig : IBuildingConfig
{
	// Token: 0x06000810 RID: 2064 RVA: 0x0002F038 File Offset: 0x0002D238
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GasPermeableMembrane";
		int num = 1;
		int num2 = 1;
		string text2 = "floor_gasperm_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		BuildingTemplates.CreateFoundationTileDef(buildingDef);
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
		buildingDef.isKAnimTile = true;
		buildingDef.BlockTileAtlas = Assets.GetTextureAtlas("tiles_gasmembrane");
		buildingDef.BlockTilePlaceAtlas = Assets.GetTextureAtlas("tiles_gasmembrane_place");
		buildingDef.BlockTileMaterial = Assets.GetMaterial("tiles_solid");
		buildingDef.DecorBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_mesh_tops_decor_info");
		buildingDef.DecorPlaceBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_mesh_tops_decor_place_info");
		buildingDef.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
		return buildingDef;
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x0002F124 File Offset: 0x0002D324
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
		simCellOccupier.setLiquidImpermeable = true;
		simCellOccupier.doReplaceElement = false;
		go.AddOrGet<KAnimGridTileVisualizer>().blockTileConnectorID = MeshTileConfig.BlockTileConnectorID;
		go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
		go.AddComponent<SimTemperatureTransfer>();
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x0002F182 File Offset: 0x0002D382
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<ZoneTile>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.FloorTiles, false);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x0002F19C File Offset: 0x0002D39C
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<KAnimGridTileVisualizer>();
	}

	// Token: 0x0400051D RID: 1309
	public const string ID = "GasPermeableMembrane";
}
