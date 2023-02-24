using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002A7 RID: 679
public class MopPlacerConfig : CommonPlacerConfig, IEntityConfig
{
	// Token: 0x06000D7A RID: 3450 RVA: 0x0004ADC0 File Offset: 0x00048FC0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = base.CreatePrefab(MopPlacerConfig.ID, MISC.PLACERS.MOPPLACER.NAME, Assets.instance.mopPlacerAssets.material);
		gameObject.AddTag(GameTags.NotConversationTopic);
		Moppable moppable = gameObject.AddOrGet<Moppable>();
		moppable.synchronizeAnims = false;
		moppable.amountMoppedPerTick = 20f;
		gameObject.AddOrGet<Cancellable>();
		return gameObject;
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0004AE1A File Offset: 0x0004901A
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0004AE1C File Offset: 0x0004901C
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040007D0 RID: 2000
	public static string ID = "MopPlacer";

	// Token: 0x02000EF8 RID: 3832
	[Serializable]
	public class MopPlacerAssets
	{
		// Token: 0x040052D2 RID: 21202
		public Material material;
	}
}
