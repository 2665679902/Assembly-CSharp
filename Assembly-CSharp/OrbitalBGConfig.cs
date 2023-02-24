using System;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class OrbitalBGConfig : IEntityConfig
{
	// Token: 0x06000C58 RID: 3160 RVA: 0x0004646E File Offset: 0x0004466E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00046475 File Offset: 0x00044675
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(OrbitalBGConfig.ID, OrbitalBGConfig.ID, false);
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<OrbitalObject>();
		gameObject.AddOrGet<SaveLoadRoot>();
		return gameObject;
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x0004649C File Offset: 0x0004469C
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0004649E File Offset: 0x0004469E
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400073C RID: 1852
	public static string ID = "OrbitalBG";
}
