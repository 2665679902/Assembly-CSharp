using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class OilFloaterHighTempConfig : IEntityConfig
{
	// Token: 0x0600050F RID: 1295 RVA: 0x00022AE4 File Offset: 0x00020CE4
	public static GameObject CreateOilFloater(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseOilFloaterConfig.BaseOilFloater(id, name, desc, anim_file, "OilfloaterHighTempBaseTrait", 363.15f, 523.15f, is_baby, "hot_");
		EntityTemplates.ExtendEntityToWildCreature(gameObject, OilFloaterTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("OilfloaterHighTempBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, OilFloaterTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		return BaseOilFloaterConfig.SetupDiet(gameObject, SimHashes.CarbonDioxide.CreateTag(), SimHashes.Petroleum.CreateTag(), OilFloaterHighTempConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f, OilFloaterHighTempConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00022C1A File Offset: 0x00020E1A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x00022C24 File Offset: 0x00020E24
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterHighTempConfig.CreateOilFloater("OilfloaterHighTemp", STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_HIGHTEMP.NAME, STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_HIGHTEMP.DESC, "oilfloater_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, "OilfloaterHighTempEgg", STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_HIGHTEMP.EGG_NAME, STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_HIGHTEMP.DESC, "egg_oilfloater_kanim", OilFloaterTuning.EGG_MASS, "OilfloaterHighTempBaby", 60.000004f, 20f, OilFloaterTuning.EGG_CHANCES_HIGHTEMP, OilFloaterHighTempConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
		return gameObject;
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00022CA1 File Offset: 0x00020EA1
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00022CA3 File Offset: 0x00020EA3
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000362 RID: 866
	public const string ID = "OilfloaterHighTemp";

	// Token: 0x04000363 RID: 867
	public const string BASE_TRAIT_ID = "OilfloaterHighTempBaseTrait";

	// Token: 0x04000364 RID: 868
	public const string EGG_ID = "OilfloaterHighTempEgg";

	// Token: 0x04000365 RID: 869
	public const SimHashes CONSUME_ELEMENT = SimHashes.CarbonDioxide;

	// Token: 0x04000366 RID: 870
	public const SimHashes EMIT_ELEMENT = SimHashes.Petroleum;

	// Token: 0x04000367 RID: 871
	private static float KG_ORE_EATEN_PER_CYCLE = 20f;

	// Token: 0x04000368 RID: 872
	private static float CALORIES_PER_KG_OF_ORE = OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / OilFloaterHighTempConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000369 RID: 873
	private static float MIN_POOP_SIZE_IN_KG = 0.5f;

	// Token: 0x0400036A RID: 874
	public static int EGG_SORT_ORDER = OilFloaterConfig.EGG_SORT_ORDER + 1;
}
