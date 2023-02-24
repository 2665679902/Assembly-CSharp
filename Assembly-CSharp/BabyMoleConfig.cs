using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000105 RID: 261
public class BabyMoleConfig : IEntityConfig
{
	// Token: 0x060004DE RID: 1246 RVA: 0x00021FFB File Offset: 0x000201FB
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00022002 File Offset: 0x00020202
	public GameObject CreatePrefab()
	{
		GameObject gameObject = MoleConfig.CreateMole("MoleBaby", CREATURES.SPECIES.MOLE.BABY.NAME, CREATURES.SPECIES.MOLE.BABY.DESC, "baby_driller_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Mole", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00022040 File Offset: 0x00020240
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00022042 File Offset: 0x00020242
	public void OnSpawn(GameObject inst)
	{
		MoleConfig.SetSpawnNavType(inst);
	}

	// Token: 0x04000338 RID: 824
	public const string ID = "MoleBaby";
}
