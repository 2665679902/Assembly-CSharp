using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000F0 RID: 240
[EntityConfigOrder(1)]
public class HatchHardConfig : IEntityConfig
{
	// Token: 0x0600045D RID: 1117 RVA: 0x0002039C File Offset: 0x0001E59C
	public static GameObject CreateHatch(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseHatchConfig.BaseHatch(id, name, desc, anim_file, "HatchHardBaseTrait", is_baby, "hvy_"), HatchTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("HatchHardBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, HatchTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -HatchTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 200f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseHatchConfig.HardRockDiet(SimHashes.Carbon.CreateTag(), HatchHardConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f);
		list.AddRange(BaseHatchConfig.MetalDiet(SimHashes.Carbon.CreateTag(), HatchHardConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_1, null, 0f));
		return BaseHatchConfig.SetupDiet(gameObject, list, HatchHardConfig.CALORIES_PER_KG_OF_ORE, HatchHardConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x000204ED File Offset: 0x0001E6ED
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x000204F4 File Offset: 0x0001E6F4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(HatchHardConfig.CreateHatch("HatchHard", STRINGS.CREATURES.SPECIES.HATCH.VARIANT_HARD.NAME, STRINGS.CREATURES.SPECIES.HATCH.VARIANT_HARD.DESC, "hatch_kanim", false), "HatchHardEgg", STRINGS.CREATURES.SPECIES.HATCH.VARIANT_HARD.EGG_NAME, STRINGS.CREATURES.SPECIES.HATCH.VARIANT_HARD.DESC, "egg_hatch_kanim", HatchTuning.EGG_MASS, "HatchHardBaby", 60.000004f, 20f, HatchTuning.EGG_CHANCES_HARD, HatchHardConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x0002056F File Offset: 0x0001E76F
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x00020571 File Offset: 0x0001E771
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002E7 RID: 743
	public const string ID = "HatchHard";

	// Token: 0x040002E8 RID: 744
	public const string BASE_TRAIT_ID = "HatchHardBaseTrait";

	// Token: 0x040002E9 RID: 745
	public const string EGG_ID = "HatchHardEgg";

	// Token: 0x040002EA RID: 746
	private const SimHashes EMIT_ELEMENT = SimHashes.Carbon;

	// Token: 0x040002EB RID: 747
	private static float KG_ORE_EATEN_PER_CYCLE = 140f;

	// Token: 0x040002EC RID: 748
	private static float CALORIES_PER_KG_OF_ORE = HatchTuning.STANDARD_CALORIES_PER_CYCLE / HatchHardConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040002ED RID: 749
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x040002EE RID: 750
	public static int EGG_SORT_ORDER = HatchConfig.EGG_SORT_ORDER + 2;
}
