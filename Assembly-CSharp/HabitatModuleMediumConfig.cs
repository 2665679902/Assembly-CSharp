using System;
using TUNING;
using UnityEngine;

// Token: 0x020001B8 RID: 440
public class HabitatModuleMediumConfig : IBuildingConfig
{
	// Token: 0x060008A0 RID: 2208 RVA: 0x00033015 File Offset: 0x00031215
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0003301C File Offset: 0x0003121C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HabitatModuleMedium";
		int num = 5;
		int num2 = 4;
		string text2 = "rocket_habitat_medium_module_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] dense_TIER = BUILDINGS.ROCKETRY_MASS_KG.DENSE_TIER1;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, dense_TIER, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
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

	// Token: 0x060008A2 RID: 2210 RVA: 0x000330C8 File Offset: 0x000312C8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.LaunchButtonRocketModule, false);
		go.AddOrGet<AssignmentGroupController>().generateGroupOnStart = true;
		go.AddOrGet<PassengerRocketModule>().interiorReverbSnapshot = AudioMixerSnapshots.Get().MediumRocketInteriorReverbSnapshot;
		go.AddOrGet<ClustercraftExteriorDoor>().interiorTemplateName = "expansion1::interiors/habitat_medium";
		go.AddOrGetDef<SimpleDoorController.Def>();
		go.AddOrGet<NavTeleporter>();
		go.AddOrGet<AccessControl>();
		go.AddOrGet<LaunchableRocketCluster>();
		go.AddOrGet<RocketCommandConditions>();
		go.AddOrGet<RocketProcessConditionDisplayTarget>();
		go.AddOrGet<CharacterOverlay>().shouldShowName = true;
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 4), GameTags.Rocket, null)
		};
		Storage storage = go.AddComponent<Storage>();
		storage.showInUI = false;
		storage.capacityKg = 10f;
		RocketConduitSender rocketConduitSender = go.AddComponent<RocketConduitSender>();
		rocketConduitSender.conduitStorage = storage;
		rocketConduitSender.conduitPortInfo = this.liquidInputPort;
		go.AddComponent<RocketConduitReceiver>().conduitPortInfo = this.liquidOutputPort;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.showInUI = false;
		storage2.capacityKg = 1f;
		RocketConduitSender rocketConduitSender2 = go.AddComponent<RocketConduitSender>();
		rocketConduitSender2.conduitStorage = storage2;
		rocketConduitSender2.conduitPortInfo = this.gasInputPort;
		go.AddComponent<RocketConduitReceiver>().conduitPortInfo = this.gasOutputPort;
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00033228 File Offset: 0x00031428
	private void AttachPorts(GameObject go)
	{
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.liquidInputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.liquidOutputPort;
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.gasInputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.gasOutputPort;
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0003327C File Offset: 0x0003147C
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MAJOR, 0f, 0f);
		Ownable ownable = go.AddOrGet<Ownable>();
		ownable.slotID = Db.Get().AssignableSlots.HabitatModule.Id;
		ownable.canBePublic = false;
		FakeFloorAdder fakeFloorAdder = go.AddOrGet<FakeFloorAdder>();
		fakeFloorAdder.floorOffsets = new CellOffset[]
		{
			new CellOffset(-2, -1),
			new CellOffset(-1, -1),
			new CellOffset(0, -1),
			new CellOffset(1, -1),
			new CellOffset(2, -1)
		};
		fakeFloorAdder.initiallyActive = false;
		go.AddOrGet<BuildingCellVisualizer>();
		go.GetComponent<ReorderableBuilding>().buildConditions.Add(new LimitOneCommandModule());
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00033343 File Offset: 0x00031543
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		go.AddOrGet<BuildingCellVisualizer>();
		this.AttachPorts(go);
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0003335B File Offset: 0x0003155B
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<BuildingCellVisualizer>();
		this.AttachPorts(go);
	}

	// Token: 0x0400056A RID: 1386
	public const string ID = "HabitatModuleMedium";

	// Token: 0x0400056B RID: 1387
	private ConduitPortInfo gasInputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(-2, 0));

	// Token: 0x0400056C RID: 1388
	private ConduitPortInfo gasOutputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(2, 0));

	// Token: 0x0400056D RID: 1389
	private ConduitPortInfo liquidInputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(-2, 3));

	// Token: 0x0400056E RID: 1390
	private ConduitPortInfo liquidOutputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(2, 3));
}
