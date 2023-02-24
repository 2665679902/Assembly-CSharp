using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class MooConfig : IEntityConfig
{
	// Token: 0x060004F0 RID: 1264 RVA: 0x00022430 File Offset: 0x00020630
	public static GameObject CreateMoo(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseMooConfig.BaseMoo(id, name, CREATURES.SPECIES.MOO.DESC, "MooBaseTrait", anim_file, is_baby, null);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, MooTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("MooBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, MooTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -MooTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		Diet diet = new Diet(new Diet.Info[]
		{
			new Diet.Info(new HashSet<Tag> { "GasGrass".ToTag() }, MooConfig.POOP_ELEMENT, MooConfig.CALORIES_PER_DAY_OF_PLANT_EATEN, MooConfig.KG_POOP_PER_DAY_OF_PLANT, null, 0f, false, true)
		});
		CreatureCalorieMonitor.Def def = gameObject.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minPoopSizeInCalories = MooConfig.MIN_POOP_SIZE_IN_CALORIES;
		gameObject.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x000225A6 File Offset: 0x000207A6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x000225AD File Offset: 0x000207AD
	public GameObject CreatePrefab()
	{
		return MooConfig.CreateMoo("Moo", CREATURES.SPECIES.MOO.NAME, CREATURES.SPECIES.MOO.DESC, "gassy_moo_kanim", false);
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x000225D3 File Offset: 0x000207D3
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x000225D5 File Offset: 0x000207D5
	public void OnSpawn(GameObject inst)
	{
		BaseMooConfig.OnSpawn(inst);
	}

	// Token: 0x04000347 RID: 839
	public const string ID = "Moo";

	// Token: 0x04000348 RID: 840
	public const string BASE_TRAIT_ID = "MooBaseTrait";

	// Token: 0x04000349 RID: 841
	public const SimHashes CONSUME_ELEMENT = SimHashes.Carbon;

	// Token: 0x0400034A RID: 842
	public static Tag POOP_ELEMENT = SimHashes.Methane.CreateTag();

	// Token: 0x0400034B RID: 843
	private static float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 2f;

	// Token: 0x0400034C RID: 844
	private static float CALORIES_PER_DAY_OF_PLANT_EATEN = MooTuning.STANDARD_CALORIES_PER_CYCLE / MooConfig.DAYS_PLANT_GROWTH_EATEN_PER_CYCLE;

	// Token: 0x0400034D RID: 845
	private static float KG_POOP_PER_DAY_OF_PLANT = 5f;

	// Token: 0x0400034E RID: 846
	private static float MIN_POOP_SIZE_IN_KG = 1.5f;

	// Token: 0x0400034F RID: 847
	private static float MIN_POOP_SIZE_IN_CALORIES = MooConfig.CALORIES_PER_DAY_OF_PLANT_EATEN * MooConfig.MIN_POOP_SIZE_IN_KG / MooConfig.KG_POOP_PER_DAY_OF_PLANT;
}
