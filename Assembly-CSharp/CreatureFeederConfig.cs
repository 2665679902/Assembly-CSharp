using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class CreatureFeederConfig : IBuildingConfig
{
	// Token: 0x06000186 RID: 390 RVA: 0x0000B6C0 File Offset: 0x000098C0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CreatureFeeder";
		int num = 1;
		int num2 = 2;
		string text2 = "feeder_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000B711 File Offset: 0x00009911
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000B714 File Offset: 0x00009914
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 2000f;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.allowItemRemoval = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.showCapacityStatusItem = true;
		storage.showCapacityAsMainStatus = true;
		go.AddOrGet<StorageLocker>().choreTypeID = Db.Get().ChoreTypes.RanchingFetch.Id;
		go.AddOrGet<UserNameable>();
		go.AddOrGet<TreeFilterable>();
		go.AddOrGet<CreatureFeeder>();
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000B795 File Offset: 0x00009995
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<StorageController.Def>();
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0000B7A0 File Offset: 0x000099A0
	public override void ConfigurePost(BuildingDef def)
	{
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, Diet> keyValuePair in DietManager.CollectDiets(new Tag[]
		{
			GameTags.Creatures.Species.LightBugSpecies,
			GameTags.Creatures.Species.HatchSpecies,
			GameTags.Creatures.Species.MoleSpecies,
			GameTags.Creatures.Species.CrabSpecies,
			GameTags.Creatures.Species.StaterpillarSpecies,
			GameTags.Creatures.Species.DivergentSpecies
		}))
		{
			list.Add(keyValuePair.Key);
		}
		def.BuildingComplete.GetComponent<Storage>().storageFilters = list;
	}

	// Token: 0x040000E2 RID: 226
	public const string ID = "CreatureFeeder";
}
