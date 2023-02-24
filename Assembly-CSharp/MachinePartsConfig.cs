using System;
using STRINGS;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class MachinePartsConfig : IEntityConfig
{
	// Token: 0x06000916 RID: 2326 RVA: 0x000353E9 File Offset: 0x000335E9
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x000353F0 File Offset: 0x000335F0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateLooseEntity("MachineParts", ITEMS.INDUSTRIAL_PRODUCTS.MACHINE_PARTS.NAME, ITEMS.INDUSTRIAL_PRODUCTS.MACHINE_PARTS.DESC, 5f, true, Assets.GetAnim("buildingrelocate_kanim"), "idle", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, true, 0, SimHashes.Creature, null);
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x0003544A File Offset: 0x0003364A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0003544C File Offset: 0x0003364C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005BA RID: 1466
	public const string ID = "MachineParts";

	// Token: 0x040005BB RID: 1467
	public static readonly Tag TAG = TagManager.Create("MachineParts");

	// Token: 0x040005BC RID: 1468
	public const float MASS = 5f;
}
