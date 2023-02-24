using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class LightBugOrangeConfig : IEntityConfig
{
	// Token: 0x060004B2 RID: 1202 RVA: 0x00021580 File Offset: 0x0001F780
	public static GameObject CreateLightBug(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseLightBugConfig.BaseLightBug(id, name, desc, anim_file, "LightBugOrangeBaseTrait", LIGHT2D.LIGHTBUG_COLOR_ORANGE, DECOR.BONUS.TIER6, is_baby, "org_");
		EntityTemplates.ExtendEntityToWildCreature(gameObject, LightBugTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("LightBugOrangeBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, LightBugTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -LightBugTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 25f, name, false, false, true));
		return BaseLightBugConfig.SetupDiet(gameObject, new HashSet<Tag>
		{
			TagManager.Create(MushroomConfig.ID),
			TagManager.Create("FriedMushroom"),
			TagManager.Create("GrilledPrickleFruit"),
			SimHashes.Phosphorite.CreateTag()
		}, Tag.Invalid, LightBugOrangeConfig.CALORIES_PER_KG_OF_ORE);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x000216E2 File Offset: 0x0001F8E2
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x000216EC File Offset: 0x0001F8EC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugOrangeConfig.CreateLightBug("LightBugOrange", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_ORANGE.NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_ORANGE.DESC, "lightbug_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, "LightBugOrangeEgg", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_ORANGE.EGG_NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_ORANGE.DESC, "egg_lightbug_kanim", LightBugTuning.EGG_MASS, "LightBugOrangeBaby", 15.000001f, 5f, LightBugTuning.EGG_CHANCES_ORANGE, LightBugOrangeConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		return gameObject;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00021769 File Offset: 0x0001F969
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0002176B File Offset: 0x0001F96B
	public void OnSpawn(GameObject inst)
	{
		BaseLightBugConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x0400031D RID: 797
	public const string ID = "LightBugOrange";

	// Token: 0x0400031E RID: 798
	public const string BASE_TRAIT_ID = "LightBugOrangeBaseTrait";

	// Token: 0x0400031F RID: 799
	public const string EGG_ID = "LightBugOrangeEgg";

	// Token: 0x04000320 RID: 800
	private static float KG_ORE_EATEN_PER_CYCLE = 0.25f;

	// Token: 0x04000321 RID: 801
	private static float CALORIES_PER_KG_OF_ORE = LightBugTuning.STANDARD_CALORIES_PER_CYCLE / LightBugOrangeConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000322 RID: 802
	public static int EGG_SORT_ORDER = LightBugConfig.EGG_SORT_ORDER + 1;
}
