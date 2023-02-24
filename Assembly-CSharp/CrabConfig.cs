using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000DF RID: 223
[EntityConfigOrder(1)]
public class CrabConfig : IEntityConfig
{
	// Token: 0x060003F8 RID: 1016 RVA: 0x0001E8F4 File Offset: 0x0001CAF4
	public static GameObject CreateCrab(string id, string name, string desc, string anim_file, bool is_baby, string deathDropID)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseCrabConfig.BaseCrab(id, name, desc, anim_file, "CrabBaseTrait", is_baby, null, deathDropID, 1), CrabTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("CrabBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, CrabTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -CrabTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseCrabConfig.BasicDiet(SimHashes.Sand.CreateTag(), CrabConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f);
		GameObject gameObject2 = BaseCrabConfig.SetupDiet(gameObject, list, CrabConfig.CALORIES_PER_KG_OF_ORE, CrabConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x0001EA2A File Offset: 0x0001CC2A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0001EA34 File Offset: 0x0001CC34
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabConfig.CreateCrab("Crab", STRINGS.CREATURES.SPECIES.CRAB.NAME, STRINGS.CREATURES.SPECIES.CRAB.DESC, "pincher_kanim", false, "CrabShell");
		gameObject = EntityTemplates.ExtendEntityToFertileCreature(gameObject, "CrabEgg", STRINGS.CREATURES.SPECIES.CRAB.EGG_NAME, STRINGS.CREATURES.SPECIES.CRAB.DESC, "egg_pincher_kanim", CrabTuning.EGG_MASS, "CrabBaby", 60.000004f, 20f, CrabTuning.EGG_CHANCES_BASE, CrabConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		gameObject.AddOrGetDef<EggProtectionMonitor.Def>().allyTags = new Tag[] { GameTags.Creatures.CrabFriend };
		return gameObject;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x0001EAD5 File Offset: 0x0001CCD5
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x0001EAD7 File Offset: 0x0001CCD7
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000283 RID: 643
	public const string ID = "Crab";

	// Token: 0x04000284 RID: 644
	public const string BASE_TRAIT_ID = "CrabBaseTrait";

	// Token: 0x04000285 RID: 645
	public const string EGG_ID = "CrabEgg";

	// Token: 0x04000286 RID: 646
	private const SimHashes EMIT_ELEMENT = SimHashes.Sand;

	// Token: 0x04000287 RID: 647
	private static float KG_ORE_EATEN_PER_CYCLE = 70f;

	// Token: 0x04000288 RID: 648
	private static float CALORIES_PER_KG_OF_ORE = CrabTuning.STANDARD_CALORIES_PER_CYCLE / CrabConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000289 RID: 649
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x0400028A RID: 650
	public static int EGG_SORT_ORDER = 0;
}
