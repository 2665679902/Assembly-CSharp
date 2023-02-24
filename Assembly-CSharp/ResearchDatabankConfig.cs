using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class ResearchDatabankConfig : IEntityConfig
{
	// Token: 0x06000928 RID: 2344 RVA: 0x000355A9 File Offset: 0x000337A9
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x000355B0 File Offset: 0x000337B0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("ResearchDatabank", ITEMS.INDUSTRIAL_PRODUCTS.RESEARCH_DATABANK.NAME, ITEMS.INDUSTRIAL_PRODUCTS.RESEARCH_DATABANK.DESC, 1f, true, Assets.GetAnim("floppy_disc_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.Experimental
		});
		gameObject.AddOrGet<EntitySplitter>().maxStackSize = (float)ROCKETRY.DESTINATION_RESEARCH.BASIC;
		return gameObject;
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00035635 File Offset: 0x00033835
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00035637 File Offset: 0x00033837
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005C3 RID: 1475
	public const string ID = "ResearchDatabank";

	// Token: 0x040005C4 RID: 1476
	public static readonly Tag TAG = TagManager.Create("ResearchDatabank");

	// Token: 0x040005C5 RID: 1477
	public const float MASS = 1f;
}
