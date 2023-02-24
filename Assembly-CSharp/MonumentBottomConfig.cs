using System;
using Database;
using TUNING;
using UnityEngine;

// Token: 0x0200027C RID: 636
public class MonumentBottomConfig : IBuildingConfig
{
	// Token: 0x06000CB0 RID: 3248 RVA: 0x00046D4C File Offset: 0x00044F4C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MonumentBottom";
		int num = 5;
		int num2 = 5;
		string text2 = "monument_base_a_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] array = new float[] { 7500f, 2500f };
		string[] array2 = new string[]
		{
			SimHashes.Steel.ToString(),
			SimHashes.Obsidian.ToString()
		};
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.MONUMENT.INCOMPLETE, tier, 0.2f);
		BuildingTemplates.CreateMonumentBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = "MonumentBottom";
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = false;
		return buildingDef;
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00046E2C File Offset: 0x0004502C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), "MonumentMiddle", null)
		};
		go.AddOrGet<MonumentPart>().part = MonumentPartResource.Part.Bottom;
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00046E90 File Offset: 0x00045090
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00046E92 File Offset: 0x00045092
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00046E94 File Offset: 0x00045094
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<KBatchedAnimController>().initialAnim = "option_a";
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			MonumentPart monumentPart = game_object.AddOrGet<MonumentPart>();
			monumentPart.part = MonumentPartResource.Part.Bottom;
			monumentPart.stateUISymbol = "base";
		};
	}

	// Token: 0x04000759 RID: 1881
	public const string ID = "MonumentBottom";
}
