using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class PuftConfig : IEntityConfig
{
	// Token: 0x06000558 RID: 1368 RVA: 0x000237F8 File Offset: 0x000219F8
	public static GameObject CreatePuft(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BasePuftConfig.BasePuft(id, name, STRINGS.CREATURES.SPECIES.PUFT.DESC, "PuftBaseTrait", anim_file, is_baby, null, 288.15f, 328.15f);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, PuftTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("PuftBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PuftTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PuftTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		GameObject gameObject2 = BasePuftConfig.SetupDiet(gameObject, SimHashes.ContaminatedOxygen.CreateTag(), SimHashes.SlimeMold.CreateTag(), PuftConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_2, "SlimeLung", 1000f, PuftConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = "SlimeLung";
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x00023952 File Offset: 0x00021B52
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0002395C File Offset: 0x00021B5C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(PuftConfig.CreatePuft("Puft", STRINGS.CREATURES.SPECIES.PUFT.NAME, STRINGS.CREATURES.SPECIES.PUFT.DESC, "puft_kanim", false), "PuftEgg", STRINGS.CREATURES.SPECIES.PUFT.EGG_NAME, STRINGS.CREATURES.SPECIES.PUFT.DESC, "egg_puft_kanim", PuftTuning.EGG_MASS, "PuftBaby", 45f, 15f, PuftTuning.EGG_CHANCES_BASE, PuftConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x000239D7 File Offset: 0x00021BD7
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x000239D9 File Offset: 0x00021BD9
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x04000396 RID: 918
	public const string ID = "Puft";

	// Token: 0x04000397 RID: 919
	public const string BASE_TRAIT_ID = "PuftBaseTrait";

	// Token: 0x04000398 RID: 920
	public const string EGG_ID = "PuftEgg";

	// Token: 0x04000399 RID: 921
	public const SimHashes CONSUME_ELEMENT = SimHashes.ContaminatedOxygen;

	// Token: 0x0400039A RID: 922
	public const SimHashes EMIT_ELEMENT = SimHashes.SlimeMold;

	// Token: 0x0400039B RID: 923
	public const string EMIT_DISEASE = "SlimeLung";

	// Token: 0x0400039C RID: 924
	public const float EMIT_DISEASE_PER_KG = 1000f;

	// Token: 0x0400039D RID: 925
	private static float KG_ORE_EATEN_PER_CYCLE = 50f;

	// Token: 0x0400039E RID: 926
	private static float CALORIES_PER_KG_OF_ORE = PuftTuning.STANDARD_CALORIES_PER_CYCLE / PuftConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x0400039F RID: 927
	private static float MIN_POOP_SIZE_IN_KG = 15f;

	// Token: 0x040003A0 RID: 928
	public static int EGG_SORT_ORDER = 300;
}
