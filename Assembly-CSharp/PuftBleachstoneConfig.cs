using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class PuftBleachstoneConfig : IEntityConfig
{
	// Token: 0x0600054C RID: 1356 RVA: 0x0002357C File Offset: 0x0002177C
	public static GameObject CreatePuftBleachstone(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BasePuftConfig.BasePuft(id, name, desc, "PuftBleachstoneBaseTrait", anim_file, is_baby, "anti_", 258.15f, 308.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, PuftTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("PuftBleachstoneBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PuftTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PuftTuning.STANDARD_CALORIES_PER_CYCLE / 600f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		gameObject = BasePuftConfig.SetupDiet(gameObject, SimHashes.ChlorineGas.CreateTag(), SimHashes.BleachStone.CreateTag(), PuftBleachstoneConfig.CALORIES_PER_KG_OF_ORE, TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_2, null, 0f, PuftBleachstoneConfig.MIN_POOP_SIZE_IN_KG);
		gameObject.AddOrGetDef<LureableMonitor.Def>().lures = new Tag[] { SimHashes.BleachStone.CreateTag() };
		return gameObject;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x000236D8 File Offset: 0x000218D8
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x000236E0 File Offset: 0x000218E0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(PuftBleachstoneConfig.CreatePuftBleachstone("PuftBleachstone", STRINGS.CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.NAME, STRINGS.CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.DESC, "puft_kanim", false), "PuftBleachstoneEgg", STRINGS.CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.EGG_NAME, STRINGS.CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.DESC, "egg_puft_kanim", PuftTuning.EGG_MASS, "PuftBleachstoneBaby", 45f, 15f, PuftTuning.EGG_CHANCES_BLEACHSTONE, PuftBleachstoneConfig.EGG_SORT_ORDER, true, false, true, 1f, false);
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0002375B File Offset: 0x0002195B
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0002375D File Offset: 0x0002195D
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x0400038C RID: 908
	public const string ID = "PuftBleachstone";

	// Token: 0x0400038D RID: 909
	public const string BASE_TRAIT_ID = "PuftBleachstoneBaseTrait";

	// Token: 0x0400038E RID: 910
	public const string EGG_ID = "PuftBleachstoneEgg";

	// Token: 0x0400038F RID: 911
	public const SimHashes CONSUME_ELEMENT = SimHashes.ChlorineGas;

	// Token: 0x04000390 RID: 912
	public const SimHashes EMIT_ELEMENT = SimHashes.BleachStone;

	// Token: 0x04000391 RID: 913
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x04000392 RID: 914
	private static float CALORIES_PER_KG_OF_ORE = PuftTuning.STANDARD_CALORIES_PER_CYCLE / PuftBleachstoneConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000393 RID: 915
	private static float MIN_POOP_SIZE_IN_KG = 15f;

	// Token: 0x04000394 RID: 916
	public static int EGG_SORT_ORDER = PuftConfig.EGG_SORT_ORDER + 3;
}
