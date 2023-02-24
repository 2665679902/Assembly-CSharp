using System;
using TUNING;
using UnityEngine;

// Token: 0x020001B9 RID: 441
public class HabitatModuleSmallConfig : IBuildingConfig
{
	// Token: 0x060008A8 RID: 2216 RVA: 0x000333D5 File Offset: 0x000315D5
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x000333DC File Offset: 0x000315DC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HabitatModuleSmall";
		int num = 3;
		int num2 = 3;
		string text2 = "rocket_nosecone_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] dense_TIER = BUILDINGS.ROCKETRY_MASS_KG.DENSE_TIER0;
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

	// Token: 0x060008AA RID: 2218 RVA: 0x00033488 File Offset: 0x00031688
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.NoseRocketModule, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.LaunchButtonRocketModule, false);
		go.AddOrGet<AssignmentGroupController>().generateGroupOnStart = true;
		go.AddOrGet<PassengerRocketModule>().interiorReverbSnapshot = AudioMixerSnapshots.Get().SmallRocketInteriorReverbSnapshot;
		go.AddOrGet<ClustercraftExteriorDoor>().interiorTemplateName = "expansion1::interiors/habitat_small";
		go.AddOrGetDef<SimpleDoorController.Def>();
		go.AddOrGet<NavTeleporter>();
		go.AddOrGet<AccessControl>();
		go.AddOrGet<LaunchableRocketCluster>();
		go.AddOrGet<RocketCommandConditions>();
		go.AddOrGet<RocketProcessConditionDisplayTarget>();
		go.AddOrGet<CharacterOverlay>().shouldShowName = true;
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

	// Token: 0x060008AB RID: 2219 RVA: 0x000335D0 File Offset: 0x000317D0
	private void AttachPorts(GameObject go)
	{
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.liquidInputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.liquidOutputPort;
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.gasInputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.gasOutputPort;
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x00033624 File Offset: 0x00031824
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MINOR_PLUS, 0f, 0f);
		Ownable ownable = go.AddOrGet<Ownable>();
		ownable.slotID = Db.Get().AssignableSlots.HabitatModule.Id;
		ownable.canBePublic = false;
		FakeFloorAdder fakeFloorAdder = go.AddOrGet<FakeFloorAdder>();
		fakeFloorAdder.floorOffsets = new CellOffset[]
		{
			new CellOffset(-1, -1),
			new CellOffset(0, -1),
			new CellOffset(1, -1)
		};
		fakeFloorAdder.initiallyActive = false;
		go.AddOrGet<BuildingCellVisualizer>();
		go.GetComponent<ReorderableBuilding>().buildConditions.Add(new LimitOneCommandModule());
		go.GetComponent<ReorderableBuilding>().buildConditions.Add(new TopOnly());
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x000336E3 File Offset: 0x000318E3
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		go.AddOrGet<BuildingCellVisualizer>();
		this.AttachPorts(go);
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x000336FB File Offset: 0x000318FB
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<BuildingCellVisualizer>();
		this.AttachPorts(go);
	}

	// Token: 0x0400056F RID: 1391
	public const string ID = "HabitatModuleSmall";

	// Token: 0x04000570 RID: 1392
	private ConduitPortInfo gasInputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(-1, 0));

	// Token: 0x04000571 RID: 1393
	private ConduitPortInfo gasOutputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(1, 0));

	// Token: 0x04000572 RID: 1394
	private ConduitPortInfo liquidInputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(-1, 1));

	// Token: 0x04000573 RID: 1395
	private ConduitPortInfo liquidOutputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(1, 1));
}
