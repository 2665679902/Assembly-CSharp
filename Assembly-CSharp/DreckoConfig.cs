using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class DreckoConfig : IEntityConfig
{
	// Token: 0x06000434 RID: 1076 RVA: 0x0001F7B0 File Offset: 0x0001D9B0
	public static GameObject CreateDrecko(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseDreckoConfig.BaseDrecko(id, name, desc, anim_file, "DreckoBaseTrait", is_baby, "fbr_", 308.15f, 363.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, DreckoTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("DreckoBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, DreckoTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -DreckoTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 150f, name, false, false, true));
		Diet diet = new Diet(new Diet.Info[]
		{
			new Diet.Info(new HashSet<Tag>
			{
				"SpiceVine".ToTag(),
				SwampLilyConfig.ID.ToTag(),
				"BasicSingleHarvestPlant".ToTag()
			}, DreckoConfig.POOP_ELEMENT, DreckoConfig.CALORIES_PER_DAY_OF_PLANT_EATEN, DreckoConfig.KG_POOP_PER_DAY_OF_PLANT, null, 0f, false, true)
		});
		CreatureCalorieMonitor.Def def = gameObject.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minPoopSizeInCalories = DreckoConfig.MIN_POOP_SIZE_IN_CALORIES;
		gameObject.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
		ScaleGrowthMonitor.Def def2 = gameObject.AddOrGetDef<ScaleGrowthMonitor.Def>();
		def2.defaultGrowthRate = 1f / DreckoConfig.SCALE_GROWTH_TIME_IN_CYCLES / 600f;
		def2.dropMass = DreckoConfig.FIBER_PER_CYCLE * DreckoConfig.SCALE_GROWTH_TIME_IN_CYCLES;
		def2.itemDroppedOnShear = DreckoConfig.EMIT_ELEMENT;
		def2.levelCount = 6;
		def2.targetAtmosphere = SimHashes.Hydrogen;
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x0001F997 File Offset: 0x0001DB97
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0001F9A0 File Offset: 0x0001DBA0
	public virtual GameObject CreatePrefab()
	{
		GameObject gameObject = DreckoConfig.CreateDrecko("Drecko", CREATURES.SPECIES.DRECKO.NAME, CREATURES.SPECIES.DRECKO.DESC, "drecko_kanim", false);
		string text = "DreckoEgg";
		string text2 = CREATURES.SPECIES.DRECKO.EGG_NAME;
		string text3 = CREATURES.SPECIES.DRECKO.DESC;
		string text4 = "egg_drecko_kanim";
		float egg_MASS = DreckoTuning.EGG_MASS;
		string text5 = "DreckoBaby";
		float num = 90f;
		float num2 = 30f;
		int egg_SORT_ORDER = DreckoConfig.EGG_SORT_ORDER;
		return EntityTemplates.ExtendEntityToFertileCreature(gameObject, text, text2, text3, text4, egg_MASS, text5, num, num2, DreckoTuning.EGG_CHANCES_BASE, egg_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x0001FA1D File Offset: 0x0001DC1D
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x0001FA1F File Offset: 0x0001DC1F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002B9 RID: 697
	public const string ID = "Drecko";

	// Token: 0x040002BA RID: 698
	public const string BASE_TRAIT_ID = "DreckoBaseTrait";

	// Token: 0x040002BB RID: 699
	public const string EGG_ID = "DreckoEgg";

	// Token: 0x040002BC RID: 700
	public static Tag POOP_ELEMENT = SimHashes.Phosphorite.CreateTag();

	// Token: 0x040002BD RID: 701
	public static Tag EMIT_ELEMENT = BasicFabricConfig.ID;

	// Token: 0x040002BE RID: 702
	private static float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 0.75f;

	// Token: 0x040002BF RID: 703
	private static float CALORIES_PER_DAY_OF_PLANT_EATEN = DreckoTuning.STANDARD_CALORIES_PER_CYCLE / DreckoConfig.DAYS_PLANT_GROWTH_EATEN_PER_CYCLE;

	// Token: 0x040002C0 RID: 704
	private static float KG_POOP_PER_DAY_OF_PLANT = 13.33f;

	// Token: 0x040002C1 RID: 705
	private static float MIN_POOP_SIZE_IN_KG = 1.5f;

	// Token: 0x040002C2 RID: 706
	private static float MIN_POOP_SIZE_IN_CALORIES = DreckoConfig.CALORIES_PER_DAY_OF_PLANT_EATEN * DreckoConfig.MIN_POOP_SIZE_IN_KG / DreckoConfig.KG_POOP_PER_DAY_OF_PLANT;

	// Token: 0x040002C3 RID: 707
	public static float SCALE_GROWTH_TIME_IN_CYCLES = 8f;

	// Token: 0x040002C4 RID: 708
	public static float FIBER_PER_CYCLE = 0.25f;

	// Token: 0x040002C5 RID: 709
	public static int EGG_SORT_ORDER = 800;
}
