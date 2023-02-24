using System;
using TUNING;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class ParkSignConfig : IBuildingConfig
{
	// Token: 0x06000D58 RID: 3416 RVA: 0x0004A284 File Offset: 0x00048484
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("ParkSign", 1, 2, "parksign_kanim", 10, 10f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER1, MATERIALS.ANY_BUILDABLE, 1600f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.NONE, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		return buildingDef;
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0004A2DE File Offset: 0x000484DE
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.Park, false);
		go.AddOrGet<ParkSign>();
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0004A2F8 File Offset: 0x000484F8
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007BE RID: 1982
	public const string ID = "ParkSign";
}
