using System;
using TUNING;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class RocketWallTileConfig : IBuildingConfig
{
	// Token: 0x06000F2E RID: 3886 RVA: 0x00052A83 File Offset: 0x00050C83
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x00052A8C File Offset: 0x00050C8C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RocketWallTile";
		int num = 1;
		int num2 = 1;
		string text2 = "floor_rocket_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.DebugOnly = true;
		BuildingTemplates.CreateFoundationTileDef(buildingDef);
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.Overheatable = false;
		buildingDef.UseStructureTemperature = false;
		buildingDef.Replaceable = false;
		buildingDef.Invincible = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
		buildingDef.isKAnimTile = true;
		buildingDef.BlockTileAtlas = Assets.GetTextureAtlas("tiles_rocket_wall_int");
		buildingDef.BlockTilePlaceAtlas = Assets.GetTextureAtlas("tiles_rocket_wall_int_place");
		buildingDef.BlockTileMaterial = Assets.GetMaterial("tiles_solid");
		buildingDef.DecorBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_rocket_wall_ext_decor_info");
		buildingDef.DecorPlaceBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_rocket_wall_ext_place_decor_info");
		buildingDef.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x00052BB8 File Offset: 0x00050DB8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
		simCellOccupier.strengthMultiplier = 10f;
		simCellOccupier.notifyOnMelt = true;
		go.AddOrGet<TileTemperature>();
		go.AddOrGet<KAnimGridTileVisualizer>().blockTileConnectorID = RocketWallTileConfig.BlockTileConnectorID;
		go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x00052C1C File Offset: 0x00050E1C
	public override void DoPostConfigureComplete(GameObject go)
	{
		GeneratedBuildings.RemoveLoopingSounds(go);
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Bunker, false);
		component.AddTag(GameTags.FloorTiles, false);
		component.AddTag(GameTags.RocketEnvelopeTile, false);
		component.AddTag(GameTags.NoRocketRefund, false);
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x00052C70 File Offset: 0x00050E70
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<KAnimGridTileVisualizer>();
	}

	// Token: 0x04000867 RID: 2151
	public const string ID = "RocketWallTile";

	// Token: 0x04000868 RID: 2152
	public static readonly int BlockTileConnectorID = Hash.SDBMLower("tiles_rocket_wall_int");
}
