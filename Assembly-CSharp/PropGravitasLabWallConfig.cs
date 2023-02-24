using System;
using TUNING;
using UnityEngine;

// Token: 0x020002CD RID: 717
public class PropGravitasLabWallConfig : IBuildingConfig
{
	// Token: 0x06000E34 RID: 3636 RVA: 0x0004D642 File Offset: 0x0004B842
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0004D64C File Offset: 0x0004B84C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PropGravitasLabWall";
		int num = 2;
		int num2 = 3;
		string text2 = "gravitas_lab_wall_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.PermittedRotations = PermittedRotations.R90;
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

	// Token: 0x06000E36 RID: 3638 RVA: 0x0004D6E4 File Offset: 0x0004B8E4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		go.AddComponent<ZoneTile>();
		go.GetComponent<PrimaryElement>().SetElement(SimHashes.Glass, true);
		go.GetComponent<PrimaryElement>().Temperature = 273f;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x0004D74B File Offset: 0x0004B94B
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007EB RID: 2027
	public const string ID = "PropGravitasLabWall";
}
