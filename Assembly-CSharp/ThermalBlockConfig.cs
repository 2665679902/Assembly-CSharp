using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200033A RID: 826
public class ThermalBlockConfig : IBuildingConfig
{
	// Token: 0x06001077 RID: 4215 RVA: 0x0005995C File Offset: 0x00057B5C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ThermalBlock";
		int num = 1;
		int num2 = 1;
		string text2 = "thermalblock_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] any_BUILDABLE = MATERIALS.ANY_BUILDABLE;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, any_BUILDABLE, num5, buildLocationRule, DECOR.NONE, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.ViewMode = OverlayModes.Temperature.ID;
		buildingDef.DefaultAnimState = "off";
		buildingDef.ObjectLayer = ObjectLayer.Backwall;
		buildingDef.SceneLayer = Grid.SceneLayer.Backwall;
		buildingDef.ReplacementLayer = ObjectLayer.ReplacementBackwall;
		buildingDef.ReplacementCandidateLayers = new List<ObjectLayer>
		{
			ObjectLayer.FoundationTile,
			ObjectLayer.Backwall
		};
		buildingDef.ReplacementTags = new List<Tag>
		{
			GameTags.FloorTiles,
			GameTags.Backwall
		};
		return buildingDef;
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x00059A2D File Offset: 0x00057C2D
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		go.AddComponent<ZoneTile>();
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x00059A57 File Offset: 0x00057C57
	public override void DoPostConfigureComplete(GameObject go)
	{
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Backwall, false);
		component.prefabSpawnFn += delegate(GameObject game_object)
		{
			HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(game_object);
			StructureTemperaturePayload payload = GameComps.StructureTemperatures.GetPayload(handle);
			int num = Grid.PosToCell(game_object);
			payload.OverrideExtents(new Extents(num, ThermalBlockConfig.overrideOffsets));
			GameComps.StructureTemperatures.SetPayload(handle, ref payload);
		};
	}

	// Token: 0x040008FB RID: 2299
	public const string ID = "ThermalBlock";

	// Token: 0x040008FC RID: 2300
	private static readonly CellOffset[] overrideOffsets = new CellOffset[]
	{
		new CellOffset(-1, -1),
		new CellOffset(1, -1),
		new CellOffset(-1, 1),
		new CellOffset(1, 1)
	};
}
