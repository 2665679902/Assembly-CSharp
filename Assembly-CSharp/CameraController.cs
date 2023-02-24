using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000455 RID: 1109
[AddComponentMenu("KMonoBehaviour/scripts/CameraController")]
public class CameraController : KMonoBehaviour, IInputHandler
{
	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06001818 RID: 6168 RVA: 0x0007EDD7 File Offset: 0x0007CFD7
	public string handlerName
	{
		get
		{
			return base.gameObject.name;
		}
	}

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x06001819 RID: 6169 RVA: 0x0007EDE4 File Offset: 0x0007CFE4
	// (set) Token: 0x0600181A RID: 6170 RVA: 0x0007EE08 File Offset: 0x0007D008
	public float OrthographicSize
	{
		get
		{
			if (!(this.baseCamera == null))
			{
				return this.baseCamera.orthographicSize;
			}
			return 0f;
		}
		set
		{
			for (int i = 0; i < this.cameras.Count; i++)
			{
				this.cameras[i].orthographicSize = value;
			}
		}
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x0600181B RID: 6171 RVA: 0x0007EE3D File Offset: 0x0007D03D
	// (set) Token: 0x0600181C RID: 6172 RVA: 0x0007EE45 File Offset: 0x0007D045
	public KInputHandler inputHandler { get; set; }

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x0600181D RID: 6173 RVA: 0x0007EE4E File Offset: 0x0007D04E
	// (set) Token: 0x0600181E RID: 6174 RVA: 0x0007EE56 File Offset: 0x0007D056
	public float targetOrthographicSize { get; private set; }

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x0600181F RID: 6175 RVA: 0x0007EE5F File Offset: 0x0007D05F
	// (set) Token: 0x06001820 RID: 6176 RVA: 0x0007EE67 File Offset: 0x0007D067
	public bool isTargetPosSet { get; set; }

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06001821 RID: 6177 RVA: 0x0007EE70 File Offset: 0x0007D070
	// (set) Token: 0x06001822 RID: 6178 RVA: 0x0007EE78 File Offset: 0x0007D078
	public Vector3 targetPos { get; private set; }

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x06001823 RID: 6179 RVA: 0x0007EE81 File Offset: 0x0007D081
	// (set) Token: 0x06001824 RID: 6180 RVA: 0x0007EE89 File Offset: 0x0007D089
	public bool ignoreClusterFX { get; private set; }

	// Token: 0x06001825 RID: 6181 RVA: 0x0007EE92 File Offset: 0x0007D092
	public void ToggleClusterFX()
	{
		this.ignoreClusterFX = !this.ignoreClusterFX;
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x0007EEA4 File Offset: 0x0007D0A4
	protected override void OnForcedCleanUp()
	{
		GameInputManager inputManager = Global.GetInputManager();
		if (inputManager == null)
		{
			return;
		}
		inputManager.usedMenus.Remove(this);
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x06001827 RID: 6183 RVA: 0x0007EEC8 File Offset: 0x0007D0C8
	public int cameraActiveCluster
	{
		get
		{
			if (ClusterManager.Instance == null)
			{
				return (int)ClusterManager.INVALID_WORLD_IDX;
			}
			return ClusterManager.Instance.activeWorldId;
		}
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x0007EEE8 File Offset: 0x0007D0E8
	public void GetWorldCamera(out Vector2I worldOffset, out Vector2I worldSize)
	{
		WorldContainer worldContainer = null;
		if (ClusterManager.Instance != null)
		{
			worldContainer = ClusterManager.Instance.activeWorld;
		}
		if (!this.ignoreClusterFX && worldContainer != null)
		{
			worldOffset = worldContainer.WorldOffset;
			worldSize = worldContainer.WorldSize;
			return;
		}
		worldOffset = new Vector2I(0, 0);
		worldSize = new Vector2I(Grid.WidthInCells, Grid.HeightInCells);
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06001829 RID: 6185 RVA: 0x0007EF5B File Offset: 0x0007D15B
	// (set) Token: 0x0600182A RID: 6186 RVA: 0x0007EF63 File Offset: 0x0007D163
	public bool DisableUserCameraControl
	{
		get
		{
			return this.userCameraControlDisabled;
		}
		set
		{
			this.userCameraControlDisabled = value;
			if (this.userCameraControlDisabled)
			{
				this.panning = false;
				this.panLeft = false;
				this.panRight = false;
				this.panUp = false;
				this.panDown = false;
			}
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x0600182B RID: 6187 RVA: 0x0007EF97 File Offset: 0x0007D197
	// (set) Token: 0x0600182C RID: 6188 RVA: 0x0007EF9E File Offset: 0x0007D19E
	public static CameraController Instance { get; private set; }

	// Token: 0x0600182D RID: 6189 RVA: 0x0007EFA6 File Offset: 0x0007D1A6
	public static void DestroyInstance()
	{
		CameraController.Instance = null;
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x0007EFAE File Offset: 0x0007D1AE
	public void ToggleColouredOverlayView(bool enabled)
	{
		this.mrt.ToggleColouredOverlayView(enabled);
	}

	// Token: 0x0600182F RID: 6191 RVA: 0x0007EFBC File Offset: 0x0007D1BC
	protected override void OnPrefabInit()
	{
		global::Util.Reset(base.transform);
		base.transform.SetLocalPosition(new Vector3(Grid.WidthInMeters / 2f, Grid.HeightInMeters / 2f, -100f));
		this.targetOrthographicSize = this.maxOrthographicSize;
		CameraController.Instance = this;
		this.DisableUserCameraControl = false;
		this.baseCamera = this.CopyCamera(Camera.main, "baseCamera");
		this.mrt = this.baseCamera.gameObject.AddComponent<MultipleRenderTarget>();
		this.mrt.onSetupComplete += this.OnMRTSetupComplete;
		this.baseCamera.gameObject.AddComponent<LightBufferCompositor>();
		this.baseCamera.transparencySortMode = TransparencySortMode.Orthographic;
		this.baseCamera.transform.parent = base.transform;
		global::Util.Reset(this.baseCamera.transform);
		int mask = LayerMask.GetMask(new string[] { "PlaceWithDepth", "Overlay" });
		int mask2 = LayerMask.GetMask(new string[] { "Construction" });
		this.baseCamera.cullingMask &= ~mask;
		this.baseCamera.cullingMask |= mask2;
		this.baseCamera.tag = "Untagged";
		this.baseCamera.gameObject.AddComponent<CameraRenderTexture>().TextureName = "_LitTex";
		this.infraredCamera = this.CopyCamera(this.baseCamera, "Infrared");
		this.infraredCamera.cullingMask = 0;
		this.infraredCamera.clearFlags = CameraClearFlags.Color;
		this.infraredCamera.depth = this.baseCamera.depth - 1f;
		this.infraredCamera.transform.parent = base.transform;
		this.infraredCamera.gameObject.AddComponent<Infrared>();
		if (SimDebugView.Instance != null)
		{
			this.simOverlayCamera = this.CopyCamera(this.baseCamera, "SimOverlayCamera");
			this.simOverlayCamera.cullingMask = LayerMask.GetMask(new string[] { "SimDebugView" });
			this.simOverlayCamera.clearFlags = CameraClearFlags.Color;
			this.simOverlayCamera.depth = this.baseCamera.depth + 1f;
			this.simOverlayCamera.transform.parent = base.transform;
			this.simOverlayCamera.gameObject.AddComponent<CameraRenderTexture>().TextureName = "_SimDebugViewTex";
		}
		this.overlayCamera = Camera.main;
		this.overlayCamera.name = "Overlay";
		this.overlayCamera.cullingMask = mask | mask2;
		this.overlayCamera.clearFlags = CameraClearFlags.Nothing;
		this.overlayCamera.transform.parent = base.transform;
		this.overlayCamera.depth = this.baseCamera.depth + 3f;
		this.overlayCamera.transform.SetLocalPosition(Vector3.zero);
		this.overlayCamera.transform.localRotation = Quaternion.identity;
		this.overlayCamera.renderingPath = RenderingPath.Forward;
		this.overlayCamera.allowHDR = false;
		this.overlayCamera.tag = "Untagged";
		this.overlayCamera.gameObject.AddComponent<CameraReferenceTexture>().referenceCamera = this.baseCamera;
		ColorCorrectionLookup component = this.overlayCamera.GetComponent<ColorCorrectionLookup>();
		component.Convert(this.dayColourCube, "");
		component.Convert2(this.nightColourCube, "");
		this.cameras.Add(this.overlayCamera);
		this.lightBufferCamera = this.CopyCamera(this.overlayCamera, "Light Buffer");
		this.lightBufferCamera.clearFlags = CameraClearFlags.Color;
		this.lightBufferCamera.cullingMask = LayerMask.GetMask(new string[] { "Lights" });
		this.lightBufferCamera.depth = this.baseCamera.depth - 1f;
		this.lightBufferCamera.transform.parent = base.transform;
		this.lightBufferCamera.transform.SetLocalPosition(Vector3.zero);
		this.lightBufferCamera.rect = new Rect(0f, 0f, 1f, 1f);
		LightBuffer lightBuffer = this.lightBufferCamera.gameObject.AddComponent<LightBuffer>();
		lightBuffer.Material = this.LightBufferMaterial;
		lightBuffer.CircleMaterial = this.LightCircleOverlay;
		lightBuffer.ConeMaterial = this.LightConeOverlay;
		this.overlayNoDepthCamera = this.CopyCamera(this.overlayCamera, "overlayNoDepth");
		int mask3 = LayerMask.GetMask(new string[] { "Overlay", "Place" });
		this.baseCamera.cullingMask &= ~mask3;
		this.overlayNoDepthCamera.clearFlags = CameraClearFlags.Depth;
		this.overlayNoDepthCamera.cullingMask = mask3;
		this.overlayNoDepthCamera.transform.parent = base.transform;
		this.overlayNoDepthCamera.transform.SetLocalPosition(Vector3.zero);
		this.overlayNoDepthCamera.depth = this.baseCamera.depth + 4f;
		this.overlayNoDepthCamera.tag = "MainCamera";
		this.overlayNoDepthCamera.gameObject.AddComponent<NavPathDrawer>();
		this.uiCamera = this.CopyCamera(this.overlayCamera, "uiCamera");
		this.uiCamera.clearFlags = CameraClearFlags.Depth;
		this.uiCamera.cullingMask = LayerMask.GetMask(new string[] { "UI" });
		this.uiCamera.transform.parent = base.transform;
		this.uiCamera.transform.SetLocalPosition(Vector3.zero);
		this.uiCamera.depth = this.baseCamera.depth + 5f;
		if (Game.Instance != null)
		{
			this.timelapseFreezeCamera = this.CopyCamera(this.uiCamera, "timelapseFreezeCamera");
			this.timelapseFreezeCamera.depth = this.uiCamera.depth + 3f;
			this.timelapseFreezeCamera.gameObject.AddComponent<FillRenderTargetEffect>();
			this.timelapseFreezeCamera.enabled = false;
			Camera camera = CameraController.CloneCamera(this.overlayCamera, "timelapseCamera");
			Timelapser timelapser = camera.gameObject.AddComponent<Timelapser>();
			camera.transparencySortMode = TransparencySortMode.Orthographic;
			camera.depth = this.baseCamera.depth + 2f;
			Game.Instance.timelapser = timelapser;
		}
		if (GameScreenManager.Instance != null)
		{
			for (int i = 0; i < this.uiCameraTargets.Count; i++)
			{
				GameScreenManager.Instance.SetCamera(this.uiCameraTargets[i], this.uiCamera);
			}
			this.infoText = GameScreenManager.Instance.screenshotModeCanvas.GetComponentInChildren<LocText>();
		}
		if (!KPlayerPrefs.HasKey("CameraSpeed"))
		{
			CameraController.SetDefaultCameraSpeed();
		}
		this.SetSpeedFromPrefs(null);
		Game.Instance.Subscribe(75424175, new Action<object>(this.SetSpeedFromPrefs));
	}

	// Token: 0x06001830 RID: 6192 RVA: 0x0007F6A3 File Offset: 0x0007D8A3
	private void SetSpeedFromPrefs(object data = null)
	{
		this.keyPanningSpeed = Mathf.Clamp(0.1f, KPlayerPrefs.GetFloat("CameraSpeed"), 2f);
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x0007F6C4 File Offset: 0x0007D8C4
	public int GetCursorCell()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos());
		Vector3 vector2 = Vector3.Max(ClusterManager.Instance.activeWorld.minimumBounds, vector);
		vector2 = Vector3.Min(ClusterManager.Instance.activeWorld.maximumBounds, vector2);
		return Grid.PosToCell(vector2);
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x0007F71D File Offset: 0x0007D91D
	public static Camera CloneCamera(Camera camera, string name)
	{
		Camera camera2 = new GameObject
		{
			name = name
		}.AddComponent<Camera>();
		camera2.CopyFrom(camera);
		return camera2;
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x0007F738 File Offset: 0x0007D938
	private Camera CopyCamera(Camera camera, string name)
	{
		Camera camera2 = CameraController.CloneCamera(camera, name);
		this.cameras.Add(camera2);
		return camera2;
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x0007F75A File Offset: 0x0007D95A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Restore();
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x0007F768 File Offset: 0x0007D968
	public static void SetDefaultCameraSpeed()
	{
		KPlayerPrefs.SetFloat("CameraSpeed", 1f);
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06001836 RID: 6198 RVA: 0x0007F779 File Offset: 0x0007D979
	// (set) Token: 0x06001837 RID: 6199 RVA: 0x0007F781 File Offset: 0x0007D981
	public Coroutine activeFadeRoutine { get; private set; }

	// Token: 0x06001838 RID: 6200 RVA: 0x0007F78A File Offset: 0x0007D98A
	public void FadeOut(float targetPercentage = 1f, float speed = 1f, System.Action callback = null)
	{
		if (this.activeFadeRoutine != null)
		{
			base.StopCoroutine(this.activeFadeRoutine);
		}
		this.activeFadeRoutine = base.StartCoroutine(this.FadeWithBlack(true, 0f, targetPercentage, speed, null));
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x0007F7BB File Offset: 0x0007D9BB
	public void FadeIn(float targetPercentage = 0f, float speed = 1f, System.Action callback = null)
	{
		if (this.activeFadeRoutine != null)
		{
			base.StopCoroutine(this.activeFadeRoutine);
		}
		this.activeFadeRoutine = base.StartCoroutine(this.FadeWithBlack(true, 1f, targetPercentage, speed, callback));
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x0007F7EC File Offset: 0x0007D9EC
	public void ActiveWorldStarWipe(int id, System.Action callback = null)
	{
		this.ActiveWorldStarWipe(id, false, default(Vector3), 10f, callback);
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x0007F810 File Offset: 0x0007DA10
	public void ActiveWorldStarWipe(int id, Vector3 position, float forceOrthgraphicSize = 10f, System.Action callback = null)
	{
		this.ActiveWorldStarWipe(id, true, position, forceOrthgraphicSize, callback);
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x0007F820 File Offset: 0x0007DA20
	private void ActiveWorldStarWipe(int id, bool useForcePosition, Vector3 forcePosition, float forceOrthgraphicSize, System.Action callback)
	{
		if (this.activeFadeRoutine != null)
		{
			base.StopCoroutine(this.activeFadeRoutine);
		}
		if (ClusterManager.Instance.activeWorldId != id)
		{
			DetailsScreen.Instance.DeselectAndClose();
			this.activeFadeRoutine = base.StartCoroutine(this.SwapToWorldFade(id, useForcePosition, forcePosition, forceOrthgraphicSize, callback));
			return;
		}
		ManagementMenu.Instance.CloseAll();
		if (useForcePosition)
		{
			CameraController.Instance.SetTargetPos(forcePosition, 8f, true);
			if (callback != null)
			{
				callback();
			}
		}
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x0007F89B File Offset: 0x0007DA9B
	private IEnumerator SwapToWorldFade(int worldId, bool useForcePosition, Vector3 forcePosition, float forceOrthgraphicSize, System.Action newWorldCallback)
	{
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().ActiveBaseChangeSnapshot);
		ClusterManager.Instance.UpdateWorldReverbSnapshot(worldId);
		yield return base.StartCoroutine(this.FadeWithBlack(false, 0f, 1f, 3f, null));
		ClusterManager.Instance.SetActiveWorld(worldId);
		if (useForcePosition)
		{
			CameraController.Instance.SetTargetPos(forcePosition, forceOrthgraphicSize, false);
			CameraController.Instance.SetPosition(forcePosition);
		}
		if (newWorldCallback != null)
		{
			newWorldCallback();
		}
		ManagementMenu.Instance.CloseAll();
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().ActiveBaseChangeSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		yield return base.StartCoroutine(this.FadeWithBlack(false, 1f, 0f, 3f, null));
		yield break;
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x0007F8CF File Offset: 0x0007DACF
	public void SetWorldInteractive(bool state)
	{
		GameScreenManager.Instance.fadePlaneFront.raycastTarget = !state;
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x0007F8E4 File Offset: 0x0007DAE4
	private IEnumerator FadeWithBlack(bool fadeUI, float startBlackPercent, float targetBlackPercent, float speed = 1f, System.Action callback = null)
	{
		Image fadePlane = (fadeUI ? GameScreenManager.Instance.fadePlaneFront : GameScreenManager.Instance.fadePlaneBack);
		float percent = 0f;
		while (percent < 1f)
		{
			percent += Time.unscaledDeltaTime * speed;
			float num = MathUtil.ReRange(percent, 0f, 1f, startBlackPercent, targetBlackPercent);
			fadePlane.color = new Color(0f, 0f, 0f, num);
			yield return SequenceUtil.WaitForNextFrame;
		}
		fadePlane.color = new Color(0f, 0f, 0f, targetBlackPercent);
		if (callback != null)
		{
			callback();
		}
		this.activeFadeRoutine = null;
		yield return SequenceUtil.WaitForNextFrame;
		yield break;
	}

	// Token: 0x06001840 RID: 6208 RVA: 0x0007F918 File Offset: 0x0007DB18
	public void EnableFreeCamera(bool enable)
	{
		this.FreeCameraEnabled = enable;
		this.SetInfoText("Screenshot Mode (ESC to exit)");
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x0007F92C File Offset: 0x0007DB2C
	private static bool WithinInputField()
	{
		UnityEngine.EventSystems.EventSystem current = UnityEngine.EventSystems.EventSystem.current;
		if (current == null)
		{
			return false;
		}
		bool flag = false;
		if (current.currentSelectedGameObject != null && (current.currentSelectedGameObject.GetComponent<KInputTextField>() != null || current.currentSelectedGameObject.GetComponent<InputField>() != null))
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06001842 RID: 6210 RVA: 0x0007F984 File Offset: 0x0007DB84
	private bool IsMouseOverGameWindow
	{
		get
		{
			return 0f <= Input.mousePosition.x && 0f <= Input.mousePosition.y && (float)Screen.width >= Input.mousePosition.x && (float)Screen.height >= Input.mousePosition.y;
		}
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x0007F9DC File Offset: 0x0007DBDC
	private void SetInfoText(string text)
	{
		this.infoText.text = text;
		Color color = this.infoText.color;
		color.a = 0.5f;
		this.infoText.color = color;
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x0007FA1C File Offset: 0x0007DC1C
	public void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (this.DisableUserCameraControl)
		{
			return;
		}
		if (CameraController.WithinInputField())
		{
			return;
		}
		if (SaveGame.Instance != null && SaveGame.Instance.GetComponent<UserNavigation>().Handle(e))
		{
			return;
		}
		if (!this.ChangeWorldInput(e))
		{
			if (e.TryConsume(global::Action.TogglePause))
			{
				SpeedControlScreen.Instance.TogglePause(false);
			}
			else if (e.TryConsume(global::Action.ZoomIn) && this.IsMouseOverGameWindow)
			{
				float num = this.targetOrthographicSize * (1f / this.zoomFactor);
				this.targetOrthographicSize = Mathf.Max(num, this.minOrthographicSize);
				this.overrideZoomSpeed = 0f;
				this.isTargetPosSet = false;
			}
			else if (e.TryConsume(global::Action.ZoomOut) && this.IsMouseOverGameWindow)
			{
				float num2 = this.targetOrthographicSize * this.zoomFactor;
				this.targetOrthographicSize = Mathf.Min(num2, this.FreeCameraEnabled ? TuningData<CameraController.Tuning>.Get().maxOrthographicSizeDebug : this.maxOrthographicSize);
				this.overrideZoomSpeed = 0f;
				this.isTargetPosSet = false;
			}
			else if (e.TryConsume(global::Action.MouseMiddle) || e.IsAction(global::Action.MouseRight))
			{
				this.panning = true;
				this.overrideZoomSpeed = 0f;
				this.isTargetPosSet = false;
			}
			else if (this.FreeCameraEnabled && e.TryConsume(global::Action.CinemaCamEnable))
			{
				this.cinemaCamEnabled = !this.cinemaCamEnabled;
				DebugUtil.LogArgs(new object[] { "Cinema Cam Enabled ", this.cinemaCamEnabled });
				this.SetInfoText(this.cinemaCamEnabled ? "Cinema Cam Enabled" : "Cinema Cam Disabled");
			}
			else if (this.FreeCameraEnabled && this.cinemaCamEnabled)
			{
				if (e.TryConsume(global::Action.CinemaToggleLock))
				{
					this.cinemaToggleLock = !this.cinemaToggleLock;
					DebugUtil.LogArgs(new object[] { "Cinema Toggle Lock ", this.cinemaToggleLock });
					this.SetInfoText(this.cinemaToggleLock ? "Cinema Input Lock ON" : "Cinema Input Lock OFF");
				}
				else if (e.TryConsume(global::Action.CinemaToggleEasing))
				{
					this.cinemaToggleEasing = !this.cinemaToggleEasing;
					DebugUtil.LogArgs(new object[] { "Cinema Toggle Easing ", this.cinemaToggleEasing });
					this.SetInfoText(this.cinemaToggleEasing ? "Cinema Easing ON" : "Cinema Easing OFF");
				}
				else if (e.TryConsume(global::Action.CinemaUnpauseOnMove))
				{
					this.cinemaUnpauseNextMove = !this.cinemaUnpauseNextMove;
					DebugUtil.LogArgs(new object[] { "Cinema Unpause Next Move ", this.cinemaUnpauseNextMove });
					this.SetInfoText(this.cinemaUnpauseNextMove ? "Cinema Unpause Next Move ON" : "Cinema Unpause Next Move OFF");
				}
				else if (e.TryConsume(global::Action.CinemaPanLeft))
				{
					this.cinemaPanLeft = !this.cinemaToggleLock || !this.cinemaPanLeft;
					this.cinemaPanRight = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaPanRight))
				{
					this.cinemaPanRight = !this.cinemaToggleLock || !this.cinemaPanRight;
					this.cinemaPanLeft = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaPanUp))
				{
					this.cinemaPanUp = !this.cinemaToggleLock || !this.cinemaPanUp;
					this.cinemaPanDown = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaPanDown))
				{
					this.cinemaPanDown = !this.cinemaToggleLock || !this.cinemaPanDown;
					this.cinemaPanUp = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaZoomIn))
				{
					this.cinemaZoomIn = !this.cinemaToggleLock || !this.cinemaZoomIn;
					this.cinemaZoomOut = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaZoomOut))
				{
					this.cinemaZoomOut = !this.cinemaToggleLock || !this.cinemaZoomOut;
					this.cinemaZoomIn = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaZoomSpeedPlus))
				{
					this.cinemaZoomSpeed++;
					DebugUtil.LogArgs(new object[] { "Cinema Zoom Speed ", this.cinemaZoomSpeed });
					this.SetInfoText("Cinema Zoom Speed: " + this.cinemaZoomSpeed.ToString());
				}
				else if (e.TryConsume(global::Action.CinemaZoomSpeedMinus))
				{
					this.cinemaZoomSpeed--;
					DebugUtil.LogArgs(new object[] { "Cinema Zoom Speed ", this.cinemaZoomSpeed });
					this.SetInfoText("Cinema Zoom Speed: " + this.cinemaZoomSpeed.ToString());
				}
			}
			else if (e.TryConsume(global::Action.PanLeft))
			{
				this.panLeft = true;
			}
			else if (e.TryConsume(global::Action.PanRight))
			{
				this.panRight = true;
			}
			else if (e.TryConsume(global::Action.PanUp))
			{
				this.panUp = true;
			}
			else if (e.TryConsume(global::Action.PanDown))
			{
				this.panDown = true;
			}
		}
		if (!e.Consumed && OverlayMenu.Instance != null)
		{
			OverlayMenu.Instance.OnKeyDown(e);
		}
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x0007FF78 File Offset: 0x0007E178
	public bool ChangeWorldInput(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return true;
		}
		int num = -1;
		if (e.TryConsume(global::Action.SwitchActiveWorld1))
		{
			num = 0;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld2))
		{
			num = 1;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld3))
		{
			num = 2;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld4))
		{
			num = 3;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld5))
		{
			num = 4;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld6))
		{
			num = 5;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld7))
		{
			num = 6;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld8))
		{
			num = 7;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld9))
		{
			num = 8;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld10))
		{
			num = 9;
		}
		if (num != -1)
		{
			List<int> discoveredAsteroidIDsSorted = ClusterManager.Instance.GetDiscoveredAsteroidIDsSorted();
			if (num < discoveredAsteroidIDsSorted.Count && num >= 0)
			{
				num = discoveredAsteroidIDsSorted[num];
				WorldContainer world = ClusterManager.Instance.GetWorld(num);
				if (world != null && world.IsDiscovered && ClusterManager.Instance.activeWorldId != world.id)
				{
					ManagementMenu.Instance.CloseClusterMap();
					this.ActiveWorldStarWipe(world.id, null);
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x000800B0 File Offset: 0x0007E2B0
	public void OnKeyUp(KButtonEvent e)
	{
		if (this.DisableUserCameraControl)
		{
			return;
		}
		if (CameraController.WithinInputField())
		{
			return;
		}
		if (e.TryConsume(global::Action.MouseMiddle) || e.IsAction(global::Action.MouseRight))
		{
			this.panning = false;
			return;
		}
		if (this.FreeCameraEnabled && this.cinemaCamEnabled)
		{
			if (e.TryConsume(global::Action.CinemaPanLeft))
			{
				this.cinemaPanLeft = this.cinemaToggleLock && this.cinemaPanLeft;
				return;
			}
			if (e.TryConsume(global::Action.CinemaPanRight))
			{
				this.cinemaPanRight = this.cinemaToggleLock && this.cinemaPanRight;
				return;
			}
			if (e.TryConsume(global::Action.CinemaPanUp))
			{
				this.cinemaPanUp = this.cinemaToggleLock && this.cinemaPanUp;
				return;
			}
			if (e.TryConsume(global::Action.CinemaPanDown))
			{
				this.cinemaPanDown = this.cinemaToggleLock && this.cinemaPanDown;
				return;
			}
			if (e.TryConsume(global::Action.CinemaZoomIn))
			{
				this.cinemaZoomIn = this.cinemaToggleLock && this.cinemaZoomIn;
				return;
			}
			if (e.TryConsume(global::Action.CinemaZoomOut))
			{
				this.cinemaZoomOut = this.cinemaToggleLock && this.cinemaZoomOut;
				return;
			}
		}
		else
		{
			if (e.TryConsume(global::Action.CameraHome))
			{
				this.CameraGoHome(2f);
				return;
			}
			if (e.TryConsume(global::Action.PanLeft))
			{
				this.panLeft = false;
				return;
			}
			if (e.TryConsume(global::Action.PanRight))
			{
				this.panRight = false;
				return;
			}
			if (e.TryConsume(global::Action.PanUp))
			{
				this.panUp = false;
				return;
			}
			if (e.TryConsume(global::Action.PanDown))
			{
				this.panDown = false;
			}
		}
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x0008024B File Offset: 0x0007E44B
	public void ForcePanningState(bool state)
	{
		this.panning = false;
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x00080254 File Offset: 0x0007E454
	public void CameraGoHome(float speed = 2f)
	{
		GameObject activeTelepad = GameUtil.GetActiveTelepad();
		if (activeTelepad != null && ClusterUtil.ActiveWorldHasPrinter())
		{
			Vector3 vector = new Vector3(activeTelepad.transform.GetPosition().x, activeTelepad.transform.GetPosition().y + 1f, base.transform.GetPosition().z);
			this.SetTargetPos(vector, 10f, true);
			this.SetOverrideZoomSpeed(speed);
		}
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x000802C8 File Offset: 0x0007E4C8
	public void CameraGoTo(Vector3 pos, float speed = 2f, bool playSound = true)
	{
		pos.z = base.transform.GetPosition().z;
		this.SetTargetPos(pos, 10f, playSound);
		this.SetOverrideZoomSpeed(speed);
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x000802F8 File Offset: 0x0007E4F8
	public void SnapTo(Vector3 pos)
	{
		this.ClearFollowTarget();
		pos.z = -100f;
		this.targetPos = Vector3.zero;
		this.isTargetPosSet = false;
		base.transform.SetPosition(pos);
		this.keyPanDelta = Vector3.zero;
		this.OrthographicSize = this.targetOrthographicSize;
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x0008034D File Offset: 0x0007E54D
	public void SnapTo(Vector3 pos, float orthographicSize)
	{
		this.targetOrthographicSize = orthographicSize;
		this.SnapTo(pos);
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x0008035D File Offset: 0x0007E55D
	public void SetOverrideZoomSpeed(float tempZoomSpeed)
	{
		this.overrideZoomSpeed = tempZoomSpeed;
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x00080368 File Offset: 0x0007E568
	public void SetTargetPos(Vector3 pos, float orthographic_size, bool playSound)
	{
		int num = Grid.PosToCell(pos);
		if (!Grid.IsValidCell(num) || Grid.WorldIdx[num] == ClusterManager.INVALID_WORLD_IDX || ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num]) == null)
		{
			return;
		}
		this.ClearFollowTarget();
		if (playSound && !this.isTargetPosSet)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Click_Notification", false));
		}
		pos.z = -100f;
		if ((int)Grid.WorldIdx[num] != ClusterManager.Instance.activeWorldId)
		{
			this.targetOrthographicSize = 20f;
			this.ActiveWorldStarWipe((int)Grid.WorldIdx[num], pos, 10f, delegate
			{
				this.targetPos = pos;
				this.isTargetPosSet = true;
				this.OrthographicSize = orthographic_size + 5f;
				this.targetOrthographicSize = orthographic_size;
			});
		}
		else
		{
			this.targetPos = pos;
			this.isTargetPosSet = true;
			this.targetOrthographicSize = orthographic_size;
		}
		PlayerController.Instance.CancelDragging();
		this.CheckMoveUnpause();
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x00080470 File Offset: 0x0007E670
	public void SetTargetPosForWorldChange(Vector3 pos, float orthographic_size, bool playSound)
	{
		int num = Grid.PosToCell(pos);
		if (!Grid.IsValidCell(num) || Grid.WorldIdx[num] == ClusterManager.INVALID_WORLD_IDX || ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num]) == null)
		{
			return;
		}
		this.ClearFollowTarget();
		if (playSound && !this.isTargetPosSet)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Click_Notification", false));
		}
		pos.z = -100f;
		this.targetPos = pos;
		this.isTargetPosSet = true;
		this.targetOrthographicSize = orthographic_size;
		PlayerController.Instance.CancelDragging();
		this.CheckMoveUnpause();
		this.SetPosition(pos);
		this.OrthographicSize = orthographic_size;
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x00080514 File Offset: 0x0007E714
	public void SetMaxOrthographicSize(float size)
	{
		this.maxOrthographicSize = size;
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x0008051D File Offset: 0x0007E71D
	public void SetPosition(Vector3 pos)
	{
		base.transform.SetPosition(pos);
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x0008052C File Offset: 0x0007E72C
	public IEnumerator DoCinematicZoom(float targetOrthographicSize)
	{
		this.cinemaCamEnabled = true;
		this.FreeCameraEnabled = true;
		this.targetOrthographicSize = targetOrthographicSize;
		while (targetOrthographicSize - this.OrthographicSize >= 0.001f)
		{
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.OrthographicSize = targetOrthographicSize;
		this.FreeCameraEnabled = false;
		this.cinemaCamEnabled = false;
		yield break;
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x00080544 File Offset: 0x0007E744
	private Vector3 PointUnderCursor(Vector3 mousePos, Camera cam)
	{
		Ray ray = cam.ScreenPointToRay(mousePos);
		Vector3 direction = ray.direction;
		Vector3 vector = direction * Mathf.Abs(cam.transform.GetPosition().z / direction.z);
		return ray.origin + vector;
	}

	// Token: 0x06001853 RID: 6227 RVA: 0x00080594 File Offset: 0x0007E794
	private void CinemaCamUpdate()
	{
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		Camera main = Camera.main;
		Vector3 localPosition = base.transform.GetLocalPosition();
		float num = Mathf.Pow((float)this.cinemaZoomSpeed, 3f);
		if (this.cinemaZoomIn)
		{
			this.overrideZoomSpeed = -num / TuningData<CameraController.Tuning>.Get().cinemaZoomFactor;
			this.isTargetPosSet = false;
		}
		else if (this.cinemaZoomOut)
		{
			this.overrideZoomSpeed = num / TuningData<CameraController.Tuning>.Get().cinemaZoomFactor;
			this.isTargetPosSet = false;
		}
		else
		{
			this.overrideZoomSpeed = 0f;
		}
		if (this.cinemaToggleEasing)
		{
			this.cinemaZoomVelocity += (this.overrideZoomSpeed - this.cinemaZoomVelocity) * this.cinemaEasing;
		}
		else
		{
			this.cinemaZoomVelocity = this.overrideZoomSpeed;
		}
		if (this.cinemaZoomVelocity != 0f)
		{
			this.OrthographicSize = main.orthographicSize + this.cinemaZoomVelocity * unscaledDeltaTime * (main.orthographicSize / 20f);
			this.targetOrthographicSize = main.orthographicSize;
		}
		float num2 = num / TuningData<CameraController.Tuning>.Get().cinemaZoomToFactor;
		float num3 = this.keyPanningSpeed / 20f * main.orthographicSize;
		float num4 = num3 * (num / TuningData<CameraController.Tuning>.Get().cinemaPanToFactor);
		if (!this.isTargetPosSet && this.targetOrthographicSize != main.orthographicSize)
		{
			float num5 = Mathf.Min(num2 * unscaledDeltaTime, 0.1f);
			this.OrthographicSize = Mathf.Lerp(main.orthographicSize, this.targetOrthographicSize, num5);
		}
		Vector3 vector = Vector3.zero;
		if (this.isTargetPosSet)
		{
			float num6 = this.cinemaEasing * TuningData<CameraController.Tuning>.Get().targetZoomEasingFactor;
			float num7 = this.cinemaEasing * TuningData<CameraController.Tuning>.Get().targetPanEasingFactor;
			float num8 = this.targetOrthographicSize - main.orthographicSize;
			Vector3 vector2 = this.targetPos - localPosition;
			float num9;
			float num10;
			if (!this.cinemaToggleEasing)
			{
				num9 = num2 * unscaledDeltaTime;
				num10 = num4 * unscaledDeltaTime;
			}
			else
			{
				DebugUtil.LogArgs(new object[]
				{
					"Min zoom of:",
					num2 * unscaledDeltaTime,
					Mathf.Abs(num8) * num6 * unscaledDeltaTime
				});
				num9 = Mathf.Min(num2 * unscaledDeltaTime, Mathf.Abs(num8) * num6 * unscaledDeltaTime);
				DebugUtil.LogArgs(new object[]
				{
					"Min pan of:",
					num4 * unscaledDeltaTime,
					vector2.magnitude * num7 * unscaledDeltaTime
				});
				num10 = Mathf.Min(num4 * unscaledDeltaTime, vector2.magnitude * num7 * unscaledDeltaTime);
			}
			float num11;
			if (Mathf.Abs(num8) < num9)
			{
				num11 = num8;
			}
			else
			{
				num11 = Mathf.Sign(num8) * num9;
			}
			if (vector2.magnitude < num10)
			{
				vector = vector2;
			}
			else
			{
				vector = vector2.normalized * num10;
			}
			if (Mathf.Abs(num11) < 0.001f && vector.magnitude < 0.001f)
			{
				this.isTargetPosSet = false;
				num11 = num8;
				vector = vector2;
			}
			this.OrthographicSize = main.orthographicSize + num11 * (main.orthographicSize / 20f);
		}
		if (!PlayerController.Instance.CanDrag())
		{
			this.panning = false;
		}
		Vector3 vector3 = Vector3.zero;
		if (this.panning)
		{
			vector3 = -PlayerController.Instance.GetWorldDragDelta();
			this.isTargetPosSet = false;
			if (vector3.magnitude > 0f)
			{
				this.ClearFollowTarget();
			}
			this.keyPanDelta = Vector3.zero;
		}
		else
		{
			float num12 = num / TuningData<CameraController.Tuning>.Get().cinemaPanFactor;
			Vector3 zero = Vector3.zero;
			if (this.cinemaPanLeft)
			{
				this.ClearFollowTarget();
				zero.x = -num3 * num12;
				this.isTargetPosSet = false;
			}
			if (this.cinemaPanRight)
			{
				this.ClearFollowTarget();
				zero.x = num3 * num12;
				this.isTargetPosSet = false;
			}
			if (this.cinemaPanUp)
			{
				this.ClearFollowTarget();
				zero.y = num3 * num12;
				this.isTargetPosSet = false;
			}
			if (this.cinemaPanDown)
			{
				this.ClearFollowTarget();
				zero.y = -num3 * num12;
				this.isTargetPosSet = false;
			}
			if (this.cinemaToggleEasing)
			{
				this.keyPanDelta += (zero - this.keyPanDelta) * this.cinemaEasing;
			}
			else
			{
				this.keyPanDelta = zero;
			}
		}
		Vector3 vector4 = localPosition + vector + vector3 + this.keyPanDelta * unscaledDeltaTime;
		if (this.followTarget != null)
		{
			vector4.x = this.followTargetPos.x;
			vector4.y = this.followTargetPos.y;
		}
		vector4.z = -100f;
		if ((double)(vector4 - base.transform.GetLocalPosition()).magnitude > 0.001)
		{
			base.transform.SetLocalPosition(vector4);
		}
	}

	// Token: 0x06001854 RID: 6228 RVA: 0x00080A5C File Offset: 0x0007EC5C
	private void NormalCamUpdate()
	{
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		Camera main = Camera.main;
		this.smoothDt = this.smoothDt * 2f / 3f + unscaledDeltaTime / 3f;
		float num = ((this.overrideZoomSpeed != 0f) ? this.overrideZoomSpeed : this.zoomSpeed);
		Vector3 localPosition = base.transform.GetLocalPosition();
		Vector3 vector = ((this.overrideZoomSpeed != 0f) ? new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0f) : KInputManager.GetMousePos());
		Vector3 vector2 = this.PointUnderCursor(vector, main);
		Vector3 vector3 = main.ScreenToViewportPoint(vector);
		float num2 = this.keyPanningSpeed / 20f * main.orthographicSize;
		num2 *= Mathf.Min(unscaledDeltaTime / 0.016666666f, 10f);
		float num3 = num * Mathf.Min(this.smoothDt, 0.3f);
		this.OrthographicSize = Mathf.Lerp(main.orthographicSize, this.targetOrthographicSize, num3);
		base.transform.SetLocalPosition(localPosition);
		Vector3 vector4 = main.WorldToViewportPoint(vector2);
		vector3.z = vector4.z;
		Vector3 vector5 = main.ViewportToWorldPoint(vector4) - main.ViewportToWorldPoint(vector3);
		if (this.isTargetPosSet)
		{
			vector5 = Vector3.Lerp(localPosition, this.targetPos, num * this.smoothDt) - localPosition;
			if (vector5.magnitude < 0.001f)
			{
				this.isTargetPosSet = false;
				vector5 = this.targetPos - localPosition;
			}
		}
		if (!PlayerController.Instance.CanDrag())
		{
			this.panning = false;
		}
		Vector3 vector6 = Vector3.zero;
		if (this.panning)
		{
			vector6 = -PlayerController.Instance.GetWorldDragDelta();
			this.isTargetPosSet = false;
		}
		Vector3 vector7 = localPosition + vector5 + vector6;
		if (this.panning)
		{
			if (vector6.magnitude > 0f)
			{
				this.ClearFollowTarget();
			}
			this.keyPanDelta = Vector3.zero;
		}
		else if (!this.DisableUserCameraControl)
		{
			if (this.panLeft)
			{
				this.ClearFollowTarget();
				this.keyPanDelta.x = this.keyPanDelta.x - num2;
				this.isTargetPosSet = false;
				this.overrideZoomSpeed = 0f;
			}
			if (this.panRight)
			{
				this.ClearFollowTarget();
				this.keyPanDelta.x = this.keyPanDelta.x + num2;
				this.isTargetPosSet = false;
				this.overrideZoomSpeed = 0f;
			}
			if (this.panUp)
			{
				this.ClearFollowTarget();
				this.keyPanDelta.y = this.keyPanDelta.y + num2;
				this.isTargetPosSet = false;
				this.overrideZoomSpeed = 0f;
			}
			if (this.panDown)
			{
				this.ClearFollowTarget();
				this.keyPanDelta.y = this.keyPanDelta.y - num2;
				this.isTargetPosSet = false;
				this.overrideZoomSpeed = 0f;
			}
			if (KInputManager.currentControllerIsGamepad)
			{
				Vector2 vector8 = num2 * KInputManager.steamInputInterpreter.GetSteamCameraMovement();
				if (Mathf.Abs(vector8.x) > Mathf.Epsilon || Mathf.Abs(vector8.y) > Mathf.Epsilon)
				{
					this.ClearFollowTarget();
					this.isTargetPosSet = false;
					this.overrideZoomSpeed = 0f;
				}
				this.keyPanDelta += new Vector3(vector8.x, vector8.y, 0f);
			}
			Vector3 vector9 = new Vector3(Mathf.Lerp(0f, this.keyPanDelta.x, this.smoothDt * this.keyPanningEasing), Mathf.Lerp(0f, this.keyPanDelta.y, this.smoothDt * this.keyPanningEasing), 0f);
			this.keyPanDelta -= vector9;
			vector7.x += vector9.x;
			vector7.y += vector9.y;
		}
		if (this.followTarget != null)
		{
			vector7.x = this.followTargetPos.x;
			vector7.y = this.followTargetPos.y;
		}
		vector7.z = -100f;
		if ((double)(vector7 - base.transform.GetLocalPosition()).magnitude > 0.001)
		{
			base.transform.SetLocalPosition(vector7);
		}
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x00080EA8 File Offset: 0x0007F0A8
	private void Update()
	{
		if (Game.Instance == null || !Game.Instance.timelapser.CapturingTimelapseScreenshot)
		{
			if (this.FreeCameraEnabled && this.cinemaCamEnabled)
			{
				this.CinemaCamUpdate();
			}
			else
			{
				this.NormalCamUpdate();
			}
		}
		if (this.infoText != null && this.infoText.color.a > 0f)
		{
			Color color = this.infoText.color;
			color.a = Mathf.Max(0f, this.infoText.color.a - Time.unscaledDeltaTime * 0.5f);
			this.infoText.color = color;
		}
		this.ConstrainToWorld();
		Vector3 vector = this.PointUnderCursor(KInputManager.GetMousePos(), Camera.main);
		Shader.SetGlobalVector("_WorldCameraPos", new Vector4(base.transform.GetPosition().x, base.transform.GetPosition().y, base.transform.GetPosition().z, Camera.main.orthographicSize));
		Shader.SetGlobalVector("_WorldCursorPos", new Vector4(vector.x, vector.y, 0f, 0f));
		this.VisibleArea.Update();
		this.soundCuller = SoundCuller.CreateCuller();
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x00080FF8 File Offset: 0x0007F1F8
	private Vector3 GetFollowPos()
	{
		if (this.followTarget != null)
		{
			Vector3 vector = this.followTarget.transform.GetPosition();
			KAnimControllerBase component = this.followTarget.GetComponent<KAnimControllerBase>();
			if (component != null)
			{
				vector = component.GetWorldPivot();
			}
			return vector;
		}
		return Vector3.zero;
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x00081048 File Offset: 0x0007F248
	private void ConstrainToWorld()
	{
		if (Game.Instance != null && Game.Instance.IsLoading())
		{
			return;
		}
		if (this.FreeCameraEnabled)
		{
			return;
		}
		Camera main = Camera.main;
		float num = 0.33f;
		Ray ray = main.ViewportPointToRay(Vector3.zero + Vector3.one * num);
		Ray ray2 = main.ViewportPointToRay(Vector3.one - Vector3.one * num);
		float num2 = Mathf.Abs(ray.origin.z / ray.direction.z);
		float num3 = Mathf.Abs(ray2.origin.z / ray2.direction.z);
		Vector3 point = ray.GetPoint(num2);
		Vector3 point2 = ray2.GetPoint(num3);
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = new Vector2(Grid.WidthInMeters, Grid.HeightInMeters);
		Vector2 vector3 = vector2;
		if (ClusterManager.Instance != null)
		{
			WorldContainer activeWorld = ClusterManager.Instance.activeWorld;
			vector = activeWorld.minimumBounds * Grid.CellSizeInMeters;
			vector2 = activeWorld.maximumBounds * Grid.CellSizeInMeters;
			vector3 = new Vector2((float)activeWorld.Width, (float)activeWorld.Height) * Grid.CellSizeInMeters;
		}
		if (point2.x - point.x > vector3.x || point2.y - point.y > vector3.y)
		{
			return;
		}
		Vector3 vector4 = base.transform.GetPosition() - ray.origin;
		Vector3 vector5 = point;
		vector5.x = Mathf.Max(vector.x, vector5.x);
		vector5.y = Mathf.Max(vector.y * Grid.CellSizeInMeters, vector5.y);
		ray.origin = vector5;
		ray.direction = -ray.direction;
		vector5 = ray.GetPoint(num2);
		base.transform.SetPosition(vector5 + vector4);
		vector4 = base.transform.GetPosition() - ray2.origin;
		vector5 = point2;
		vector5.x = Mathf.Min(vector2.x, vector5.x);
		vector5.y = Mathf.Min(vector2.y * this.MAX_Y_SCALE, vector5.y);
		ray2.origin = vector5;
		ray2.direction = -ray2.direction;
		vector5 = ray2.GetPoint(num3);
		Vector3 vector6 = vector5 + vector4;
		vector6.z = -100f;
		base.transform.SetPosition(vector6);
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x000812E8 File Offset: 0x0007F4E8
	public void Save(BinaryWriter writer)
	{
		writer.Write(base.transform.GetPosition());
		writer.Write(base.transform.localScale);
		writer.Write(base.transform.rotation);
		writer.Write(this.targetOrthographicSize);
		CameraSaveData.position = base.transform.GetPosition();
		CameraSaveData.localScale = base.transform.localScale;
		CameraSaveData.rotation = base.transform.rotation;
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x00081364 File Offset: 0x0007F564
	private void Restore()
	{
		if (CameraSaveData.valid)
		{
			int num = Grid.PosToCell(CameraSaveData.position);
			if (Grid.IsValidCell(num) && !Grid.IsVisible(num))
			{
				global::Debug.LogWarning("Resetting Camera Position... camera was saved in an undiscovered area of the map.");
				this.CameraGoHome(2f);
				return;
			}
			base.transform.SetPosition(CameraSaveData.position);
			base.transform.localScale = CameraSaveData.localScale;
			base.transform.rotation = CameraSaveData.rotation;
			this.targetOrthographicSize = Mathf.Clamp(CameraSaveData.orthographicsSize, this.minOrthographicSize, this.FreeCameraEnabled ? TuningData<CameraController.Tuning>.Get().maxOrthographicSizeDebug : this.maxOrthographicSize);
			this.SnapTo(base.transform.GetPosition());
		}
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x0008141E File Offset: 0x0007F61E
	private void OnMRTSetupComplete(Camera cam)
	{
		this.cameras.Add(cam);
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x0008142C File Offset: 0x0007F62C
	public bool IsAudibleSound(Vector2 pos)
	{
		return this.soundCuller.IsAudible(pos);
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x0008143C File Offset: 0x0007F63C
	public bool IsAudibleSound(Vector3 pos, EventReference event_ref)
	{
		string eventReferencePath = KFMOD.GetEventReferencePath(event_ref);
		return this.soundCuller.IsAudible(pos, eventReferencePath);
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x00081467 File Offset: 0x0007F667
	public bool IsAudibleSound(Vector3 pos, HashedString sound_path)
	{
		return this.soundCuller.IsAudible(pos, sound_path);
	}

	// Token: 0x0600185E RID: 6238 RVA: 0x0008147B File Offset: 0x0007F67B
	public Vector3 GetVerticallyScaledPosition(Vector3 pos, bool objectIsSelectedAndVisible = false)
	{
		return this.soundCuller.GetVerticallyScaledPosition(pos, objectIsSelectedAndVisible);
	}

	// Token: 0x0600185F RID: 6239 RVA: 0x0008148C File Offset: 0x0007F68C
	public bool IsVisiblePos(Vector3 pos)
	{
		GridArea visibleArea = GridVisibleArea.GetVisibleArea();
		return visibleArea.Min <= pos && pos <= visibleArea.Max;
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x000814C7 File Offset: 0x0007F6C7
	protected override void OnCleanUp()
	{
		CameraController.Instance = null;
	}

	// Token: 0x06001861 RID: 6241 RVA: 0x000814D0 File Offset: 0x0007F6D0
	public void SetFollowTarget(Transform follow_target)
	{
		this.ClearFollowTarget();
		if (follow_target == null)
		{
			return;
		}
		this.followTarget = follow_target;
		this.OrthographicSize = 6f;
		this.targetOrthographicSize = 6f;
		Vector3 followPos = this.GetFollowPos();
		this.followTargetPos = new Vector3(followPos.x, followPos.y, base.transform.GetPosition().z);
		base.transform.SetPosition(this.followTargetPos);
		this.followTarget.GetComponent<KMonoBehaviour>().Trigger(-1506069671, null);
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x00081560 File Offset: 0x0007F760
	public void ClearFollowTarget()
	{
		if (this.followTarget == null)
		{
			return;
		}
		this.followTarget.GetComponent<KMonoBehaviour>().Trigger(-485480405, null);
		this.followTarget = null;
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x00081590 File Offset: 0x0007F790
	public void UpdateFollowTarget()
	{
		if (this.followTarget != null)
		{
			Vector3 followPos = this.GetFollowPos();
			Vector2 vector = new Vector2(base.transform.GetLocalPosition().x, base.transform.GetLocalPosition().y);
			byte b = Grid.WorldIdx[Grid.PosToCell(followPos)];
			if (ClusterManager.Instance.activeWorldId != (int)b)
			{
				Transform transform = this.followTarget;
				this.SetFollowTarget(null);
				ClusterManager.Instance.SetActiveWorld((int)b);
				this.SetFollowTarget(transform);
				return;
			}
			Vector2 vector2 = Vector2.Lerp(vector, followPos, Time.unscaledDeltaTime * 25f);
			this.followTargetPos = new Vector3(vector2.x, vector2.y, base.transform.GetLocalPosition().z);
		}
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x0008165C File Offset: 0x0007F85C
	public void RenderForTimelapser(ref RenderTexture tex)
	{
		this.RenderCameraForTimelapse(this.baseCamera, ref tex, this.timelapseCameraCullingMask, -1f);
		CameraClearFlags clearFlags = this.overlayCamera.clearFlags;
		this.overlayCamera.clearFlags = CameraClearFlags.Nothing;
		this.RenderCameraForTimelapse(this.overlayCamera, ref tex, this.timelapseOverlayCameraCullingMask, -1f);
		this.overlayCamera.clearFlags = clearFlags;
	}

	// Token: 0x06001865 RID: 6245 RVA: 0x000816C0 File Offset: 0x0007F8C0
	private void RenderCameraForTimelapse(Camera cam, ref RenderTexture tex, LayerMask mask, float overrideAspect = -1f)
	{
		int cullingMask = cam.cullingMask;
		RenderTexture targetTexture = cam.targetTexture;
		cam.targetTexture = tex;
		cam.aspect = (float)tex.width / (float)tex.height;
		if (overrideAspect != -1f)
		{
			cam.aspect = overrideAspect;
		}
		if (mask != -1)
		{
			cam.cullingMask = mask;
		}
		cam.Render();
		cam.ResetAspect();
		cam.cullingMask = cullingMask;
		cam.targetTexture = targetTexture;
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x0008173A File Offset: 0x0007F93A
	private void CheckMoveUnpause()
	{
		if (this.cinemaCamEnabled && this.cinemaUnpauseNextMove)
		{
			this.cinemaUnpauseNextMove = !this.cinemaUnpauseNextMove;
			if (SpeedControlScreen.Instance.IsPaused)
			{
				SpeedControlScreen.Instance.Unpause(false);
			}
		}
	}

	// Token: 0x04000D67 RID: 3431
	public const float DEFAULT_MAX_ORTHO_SIZE = 20f;

	// Token: 0x04000D68 RID: 3432
	public float MAX_Y_SCALE = 1.1f;

	// Token: 0x04000D69 RID: 3433
	public LocText infoText;

	// Token: 0x04000D6A RID: 3434
	private const float FIXED_Z = -100f;

	// Token: 0x04000D6C RID: 3436
	public bool FreeCameraEnabled;

	// Token: 0x04000D6D RID: 3437
	public float zoomSpeed;

	// Token: 0x04000D6E RID: 3438
	public float minOrthographicSize;

	// Token: 0x04000D6F RID: 3439
	public float zoomFactor;

	// Token: 0x04000D70 RID: 3440
	public float keyPanningSpeed;

	// Token: 0x04000D71 RID: 3441
	public float keyPanningEasing;

	// Token: 0x04000D72 RID: 3442
	public Texture2D dayColourCube;

	// Token: 0x04000D73 RID: 3443
	public Texture2D nightColourCube;

	// Token: 0x04000D74 RID: 3444
	public Material LightBufferMaterial;

	// Token: 0x04000D75 RID: 3445
	public Material LightCircleOverlay;

	// Token: 0x04000D76 RID: 3446
	public Material LightConeOverlay;

	// Token: 0x04000D77 RID: 3447
	public Transform followTarget;

	// Token: 0x04000D78 RID: 3448
	public Vector3 followTargetPos;

	// Token: 0x04000D79 RID: 3449
	public GridVisibleArea VisibleArea = new GridVisibleArea();

	// Token: 0x04000D7B RID: 3451
	private float maxOrthographicSize = 20f;

	// Token: 0x04000D7C RID: 3452
	private float overrideZoomSpeed;

	// Token: 0x04000D7D RID: 3453
	private bool panning;

	// Token: 0x04000D7E RID: 3454
	private Vector3 keyPanDelta;

	// Token: 0x04000D81 RID: 3457
	[SerializeField]
	private LayerMask timelapseCameraCullingMask;

	// Token: 0x04000D82 RID: 3458
	[SerializeField]
	private LayerMask timelapseOverlayCameraCullingMask;

	// Token: 0x04000D84 RID: 3460
	private bool userCameraControlDisabled;

	// Token: 0x04000D85 RID: 3461
	private bool panLeft;

	// Token: 0x04000D86 RID: 3462
	private bool panRight;

	// Token: 0x04000D87 RID: 3463
	private bool panUp;

	// Token: 0x04000D88 RID: 3464
	private bool panDown;

	// Token: 0x04000D8A RID: 3466
	[NonSerialized]
	public Camera baseCamera;

	// Token: 0x04000D8B RID: 3467
	[NonSerialized]
	public Camera overlayCamera;

	// Token: 0x04000D8C RID: 3468
	[NonSerialized]
	public Camera overlayNoDepthCamera;

	// Token: 0x04000D8D RID: 3469
	[NonSerialized]
	public Camera uiCamera;

	// Token: 0x04000D8E RID: 3470
	[NonSerialized]
	public Camera lightBufferCamera;

	// Token: 0x04000D8F RID: 3471
	[NonSerialized]
	public Camera simOverlayCamera;

	// Token: 0x04000D90 RID: 3472
	[NonSerialized]
	public Camera infraredCamera;

	// Token: 0x04000D91 RID: 3473
	[NonSerialized]
	public Camera timelapseFreezeCamera;

	// Token: 0x04000D92 RID: 3474
	[SerializeField]
	private List<GameScreenManager.UIRenderTarget> uiCameraTargets;

	// Token: 0x04000D93 RID: 3475
	public List<Camera> cameras = new List<Camera>();

	// Token: 0x04000D94 RID: 3476
	private MultipleRenderTarget mrt;

	// Token: 0x04000D95 RID: 3477
	public SoundCuller soundCuller;

	// Token: 0x04000D96 RID: 3478
	private bool cinemaCamEnabled;

	// Token: 0x04000D97 RID: 3479
	private bool cinemaToggleLock;

	// Token: 0x04000D98 RID: 3480
	private bool cinemaToggleEasing;

	// Token: 0x04000D99 RID: 3481
	private bool cinemaUnpauseNextMove;

	// Token: 0x04000D9A RID: 3482
	private bool cinemaPanLeft;

	// Token: 0x04000D9B RID: 3483
	private bool cinemaPanRight;

	// Token: 0x04000D9C RID: 3484
	private bool cinemaPanUp;

	// Token: 0x04000D9D RID: 3485
	private bool cinemaPanDown;

	// Token: 0x04000D9E RID: 3486
	private bool cinemaZoomIn;

	// Token: 0x04000D9F RID: 3487
	private bool cinemaZoomOut;

	// Token: 0x04000DA0 RID: 3488
	private int cinemaZoomSpeed = 10;

	// Token: 0x04000DA1 RID: 3489
	private float cinemaEasing = 0.05f;

	// Token: 0x04000DA2 RID: 3490
	private float cinemaZoomVelocity;

	// Token: 0x04000DA4 RID: 3492
	private float smoothDt;

	// Token: 0x02001070 RID: 4208
	public class Tuning : TuningData<CameraController.Tuning>
	{
		// Token: 0x04005799 RID: 22425
		public float maxOrthographicSizeDebug;

		// Token: 0x0400579A RID: 22426
		public float cinemaZoomFactor = 100f;

		// Token: 0x0400579B RID: 22427
		public float cinemaPanFactor = 50f;

		// Token: 0x0400579C RID: 22428
		public float cinemaZoomToFactor = 100f;

		// Token: 0x0400579D RID: 22429
		public float cinemaPanToFactor = 50f;

		// Token: 0x0400579E RID: 22430
		public float targetZoomEasingFactor = 400f;

		// Token: 0x0400579F RID: 22431
		public float targetPanEasingFactor = 100f;
	}
}
