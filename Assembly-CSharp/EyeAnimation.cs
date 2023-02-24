using System;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class EyeAnimation : IEntityConfig
{
	// Token: 0x06000BE0 RID: 3040 RVA: 0x00042C9D File Offset: 0x00040E9D
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00042CA4 File Offset: 0x00040EA4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(EyeAnimation.ID, EyeAnimation.ID, false);
		gameObject.AddOrGet<KBatchedAnimController>().AnimFiles = new KAnimFile[] { Assets.GetAnim("anim_blinks_kanim") };
		return gameObject;
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00042CE6 File Offset: 0x00040EE6
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00042CE8 File Offset: 0x00040EE8
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006ED RID: 1773
	public static string ID = "EyeAnimation";
}
