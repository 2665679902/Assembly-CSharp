using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000269 RID: 617
public class MinionUIPortrait : IEntityConfig
{
	// Token: 0x06000C4C RID: 3148 RVA: 0x00046263 File Offset: 0x00044463
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0004626C File Offset: 0x0004446C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MinionUIPortrait.ID, MinionUIPortrait.ID, true);
		RectTransform rectTransform = gameObject.AddOrGet<RectTransform>();
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.pivot = new Vector2(0.5f, 0f);
		rectTransform.anchoredPosition = new Vector2(0f, 0f);
		rectTransform.sizeDelta = new Vector2(0f, 0f);
		LayoutElement layoutElement = gameObject.AddOrGet<LayoutElement>();
		layoutElement.preferredHeight = 100f;
		layoutElement.preferredWidth = 100f;
		gameObject.AddOrGet<BoxCollider2D>().size = new Vector2(1f, 1f);
		gameObject.AddOrGet<Accessorizer>();
		gameObject.AddOrGet<WearableAccessorizer>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.materialType = KAnimBatchGroup.MaterialType.UI;
		kbatchedAnimController.animScale = 0.5f;
		kbatchedAnimController.setScaleFromAnim = false;
		kbatchedAnimController.animOverrideSize = new Vector2(100f, 120f);
		kbatchedAnimController.AnimFiles = new KAnimFile[]
		{
			Assets.GetAnim("body_comp_default_kanim"),
			Assets.GetAnim("anim_idles_default_kanim"),
			Assets.GetAnim("anim_idle_healthy_kanim"),
			Assets.GetAnim("anim_cheer_kanim"),
			Assets.GetAnim("inventory_screen_dupe_kanim"),
			Assets.GetAnim("anim_react_wave_shy_kanim")
		};
		SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		MinionConfig.ConfigureSymbols(gameObject, false);
		return gameObject;
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x000463F5 File Offset: 0x000445F5
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x000463F7 File Offset: 0x000445F7
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400073A RID: 1850
	public static string ID = "MinionUIPortrait";
}
