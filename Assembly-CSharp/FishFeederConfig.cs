using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class FishFeederConfig : IBuildingConfig
{
	// Token: 0x060005C5 RID: 1477 RVA: 0x00025F74 File Offset: 0x00024174
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FishFeeder";
		int num = 1;
		int num2 = 3;
		string text2 = "fishfeeder_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, TUNING.BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.Entombable = true;
		buildingDef.Floodable = true;
		buildingDef.ForegroundLayer = Grid.SceneLayer.TileMain;
		return buildingDef;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x00025FDB File Offset: 0x000241DB
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x00025FE0 File Offset: 0x000241E0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 200f;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.allowItemRemoval = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.showCapacityStatusItem = true;
		storage.showCapacityAsMainStatus = true;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.capacityKg = 200f;
		storage2.showInUI = true;
		storage2.showDescriptor = true;
		storage2.allowItemRemoval = false;
		go.AddOrGet<StorageLocker>().choreTypeID = Db.Get().ChoreTypes.RanchingFetch.Id;
		go.AddOrGet<UserNameable>();
		Effect effect = new Effect("AteFromFeeder", STRINGS.CREATURES.MODIFIERS.ATE_FROM_FEEDER.NAME, STRINGS.CREATURES.MODIFIERS.ATE_FROM_FEEDER.TOOLTIP, 600f, true, false, false, null, -1f, 0f, null, "");
		effect.Add(new AttributeModifier(Db.Get().Amounts.Wildness.deltaAttribute.Id, -0.033333335f, STRINGS.CREATURES.MODIFIERS.ATE_FROM_FEEDER.NAME, false, false, true));
		effect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, 2f, STRINGS.CREATURES.MODIFIERS.ATE_FROM_FEEDER.NAME, false, false, true));
		Db.Get().effects.Add(effect);
		go.AddOrGet<TreeFilterable>();
		go.AddOrGet<CreatureFeeder>().effectId = effect.Id;
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00026140 File Offset: 0x00024340
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<StorageController.Def>();
		go.AddOrGetDef<FishFeeder.Def>();
		go.AddOrGetDef<MakeBaseSolid.Def>().solidOffsets = new CellOffset[]
		{
			new CellOffset(0, 0)
		};
		SymbolOverrideControllerUtil.AddToPrefab(go);
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00026178 File Offset: 0x00024378
	public override void ConfigurePost(BuildingDef def)
	{
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, Diet> keyValuePair in DietManager.CollectDiets(new Tag[] { GameTags.Creatures.Species.PacuSpecies }))
		{
			list.Add(keyValuePair.Key);
		}
		def.BuildingComplete.GetComponent<Storage>().storageFilters = list;
	}

	// Token: 0x040003FC RID: 1020
	public const string ID = "FishFeeder";
}
