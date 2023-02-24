using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x020001B4 RID: 436
public class GravitasPedestalConfig : IBuildingConfig
{
	// Token: 0x06000888 RID: 2184 RVA: 0x000327EC File Offset: 0x000309EC
	public override string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x000327F4 File Offset: 0x000309F4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GravitasPedestal";
		int num = 1;
		int num2 = 2;
		string text2 = "gravitas_pedestal_nice_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.DefaultAnimState = "pedestal_nice";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "small";
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0003287C File Offset: 0x00030A7C
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
		go.AddOrGet<PedestalArtifactSpawner>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x000328FD File Offset: 0x00030AFD
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400055A RID: 1370
	public const string ID = "GravitasPedestal";
}
