﻿using System;
using TUNING;
using UnityEngine;

// Token: 0x0200023C RID: 572
public class MetalTileConfig : IBuildingConfig
{
	// Token: 0x06000B47 RID: 2887 RVA: 0x0003FD0C File Offset: 0x0003DF0C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MetalTile";
		int num = 1;
		int num2 = 1;
		string text2 = "floor_metal_kanim";
		int num3 = 100;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER2, none, 0.2f);
		BuildingTemplates.CreateFoundationTileDef(buildingDef);
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.Overheatable = false;
		buildingDef.UseStructureTemperature = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
		buildingDef.isKAnimTile = true;
		buildingDef.BlockTileMaterial = Assets.GetMaterial("tiles_solid");
		buildingDef.BlockTileAtlas = Assets.GetTextureAtlas("tiles_metal");
		buildingDef.BlockTilePlaceAtlas = Assets.GetTextureAtlas("tiles_metal_place");
		buildingDef.BlockTileShineAtlas = Assets.GetTextureAtlas("tiles_metal_spec");
		buildingDef.DecorBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_metal_tops_decor_info");
		buildingDef.DecorPlaceBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_metal_tops_decor_place_info");
		buildingDef.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
		return buildingDef;
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x0003FE10 File Offset: 0x0003E010
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
		simCellOccupier.movementSpeedMultiplier = DUPLICANTSTATS.MOVEMENT.BONUS_3;
		simCellOccupier.notifyOnMelt = true;
		go.AddOrGet<TileTemperature>();
		go.AddOrGet<KAnimGridTileVisualizer>().blockTileConnectorID = MetalTileConfig.BlockTileConnectorID;
		go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x0003FE72 File Offset: 0x0003E072
	public override void DoPostConfigureComplete(GameObject go)
	{
		GeneratedBuildings.RemoveLoopingSounds(go);
		go.GetComponent<KPrefabID>().AddTag(GameTags.FloorTiles, false);
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x0003FE8B File Offset: 0x0003E08B
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<KAnimGridTileVisualizer>();
	}

	// Token: 0x040006B1 RID: 1713
	public const string ID = "MetalTile";

	// Token: 0x040006B2 RID: 1714
	public static readonly int BlockTileConnectorID = Hash.SDBMLower("tiles_metal_tops");
}
