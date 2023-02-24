using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000259 RID: 601
public class FullMinionUIPortrait : IEntityConfig
{
	// Token: 0x06000BE6 RID: 3046 RVA: 0x00042CFE File Offset: 0x00040EFE
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00042D08 File Offset: 0x00040F08
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(FullMinionUIPortrait.ID, FullMinionUIPortrait.ID, true);
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
		gameObject.AddOrGet<FaceGraph>();
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
			Assets.GetAnim("anim_cheer_kanim")
		};
		SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		MinionConfig.ConfigureSymbols(gameObject, true);
		return gameObject;
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00042E74 File Offset: 0x00041074
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00042E76 File Offset: 0x00041076
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040006EE RID: 1774
	public static string ID = "FullMinionUIPortrait";
}
