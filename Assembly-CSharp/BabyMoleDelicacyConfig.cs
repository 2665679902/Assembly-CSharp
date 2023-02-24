using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class BabyMoleDelicacyConfig : IEntityConfig
{
	// Token: 0x060004EB RID: 1259 RVA: 0x000223D6 File Offset: 0x000205D6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x000223DD File Offset: 0x000205DD
	public GameObject CreatePrefab()
	{
		GameObject gameObject = MoleDelicacyConfig.CreateMole("MoleDelicacyBaby", CREATURES.SPECIES.MOLE.VARIANT_DELICACY.BABY.NAME, CREATURES.SPECIES.MOLE.VARIANT_DELICACY.BABY.DESC, "baby_driller_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "MoleDelicacy", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0002241B File Offset: 0x0002061B
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0002241D File Offset: 0x0002061D
	public void OnSpawn(GameObject inst)
	{
		MoleConfig.SetSpawnNavType(inst);
	}

	// Token: 0x04000346 RID: 838
	public const string ID = "MoleDelicacyBaby";
}
