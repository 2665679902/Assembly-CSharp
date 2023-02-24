using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class LightBugPurpleBabyConfig : IEntityConfig
{
	// Token: 0x060004D1 RID: 1233 RVA: 0x00021CBB File Offset: 0x0001FEBB
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00021CC2 File Offset: 0x0001FEC2
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugPurpleConfig.CreateLightBug("LightBugPurpleBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugPurple", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00021D00 File Offset: 0x0001FF00
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00021D02 File Offset: 0x0001FF02
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000331 RID: 817
	public const string ID = "LightBugPurpleBaby";
}
