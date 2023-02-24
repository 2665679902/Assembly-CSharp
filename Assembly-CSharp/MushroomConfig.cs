using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000178 RID: 376
public class MushroomConfig : IEntityConfig
{
	// Token: 0x06000745 RID: 1861 RVA: 0x0002CB70 File Offset: 0x0002AD70
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0002CB78 File Offset: 0x0002AD78
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity(MushroomConfig.ID, ITEMS.FOOD.MUSHROOM.NAME, ITEMS.FOOD.MUSHROOM.DESC, 1f, false, Assets.GetAnim("funguscap_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.77f, 0.48f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.MUSHROOM);
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x0002CBDC File Offset: 0x0002ADDC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0002CBDE File Offset: 0x0002ADDE
	public void OnSpawn(GameObject inst)
	{
		inst.Subscribe(-10536414, MushroomConfig.OnEatCompleteDelegate);
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
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
				if (UnityEngine.Random.value < MushroomConfig.SEEDS_PER_FRUIT_CHANCE)
				{
					num++;
				}
			}
			if (num > 0)
			{
				Vector3 vector = edible.transform.GetPosition() + new Vector3(0f, 0.05f, 0f);
				vector = Grid.CellToPosCCC(Grid.PosToCell(vector), Grid.SceneLayer.Ore);
				GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(new Tag("MushroomSeed")), vector, Grid.SceneLayer.Ore, null, 0);
				PrimaryElement component = edible.GetComponent<PrimaryElement>();
				PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
				component2.Temperature = component.Temperature;
				component2.Units = (float)num;
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x040004D5 RID: 1237
	public static float SEEDS_PER_FRUIT_CHANCE = 0.05f;

	// Token: 0x040004D6 RID: 1238
	public static string ID = "Mushroom";

	// Token: 0x040004D7 RID: 1239
	private static readonly EventSystem.IntraObjectHandler<Edible> OnEatCompleteDelegate = new EventSystem.IntraObjectHandler<Edible>(delegate(Edible component, object data)
	{
		MushroomConfig.OnEatComplete(component);
	});
}
