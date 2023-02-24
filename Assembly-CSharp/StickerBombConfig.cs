using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class StickerBombConfig : IEntityConfig
{
	// Token: 0x06001026 RID: 4134 RVA: 0x000574E1 File Offset: 0x000556E1
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x000574E8 File Offset: 0x000556E8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateBasicEntity("StickerBomb", STRINGS.BUILDINGS.PREFABS.STICKERBOMB.NAME, STRINGS.BUILDINGS.PREFABS.STICKERBOMB.DESC, 1f, true, Assets.GetAnim("sticker_a_kanim"), "off", Grid.SceneLayer.Backwall, SimHashes.Creature, null, 293f);
		EntityTemplates.AddCollision(gameObject, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f);
		gameObject.AddOrGet<StickerBomb>();
		return gameObject;
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x00057552 File Offset: 0x00055752
	public void OnPrefabInit(GameObject inst)
	{
		inst.AddOrGet<OccupyArea>().OccupiedCellsOffsets = new CellOffset[1];
		inst.AddComponent<Modifiers>();
		inst.AddOrGet<DecorProvider>().SetValues(DECOR.BONUS.TIER2);
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x0005757C File Offset: 0x0005577C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040008D1 RID: 2257
	public const string ID = "StickerBomb";
}
