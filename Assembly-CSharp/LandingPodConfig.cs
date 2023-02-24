using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E3 RID: 483
public class LandingPodConfig : IEntityConfig
{
	// Token: 0x06000984 RID: 2436 RVA: 0x00037092 File Offset: 0x00035292
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0003709C File Offset: 0x0003529C
	public GameObject CreatePrefab()
	{
		string text = "LandingPod";
		string text2 = STRINGS.BUILDINGS.PREFABS.LANDING_POD.NAME;
		string text3 = STRINGS.BUILDINGS.PREFABS.LANDING_POD.DESC;
		float num = 2000f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("rocket_puft_pod_kanim"), "grounded", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<PodLander>();
		gameObject.AddOrGet<MinionStorage>();
		return gameObject;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0003710B File Offset: 0x0003530B
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00037122 File Offset: 0x00035322
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005E9 RID: 1513
	public const string ID = "LandingPod";
}
