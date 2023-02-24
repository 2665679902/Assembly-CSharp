using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using FMOD.Studio;
using ProcGen;
using ProcGenGame;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B72 RID: 2930
public class RetiredColonyInfoScreen : KModalScreen
{
	// Token: 0x06005BC6 RID: 23494 RVA: 0x00217194 File Offset: 0x00215394
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		RetiredColonyInfoScreen.Instance = this;
		this.ConfigButtons();
		this.LoadExplorer();
		this.PopulateAchievements();
		base.ConsumeMouseScroll = true;
		this.explorerSearch.text = "";
		this.explorerSearch.onValueChanged.AddListener(delegate(string value)
		{
			if (this.colonyDataRoot.activeSelf)
			{
				this.FilterColonyData(this.explorerSearch.text);
				return;
			}
			this.FilterExplorer(this.explorerSearch.text);
		});
		this.clearExplorerSearchButton.onClick += delegate
		{
			this.explorerSearch.text = "";
		};
		this.achievementSearch.text = "";
		this.achievementSearch.onValueChanged.AddListener(delegate(string value)
		{
			this.FilterAchievements(this.achievementSearch.text);
		});
		this.clearAchievementSearchButton.onClick += delegate
		{
			this.achievementSearch.text = "";
		};
		this.RefreshUIScale(null);
		base.Subscribe(-810220474, new Action<object>(this.RefreshUIScale));
	}

	// Token: 0x06005BC7 RID: 23495 RVA: 0x0021726B File Offset: 0x0021546B
	private void RefreshUIScale(object data = null)
	{
		base.StartCoroutine(this.DelayedRefreshScale());
	}

	// Token: 0x06005BC8 RID: 23496 RVA: 0x0021727A File Offset: 0x0021547A
	private IEnumerator DelayedRefreshScale()
	{
		int num;
		for (int i = 0; i < 3; i = num + 1)
		{
			yield return 0;
			num = i;
		}
		float num2 = 36f;
		if (GameObject.Find("ScreenSpaceOverlayCanvas") != null)
		{
			this.explorerRoot.transform.parent.localScale = Vector3.one * ((this.colonyScroll.rectTransform().rect.width - num2) / this.explorerRoot.transform.parent.rectTransform().rect.width);
		}
		else
		{
			this.explorerRoot.transform.parent.localScale = Vector3.one * ((this.colonyScroll.rectTransform().rect.width - num2) / this.explorerRoot.transform.parent.rectTransform().rect.width);
		}
		yield break;
	}

	// Token: 0x06005BC9 RID: 23497 RVA: 0x0021728C File Offset: 0x0021548C
	private void ConfigButtons()
	{
		this.closeButton.ClearOnClick();
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		this.viewOtherColoniesButton.ClearOnClick();
		this.viewOtherColoniesButton.onClick += delegate
		{
			this.ToggleExplorer(true);
		};
		this.quitToMainMenuButton.ClearOnClick();
		this.quitToMainMenuButton.onClick += delegate
		{
			this.ConfirmDecision(UI.FRONTEND.MAINMENU.QUITCONFIRM, new System.Action(this.OnQuitConfirm));
		};
		this.closeScreenButton.ClearOnClick();
		this.closeScreenButton.onClick += delegate
		{
			this.Show(false);
		};
		this.viewOtherColoniesButton.gameObject.SetActive(false);
		if (Game.Instance != null)
		{
			this.closeScreenButton.gameObject.SetActive(true);
			this.closeScreenButton.GetComponentInChildren<LocText>().SetText(UI.RETIRED_COLONY_INFO_SCREEN.BUTTONS.RETURN_TO_GAME);
			this.quitToMainMenuButton.gameObject.SetActive(true);
			return;
		}
		this.closeScreenButton.gameObject.SetActive(true);
		this.closeScreenButton.GetComponentInChildren<LocText>().SetText(UI.RETIRED_COLONY_INFO_SCREEN.BUTTONS.CLOSE);
		this.quitToMainMenuButton.gameObject.SetActive(false);
	}

	// Token: 0x06005BCA RID: 23498 RVA: 0x002173B8 File Offset: 0x002155B8
	private void ConfirmDecision(string text, System.Action onConfirm)
	{
		base.gameObject.SetActive(false);
		((ConfirmDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.transform.parent.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)).PopupConfirmDialog(text, onConfirm, new System.Action(this.OnCancelPopup), null, null, null, null, null, null);
	}

	// Token: 0x06005BCB RID: 23499 RVA: 0x00217419 File Offset: 0x00215619
	private void OnCancelPopup()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06005BCC RID: 23500 RVA: 0x00217427 File Offset: 0x00215627
	private void OnQuitConfirm()
	{
		LoadingOverlay.Load(delegate
		{
			this.Deactivate();
			PauseScreen.TriggerQuitGame();
		});
	}

	// Token: 0x06005BCD RID: 23501 RVA: 0x0021743A File Offset: 0x0021563A
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.GetCanvasRef();
		this.wasPixelPerfect = this.canvasRef.pixelPerfect;
		this.canvasRef.pixelPerfect = false;
	}

	// Token: 0x06005BCE RID: 23502 RVA: 0x00217465 File Offset: 0x00215665
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (e.TryConsume(global::Action.Escape))
		{
			this.Show(false);
		}
		else if (e.TryConsume(global::Action.MouseRight))
		{
			this.Show(false);
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005BCF RID: 23503 RVA: 0x0021749C File Offset: 0x0021569C
	private void GetCanvasRef()
	{
		if (base.transform.parent.GetComponent<Canvas>() != null)
		{
			this.canvasRef = base.transform.parent.GetComponent<Canvas>();
			return;
		}
		this.canvasRef = base.transform.parent.parent.GetComponent<Canvas>();
	}

	// Token: 0x06005BD0 RID: 23504 RVA: 0x002174F3 File Offset: 0x002156F3
	protected override void OnCmpDisable()
	{
		this.canvasRef.pixelPerfect = this.wasPixelPerfect;
		base.OnCmpDisable();
	}

	// Token: 0x06005BD1 RID: 23505 RVA: 0x0021750C File Offset: 0x0021570C
	public RetiredColonyData GetColonyDataByBaseName(string name)
	{
		name = RetireColonyUtility.StripInvalidCharacters(name);
		for (int i = 0; i < this.retiredColonyData.Length; i++)
		{
			if (RetireColonyUtility.StripInvalidCharacters(this.retiredColonyData[i].colonyName) == name)
			{
				return this.retiredColonyData[i];
			}
		}
		return null;
	}

	// Token: 0x06005BD2 RID: 23506 RVA: 0x00217558 File Offset: 0x00215758
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.explorerSearch.text = "";
			this.achievementSearch.text = "";
			this.RefreshUIScale(null);
		}
		else
		{
			this.InstantClearAchievementVeils();
		}
		if (Game.Instance != null)
		{
			if (!show)
			{
				if (MusicManager.instance.SongIsPlaying("Music_Victory_03_StoryAndSummary"))
				{
					MusicManager.instance.StopSong("Music_Victory_03_StoryAndSummary", true, STOP_MODE.ALLOWFADEOUT);
				}
			}
			else
			{
				this.retiredColonyData = RetireColonyUtility.LoadRetiredColonies(true);
				if (MusicManager.instance.SongIsPlaying("Music_Victory_03_StoryAndSummary"))
				{
					MusicManager.instance.SetSongParameter("Music_Victory_03_StoryAndSummary", "songSection", 2f, true);
				}
			}
		}
		else if (Game.Instance == null)
		{
			this.ToggleExplorer(true);
		}
		this.disabledPlatformUnlocks.SetActive(SaveGame.Instance != null);
		if (SaveGame.Instance != null)
		{
			this.disabledPlatformUnlocks.GetComponent<HierarchyReferences>().GetReference("enabled").gameObject.SetActive(!DebugHandler.InstantBuildMode && !SaveGame.Instance.sandboxEnabled && !Game.Instance.debugWasUsed);
			this.disabledPlatformUnlocks.GetComponent<HierarchyReferences>().GetReference("disabled").gameObject.SetActive(DebugHandler.InstantBuildMode || SaveGame.Instance.sandboxEnabled || Game.Instance.debugWasUsed);
		}
	}

	// Token: 0x06005BD3 RID: 23507 RVA: 0x002176C8 File Offset: 0x002158C8
	public void LoadColony(RetiredColonyData data)
	{
		this.colonyName.text = data.colonyName.ToUpper();
		this.cycleCount.text = string.Format(UI.RETIRED_COLONY_INFO_SCREEN.CYCLE_COUNT, data.cycleCount.ToString());
		this.focusedWorld = data.startWorld;
		this.ToggleExplorer(false);
		this.RefreshUIScale(null);
		if (Game.Instance == null)
		{
			this.viewOtherColoniesButton.gameObject.SetActive(true);
		}
		this.ClearColony();
		if (SaveGame.Instance != null)
		{
			ColonyAchievementTracker component = SaveGame.Instance.GetComponent<ColonyAchievementTracker>();
			this.UpdateAchievementData(data, component.achievementsToDisplay.ToArray());
			component.ClearDisplayAchievements();
			this.PopulateAchievementProgress(component);
		}
		else
		{
			this.UpdateAchievementData(data, null);
		}
		this.DisplayStatistics(data);
		this.colonyDataRoot.transform.parent.rectTransform().SetPosition(new Vector3(this.colonyDataRoot.transform.parent.rectTransform().position.x, 0f, 0f));
	}

	// Token: 0x06005BD4 RID: 23508 RVA: 0x002177E4 File Offset: 0x002159E4
	private void PopulateAchievementProgress(ColonyAchievementTracker tracker)
	{
		if (tracker != null)
		{
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.achievementEntries)
			{
				ColonyAchievementStatus colonyAchievementStatus;
				tracker.achievements.TryGetValue(keyValuePair.Key, out colonyAchievementStatus);
				if (colonyAchievementStatus != null)
				{
					AchievementWidget component = keyValuePair.Value.GetComponent<AchievementWidget>();
					if (component != null)
					{
						component.ShowProgress(colonyAchievementStatus);
						if (colonyAchievementStatus.failed)
						{
							component.SetFailed();
						}
					}
				}
			}
		}
	}

	// Token: 0x06005BD5 RID: 23509 RVA: 0x0021787C File Offset: 0x00215A7C
	private bool LoadSlideshow(RetiredColonyData data)
	{
		this.clearCurrentSlideshow();
		this.currentSlideshowFiles = RetireColonyUtility.LoadColonySlideshowFiles(data.colonyName, this.focusedWorld);
		this.slideshow.SetFiles(this.currentSlideshowFiles, -1);
		return this.currentSlideshowFiles != null && this.currentSlideshowFiles.Length != 0;
	}

	// Token: 0x06005BD6 RID: 23510 RVA: 0x002178CC File Offset: 0x00215ACC
	private void clearCurrentSlideshow()
	{
		this.currentSlideshowFiles = new string[0];
	}

	// Token: 0x06005BD7 RID: 23511 RVA: 0x002178DC File Offset: 0x00215ADC
	private bool LoadScreenshot(RetiredColonyData data, string world)
	{
		this.clearCurrentSlideshow();
		Sprite sprite = RetireColonyUtility.LoadRetiredColonyPreview(data.colonyName, world);
		if (sprite != null)
		{
			this.slideshow.setSlide(sprite);
			this.CorrectTimelapseImageSize(sprite);
		}
		return sprite != null;
	}

	// Token: 0x06005BD8 RID: 23512 RVA: 0x00217920 File Offset: 0x00215B20
	private void ClearColony()
	{
		foreach (GameObject gameObject in this.activeColonyWidgetContainers)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		this.activeColonyWidgetContainers.Clear();
		this.activeColonyWidgets.Clear();
		this.UpdateAchievementData(null, null);
	}

	// Token: 0x06005BD9 RID: 23513 RVA: 0x00217990 File Offset: 0x00215B90
	private void PopulateAchievements()
	{
		foreach (ColonyAchievement colonyAchievement in Db.Get().ColonyAchievements.resources)
		{
			GameObject gameObject = global::Util.KInstantiateUI(colonyAchievement.isVictoryCondition ? this.victoryAchievementsPrefab : this.achievementsPrefab, this.achievementsContainer, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("nameLabel").SetText(colonyAchievement.Name);
			component.GetReference<LocText>("descriptionLabel").SetText(colonyAchievement.description);
			if (string.IsNullOrEmpty(colonyAchievement.icon) || Assets.GetSprite(colonyAchievement.icon) == null)
			{
				if (Assets.GetSprite(colonyAchievement.Name) != null)
				{
					component.GetReference<Image>("icon").sprite = Assets.GetSprite(colonyAchievement.Name);
				}
				else
				{
					component.GetReference<Image>("icon").sprite = Assets.GetSprite("check");
				}
			}
			else
			{
				component.GetReference<Image>("icon").sprite = Assets.GetSprite(colonyAchievement.icon);
			}
			if (colonyAchievement.isVictoryCondition)
			{
				gameObject.transform.SetAsFirstSibling();
			}
			bool flag = !DlcManager.IsValidForVanilla(colonyAchievement.dlcIds);
			component.GetReference<KImage>("dlc_overlay").gameObject.SetActive(flag);
			gameObject.GetComponent<MultiToggle>().ChangeState(2);
			gameObject.GetComponent<AchievementWidget>().dlcAchievement = flag;
			this.achievementEntries.Add(colonyAchievement.Id, gameObject);
		}
		this.UpdateAchievementData(null, null);
	}

	// Token: 0x06005BDA RID: 23514 RVA: 0x00217B5C File Offset: 0x00215D5C
	private void InstantClearAchievementVeils()
	{
		GameObject[] array = this.achievementVeils;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
		}
		array = this.achievementVeils;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.achievementEntries)
		{
			AchievementWidget component = keyValuePair.Value.GetComponent<AchievementWidget>();
			component.StopAllCoroutines();
			component.CompleteFlourish();
		}
	}

	// Token: 0x06005BDB RID: 23515 RVA: 0x00217C18 File Offset: 0x00215E18
	private IEnumerator ClearAchievementVeil(float delay = 0f)
	{
		yield return new WaitForSecondsRealtime(delay);
		for (float i = 0.7f; i >= 0f; i -= Time.unscaledDeltaTime)
		{
			GameObject[] array = this.achievementVeils;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].GetComponent<Image>().color = new Color(0f, 0f, 0f, i);
			}
			yield return 0;
		}
		this.InstantClearAchievementVeils();
		yield break;
	}

	// Token: 0x06005BDC RID: 23516 RVA: 0x00217C2E File Offset: 0x00215E2E
	private IEnumerator ShowAchievementVeil()
	{
		float targetAlpha = 0.7f;
		GameObject[] array = this.achievementVeils;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].SetActive(true);
		}
		for (float i = 0f; i <= targetAlpha; i += Time.unscaledDeltaTime)
		{
			array = this.achievementVeils;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].GetComponent<Image>().color = new Color(0f, 0f, 0f, i);
			}
			yield return 0;
		}
		for (float num = 0f; num <= targetAlpha; num += Time.unscaledDeltaTime)
		{
			array = this.achievementVeils;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].GetComponent<Image>().color = new Color(0f, 0f, 0f, targetAlpha);
			}
		}
		yield break;
	}

	// Token: 0x06005BDD RID: 23517 RVA: 0x00217C40 File Offset: 0x00215E40
	private void UpdateAchievementData(RetiredColonyData data, string[] newlyAchieved = null)
	{
		int num = 0;
		float num2 = 2f;
		float num3 = 1f;
		if (newlyAchieved != null && newlyAchieved.Length != 0)
		{
			this.retiredColonyData = RetireColonyUtility.LoadRetiredColonies(true);
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.achievementEntries)
		{
			bool flag = false;
			bool flag2 = false;
			if (data != null)
			{
				string[] array = data.achievements;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == keyValuePair.Key)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag && data == null && this.retiredColonyData != null)
			{
				RetiredColonyData[] array2 = this.retiredColonyData;
				for (int i = 0; i < array2.Length; i++)
				{
					string[] array = array2[i].achievements;
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j] == keyValuePair.Key)
						{
							flag2 = true;
						}
					}
				}
			}
			bool flag3 = false;
			if (newlyAchieved != null)
			{
				for (int k = 0; k < newlyAchieved.Length; k++)
				{
					if (newlyAchieved[k] == keyValuePair.Key)
					{
						flag3 = true;
					}
				}
			}
			if (flag || flag3)
			{
				if (flag3)
				{
					keyValuePair.Value.GetComponent<AchievementWidget>().ActivateNewlyAchievedFlourish(num3 + (float)num * num2);
					num++;
				}
				else
				{
					keyValuePair.Value.GetComponent<AchievementWidget>().SetAchievedNow();
				}
			}
			else if (flag2)
			{
				keyValuePair.Value.GetComponent<AchievementWidget>().SetAchievedBefore();
			}
			else if (data == null)
			{
				keyValuePair.Value.GetComponent<AchievementWidget>().SetNeverAchieved();
			}
			else
			{
				keyValuePair.Value.GetComponent<AchievementWidget>().SetNotAchieved();
			}
		}
		if (newlyAchieved != null && newlyAchieved.Length != 0)
		{
			base.StartCoroutine(this.ShowAchievementVeil());
			base.StartCoroutine(this.ClearAchievementVeil(num3 + (float)num * num2));
			return;
		}
		this.InstantClearAchievementVeils();
	}

	// Token: 0x06005BDE RID: 23518 RVA: 0x00217E30 File Offset: 0x00216030
	private void DisplayInfoBlock(RetiredColonyData data, GameObject container)
	{
		container.GetComponent<HierarchyReferences>().GetReference<LocText>("ColonyNameLabel").SetText(data.colonyName);
		container.GetComponent<HierarchyReferences>().GetReference<LocText>("CycleCountLabel").SetText(string.Format(UI.RETIRED_COLONY_INFO_SCREEN.CYCLE_COUNT, data.cycleCount.ToString()));
	}

	// Token: 0x06005BDF RID: 23519 RVA: 0x00217E8C File Offset: 0x0021608C
	private void CorrectTimelapseImageSize(Sprite sprite)
	{
		Vector2 sizeDelta = this.slideshow.transform.parent.GetComponent<RectTransform>().sizeDelta;
		Vector2 fittedSize = this.slideshow.GetFittedSize(sprite, sizeDelta.x, sizeDelta.y);
		LayoutElement component = this.slideshow.GetComponent<LayoutElement>();
		if (fittedSize.y > component.preferredHeight)
		{
			component.minHeight = component.preferredHeight / (fittedSize.y / fittedSize.x);
			component.minHeight = component.preferredHeight;
			return;
		}
		component.minWidth = (component.preferredWidth = fittedSize.x);
		component.minHeight = (component.preferredHeight = fittedSize.y);
	}

	// Token: 0x06005BE0 RID: 23520 RVA: 0x00217F38 File Offset: 0x00216138
	private void DisplayTimelapse(RetiredColonyData data, GameObject container)
	{
		container.GetComponent<HierarchyReferences>().GetReference<LocText>("Title").SetText(UI.RETIRED_COLONY_INFO_SCREEN.TITLES.TIMELAPSE);
		RectTransform reference = container.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Worlds");
		this.DisplayWorlds(data, reference.gameObject);
		RectTransform reference2 = container.GetComponent<HierarchyReferences>().GetReference<RectTransform>("PlayIcon");
		this.slideshow = container.GetComponent<HierarchyReferences>().GetReference<Slideshow>("Slideshow");
		this.slideshow.updateType = SlideshowUpdateType.loadOnDemand;
		this.slideshow.SetPaused(true);
		this.slideshow.onBeforePlay = delegate
		{
			this.LoadSlideshow(data);
		};
		this.slideshow.onEndingPlay = delegate
		{
			this.LoadScreenshot(data, this.focusedWorld);
		};
		if (!this.LoadScreenshot(data, this.focusedWorld))
		{
			this.slideshow.gameObject.SetActive(false);
			reference2.gameObject.SetActive(false);
			return;
		}
		this.slideshow.gameObject.SetActive(true);
		reference2.gameObject.SetActive(true);
	}

	// Token: 0x06005BE1 RID: 23521 RVA: 0x00218058 File Offset: 0x00216258
	private void DisplayDuplicants(RetiredColonyData data, GameObject container, int range_min = -1, int range_max = -1)
	{
		for (int i = container.transform.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.DestroyImmediate(container.transform.GetChild(i).gameObject);
		}
		for (int j = 0; j < data.Duplicants.Length; j++)
		{
			if (j < range_min || (j > range_max && range_max != -1))
			{
				new GameObject().transform.SetParent(container.transform);
			}
			else
			{
				RetiredColonyData.RetiredDuplicantData retiredDuplicantData = data.Duplicants[j];
				GameObject gameObject = global::Util.KInstantiateUI(this.duplicantPrefab, container, true);
				HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("NameLabel").SetText(retiredDuplicantData.name);
				component.GetReference<LocText>("AgeLabel").SetText(string.Format(UI.RETIRED_COLONY_INFO_SCREEN.DUPLICANT_AGE, retiredDuplicantData.age.ToString()));
				component.GetReference<LocText>("SkillLabel").SetText(string.Format(UI.RETIRED_COLONY_INFO_SCREEN.SKILL_LEVEL, retiredDuplicantData.skillPointsGained.ToString()));
				SymbolOverrideController reference = component.GetReference<SymbolOverrideController>("SymbolOverrideController");
				reference.RemoveAllSymbolOverrides(0);
				KBatchedAnimController componentInChildren = gameObject.GetComponentInChildren<KBatchedAnimController>();
				componentInChildren.SetSymbolVisiblity("snapTo_neck", false);
				componentInChildren.SetSymbolVisiblity("snapTo_goggles", false);
				componentInChildren.SetSymbolVisiblity("snapTo_hat", false);
				componentInChildren.SetSymbolVisiblity("snapTo_headfx", false);
				componentInChildren.SetSymbolVisiblity("snapTo_hat_hair", false);
				foreach (KeyValuePair<string, string> keyValuePair in retiredDuplicantData.accessories)
				{
					if (Db.Get().Accessories.Exists(keyValuePair.Value))
					{
						KAnim.Build.Symbol symbol = Db.Get().Accessories.Get(keyValuePair.Value).symbol;
						AccessorySlot accessorySlot = Db.Get().AccessorySlots.Get(keyValuePair.Key);
						reference.AddSymbolOverride(accessorySlot.targetSymbolId, symbol, 0);
						gameObject.GetComponentInChildren<KBatchedAnimController>().SetSymbolVisiblity(keyValuePair.Key, true);
					}
				}
				reference.ApplyOverrides();
			}
		}
		base.StartCoroutine(this.ActivatePortraitsWhenReady(container));
	}

	// Token: 0x06005BE2 RID: 23522 RVA: 0x002182A4 File Offset: 0x002164A4
	private IEnumerator ActivatePortraitsWhenReady(GameObject container)
	{
		yield return 0;
		if (container == null)
		{
			global::Debug.LogError("RetiredColonyInfoScreen minion container is null");
		}
		else
		{
			for (int i = 0; i < container.transform.childCount; i++)
			{
				KBatchedAnimController componentInChildren = container.transform.GetChild(i).GetComponentInChildren<KBatchedAnimController>();
				if (componentInChildren != null)
				{
					componentInChildren.transform.localScale = Vector3.one;
				}
			}
		}
		yield break;
	}

	// Token: 0x06005BE3 RID: 23523 RVA: 0x002182B4 File Offset: 0x002164B4
	private void DisplayBuildings(RetiredColonyData data, GameObject container)
	{
		for (int i = container.transform.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(container.transform.GetChild(i).gameObject);
		}
		data.buildings.Sort(delegate(global::Tuple<string, int> a, global::Tuple<string, int> b)
		{
			if (a.second > b.second)
			{
				return 1;
			}
			if (a.second == b.second)
			{
				return 0;
			}
			return -1;
		});
		data.buildings.Reverse();
		foreach (global::Tuple<string, int> tuple in data.buildings)
		{
			GameObject prefab = Assets.GetPrefab(tuple.first);
			if (!(prefab == null))
			{
				HierarchyReferences component = global::Util.KInstantiateUI(this.buildingPrefab, container, true).GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("NameLabel").SetText(GameUtil.ApplyBoldString(prefab.GetProperName()));
				component.GetReference<LocText>("CountLabel").SetText(string.Format(UI.RETIRED_COLONY_INFO_SCREEN.BUILDING_COUNT, tuple.second.ToString()));
				global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(prefab, "ui", false);
				component.GetReference<Image>("Portrait").sprite = uisprite.first;
			}
		}
	}

	// Token: 0x06005BE4 RID: 23524 RVA: 0x00218400 File Offset: 0x00216600
	private void DisplayWorlds(RetiredColonyData data, GameObject container)
	{
		container.SetActive(data.worldIdentities.Count > 0);
		for (int i = container.transform.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(container.transform.GetChild(i).gameObject);
		}
		if (data.worldIdentities.Count <= 0)
		{
			return;
		}
		using (Dictionary<string, string>.Enumerator enumerator = data.worldIdentities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, string> worldPair = enumerator.Current;
				GameObject gameObject = global::Util.KInstantiateUI(this.worldPrefab, container, true);
				HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
				ProcGen.World worldData = SettingsCache.worlds.GetWorldData(worldPair.Value);
				Sprite sprite = ((worldData != null) ? ColonyDestinationAsteroidBeltData.GetUISprite(worldData.asteroidIcon) : null);
				if (sprite != null)
				{
					component.GetReference<Image>("Portrait").sprite = sprite;
				}
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.focusedWorld = worldPair.Key;
					this.LoadScreenshot(data, this.focusedWorld);
				};
			}
		}
	}

	// Token: 0x06005BE5 RID: 23525 RVA: 0x0021854C File Offset: 0x0021674C
	private IEnumerator ComputeSizeStatGrid()
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		GridLayoutGroup component = this.statsContainer.GetComponent<GridLayoutGroup>();
		component.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		component.constraintCount = ((Screen.width < 1920) ? 2 : 3);
		yield return SequenceUtil.WaitForEndOfFrame;
		float num = base.gameObject.rectTransform().rect.width - this.explorerRoot.transform.parent.rectTransform().rect.width - 50f;
		num = Mathf.Min(830f, num);
		this.achievementsSection.GetComponent<LayoutElement>().preferredWidth = num;
		yield break;
	}

	// Token: 0x06005BE6 RID: 23526 RVA: 0x0021855B File Offset: 0x0021675B
	private IEnumerator ComputeSizeExplorerGrid()
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		GridLayoutGroup component = this.explorerGrid.GetComponent<GridLayoutGroup>();
		component.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		component.constraintCount = ((Screen.width < 1920) ? 2 : 3);
		yield return SequenceUtil.WaitForEndOfFrame;
		float num = base.gameObject.rectTransform().rect.width - this.explorerRoot.transform.parent.rectTransform().rect.width - 50f;
		num = Mathf.Min(830f, num);
		this.achievementsSection.GetComponent<LayoutElement>().preferredWidth = num;
		yield break;
	}

	// Token: 0x06005BE7 RID: 23527 RVA: 0x0021856C File Offset: 0x0021676C
	private void DisplayStatistics(RetiredColonyData data)
	{
		GameObject gameObject = global::Util.KInstantiateUI(this.specialMediaBlock, this.statsContainer, true);
		this.activeColonyWidgetContainers.Add(gameObject);
		this.activeColonyWidgets.Add("timelapse", gameObject);
		this.DisplayTimelapse(data, gameObject);
		GameObject duplicantBlock = global::Util.KInstantiateUI(this.tallFeatureBlock, this.statsContainer, true);
		this.activeColonyWidgetContainers.Add(duplicantBlock);
		this.activeColonyWidgets.Add("duplicants", duplicantBlock);
		duplicantBlock.GetComponent<HierarchyReferences>().GetReference<LocText>("Title").SetText(UI.RETIRED_COLONY_INFO_SCREEN.TITLES.DUPLICANTS);
		PageView pageView = duplicantBlock.GetComponentInChildren<PageView>();
		pageView.OnChangePage = delegate(int page)
		{
			this.DisplayDuplicants(data, duplicantBlock.GetComponent<HierarchyReferences>().GetReference("Content").gameObject, page * pageView.ChildrenPerPage, (page + 1) * pageView.ChildrenPerPage);
		};
		this.DisplayDuplicants(data, duplicantBlock.GetComponent<HierarchyReferences>().GetReference("Content").gameObject, -1, -1);
		GameObject gameObject2 = global::Util.KInstantiateUI(this.tallFeatureBlock, this.statsContainer, true);
		this.activeColonyWidgetContainers.Add(gameObject2);
		this.activeColonyWidgets.Add("buildings", gameObject2);
		gameObject2.GetComponent<HierarchyReferences>().GetReference<LocText>("Title").SetText(UI.RETIRED_COLONY_INFO_SCREEN.TITLES.BUILDINGS);
		this.DisplayBuildings(data, gameObject2.GetComponent<HierarchyReferences>().GetReference("Content").gameObject);
		int num = 2;
		for (int i = 0; i < data.Stats.Length; i += num)
		{
			GameObject gameObject3 = global::Util.KInstantiateUI(this.standardStatBlock, this.statsContainer, true);
			this.activeColonyWidgetContainers.Add(gameObject3);
			for (int j = 0; j < num; j++)
			{
				if (i + j <= data.Stats.Length - 1)
				{
					RetiredColonyData.RetiredColonyStatistic retiredColonyStatistic = data.Stats[i + j];
					this.ConfigureGraph(this.GetStatistic(retiredColonyStatistic.id, data), gameObject3);
				}
			}
		}
		base.StartCoroutine(this.ComputeSizeStatGrid());
	}

	// Token: 0x06005BE8 RID: 23528 RVA: 0x00218790 File Offset: 0x00216990
	private void ConfigureGraph(RetiredColonyData.RetiredColonyStatistic statistic, GameObject layoutBlockGameObject)
	{
		GameObject gameObject = global::Util.KInstantiateUI(this.lineGraphPrefab, layoutBlockGameObject, true);
		this.activeColonyWidgets.Add(statistic.name, gameObject);
		GraphBase componentInChildren = gameObject.GetComponentInChildren<GraphBase>();
		componentInChildren.graphName = statistic.name;
		componentInChildren.label_title.SetText(componentInChildren.graphName);
		componentInChildren.axis_x.name = statistic.nameX;
		componentInChildren.axis_y.name = statistic.nameY;
		componentInChildren.label_x.SetText(componentInChildren.axis_x.name);
		componentInChildren.label_y.SetText(componentInChildren.axis_y.name);
		LineLayer componentInChildren2 = gameObject.GetComponentInChildren<LineLayer>();
		componentInChildren.axis_y.min_value = 0f;
		componentInChildren.axis_y.max_value = statistic.GetByMaxValue().second * 1.2f;
		if (float.IsNaN(componentInChildren.axis_y.max_value))
		{
			componentInChildren.axis_y.max_value = 1f;
		}
		componentInChildren.axis_x.min_value = 0f;
		componentInChildren.axis_x.max_value = statistic.GetByMaxKey().first;
		componentInChildren.axis_x.guide_frequency = (componentInChildren.axis_x.max_value - componentInChildren.axis_x.min_value) / 10f;
		componentInChildren.axis_y.guide_frequency = (componentInChildren.axis_y.max_value - componentInChildren.axis_y.min_value) / 10f;
		componentInChildren.RefreshGuides();
		global::Tuple<float, float>[] value = statistic.value;
		GraphedLine graphedLine = componentInChildren2.NewLine(value, statistic.id);
		if (this.statColors.ContainsKey(statistic.id))
		{
			componentInChildren2.line_formatting[componentInChildren2.line_formatting.Length - 1].color = this.statColors[statistic.id];
		}
		graphedLine.line_renderer.color = componentInChildren2.line_formatting[componentInChildren2.line_formatting.Length - 1].color;
	}

	// Token: 0x06005BE9 RID: 23529 RVA: 0x00218978 File Offset: 0x00216B78
	private RetiredColonyData.RetiredColonyStatistic GetStatistic(string id, RetiredColonyData data)
	{
		foreach (RetiredColonyData.RetiredColonyStatistic retiredColonyStatistic in data.Stats)
		{
			if (retiredColonyStatistic.id == id)
			{
				return retiredColonyStatistic;
			}
		}
		return null;
	}

	// Token: 0x06005BEA RID: 23530 RVA: 0x002189B0 File Offset: 0x00216BB0
	private void ToggleExplorer(bool active)
	{
		if (active && Game.Instance == null)
		{
			WorldGen.LoadSettings(false);
		}
		this.ConfigButtons();
		this.explorerRoot.SetActive(active);
		this.colonyDataRoot.SetActive(!active);
		if (!this.explorerGridConfigured)
		{
			this.explorerGridConfigured = true;
			base.StartCoroutine(this.ComputeSizeExplorerGrid());
		}
		this.explorerHeaderContainer.SetActive(active);
		this.colonyHeaderContainer.SetActive(!active);
		if (active)
		{
			this.colonyDataRoot.transform.parent.rectTransform().SetPosition(new Vector3(this.colonyDataRoot.transform.parent.rectTransform().position.x, 0f, 0f));
		}
		this.UpdateAchievementData(null, null);
		this.explorerSearch.text = "";
	}

	// Token: 0x06005BEB RID: 23531 RVA: 0x00218A90 File Offset: 0x00216C90
	private void LoadExplorer()
	{
		if (SaveGame.Instance != null)
		{
			return;
		}
		this.ToggleExplorer(true);
		this.retiredColonyData = RetireColonyUtility.LoadRetiredColonies(false);
		RetiredColonyData[] array = this.retiredColonyData;
		for (int i = 0; i < array.Length; i++)
		{
			RetiredColonyData retiredColonyData = array[i];
			RetiredColonyData data = retiredColonyData;
			GameObject gameObject = global::Util.KInstantiateUI(this.colonyButtonPrefab, this.explorerGrid, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			Sprite sprite = RetireColonyUtility.LoadRetiredColonyPreview(RetireColonyUtility.StripInvalidCharacters(data.colonyName), data.startWorld);
			Image reference = component.GetReference<Image>("ColonyImage");
			RectTransform reference2 = component.GetReference<RectTransform>("PreviewUnavailableText");
			if (sprite != null)
			{
				reference.enabled = true;
				reference.sprite = sprite;
				reference2.gameObject.SetActive(false);
			}
			else
			{
				reference.enabled = false;
				reference2.gameObject.SetActive(true);
			}
			component.GetReference<LocText>("ColonyNameLabel").SetText(retiredColonyData.colonyName);
			component.GetReference<LocText>("CycleCountLabel").SetText(string.Format(UI.RETIRED_COLONY_INFO_SCREEN.CYCLE_COUNT, retiredColonyData.cycleCount.ToString()));
			component.GetReference<LocText>("DateLabel").SetText(retiredColonyData.date);
			gameObject.GetComponent<KButton>().onClick += delegate
			{
				this.LoadColony(data);
			};
			string text = retiredColonyData.colonyName;
			int num = 0;
			while (this.explorerColonyWidgets.ContainsKey(text))
			{
				num++;
				text = retiredColonyData.colonyName + "_" + num.ToString();
			}
			this.explorerColonyWidgets.Add(text, gameObject);
		}
	}

	// Token: 0x06005BEC RID: 23532 RVA: 0x00218C44 File Offset: 0x00216E44
	private void FilterExplorer(string search)
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.explorerColonyWidgets)
		{
			if (string.IsNullOrEmpty(search) || keyValuePair.Key.ToUpper().Contains(search.ToUpper()))
			{
				keyValuePair.Value.SetActive(true);
			}
			else
			{
				keyValuePair.Value.SetActive(false);
			}
		}
	}

	// Token: 0x06005BED RID: 23533 RVA: 0x00218CD0 File Offset: 0x00216ED0
	private void FilterColonyData(string search)
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.activeColonyWidgets)
		{
			if (string.IsNullOrEmpty(search) || keyValuePair.Key.ToUpper().Contains(search.ToUpper()))
			{
				keyValuePair.Value.SetActive(true);
			}
			else
			{
				keyValuePair.Value.SetActive(false);
			}
		}
	}

	// Token: 0x06005BEE RID: 23534 RVA: 0x00218D5C File Offset: 0x00216F5C
	private void FilterAchievements(string search)
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.achievementEntries)
		{
			if (string.IsNullOrEmpty(search) || Db.Get().ColonyAchievements.Get(keyValuePair.Key).Name.ToUpper().Contains(search.ToUpper()))
			{
				keyValuePair.Value.SetActive(true);
			}
			else
			{
				keyValuePair.Value.SetActive(false);
			}
		}
	}

	// Token: 0x04003E9A RID: 16026
	public static RetiredColonyInfoScreen Instance;

	// Token: 0x04003E9B RID: 16027
	private bool wasPixelPerfect;

	// Token: 0x04003E9C RID: 16028
	[Header("Screen")]
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003E9D RID: 16029
	[Header("Header References")]
	[SerializeField]
	private GameObject explorerHeaderContainer;

	// Token: 0x04003E9E RID: 16030
	[SerializeField]
	private GameObject colonyHeaderContainer;

	// Token: 0x04003E9F RID: 16031
	[SerializeField]
	private LocText colonyName;

	// Token: 0x04003EA0 RID: 16032
	[SerializeField]
	private LocText cycleCount;

	// Token: 0x04003EA1 RID: 16033
	[Header("Timelapse References")]
	[SerializeField]
	private Slideshow slideshow;

	// Token: 0x04003EA2 RID: 16034
	[SerializeField]
	private GameObject worldPrefab;

	// Token: 0x04003EA3 RID: 16035
	private string focusedWorld;

	// Token: 0x04003EA4 RID: 16036
	private string[] currentSlideshowFiles = new string[0];

	// Token: 0x04003EA5 RID: 16037
	[Header("Main Layout")]
	[SerializeField]
	private GameObject coloniesSection;

	// Token: 0x04003EA6 RID: 16038
	[SerializeField]
	private GameObject achievementsSection;

	// Token: 0x04003EA7 RID: 16039
	[Header("Achievement References")]
	[SerializeField]
	private GameObject achievementsContainer;

	// Token: 0x04003EA8 RID: 16040
	[SerializeField]
	private GameObject achievementsPrefab;

	// Token: 0x04003EA9 RID: 16041
	[SerializeField]
	private GameObject victoryAchievementsPrefab;

	// Token: 0x04003EAA RID: 16042
	[SerializeField]
	private KInputTextField achievementSearch;

	// Token: 0x04003EAB RID: 16043
	[SerializeField]
	private KButton clearAchievementSearchButton;

	// Token: 0x04003EAC RID: 16044
	[SerializeField]
	private GameObject[] achievementVeils;

	// Token: 0x04003EAD RID: 16045
	[Header("Duplicant References")]
	[SerializeField]
	private GameObject duplicantPrefab;

	// Token: 0x04003EAE RID: 16046
	[Header("Building References")]
	[SerializeField]
	private GameObject buildingPrefab;

	// Token: 0x04003EAF RID: 16047
	[Header("Colony Stat References")]
	[SerializeField]
	private GameObject statsContainer;

	// Token: 0x04003EB0 RID: 16048
	[SerializeField]
	private GameObject specialMediaBlock;

	// Token: 0x04003EB1 RID: 16049
	[SerializeField]
	private GameObject tallFeatureBlock;

	// Token: 0x04003EB2 RID: 16050
	[SerializeField]
	private GameObject standardStatBlock;

	// Token: 0x04003EB3 RID: 16051
	[SerializeField]
	private GameObject lineGraphPrefab;

	// Token: 0x04003EB4 RID: 16052
	public RetiredColonyData[] retiredColonyData;

	// Token: 0x04003EB5 RID: 16053
	[Header("Explorer References")]
	[SerializeField]
	private GameObject colonyScroll;

	// Token: 0x04003EB6 RID: 16054
	[SerializeField]
	private GameObject explorerRoot;

	// Token: 0x04003EB7 RID: 16055
	[SerializeField]
	private GameObject explorerGrid;

	// Token: 0x04003EB8 RID: 16056
	[SerializeField]
	private GameObject colonyDataRoot;

	// Token: 0x04003EB9 RID: 16057
	[SerializeField]
	private GameObject colonyButtonPrefab;

	// Token: 0x04003EBA RID: 16058
	[SerializeField]
	private KInputTextField explorerSearch;

	// Token: 0x04003EBB RID: 16059
	[SerializeField]
	private KButton clearExplorerSearchButton;

	// Token: 0x04003EBC RID: 16060
	[Header("Navigation Buttons")]
	[SerializeField]
	private KButton closeScreenButton;

	// Token: 0x04003EBD RID: 16061
	[SerializeField]
	private KButton viewOtherColoniesButton;

	// Token: 0x04003EBE RID: 16062
	[SerializeField]
	private KButton quitToMainMenuButton;

	// Token: 0x04003EBF RID: 16063
	[SerializeField]
	private GameObject disabledPlatformUnlocks;

	// Token: 0x04003EC0 RID: 16064
	private bool explorerGridConfigured;

	// Token: 0x04003EC1 RID: 16065
	private Dictionary<string, GameObject> achievementEntries = new Dictionary<string, GameObject>();

	// Token: 0x04003EC2 RID: 16066
	private List<GameObject> activeColonyWidgetContainers = new List<GameObject>();

	// Token: 0x04003EC3 RID: 16067
	private Dictionary<string, GameObject> activeColonyWidgets = new Dictionary<string, GameObject>();

	// Token: 0x04003EC4 RID: 16068
	private const float maxAchievementWidth = 830f;

	// Token: 0x04003EC5 RID: 16069
	private Canvas canvasRef;

	// Token: 0x04003EC6 RID: 16070
	private Dictionary<string, Color> statColors = new Dictionary<string, Color>
	{
		{
			RetiredColonyData.DataIDs.OxygenProduced,
			new Color(0.17f, 0.91f, 0.91f, 1f)
		},
		{
			RetiredColonyData.DataIDs.OxygenConsumed,
			new Color(0.17f, 0.91f, 0.91f, 1f)
		},
		{
			RetiredColonyData.DataIDs.CaloriesProduced,
			new Color(0.24f, 0.49f, 0.32f, 1f)
		},
		{
			RetiredColonyData.DataIDs.CaloriesRemoved,
			new Color(0.24f, 0.49f, 0.32f, 1f)
		},
		{
			RetiredColonyData.DataIDs.PowerProduced,
			new Color(0.98f, 0.69f, 0.23f, 1f)
		},
		{
			RetiredColonyData.DataIDs.PowerWasted,
			new Color(0.82f, 0.3f, 0.35f, 1f)
		},
		{
			RetiredColonyData.DataIDs.WorkTime,
			new Color(0.99f, 0.51f, 0.28f, 1f)
		},
		{
			RetiredColonyData.DataIDs.TravelTime,
			new Color(0.55f, 0.55f, 0.75f, 1f)
		},
		{
			RetiredColonyData.DataIDs.AverageWorkTime,
			new Color(0.99f, 0.51f, 0.28f, 1f)
		},
		{
			RetiredColonyData.DataIDs.AverageTravelTime,
			new Color(0.55f, 0.55f, 0.75f, 1f)
		},
		{
			RetiredColonyData.DataIDs.LiveDuplicants,
			new Color(0.98f, 0.69f, 0.23f, 1f)
		},
		{
			RetiredColonyData.DataIDs.RocketsInFlight,
			new Color(0.9f, 0.9f, 0.16f, 1f)
		},
		{
			RetiredColonyData.DataIDs.AverageStressCreated,
			new Color(0.8f, 0.32f, 0.33f, 1f)
		},
		{
			RetiredColonyData.DataIDs.AverageStressRemoved,
			new Color(0.8f, 0.32f, 0.33f, 1f)
		},
		{
			RetiredColonyData.DataIDs.AverageGerms,
			new Color(0.68f, 0.79f, 0.18f, 1f)
		},
		{
			RetiredColonyData.DataIDs.DomesticatedCritters,
			new Color(0.62f, 0.31f, 0.47f, 1f)
		},
		{
			RetiredColonyData.DataIDs.WildCritters,
			new Color(0.62f, 0.31f, 0.47f, 1f)
		}
	};

	// Token: 0x04003EC7 RID: 16071
	private Dictionary<string, GameObject> explorerColonyWidgets = new Dictionary<string, GameObject>();
}
