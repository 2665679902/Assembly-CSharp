using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000AA5 RID: 2725
internal class GraphicsOptionsScreen : KModalScreen
{
	// Token: 0x0600537F RID: 21375 RVA: 0x001E48B0 File Offset: 0x001E2AB0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.title.SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.TITLE);
		this.originalSettings = this.CaptureSettings();
		this.applyButton.isInteractable = false;
		this.applyButton.onClick += this.OnApply;
		this.applyButton.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.APPLYBUTTON);
		this.doneButton.onClick += this.OnDone;
		this.closeButton.onClick += this.OnDone;
		this.doneButton.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.DONE_BUTTON);
		bool flag = QualitySettings.GetQualityLevel() == 1;
		this.lowResToggle.ChangeState(flag ? 1 : 0);
		MultiToggle multiToggle = this.lowResToggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(this.OnLowResToggle));
		this.lowResToggle.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.LOWRES);
		this.resolutionDropdown.ClearOptions();
		this.BuildOptions();
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (System.Action)Delegate.Combine(instance.OnResize, new System.Action(delegate
		{
			this.BuildOptions();
			this.resolutionDropdown.options = this.options;
		}));
		this.resolutionDropdown.options = this.options;
		this.resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnResolutionChanged));
		this.fullscreenToggle.ChangeState(Screen.fullScreen ? 1 : 0);
		MultiToggle multiToggle2 = this.fullscreenToggle;
		multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(this.OnFullscreenToggle));
		this.fullscreenToggle.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.FULLSCREEN);
		this.resolutionDropdown.transform.parent.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.RESOLUTION);
		if (this.fullscreenToggle.CurrentState == 1)
		{
			int resolutionIndex = this.GetResolutionIndex(this.originalSettings.resolution);
			if (resolutionIndex != -1)
			{
				this.resolutionDropdown.value = resolutionIndex;
			}
		}
		this.CanvasScalers = UnityEngine.Object.FindObjectsOfType<KCanvasScaler>(true);
		this.UpdateSliderLabel();
		this.uiScaleSlider.onValueChanged.AddListener(delegate(float data)
		{
			this.sliderLabel.text = this.uiScaleSlider.value.ToString() + "%";
		});
		this.uiScaleSlider.onReleaseHandle += delegate
		{
			this.UpdateUIScale(this.uiScaleSlider.value);
		};
		this.BuildColorModeOptions();
		this.colorModeDropdown.options = this.colorModeOptions;
		this.colorModeDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnColorModeChanged));
		int num = 0;
		if (KPlayerPrefs.HasKey(GraphicsOptionsScreen.ColorModeKey))
		{
			num = KPlayerPrefs.GetInt(GraphicsOptionsScreen.ColorModeKey);
		}
		this.colorModeDropdown.value = num;
		this.RefreshColorExamples(this.originalSettings.colorSetId);
	}

	// Token: 0x06005380 RID: 21376 RVA: 0x001E4B82 File Offset: 0x001E2D82
	public static void SetSettingsFromPrefs()
	{
		GraphicsOptionsScreen.SetResolutionFromPrefs();
		GraphicsOptionsScreen.SetLowResFromPrefs();
	}

	// Token: 0x06005381 RID: 21377 RVA: 0x001E4B90 File Offset: 0x001E2D90
	public static void SetLowResFromPrefs()
	{
		int num = 0;
		if (KPlayerPrefs.HasKey(GraphicsOptionsScreen.LowResKey))
		{
			num = KPlayerPrefs.GetInt(GraphicsOptionsScreen.LowResKey);
			QualitySettings.SetQualityLevel(num, true);
		}
		else
		{
			QualitySettings.SetQualityLevel(num, true);
		}
		DebugUtil.LogArgs(new object[] { string.Format("Low Res Textures? {0}", (num == 1) ? "Yes" : "No") });
	}

	// Token: 0x06005382 RID: 21378 RVA: 0x001E4BF0 File Offset: 0x001E2DF0
	public static void SetResolutionFromPrefs()
	{
		int num = Screen.currentResolution.width;
		int num2 = Screen.currentResolution.height;
		int num3 = Screen.currentResolution.refreshRate;
		bool flag = Screen.fullScreen;
		if (KPlayerPrefs.HasKey(GraphicsOptionsScreen.ResolutionWidthKey) && KPlayerPrefs.HasKey(GraphicsOptionsScreen.ResolutionHeightKey))
		{
			int @int = KPlayerPrefs.GetInt(GraphicsOptionsScreen.ResolutionWidthKey);
			int int2 = KPlayerPrefs.GetInt(GraphicsOptionsScreen.ResolutionHeightKey);
			int int3 = KPlayerPrefs.GetInt(GraphicsOptionsScreen.RefreshRateKey, Screen.currentResolution.refreshRate);
			bool flag2 = KPlayerPrefs.GetInt(GraphicsOptionsScreen.FullScreenKey, Screen.fullScreen ? 1 : 0) == 1;
			if (int2 <= 1 || @int <= 1)
			{
				DebugUtil.LogArgs(new object[] { "Saved resolution was invalid, ignoring..." });
			}
			else
			{
				num = @int;
				num2 = int2;
				num3 = int3;
				flag = flag2;
			}
		}
		if (num <= 1 || num2 <= 1)
		{
			DebugUtil.LogWarningArgs(new object[] { "Detected a degenerate resolution, attempting to fix..." });
			foreach (Resolution resolution in Screen.resolutions)
			{
				if (resolution.width == 1920)
				{
					num = resolution.width;
					num2 = resolution.height;
					num3 = 0;
				}
			}
			if (num <= 1 || num2 <= 1)
			{
				foreach (Resolution resolution2 in Screen.resolutions)
				{
					if (resolution2.width == 1280)
					{
						num = resolution2.width;
						num2 = resolution2.height;
						num3 = 0;
					}
				}
			}
			if (num <= 1 || num2 <= 1)
			{
				foreach (Resolution resolution3 in Screen.resolutions)
				{
					if (resolution3.width > 1 && resolution3.height > 1 && resolution3.refreshRate > 0)
					{
						num = resolution3.width;
						num2 = resolution3.height;
						num3 = 0;
					}
				}
			}
			if (num <= 1 || num2 <= 1)
			{
				string text = "Could not find a suitable resolution for this screen! Reported available resolutions are:";
				foreach (Resolution resolution4 in Screen.resolutions)
				{
					text += string.Format("\n{0}x{1} @ {2}hz", resolution4.width, resolution4.height, resolution4.refreshRate);
				}
				global::Debug.LogError(text);
				num = 1280;
				num2 = 720;
				flag = false;
				num3 = 0;
			}
		}
		DebugUtil.LogArgs(new object[] { string.Format("Applying resolution {0}x{1} @{2}hz (fullscreen: {3})", new object[] { num, num2, num3, flag }) });
		Screen.SetResolution(num, num2, flag, num3);
	}

	// Token: 0x06005383 RID: 21379 RVA: 0x001E4EA0 File Offset: 0x001E30A0
	public static void SetColorModeFromPrefs()
	{
		int num = 0;
		if (KPlayerPrefs.HasKey(GraphicsOptionsScreen.ColorModeKey))
		{
			num = KPlayerPrefs.GetInt(GraphicsOptionsScreen.ColorModeKey);
		}
		GlobalAssets.Instance.colorSet = GlobalAssets.Instance.colorSetOptions[num];
	}

	// Token: 0x06005384 RID: 21380 RVA: 0x001E4EDC File Offset: 0x001E30DC
	public static void OnResize()
	{
		GraphicsOptionsScreen.Settings settings = default(GraphicsOptionsScreen.Settings);
		settings.resolution = Screen.currentResolution;
		settings.resolution.width = Screen.width;
		settings.resolution.height = Screen.height;
		settings.fullscreen = Screen.fullScreen;
		settings.lowRes = QualitySettings.GetQualityLevel();
		settings.colorSetId = Array.IndexOf<ColorSet>(GlobalAssets.Instance.colorSetOptions, GlobalAssets.Instance.colorSet);
		GraphicsOptionsScreen.SaveSettingsToPrefs(settings);
	}

	// Token: 0x06005385 RID: 21381 RVA: 0x001E4F60 File Offset: 0x001E3160
	private static void SaveSettingsToPrefs(GraphicsOptionsScreen.Settings settings)
	{
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.LowResKey, settings.lowRes);
		global::Debug.LogFormat("Screen resolution updated, saving values to prefs: {0}x{1} @ {2}, fullscreen: {3}", new object[]
		{
			settings.resolution.width,
			settings.resolution.height,
			settings.resolution.refreshRate,
			settings.fullscreen
		});
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.ResolutionWidthKey, settings.resolution.width);
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.ResolutionHeightKey, settings.resolution.height);
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.RefreshRateKey, settings.resolution.refreshRate);
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.FullScreenKey, settings.fullscreen ? 1 : 0);
		KPlayerPrefs.SetInt(GraphicsOptionsScreen.ColorModeKey, settings.colorSetId);
	}

	// Token: 0x06005386 RID: 21382 RVA: 0x001E5040 File Offset: 0x001E3240
	private void UpdateUIScale(float value)
	{
		this.CanvasScalers = UnityEngine.Object.FindObjectsOfType<KCanvasScaler>(true);
		foreach (KCanvasScaler kcanvasScaler in this.CanvasScalers)
		{
			float num = value / 100f;
			kcanvasScaler.SetUserScale(num);
			KPlayerPrefs.SetFloat(KCanvasScaler.UIScalePrefKey, value);
		}
		ScreenResize.Instance.TriggerResize();
		this.UpdateSliderLabel();
	}

	// Token: 0x06005387 RID: 21383 RVA: 0x001E509C File Offset: 0x001E329C
	private void UpdateSliderLabel()
	{
		if (this.CanvasScalers != null && this.CanvasScalers.Length != 0 && this.CanvasScalers[0] != null)
		{
			this.uiScaleSlider.value = this.CanvasScalers[0].GetUserScale() * 100f;
			this.sliderLabel.text = this.uiScaleSlider.value.ToString() + "%";
		}
	}

	// Token: 0x06005388 RID: 21384 RVA: 0x001E5110 File Offset: 0x001E3310
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.resolutionDropdown.Hide();
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005389 RID: 21385 RVA: 0x001E5140 File Offset: 0x001E3340
	private void BuildOptions()
	{
		this.options.Clear();
		this.resolutions.Clear();
		Resolution resolution = default(Resolution);
		resolution.width = Screen.width;
		resolution.height = Screen.height;
		resolution.refreshRate = Screen.currentResolution.refreshRate;
		this.options.Add(new Dropdown.OptionData(resolution.ToString()));
		this.resolutions.Add(resolution);
		foreach (Resolution resolution2 in Screen.resolutions)
		{
			if (resolution2.height >= 720)
			{
				this.options.Add(new Dropdown.OptionData(resolution2.ToString()));
				this.resolutions.Add(resolution2);
			}
		}
	}

	// Token: 0x0600538A RID: 21386 RVA: 0x001E5218 File Offset: 0x001E3418
	private void BuildColorModeOptions()
	{
		this.colorModeOptions.Clear();
		for (int i = 0; i < GlobalAssets.Instance.colorSetOptions.Length; i++)
		{
			this.colorModeOptions.Add(new Dropdown.OptionData(Strings.Get(GlobalAssets.Instance.colorSetOptions[i].settingName)));
		}
	}

	// Token: 0x0600538B RID: 21387 RVA: 0x001E5274 File Offset: 0x001E3474
	private void RefreshColorExamples(int idx)
	{
		Color32 logicOn = GlobalAssets.Instance.colorSetOptions[idx].logicOn;
		Color32 logicOff = GlobalAssets.Instance.colorSetOptions[idx].logicOff;
		Color32 cropHalted = GlobalAssets.Instance.colorSetOptions[idx].cropHalted;
		Color32 cropGrowing = GlobalAssets.Instance.colorSetOptions[idx].cropGrowing;
		Color32 cropGrown = GlobalAssets.Instance.colorSetOptions[idx].cropGrown;
		logicOn.a = byte.MaxValue;
		logicOff.a = byte.MaxValue;
		cropHalted.a = byte.MaxValue;
		cropGrowing.a = byte.MaxValue;
		cropGrown.a = byte.MaxValue;
		this.colorExampleLogicOn.color = logicOn;
		this.colorExampleLogicOff.color = logicOff;
		this.colorExampleCropHalted.color = cropHalted;
		this.colorExampleCropGrowing.color = cropGrowing;
		this.colorExampleCropGrown.color = cropGrown;
	}

	// Token: 0x0600538C RID: 21388 RVA: 0x001E5370 File Offset: 0x001E3570
	private int GetResolutionIndex(Resolution resolution)
	{
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < this.resolutions.Count; i++)
		{
			Resolution resolution2 = this.resolutions[i];
			if (resolution2.width == resolution.width && resolution2.height == resolution.height && resolution2.refreshRate == 0)
			{
				num2 = i;
			}
			if (resolution2.width == resolution.width && resolution2.height == resolution.height && Math.Abs(resolution2.refreshRate - resolution.refreshRate) <= 1)
			{
				num = i;
				break;
			}
		}
		if (num != -1)
		{
			return num;
		}
		return num2;
	}

	// Token: 0x0600538D RID: 21389 RVA: 0x001E5414 File Offset: 0x001E3614
	private GraphicsOptionsScreen.Settings CaptureSettings()
	{
		return new GraphicsOptionsScreen.Settings
		{
			fullscreen = Screen.fullScreen,
			resolution = new Resolution
			{
				width = Screen.width,
				height = Screen.height,
				refreshRate = Screen.currentResolution.refreshRate
			},
			lowRes = QualitySettings.GetQualityLevel(),
			colorSetId = Array.IndexOf<ColorSet>(GlobalAssets.Instance.colorSetOptions, GlobalAssets.Instance.colorSet)
		};
	}

	// Token: 0x0600538E RID: 21390 RVA: 0x001E54A0 File Offset: 0x001E36A0
	private void OnApply()
	{
		try
		{
			GraphicsOptionsScreen.Settings new_settings = default(GraphicsOptionsScreen.Settings);
			new_settings.resolution = this.resolutions[this.resolutionDropdown.value];
			new_settings.fullscreen = this.fullscreenToggle.CurrentState != 0;
			new_settings.lowRes = this.lowResToggle.CurrentState;
			new_settings.colorSetId = this.colorModeId;
			if (GlobalAssets.Instance.colorSetOptions[this.colorModeId] != GlobalAssets.Instance.colorSet)
			{
				this.colorModeChanged = true;
			}
			this.ApplyConfirmSettings(new_settings, delegate
			{
				this.applyButton.isInteractable = false;
				if (this.colorModeChanged)
				{
					this.feedbackDialog = Util.KInstantiateUI(this.confirmPrefab.gameObject, this.transform.gameObject, false).GetComponent<ConfirmDialogScreen>();
					this.feedbackDialog.PopupConfirmDialog(UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.COLORBLIND_FEEDBACK.text, null, null, UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.COLORBLIND_FEEDBACK_BUTTON.text, delegate
					{
						App.OpenWebURL("https://forums.kleientertainment.com/forums/topic/117325-color-blindness-feedback/");
					}, null, null, null, null);
					this.feedbackDialog.gameObject.SetActive(true);
				}
				this.colorModeChanged = false;
				GraphicsOptionsScreen.SaveSettingsToPrefs(new_settings);
			});
		}
		catch (Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Failed to apply graphics options!\nResolutions:");
			foreach (Resolution resolution in this.resolutions)
			{
				stringBuilder.Append("\t" + resolution.ToString() + "\n");
			}
			stringBuilder.Append("Selected Resolution Idx: " + this.resolutionDropdown.value.ToString());
			stringBuilder.Append("FullScreen: " + this.fullscreenToggle.CurrentState.ToString());
			global::Debug.LogError(stringBuilder.ToString());
			throw ex;
		}
	}

	// Token: 0x0600538F RID: 21391 RVA: 0x001E564C File Offset: 0x001E384C
	public void OnDone()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06005390 RID: 21392 RVA: 0x001E565C File Offset: 0x001E385C
	private void RefreshApplyButton()
	{
		GraphicsOptionsScreen.Settings settings = this.CaptureSettings();
		if (settings.fullscreen && this.fullscreenToggle.CurrentState == 0)
		{
			this.applyButton.isInteractable = true;
			return;
		}
		if (!settings.fullscreen && this.fullscreenToggle.CurrentState == 1)
		{
			this.applyButton.isInteractable = true;
			return;
		}
		if (settings.lowRes != this.lowResToggle.CurrentState)
		{
			this.applyButton.isInteractable = true;
			return;
		}
		if (settings.colorSetId != this.colorModeId)
		{
			this.applyButton.isInteractable = true;
			return;
		}
		int resolutionIndex = this.GetResolutionIndex(settings.resolution);
		this.applyButton.isInteractable = this.resolutionDropdown.value != resolutionIndex;
	}

	// Token: 0x06005391 RID: 21393 RVA: 0x001E5719 File Offset: 0x001E3919
	private void OnFullscreenToggle()
	{
		this.fullscreenToggle.ChangeState((this.fullscreenToggle.CurrentState == 0) ? 1 : 0);
		this.RefreshApplyButton();
	}

	// Token: 0x06005392 RID: 21394 RVA: 0x001E573D File Offset: 0x001E393D
	private void OnResolutionChanged(int idx)
	{
		this.RefreshApplyButton();
	}

	// Token: 0x06005393 RID: 21395 RVA: 0x001E5745 File Offset: 0x001E3945
	private void OnColorModeChanged(int idx)
	{
		this.colorModeId = idx;
		this.RefreshApplyButton();
		this.RefreshColorExamples(this.colorModeId);
	}

	// Token: 0x06005394 RID: 21396 RVA: 0x001E5760 File Offset: 0x001E3960
	private void OnLowResToggle()
	{
		this.lowResToggle.ChangeState((this.lowResToggle.CurrentState == 0) ? 1 : 0);
		this.RefreshApplyButton();
	}

	// Token: 0x06005395 RID: 21397 RVA: 0x001E5784 File Offset: 0x001E3984
	private void ApplyConfirmSettings(GraphicsOptionsScreen.Settings new_settings, System.Action on_confirm)
	{
		GraphicsOptionsScreen.Settings current_settings = this.CaptureSettings();
		this.ApplySettings(new_settings);
		this.confirmDialog = Util.KInstantiateUI(this.confirmPrefab.gameObject, base.transform.gameObject, false).GetComponent<ConfirmDialogScreen>();
		System.Action action = delegate
		{
			this.ApplySettings(current_settings);
		};
		Coroutine timer = base.StartCoroutine(this.Timer(15f, action));
		this.confirmDialog.onDeactivateCB = delegate
		{
			this.StopCoroutine(timer);
		};
		this.confirmDialog.PopupConfirmDialog(this.colorModeChanged ? UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.ACCEPT_CHANGES_STRING_COLOR.text : UI.FRONTEND.GRAPHICS_OPTIONS_SCREEN.ACCEPT_CHANGES.text, on_confirm, action, null, null, null, null, null, null);
		this.confirmDialog.gameObject.SetActive(true);
	}

	// Token: 0x06005396 RID: 21398 RVA: 0x001E5858 File Offset: 0x001E3A58
	private void ApplySettings(GraphicsOptionsScreen.Settings new_settings)
	{
		Resolution resolution = new_settings.resolution;
		Screen.SetResolution(resolution.width, resolution.height, new_settings.fullscreen, resolution.refreshRate);
		Screen.fullScreen = new_settings.fullscreen;
		int resolutionIndex = this.GetResolutionIndex(new_settings.resolution);
		if (resolutionIndex != -1)
		{
			this.resolutionDropdown.value = resolutionIndex;
		}
		GlobalAssets.Instance.colorSet = GlobalAssets.Instance.colorSetOptions[new_settings.colorSetId];
		global::Debug.Log("Applying low res settings " + new_settings.lowRes.ToString() + " / existing is " + QualitySettings.GetQualityLevel().ToString());
		if (QualitySettings.GetQualityLevel() != new_settings.lowRes)
		{
			QualitySettings.SetQualityLevel(new_settings.lowRes, true);
		}
	}

	// Token: 0x06005397 RID: 21399 RVA: 0x001E5915 File Offset: 0x001E3B15
	private IEnumerator Timer(float time, System.Action revert)
	{
		yield return SequenceUtil.WaitForSeconds(time);
		if (this.confirmDialog != null)
		{
			this.confirmDialog.Deactivate();
			revert();
		}
		yield break;
	}

	// Token: 0x06005398 RID: 21400 RVA: 0x001E5932 File Offset: 0x001E3B32
	private void Update()
	{
		global::Debug.developerConsoleVisible = false;
	}

	// Token: 0x04003897 RID: 14487
	[SerializeField]
	private Dropdown resolutionDropdown;

	// Token: 0x04003898 RID: 14488
	[SerializeField]
	private MultiToggle lowResToggle;

	// Token: 0x04003899 RID: 14489
	[SerializeField]
	private MultiToggle fullscreenToggle;

	// Token: 0x0400389A RID: 14490
	[SerializeField]
	private KButton applyButton;

	// Token: 0x0400389B RID: 14491
	[SerializeField]
	private KButton doneButton;

	// Token: 0x0400389C RID: 14492
	[SerializeField]
	private KButton closeButton;

	// Token: 0x0400389D RID: 14493
	[SerializeField]
	private ConfirmDialogScreen confirmPrefab;

	// Token: 0x0400389E RID: 14494
	[SerializeField]
	private ConfirmDialogScreen feedbackPrefab;

	// Token: 0x0400389F RID: 14495
	[SerializeField]
	private KSlider uiScaleSlider;

	// Token: 0x040038A0 RID: 14496
	[SerializeField]
	private LocText sliderLabel;

	// Token: 0x040038A1 RID: 14497
	[SerializeField]
	private LocText title;

	// Token: 0x040038A2 RID: 14498
	[SerializeField]
	private Dropdown colorModeDropdown;

	// Token: 0x040038A3 RID: 14499
	[SerializeField]
	private KImage colorExampleLogicOn;

	// Token: 0x040038A4 RID: 14500
	[SerializeField]
	private KImage colorExampleLogicOff;

	// Token: 0x040038A5 RID: 14501
	[SerializeField]
	private KImage colorExampleCropHalted;

	// Token: 0x040038A6 RID: 14502
	[SerializeField]
	private KImage colorExampleCropGrowing;

	// Token: 0x040038A7 RID: 14503
	[SerializeField]
	private KImage colorExampleCropGrown;

	// Token: 0x040038A8 RID: 14504
	public static readonly string ResolutionWidthKey = "ResolutionWidth";

	// Token: 0x040038A9 RID: 14505
	public static readonly string ResolutionHeightKey = "ResolutionHeight";

	// Token: 0x040038AA RID: 14506
	public static readonly string RefreshRateKey = "RefreshRate";

	// Token: 0x040038AB RID: 14507
	public static readonly string FullScreenKey = "FullScreen";

	// Token: 0x040038AC RID: 14508
	public static readonly string LowResKey = "LowResTextures";

	// Token: 0x040038AD RID: 14509
	public static readonly string ColorModeKey = "ColorModeID";

	// Token: 0x040038AE RID: 14510
	private KCanvasScaler[] CanvasScalers;

	// Token: 0x040038AF RID: 14511
	private ConfirmDialogScreen confirmDialog;

	// Token: 0x040038B0 RID: 14512
	private ConfirmDialogScreen feedbackDialog;

	// Token: 0x040038B1 RID: 14513
	private List<Resolution> resolutions = new List<Resolution>();

	// Token: 0x040038B2 RID: 14514
	private List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

	// Token: 0x040038B3 RID: 14515
	private List<Dropdown.OptionData> colorModeOptions = new List<Dropdown.OptionData>();

	// Token: 0x040038B4 RID: 14516
	private int colorModeId;

	// Token: 0x040038B5 RID: 14517
	private bool colorModeChanged;

	// Token: 0x040038B6 RID: 14518
	private GraphicsOptionsScreen.Settings originalSettings;

	// Token: 0x0200192B RID: 6443
	private struct Settings
	{
		// Token: 0x04007386 RID: 29574
		public bool fullscreen;

		// Token: 0x04007387 RID: 29575
		public Resolution resolution;

		// Token: 0x04007388 RID: 29576
		public int lowRes;

		// Token: 0x04007389 RID: 29577
		public int colorSetId;
	}
}
