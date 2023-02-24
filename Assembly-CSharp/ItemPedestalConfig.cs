using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class ItemPedestalConfig : IBuildingConfig
{
	// Token: 0x06000943 RID: 2371 RVA: 0x00035BBC File Offset: 0x00033DBC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ItemPedestal";
		int num = 1;
		int num2 = 2;
		string text2 = "pedestal_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.DefaultAnimState = "pedestal";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "small";
		return buildingDef;
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x00035C3C File Offset: 0x00033E3C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>(new Storage.StoredItemModifier[]
		{
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Preserve
		}));
		Prioritizable.AddRef(go);
		SingleEntityReceptacle singleEntityReceptacle = go.AddOrGet<SingleEntityReceptacle>();
		singleEntityReceptacle.AddDepositTag(GameTags.PedestalDisplayable);
		singleEntityReceptacle.occupyingObjectRelativePosition = new Vector3(0f, 1.2f, -1f);
		go.AddOrGet<DecorProvider>();
		go.AddOrGet<ItemPedestal>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00035CB6 File Offset: 0x00033EB6
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040005CB RID: 1483
	public const string ID = "ItemPedestal";
}
