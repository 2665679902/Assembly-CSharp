using System;
using TUNING;
using UnityEngine;

// Token: 0x02000227 RID: 551
public class MachineShopConfig : IBuildingConfig
{
	// Token: 0x06000AE5 RID: 2789 RVA: 0x0003D794 File Offset: 0x0003B994
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MachineShop";
		int num = 4;
		int num2 = 2;
		string text2 = "machineshop_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.Deprecated = true;
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x0003D809 File Offset: 0x0003BA09
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.MachineShopType, false);
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x0003D823 File Offset: 0x0003BA23
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400065F RID: 1631
	public const string ID = "MachineShop";

	// Token: 0x04000660 RID: 1632
	public static readonly Tag MATERIAL_FOR_TINKER = GameTags.RefinedMetal;

	// Token: 0x04000661 RID: 1633
	public const float MASS_PER_TINKER = 5f;

	// Token: 0x04000662 RID: 1634
	public static readonly string ROLE_PERK = "IncreaseMachinery";
}
