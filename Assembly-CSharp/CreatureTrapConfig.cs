using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class CreatureTrapConfig : IBuildingConfig
{
	// Token: 0x0600018C RID: 396 RVA: 0x0000B868 File Offset: 0x00009A68
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("CreatureTrap", 2, 1, "creaturetrap_kanim", 10, 10f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.PLASTICS, 1600f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.Floodable = false;
		return buildingDef;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000B8C0 File Offset: 0x00009AC0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.allowItemRemoval = true;
		storage.SetDefaultStoredItemModifiers(CreatureTrapConfig.StoredItemModifiers);
		storage.sendOnStoreOnSpawn = true;
		TrapTrigger trapTrigger = go.AddOrGet<TrapTrigger>();
		trapTrigger.trappableCreatures = new Tag[]
		{
			GameTags.Creatures.Walker,
			GameTags.Creatures.Hoverer
		};
		trapTrigger.trappedOffset = new Vector2(0.5f, 0f);
		go.AddOrGet<Trap>();
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000B932 File Offset: 0x00009B32
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040000E3 RID: 227
	public const string ID = "CreatureTrap";

	// Token: 0x040000E4 RID: 228
	private static readonly List<Storage.StoredItemModifier> StoredItemModifiers = new List<Storage.StoredItemModifier>();
}
