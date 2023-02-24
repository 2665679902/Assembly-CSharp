using System;
using TUNING;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class CanvasWideConfig : IBuildingConfig
{
	// Token: 0x060000ED RID: 237 RVA: 0x000077CC File Offset: 0x000059CC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CanvasWide";
		int num = 3;
		int num2 = 2;
		string text2 = "painting_wide_off_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] array = new float[] { 400f, 1f };
		string[] array2 = new string[] { "Metal", "BuildingFiber" };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, new EffectorValues
		{
			amount = 15,
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

	// Token: 0x060000EE RID: 238 RVA: 0x00007892 File Offset: 0x00005A92
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x000078B1 File Offset: 0x00005AB1
	public override void DoPostConfigureComplete(GameObject go)
	{
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddComponent<Painting>().defaultAnimName = "off";
	}

	// Token: 0x04000089 RID: 137
	public const string ID = "CanvasWide";
}
