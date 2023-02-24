using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class LightBugBlackConfig : IEntityConfig
{
	// Token: 0x06000482 RID: 1154 RVA: 0x00020B24 File Offset: 0x0001ED24
	public static GameObject CreateLightBug(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseLightBugConfig.BaseLightBug(id, name, desc, anim_file, "LightBugBlackBaseTrait", Color.black, DECOR.BONUS.TIER7, is_baby, "blk_");
		EntityTemplates.ExtendEntityToWildCreature(gameObject, LightBugTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("LightBugBlackBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, LightBugTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -LightBugTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		gameObject = BaseLightBugConfig.SetupDiet(gameObject, new HashSet<Tag>
		{
			TagManager.Create("Salsa"),
			TagManager.Create("Meat"),
			TagManager.Create("CookedMeat"),
			SimHashes.Katairite.CreateTag(),
			SimHashes.Phosphorus.CreateTag()
		}, Tag.Invalid, LightBugBlackConfig.CALORIES_PER_KG_OF_ORE);
		gameObject.AddOrGetDef<LureableMonitor.Def>().lures = new Tag[] { SimHashes.Phosphorus.CreateTag() };
		return gameObject;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00020CBD File Offset: 0x0001EEBD
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00020CC4 File Offset: 0x0001EEC4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugBlackConfig.CreateLightBug("LightBugBlack", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_BLACK.NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_BLACK.DESC, "lightbug_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, "LightBugBlackEgg", STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_BLACK.EGG_NAME, STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_BLACK.DESC, "egg_lightbug_kanim", LightBugTuning.EGG_MASS, "LightBugBlackBaby", 45f, 15f, LightBugTuning.EGG_CHANCES_BLACK, LightBugBlackConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		return gameObject;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00020D41 File Offset: 0x0001EF41
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00020D43 File Offset: 0x0001EF43
	public void OnSpawn(GameObject inst)
	{
		BaseLightBugConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x04000301 RID: 769
	public const string ID = "LightBugBlack";

	// Token: 0x04000302 RID: 770
	public const string BASE_TRAIT_ID = "LightBugBlackBaseTrait";

	// Token: 0x04000303 RID: 771
	public const string EGG_ID = "LightBugBlackEgg";

	// Token: 0x04000304 RID: 772
	private static float KG_ORE_EATEN_PER_CYCLE = 1f;

	// Token: 0x04000305 RID: 773
	private static float CALORIES_PER_KG_OF_ORE = LightBugTuning.STANDARD_CALORIES_PER_CYCLE / LightBugBlackConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000306 RID: 774
	public static int EGG_SORT_ORDER = LightBugConfig.EGG_SORT_ORDER + 5;
}
