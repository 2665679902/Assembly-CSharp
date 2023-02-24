using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000050 RID: 80
[AddComponentMenu("KMonoBehaviour/Plugins/GameScreenManager")]
public class GameScreenManager : KMonoBehaviour
{
	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000328 RID: 808 RVA: 0x00011166 File Offset: 0x0000F366
	// (set) Token: 0x06000329 RID: 809 RVA: 0x0001116D File Offset: 0x0000F36D
	public static GameScreenManager Instance { get; private set; }

	// Token: 0x0600032A RID: 810 RVA: 0x00011175 File Offset: 0x0000F375
	public static void DestroyInstance()
	{
		GameScreenManager.Instance = null;
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x0600032B RID: 811 RVA: 0x0001117D File Offset: 0x0000F37D
	public static Color[] UIColors
	{
		get
		{
			return GameScreenManager.Instance.uiColors;
		}
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00011189 File Offset: 0x0000F389
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		global::Debug.Assert(GameScreenManager.Instance == null);
		GameScreenManager.Instance = this;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x000111A7 File Offset: 0x0000F3A7
	protected override void OnCleanUp()
	{
		global::Debug.Assert(GameScreenManager.Instance != null);
		GameScreenManager.Instance = null;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x000111BF File Offset: 0x0000F3BF
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x0600032F RID: 815 RVA: 0x000111C8 File Offset: 0x0000F3C8
	public Camera GetCamera(GameScreenManager.UIRenderTarget target)
	{
		switch (target)
		{
		case GameScreenManager.UIRenderTarget.WorldSpace:
			return this.worldSpaceCanvas.GetComponent<Canvas>().worldCamera;
		case GameScreenManager.UIRenderTarget.ScreenSpaceCamera:
			return this.ssCameraCanvas.GetComponent<Canvas>().worldCamera;
		case GameScreenManager.UIRenderTarget.ScreenSpaceOverlay:
			return this.ssOverlayCanvas.GetComponent<Canvas>().worldCamera;
		case GameScreenManager.UIRenderTarget.HoverTextScreen:
			return this.ssHoverTextCanvas.GetComponent<Canvas>().worldCamera;
		case GameScreenManager.UIRenderTarget.ScreenshotModeCamera:
			return this.screenshotModeCanvas.GetComponent<Canvas>().worldCamera;
		default:
			return base.gameObject.GetComponent<Canvas>().worldCamera;
		}
	}

	// Token: 0x06000330 RID: 816 RVA: 0x00011258 File Offset: 0x0000F458
	public void SetCamera(GameScreenManager.UIRenderTarget target, Camera camera)
	{
		switch (target)
		{
		case GameScreenManager.UIRenderTarget.WorldSpace:
			this.worldSpaceCanvas.GetComponent<Canvas>().worldCamera = camera;
			return;
		case GameScreenManager.UIRenderTarget.ScreenSpaceOverlay:
			this.ssOverlayCanvas.GetComponent<Canvas>().worldCamera = camera;
			return;
		case GameScreenManager.UIRenderTarget.ScreenshotModeCamera:
			this.screenshotModeCanvas.GetComponent<Canvas>().worldCamera = camera;
			return;
		}
		this.ssCameraCanvas.GetComponent<Canvas>().worldCamera = camera;
	}

	// Token: 0x06000331 RID: 817 RVA: 0x000112C8 File Offset: 0x0000F4C8
	public GameObject GetParent(GameScreenManager.UIRenderTarget target)
	{
		switch (target)
		{
		case GameScreenManager.UIRenderTarget.WorldSpace:
			return this.worldSpaceCanvas;
		case GameScreenManager.UIRenderTarget.ScreenSpaceCamera:
			return this.ssCameraCanvas;
		case GameScreenManager.UIRenderTarget.ScreenSpaceOverlay:
			return this.ssOverlayCanvas;
		case GameScreenManager.UIRenderTarget.HoverTextScreen:
			return this.ssHoverTextCanvas;
		case GameScreenManager.UIRenderTarget.ScreenshotModeCamera:
			return this.screenshotModeCanvas;
		default:
			return base.gameObject;
		}
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0001131A File Offset: 0x0000F51A
	public GameObject ActivateScreen(GameObject screen, GameObject parent = null, GameScreenManager.UIRenderTarget target = GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)
	{
		if (parent == null)
		{
			parent = this.GetParent(target);
		}
		KScreenManager.AddExistingChild(parent, screen);
		screen.GetComponent<KScreen>().Activate();
		return screen;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00011342 File Offset: 0x0000F542
	public KScreen InstantiateScreen(GameObject screenPrefab, GameObject parent = null, GameScreenManager.UIRenderTarget target = GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)
	{
		if (parent == null)
		{
			parent = this.GetParent(target);
		}
		return KScreenManager.AddChild(parent, screenPrefab).GetComponent<KScreen>();
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00011362 File Offset: 0x0000F562
	public KScreen StartScreen(GameObject screenPrefab, GameObject parent = null, GameScreenManager.UIRenderTarget target = GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)
	{
		if (parent == null)
		{
			parent = this.GetParent(target);
		}
		KScreen component = KScreenManager.AddChild(parent, screenPrefab).GetComponent<KScreen>();
		component.Activate();
		return component;
	}

	// Token: 0x040003E3 RID: 995
	public GameObject ssHoverTextCanvas;

	// Token: 0x040003E4 RID: 996
	public GameObject ssCameraCanvas;

	// Token: 0x040003E5 RID: 997
	public GameObject ssOverlayCanvas;

	// Token: 0x040003E6 RID: 998
	public GameObject worldSpaceCanvas;

	// Token: 0x040003E7 RID: 999
	public GameObject screenshotModeCanvas;

	// Token: 0x040003E8 RID: 1000
	[SerializeField]
	private Color[] uiColors;

	// Token: 0x040003E9 RID: 1001
	public Image fadePlaneBack;

	// Token: 0x040003EA RID: 1002
	public Image fadePlaneFront;

	// Token: 0x020009A6 RID: 2470
	public enum UIRenderTarget
	{
		// Token: 0x04002175 RID: 8565
		WorldSpace,
		// Token: 0x04002176 RID: 8566
		ScreenSpaceCamera,
		// Token: 0x04002177 RID: 8567
		ScreenSpaceOverlay,
		// Token: 0x04002178 RID: 8568
		HoverTextScreen,
		// Token: 0x04002179 RID: 8569
		ScreenshotModeCamera
	}
}
