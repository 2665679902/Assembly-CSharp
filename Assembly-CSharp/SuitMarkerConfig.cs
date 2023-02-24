using System;
using TUNING;
using UnityEngine;

// Token: 0x02000330 RID: 816
public class SuitMarkerConfig : IBuildingConfig
{
	// Token: 0x06001048 RID: 4168 RVA: 0x00058758 File Offset: 0x00056958
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SuitMarker";
		int num = 1;
		int num2 = 3;
		string text2 = "changingarea_arrow_kanim";
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
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "SuitMarker");
		return buildingDef;
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x000587D4 File Offset: 0x000569D4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		SuitMarker suitMarker = go.AddOrGet<SuitMarker>();
		suitMarker.LockerTags = new Tag[]
		{
			new Tag("SuitLocker")
		};
		suitMarker.PathFlag = PathFinder.PotentialPath.Flags.HasAtmoSuit;
		go.AddOrGet<AnimTileable>().tags = new Tag[]
		{
			new Tag("SuitMarker"),
			new Tag("SuitLocker")
		};
		go.AddTag(GameTags.JetSuitBlocker);
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x0005884A File Offset: 0x00056A4A
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040008DF RID: 2271
	public const string ID = "SuitMarker";
}
