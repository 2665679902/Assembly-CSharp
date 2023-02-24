using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x020001ED RID: 493
public class LiquidConduitConfig : IBuildingConfig
{
	// Token: 0x060009BB RID: 2491 RVA: 0x00038610 File Offset: 0x00036810
	public static void CommonConduitPostConfigureComplete(GameObject go)
	{
		GeneratedBuildings.RemoveLoopingSounds(go);
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x00038618 File Offset: 0x00036818
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LiquidConduit";
		int num = 1;
		int num2 = 1;
		string text2 = "utilities_liquid_kanim";
		int num3 = 10;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] plumbable = MATERIALS.PLUMBABLE;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, plumbable, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.ObjectLayer = ObjectLayer.LiquidConduit;
		buildingDef.TileLayer = ObjectLayer.LiquidConduitTile;
		buildingDef.ReplacementLayer = ObjectLayer.ReplacementLiquidConduit;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.SceneLayer = Grid.SceneLayer.LiquidConduits;
		buildingDef.isKAnimTile = true;
		buildingDef.isUtility = true;
		buildingDef.DragBuild = true;
		buildingDef.ReplacementTags = new List<Tag>();
		buildingDef.ReplacementTags.Add(GameTags.Pipes);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "LiquidConduit");
		return buildingDef;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00038717 File Offset: 0x00036917
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<Conduit>().type = ConduitType.Liquid;
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x00038740 File Offset: 0x00036940
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<Building>().Def.BuildingUnderConstruction.GetComponent<Constructable>().isDiggingRequired = false;
		go.AddComponent<EmptyConduitWorkable>();
		KAnimGraphTileVisualizer kanimGraphTileVisualizer = go.AddComponent<KAnimGraphTileVisualizer>();
		kanimGraphTileVisualizer.connectionSource = KAnimGraphTileVisualizer.ConnectionSource.Liquid;
		kanimGraphTileVisualizer.isPhysicalBuilding = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Pipes, false);
		LiquidConduitConfig.CommonConduitPostConfigureComplete(go);
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x00038799 File Offset: 0x00036999
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		KAnimGraphTileVisualizer kanimGraphTileVisualizer = go.AddComponent<KAnimGraphTileVisualizer>();
		kanimGraphTileVisualizer.connectionSource = KAnimGraphTileVisualizer.ConnectionSource.Liquid;
		kanimGraphTileVisualizer.isPhysicalBuilding = false;
	}

	// Token: 0x040005FA RID: 1530
	public const string ID = "LiquidConduit";
}
