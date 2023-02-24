﻿using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class LightBugCrystalConfig : IEntityConfig
{
	// Token: 0x060004A6 RID: 1190 RVA: 0x00021308 File Offset: 0x0001F508
	public static GameObject CreateLightBug(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseLightBugConfig.BaseLightBug(id, name, desc, anim_file, "LightBugCrystalBaseTrait", LIGHT2D.LIGHTBUG_COLOR_CRYSTAL, DECOR.BONUS.TIER8, is_baby, "cry_");
		EntityTemplates.ExtendEntityToWildCreature(gameObject, LightBugTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("LightBugCrystalBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, LightBugTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -LightBugTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		gameObject = BaseLightBugConfig.SetupDiet(gameObject, new HashSet<Tag>
		{
			TagManager.Create("CookedMeat"),
			SimHashes.Diamond.CreateTag()
		}, Tag.Invalid, LightBugCrystalConfig.CALORIES_PER_KG_OF_ORE);
		gameObject.AddOrGetDef<LureableMonitor.Def>().lures = new Tag[] { SimHashes.Diamond.CreateTag() };
		return gameObject;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0002146E File Offset: 0x0001F66E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00021478 File Offset: 0x0001F678
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugCrystalConfig.CreateLightBug("LightBugCrystal", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.DESC, "lightbug_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, "LightBugCrystalEgg", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.EGG_NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.DESC, "egg_lightbug_kanim", LightBugTuning.EGG_MASS, "LightBugCrystalBaby", 45f, 15f, LightBugTuning.EGG_CHANCES_CRYSTAL, LightBugCrystalConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		return gameObject;
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x000214F5 File Offset: 0x0001F6F5
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x000214F7 File Offset: 0x0001F6F7
	public void OnSpawn(GameObject inst)
	{
		BaseLightBugConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x04000316 RID: 790
	public const string ID = "LightBugCrystal";

	// Token: 0x04000317 RID: 791
	public const string BASE_TRAIT_ID = "LightBugCrystalBaseTrait";

	// Token: 0x04000318 RID: 792
	public const string EGG_ID = "LightBugCrystalEgg";

	// Token: 0x04000319 RID: 793
	private static float KG_ORE_EATEN_PER_CYCLE = 1f;

	// Token: 0x0400031A RID: 794
	private static float CALORIES_PER_KG_OF_ORE = LightBugTuning.STANDARD_CALORIES_PER_CYCLE / LightBugCrystalConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x0400031B RID: 795
	public static int EGG_SORT_ORDER = LightBugConfig.EGG_SORT_ORDER + 7;
}
