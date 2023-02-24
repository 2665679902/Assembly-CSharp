using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class MushBarConfig : IEntityConfig
{
	// Token: 0x0600073F RID: 1855 RVA: 0x0002CA42 File Offset: 0x0002AC42
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0002CA4C File Offset: 0x0002AC4C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("MushBar", ITEMS.FOOD.MUSHBAR.NAME, ITEMS.FOOD.MUSHBAR.DESC, 1f, false, Assets.GetAnim("mushbar_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		gameObject = EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.MUSHBAR);
		ComplexRecipeManager.Get().GetRecipe(MushBarConfig.recipe.id).FabricationVisualizer = MushBarConfig.CreateFabricationVisualizer(gameObject);
		return gameObject;
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0002CAD3 File Offset: 0x0002ACD3
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0002CAD5 File Offset: 0x0002ACD5
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0002CAD8 File Offset: 0x0002ACD8
	public static GameObject CreateFabricationVisualizer(GameObject result)
	{
		KBatchedAnimController component = result.GetComponent<KBatchedAnimController>();
		GameObject gameObject = new GameObject();
		gameObject.name = result.name + "Visualizer";
		gameObject.SetActive(false);
		gameObject.transform.SetLocalPosition(Vector3.zero);
		KBatchedAnimController kbatchedAnimController = gameObject.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = component.AnimFiles;
		kbatchedAnimController.initialAnim = "fabricating";
		kbatchedAnimController.isMovable = true;
		KBatchedAnimTracker kbatchedAnimTracker = gameObject.AddComponent<KBatchedAnimTracker>();
		kbatchedAnimTracker.symbol = new HashedString("meter_ration");
		kbatchedAnimTracker.offset = Vector3.zero;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		return gameObject;
	}

	// Token: 0x040004D3 RID: 1235
	public const string ID = "MushBar";

	// Token: 0x040004D4 RID: 1236
	public static ComplexRecipe recipe;
}
