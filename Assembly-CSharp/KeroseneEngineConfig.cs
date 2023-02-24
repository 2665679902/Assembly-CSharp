using System;
using TUNING;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class KeroseneEngineConfig : IBuildingConfig
{
	// Token: 0x06000960 RID: 2400 RVA: 0x00036711 File Offset: 0x00034911
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_VANILLA_ONLY;
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x00036718 File Offset: 0x00034918
	public override BuildingDef CreateBuildingDef()
	{
		string text = "KeroseneEngine";
		int num = 7;
		int num2 = 5;
		string text2 = "rocket_petroleum_engine_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] engine_MASS_SMALL = BUILDINGS.ROCKETRY_MASS_KG.ENGINE_MASS_SMALL;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, engine_MASS_SMALL, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x000367C8 File Offset: 0x000349C8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0003682C File Offset: 0x00034A2C
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngine rocketEngine = go.AddOrGet<RocketEngine>();
		rocketEngine.fuelTag = ElementLoader.FindElementByHash(SimHashes.Petroleum).tag;
		rocketEngine.efficiency = ROCKETRY.ENGINE_EFFICIENCY.MEDIUM;
		rocketEngine.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_petroleum_engine_bg_kanim", false);
	}

	// Token: 0x040005DB RID: 1499
	public const string ID = "KeroseneEngine";
}
