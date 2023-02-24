using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class LightBugPinkBabyConfig : IEntityConfig
{
	// Token: 0x060004C5 RID: 1221 RVA: 0x00021A37 File Offset: 0x0001FC37
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x00021A3E File Offset: 0x0001FC3E
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugPinkConfig.CreateLightBug("LightBugPinkBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_PINK.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_PINK.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugPink", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00021A7C File Offset: 0x0001FC7C
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00021A7E File Offset: 0x0001FC7E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400032A RID: 810
	public const string ID = "LightBugPinkBaby";
}
