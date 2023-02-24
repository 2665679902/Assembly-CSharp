using System;
using TUNING;
using UnityEngine;

// Token: 0x020002CE RID: 718
public class PropGravitasLabWindowConfig : IBuildingConfig
{
	// Token: 0x06000E39 RID: 3641 RVA: 0x0004D755 File Offset: 0x0004B955
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x0004D75C File Offset: 0x0004B95C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PropGravitasLabWindow";
		int num = 2;
		int num2 = 3;
		string text2 = "gravitas_lab_window_kanim";
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

	// Token: 0x06000E3B RID: 3643 RVA: 0x0004D7F0 File Offset: 0x0004B9F0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		go.AddComponent<ZoneTile>();
		go.GetComponent<PrimaryElement>().SetElement(SimHashes.Glass, true);
		go.GetComponent<PrimaryElement>().Temperature = 273f;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x0004D857 File Offset: 0x0004BA57
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007EC RID: 2028
	public const string ID = "PropGravitasLabWindow";
}
