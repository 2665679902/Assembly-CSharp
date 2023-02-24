using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000114 RID: 276
[EntityConfigOrder(1)]
public class PacuTropicalConfig : IEntityConfig
{
	// Token: 0x06000534 RID: 1332 RVA: 0x000230F4 File Offset: 0x000212F4
	public static GameObject CreatePacu(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BasePacuConfig.CreatePrefab(id, "PacuTropicalBaseTrait", name, desc, anim_file, is_baby, "trp_", 303.15f, 353.15f), PacuTuning.PEN_SIZE_PER_CREATURE);
		gameObject.AddOrGet<DecorProvider>().SetValues(PacuTropicalConfig.DECOR);
		return gameObject;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0002313A File Offset: 0x0002133A
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00023144 File Offset: 0x00021344
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(EntityTemplates.ExtendEntityToWildCreature(PacuTropicalConfig.CreatePacu("PacuTropical", STRINGS.CREATURES.SPECIES.PACU.VARIANT_TROPICAL.NAME, STRINGS.CREATURES.SPECIES.PACU.VARIANT_TROPICAL.DESC, "pacu_kanim", false), PacuTuning.PEN_SIZE_PER_CREATURE), "PacuTropicalEgg", STRINGS.CREATURES.SPECIES.PACU.VARIANT_TROPICAL.EGG_NAME, STRINGS.CREATURES.SPECIES.PACU.VARIANT_TROPICAL.DESC, "egg_pacu_kanim", PacuTuning.EGG_MASS, "PacuTropicalBaby", 15.000001f, 5f, PacuTuning.EGG_CHANCES_TROPICAL, 502, false, true, false, 0.75f, false);
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x000231C9 File Offset: 0x000213C9
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x000231CB File Offset: 0x000213CB
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400037A RID: 890
	public const string ID = "PacuTropical";

	// Token: 0x0400037B RID: 891
	public const string BASE_TRAIT_ID = "PacuTropicalBaseTrait";

	// Token: 0x0400037C RID: 892
	public const string EGG_ID = "PacuTropicalEgg";

	// Token: 0x0400037D RID: 893
	public static readonly EffectorValues DECOR = TUNING.BUILDINGS.DECOR.BONUS.TIER4;

	// Token: 0x0400037E RID: 894
	public const int EGG_SORT_ORDER = 502;
}
