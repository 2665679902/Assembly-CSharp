using System;
using TUNING;
using UnityEngine;

// Token: 0x020001E6 RID: 486
public class LeadSuitMarkerConfig : IBuildingConfig
{
	// Token: 0x06000993 RID: 2451 RVA: 0x0003755B File Offset: 0x0003575B
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00037564 File Offset: 0x00035764
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LeadSuitMarker";
		int num = 2;
		int num2 = 4;
		string text2 = "changingarea_radiation_arrow_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float[] array = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
		};
		string[] array2 = refined_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.PreventIdleTraversalPastBuilding = true;
		buildingDef.Deprecated = !Sim.IsRadiationEnabled();
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "LeadSuitMarker");
		return buildingDef;
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x000375EC File Offset: 0x000357EC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		SuitMarker suitMarker = go.AddOrGet<SuitMarker>();
		suitMarker.LockerTags = new Tag[]
		{
			new Tag("LeadSuitLocker")
		};
		suitMarker.PathFlag = PathFinder.PotentialPath.Flags.HasAtmoSuit;
		go.AddOrGet<AnimTileable>().tags = new Tag[]
		{
			new Tag("LeadSuitMarker"),
			new Tag("LeadSuitLocker")
		};
		go.AddTag(GameTags.JetSuitBlocker);
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x00037662 File Offset: 0x00035862
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040005F0 RID: 1520
	public const string ID = "LeadSuitMarker";
}
