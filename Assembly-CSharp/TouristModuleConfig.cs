using System;
using TUNING;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class TouristModuleConfig : IBuildingConfig
{
	// Token: 0x06001088 RID: 4232 RVA: 0x00059E30 File Offset: 0x00058030
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x00059E38 File Offset: 0x00058038
	public override BuildingDef CreateBuildingDef()
	{
		string text = "TouristModule";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_tourist_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] command_MODULE_MASS = BUILDINGS.ROCKETRY_MASS_KG.COMMAND_MODULE_MASS;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, command_MODULE_MASS, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x00059EE8 File Offset: 0x000580E8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<TouristModule>();
		go.AddOrGet<CommandModuleWorkable>();
		go.AddOrGet<ArtifactFinder>();
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
		go.AddOrGet<Storage>();
		go.AddOrGet<MinionStorage>();
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x00059F6F File Offset: 0x0005816F
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_tourist_bg_kanim", false);
		Ownable ownable = go.AddOrGet<Ownable>();
		ownable.slotID = Db.Get().AssignableSlots.RocketCommandModule.Id;
		ownable.canBePublic = false;
	}

	// Token: 0x04000900 RID: 2304
	public const string ID = "TouristModule";
}
