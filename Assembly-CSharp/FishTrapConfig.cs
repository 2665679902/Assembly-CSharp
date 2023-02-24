using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200012F RID: 303
public class FishTrapConfig : IBuildingConfig
{
	// Token: 0x060005D0 RID: 1488 RVA: 0x00026270 File Offset: 0x00024470
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("FishTrap", 1, 2, "fishtrap_kanim", 10, 10f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.PLASTICS, 1600f, BuildLocationRule.Anywhere, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.Floodable = false;
		return buildingDef;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x000262C8 File Offset: 0x000244C8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.allowItemRemoval = true;
		storage.SetDefaultStoredItemModifiers(FishTrapConfig.StoredItemModifiers);
		storage.sendOnStoreOnSpawn = true;
		TrapTrigger trapTrigger = go.AddOrGet<TrapTrigger>();
		trapTrigger.trappableCreatures = new Tag[] { GameTags.Creatures.Swimmer };
		trapTrigger.trappedOffset = new Vector2(0f, 1f);
		go.AddOrGet<Trap>();
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x00026330 File Offset: 0x00024530
	public override void DoPostConfigureComplete(GameObject go)
	{
		Lure.Def def = go.AddOrGetDef<Lure.Def>();
		def.lurePoints = new CellOffset[]
		{
			new CellOffset(0, 0)
		};
		def.radius = 32;
		def.initialLures = new Tag[] { GameTags.Creatures.FishTrapLure };
	}

	// Token: 0x040003FE RID: 1022
	public const string ID = "FishTrap";

	// Token: 0x040003FF RID: 1023
	private static readonly List<Storage.StoredItemModifier> StoredItemModifiers = new List<Storage.StoredItemModifier>();
}
