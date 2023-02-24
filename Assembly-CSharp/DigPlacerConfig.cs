using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002A6 RID: 678
public class DigPlacerConfig : CommonPlacerConfig, IEntityConfig
{
	// Token: 0x06000D75 RID: 3445 RVA: 0x0004AD00 File Offset: 0x00048F00
	public GameObject CreatePrefab()
	{
		GameObject gameObject = base.CreatePrefab(DigPlacerConfig.ID, MISC.PLACERS.DIGPLACER.NAME, Assets.instance.digPlacerAssets.materials[0]);
		Diggable diggable = gameObject.AddOrGet<Diggable>();
		diggable.workTime = 5f;
		diggable.synchronizeAnims = false;
		diggable.workAnims = new HashedString[] { "place", "release" };
		diggable.materials = Assets.instance.digPlacerAssets.materials;
		diggable.materialDisplay = gameObject.GetComponentInChildren<MeshRenderer>(true);
		gameObject.AddOrGet<CancellableDig>();
		return gameObject;
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x0004ADA5 File Offset: 0x00048FA5
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0004ADA7 File Offset: 0x00048FA7
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040007CF RID: 1999
	public static string ID = "DigPlacer";

	// Token: 0x02000EF7 RID: 3831
	[Serializable]
	public class DigPlacerAssets
	{
		// Token: 0x040052D1 RID: 21201
		public Material[] materials;
	}
}
