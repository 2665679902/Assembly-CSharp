using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class GasBottlerConfig : IBuildingConfig
{
	// Token: 0x060007CE RID: 1998 RVA: 0x0002DC94 File Offset: 0x0002BE94
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("GasBottler", 3, 2, "gas_bottler_kanim", 100, 120f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER4, MATERIALS.ALL_METALS, 800f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER1, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.Floodable = false;
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.GasVentIDs, "GasBottler");
		return buildingDef;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0002DD18 File Offset: 0x0002BF18
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.showDescriptor = true;
		storage.storageFilters = STORAGEFILTERS.GASES;
		storage.capacityKg = 25f;
		storage.allowItemRemoval = false;
		go.AddOrGet<DropAllWorkable>().removeTags = new List<Tag> { GameTags.GasSource };
		GasBottler gasBottler = go.AddOrGet<GasBottler>();
		gasBottler.storage = storage;
		gasBottler.workTime = 9f;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.storage = storage;
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.ignoreMinMassCheck = true;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.alwaysConsume = true;
		conduitConsumer.capacityKG = storage.capacityKg;
		conduitConsumer.keepZeroMassObject = false;
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0002DDBE File Offset: 0x0002BFBE
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x04000502 RID: 1282
	public const string ID = "GasBottler";

	// Token: 0x04000503 RID: 1283
	private const ConduitType CONDUIT_TYPE = ConduitType.Gas;

	// Token: 0x04000504 RID: 1284
	private const int WIDTH = 3;

	// Token: 0x04000505 RID: 1285
	private const int HEIGHT = 2;
}
