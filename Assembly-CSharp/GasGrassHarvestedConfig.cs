using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200025A RID: 602
public class GasGrassHarvestedConfig : IEntityConfig
{
	// Token: 0x06000BEC RID: 3052 RVA: 0x00042E8C File Offset: 0x0004108C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00042E94 File Offset: 0x00041094
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("GasGrassHarvested", CREATURES.SPECIES.GASGRASS.NAME, CREATURES.SPECIES.GASGRASS.DESC, 1f, false, Assets.GetAnim("harvested_gassygrass_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.Other });
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x00042F04 File Offset: 0x00041104
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00042F06 File Offset: 0x00041106
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006EF RID: 1775
	public const string ID = "GasGrassHarvested";
}
