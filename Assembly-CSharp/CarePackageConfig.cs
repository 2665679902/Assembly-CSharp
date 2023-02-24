using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000248 RID: 584
public class CarePackageConfig : IEntityConfig
{
	// Token: 0x06000B88 RID: 2952 RVA: 0x0004194C File Offset: 0x0003FB4C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00041954 File Offset: 0x0003FB54
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateLooseEntity(CarePackageConfig.ID, ITEMS.CARGO_CAPSULE.NAME, ITEMS.CARGO_CAPSULE.DESC, 1f, true, Assets.GetAnim("portal_carepackage_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f, false, 0, SimHashes.Creature, null);
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x000419AE File Offset: 0x0003FBAE
	public void OnPrefabInit(GameObject go)
	{
		go.AddOrGet<CarePackage>();
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x000419B7 File Offset: 0x0003FBB7
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006DB RID: 1755
	public static readonly string ID = "CarePackage";
}
