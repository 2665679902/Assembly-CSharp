using System;
using TUNING;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class RocketInteriorSolidInputConfig : IBuildingConfig
{
	// Token: 0x06000F24 RID: 3876 RVA: 0x000527B5 File Offset: 0x000509B5
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x000527BC File Offset: 0x000509BC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RocketInteriorSolidInput";
		int num = 1;
		int num2 = 1;
		string text2 = "rocket_floor_plug_solid_kanim";
		int num3 = 30;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnRocketEnvelope;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.InputConduitType = ConduitType.Solid;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.SolidConveyor.ID;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, "RocketInteriorSolidInput");
		return buildingDef;
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x0005287D File Offset: 0x00050A7D
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		base.ConfigureBuildingTemplate(go, prefab_tag);
		go.GetComponent<KPrefabID>().AddTag(GameTags.RocketInteriorBuilding, false);
		go.AddComponent<RequireInputs>();
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x000528A0 File Offset: 0x00050AA0
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<ActiveController.Def>();
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 20f;
		RocketConduitStorageAccess rocketConduitStorageAccess = go.AddOrGet<RocketConduitStorageAccess>();
		rocketConduitStorageAccess.storage = storage;
		rocketConduitStorageAccess.cargoType = CargoBay.CargoType.Solids;
		rocketConduitStorageAccess.targetLevel = 0f;
		SolidConduitConsumer solidConduitConsumer = go.AddOrGet<SolidConduitConsumer>();
		solidConduitConsumer.alwaysConsume = true;
		solidConduitConsumer.capacityKG = storage.capacityKg;
	}

	// Token: 0x04000861 RID: 2145
	private const ConduitType CONDUIT_TYPE = ConduitType.Solid;

	// Token: 0x04000862 RID: 2146
	private const CargoBay.CargoType CARGO_TYPE = CargoBay.CargoType.Solids;

	// Token: 0x04000863 RID: 2147
	public const string ID = "RocketInteriorSolidInput";
}
