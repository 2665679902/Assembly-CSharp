using System;
using System.IO;
using FMOD.Studio;
using Klei;
using ProcGen;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000B55 RID: 2901
public class PauseScreen : KModalButtonMenu
{
	// Token: 0x17000665 RID: 1637
	// (get) Token: 0x06005A1D RID: 23069 RVA: 0x00209F39 File Offset: 0x00208139
	public static PauseScreen Instance
	{
		get
		{
			return PauseScreen.instance;
		}
	}

	// Token: 0x06005A1E RID: 23070 RVA: 0x00209F40 File Offset: 0x00208140
	public static void DestroyInstance()
	{
		PauseScreen.instance = null;
	}

	// Token: 0x06005A1F RID: 23071 RVA: 0x00209F48 File Offset: 0x00208148
	protected override void OnPrefabInit()
	{
		this.keepMenuOpen = true;
		base.OnPrefabInit();
		if (!GenericGameSettings.instance.demoMode)
		{
			this.buttons = new KButtonMenu.ButtonInfo[]
			{
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.RESUME, global::Action.NumActions, new UnityAction(this.OnResume), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.SAVE, global::Action.NumActions, new UnityAction(this.OnSave), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.SAVEAS, global::Action.NumActions, new UnityAction(this.OnSaveAs), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.LOAD, global::Action.NumActions, new UnityAction(this.OnLoad), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.OPTIONS, global::Action.NumActions, new UnityAction(this.OnOptions), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.COLONY_SUMMARY, global::Action.NumActions, new UnityAction(this.OnColonySummary), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.LOCKERMENU, global::Action.NumActions, new UnityAction(this.OnLockerMenu), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.QUIT, global::Action.NumActions, new UnityAction(this.OnQuit), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.DESKTOPQUIT, global::Action.NumActions, new UnityAction(this.OnDesktopQuit), null, null)
			};
		}
		else
		{
			this.buttons = new KButtonMenu.ButtonInfo[]
			{
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.RESUME, global::Action.NumActions, new UnityAction(this.OnResume), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.OPTIONS, global::Action.NumActions, new UnityAction(this.OnOptions), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.QUIT, global::Action.NumActions, new UnityAction(this.OnQuit), null, null),
				new KButtonMenu.ButtonInfo(UI.FRONTEND.PAUSE_SCREEN.DESKTOPQUIT, global::Action.NumActions, new UnityAction(this.OnDesktopQuit), null, null)
			};
		}
		this.closeButton.onClick += this.OnResume;
		PauseScreen.instance = this;
		this.Show(false);
	}

	// Token: 0x06005A20 RID: 23072 RVA: 0x0020A194 File Offset: 0x00208394
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.clipboard.GetText = new Func<string>(this.GetClipboardText);
		this.title.SetText(UI.FRONTEND.PAUSE_SCREEN.TITLE);
		try
		{
			string settingsCoordinate = CustomGameSettings.Instance.GetSettingsCoordinate();
			string[] array = CustomGameSettings.ParseSettingCoordinate(settingsCoordinate);
			this.worldSeed.SetText(string.Format(UI.FRONTEND.PAUSE_SCREEN.WORLD_SEED, settingsCoordinate));
			this.worldSeed.GetComponent<ToolTip>().toolTip = string.Format(UI.FRONTEND.PAUSE_SCREEN.WORLD_SEED_TOOLTIP, new object[]
			{
				array[1],
				array[2],
				array[3],
				array[4]
			});
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning(string.Format("Failed to load Coordinates on ClusterLayout {0}, please report this error on the forums", ex));
			CustomGameSettings.Instance.Print();
			global::Debug.Log("ClusterCache: " + string.Join(",", SettingsCache.clusterLayouts.clusterCache.Keys));
			this.worldSeed.SetText(string.Format(UI.FRONTEND.PAUSE_SCREEN.WORLD_SEED, "0"));
		}
	}

	// Token: 0x06005A21 RID: 23073 RVA: 0x0020A2B4 File Offset: 0x002084B4
	public override float GetSortKey()
	{
		return 30f;
	}

	// Token: 0x06005A22 RID: 23074 RVA: 0x0020A2BC File Offset: 0x002084BC
	private string GetClipboardText()
	{
		string text;
		try
		{
			text = CustomGameSettings.Instance.GetSettingsCoordinate();
		}
		catch
		{
			text = "";
		}
		return text;
	}

	// Token: 0x06005A23 RID: 23075 RVA: 0x0020A2F0 File Offset: 0x002084F0
	private void OnResume()
	{
		this.Show(false);
	}

	// Token: 0x06005A24 RID: 23076 RVA: 0x0020A2FC File Offset: 0x002084FC
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().ESCPauseSnapshot);
			MusicManager.instance.OnEscapeMenu(true);
			MusicManager.instance.PlaySong("Music_ESC_Menu", false);
			return;
		}
		ToolTipScreen.Instance.ClearToolTip(this.closeButton.GetComponent<ToolTip>());
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().ESCPauseSnapshot, STOP_MODE.ALLOWFADEOUT);
		MusicManager.instance.OnEscapeMenu(false);
		if (MusicManager.instance.SongIsPlaying("Music_ESC_Menu"))
		{
			MusicManager.instance.StopSong("Music_ESC_Menu", true, STOP_MODE.ALLOWFADEOUT);
		}
	}

	// Token: 0x06005A25 RID: 23077 RVA: 0x0020A39C File Offset: 0x0020859C
	private void OnOptions()
	{
		base.ActivateChildScreen(this.optionsScreen.gameObject);
	}

	// Token: 0x06005A26 RID: 23078 RVA: 0x0020A3B0 File Offset: 0x002085B0
	private void OnSaveAs()
	{
		base.ActivateChildScreen(this.saveScreenPrefab.gameObject);
	}

	// Token: 0x06005A27 RID: 23079 RVA: 0x0020A3C4 File Offset: 0x002085C4
	private void OnSave()
	{
		string filename = SaveLoader.GetActiveSaveFilePath();
		if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
		{
			base.gameObject.SetActive(false);
			((ConfirmDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.transform.parent.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)).PopupConfirmDialog(string.Format(UI.FRONTEND.SAVESCREEN.OVERWRITEMESSAGE, System.IO.Path.GetFileNameWithoutExtension(filename)), delegate
			{
				this.DoSave(filename);
				this.gameObject.SetActive(true);
			}, new System.Action(this.OnCancelPopup), null, null, null, null, null, null);
			return;
		}
		this.OnSaveAs();
	}

	// Token: 0x06005A28 RID: 23080 RVA: 0x0020A488 File Offset: 0x00208688
	private void DoSave(string filename)
	{
		try
		{
			SaveLoader.Instance.Save(filename, false, true);
		}
		catch (IOException ex)
		{
			IOException ex2 = ex;
			IOException e = ex2;
			global::Util.KInstantiateUI(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.transform.parent.gameObject, true).GetComponent<ConfirmDialogScreen>().PopupConfirmDialog(string.Format(UI.FRONTEND.SAVESCREEN.IO_ERROR, e.ToString()), delegate
			{
				this.Deactivate();
			}, null, UI.FRONTEND.SAVESCREEN.REPORT_BUG, delegate
			{
				KCrashReporter.ReportError(e.Message, e.StackTrace.ToString(), null, null, null, "");
			}, null, null, null, null);
		}
	}

	// Token: 0x06005A29 RID: 23081 RVA: 0x0020A538 File Offset: 0x00208738
	private void ConfirmDecision(string text, System.Action onConfirm)
	{
		base.gameObject.SetActive(false);
		((ConfirmDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.transform.parent.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)).PopupConfirmDialog(text, onConfirm, new System.Action(this.OnCancelPopup), null, null, null, null, null, null);
	}

	// Token: 0x06005A2A RID: 23082 RVA: 0x0020A599 File Offset: 0x00208799
	private void OnLoad()
	{
		base.ActivateChildScreen(this.loadScreenPrefab.gameObject);
	}

	// Token: 0x06005A2B RID: 23083 RVA: 0x0020A5B0 File Offset: 0x002087B0
	private void OnColonySummary()
	{
		RetiredColonyData currentColonyRetiredColonyData = RetireColonyUtility.GetCurrentColonyRetiredColonyData();
		MainMenu.ActivateRetiredColoniesScreenFromData(PauseScreen.Instance.transform.parent.gameObject, currentColonyRetiredColonyData);
	}

	// Token: 0x06005A2C RID: 23084 RVA: 0x0020A5DD File Offset: 0x002087DD
	private void OnLockerMenu()
	{
		LockerMenuScreen.Instance.Show(true);
	}

	// Token: 0x06005A2D RID: 23085 RVA: 0x0020A5EA File Offset: 0x002087EA
	private void OnQuit()
	{
		this.ConfirmDecision(UI.FRONTEND.MAINMENU.QUITCONFIRM, new System.Action(this.OnQuitConfirm));
	}

	// Token: 0x06005A2E RID: 23086 RVA: 0x0020A608 File Offset: 0x00208808
	private void OnDesktopQuit()
	{
		this.ConfirmDecision(UI.FRONTEND.MAINMENU.DESKTOPQUITCONFIRM, new System.Action(this.OnDesktopQuitConfirm));
	}

	// Token: 0x06005A2F RID: 23087 RVA: 0x0020A626 File Offset: 0x00208826
	private void OnCancelPopup()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06005A30 RID: 23088 RVA: 0x0020A634 File Offset: 0x00208834
	private void OnLoadConfirm()
	{
		LoadingOverlay.Load(delegate
		{
			LoadScreen.ForceStopGame();
			this.Deactivate();
			App.LoadScene("frontend");
		});
	}

	// Token: 0x06005A31 RID: 23089 RVA: 0x0020A647 File Offset: 0x00208847
	private void OnRetireConfirm()
	{
		RetireColonyUtility.SaveColonySummaryData();
	}

	// Token: 0x06005A32 RID: 23090 RVA: 0x0020A64F File Offset: 0x0020884F
	private void OnQuitConfirm()
	{
		LoadingOverlay.Load(delegate
		{
			this.Deactivate();
			PauseScreen.TriggerQuitGame();
		});
	}

	// Token: 0x06005A33 RID: 23091 RVA: 0x0020A662 File Offset: 0x00208862
	private void OnDesktopQuitConfirm()
	{
		App.Quit();
	}

	// Token: 0x06005A34 RID: 23092 RVA: 0x0020A669 File Offset: 0x00208869
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Show(false);
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005A35 RID: 23093 RVA: 0x0020A68C File Offset: 0x0020888C
	public static void TriggerQuitGame()
	{
		ThreadedHttps<KleiMetrics>.Instance.EndGame();
		LoadScreen.ForceStopGame();
		App.LoadScene("frontend");
	}

	// Token: 0x04003CF1 RID: 15601
	[SerializeField]
	private OptionsMenuScreen optionsScreen;

	// Token: 0x04003CF2 RID: 15602
	[SerializeField]
	private SaveScreen saveScreenPrefab;

	// Token: 0x04003CF3 RID: 15603
	[SerializeField]
	private LoadScreen loadScreenPrefab;

	// Token: 0x04003CF4 RID: 15604
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003CF5 RID: 15605
	[SerializeField]
	private LocText title;

	// Token: 0x04003CF6 RID: 15606
	[SerializeField]
	private LocText worldSeed;

	// Token: 0x04003CF7 RID: 15607
	[SerializeField]
	private CopyTextFieldToClipboard clipboard;

	// Token: 0x04003CF8 RID: 15608
	private float originalTimeScale;

	// Token: 0x04003CF9 RID: 15609
	private static PauseScreen instance;
}
