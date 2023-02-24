using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000116 RID: 278
public class PuftAlphaConfig : IEntityConfig
{
	// Token: 0x06000540 RID: 1344 RVA: 0x00023234 File Offset: 0x00021434
	public static GameObject CreatePuftAlpha(string id, string name, string desc, string anim_file, bool is_baby)
	{
		string text = "alp_";
		GameObject gameObject = BasePuftConfig.BasePuft(id, name, desc, "PuftAlphaBaseTrait", anim_file, is_baby, text, 258.15f, 338.15f);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, PuftTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("PuftAlphaBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PuftTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PuftTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		gameObject = BasePuftConfig.SetupDiet(gameObject, new List<Diet.Info>
		{
			new Diet.Info(new HashSet<Tag>(new Tag[] { SimHashes.ContaminatedOxygen.CreateTag() }), SimHashes.SlimeMold.CreateTag(), PuftAlphaConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_2, "SlimeLung", 1000f, false, false),
			new Diet.Info(new HashSet<Tag>(new Tag[] { SimHashes.ChlorineGas.CreateTag() }), SimHashes.BleachStone.CreateTag(), PuftAlphaConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_2, "SlimeLung", 1000f, false, false),
			new Diet.Info(new HashSet<Tag>(new Tag[] { SimHashes.Oxygen.CreateTag() }), SimHashes.OxyRock.CreateTag(), PuftAlphaConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_2, "SlimeLung", 1000f, false, false)
		}.ToArray(), PuftAlphaConfig.CALORIES_PER_KG_OF_ORE, PuftAlphaConfig.MIN_POOP_SIZE_IN_KG);
		gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = "SlimeLung";
		return gameObject;
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00023442 File Offset: 0x00021642
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0002344C File Offset: 0x0002164C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(PuftAlphaConfig.CreatePuftAlpha("PuftAlpha", STRINGS.CREATURES.SPECIES.PUFT.VARIANT_ALPHA.NAME, STRINGS.CREATURES.SPECIES.PUFT.VARIANT_ALPHA.DESC, "puft_kanim", false), "PuftAlphaEgg", STRINGS.CREATURES.SPECIES.PUFT.VARIANT_ALPHA.EGG_NAME, STRINGS.CREATURES.SPECIES.PUFT.VARIANT_ALPHA.DESC, "egg_puft_kanim", PuftTuning.EGG_MASS, "PuftAlphaBaby", 45f, 15f, PuftTuning.EGG_CHANCES_ALPHA, PuftAlphaConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x000234C7 File Offset: 0x000216C7
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<KBatchedAnimController>().animScale *= 1.1f;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x000234E0 File Offset: 0x000216E0
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x04000380 RID: 896
	public const string ID = "PuftAlpha";

	// Token: 0x04000381 RID: 897
	public const string BASE_TRAIT_ID = "PuftAlphaBaseTrait";

	// Token: 0x04000382 RID: 898
	public const string EGG_ID = "PuftAlphaEgg";

	// Token: 0x04000383 RID: 899
	public const SimHashes CONSUME_ELEMENT = SimHashes.ContaminatedOxygen;

	// Token: 0x04000384 RID: 900
	public const SimHashes EMIT_ELEMENT = SimHashes.SlimeMold;

	// Token: 0x04000385 RID: 901
	public const string EMIT_DISEASE = "SlimeLung";

	// Token: 0x04000386 RID: 902
	public const float EMIT_DISEASE_PER_KG = 1000f;

	// Token: 0x04000387 RID: 903
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x04000388 RID: 904
	private static float CALORIES_PER_KG_OF_ORE = PuftTuning.STANDARD_CALORIES_PER_CYCLE / PuftAlphaConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000389 RID: 905
	private static float MIN_POOP_SIZE_IN_KG = 5f;

	// Token: 0x0400038A RID: 906
	public static int EGG_SORT_ORDER = PuftConfig.EGG_SORT_ORDER + 1;
}
