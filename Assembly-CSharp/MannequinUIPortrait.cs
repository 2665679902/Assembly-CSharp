using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000264 RID: 612
public class MannequinUIPortrait : IEntityConfig
{
	// Token: 0x06000C29 RID: 3113 RVA: 0x000443CF File Offset: 0x000425CF
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_ALL_VERSIONS;
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x000443D8 File Offset: 0x000425D8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MannequinUIPortrait.ID, MannequinUIPortrait.ID, true);
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
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("mannequin_kanim") };
		SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		MinionConfig.ConfigureSymbols(gameObject, false);
		return gameObject;
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00044507 File Offset: 0x00042707
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00044509 File Offset: 0x00042709
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0400072C RID: 1836
	public static string ID = "MannequinUIPortrait";
}
