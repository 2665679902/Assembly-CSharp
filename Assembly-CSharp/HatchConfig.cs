using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000EE RID: 238
[EntityConfigOrder(1)]
public class HatchConfig : IEntityConfig
{
	// Token: 0x06000451 RID: 1105 RVA: 0x00020138 File Offset: 0x0001E338
	public static GameObject CreateHatch(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseHatchConfig.BaseHatch(id, name, desc, anim_file, "HatchBaseTrait", is_baby, null), HatchTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("HatchBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, HatchTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -HatchTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseHatchConfig.BasicRockDiet(SimHashes.Carbon.CreateTag(), HatchConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f);
		list.AddRange(BaseHatchConfig.FoodDiet(SimHashes.Carbon.CreateTag(), HatchConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_1, null, 0f));
		GameObject gameObject2 = BaseHatchConfig.SetupDiet(gameObject, list, HatchConfig.CALORIES_PER_KG_OF_ORE, HatchConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00020290 File Offset: 0x0001E490
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x00020298 File Offset: 0x0001E498
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(HatchConfig.CreateHatch("Hatch", STRINGS.CREATURES.SPECIES.HATCH.NAME, STRINGS.CREATURES.SPECIES.HATCH.DESC, "hatch_kanim", false), "HatchEgg", STRINGS.CREATURES.SPECIES.HATCH.EGG_NAME, STRINGS.CREATURES.SPECIES.HATCH.DESC, "egg_hatch_kanim", HatchTuning.EGG_MASS, "HatchBaby", 60.000004f, 20f, HatchTuning.EGG_CHANCES_BASE, HatchConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x00020313 File Offset: 0x0001E513
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x00020315 File Offset: 0x0001E515
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002DE RID: 734
	public const string ID = "Hatch";

	// Token: 0x040002DF RID: 735
	public const string BASE_TRAIT_ID = "HatchBaseTrait";

	// Token: 0x040002E0 RID: 736
	public const string EGG_ID = "HatchEgg";

	// Token: 0x040002E1 RID: 737
	private const SimHashes EMIT_ELEMENT = SimHashes.Carbon;

	// Token: 0x040002E2 RID: 738
	private static float KG_ORE_EATEN_PER_CYCLE = 140f;

	// Token: 0x040002E3 RID: 739
	private static float CALORIES_PER_KG_OF_ORE = HatchTuning.STANDARD_CALORIES_PER_CYCLE / HatchConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040002E4 RID: 740
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x040002E5 RID: 741
	public static int EGG_SORT_ORDER = 0;
}
