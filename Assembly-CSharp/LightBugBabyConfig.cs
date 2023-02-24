using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class LightBugBabyConfig : IEntityConfig
{
	// Token: 0x060004A1 RID: 1185 RVA: 0x00021297 File Offset: 0x0001F497
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000212A0 File Offset: 0x0001F4A0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugConfig.CreateLightBug("LightBugBaby", CREATURES.SPECIES.LIGHTBUG.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBug", null, false, 5f);
		gameObject.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.LightSource, false);
		return gameObject;
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x000212FA File Offset: 0x0001F4FA
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x000212FC File Offset: 0x0001F4FC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000315 RID: 789
	public const string ID = "LightBugBaby";
}
