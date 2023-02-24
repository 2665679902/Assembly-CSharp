﻿using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000E3 RID: 227
[EntityConfigOrder(1)]
public class CrabWoodConfig : IEntityConfig
{
	// Token: 0x06000410 RID: 1040 RVA: 0x0001EF34 File Offset: 0x0001D134
	public static GameObject CreateCrabWood(string id, string name, string desc, string anim_file, bool is_baby, string deathDropID = "CrabWoodShell")
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseCrabConfig.BaseCrab(id, name, desc, anim_file, "CrabWoodBaseTrait", is_baby, CrabWoodConfig.animPrefix, deathDropID, 5), CrabTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("CrabWoodBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, CrabTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -CrabTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseCrabConfig.DietWithSlime(SimHashes.Sand.CreateTag(), CrabWoodConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_1, null, 0f);
		return BaseCrabConfig.SetupDiet(gameObject, list, CrabWoodConfig.CALORIES_PER_KG_OF_ORE, CrabWoodConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0001F063 File Offset: 0x0001D263
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0001F06C File Offset: 0x0001D26C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabWoodConfig.CreateCrabWood("CrabWood", STRINGS.CREATURES.SPECIES.CRAB.VARIANT_WOOD.NAME, STRINGS.CREATURES.SPECIES.CRAB.VARIANT_WOOD.DESC, "pincher_kanim", false, "CrabWoodShell");
		gameObject = EntityTemplates.ExtendEntityToFertileCreature(gameObject, "CrabWoodEgg", STRINGS.CREATURES.SPECIES.CRAB.VARIANT_WOOD.EGG_NAME, STRINGS.CREATURES.SPECIES.CRAB.VARIANT_WOOD.DESC, "egg_pincher_kanim", CrabTuning.EGG_MASS, "CrabWoodBaby", 60.000004f, 20f, CrabTuning.EGG_CHANCES_WOOD, CrabWoodConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		EggProtectionMonitor.Def def = gameObject.AddOrGetDef<EggProtectionMonitor.Def>();
		def.allyTags = new Tag[] { GameTags.Creatures.CrabFriend };
		def.animPrefix = CrabWoodConfig.animPrefix;
		MoltDropperMonitor.Def def2 = gameObject.AddOrGetDef<MoltDropperMonitor.Def>();
		def2.onGrowDropID = "CrabWoodShell";
		def2.massToDrop = 100f;
		def2.blockedElement = SimHashes.Ethanol;
		return gameObject;
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0001F140 File Offset: 0x0001D340
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x0001F142 File Offset: 0x0001D342
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000296 RID: 662
	public const string ID = "CrabWood";

	// Token: 0x04000297 RID: 663
	public const string BASE_TRAIT_ID = "CrabWoodBaseTrait";

	// Token: 0x04000298 RID: 664
	public const string EGG_ID = "CrabWoodEgg";

	// Token: 0x04000299 RID: 665
	private const SimHashes EMIT_ELEMENT = SimHashes.Sand;

	// Token: 0x0400029A RID: 666
	private static float KG_ORE_EATEN_PER_CYCLE = 70f;

	// Token: 0x0400029B RID: 667
	private static float CALORIES_PER_KG_OF_ORE = CrabTuning.STANDARD_CALORIES_PER_CYCLE / CrabWoodConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x0400029C RID: 668
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x0400029D RID: 669
	public static int EGG_SORT_ORDER = 0;

	// Token: 0x0400029E RID: 670
	private static string animPrefix = "wood_";
}
