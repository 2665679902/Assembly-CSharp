using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class FishFeederBotConfig : IEntityConfig
{
	// Token: 0x060005CB RID: 1483 RVA: 0x00026204 File Offset: 0x00024404
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0002620C File Offset: 0x0002440C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("FishFeederBot", "FishFeederBot", true);
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("fishfeeder_kanim") };
		kbatchedAnimController.sceneLayer = Grid.SceneLayer.BuildingBack;
		SymbolOverrideControllerUtil.AddToPrefab(kbatchedAnimController.gameObject);
		return gameObject;
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x00026264 File Offset: 0x00024464
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x00026266 File Offset: 0x00024466
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003FD RID: 1021
	public const string ID = "FishFeederBot";
}
