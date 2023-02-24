using System;
using TUNING;
using UnityEngine;

// Token: 0x0200030C RID: 780
public class SolarPanelModuleConfig : IBuildingConfig
{
	// Token: 0x06000F85 RID: 3973 RVA: 0x00054850 File Offset: 0x00052A50
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x00054858 File Offset: 0x00052A58
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SolarPanelModule";
		int num = 3;
		int num2 = 1;
		string text2 = "rocket_solar_panel_module_kanim";
		int num3 = 1000;
		float num4 = 30f;
		float[] hollow_TIER = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER1;
		string[] glasses = MATERIALS.GLASSES;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, glasses, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.DefaultAnimState = "grounded";
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.GeneratorWattageRating = 60f;
		buildingDef.GeneratorBaseCapacity = buildingDef.GeneratorWattageRating;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.Front;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.PowerInputOffset = SolarPanelModuleConfig.PLUG_OFFSET;
		buildingDef.PowerOutputOffset = SolarPanelModuleConfig.PLUG_OFFSET;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerOutput = true;
		buildingDef.UseWhitePowerOutputConnectorColour = true;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		return buildingDef;
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x00054948 File Offset: 0x00052B48
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddComponent<RequireInputs>();
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 1), GameTags.Rocket, null)
		};
		go.AddComponent<PartialLightBlocking>();
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x000549BA File Offset: 0x00052BBA
	public override void DoPostConfigureComplete(GameObject go)
	{
		Prioritizable.AddRef(go);
		go.AddOrGet<ModuleSolarPanel>().showConnectedConsumerStatusItems = false;
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.INSIGNIFICANT, 0f, 0f);
		go.GetComponent<RocketModule>().operationalLandedRequired = false;
	}

	// Token: 0x04000889 RID: 2185
	public const string ID = "SolarPanelModule";

	// Token: 0x0400088A RID: 2186
	private static readonly CellOffset PLUG_OFFSET = new CellOffset(-1, 0);

	// Token: 0x0400088B RID: 2187
	private const float EFFICIENCY_RATIO = 0.75f;

	// Token: 0x0400088C RID: 2188
	public const float MAX_WATTS = 60f;
}
