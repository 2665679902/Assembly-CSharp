using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class LightBugCrystalBabyConfig : IEntityConfig
{
	// Token: 0x060004AD RID: 1197 RVA: 0x0002152F File Offset: 0x0001F72F
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00021536 File Offset: 0x0001F736
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugCrystalConfig.CreateLightBug("LightBugCrystalBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugCrystal", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00021574 File Offset: 0x0001F774
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00021576 File Offset: 0x0001F776
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400031C RID: 796
	public const string ID = "LightBugCrystalBaby";
}
