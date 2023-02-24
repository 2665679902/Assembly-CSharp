﻿using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public class LadderPOIConfig : IEntityConfig
{
	// Token: 0x06000978 RID: 2424 RVA: 0x00036EB9 File Offset: 0x000350B9
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00036EC0 File Offset: 0x000350C0
	public GameObject CreatePrefab()
	{
		int num = 1;
		int num2 = 1;
		string text = "PropLadder";
		string text2 = STRINGS.BUILDINGS.PREFABS.PROPLADDER.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.PROPLADDER.DESC;
		float num3 = 50f;
		int num4 = num;
		int num5 = num2;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num3, Assets.GetAnim("ladder_poi_kanim"), "off", Grid.SceneLayer.Building, num4, num5, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Polypropylene, true);
		component.Temperature = 294.15f;
		Ladder ladder = gameObject.AddOrGet<Ladder>();
		ladder.upwardsMovementSpeedMultiplier = 1.5f;
		ladder.downwardsMovementSpeedMultiplier = 1.5f;
		gameObject.AddOrGet<AnimTileable>();
		UnityEngine.Object.DestroyImmediate(gameObject.AddOrGet<OccupyArea>());
		OccupyArea occupyArea = gameObject.AddOrGet<OccupyArea>();
		occupyArea.OccupiedCellsOffsets = EntityTemplates.GenerateOffsets(num, num2);
		occupyArea.objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00036FB2 File Offset: 0x000351B2
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00036FB4 File Offset: 0x000351B4
	public void OnSpawn(GameObject inst)
	{
	}
}
