using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000F4 RID: 244
[EntityConfigOrder(1)]
public class HatchVeggieConfig : IEntityConfig
{
	// Token: 0x06000476 RID: 1142 RVA: 0x000208C0 File Offset: 0x0001EAC0
	public static GameObject CreateHatch(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseHatchConfig.BaseHatch(id, name, desc, anim_file, "HatchVeggieBaseTrait", is_baby, "veg_"), HatchTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("HatchVeggieBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, HatchTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -HatchTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseHatchConfig.VeggieDiet(SimHashes.Carbon.CreateTag(), HatchVeggieConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_3, null, 0f);
		list.AddRange(BaseHatchConfig.FoodDiet(SimHashes.Carbon.CreateTag(), HatchVeggieConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_3, null, 0f));
		return BaseHatchConfig.SetupDiet(gameObject, list, HatchVeggieConfig.CALORIES_PER_KG_OF_ORE, HatchVeggieConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00020A11 File Offset: 0x0001EC11
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x00020A18 File Offset: 0x0001EC18
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(HatchVeggieConfig.CreateHatch("HatchVeggie", STRINGS.CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.NAME, STRINGS.CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.DESC, "hatch_kanim", false), "HatchVeggieEgg", STRINGS.CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.EGG_NAME, STRINGS.CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.DESC, "egg_hatch_kanim", HatchTuning.EGG_MASS, "HatchVeggieBaby", 60.000004f, 20f, HatchTuning.EGG_CHANCES_VEGGIE, HatchVeggieConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00020A93 File Offset: 0x0001EC93
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00020A95 File Offset: 0x0001EC95
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002F8 RID: 760
	public const string ID = "HatchVeggie";

	// Token: 0x040002F9 RID: 761
	public const string BASE_TRAIT_ID = "HatchVeggieBaseTrait";

	// Token: 0x040002FA RID: 762
	public const string EGG_ID = "HatchVeggieEgg";

	// Token: 0x040002FB RID: 763
	private const SimHashes EMIT_ELEMENT = SimHashes.Carbon;

	// Token: 0x040002FC RID: 764
	private static float KG_ORE_EATEN_PER_CYCLE = 140f;

	// Token: 0x040002FD RID: 765
	private static float CALORIES_PER_KG_OF_ORE = HatchTuning.STANDARD_CALORIES_PER_CYCLE / HatchVeggieConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040002FE RID: 766
	private static float MIN_POOP_SIZE_IN_KG = 50f;

	// Token: 0x040002FF RID: 767
	public static int EGG_SORT_ORDER = HatchConfig.EGG_SORT_ORDER + 1;
}
