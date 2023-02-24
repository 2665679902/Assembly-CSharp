using System;
using TUNING;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class CanvasConfig : IBuildingConfig
{
	// Token: 0x060000E5 RID: 229 RVA: 0x000075BC File Offset: 0x000057BC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Canvas";
		int num = 2;
		int num2 = 2;
		string text2 = "painting_off_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] array = new float[] { 400f, 1f };
		string[] array2 = new string[] { "Metal", "BuildingFiber" };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, new EffectorValues
		{
			amount = 10,
			radius = 6
		}, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.SceneLayer = Grid.SceneLayer.InteriorWall;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.DefaultAnimState = "off";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00007682 File Offset: 0x00005882
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x000076A1 File Offset: 0x000058A1
	public override void DoPostConfigureComplete(GameObject go)
	{
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddComponent<Painting>().defaultAnimName = "off";
	}

	// Token: 0x04000087 RID: 135
	public const string ID = "Canvas";
}
