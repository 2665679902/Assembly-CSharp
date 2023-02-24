using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000112 RID: 274
[EntityConfigOrder(1)]
public class PacuConfig : IEntityConfig
{
	// Token: 0x06000529 RID: 1321 RVA: 0x00022FCC File Offset: 0x000211CC
	public static GameObject CreatePacu(string id, string name, string desc, string anim_file, bool is_baby)
	{
		return EntityTemplates.ExtendEntityToWildCreature(BasePacuConfig.CreatePrefab(id, "PacuBaseTrait", name, desc, anim_file, is_baby, null, 273.15f, 333.15f), PacuTuning.PEN_SIZE_PER_CREATURE);
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x00022FFE File Offset: 0x000211FE
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x00023008 File Offset: 0x00021208
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(PacuConfig.CreatePacu("Pacu", CREATURES.SPECIES.PACU.NAME, CREATURES.SPECIES.PACU.DESC, "pacu_kanim", false), "PacuEgg", CREATURES.SPECIES.PACU.EGG_NAME, CREATURES.SPECIES.PACU.DESC, "egg_pacu_kanim", PacuTuning.EGG_MASS, "PacuBaby", 15.000001f, 5f, PacuTuning.EGG_CHANCES_BASE, 500, false, true, false, 0.75f, false);
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0002308E File Offset: 0x0002128E
	public void OnPrefabInit(GameObject prefab)
	{
		prefab.AddOrGet<LoopingSounds>();
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00023097 File Offset: 0x00021297
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000375 RID: 885
	public const string ID = "Pacu";

	// Token: 0x04000376 RID: 886
	public const string BASE_TRAIT_ID = "PacuBaseTrait";

	// Token: 0x04000377 RID: 887
	public const string EGG_ID = "PacuEgg";

	// Token: 0x04000378 RID: 888
	public const int EGG_SORT_ORDER = 500;
}
