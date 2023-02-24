using System;
using TUNING;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class CanvasTallConfig : IBuildingConfig
{
	// Token: 0x060000E9 RID: 233 RVA: 0x000076C4 File Offset: 0x000058C4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CanvasTall";
		int num = 2;
		int num2 = 3;
		string text2 = "painting_tall_off_kanim";
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

	// Token: 0x060000EA RID: 234 RVA: 0x0000778A File Offset: 0x0000598A
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060000EB RID: 235 RVA: 0x000077A9 File Offset: 0x000059A9
	public override void DoPostConfigureComplete(GameObject go)
	{
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddComponent<Painting>().defaultAnimName = "off";
	}

	// Token: 0x04000088 RID: 136
	public const string ID = "CanvasTall";
}
