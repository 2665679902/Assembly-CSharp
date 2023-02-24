using System;
using System.IO;
using Steamworks;
using STRINGS;
using UnityEngine;

// Token: 0x02000AA1 RID: 2721
public class GameOptionsScreen : KModalButtonMenu
{
	// Token: 0x06005361 RID: 21345 RVA: 0x001E418D File Offset: 0x001E238D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06005362 RID: 21346 RVA: 0x001E4198 File Offset: 0x001E2398
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.unitConfiguration.Init();
		if (SaveGame.Instance != null)
		{
			this.saveConfiguration.ToggleDisabledContent(true);
			this.saveConfiguration.Init();
			this.SetSandboxModeActive(SaveGame.Instance.sandboxEnabled);
		}
		else
		{
			this.saveConfiguration.ToggleDisabledContent(false);
		}
		this.resetTutorialButton.onClick += this.OnTutorialReset;
		if (DistributionPlatform.Initialized && SteamUtils.IsSteamRunningOnSteamDeck())
		{
			this.controlsButton.gameObject.SetActive(false);
		}
		else
		{
			this.controlsButton.onClick += this.OnKeyBindings;
		}
		this.sandboxButton.onClick += this.OnUnlockSandboxMode;
		this.doneButton.onClick += this.Deactivate;
		this.closeButton.onClick += this.Deactivate;
		if (this.defaultToCloudSaveToggle != null)
		{
			this.RefreshCloudSaveToggle();
			this.defaultToCloudSaveToggle.GetComponentInChildren<KButton>().onClick += this.OnDefaultToCloudSaveToggle;
		}
		if (this.cloudSavesPanel != null)
		{
			this.cloudSavesPanel.SetActive(SaveLoader.GetCloudSavesAvailable());
		}
		this.cameraSpeedSlider.minValue = 1f;
		this.cameraSpeedSlider.maxValue = 20f;
		this.cameraSpeedSlider.onValueChanged.AddListener(delegate(float val)
		{
			this.OnCameraSpeedValueChanged(Mathf.FloorToInt(val));
		});
		this.cameraSpeedSlider.value = this.CameraSpeedToSlider(KPlayerPrefs.GetFloat("CameraSpeed"));
		this.RefreshCameraSliderLabel();
	}

	// Token: 0x06005363 RID: 21347 RVA: 0x001E433C File Offset: 0x001E253C
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (SaveGame.Instance != null)
		{
			this.savePanel.SetActive(true);
			this.saveConfiguration.Show(show);
			this.SetSandboxModeActive(SaveGame.Instance.sandboxEnabled);
		}
		else
		{
			this.savePanel.SetActive(false);
		}
		if (!KPlayerPrefs.HasKey("CameraSpeed"))
		{
			CameraController.SetDefaultCameraSpeed();
		}
	}

	// Token: 0x06005364 RID: 21348 RVA: 0x001E43A4 File Offset: 0x001E25A4
	private float CameraSpeedToSlider(float prefsValue)
	{
		return prefsValue * 10f;
	}

	// Token: 0x06005365 RID: 21349 RVA: 0x001E43AD File Offset: 0x001E25AD
	private void OnCameraSpeedValueChanged(int sliderValue)
	{
		KPlayerPrefs.SetFloat("CameraSpeed", (float)sliderValue / 10f);
		this.RefreshCameraSliderLabel();
		if (Game.Instance != null)
		{
			Game.Instance.Trigger(75424175, null);
		}
	}

	// Token: 0x06005366 RID: 21350 RVA: 0x001E43E4 File Offset: 0x001E25E4
	private void RefreshCameraSliderLabel()
	{
		this.cameraSpeedSliderLabel.text = string.Format(UI.FRONTEND.GAME_OPTIONS_SCREEN.CAMERA_SPEED_LABEL, (KPlayerPrefs.GetFloat("CameraSpeed") * 10f * 10f).ToString());
	}

	// Token: 0x06005367 RID: 21351 RVA: 0x001E4429 File Offset: 0x001E2629
	private void OnDefaultToCloudSaveToggle()
	{
		SaveLoader.SetCloudSavesDefault(!SaveLoader.GetCloudSavesDefault());
		this.RefreshCloudSaveToggle();
	}

	// Token: 0x06005368 RID: 21352 RVA: 0x001E4440 File Offset: 0x001E2640
	private void RefreshCloudSaveToggle()
	{
		bool cloudSavesDefault = SaveLoader.GetCloudSavesDefault();
		this.defaultToCloudSaveToggle.GetComponent<HierarchyReferences>().GetReference("Checkmark").gameObject.SetActive(cloudSavesDefault);
	}

	// Token: 0x06005369 RID: 21353 RVA: 0x001E4473 File Offset: 0x001E2673
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600536A RID: 21354 RVA: 0x001E4498 File Offset: 0x001E2698
	private void OnTutorialReset()
	{
		ConfirmDialogScreen component = base.ActivateChildScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject).GetComponent<ConfirmDialogScreen>();
		component.PopupConfirmDialog(UI.FRONTEND.OPTIONS_SCREEN.RESET_TUTORIAL_WARNING, delegate
		{
			Tutorial.ResetHiddenTutorialMessages();
		}, delegate
		{
		}, null, null, null, null, null, null);
		component.Activate();
	}

	// Token: 0x0600536B RID: 21355 RVA: 0x001E4518 File Offset: 0x001E2718
	private void OnUnlockSandboxMode()
	{
		ConfirmDialogScreen component = base.ActivateChildScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject).GetComponent<ConfirmDialogScreen>();
		string text = UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.UNLOCK_SANDBOX_WARNING;
		System.Action action = delegate
		{
			SaveGame.Instance.sandboxEnabled = true;
			this.SetSandboxModeActive(SaveGame.Instance.sandboxEnabled);
			TopLeftControlScreen.Instance.UpdateSandboxToggleState();
			this.Deactivate();
		};
		System.Action action2 = delegate
		{
			string savePrefixAndCreateFolder = SaveLoader.GetSavePrefixAndCreateFolder();
			string text4 = SaveGame.Instance.BaseName + UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.BACKUP_SAVE_GAME_APPEND + ".sav";
			SaveLoader.Instance.Save(Path.Combine(savePrefixAndCreateFolder, text4), false, false);
			this.SetSandboxModeActive(SaveGame.Instance.sandboxEnabled);
			TopLeftControlScreen.Instance.UpdateSandboxToggleState();
			this.Deactivate();
		};
		string text2 = UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.CONFIRM;
		string text3 = UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.CONFIRM_SAVE_BACKUP;
		component.PopupConfirmDialog(text, action, action2, UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.CANCEL, delegate
		{
		}, null, text2, text3, null);
		component.Activate();
	}

	// Token: 0x0600536C RID: 21356 RVA: 0x001E45AF File Offset: 0x001E27AF
	private void OnKeyBindings()
	{
		base.ActivateChildScreen(this.inputBindingsScreenPrefab.gameObject);
	}

	// Token: 0x0600536D RID: 21357 RVA: 0x001E45C4 File Offset: 0x001E27C4
	private void SetSandboxModeActive(bool active)
	{
		this.sandboxButton.GetComponent<HierarchyReferences>().GetReference("Checkmark").gameObject.SetActive(active);
		this.sandboxButton.isInteractable = !active;
		this.sandboxButton.gameObject.GetComponentInParent<CanvasGroup>().alpha = (active ? 0.5f : 1f);
	}

	// Token: 0x0400387C RID: 14460
	[SerializeField]
	private SaveConfigurationScreen saveConfiguration;

	// Token: 0x0400387D RID: 14461
	[SerializeField]
	private UnitConfigurationScreen unitConfiguration;

	// Token: 0x0400387E RID: 14462
	[SerializeField]
	private KButton resetTutorialButton;

	// Token: 0x0400387F RID: 14463
	[SerializeField]
	private KButton controlsButton;

	// Token: 0x04003880 RID: 14464
	[SerializeField]
	private KButton sandboxButton;

	// Token: 0x04003881 RID: 14465
	[SerializeField]
	private ConfirmDialogScreen confirmPrefab;

	// Token: 0x04003882 RID: 14466
	[SerializeField]
	private KButton doneButton;

	// Token: 0x04003883 RID: 14467
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003884 RID: 14468
	[SerializeField]
	private GameObject cloudSavesPanel;

	// Token: 0x04003885 RID: 14469
	[SerializeField]
	private GameObject defaultToCloudSaveToggle;

	// Token: 0x04003886 RID: 14470
	[SerializeField]
	private GameObject savePanel;

	// Token: 0x04003887 RID: 14471
	[SerializeField]
	private InputBindingsScreen inputBindingsScreenPrefab;

	// Token: 0x04003888 RID: 14472
	[SerializeField]
	private KSlider cameraSpeedSlider;

	// Token: 0x04003889 RID: 14473
	[SerializeField]
	private LocText cameraSpeedSliderLabel;

	// Token: 0x0400388A RID: 14474
	private const int cameraSliderNotchScale = 10;

	// Token: 0x0400388B RID: 14475
	public const string PREFS_KEY_CAMERA_SPEED = "CameraSpeed";
}
