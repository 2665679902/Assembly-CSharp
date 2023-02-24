using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class OilFloaterConfig : IEntityConfig
{
	// Token: 0x060004F7 RID: 1271 RVA: 0x00022648 File Offset: 0x00020848
	public static GameObject CreateOilFloater(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseOilFloaterConfig.BaseOilFloater(id, name, desc, anim_file, "OilfloaterBaseTrait", 323.15f, 413.15f, is_baby, null);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, OilFloaterTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("OilfloaterBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, OilFloaterTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		GameObject gameObject2 = BaseOilFloaterConfig.SetupDiet(gameObject, SimHashes.CarbonDioxide.CreateTag(), SimHashes.CrudeOil.CreateTag(), OilFloaterConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f, OilFloaterConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00022785 File Offset: 0x00020985
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0002278C File Offset: 0x0002098C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterConfig.CreateOilFloater("Oilfloater", STRINGS.CREATURES.SPECIES.OILFLOATER.NAME, STRINGS.CREATURES.SPECIES.OILFLOATER.DESC, "oilfloater_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, "OilfloaterEgg", STRINGS.CREATURES.SPECIES.OILFLOATER.EGG_NAME, STRINGS.CREATURES.SPECIES.OILFLOATER.DESC, "egg_oilfloater_kanim", OilFloaterTuning.EGG_MASS, "OilfloaterBaby", 60.000004f, 20f, OilFloaterTuning.EGG_CHANCES_BASE, OilFloaterConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		return gameObject;
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x00022809 File Offset: 0x00020A09
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060004FB RID: 1275 RVA: 0x0002280B File Offset: 0x00020A0B
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000350 RID: 848
	public const string ID = "Oilfloater";

	// Token: 0x04000351 RID: 849
	public const string BASE_TRAIT_ID = "OilfloaterBaseTrait";

	// Token: 0x04000352 RID: 850
	public const string EGG_ID = "OilfloaterEgg";

	// Token: 0x04000353 RID: 851
	public const SimHashes CONSUME_ELEMENT = SimHashes.CarbonDioxide;

	// Token: 0x04000354 RID: 852
	public const SimHashes EMIT_ELEMENT = SimHashes.CrudeOil;

	// Token: 0x04000355 RID: 853
	private static float KG_ORE_EATEN_PER_CYCLE = 20f;

	// Token: 0x04000356 RID: 854
	private static float CALORIES_PER_KG_OF_ORE = OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / OilFloaterConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000357 RID: 855
	private static float MIN_POOP_SIZE_IN_KG = 0.5f;

	// Token: 0x04000358 RID: 856
	public static int EGG_SORT_ORDER = 400;
}
