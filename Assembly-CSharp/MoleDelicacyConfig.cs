using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class MoleDelicacyConfig : IEntityConfig
{
	// Token: 0x060004E3 RID: 1251 RVA: 0x00022054 File Offset: 0x00020254
	public static GameObject CreateMole(string id, string name, string desc, string anim_file, bool is_baby = false)
	{
		GameObject gameObject = BaseMoleConfig.BaseMole(id, name, desc, "MoleDelicacyBaseTrait", anim_file, is_baby, "del_", 5);
		gameObject.AddTag(GameTags.Creatures.Digger);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, MoleTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("MoleDelicacyBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, MoleTuning.DELICACY_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -MoleTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		Diet diet = new Diet(BaseMoleConfig.SimpleOreDiet(new List<Tag>
		{
			SimHashes.Regolith.CreateTag(),
			SimHashes.Dirt.CreateTag(),
			SimHashes.IronOre.CreateTag()
		}, MoleDelicacyConfig.CALORIES_PER_KG_OF_DIRT, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL).ToArray());
		CreatureCalorieMonitor.Def def = gameObject.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minPoopSizeInCalories = MoleDelicacyConfig.MIN_POOP_SIZE_IN_CALORIES;
		gameObject.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
		gameObject.AddOrGetDef<OvercrowdingMonitor.Def>().spaceRequiredPerCreature = 0;
		gameObject.AddOrGet<LoopingSounds>();
		if (!is_baby)
		{
			ElementGrowthMonitor.Def def2 = gameObject.AddOrGetDef<ElementGrowthMonitor.Def>();
			def2.defaultGrowthRate = 1f / MoleDelicacyConfig.GINGER_GROWTH_TIME_IN_CYCLES / 600f;
			def2.dropMass = MoleDelicacyConfig.GINGER_PER_CYCLE * MoleDelicacyConfig.GINGER_GROWTH_TIME_IN_CYCLES;
			def2.itemDroppedOnShear = MoleDelicacyConfig.SHEAR_DROP_ELEMENT;
			def2.levelCount = 5;
			def2.minTemperature = MoleDelicacyConfig.MIN_GROWTH_TEMPERATURE;
			def2.maxTemperature = MoleDelicacyConfig.MAX_GROWTH_TEMPERATURE;
		}
		else
		{
			gameObject.GetComponent<Modifiers>().initialAmounts.Add(Db.Get().Amounts.ElementGrowth.Id);
		}
		return gameObject;
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00022264 File Offset: 0x00020464
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0002226C File Offset: 0x0002046C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = MoleDelicacyConfig.CreateMole("MoleDelicacy", STRINGS.CREATURES.SPECIES.MOLE.VARIANT_DELICACY.NAME, STRINGS.CREATURES.SPECIES.MOLE.VARIANT_DELICACY.DESC, "driller_kanim", false);
		string text = "MoleDelicacyEgg";
		string text2 = STRINGS.CREATURES.SPECIES.MOLE.VARIANT_DELICACY.EGG_NAME;
		string text3 = STRINGS.CREATURES.SPECIES.MOLE.VARIANT_DELICACY.DESC;
		string text4 = "egg_driller_kanim";
		float egg_MASS = MoleTuning.EGG_MASS;
		string text5 = "MoleDelicacyBaby";
		float num = 60.000004f;
		float num2 = 20f;
		int egg_SORT_ORDER = MoleDelicacyConfig.EGG_SORT_ORDER;
		return EntityTemplates.ExtendEntityToFertileCreature(gameObject, text, text2, text3, text4, egg_MASS, text5, num, num2, MoleTuning.EGG_CHANCES_DELICACY, egg_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x000222E9 File Offset: 0x000204E9
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x000222EB File Offset: 0x000204EB
	public void OnSpawn(GameObject inst)
	{
		MoleDelicacyConfig.SetSpawnNavType(inst);
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x000222F4 File Offset: 0x000204F4
	public static void SetSpawnNavType(GameObject inst)
	{
		int num = Grid.PosToCell(inst);
		Navigator component = inst.GetComponent<Navigator>();
		if (component != null)
		{
			if (Grid.IsSolidCell(num))
			{
				component.SetCurrentNavType(NavType.Solid);
				inst.transform.SetPosition(Grid.CellToPosCBC(num, Grid.SceneLayer.FXFront));
				inst.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.FXFront);
				return;
			}
			inst.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Creatures);
		}
	}

	// Token: 0x04000339 RID: 825
	public const string ID = "MoleDelicacy";

	// Token: 0x0400033A RID: 826
	public const string BASE_TRAIT_ID = "MoleDelicacyBaseTrait";

	// Token: 0x0400033B RID: 827
	public const string EGG_ID = "MoleDelicacyEgg";

	// Token: 0x0400033C RID: 828
	private static float MIN_POOP_SIZE_IN_CALORIES = 2400000f;

	// Token: 0x0400033D RID: 829
	private static float CALORIES_PER_KG_OF_DIRT = 1000f;

	// Token: 0x0400033E RID: 830
	public static int EGG_SORT_ORDER = 800;

	// Token: 0x0400033F RID: 831
	public static float GINGER_GROWTH_TIME_IN_CYCLES = 8f;

	// Token: 0x04000340 RID: 832
	public static float GINGER_PER_CYCLE = 1f;

	// Token: 0x04000341 RID: 833
	public static Tag SHEAR_DROP_ELEMENT = GingerConfig.ID;

	// Token: 0x04000342 RID: 834
	public static float MIN_GROWTH_TEMPERATURE = 343.15f;

	// Token: 0x04000343 RID: 835
	public static float MAX_GROWTH_TEMPERATURE = 353.15f;

	// Token: 0x04000344 RID: 836
	public static float EGG_CHANCES_TEMPERATURE_MIN = 333.15f;

	// Token: 0x04000345 RID: 837
	public static float EGG_CHANCES_TEMPERATURE_MAX = 373.15f;
}
