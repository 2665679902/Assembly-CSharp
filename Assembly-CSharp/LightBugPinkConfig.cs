using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class LightBugPinkConfig : IEntityConfig
{
	// Token: 0x060004BE RID: 1214 RVA: 0x000217F4 File Offset: 0x0001F9F4
	public static GameObject CreateLightBug(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseLightBugConfig.BaseLightBug(id, name, desc, anim_file, "LightBugPinkBaseTrait", LIGHT2D.LIGHTBUG_COLOR_PINK, DECOR.BONUS.TIER6, is_baby, "pnk_");
		EntityTemplates.ExtendEntityToWildCreature(gameObject, LightBugTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("LightBugPinkBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, LightBugTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -LightBugTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 25f, name, false, false, true));
		return BaseLightBugConfig.SetupDiet(gameObject, new HashSet<Tag>
		{
			TagManager.Create("FriedMushroom"),
			TagManager.Create("SpiceBread"),
			TagManager.Create(PrickleFruitConfig.ID),
			TagManager.Create("GrilledPrickleFruit"),
			TagManager.Create("Salsa"),
			SimHashes.Phosphorite.CreateTag()
		}, Tag.Invalid, LightBugPinkConfig.CALORIES_PER_KG_OF_ORE);
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00021978 File Offset: 0x0001FB78
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00021980 File Offset: 0x0001FB80
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugPinkConfig.CreateLightBug("LightBugPink", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PINK.NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PINK.DESC, "lightbug_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, "LightBugPinkEgg", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PINK.EGG_NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PINK.DESC, "egg_lightbug_kanim", LightBugTuning.EGG_MASS, "LightBugPinkBaby", 15.000001f, 5f, LightBugTuning.EGG_CHANCES_PINK, LightBugPinkConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		return gameObject;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x000219FD File Offset: 0x0001FBFD
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x000219FF File Offset: 0x0001FBFF
	public void OnSpawn(GameObject inst)
	{
		BaseLightBugConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x04000324 RID: 804
	public const string ID = "LightBugPink";

	// Token: 0x04000325 RID: 805
	public const string BASE_TRAIT_ID = "LightBugPinkBaseTrait";

	// Token: 0x04000326 RID: 806
	public const string EGG_ID = "LightBugPinkEgg";

	// Token: 0x04000327 RID: 807
	private static float KG_ORE_EATEN_PER_CYCLE = 1f;

	// Token: 0x04000328 RID: 808
	private static float CALORIES_PER_KG_OF_ORE = LightBugTuning.STANDARD_CALORIES_PER_CYCLE / LightBugPinkConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000329 RID: 809
	public static int EGG_SORT_ORDER = LightBugConfig.EGG_SORT_ORDER + 3;
}
