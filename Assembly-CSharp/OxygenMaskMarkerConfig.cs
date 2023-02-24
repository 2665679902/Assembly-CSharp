using System;
using TUNING;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class OxygenMaskMarkerConfig : IBuildingConfig
{
	// Token: 0x06000D3D RID: 3389 RVA: 0x00049848 File Offset: 0x00047A48
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x00049850 File Offset: 0x00047A50
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OxygenMaskMarker";
		int num = 1;
		int num2 = 2;
		string text2 = "oxygen_checkpoint_arrow_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float[] array = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
		};
		string[] array2 = raw_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.PreventIdleTraversalPastBuilding = true;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SuitIDs, "OxygenMaskMarker");
		return buildingDef;
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x000498CC File Offset: 0x00047ACC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		SuitMarker suitMarker = go.AddOrGet<SuitMarker>();
		suitMarker.LockerTags = new Tag[]
		{
			new Tag("OxygenMaskLocker")
		};
		suitMarker.PathFlag = PathFinder.PotentialPath.Flags.HasOxygenMask;
		go.AddOrGet<AnimTileable>().tags = new Tag[]
		{
			new Tag("OxygenMaskMarker"),
			new Tag("OxygenMaskLocker")
		};
		go.AddTag(GameTags.JetSuitBlocker);
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x00049942 File Offset: 0x00047B42
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007AA RID: 1962
	public const string ID = "OxygenMaskMarker";
}
