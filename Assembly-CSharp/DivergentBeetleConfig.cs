using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000E5 RID: 229
[EntityConfigOrder(1)]
public class DivergentBeetleConfig : IEntityConfig
{
	// Token: 0x0600041C RID: 1052 RVA: 0x0001F1EC File Offset: 0x0001D3EC
	public static GameObject CreateDivergentBeetle(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseDivergentConfig.BaseDivergent(id, name, desc, 50f, anim_file, "DivergentBeetleBaseTrait", is_baby, 8f, null, "DivergentCropTended", 1, true), DivergentTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("DivergentBeetleBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, DivergentTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -DivergentTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		List<Diet.Info> list = BaseDivergentConfig.BasicSulfurDiet(SimHashes.Sucrose.CreateTag(), DivergentBeetleConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f);
		GameObject gameObject2 = BaseDivergentConfig.SetupDiet(gameObject, list, DivergentBeetleConfig.CALORIES_PER_KG_OF_ORE, DivergentBeetleConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x0001F330 File Offset: 0x0001D530
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x0001F338 File Offset: 0x0001D538
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(DivergentBeetleConfig.CreateDivergentBeetle("DivergentBeetle", STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.NAME, STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.DESC, "critter_kanim", false), "DivergentBeetleEgg", STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.EGG_NAME, STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.DESC, "egg_critter_kanim", DivergentTuning.EGG_MASS, "DivergentBeetleBaby", 45f, 15f, DivergentTuning.EGG_CHANCES_BEETLE, DivergentBeetleConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0001F3B3 File Offset: 0x0001D5B3
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x0001F3B5 File Offset: 0x0001D5B5
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002A0 RID: 672
	public const string ID = "DivergentBeetle";

	// Token: 0x040002A1 RID: 673
	public const string BASE_TRAIT_ID = "DivergentBeetleBaseTrait";

	// Token: 0x040002A2 RID: 674
	public const string EGG_ID = "DivergentBeetleEgg";

	// Token: 0x040002A3 RID: 675
	private const float LIFESPAN = 75f;

	// Token: 0x040002A4 RID: 676
	private const SimHashes EMIT_ELEMENT = SimHashes.Sucrose;

	// Token: 0x040002A5 RID: 677
	private static float KG_ORE_EATEN_PER_CYCLE = 20f;

	// Token: 0x040002A6 RID: 678
	private static float CALORIES_PER_KG_OF_ORE = DivergentTuning.STANDARD_CALORIES_PER_CYCLE / DivergentBeetleConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040002A7 RID: 679
	private static float MIN_POOP_SIZE_IN_KG = 4f;

	// Token: 0x040002A8 RID: 680
	public static int EGG_SORT_ORDER = 0;
}
