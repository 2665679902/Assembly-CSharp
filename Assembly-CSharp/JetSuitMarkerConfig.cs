using System;
using TUNING;
using UnityEngine;

// Token: 0x020001D8 RID: 472
public class JetSuitMarkerConfig : IBuildingConfig
{
	// Token: 0x0600094E RID: 2382 RVA: 0x00035E74 File Offset: 0x00034074
	public override BuildingDef CreateBuildingDef()
	{
		string text = "JetSuitMarker";
		int num = 2;
		int num2 = 4;
		string text2 = "changingarea_jetsuit_arrow_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float[] array = new float[] { BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0] };
		string[] array2 = refined_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.PreventIdleTraversalPastBuilding = true;
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingUse;
		buildingDef.ForegroundLayer = Grid.SceneLayer.TileMain;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "JetSuitMarker");
		return buildingDef;
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00035EF4 File Offset: 0x000340F4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		SuitMarker suitMarker = go.AddOrGet<SuitMarker>();
		suitMarker.LockerTags = new Tag[]
		{
			new Tag("JetSuitLocker")
		};
		suitMarker.PathFlag = PathFinder.PotentialPath.Flags.HasJetPack;
		suitMarker.interactAnim = Assets.GetAnim("anim_interacts_changingarea_jetsuit_arrow_kanim");
		go.AddOrGet<AnimTileable>().tags = new Tag[]
		{
			new Tag("JetSuitMarker"),
			new Tag("JetSuitLocker")
		};
		go.AddTag(GameTags.JetSuitBlocker);
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00035F7F File Offset: 0x0003417F
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040005D0 RID: 1488
	public const string ID = "JetSuitMarker";
}
