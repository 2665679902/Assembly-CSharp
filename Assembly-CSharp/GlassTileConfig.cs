using System;
using TUNING;
using UnityEngine;

// Token: 0x020001AC RID: 428
public class GlassTileConfig : IBuildingConfig
{
	// Token: 0x06000850 RID: 2128 RVA: 0x000310FC File Offset: 0x0002F2FC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GlassTile";
		int num = 1;
		int num2 = 1;
		string text2 = "floor_glass_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] transparents = MATERIALS.TRANSPARENTS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, transparents, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
		BuildingTemplates.CreateFoundationTileDef(buildingDef);
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.Overheatable = false;
		buildingDef.UseStructureTemperature = false;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.SceneLayer = Grid.SceneLayer.GlassTile;
		buildingDef.isKAnimTile = true;
		buildingDef.BlockTileIsTransparent = true;
		buildingDef.BlockTileAtlas = Assets.GetTextureAtlas("tiles_glass");
		buildingDef.BlockTilePlaceAtlas = Assets.GetTextureAtlas("tiles_glass_place");
		buildingDef.BlockTileMaterial = Assets.GetMaterial("tiles_solid");
		buildingDef.DecorBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_glass_tops_decor_info");
		buildingDef.DecorPlaceBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_glass_tops_decor_place_info");
		buildingDef.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
		return buildingDef;
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x000311F8 File Offset: 0x0002F3F8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
		simCellOccupier.setTransparent = true;
		simCellOccupier.notifyOnMelt = true;
		go.AddOrGet<TileTemperature>();
		go.AddOrGet<KAnimGridTileVisualizer>().blockTileConnectorID = GlassTileConfig.BlockTileConnectorID;
		go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Window, false);
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x00031267 File Offset: 0x0002F467
	public override void DoPostConfigureComplete(GameObject go)
	{
		GeneratedBuildings.RemoveLoopingSounds(go);
		go.GetComponent<KPrefabID>().AddTag(GameTags.FloorTiles, false);
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x00031280 File Offset: 0x0002F480
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<KAnimGridTileVisualizer>();
	}

	// Token: 0x0400053E RID: 1342
	public const string ID = "GlassTile";

	// Token: 0x0400053F RID: 1343
	public static readonly int BlockTileConnectorID = Hash.SDBMLower("tiles_glass_tops");
}
