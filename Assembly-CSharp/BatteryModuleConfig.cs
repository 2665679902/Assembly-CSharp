using System;
using TUNING;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class BatteryModuleConfig : IBuildingConfig
{
	// Token: 0x060000A9 RID: 169 RVA: 0x000060C4 File Offset: 0x000042C4
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000060CC File Offset: 0x000042CC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BatteryModule";
		int num = 3;
		int num2 = 2;
		string text2 = "rocket_battery_pack_kanim";
		int num3 = 1000;
		float num4 = 30f;
		float[] hollow_TIER = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER2;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.DefaultAnimState = "grounded";
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.Front;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.PowerInputOffset = BatteryModuleConfig.PLUG_OFFSET;
		buildingDef.PowerOutputOffset = BatteryModuleConfig.PLUG_OFFSET;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerOutput = true;
		buildingDef.UseWhitePowerOutputConnectorColour = true;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		return buildingDef;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x0000618C File Offset: 0x0000438C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddComponent<RequireInputs>();
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 2), GameTags.Rocket, null)
		};
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000061F8 File Offset: 0x000043F8
	public override void DoPostConfigureComplete(GameObject go)
	{
		Prioritizable.AddRef(go);
		ModuleBattery moduleBattery = go.AddOrGet<ModuleBattery>();
		moduleBattery.capacity = 100000f;
		moduleBattery.joulesLostPerSecond = 0.6666667f;
		WireUtilitySemiVirtualNetworkLink wireUtilitySemiVirtualNetworkLink = go.AddOrGet<WireUtilitySemiVirtualNetworkLink>();
		wireUtilitySemiVirtualNetworkLink.link1 = BatteryModuleConfig.PLUG_OFFSET;
		wireUtilitySemiVirtualNetworkLink.visualizeOnly = true;
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MINOR, 0f, 0f);
	}

	// Token: 0x04000072 RID: 114
	public const string ID = "BatteryModule";

	// Token: 0x04000073 RID: 115
	public const float NUM_CAPSULES = 3f;

	// Token: 0x04000074 RID: 116
	private static readonly CellOffset PLUG_OFFSET = new CellOffset(-1, 0);
}
