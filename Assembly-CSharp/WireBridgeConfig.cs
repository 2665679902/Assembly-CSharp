using System;
using TUNING;
using UnityEngine;

// Token: 0x0200034E RID: 846
public class WireBridgeConfig : IBuildingConfig
{
	// Token: 0x06001106 RID: 4358 RVA: 0x0005C344 File Offset: 0x0005A544
	protected virtual string GetID()
	{
		return "WireBridge";
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x0005C34C File Offset: 0x0005A54C
	public override BuildingDef CreateBuildingDef()
	{
		string id = this.GetID();
		int num = 3;
		int num2 = 1;
		string text = "utilityelectricbridge_kanim";
		int num3 = 30;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.WireBridge;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.ObjectLayer = ObjectLayer.WireConnectors;
		buildingDef.SceneLayer = Grid.SceneLayer.WireBridges;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 2);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.WireIDs, "WireBridge");
		return buildingDef;
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0005C415 File Offset: 0x0005A615
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0005C41D File Offset: 0x0005A61D
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		this.AddNetworkLink(go).visualizeOnly = true;
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x0005C43B File Offset: 0x0005A63B
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		this.AddNetworkLink(go).visualizeOnly = true;
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0005C458 File Offset: 0x0005A658
	public override void DoPostConfigureComplete(GameObject go)
	{
		this.AddNetworkLink(go).visualizeOnly = false;
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x0005C46E File Offset: 0x0005A66E
	protected virtual WireUtilityNetworkLink AddNetworkLink(GameObject go)
	{
		WireUtilityNetworkLink wireUtilityNetworkLink = go.AddOrGet<WireUtilityNetworkLink>();
		wireUtilityNetworkLink.maxWattageRating = Wire.WattageRating.Max1000;
		wireUtilityNetworkLink.link1 = new CellOffset(-1, 0);
		wireUtilityNetworkLink.link2 = new CellOffset(1, 0);
		return wireUtilityNetworkLink;
	}

	// Token: 0x0400093C RID: 2364
	public const string ID = "WireBridge";
}
