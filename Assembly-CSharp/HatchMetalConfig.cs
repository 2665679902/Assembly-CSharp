using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000F2 RID: 242
[EntityConfigOrder(1)]
public class HatchMetalConfig : IEntityConfig
{
	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000469 RID: 1129 RVA: 0x00020600 File Offset: 0x0001E800
	public static HashSet<Tag> METAL_ORE_TAGS
	{
		get
		{
			HashSet<Tag> hashSet = new HashSet<Tag>
			{
				SimHashes.Cuprite.CreateTag(),
				SimHashes.GoldAmalgam.CreateTag(),
				SimHashes.IronOre.CreateTag(),
				SimHashes.Wolframite.CreateTag(),
				SimHashes.AluminumOre.CreateTag()
			};
			if (DlcManager.IsExpansion1Active())
			{
				hashSet.Add(SimHashes.Cobaltite.CreateTag());
			}
			return hashSet;
		}
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00020684 File Offset: 0x0001E884
	public static GameObject CreateHatch(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseHatchConfig.BaseHatch(id, name, desc, anim_file, "HatchMetalBaseTrait", is_baby, "mtl_"), HatchTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("HatchMetalBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, HatchTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -HatchTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 400f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseHatchConfig.MetalDiet(GameTags.Metal, HatchMetalConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_1, null, 0f);
		return BaseHatchConfig.SetupDiet(gameObject, list, HatchMetalConfig.CALORIES_PER_KG_OF_ORE, HatchMetalConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x000207AB File Offset: 0x0001E9AB
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x000207B4 File Offset: 0x0001E9B4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(HatchMetalConfig.CreateHatch("HatchMetal", STRINGS.CREATURES.SPECIES.HATCH.VARIANT_METAL.NAME, STRINGS.CREATURES.SPECIES.HATCH.VARIANT_METAL.DESC, "hatch_kanim", false), "HatchMetalEgg", STRINGS.CREATURES.SPECIES.HATCH.VARIANT_METAL.EGG_NAME, STRINGS.CREATURES.SPECIES.HATCH.VARIANT_METAL.DESC, "egg_hatch_kanim", HatchTuning.EGG_MASS, "HatchMetalBaby", 60.000004f, 20f, HatchTuning.EGG_CHANCES_METAL, HatchMetalConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0002082F File Offset: 0x0001EA2F
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00020831 File Offset: 0x0001EA31
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002F0 RID: 752
	public const string ID = "HatchMetal";

	// Token: 0x040002F1 RID: 753
	public const string BASE_TRAIT_ID = "HatchMetalBaseTrait";

	// Token: 0x040002F2 RID: 754
	public const string EGG_ID = "HatchMetalEgg";

	// Token: 0x040002F3 RID: 755
	private static float KG_ORE_EATEN_PER_CYCLE = 100f;

	// Token: 0x040002F4 RID: 756
	private static float CALORIES_PER_KG_OF_ORE = HatchTuning.STANDARD_CALORIES_PER_CYCLE / HatchMetalConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040002F5 RID: 757
	private static float MIN_POOP_SIZE_IN_KG = 10f;

	// Token: 0x040002F6 RID: 758
	public static int EGG_SORT_ORDER = HatchConfig.EGG_SORT_ORDER + 3;
}
