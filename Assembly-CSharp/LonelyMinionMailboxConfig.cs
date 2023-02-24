using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000225 RID: 549
public class LonelyMinionMailboxConfig : IBuildingConfig
{
	// Token: 0x06000ADC RID: 2780 RVA: 0x0003D55C File Offset: 0x0003B75C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LonelyMailBox";
		int num = 2;
		int num2 = 2;
		string text2 = "parcel_delivery_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingBack;
		buildingDef.DefaultAnimState = "idle";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ShowInBuildMenu = false;
		buildingDef.ViewMode = OverlayModes.None.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		return buildingDef;
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0003D5EC File Offset: 0x0003B7EC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		SingleEntityReceptacle singleEntityReceptacle = go.AddComponent<SingleEntityReceptacle>();
		singleEntityReceptacle.AddDepositTag(GameTags.Edible);
		singleEntityReceptacle.enabled = false;
		go.AddComponent<LonelyMinionMailbox>();
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
		Storage storage = go.AddOrGet<Storage>();
		storage.allowItemRemoval = false;
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Preserve
		});
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0003D64E File Offset: 0x0003B84E
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400065C RID: 1628
	public const string ID = "LonelyMailBox";

	// Token: 0x0400065D RID: 1629
	public static readonly HashedString IdHash = "LonelyMailBox";
}
