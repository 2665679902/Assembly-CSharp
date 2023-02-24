using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A9F RID: 2719
[AddComponentMenu("KMonoBehaviour/scripts/FrontEndManager")]
public class FrontEndManager : KMonoBehaviour
{
	// Token: 0x0600534E RID: 21326 RVA: 0x001E3968 File Offset: 0x001E1B68
	protected override void OnPrefabInit()
	{
		FrontEndManager.<>c__DisplayClass2_0 CS$<>8__locals1 = new FrontEndManager.<>c__DisplayClass2_0();
		CS$<>8__locals1.<>4__this = this;
		base.OnPrefabInit();
		FrontEndManager.Instance = this;
		GameObject gameObject = base.gameObject;
		string highestActiveDlcId = DlcManager.GetHighestActiveDlcId();
		if (highestActiveDlcId == null || (highestActiveDlcId != null && highestActiveDlcId.Length == 0) || !(highestActiveDlcId == "EXPANSION1_ID"))
		{
			Util.KInstantiateUI(ScreenPrefabs.Instance.MainMenuForVanilla, gameObject, true);
		}
		else
		{
			Util.KInstantiateUI(ScreenPrefabs.Instance.MainMenuForSpacedOut, gameObject, true);
		}
		if (!FrontEndManager.firstInit)
		{
			return;
		}
		FrontEndManager.firstInit = false;
		GameObject[] array = new GameObject[]
		{
			ScreenPrefabs.Instance.MainMenuIntroShort,
			ScreenPrefabs.Instance.MainMenuHealthyGameMessage
		};
		for (int i = 0; i < array.Length; i++)
		{
			Util.KInstantiateUI(array[i], gameObject, true);
		}
		CS$<>8__locals1.screensPrefabsToSpawn = new GameObject[]
		{
			ScreenPrefabs.Instance.KleiItemDropScreen,
			ScreenPrefabs.Instance.LockerMenuScreen,
			ScreenPrefabs.Instance.LockerNavigator
		};
		CS$<>8__locals1.gameObjectsToDestroyOnNextCreate = new List<GameObject>();
		FrontEndManager.<>c__DisplayClass2_1 CS$<>8__locals2 = new FrontEndManager.<>c__DisplayClass2_1();
		CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
		CS$<>8__locals2.coroutineRunner = CoroutineRunner.Create();
		UnityEngine.Object.DontDestroyOnLoad(CS$<>8__locals2.coroutineRunner);
		CS$<>8__locals2.CS$<>8__locals1.<OnPrefabInit>g__CreateCanvases|0();
		Singleton<KBatchedAnimUpdater>.Instance.OnClear += CS$<>8__locals2.<OnPrefabInit>g__RecreateCanvases|1;
	}

	// Token: 0x0600534F RID: 21327 RVA: 0x001E3AB1 File Offset: 0x001E1CB1
	protected override void OnForcedCleanUp()
	{
		FrontEndManager.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06005350 RID: 21328 RVA: 0x001E3AC0 File Offset: 0x001E1CC0
	private void LateUpdate()
	{
		if (global::Debug.developerConsoleVisible)
		{
			global::Debug.developerConsoleVisible = false;
		}
		KAnimBatchManager.Instance().UpdateActiveArea(new Vector2I(0, 0), new Vector2I(9999, 9999));
		KAnimBatchManager.Instance().UpdateDirty(Time.frameCount);
		KAnimBatchManager.Instance().Render();
	}

	// Token: 0x06005351 RID: 21329 RVA: 0x001E3B14 File Offset: 0x001E1D14
	public GameObject MakeKleiCanvas(string gameObjectName = "Canvas")
	{
		GameObject gameObject = new GameObject(gameObjectName, new Type[] { typeof(RectTransform) });
		this.ConfigureAsKleiCanvas(gameObject);
		return gameObject;
	}

	// Token: 0x06005352 RID: 21330 RVA: 0x001E3B44 File Offset: 0x001E1D44
	public void ConfigureAsKleiCanvas(GameObject gameObject)
	{
		Canvas canvas = gameObject.AddOrGet<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.sortingOrder = 10;
		canvas.pixelPerfect = false;
		canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.Normal | AdditionalCanvasShaderChannels.Tangent;
		GraphicRaycaster graphicRaycaster = gameObject.AddOrGet<GraphicRaycaster>();
		graphicRaycaster.ignoreReversedGraphics = true;
		graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
		graphicRaycaster.blockingMask = -1;
		CanvasScaler canvasScaler = gameObject.AddOrGet<CanvasScaler>();
		canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
		canvasScaler.referencePixelsPerUnit = 100f;
		gameObject.AddOrGet<KCanvasScaler>();
	}

	// Token: 0x04003877 RID: 14455
	public static FrontEndManager Instance;

	// Token: 0x04003878 RID: 14456
	public static bool firstInit = true;
}
