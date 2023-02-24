using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class RailGunConfig : IBuildingConfig
{
	// Token: 0x06000E84 RID: 3716 RVA: 0x0004E89D File Offset: 0x0004CA9D
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x0004E8A4 File Offset: 0x0004CAA4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RailGun";
		int num = 5;
		int num2 = 6;
		string text2 = "rail_gun_kanim";
		int num3 = 250;
		float num4 = 30f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = 400f;
		buildingDef.DefaultAnimState = "off";
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(-2, 0);
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(-2, 1);
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(RailGun.PORT_ID, new CellOffset(-2, 2), STRINGS.BUILDINGS.PREFABS.RAILGUN.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.RAILGUN.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.RAILGUN.LOGIC_PORT_INACTIVE, false, false) };
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("HEP_STORAGE", new CellOffset(2, 0), STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_ACTIVE, STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_INACTIVE, false, false) };
		return buildingDef;
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x0004E9FC File Offset: 0x0004CBFC
	private void AttachPorts(GameObject go)
	{
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.liquidInputPort;
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.gasInputPort;
		go.AddComponent<ConduitSecondaryInput>().portInfo = this.solidInputPort;
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x0004EA34 File Offset: 0x0004CC34
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		RailGun railGun = go.AddOrGet<RailGun>();
		go.AddOrGet<LoopingSounds>();
		ClusterDestinationSelector clusterDestinationSelector = go.AddOrGet<ClusterDestinationSelector>();
		clusterDestinationSelector.assignable = true;
		clusterDestinationSelector.requireAsteroidDestination = true;
		railGun.liquidPortInfo = this.liquidInputPort;
		railGun.gasPortInfo = this.gasInputPort;
		railGun.solidPortInfo = this.solidInputPort;
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.capacity = 200f;
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.showInUI = false;
		highEnergyParticleStorage.PORT_ID = "HEP_STORAGE";
		highEnergyParticleStorage.showCapacityStatusItem = true;
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x0004EAB4 File Offset: 0x0004CCB4
	public override void DoPostConfigureComplete(GameObject go)
	{
		List<Tag> list = new List<Tag>();
		list.AddRange(STORAGEFILTERS.NOT_EDIBLE_SOLIDS);
		list.AddRange(STORAGEFILTERS.GASES);
		list.AddRange(STORAGEFILTERS.FOOD);
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.showInUI = true;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.storageFilters = list;
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
		storage.capacityKg = 1200f;
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x0004EB20 File Offset: 0x0004CD20
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		this.AttachPorts(go);
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x0004EB31 File Offset: 0x0004CD31
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		this.AttachPorts(go);
	}

	// Token: 0x040007FC RID: 2044
	public const string ID = "RailGun";

	// Token: 0x040007FD RID: 2045
	public const string PORT_ID = "HEP_STORAGE";

	// Token: 0x040007FE RID: 2046
	public const int RANGE = 20;

	// Token: 0x040007FF RID: 2047
	public const float BASE_PARTICLE_COST = 0f;

	// Token: 0x04000800 RID: 2048
	public const float HEX_PARTICLE_COST = 10f;

	// Token: 0x04000801 RID: 2049
	public const float HEP_CAPACITY = 200f;

	// Token: 0x04000802 RID: 2050
	public const float TAKEOFF_VELOCITY = 35f;

	// Token: 0x04000803 RID: 2051
	public const int MAINTENANCE_AFTER_NUM_PAYLOADS = 6;

	// Token: 0x04000804 RID: 2052
	public const int MAINTENANCE_COOLDOWN = 30;

	// Token: 0x04000805 RID: 2053
	public const float CAPACITY = 1200f;

	// Token: 0x04000806 RID: 2054
	private ConduitPortInfo solidInputPort = new ConduitPortInfo(ConduitType.Solid, new CellOffset(-1, 0));

	// Token: 0x04000807 RID: 2055
	private ConduitPortInfo liquidInputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(0, 0));

	// Token: 0x04000808 RID: 2056
	private ConduitPortInfo gasInputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(1, 0));
}
