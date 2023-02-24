using System;
using TUNING;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class WireRefinedBridgeHighWattageConfig : WireBridgeHighWattageConfig
{
	// Token: 0x06001121 RID: 4385 RVA: 0x0005C794 File Offset: 0x0005A994
	protected override string GetID()
	{
		return "WireRefinedBridgeHighWattage";
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0005C79C File Offset: 0x0005A99C
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = base.CreateBuildingDef();
		buildingDef.AnimFiles = new KAnimFile[] { Assets.GetAnim("heavywatttile_conductive_kanim") };
		buildingDef.Mass = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		buildingDef.MaterialCategory = MATERIALS.REFINED_METALS;
		buildingDef.SceneLayer = Grid.SceneLayer.WireBridges;
		buildingDef.ForegroundLayer = Grid.SceneLayer.TileMain;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.WireIDs, "WireRefinedBridgeHighWattage");
		return buildingDef;
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0005C804 File Offset: 0x0005AA04
	protected override WireUtilityNetworkLink AddNetworkLink(GameObject go)
	{
		WireUtilityNetworkLink wireUtilityNetworkLink = base.AddNetworkLink(go);
		wireUtilityNetworkLink.maxWattageRating = Wire.WattageRating.Max50000;
		return wireUtilityNetworkLink;
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0005C814 File Offset: 0x0005AA14
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.CanPowerTinker.Id;
	}

	// Token: 0x04000941 RID: 2369
	public new const string ID = "WireRefinedBridgeHighWattage";
}
