using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class OrbitalResearchDatabankConfig : IEntityConfig
{
	// Token: 0x0600091C RID: 2332 RVA: 0x00035467 File Offset: 0x00033667
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00035470 File Offset: 0x00033670
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("OrbitalResearchDatabank", ITEMS.INDUSTRIAL_PRODUCTS.ORBITAL_RESEARCH_DATABANK.NAME, ITEMS.INDUSTRIAL_PRODUCTS.ORBITAL_RESEARCH_DATABANK.DESC, 1f, true, Assets.GetAnim("floppy_disc_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.Experimental
		});
		gameObject.AddOrGet<EntitySplitter>().maxStackSize = (float)ROCKETRY.DESTINATION_RESEARCH.BASIC;
		return gameObject;
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x000354F5 File Offset: 0x000336F5
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x000354F7 File Offset: 0x000336F7
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005BD RID: 1469
	public const string ID = "OrbitalResearchDatabank";

	// Token: 0x040005BE RID: 1470
	public static readonly Tag TAG = TagManager.Create("OrbitalResearchDatabank");

	// Token: 0x040005BF RID: 1471
	public const float MASS = 1f;
}
