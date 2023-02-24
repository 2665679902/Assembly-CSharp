using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class OilFloaterDecorConfig : IEntityConfig
{
	// Token: 0x06000503 RID: 1283 RVA: 0x00022898 File Offset: 0x00020A98
	public static GameObject CreateOilFloater(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseOilFloaterConfig.BaseOilFloater(id, name, desc, anim_file, "OilfloaterDecorBaseTrait", 283.15f, 343.15f, is_baby, "oxy_");
		gameObject.AddOrGet<DecorProvider>().SetValues(DECOR.BONUS.TIER6);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, OilFloaterTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("OilfloaterDecorBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, OilFloaterTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 150f, name, false, false, true));
		return BaseOilFloaterConfig.SetupDiet(gameObject, SimHashes.Oxygen.CreateTag(), Tag.Invalid, OilFloaterDecorConfig.CALORIES_PER_KG_OF_ORE, 0f, null, 0f, 0f);
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x000229D9 File Offset: 0x00020BD9
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x000229E0 File Offset: 0x00020BE0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterDecorConfig.CreateOilFloater("OilfloaterDecor", STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.NAME, STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.DESC, "oilfloater_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, "OilfloaterDecorEgg", STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.EGG_NAME, STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.DESC, "egg_oilfloater_kanim", OilFloaterTuning.EGG_MASS, "OilfloaterDecorBaby", 90f, 30f, OilFloaterTuning.EGG_CHANCES_DECOR, OilFloaterDecorConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		return gameObject;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x00022A5D File Offset: 0x00020C5D
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00022A5F File Offset: 0x00020C5F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400035A RID: 858
	public const string ID = "OilfloaterDecor";

	// Token: 0x0400035B RID: 859
	public const string BASE_TRAIT_ID = "OilfloaterDecorBaseTrait";

	// Token: 0x0400035C RID: 860
	public const string EGG_ID = "OilfloaterDecorEgg";

	// Token: 0x0400035D RID: 861
	public const SimHashes CONSUME_ELEMENT = SimHashes.Oxygen;

	// Token: 0x0400035E RID: 862
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x0400035F RID: 863
	private static float CALORIES_PER_KG_OF_ORE = OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / OilFloaterDecorConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000360 RID: 864
	public static int EGG_SORT_ORDER = OilFloaterConfig.EGG_SORT_ORDER + 2;
}
