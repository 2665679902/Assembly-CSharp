using System;
using UnityEngine;

// Token: 0x02000245 RID: 581
public class BackgroundEarthConfig : IEntityConfig
{
	// Token: 0x06000B6C RID: 2924 RVA: 0x00041592 File Offset: 0x0003F792
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x0004159C File Offset: 0x0003F79C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(BackgroundEarthConfig.ID, BackgroundEarthConfig.ID, true);
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("earth_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "idle";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<LoopingSounds>();
		return gameObject;
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x00041607 File Offset: 0x0003F807
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00041609 File Offset: 0x0003F809
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006D2 RID: 1746
	public static string ID = "BackgroundEarth";
}
