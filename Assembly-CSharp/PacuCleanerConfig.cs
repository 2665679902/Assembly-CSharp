using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200010F RID: 271
[EntityConfigOrder(1)]
public class PacuCleanerConfig : IEntityConfig
{
	// Token: 0x0600051B RID: 1307 RVA: 0x00022D30 File Offset: 0x00020F30
	public static GameObject CreatePacu(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BasePacuConfig.CreatePrefab(id, "PacuCleanerBaseTrait", name, desc, anim_file, is_baby, "glp_", 243.15f, 278.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, PacuTuning.PEN_SIZE_PER_CREATURE);
		if (!is_baby)
		{
			Storage storage = gameObject.AddComponent<Storage>();
			storage.capacityKg = 10f;
			storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
			PassiveElementConsumer passiveElementConsumer = gameObject.AddOrGet<PassiveElementConsumer>();
			passiveElementConsumer.elementToConsume = SimHashes.DirtyWater;
			passiveElementConsumer.consumptionRate = 0.2f;
			passiveElementConsumer.capacityKG = 10f;
			passiveElementConsumer.consumptionRadius = 3;
			passiveElementConsumer.showInStatusPanel = true;
			passiveElementConsumer.sampleCellOffset = new Vector3(0f, 0f, 0f);
			passiveElementConsumer.isRequired = false;
			passiveElementConsumer.storeOnConsume = true;
			passiveElementConsumer.showDescriptor = false;
			gameObject.AddOrGet<UpdateElementConsumerPosition>();
			BubbleSpawner bubbleSpawner = gameObject.AddComponent<BubbleSpawner>();
			bubbleSpawner.element = SimHashes.Water;
			bubbleSpawner.emitMass = 2f;
			bubbleSpawner.emitVariance = 0.5f;
			bubbleSpawner.initialVelocity = new Vector2f(0, 1);
			ElementConverter elementConverter = gameObject.AddOrGet<ElementConverter>();
			elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
			{
				new ElementConverter.ConsumedElement(SimHashes.DirtyWater.CreateTag(), 0.2f, true)
			};
			elementConverter.outputElements = new ElementConverter.OutputElement[]
			{
				new ElementConverter.OutputElement(0.2f, SimHashes.Water, 0f, true, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
			};
		}
		return gameObject;
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00022E9C File Offset: 0x0002109C
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00022EA4 File Offset: 0x000210A4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(EntityTemplates.ExtendEntityToWildCreature(PacuCleanerConfig.CreatePacu("PacuCleaner", STRINGS.CREATURES.SPECIES.PACU.VARIANT_CLEANER.NAME, STRINGS.CREATURES.SPECIES.PACU.VARIANT_CLEANER.DESC, "pacu_kanim", false), PacuTuning.PEN_SIZE_PER_CREATURE), "PacuCleanerEgg", STRINGS.CREATURES.SPECIES.PACU.VARIANT_CLEANER.EGG_NAME, STRINGS.CREATURES.SPECIES.PACU.VARIANT_CLEANER.DESC, "egg_pacu_kanim", PacuTuning.EGG_MASS, "PacuCleanerBaby", 15.000001f, 5f, PacuTuning.EGG_CHANCES_CLEANER, 501, false, true, false, 0.75f, false);
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00022F29 File Offset: 0x00021129
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00022F2C File Offset: 0x0002112C
	public void OnSpawn(GameObject inst)
	{
		ElementConsumer component = inst.GetComponent<ElementConsumer>();
		if (component != null)
		{
			component.EnableConsumption(true);
		}
	}

	// Token: 0x0400036C RID: 876
	public const string ID = "PacuCleaner";

	// Token: 0x0400036D RID: 877
	public const string BASE_TRAIT_ID = "PacuCleanerBaseTrait";

	// Token: 0x0400036E RID: 878
	public const string EGG_ID = "PacuCleanerEgg";

	// Token: 0x0400036F RID: 879
	public const float POLLUTED_WATER_CONVERTED_PER_CYCLE = 120f;

	// Token: 0x04000370 RID: 880
	public const SimHashes INPUT_ELEMENT = SimHashes.DirtyWater;

	// Token: 0x04000371 RID: 881
	public const SimHashes OUTPUT_ELEMENT = SimHashes.Water;

	// Token: 0x04000372 RID: 882
	public static readonly EffectorValues DECOR = TUNING.BUILDINGS.DECOR.BONUS.TIER4;

	// Token: 0x04000373 RID: 883
	public const int EGG_SORT_ORDER = 501;
}
