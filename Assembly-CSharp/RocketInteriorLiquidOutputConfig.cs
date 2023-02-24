using System;
using TUNING;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class RocketInteriorLiquidOutputConfig : IBuildingConfig
{
	// Token: 0x06000F12 RID: 3858 RVA: 0x00052354 File Offset: 0x00050554
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0005235C File Offset: 0x0005055C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RocketInteriorLiquidOutput";
		int num = 1;
		int num2 = 1;
		string text2 = "rocket_floor_plug_liquid_out_kanim";
		int num3 = 30;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnRocketEnvelope;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.ShowInBuildMenu = true;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "RocketInteriorLiquidOutput");
		return buildingDef;
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x0005244C File Offset: 0x0005064C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		base.ConfigureBuildingTemplate(go, prefab_tag);
		go.GetComponent<KPrefabID>().AddTag(GameTags.RocketInteriorBuilding, false);
		go.AddComponent<RequireInputs>();
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x00052470 File Offset: 0x00050670
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<PoweredActiveController.Def>();
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 10f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		go.AddOrGet<Filterable>().filterElementState = Filterable.ElementState.Liquid;
		RocketConduitStorageAccess rocketConduitStorageAccess = go.AddOrGet<RocketConduitStorageAccess>();
		rocketConduitStorageAccess.storage = storage;
		rocketConduitStorageAccess.cargoType = CargoBay.CargoType.Liquids;
		rocketConduitStorageAccess.targetLevel = 10f;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.alwaysDispense = true;
		conduitDispenser.elementFilter = null;
	}

	// Token: 0x0400085B RID: 2139
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;

	// Token: 0x0400085C RID: 2140
	private const CargoBay.CargoType CARGO_TYPE = CargoBay.CargoType.Liquids;

	// Token: 0x0400085D RID: 2141
	public const string ID = "RocketInteriorLiquidOutput";
}
