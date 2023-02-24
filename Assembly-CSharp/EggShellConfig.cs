using System;
using STRINGS;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class EggShellConfig : IEntityConfig
{
	// Token: 0x0600090A RID: 2314 RVA: 0x000352B1 File Offset: 0x000334B1
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x000352B8 File Offset: 0x000334B8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("EggShell", ITEMS.INDUSTRIAL_PRODUCTS.EGG_SHELL.NAME, ITEMS.INDUSTRIAL_PRODUCTS.EGG_SHELL.DESC, 1f, false, Assets.GetAnim("eggshells_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, null);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.Organics, false);
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<SimpleMassStatusItem>();
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		return gameObject;
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00035338 File Offset: 0x00033538
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0003533A File Offset: 0x0003353A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005B4 RID: 1460
	public const string ID = "EggShell";

	// Token: 0x040005B5 RID: 1461
	public static readonly Tag TAG = TagManager.Create("EggShell");

	// Token: 0x040005B6 RID: 1462
	public const float EGG_TO_SHELL_RATIO = 0.5f;
}
