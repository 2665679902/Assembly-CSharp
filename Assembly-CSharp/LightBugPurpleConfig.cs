using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class LightBugPurpleConfig : IEntityConfig
{
	// Token: 0x060004CA RID: 1226 RVA: 0x00021A88 File Offset: 0x0001FC88
	public static GameObject CreateLightBug(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseLightBugConfig.BaseLightBug(id, name, desc, anim_file, "LightBugPurpleBaseTrait", LIGHT2D.LIGHTBUG_COLOR_PURPLE, DECOR.BONUS.TIER6, is_baby, "prp_");
		EntityTemplates.ExtendEntityToWildCreature(gameObject, LightBugTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("LightBugPurpleBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, LightBugTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -LightBugTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 25f, name, false, false, true));
		return BaseLightBugConfig.SetupDiet(gameObject, new HashSet<Tag>
		{
			TagManager.Create("FriedMushroom"),
			TagManager.Create("GrilledPrickleFruit"),
			TagManager.Create(SpiceNutConfig.ID),
			TagManager.Create("SpiceBread"),
			SimHashes.Phosphorite.CreateTag()
		}, Tag.Invalid, LightBugPurpleConfig.CALORIES_PER_KG_OF_ORE);
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00021BFB File Offset: 0x0001FDFB
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00021C04 File Offset: 0x0001FE04
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugPurpleConfig.CreateLightBug("LightBugPurple", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.DESC, "lightbug_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, "LightBugPurpleEgg", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.EGG_NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.DESC, "egg_lightbug_kanim", LightBugTuning.EGG_MASS, "LightBugPurpleBaby", 15.000001f, 5f, LightBugTuning.EGG_CHANCES_PURPLE, LightBugPurpleConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		return gameObject;
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00021C81 File Offset: 0x0001FE81
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00021C83 File Offset: 0x0001FE83
	public void OnSpawn(GameObject inst)
	{
		BaseLightBugConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x0400032B RID: 811
	public const string ID = "LightBugPurple";

	// Token: 0x0400032C RID: 812
	public const string BASE_TRAIT_ID = "LightBugPurpleBaseTrait";

	// Token: 0x0400032D RID: 813
	public const string EGG_ID = "LightBugPurpleEgg";

	// Token: 0x0400032E RID: 814
	private static float KG_ORE_EATEN_PER_CYCLE = 1f;

	// Token: 0x0400032F RID: 815
	private static float CALORIES_PER_KG_OF_ORE = LightBugTuning.STANDARD_CALORIES_PER_CYCLE / LightBugPurpleConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000330 RID: 816
	public static int EGG_SORT_ORDER = LightBugConfig.EGG_SORT_ORDER + 2;
}
