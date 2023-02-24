using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class SquirrelHugConfig : IEntityConfig
{
	// Token: 0x06000589 RID: 1417 RVA: 0x00024D58 File Offset: 0x00022F58
	public static GameObject CreateSquirrelHug(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseSquirrelConfig.BaseSquirrel(id, name, desc, anim_file, "SquirrelHugBaseTrait", is_baby, "hug_", true);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, SquirrelTuning.PEN_SIZE_PER_CREATURE_HUG);
		gameObject.AddOrGet<DecorProvider>().SetValues(DECOR.BONUS.TIER3);
		Trait trait = Db.Get().CreateTrait("SquirrelHugBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, SquirrelTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		Diet.Info[] array = BaseSquirrelConfig.BasicDiet(SimHashes.Dirt.CreateTag(), SquirrelHugConfig.CALORIES_PER_DAY_OF_PLANT_EATEN, SquirrelHugConfig.KG_POOP_PER_DAY_OF_PLANT, null, 0f);
		gameObject = BaseSquirrelConfig.SetupDiet(gameObject, array, SquirrelHugConfig.MIN_POOP_SIZE_KG);
		if (!is_baby)
		{
			gameObject.AddOrGetDef<HugMonitor.Def>();
		}
		return gameObject;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00024EA1 File Offset: 0x000230A1
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00024EA8 File Offset: 0x000230A8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(SquirrelHugConfig.CreateSquirrelHug("SquirrelHug", STRINGS.CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.NAME, STRINGS.CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.DESC, "squirrel_kanim", false), "SquirrelHugEgg", STRINGS.CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.EGG_NAME, STRINGS.CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.DESC, "egg_squirrel_kanim", SquirrelTuning.EGG_MASS, "SquirrelHugBaby", 60.000004f, 20f, SquirrelTuning.EGG_CHANCES_HUG, SquirrelHugConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00024F23 File Offset: 0x00023123
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00024F25 File Offset: 0x00023125
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003BF RID: 959
	public const string ID = "SquirrelHug";

	// Token: 0x040003C0 RID: 960
	public const string BASE_TRAIT_ID = "SquirrelHugBaseTrait";

	// Token: 0x040003C1 RID: 961
	public const string EGG_ID = "SquirrelHugEgg";

	// Token: 0x040003C2 RID: 962
	public const float OXYGEN_RATE = 0.023437504f;

	// Token: 0x040003C3 RID: 963
	public const float BABY_OXYGEN_RATE = 0.011718752f;

	// Token: 0x040003C4 RID: 964
	private const SimHashes EMIT_ELEMENT = SimHashes.Dirt;

	// Token: 0x040003C5 RID: 965
	public static float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 0.5f;

	// Token: 0x040003C6 RID: 966
	private static float CALORIES_PER_DAY_OF_PLANT_EATEN = SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / SquirrelHugConfig.DAYS_PLANT_GROWTH_EATEN_PER_CYCLE;

	// Token: 0x040003C7 RID: 967
	private static float KG_POOP_PER_DAY_OF_PLANT = 25f;

	// Token: 0x040003C8 RID: 968
	private static float MIN_POOP_SIZE_KG = 40f;

	// Token: 0x040003C9 RID: 969
	public static int EGG_SORT_ORDER = 0;
}
