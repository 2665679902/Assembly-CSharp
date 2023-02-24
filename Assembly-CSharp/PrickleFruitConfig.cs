using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class PrickleFruitConfig : IEntityConfig
{
	// Token: 0x06000760 RID: 1888 RVA: 0x0002CEF6 File Offset: 0x0002B0F6
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x0002CF00 File Offset: 0x0002B100
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity(PrickleFruitConfig.ID, ITEMS.FOOD.PRICKLEFRUIT.NAME, ITEMS.FOOD.PRICKLEFRUIT.DESC, 1f, false, Assets.GetAnim("bristleberry_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.PRICKLEFRUIT);
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x0002CF64 File Offset: 0x0002B164
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x0002CF66 File Offset: 0x0002B166
	public void OnSpawn(GameObject inst)
	{
		inst.Subscribe(-10536414, PrickleFruitConfig.OnEatCompleteDelegate);
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0002CF7C File Offset: 0x0002B17C
	private static void OnEatComplete(Edible edible)
	{
		if (edible != null)
		{
			int num = 0;
			float unitsConsumed = edible.unitsConsumed;
			int num2 = Mathf.FloorToInt(unitsConsumed);
			float num3 = unitsConsumed % 1f;
			if (UnityEngine.Random.value < num3)
			{
				num2++;
			}
			for (int i = 0; i < num2; i++)
			{
				if (UnityEngine.Random.value < PrickleFruitConfig.SEEDS_PER_FRUIT_CHANCE)
				{
					num++;
				}
			}
			if (num > 0)
			{
				Vector3 vector = edible.transform.GetPosition() + new Vector3(0f, 0.05f, 0f);
				vector = Grid.CellToPosCCC(Grid.PosToCell(vector), Grid.SceneLayer.Ore);
				GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(new Tag("PrickleFlowerSeed")), vector, Grid.SceneLayer.Ore, null, 0);
				PrimaryElement component = edible.GetComponent<PrimaryElement>();
				PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
				component2.Temperature = component.Temperature;
				component2.Units = (float)num;
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x040004DE RID: 1246
	public static float SEEDS_PER_FRUIT_CHANCE = 0.05f;

	// Token: 0x040004DF RID: 1247
	public static string ID = "PrickleFruit";

	// Token: 0x040004E0 RID: 1248
	private static readonly EventSystem.IntraObjectHandler<Edible> OnEatCompleteDelegate = new EventSystem.IntraObjectHandler<Edible>(delegate(Edible component, object data)
	{
		PrickleFruitConfig.OnEatComplete(component);
	});
}
