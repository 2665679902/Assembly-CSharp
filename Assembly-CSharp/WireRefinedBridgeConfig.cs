using System;
using TUNING;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class WireRefinedBridgeConfig : WireBridgeConfig
{
	// Token: 0x0600111D RID: 4381 RVA: 0x0005C71A File Offset: 0x0005A91A
	protected override string GetID()
	{
		return "WireRefinedBridge";
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x0005C724 File Offset: 0x0005A924
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = base.CreateBuildingDef();
		buildingDef.AnimFiles = new KAnimFile[] { Assets.GetAnim("utilityelectricbridgeconductive_kanim") };
		buildingDef.Mass = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		buildingDef.MaterialCategory = MATERIALS.REFINED_METALS;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.WireIDs, "WireRefinedBridge");
		return buildingDef;
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x0005C77C File Offset: 0x0005A97C
	protected override WireUtilityNetworkLink AddNetworkLink(GameObject go)
	{
		WireUtilityNetworkLink wireUtilityNetworkLink = base.AddNetworkLink(go);
		wireUtilityNetworkLink.maxWattageRating = Wire.WattageRating.Max2000;
		return wireUtilityNetworkLink;
	}

	// Token: 0x04000940 RID: 2368
	public new const string ID = "WireRefinedBridge";
}
