using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class PuftOxyliteConfig : IEntityConfig
{
	// Token: 0x06000564 RID: 1380 RVA: 0x00023A6C File Offset: 0x00021C6C
	public static GameObject CreatePuftOxylite(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BasePuftConfig.BasePuft(id, name, desc, "PuftOxyliteBaseTrait", anim_file, is_baby, "com_", 303.15f, 338.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, PuftTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("PuftOxyliteBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PuftTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PuftTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		gameObject = BasePuftConfig.SetupDiet(gameObject, SimHashes.Oxygen.CreateTag(), SimHashes.OxyRock.CreateTag(), PuftOxyliteConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_2, null, 0f, PuftOxyliteConfig.MIN_POOP_SIZE_IN_KG);
		gameObject.AddOrGetDef<LureableMonitor.Def>().lures = new Tag[] { SimHashes.OxyRock.CreateTag() };
		return gameObject;
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00023BC8 File Offset: 0x00021DC8
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00023BD0 File Offset: 0x00021DD0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(PuftOxyliteConfig.CreatePuftOxylite("PuftOxylite", STRINGS.CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.NAME, STRINGS.CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.DESC, "puft_kanim", false), "PuftOxyliteEgg", STRINGS.CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.EGG_NAME, STRINGS.CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.DESC, "egg_puft_kanim", PuftTuning.EGG_MASS, "PuftOxyliteBaby", 45f, 15f, PuftTuning.EGG_CHANCES_OXYLITE, PuftOxyliteConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00023C4B File Offset: 0x00021E4B
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00023C4D File Offset: 0x00021E4D
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x040003A2 RID: 930
	public const string ID = "PuftOxylite";

	// Token: 0x040003A3 RID: 931
	public const string BASE_TRAIT_ID = "PuftOxyliteBaseTrait";

	// Token: 0x040003A4 RID: 932
	public const string EGG_ID = "PuftOxyliteEgg";

	// Token: 0x040003A5 RID: 933
	public const SimHashes CONSUME_ELEMENT = SimHashes.Oxygen;

	// Token: 0x040003A6 RID: 934
	public const SimHashes EMIT_ELEMENT = SimHashes.OxyRock;

	// Token: 0x040003A7 RID: 935
	private static float KG_ORE_EATEN_PER_CYCLE = 50f;

	// Token: 0x040003A8 RID: 936
	private static float CALORIES_PER_KG_OF_ORE = PuftTuning.STANDARD_CALORIES_PER_CYCLE / PuftOxyliteConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040003A9 RID: 937
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x040003AA RID: 938
	public static int EGG_SORT_ORDER = PuftConfig.EGG_SORT_ORDER + 2;
}
