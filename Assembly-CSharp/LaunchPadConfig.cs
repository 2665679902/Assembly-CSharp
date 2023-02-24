using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E4 RID: 484
public class LaunchPadConfig : IBuildingConfig
{
	// Token: 0x06000989 RID: 2441 RVA: 0x0003712C File Offset: 0x0003532C
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x00037134 File Offset: 0x00035334
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LaunchPad";
		int num = 7;
		int num2 = 2;
		string text2 = "rocket_launchpad_kanim";
		int num3 = 1000;
		float num4 = 120f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingBack;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.UseStructureTemperature = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.DefaultAnimState = "idle";
		buildingDef.CanMove = false;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort("TriggerLaunch", new CellOffset(-1, 0), STRINGS.BUILDINGS.PREFABS.LAUNCHPAD.LOGIC_PORT_LAUNCH, STRINGS.BUILDINGS.PREFABS.LAUNCHPAD.LOGIC_PORT_LAUNCH_ACTIVE, STRINGS.BUILDINGS.PREFABS.LAUNCHPAD.LOGIC_PORT_LAUNCH_INACTIVE, false, false) };
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port>
		{
			LogicPorts.Port.OutputPort("LaunchReady", new CellOffset(1, 0), STRINGS.BUILDINGS.PREFABS.LAUNCHPAD.LOGIC_PORT_READY, STRINGS.BUILDINGS.PREFABS.LAUNCHPAD.LOGIC_PORT_READY_ACTIVE, STRINGS.BUILDINGS.PREFABS.LAUNCHPAD.LOGIC_PORT_READY_INACTIVE, false, false),
			LogicPorts.Port.OutputPort("LandedRocket", new CellOffset(0, 1), STRINGS.BUILDINGS.PREFABS.LAUNCHPAD.LOGIC_PORT_LANDED_ROCKET, STRINGS.BUILDINGS.PREFABS.LAUNCHPAD.LOGIC_PORT_LANDED_ROCKET_ACTIVE, STRINGS.BUILDINGS.PREFABS.LAUNCHPAD.LOGIC_PORT_LANDED_ROCKET_INACTIVE, false, false)
		};
		return buildingDef;
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x000372A0 File Offset: 0x000354A0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.NotRocketInteriorBuilding, false);
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		LaunchPad launchPad = go.AddOrGet<LaunchPad>();
		launchPad.triggerPort = "TriggerLaunch";
		launchPad.statusPort = "LaunchReady";
		launchPad.landedRocketPort = "LandedRocket";
		FakeFloorAdder fakeFloorAdder = go.AddOrGet<FakeFloorAdder>();
		fakeFloorAdder.floorOffsets = new CellOffset[7];
		for (int i = 0; i < 7; i++)
		{
			fakeFloorAdder.floorOffsets[i] = new CellOffset(i - 3, 1);
		}
		go.AddOrGet<LaunchPadConditions>();
		ChainedBuilding.Def def = go.AddOrGetDef<ChainedBuilding.Def>();
		def.headBuildingTag = "LaunchPad".ToTag();
		def.linkBuildingTag = BaseModularLaunchpadPortConfig.LinkTag;
		def.objectLayer = ObjectLayer.Building;
		go.AddOrGetDef<LaunchPadMaterialDistributor.Def>();
		go.AddOrGet<UserNameable>();
		go.AddOrGet<CharacterOverlay>().shouldShowName = true;
		ModularConduitPortTiler modularConduitPortTiler = go.AddOrGet<ModularConduitPortTiler>();
		modularConduitPortTiler.manageRightCap = true;
		modularConduitPortTiler.manageLeftCap = false;
		modularConduitPortTiler.leftCapDefaultSceneLayerAdjust = 1;
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x000373DB File Offset: 0x000355DB
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040005EA RID: 1514
	public const string ID = "LaunchPad";

	// Token: 0x040005EB RID: 1515
	private const int WIDTH = 7;

	// Token: 0x040005EC RID: 1516
	private const string TRIGGER_LAUNCH_PORT_ID = "TriggerLaunch";

	// Token: 0x040005ED RID: 1517
	private const string LAUNCH_READY_PORT_ID = "LaunchReady";

	// Token: 0x040005EE RID: 1518
	private const string LANDED_ROCKET_ID = "LandedRocket";
}
