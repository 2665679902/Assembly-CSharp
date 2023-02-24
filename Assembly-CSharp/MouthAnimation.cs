using System;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class MouthAnimation : IEntityConfig
{
	// Token: 0x06000C52 RID: 3154 RVA: 0x0004640D File Offset: 0x0004460D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x00046414 File Offset: 0x00044614
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MouthAnimation.ID, MouthAnimation.ID, false);
		gameObject.AddOrGet<KBatchedAnimController>().AnimFiles = new KAnimFile[] { Assets.GetAnim("anim_mouth_flap_kanim") };
		return gameObject;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00046456 File Offset: 0x00044656
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00046458 File Offset: 0x00044658
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400073B RID: 1851
	public static string ID = "MouthAnimation";
}
