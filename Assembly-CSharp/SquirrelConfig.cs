using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class SquirrelConfig : IEntityConfig
{
	// Token: 0x0600057D RID: 1405 RVA: 0x00024B10 File Offset: 0x00022D10
	public static GameObject CreateSquirrel(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseSquirrelConfig.BaseSquirrel(id, name, desc, anim_file, "SquirrelBaseTrait", is_baby, null, false), SquirrelTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("SquirrelBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, SquirrelTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		Diet.Info[] array = BaseSquirrelConfig.BasicDiet(SimHashes.Dirt.CreateTag(), SquirrelConfig.CALORIES_PER_DAY_OF_PLANT_EATEN, SquirrelConfig.KG_POOP_PER_DAY_OF_PLANT, null, 0f);
		GameObject gameObject2 = BaseSquirrelConfig.SetupDiet(gameObject, array, SquirrelConfig.MIN_POOP_SIZE_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00024C3F File Offset: 0x00022E3F
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00024C48 File Offset: 0x00022E48
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(SquirrelConfig.CreateSquirrel("Squirrel", CREATURES.SPECIES.SQUIRREL.NAME, CREATURES.SPECIES.SQUIRREL.DESC, "squirrel_kanim", false), "SquirrelEgg", CREATURES.SPECIES.SQUIRREL.EGG_NAME, CREATURES.SPECIES.SQUIRREL.DESC, "egg_squirrel_kanim", SquirrelTuning.EGG_MASS, "SquirrelBaby", 60.000004f, 20f, SquirrelTuning.EGG_CHANCES_BASE, SquirrelConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00024CC3 File Offset: 0x00022EC3
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00024CC5 File Offset: 0x00022EC5
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003B3 RID: 947
	public const string ID = "Squirrel";

	// Token: 0x040003B4 RID: 948
	public const string BASE_TRAIT_ID = "SquirrelBaseTrait";

	// Token: 0x040003B5 RID: 949
	public const string EGG_ID = "SquirrelEgg";

	// Token: 0x040003B6 RID: 950
	public const float OXYGEN_RATE = 0.023437504f;

	// Token: 0x040003B7 RID: 951
	public const float BABY_OXYGEN_RATE = 0.011718752f;

	// Token: 0x040003B8 RID: 952
	private const SimHashes EMIT_ELEMENT = SimHashes.Dirt;

	// Token: 0x040003B9 RID: 953
	public static float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 0.4f;

	// Token: 0x040003BA RID: 954
	private static float CALORIES_PER_DAY_OF_PLANT_EATEN = SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / SquirrelConfig.DAYS_PLANT_GROWTH_EATEN_PER_CYCLE;

	// Token: 0x040003BB RID: 955
	private static float KG_POOP_PER_DAY_OF_PLANT = 50f;

	// Token: 0x040003BC RID: 956
	private static float MIN_POOP_SIZE_KG = 40f;

	// Token: 0x040003BD RID: 957
	public static int EGG_SORT_ORDER = 0;
}
