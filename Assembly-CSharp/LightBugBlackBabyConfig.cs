using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class LightBugBlackBabyConfig : IEntityConfig
{
	// Token: 0x06000489 RID: 1161 RVA: 0x00020D7B File Offset: 0x0001EF7B
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00020D82 File Offset: 0x0001EF82
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugBlackConfig.CreateLightBug("LightBugBlackBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_BLACK.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_BLACK.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugBlack", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00020DC0 File Offset: 0x0001EFC0
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00020DC2 File Offset: 0x0001EFC2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000307 RID: 775
	public const string ID = "LightBugBlackBaby";
}
