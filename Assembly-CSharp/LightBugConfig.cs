using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class LightBugConfig : IEntityConfig
{
	// Token: 0x0600049A RID: 1178 RVA: 0x00021074 File Offset: 0x0001F274
	public static GameObject CreateLightBug(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseLightBugConfig.BaseLightBug(id, name, desc, anim_file, "LightBugBaseTrait", LIGHT2D.LIGHTBUG_COLOR, DECOR.BONUS.TIER4, is_baby, null);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, LightBugTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("LightBugBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, LightBugTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -LightBugTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 25f, name, false, false, true));
		GameObject gameObject2 = BaseLightBugConfig.SetupDiet(gameObject, new HashSet<Tag>
		{
			TagManager.Create(PrickleFruitConfig.ID),
			TagManager.Create("GrilledPrickleFruit"),
			SimHashes.Phosphorite.CreateTag()
		}, Tag.Invalid, LightBugConfig.CALORIES_PER_KG_OF_ORE);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x000211CC File Offset: 0x0001F3CC
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x000211D4 File Offset: 0x0001F3D4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugConfig.CreateLightBug("LightBug", STRINGS.CREATURES.SPECIES.LIGHTBUG.NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.DESC, "lightbug_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, "LightBugEgg", STRINGS.CREATURES.SPECIES.LIGHTBUG.EGG_NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.DESC, "egg_lightbug_kanim", LightBugTuning.EGG_MASS, "LightBugBaby", 15.000001f, 5f, LightBugTuning.EGG_CHANCES_BASE, LightBugConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		gameObject.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.LightSource, false);
		return gameObject;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00021262 File Offset: 0x0001F462
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00021264 File Offset: 0x0001F464
	public void OnSpawn(GameObject inst)
	{
		BaseLightBugConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x0400030F RID: 783
	public const string ID = "LightBug";

	// Token: 0x04000310 RID: 784
	public const string BASE_TRAIT_ID = "LightBugBaseTrait";

	// Token: 0x04000311 RID: 785
	public const string EGG_ID = "LightBugEgg";

	// Token: 0x04000312 RID: 786
	private static float KG_ORE_EATEN_PER_CYCLE = 0.166f;

	// Token: 0x04000313 RID: 787
	private static float CALORIES_PER_KG_OF_ORE = LightBugTuning.STANDARD_CALORIES_PER_CYCLE / LightBugConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000314 RID: 788
	public static int EGG_SORT_ORDER = 100;
}
