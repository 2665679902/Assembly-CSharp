using System;
using System.Collections.Generic;
using System.Linq;
using TUNING;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class RailGunPayloadOpenerConfig : IBuildingConfig
{
	// Token: 0x06000E91 RID: 3729 RVA: 0x0004ECE8 File Offset: 0x0004CEE8
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0004ECF0 File Offset: 0x0004CEF0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RailGunPayloadOpener";
		int num = 3;
		int num2 = 3;
		string text2 = "railgun_emptier_kanim";
		int num3 = 250;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.DefaultAnimState = "on";
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		return buildingDef;
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x0004ED87 File Offset: 0x0004CF87
	private void AttachPorts(GameObject go)
	{
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.liquidOutputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.gasOutputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.solidOutputPort;
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x0004EDBC File Offset: 0x0004CFBC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		RailGunPayloadOpener railGunPayloadOpener = go.AddOrGet<RailGunPayloadOpener>();
		railGunPayloadOpener.liquidPortInfo = this.liquidOutputPort;
		railGunPayloadOpener.gasPortInfo = this.gasOutputPort;
		railGunPayloadOpener.solidPortInfo = this.solidOutputPort;
		railGunPayloadOpener.payloadStorage = go.AddComponent<Storage>();
		railGunPayloadOpener.payloadStorage.showInUI = true;
		railGunPayloadOpener.payloadStorage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		railGunPayloadOpener.payloadStorage.storageFilters = new List<Tag> { GameTags.RailGunPayloadEmptyable };
		railGunPayloadOpener.payloadStorage.capacityKg = 10f;
		railGunPayloadOpener.resourceStorage = go.AddComponent<Storage>();
		railGunPayloadOpener.resourceStorage.showInUI = true;
		railGunPayloadOpener.resourceStorage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		List<Tag> list = STORAGEFILTERS.NOT_EDIBLE_SOLIDS.Concat(STORAGEFILTERS.GASES).ToList<Tag>();
		list = list.Concat(STORAGEFILTERS.LIQUIDS).ToList<Tag>();
		railGunPayloadOpener.resourceStorage.storageFilters = list;
		railGunPayloadOpener.resourceStorage.capacityKg = 20000f;
		ManualDeliveryKG manualDeliveryKG = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(railGunPayloadOpener.payloadStorage);
		manualDeliveryKG.RequestedItemTag = GameTags.RailGunPayloadEmptyable;
		manualDeliveryKG.capacity = 10f;
		manualDeliveryKG.refillMass = 1f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		manualDeliveryKG.operationalRequirement = Operational.State.None;
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0004EF04 File Offset: 0x0004D104
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<BuildingCellVisualizer>();
		DropAllWorkable dropAllWorkable = go.AddOrGet<DropAllWorkable>();
		dropAllWorkable.dropWorkTime = 90f;
		dropAllWorkable.choreTypeID = Db.Get().ChoreTypes.Fetch.Id;
		dropAllWorkable.ConfigureMultitoolContext("build", EffectConfigs.BuildSplashId);
		RequireInputs component = go.GetComponent<RequireInputs>();
		component.SetRequirements(true, false);
		component.requireConduitHasMass = false;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0004EF70 File Offset: 0x0004D170
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		this.AttachPorts(go);
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x0004EF88 File Offset: 0x0004D188
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		this.AttachPorts(go);
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x0400080E RID: 2062
	public const string ID = "RailGunPayloadOpener";

	// Token: 0x0400080F RID: 2063
	private ConduitPortInfo liquidOutputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(0, 0));

	// Token: 0x04000810 RID: 2064
	private ConduitPortInfo gasOutputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(1, 0));

	// Token: 0x04000811 RID: 2065
	private ConduitPortInfo solidOutputPort = new ConduitPortInfo(ConduitType.Solid, new CellOffset(-1, 0));
}
