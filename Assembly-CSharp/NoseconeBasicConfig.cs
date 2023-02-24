using System;
using TUNING;
using UnityEngine;

// Token: 0x02000280 RID: 640
public class NoseconeBasicConfig : IBuildingConfig
{
	// Token: 0x06000CC8 RID: 3272 RVA: 0x00047384 File Offset: 0x00045584
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0004738C File Offset: 0x0004558C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "NoseconeBasic";
		int num = 5;
		int num2 = 2;
		string text2 = "rocket_nosecone_default_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] hollow_TIER = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER2;
		string[] array = new string[] { "RefinedMetal", "Insulator" };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.Front;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00047449 File Offset: 0x00045649
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.NoseRocketModule, false);
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x00047489 File Offset: 0x00045689
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MINOR, 0f, 0f);
		go.GetComponent<ReorderableBuilding>().buildConditions.Add(new TopOnly());
	}

	// Token: 0x0400075E RID: 1886
	public const string ID = "NoseconeBasic";
}
