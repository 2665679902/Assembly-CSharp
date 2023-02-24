using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class LightBugBlueBabyConfig : IEntityConfig
{
	// Token: 0x06000495 RID: 1173 RVA: 0x00021023 File Offset: 0x0001F223
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0002102A File Offset: 0x0001F22A
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugBlueConfig.CreateLightBug("LightBugBlueBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_BLUE.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_BLUE.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugBlue", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00021068 File Offset: 0x0001F268
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0002106A File Offset: 0x0001F26A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400030E RID: 782
	public const string ID = "LightBugBlueBaby";
}
