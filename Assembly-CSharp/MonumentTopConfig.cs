using System;
using Database;
using TUNING;
using UnityEngine;

// Token: 0x0200027E RID: 638
public class MonumentTopConfig : IBuildingConfig
{
	// Token: 0x06000CBC RID: 3260 RVA: 0x00047078 File Offset: 0x00045278
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MonumentTop";
		int num = 5;
		int num2 = 5;
		string text2 = "monument_upper_a_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] array = new float[] { 2500f, 2500f, 5000f };
		string[] array2 = new string[]
		{
			SimHashes.Glass.ToString(),
			SimHashes.Diamond.ToString(),
			SimHashes.Steel.ToString()
		};
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.MONUMENT.INCOMPLETE, tier, 0.2f);
		BuildingTemplates.CreateMonumentBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.AttachmentSlotTag = "MonumentTop";
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = false;
		return buildingDef;
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x0004716A File Offset: 0x0004536A
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<MonumentPart>().part = MonumentPartResource.Part.Top;
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00047194 File Offset: 0x00045394
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00047196 File Offset: 0x00045396
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00047198 File Offset: 0x00045398
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<KBatchedAnimController>().initialAnim = "option_a";
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			MonumentPart monumentPart = game_object.AddOrGet<MonumentPart>();
			monumentPart.part = MonumentPartResource.Part.Top;
			monumentPart.stateUISymbol = "upper";
		};
	}

	// Token: 0x0400075B RID: 1883
	public const string ID = "MonumentTop";
}
