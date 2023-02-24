using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class PropSkeletonConfig : IEntityConfig
{
	// Token: 0x06000E5C RID: 3676 RVA: 0x0004DDD6 File Offset: 0x0004BFD6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x0004DDE0 File Offset: 0x0004BFE0
	public GameObject CreatePrefab()
	{
		string text = "PropSkeleton";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPSKELETON.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPSKELETON.DESC;
		float num = 50f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER5;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("skeleton_poi_kanim"), "off", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Creature, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x0004DE73 File Offset: 0x0004C073
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x0004DE8A File Offset: 0x0004C08A
	public void OnSpawn(GameObject inst)
	{
	}
}
