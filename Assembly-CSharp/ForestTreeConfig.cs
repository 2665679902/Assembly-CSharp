using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class ForestTreeConfig : IEntityConfig
{
	// Token: 0x0600063E RID: 1598 RVA: 0x0002828E File Offset: 0x0002648E
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00028298 File Offset: 0x00026498
	public GameObject CreatePrefab()
	{
		string text = "ForestTree";
		string text2 = STRINGS.CREATURES.SPECIES.WOOD_TREE.NAME;
		string text3 = STRINGS.CREATURES.SPECIES.WOOD_TREE.DESC;
		float num = 2f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("tree_kanim"), "idle_empty", Grid.SceneLayer.Building, 1, 2, tier, default(EffectorValues), SimHashes.Creature, new List<Tag>(), 298.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 258.15f, 288.15f, 313.15f, 448.15f, null, true, 0f, 0.15f, "WoodLog", true, true, true, false, 2400f, 0f, 9800f, "ForestTreeOriginal", STRINGS.CREATURES.SPECIES.WOOD_TREE.NAME);
		gameObject.AddOrGet<BuddingTrunk>();
		gameObject.UpdateComponentRequirement(false);
		Tag tag = ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag;
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = tag,
				massConsumptionRate = 0.11666667f
			}
		});
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Dirt,
				massConsumptionRate = 0.016666668f
			}
		});
		gameObject.AddComponent<StandardCropPlant>();
		gameObject.AddOrGet<BuddingTrunk>().budPrefabID = "ForestTreeBranch";
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Hidden, "ForestTreeSeed", STRINGS.CREATURES.SPECIES.SEEDS.WOOD_TREE.NAME, STRINGS.CREATURES.SPECIES.SEEDS.WOOD_TREE.DESC, Assets.GetAnim("seed_tree_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 4, STRINGS.CREATURES.SPECIES.WOOD_TREE.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "ForestTree_preview", Assets.GetAnim("tree_kanim"), "place", 3, 3);
		return gameObject;
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0002847B File Offset: 0x0002667B
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0002847D File Offset: 0x0002667D
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400043C RID: 1084
	public const string ID = "ForestTree";

	// Token: 0x0400043D RID: 1085
	public const string SEED_ID = "ForestTreeSeed";

	// Token: 0x0400043E RID: 1086
	public const float FERTILIZATION_RATE = 0.016666668f;

	// Token: 0x0400043F RID: 1087
	public const float WATER_RATE = 0.11666667f;

	// Token: 0x04000440 RID: 1088
	public const float BRANCH_GROWTH_TIME = 2100f;

	// Token: 0x04000441 RID: 1089
	public const int NUM_BRANCHES = 7;
}
