using System;
using TUNING;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class PropGravitasWallConfig : IBuildingConfig
{
	// Token: 0x06000E4D RID: 3661 RVA: 0x0004DAEC File Offset: 0x0004BCEC
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x0004DAF4 File Offset: 0x0004BCF4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PropGravitasWall";
		int num = 1;
		int num2 = 1;
		string text2 = "gravitas_walls_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.Entombable = false;
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.DefaultAnimState = "off";
		buildingDef.ObjectLayer = ObjectLayer.Backwall;
		buildingDef.SceneLayer = Grid.SceneLayer.Backwall;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0004DB8C File Offset: 0x0004BD8C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		go.AddComponent<ZoneTile>();
		go.GetComponent<PrimaryElement>().SetElement(SimHashes.Granite, true);
		go.GetComponent<PrimaryElement>().Temperature = 273f;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x0004DBF3 File Offset: 0x0004BDF3
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007EE RID: 2030
	public const string ID = "PropGravitasWall";
}
