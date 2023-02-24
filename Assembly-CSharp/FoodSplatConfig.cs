using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class FoodSplatConfig : IEntityConfig
{
	// Token: 0x060007C3 RID: 1987 RVA: 0x0002D9F8 File Offset: 0x0002BBF8
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0002DA00 File Offset: 0x0002BC00
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateBasicEntity("FoodSplat", ITEMS.FOOD.FOODSPLAT.NAME, ITEMS.FOOD.FOODSPLAT.DESC, 1f, true, Assets.GetAnim("sticker_a_kanim"), "idle_sticker_a", Grid.SceneLayer.Backwall, SimHashes.Creature, null, 293f);
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0002DA51 File Offset: 0x0002BC51
	public void OnPrefabInit(GameObject inst)
	{
		inst.AddOrGet<OccupyArea>().OccupiedCellsOffsets = new CellOffset[1];
		inst.AddComponent<Modifiers>();
		inst.AddOrGet<KSelectable>();
		inst.AddOrGet<DecorProvider>().SetValues(DECOR.PENALTY.TIER2);
		inst.AddOrGetDef<Splat.Def>();
		inst.AddOrGet<SplatWorkable>();
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0002DA90 File Offset: 0x0002BC90
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004FF RID: 1279
	public const string ID = "FoodSplat";
}
