using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class LightBugOrangeBabyConfig : IEntityConfig
{
	// Token: 0x060004B9 RID: 1209 RVA: 0x000217A3 File Offset: 0x0001F9A3
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x000217AA File Offset: 0x0001F9AA
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugOrangeConfig.CreateLightBug("LightBugOrangeBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_ORANGE.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_ORANGE.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugOrange", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x000217E8 File Offset: 0x0001F9E8
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x000217EA File Offset: 0x0001F9EA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000323 RID: 803
	public const string ID = "LightBugOrangeBaby";
}
