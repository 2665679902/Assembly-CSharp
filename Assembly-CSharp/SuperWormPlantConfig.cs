using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class SuperWormPlantConfig : IEntityConfig
{
	// Token: 0x06000698 RID: 1688 RVA: 0x0002AC1E File Offset: 0x00028E1E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0002AC28 File Offset: 0x00028E28
	public GameObject CreatePrefab()
	{
		GameObject gameObject = WormPlantConfig.BaseWormPlant("SuperWormPlant", STRINGS.CREATURES.SPECIES.SUPERWORMPLANT.NAME, STRINGS.CREATURES.SPECIES.SUPERWORMPLANT.DESC, "wormwood_kanim", SuperWormPlantConfig.SUPER_DECOR, "WormSuperFruit");
		gameObject.AddOrGet<SeedProducer>().Configure("WormPlantSeed", SeedProducer.ProductionType.Harvest, 1);
		return gameObject;
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0002AC74 File Offset: 0x00028E74
	public void OnPrefabInit(GameObject prefab)
	{
		TransformingPlant transformingPlant = prefab.AddOrGet<TransformingPlant>();
		transformingPlant.SubscribeToTransformEvent(GameHashes.HarvestComplete);
		transformingPlant.transformPlantId = "WormPlant";
		prefab.GetComponent<KAnimControllerBase>().SetSymbolVisiblity("flower", false);
		prefab.AddOrGet<StandardCropPlant>().anims = SuperWormPlantConfig.animSet;
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0002ACC2 File Offset: 0x00028EC2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400048D RID: 1165
	public const string ID = "SuperWormPlant";

	// Token: 0x0400048E RID: 1166
	public static readonly EffectorValues SUPER_DECOR = DECOR.BONUS.TIER1;

	// Token: 0x0400048F RID: 1167
	public const string SUPER_CROP_ID = "WormSuperFruit";

	// Token: 0x04000490 RID: 1168
	public const int CROP_YIELD = 8;

	// Token: 0x04000491 RID: 1169
	private static StandardCropPlant.AnimSet animSet = new StandardCropPlant.AnimSet
	{
		grow = "super_grow",
		grow_pst = "super_grow_pst",
		idle_full = "super_idle_full",
		wilt_base = "super_wilt",
		harvest = "super_harvest"
	};
}
