using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000A0B RID: 2571
public class LoadingOverlay : KModalScreen
{
	// Token: 0x06004D68 RID: 19816 RVA: 0x001B48A7 File Offset: 0x001B2AA7
	protected override void OnPrefabInit()
	{
		this.pause = false;
		this.fadeIn = false;
		base.OnPrefabInit();
	}

	// Token: 0x06004D69 RID: 19817 RVA: 0x001B48BD File Offset: 0x001B2ABD
	private void Update()
	{
		if (!this.loadNextFrame && this.showLoad)
		{
			this.loadNextFrame = true;
			this.showLoad = false;
			return;
		}
		if (this.loadNextFrame)
		{
			this.loadNextFrame = false;
			this.loadCb();
		}
	}

	// Token: 0x06004D6A RID: 19818 RVA: 0x001B48F8 File Offset: 0x001B2AF8
	public static void DestroyInstance()
	{
		LoadingOverlay.instance = null;
	}

	// Token: 0x06004D6B RID: 19819 RVA: 0x001B4900 File Offset: 0x001B2B00
	public static void Load(System.Action cb)
	{
		GameObject gameObject = GameObject.Find("/SceneInitializerFE/FrontEndManager");
		if (LoadingOverlay.instance == null)
		{
			LoadingOverlay.instance = Util.KInstantiateUI<LoadingOverlay>(ScreenPrefabs.Instance.loadingOverlay.gameObject, (GameScreenManager.Instance == null) ? gameObject : GameScreenManager.Instance.ssOverlayCanvas, false);
			LoadingOverlay.instance.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.LOADING);
		}
		if (GameScreenManager.Instance != null)
		{
			LoadingOverlay.instance.transform.SetParent(GameScreenManager.Instance.ssOverlayCanvas.transform);
			LoadingOverlay.instance.transform.SetSiblingIndex(GameScreenManager.Instance.ssOverlayCanvas.transform.childCount - 1);
		}
		else
		{
			LoadingOverlay.instance.transform.SetParent(gameObject.transform);
			LoadingOverlay.instance.transform.SetSiblingIndex(gameObject.transform.childCount - 1);
			if (MainMenu.Instance != null)
			{
				MainMenu.Instance.StopAmbience();
			}
		}
		LoadingOverlay.instance.loadCb = cb;
		LoadingOverlay.instance.showLoad = true;
		LoadingOverlay.instance.Activate();
	}

	// Token: 0x06004D6C RID: 19820 RVA: 0x001B4A2C File Offset: 0x001B2C2C
	public static void Clear()
	{
		if (LoadingOverlay.instance != null)
		{
			LoadingOverlay.instance.Deactivate();
		}
	}

	// Token: 0x040032F9 RID: 13049
	private bool loadNextFrame;

	// Token: 0x040032FA RID: 13050
	private bool showLoad;

	// Token: 0x040032FB RID: 13051
	private System.Action loadCb;

	// Token: 0x040032FC RID: 13052
	private static LoadingOverlay instance;
}
