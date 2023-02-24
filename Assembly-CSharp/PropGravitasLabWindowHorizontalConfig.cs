using System;
using TUNING;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class PropGravitasLabWindowHorizontalConfig : IBuildingConfig
{
	// Token: 0x06000E3E RID: 3646 RVA: 0x0004D861 File Offset: 0x0004BA61
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x0004D868 File Offset: 0x0004BA68
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PropGravitasLabWindowHorizontal";
		int num = 3;
		int num2 = 2;
		string text2 = "gravitas_lab_window_horizontal_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier_TINY = BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;
		string[] glasses = MATERIALS.GLASSES;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier_TINY, glasses, num5, buildLocationRule, DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.Entombable = false;
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.DefaultAnimState = "on";
		buildingDef.ObjectLayer = ObjectLayer.Backwall;
		buildingDef.SceneLayer = Grid.SceneLayer.Backwall;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x0004D8FC File Offset: 0x0004BAFC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		go.AddComponent<ZoneTile>();
		go.GetComponent<PrimaryElement>().SetElement(SimHashes.Glass, true);
		go.GetComponent<PrimaryElement>().Temperature = 273f;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x0004D963 File Offset: 0x0004BB63
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007ED RID: 2029
	public const string ID = "PropGravitasLabWindowHorizontal";
}
