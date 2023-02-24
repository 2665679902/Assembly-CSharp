using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class DreckoPlasticConfig : IEntityConfig
{
	// Token: 0x06000440 RID: 1088 RVA: 0x0001FB0C File Offset: 0x0001DD0C
	public static GameObject CreateDrecko(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseDreckoConfig.BaseDrecko(id, name, desc, anim_file, "DreckoPlasticBaseTrait", is_baby, null, 298.15f, 333.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, DreckoTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("DreckoPlasticBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, DreckoTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -DreckoTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 150f, name, false, false, true));
		Diet diet = new Diet(new Diet.Info[]
		{
			new Diet.Info(new HashSet<Tag>
			{
				"BasicSingleHarvestPlant".ToTag(),
				"PrickleFlower".ToTag()
			}, DreckoPlasticConfig.POOP_ELEMENT, DreckoPlasticConfig.CALORIES_PER_DAY_OF_PLANT_EATEN, DreckoPlasticConfig.KG_POOP_PER_DAY_OF_PLANT, null, 0f, false, true)
		});
		CreatureCalorieMonitor.Def def = gameObject.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minPoopSizeInCalories = DreckoPlasticConfig.MIN_POOP_SIZE_IN_CALORIES;
		ScaleGrowthMonitor.Def def2 = gameObject.AddOrGetDef<ScaleGrowthMonitor.Def>();
		def2.defaultGrowthRate = 1f / DreckoPlasticConfig.SCALE_GROWTH_TIME_IN_CYCLES / 600f;
		def2.dropMass = DreckoPlasticConfig.PLASTIC_PER_CYCLE * DreckoPlasticConfig.SCALE_GROWTH_TIME_IN_CYCLES;
		def2.itemDroppedOnShear = DreckoPlasticConfig.EMIT_ELEMENT;
		def2.levelCount = 6;
		def2.targetAtmosphere = SimHashes.Hydrogen;
		gameObject.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
		return gameObject;
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x0001FCD3 File Offset: 0x0001DED3
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x0001FCDC File Offset: 0x0001DEDC
	public virtual GameObject CreatePrefab()
	{
		GameObject gameObject = DreckoPlasticConfig.CreateDrecko("DreckoPlastic", CREATURES.SPECIES.DRECKO.VARIANT_PLASTIC.NAME, CREATURES.SPECIES.DRECKO.VARIANT_PLASTIC.DESC, "drecko_kanim", false);
		string text = "DreckoPlasticEgg";
		string text2 = CREATURES.SPECIES.DRECKO.VARIANT_PLASTIC.EGG_NAME;
		string text3 = CREATURES.SPECIES.DRECKO.VARIANT_PLASTIC.DESC;
		string text4 = "egg_drecko_kanim";
		float egg_MASS = DreckoTuning.EGG_MASS;
		string text5 = "DreckoPlasticBaby";
		float num = 90f;
		float num2 = 30f;
		int egg_SORT_ORDER = DreckoPlasticConfig.EGG_SORT_ORDER;
		return EntityTemplates.ExtendEntityToFertileCreature(gameObject, text, text2, text3, text4, egg_MASS, text5, num, num2, DreckoTuning.EGG_CHANCES_PLASTIC, egg_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0001FD59 File Offset: 0x0001DF59
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0001FD5B File Offset: 0x0001DF5B
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002C7 RID: 711
	public const string ID = "DreckoPlastic";

	// Token: 0x040002C8 RID: 712
	public const string BASE_TRAIT_ID = "DreckoPlasticBaseTrait";

	// Token: 0x040002C9 RID: 713
	public const string EGG_ID = "DreckoPlasticEgg";

	// Token: 0x040002CA RID: 714
	public static Tag POOP_ELEMENT = SimHashes.Phosphorite.CreateTag();

	// Token: 0x040002CB RID: 715
	public static Tag EMIT_ELEMENT = SimHashes.Polypropylene.CreateTag();

	// Token: 0x040002CC RID: 716
	private static float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 1f;

	// Token: 0x040002CD RID: 717
	private static float CALORIES_PER_DAY_OF_PLANT_EATEN = DreckoTuning.STANDARD_CALORIES_PER_CYCLE / DreckoPlasticConfig.DAYS_PLANT_GROWTH_EATEN_PER_CYCLE;

	// Token: 0x040002CE RID: 718
	private static float KG_POOP_PER_DAY_OF_PLANT = 9f;

	// Token: 0x040002CF RID: 719
	private static float MIN_POOP_SIZE_IN_KG = 1.5f;

	// Token: 0x040002D0 RID: 720
	private static float MIN_POOP_SIZE_IN_CALORIES = DreckoPlasticConfig.CALORIES_PER_DAY_OF_PLANT_EATEN * DreckoPlasticConfig.MIN_POOP_SIZE_IN_KG / DreckoPlasticConfig.KG_POOP_PER_DAY_OF_PLANT;

	// Token: 0x040002D1 RID: 721
	public static float SCALE_GROWTH_TIME_IN_CYCLES = 3f;

	// Token: 0x040002D2 RID: 722
	public static float PLASTIC_PER_CYCLE = 50f;

	// Token: 0x040002D3 RID: 723
	public static int EGG_SORT_ORDER = 800;
}
