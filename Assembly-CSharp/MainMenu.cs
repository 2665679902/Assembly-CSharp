using System;
using System.Collections.Generic;
using System.IO;
using FMOD.Studio;
using Klei;
using Steamworks;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AEF RID: 2799
public class MainMenu : KScreen
{
	// Token: 0x17000640 RID: 1600
	// (get) Token: 0x060055C4 RID: 21956 RVA: 0x001F0064 File Offset: 0x001EE264
	public static MainMenu Instance
	{
		get
		{
			return MainMenu._instance;
		}
	}

	// Token: 0x060055C5 RID: 21957 RVA: 0x001F006C File Offset: 0x001EE26C
	private KButton MakeButton(MainMenu.ButtonInfo info)
	{
		KButton kbutton = global::Util.KInstantiateUI<KButton>(this.buttonPrefab.gameObject, this.buttonParent, true);
		kbutton.onClick += info.action;
		KImage component = kbutton.GetComponent<KImage>();
		component.colorStyleSetting = info.style;
		component.ApplyColorStyleSetting();
		LocText componentInChildren = kbutton.GetComponentInChildren<LocText>();
		componentInChildren.text = info.text;
		componentInChildren.fontSize = (float)info.fontSize;
		return kbutton;
	}

	// Token: 0x060055C6 RID: 21958 RVA: 0x001F00D8 File Offset: 0x001EE2D8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MainMenu._instance = this;
		this.Button_NewGame = this.MakeButton(new MainMenu.ButtonInfo(UI.FRONTEND.MAINMENU.NEWGAME, new System.Action(this.NewGame), 22, this.topButtonStyle));
		this.MakeButton(new MainMenu.ButtonInfo(UI.FRONTEND.MAINMENU.LOADGAME, new System.Action(this.LoadGame), 22, this.normalButtonStyle));
		this.MakeButton(new MainMenu.ButtonInfo(UI.FRONTEND.MAINMENU.RETIREDCOLONIES, delegate
		{
			MainMenu.ActivateRetiredColoniesScreen(this.transform.gameObject, "");
		}, 14, this.normalButtonStyle));
		this.lockerButton = this.MakeButton(new MainMenu.ButtonInfo(UI.FRONTEND.MAINMENU.LOCKERMENU, delegate
		{
			MainMenu.ActivateLockerMenu();
		}, 14, this.normalButtonStyle));
		if (DistributionPlatform.Initialized)
		{
			this.MakeButton(new MainMenu.ButtonInfo(UI.FRONTEND.MAINMENU.TRANSLATIONS, new System.Action(this.Translations), 14, this.normalButtonStyle));
			this.MakeButton(new MainMenu.ButtonInfo(UI.FRONTEND.MODS.TITLE, new System.Action(this.Mods), 14, this.normalButtonStyle));
		}
		this.MakeButton(new MainMenu.ButtonInfo(UI.FRONTEND.MAINMENU.OPTIONS, new System.Action(this.Options), 14, this.normalButtonStyle));
		this.MakeButton(new MainMenu.ButtonInfo(UI.FRONTEND.MAINMENU.QUITTODESKTOP, new System.Action(this.QuitGame), 14, this.normalButtonStyle));
		this.RefreshResumeButton(false);
		this.Button_ResumeGame.onClick += this.ResumeGame;
		this.SpawnVideoScreen();
		this.StartFEAudio();
		this.CheckPlayerPrefsCorruption();
		if (PatchNotesScreen.ShouldShowScreen())
		{
			global::Util.KInstantiateUI(this.patchNotesScreenPrefab.gameObject, FrontEndManager.Instance.gameObject, true);
		}
		this.CheckDoubleBoundKeys();
		this.topLeftAlphaMessage.gameObject.SetActive(false);
		this.MOTDContainer.SetActive(false);
		this.buttonContainer.SetActive(false);
		this.nextUpdateTimer.gameObject.SetActive(true);
		bool flag = DistributionPlatform.Inst.IsDLCPurchased("EXPANSION1_ID");
		this.expansion1Toggle.gameObject.SetActive(flag);
		if (this.expansion1Ad != null)
		{
			this.expansion1Ad.gameObject.SetActive(!flag);
		}
		this.m_motdServerClient = new MotdServerClient();
		this.m_motdServerClient.GetMotd(delegate(MotdServerClient.MotdResponse response, string error)
		{
			if (error == null)
			{
				if (DlcManager.IsExpansion1Active())
				{
					this.nextUpdateTimer.UpdateReleaseTimes(response.expansion1_update_data.last_update_time, response.expansion1_update_data.next_update_time, response.expansion1_update_data.update_text_override);
				}
				else
				{
					this.nextUpdateTimer.UpdateReleaseTimes(response.vanilla_update_data.last_update_time, response.vanilla_update_data.next_update_time, response.vanilla_update_data.update_text_override);
				}
				this.topLeftAlphaMessage.gameObject.SetActive(true);
				this.MOTDContainer.SetActive(true);
				this.buttonContainer.SetActive(true);
				this.motdImageHeader.text = response.image_header_text;
				this.motdNewsHeader.text = response.news_header_text;
				this.motdNewsBody.text = response.news_body_text;
				PatchNotesScreen.UpdatePatchNotes(response.patch_notes_summary, response.patch_notes_link_url);
				if (response.image_texture != null)
				{
					this.motdImage.sprite = Sprite.Create(response.image_texture, new Rect(0f, 0f, (float)response.image_texture.width, (float)response.image_texture.height), Vector2.zero);
				}
				else
				{
					global::Debug.LogWarning("GetMotd failed to return an image texture");
				}
				if (this.motdImage.sprite != null && this.motdImage.sprite.rect.height != 0f)
				{
					AspectRatioFitter component = this.motdImage.gameObject.GetComponent<AspectRatioFitter>();
					if (component != null)
					{
						float num = this.motdImage.sprite.rect.width / this.motdImage.sprite.rect.height;
						component.aspectRatio = num;
					}
					else
					{
						global::Debug.LogWarning("Missing AspectRatioFitter on MainMenu motd image.");
					}
				}
				else
				{
					global::Debug.LogWarning("Cannot resize motd image, missing sprite");
				}
				this.motdImageButton.ClearOnClick();
				this.motdImageButton.onClick += delegate
				{
					App.OpenWebURL(response.image_link_url);
				};
				return;
			}
			global::Debug.LogWarning("Motd Request error: " + error);
		});
		if (DistributionPlatform.Initialized && DistributionPlatform.Inst.IsPreviousVersionBranch)
		{
			UnityEngine.Object.Instantiate<GameObject>(ScreenPrefabs.Instance.OldVersionWarningScreen, this.uiCanvas.transform);
		}
		string targetExpansion1AdURL = "";
		Sprite sprite = Assets.GetSprite("expansionPromo_en");
		if (DistributionPlatform.Initialized && this.expansion1Ad != null)
		{
			string name = DistributionPlatform.Inst.Name;
			if (name != null)
			{
				if (!(name == "Steam"))
				{
					if (!(name == "Epic"))
					{
						if (name == "Rail")
						{
							targetExpansion1AdURL = "https://www.wegame.com.cn/store/2001539/";
							sprite = Assets.GetSprite("expansionPromo_cn");
						}
					}
					else
					{
						targetExpansion1AdURL = "https://store.epicgames.com/en-US/p/oxygen-not-included--spaced-out";
					}
				}
				else
				{
					targetExpansion1AdURL = "https://store.steampowered.com/app/1452490/Oxygen_Not_Included__Spaced_Out/";
				}
			}
			this.expansion1Ad.GetComponentInChildren<KButton>().onClick += delegate
			{
				App.OpenWebURL(targetExpansion1AdURL);
			};
			this.expansion1Ad.GetComponent<HierarchyReferences>().GetReference<Image>("Image").sprite = sprite;
		}
		this.activateOnSpawn = true;
	}

	// Token: 0x060055C7 RID: 21959 RVA: 0x001F0450 File Offset: 0x001EE650
	private void OnApplicationFocus(bool focus)
	{
		if (focus)
		{
			this.RefreshResumeButton(false);
		}
	}

	// Token: 0x060055C8 RID: 21960 RVA: 0x001F045C File Offset: 0x001EE65C
	public override void OnKeyDown(KButtonEvent e)
	{
		base.OnKeyDown(e);
		if (e.Consumed)
		{
			return;
		}
		if (e.TryConsume(global::Action.DebugToggleUI))
		{
			this.m_screenshotMode = !this.m_screenshotMode;
			this.uiCanvas.alpha = (this.m_screenshotMode ? 0f : 1f);
		}
		KKeyCode kkeyCode;
		switch (this.m_cheatInputCounter)
		{
		case 0:
			kkeyCode = KKeyCode.K;
			break;
		case 1:
			kkeyCode = KKeyCode.L;
			break;
		case 2:
			kkeyCode = KKeyCode.E;
			break;
		case 3:
			kkeyCode = KKeyCode.I;
			break;
		case 4:
			kkeyCode = KKeyCode.P;
			break;
		case 5:
			kkeyCode = KKeyCode.L;
			break;
		case 6:
			kkeyCode = KKeyCode.A;
			break;
		default:
			kkeyCode = KKeyCode.Y;
			break;
		}
		if (e.Controller.GetKeyDown(kkeyCode))
		{
			e.Consumed = true;
			this.m_cheatInputCounter++;
			if (this.m_cheatInputCounter >= 8)
			{
				global::Debug.Log("Cheat Detected - enabling Debug Mode");
				DebugHandler.SetDebugEnabled(true);
				this.buildWatermark.RefreshText();
				this.m_cheatInputCounter = 0;
				return;
			}
		}
		else
		{
			this.m_cheatInputCounter = 0;
		}
	}

	// Token: 0x060055C9 RID: 21961 RVA: 0x001F055B File Offset: 0x001EE75B
	private void PlayMouseOverSound()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x060055CA RID: 21962 RVA: 0x001F056D File Offset: 0x001EE76D
	private void PlayMouseClickSound()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Open", false));
	}

	// Token: 0x060055CB RID: 21963 RVA: 0x001F0580 File Offset: 0x001EE780
	protected override void OnSpawn()
	{
		global::Debug.Log("-- MAIN MENU -- ");
		base.OnSpawn();
		this.m_cheatInputCounter = 0;
		Canvas.ForceUpdateCanvases();
		this.ShowLanguageConfirmation();
		this.InitLoadScreen();
		LoadScreen.Instance.ShowMigrationIfNecessary(true);
		string savePrefix = SaveLoader.GetSavePrefix();
		try
		{
			string text = Path.Combine(savePrefix, "__SPCCHK");
			using (FileStream fileStream = File.OpenWrite(text))
			{
				byte[] array = new byte[1024];
				for (int i = 0; i < 15360; i++)
				{
					fileStream.Write(array, 0, array.Length);
				}
			}
			File.Delete(text);
		}
		catch (Exception ex)
		{
			string text2;
			if (ex is IOException)
			{
				text2 = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_INSUFFICIENT_SPACE, savePrefix);
			}
			else
			{
				text2 = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_READ_ONLY, savePrefix);
			}
			string text3 = string.Format(text2, savePrefix);
			global::Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.gameObject, true).PopupConfirmDialog(text3, null, null, null, null, null, null, null, null);
		}
		Global.Instance.modManager.Report(base.gameObject);
		if ((GenericGameSettings.instance.autoResumeGame && !MainMenu.HasAutoresumedOnce && !KCrashReporter.hasCrash) || !string.IsNullOrEmpty(GenericGameSettings.instance.performanceCapture.saveGame) || KPlayerPrefs.HasKey("AutoResumeSaveFile"))
		{
			MainMenu.HasAutoresumedOnce = true;
			this.ResumeGame();
		}
		if (GenericGameSettings.instance.devAutoWorldGen && !KCrashReporter.hasCrash)
		{
			GenericGameSettings.instance.devAutoWorldGen = false;
			GenericGameSettings.instance.devAutoWorldGenActive = true;
			GenericGameSettings.instance.SaveSettings();
			global::Util.KInstantiateUI(ScreenPrefabs.Instance.WorldGenScreen.gameObject, base.gameObject, true);
		}
		this.RefreshInventoryNotification();
	}

	// Token: 0x060055CC RID: 21964 RVA: 0x001F0748 File Offset: 0x001EE948
	private void RefreshInventoryNotification()
	{
		this.lockerButton.GetComponent<HierarchyReferences>().GetReference<RectTransform>("AttentionIcon").gameObject.SetActive(false);
	}

	// Token: 0x060055CD RID: 21965 RVA: 0x001F076A File Offset: 0x001EE96A
	private void UnregisterMotdRequest()
	{
		if (this.m_motdServerClient != null)
		{
			this.m_motdServerClient.UnregisterCallback();
			this.m_motdServerClient = null;
		}
	}

	// Token: 0x060055CE RID: 21966 RVA: 0x001F0786 File Offset: 0x001EE986
	protected override void OnActivate()
	{
		if (!this.ambientLoopEventName.IsNullOrWhiteSpace())
		{
			this.ambientLoop = KFMOD.CreateInstance(GlobalAssets.GetSound(this.ambientLoopEventName, false));
			if (this.ambientLoop.isValid())
			{
				this.ambientLoop.start();
			}
		}
	}

	// Token: 0x060055CF RID: 21967 RVA: 0x001F07C5 File Offset: 0x001EE9C5
	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		this.UnregisterMotdRequest();
	}

	// Token: 0x060055D0 RID: 21968 RVA: 0x001F07D3 File Offset: 0x001EE9D3
	public override void ScreenUpdate(bool topLevel)
	{
		this.refreshResumeButton = topLevel;
	}

	// Token: 0x060055D1 RID: 21969 RVA: 0x001F07DC File Offset: 0x001EE9DC
	protected override void OnLoadLevel()
	{
		base.OnLoadLevel();
		this.StopAmbience();
		this.UnregisterMotdRequest();
	}

	// Token: 0x060055D2 RID: 21970 RVA: 0x001F07F0 File Offset: 0x001EE9F0
	private void ShowLanguageConfirmation()
	{
		if (SteamManager.Initialized)
		{
			if (SteamUtils.GetSteamUILanguage() != "schinese")
			{
				return;
			}
			if (KPlayerPrefs.GetInt("LanguageConfirmationVersion") >= MainMenu.LANGUAGE_CONFIRMATION_VERSION)
			{
				return;
			}
			KPlayerPrefs.SetInt("LanguageConfirmationVersion", MainMenu.LANGUAGE_CONFIRMATION_VERSION);
			this.Translations();
		}
	}

	// Token: 0x060055D3 RID: 21971 RVA: 0x001F0840 File Offset: 0x001EEA40
	private void ResumeGame()
	{
		string text;
		if (KPlayerPrefs.HasKey("AutoResumeSaveFile"))
		{
			text = KPlayerPrefs.GetString("AutoResumeSaveFile");
			KPlayerPrefs.DeleteKey("AutoResumeSaveFile");
		}
		else if (!string.IsNullOrEmpty(GenericGameSettings.instance.performanceCapture.saveGame))
		{
			text = GenericGameSettings.instance.performanceCapture.saveGame;
		}
		else
		{
			text = SaveLoader.GetLatestSaveForCurrentDLC();
		}
		if (!string.IsNullOrEmpty(text))
		{
			KCrashReporter.MOST_RECENT_SAVEFILE = text;
			SaveLoader.SetActiveSaveFilePath(text);
			LoadingOverlay.Load(delegate
			{
				App.LoadScene("backend");
			});
		}
	}

	// Token: 0x060055D4 RID: 21972 RVA: 0x001F08D6 File Offset: 0x001EEAD6
	private void NewGame()
	{
		base.GetComponent<NewGameFlow>().BeginFlow();
	}

	// Token: 0x060055D5 RID: 21973 RVA: 0x001F08E3 File Offset: 0x001EEAE3
	private void InitLoadScreen()
	{
		if (LoadScreen.Instance == null)
		{
			global::Util.KInstantiateUI(ScreenPrefabs.Instance.LoadScreen.gameObject, base.gameObject, true).GetComponent<LoadScreen>();
		}
	}

	// Token: 0x060055D6 RID: 21974 RVA: 0x001F0913 File Offset: 0x001EEB13
	private void LoadGame()
	{
		this.InitLoadScreen();
		LoadScreen.Instance.Activate();
	}

	// Token: 0x060055D7 RID: 21975 RVA: 0x001F0928 File Offset: 0x001EEB28
	public static void ActivateRetiredColoniesScreen(GameObject parent, string colonyID = "")
	{
		if (RetiredColonyInfoScreen.Instance == null)
		{
			global::Util.KInstantiateUI(ScreenPrefabs.Instance.RetiredColonyInfoScreen.gameObject, parent, true);
		}
		RetiredColonyInfoScreen.Instance.Show(true);
		if (!string.IsNullOrEmpty(colonyID))
		{
			if (SaveGame.Instance != null)
			{
				RetireColonyUtility.SaveColonySummaryData();
			}
			RetiredColonyInfoScreen.Instance.LoadColony(RetiredColonyInfoScreen.Instance.GetColonyDataByBaseName(colonyID));
		}
	}

	// Token: 0x060055D8 RID: 21976 RVA: 0x001F0994 File Offset: 0x001EEB94
	public static void ActivateRetiredColoniesScreenFromData(GameObject parent, RetiredColonyData data)
	{
		if (RetiredColonyInfoScreen.Instance == null)
		{
			global::Util.KInstantiateUI(ScreenPrefabs.Instance.RetiredColonyInfoScreen.gameObject, parent, true);
		}
		RetiredColonyInfoScreen.Instance.Show(true);
		RetiredColonyInfoScreen.Instance.LoadColony(data);
	}

	// Token: 0x060055D9 RID: 21977 RVA: 0x001F09D0 File Offset: 0x001EEBD0
	public static void ActivateInventoyScreen()
	{
		LockerNavigator.Instance.PushScreen(LockerNavigator.Instance.kleiInventoryScreen, null);
	}

	// Token: 0x060055DA RID: 21978 RVA: 0x001F09E7 File Offset: 0x001EEBE7
	public static void ActivateLockerMenu()
	{
		LockerMenuScreen.Instance.Show(true);
	}

	// Token: 0x060055DB RID: 21979 RVA: 0x001F09F4 File Offset: 0x001EEBF4
	private void SpawnVideoScreen()
	{
		VideoScreen.Instance = global::Util.KInstantiateUI(ScreenPrefabs.Instance.VideoScreen.gameObject, base.gameObject, false).GetComponent<VideoScreen>();
	}

	// Token: 0x060055DC RID: 21980 RVA: 0x001F0A1B File Offset: 0x001EEC1B
	private void Update()
	{
	}

	// Token: 0x060055DD RID: 21981 RVA: 0x001F0A20 File Offset: 0x001EEC20
	public void RefreshResumeButton(bool simpleCheck = false)
	{
		string latestSaveForCurrentDLC = SaveLoader.GetLatestSaveForCurrentDLC();
		bool flag = !string.IsNullOrEmpty(latestSaveForCurrentDLC) && File.Exists(latestSaveForCurrentDLC);
		if (flag)
		{
			try
			{
				if (GenericGameSettings.instance.demoMode)
				{
					flag = false;
				}
				System.DateTime lastWriteTime = File.GetLastWriteTime(latestSaveForCurrentDLC);
				MainMenu.SaveFileEntry saveFileEntry = default(MainMenu.SaveFileEntry);
				SaveGame.Header header = default(SaveGame.Header);
				SaveGame.GameInfo gameInfo = default(SaveGame.GameInfo);
				if (!this.saveFileEntries.TryGetValue(latestSaveForCurrentDLC, out saveFileEntry) || saveFileEntry.timeStamp != lastWriteTime)
				{
					gameInfo = SaveLoader.LoadHeader(latestSaveForCurrentDLC, out header);
					saveFileEntry = new MainMenu.SaveFileEntry
					{
						timeStamp = lastWriteTime,
						header = header,
						headerData = gameInfo
					};
					this.saveFileEntries[latestSaveForCurrentDLC] = saveFileEntry;
				}
				else
				{
					header = saveFileEntry.header;
					gameInfo = saveFileEntry.headerData;
				}
				if (header.buildVersion > 544519U || gameInfo.saveMajorVersion != 7 || gameInfo.saveMinorVersion > 31)
				{
					flag = false;
				}
				if (!DlcManager.IsContentActive(gameInfo.dlcId))
				{
					flag = false;
				}
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(latestSaveForCurrentDLC);
				if (!string.IsNullOrEmpty(gameInfo.baseName))
				{
					this.Button_ResumeGame.GetComponentsInChildren<LocText>()[1].text = string.Format(UI.FRONTEND.MAINMENU.RESUMEBUTTON_BASENAME, gameInfo.baseName, gameInfo.numberOfCycles + 1);
				}
				else
				{
					this.Button_ResumeGame.GetComponentsInChildren<LocText>()[1].text = fileNameWithoutExtension;
				}
			}
			catch (Exception ex)
			{
				global::Debug.LogWarning(ex);
				flag = false;
			}
		}
		if (this.Button_ResumeGame != null && this.Button_ResumeGame.gameObject != null)
		{
			this.Button_ResumeGame.gameObject.SetActive(flag);
			KImage component = this.Button_NewGame.GetComponent<KImage>();
			component.colorStyleSetting = (flag ? this.normalButtonStyle : this.topButtonStyle);
			component.ApplyColorStyleSetting();
			return;
		}
		global::Debug.LogWarning("Why is the resume game button null?");
	}

	// Token: 0x060055DE RID: 21982 RVA: 0x001F0C08 File Offset: 0x001EEE08
	private void Translations()
	{
		global::Util.KInstantiateUI<LanguageOptionsScreen>(ScreenPrefabs.Instance.languageOptionsScreen.gameObject, base.transform.parent.gameObject, false);
	}

	// Token: 0x060055DF RID: 21983 RVA: 0x001F0C30 File Offset: 0x001EEE30
	private void Mods()
	{
		global::Util.KInstantiateUI<ModsScreen>(ScreenPrefabs.Instance.modsMenu.gameObject, base.transform.parent.gameObject, false);
	}

	// Token: 0x060055E0 RID: 21984 RVA: 0x001F0C58 File Offset: 0x001EEE58
	private void Options()
	{
		global::Util.KInstantiateUI<OptionsMenuScreen>(ScreenPrefabs.Instance.OptionsScreen.gameObject, base.gameObject, true);
	}

	// Token: 0x060055E1 RID: 21985 RVA: 0x001F0C76 File Offset: 0x001EEE76
	private void QuitGame()
	{
		App.Quit();
	}

	// Token: 0x060055E2 RID: 21986 RVA: 0x001F0C80 File Offset: 0x001EEE80
	public void StartFEAudio()
	{
		AudioMixer.instance.Reset();
		MusicManager.instance.KillAllSongs(STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndSnapshot);
		if (!AudioMixer.instance.SnapshotIsActive(AudioMixerSnapshots.Get().UserVolumeSettingsSnapshot))
		{
			AudioMixer.instance.StartUserVolumesSnapshot();
		}
		if (AudioDebug.Get().musicEnabled && !MusicManager.instance.SongIsPlaying(this.menuMusicEventName))
		{
			MusicManager.instance.PlaySong(this.menuMusicEventName, false);
		}
		this.CheckForAudioDriverIssue();
	}

	// Token: 0x060055E3 RID: 21987 RVA: 0x001F0D0C File Offset: 0x001EEF0C
	public void StopAmbience()
	{
		if (this.ambientLoop.isValid())
		{
			this.ambientLoop.stop(STOP_MODE.ALLOWFADEOUT);
			this.ambientLoop.release();
			this.ambientLoop.clearHandle();
		}
	}

	// Token: 0x060055E4 RID: 21988 RVA: 0x001F0D3F File Offset: 0x001EEF3F
	public void StopMainMenuMusic()
	{
		if (MusicManager.instance.SongIsPlaying(this.menuMusicEventName))
		{
			MusicManager.instance.StopSong(this.menuMusicEventName, true, STOP_MODE.ALLOWFADEOUT);
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndSnapshot, STOP_MODE.ALLOWFADEOUT);
		}
	}

	// Token: 0x060055E5 RID: 21989 RVA: 0x001F0D7C File Offset: 0x001EEF7C
	private void CheckForAudioDriverIssue()
	{
		if (!KFMOD.didFmodInitializeSuccessfully)
		{
			global::Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.gameObject, true).PopupConfirmDialog(UI.FRONTEND.SUPPORTWARNINGS.AUDIO_DRIVERS, null, null, UI.FRONTEND.SUPPORTWARNINGS.AUDIO_DRIVERS_MORE_INFO, delegate
			{
				App.OpenWebURL("http://support.kleientertainment.com/customer/en/portal/articles/2947881-no-audio-when-playing-oxygen-not-included");
			}, null, null, null, GlobalResources.Instance().sadDupeAudio);
		}
	}

	// Token: 0x060055E6 RID: 21990 RVA: 0x001F0DF4 File Offset: 0x001EEFF4
	private void CheckPlayerPrefsCorruption()
	{
		if (KPlayerPrefs.HasCorruptedFlag())
		{
			KPlayerPrefs.ResetCorruptedFlag();
			global::Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.gameObject, true).PopupConfirmDialog(UI.FRONTEND.SUPPORTWARNINGS.PLAYER_PREFS_CORRUPTED, null, null, null, null, null, null, null, GlobalResources.Instance().sadDupe);
		}
	}

	// Token: 0x060055E7 RID: 21991 RVA: 0x001F0E48 File Offset: 0x001EF048
	private void CheckDoubleBoundKeys()
	{
		string text = "";
		HashSet<BindingEntry> hashSet = new HashSet<BindingEntry>();
		for (int i = 0; i < GameInputMapping.KeyBindings.Length; i++)
		{
			if (GameInputMapping.KeyBindings[i].mKeyCode != KKeyCode.Mouse1)
			{
				for (int j = 0; j < GameInputMapping.KeyBindings.Length; j++)
				{
					if (i != j)
					{
						BindingEntry bindingEntry = GameInputMapping.KeyBindings[j];
						if (!hashSet.Contains(bindingEntry))
						{
							BindingEntry bindingEntry2 = GameInputMapping.KeyBindings[i];
							if (bindingEntry2.mKeyCode != KKeyCode.None && bindingEntry2.mKeyCode == bindingEntry.mKeyCode && bindingEntry2.mModifier == bindingEntry.mModifier && bindingEntry2.mRebindable && bindingEntry.mRebindable)
							{
								string mGroup = GameInputMapping.KeyBindings[i].mGroup;
								string mGroup2 = GameInputMapping.KeyBindings[j].mGroup;
								if ((mGroup == "Root" || mGroup2 == "Root" || mGroup == mGroup2) && (!(mGroup == "Root") || !bindingEntry.mIgnoreRootConflics) && (!(mGroup2 == "Root") || !bindingEntry2.mIgnoreRootConflics))
								{
									text = string.Concat(new string[]
									{
										text,
										"\n\n",
										bindingEntry2.mAction.ToString(),
										": <b>",
										bindingEntry2.mKeyCode.ToString(),
										"</b>\n",
										bindingEntry.mAction.ToString(),
										": <b>",
										bindingEntry.mKeyCode.ToString(),
										"</b>"
									});
									BindingEntry bindingEntry3 = bindingEntry2;
									bindingEntry3.mKeyCode = KKeyCode.None;
									bindingEntry3.mModifier = Modifier.None;
									GameInputMapping.KeyBindings[i] = bindingEntry3;
									bindingEntry3 = bindingEntry;
									bindingEntry3.mKeyCode = KKeyCode.None;
									bindingEntry3.mModifier = Modifier.None;
									GameInputMapping.KeyBindings[j] = bindingEntry3;
								}
							}
						}
					}
				}
				hashSet.Add(GameInputMapping.KeyBindings[i]);
			}
		}
		if (text != "")
		{
			global::Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.gameObject, true).PopupConfirmDialog(string.Format(UI.FRONTEND.SUPPORTWARNINGS.DUPLICATE_KEY_BINDINGS, text), null, null, null, null, null, null, null, GlobalResources.Instance().sadDupe);
		}
	}

	// Token: 0x060055E8 RID: 21992 RVA: 0x001F10D5 File Offset: 0x001EF2D5
	private void RestartGame()
	{
		App.instance.Restart();
	}

	// Token: 0x04003A51 RID: 14929
	private static MainMenu _instance;

	// Token: 0x04003A52 RID: 14930
	public KButton Button_ResumeGame;

	// Token: 0x04003A53 RID: 14931
	private KButton Button_NewGame;

	// Token: 0x04003A54 RID: 14932
	public GameObject topLeftAlphaMessage;

	// Token: 0x04003A55 RID: 14933
	private MotdServerClient m_motdServerClient;

	// Token: 0x04003A56 RID: 14934
	private GameObject GameSettingsScreen;

	// Token: 0x04003A57 RID: 14935
	private bool m_screenshotMode;

	// Token: 0x04003A58 RID: 14936
	[SerializeField]
	private CanvasGroup uiCanvas;

	// Token: 0x04003A59 RID: 14937
	[SerializeField]
	private KButton buttonPrefab;

	// Token: 0x04003A5A RID: 14938
	[SerializeField]
	private GameObject buttonParent;

	// Token: 0x04003A5B RID: 14939
	[SerializeField]
	private ColorStyleSetting topButtonStyle;

	// Token: 0x04003A5C RID: 14940
	[SerializeField]
	private ColorStyleSetting normalButtonStyle;

	// Token: 0x04003A5D RID: 14941
	[SerializeField]
	private string menuMusicEventName;

	// Token: 0x04003A5E RID: 14942
	[SerializeField]
	private string ambientLoopEventName;

	// Token: 0x04003A5F RID: 14943
	private EventInstance ambientLoop;

	// Token: 0x04003A60 RID: 14944
	[SerializeField]
	private GameObject MOTDContainer;

	// Token: 0x04003A61 RID: 14945
	[SerializeField]
	private GameObject buttonContainer;

	// Token: 0x04003A62 RID: 14946
	[SerializeField]
	private LocText motdImageHeader;

	// Token: 0x04003A63 RID: 14947
	[SerializeField]
	private KButton motdImageButton;

	// Token: 0x04003A64 RID: 14948
	[SerializeField]
	private Image motdImage;

	// Token: 0x04003A65 RID: 14949
	[SerializeField]
	private LocText motdNewsHeader;

	// Token: 0x04003A66 RID: 14950
	[SerializeField]
	private LocText motdNewsBody;

	// Token: 0x04003A67 RID: 14951
	[SerializeField]
	private PatchNotesScreen patchNotesScreenPrefab;

	// Token: 0x04003A68 RID: 14952
	[SerializeField]
	private NextUpdateTimer nextUpdateTimer;

	// Token: 0x04003A69 RID: 14953
	[SerializeField]
	private DLCToggle expansion1Toggle;

	// Token: 0x04003A6A RID: 14954
	[SerializeField]
	private GameObject expansion1Ad;

	// Token: 0x04003A6B RID: 14955
	[SerializeField]
	private BuildWatermark buildWatermark;

	// Token: 0x04003A6C RID: 14956
	[SerializeField]
	public string IntroShortName;

	// Token: 0x04003A6D RID: 14957
	private KButton lockerButton;

	// Token: 0x04003A6E RID: 14958
	private static bool HasAutoresumedOnce = false;

	// Token: 0x04003A6F RID: 14959
	private bool refreshResumeButton = true;

	// Token: 0x04003A70 RID: 14960
	private int m_cheatInputCounter;

	// Token: 0x04003A71 RID: 14961
	public const string AutoResumeSaveFileKey = "AutoResumeSaveFile";

	// Token: 0x04003A72 RID: 14962
	public const string PLAY_SHORT_ON_LAUNCH = "PlayShortOnLaunch";

	// Token: 0x04003A73 RID: 14963
	private static int LANGUAGE_CONFIRMATION_VERSION = 2;

	// Token: 0x04003A74 RID: 14964
	private Dictionary<string, MainMenu.SaveFileEntry> saveFileEntries = new Dictionary<string, MainMenu.SaveFileEntry>();

	// Token: 0x0200196D RID: 6509
	private struct ButtonInfo
	{
		// Token: 0x0600903A RID: 36922 RVA: 0x003122D6 File Offset: 0x003104D6
		public ButtonInfo(LocString text, System.Action action, int font_size, ColorStyleSetting style)
		{
			this.text = text;
			this.action = action;
			this.fontSize = font_size;
			this.style = style;
		}

		// Token: 0x0400744E RID: 29774
		public LocString text;

		// Token: 0x0400744F RID: 29775
		public System.Action action;

		// Token: 0x04007450 RID: 29776
		public int fontSize;

		// Token: 0x04007451 RID: 29777
		public ColorStyleSetting style;
	}

	// Token: 0x0200196E RID: 6510
	private struct SaveFileEntry
	{
		// Token: 0x04007452 RID: 29778
		public System.DateTime timeStamp;

		// Token: 0x04007453 RID: 29779
		public SaveGame.Header header;

		// Token: 0x04007454 RID: 29780
		public SaveGame.GameInfo headerData;
	}
}
