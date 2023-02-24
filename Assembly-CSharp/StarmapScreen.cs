using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using FMOD.Studio;
using STRINGS;
using TMPro;
using TUNING;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000C06 RID: 3078
public class StarmapScreen : KModalScreen
{
	// Token: 0x06006160 RID: 24928 RVA: 0x0023CEC4 File Offset: 0x0023B0C4
	public override float GetSortKey()
	{
		if (base.isEditing)
		{
			return 50f;
		}
		return 20f;
	}

	// Token: 0x06006161 RID: 24929 RVA: 0x0023CED9 File Offset: 0x0023B0D9
	public static void DestroyInstance()
	{
		StarmapScreen.Instance = null;
	}

	// Token: 0x06006162 RID: 24930 RVA: 0x0023CEE4 File Offset: 0x0023B0E4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
		this.rocketDetailsStatus = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.rocketDetailsContainer);
		this.rocketDetailsStatus.SetTitle(UI.STARMAP.LISTTITLES.MISSIONSTATUS);
		this.rocketDetailsStatus.SetIcon(this.rocketDetailsStatusIcon);
		this.rocketDetailsStatus.gameObject.name = "rocketDetailsStatus";
		this.rocketDetailsChecklist = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.rocketDetailsContainer);
		this.rocketDetailsChecklist.SetTitle(UI.STARMAP.LISTTITLES.LAUNCHCHECKLIST);
		this.rocketDetailsChecklist.SetIcon(this.rocketDetailsChecklistIcon);
		this.rocketDetailsChecklist.gameObject.name = "rocketDetailsChecklist";
		this.rocketDetailsRange = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.rocketDetailsContainer);
		this.rocketDetailsRange.SetTitle(UI.STARMAP.LISTTITLES.MAXRANGE);
		this.rocketDetailsRange.SetIcon(this.rocketDetailsRangeIcon);
		this.rocketDetailsRange.gameObject.name = "rocketDetailsRange";
		this.rocketDetailsMass = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.rocketDetailsContainer);
		this.rocketDetailsMass.SetTitle(UI.STARMAP.LISTTITLES.MASS);
		this.rocketDetailsMass.SetIcon(this.rocketDetailsMassIcon);
		this.rocketDetailsMass.gameObject.name = "rocketDetailsMass";
		this.rocketThrustWidget = UnityEngine.Object.Instantiate<RocketThrustWidget>(this.rocketThrustWidget, this.rocketDetailsContainer);
		this.rocketDetailsStorage = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.rocketDetailsContainer);
		this.rocketDetailsStorage.SetTitle(UI.STARMAP.LISTTITLES.STORAGE);
		this.rocketDetailsStorage.SetIcon(this.rocketDetailsStorageIcon);
		this.rocketDetailsStorage.gameObject.name = "rocketDetailsStorage";
		this.rocketDetailsFuel = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.rocketDetailsContainer);
		this.rocketDetailsFuel.SetTitle(UI.STARMAP.LISTTITLES.FUEL);
		this.rocketDetailsFuel.SetIcon(this.rocketDetailsFuelIcon);
		this.rocketDetailsFuel.gameObject.name = "rocketDetailsFuel";
		this.rocketDetailsOxidizer = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.rocketDetailsContainer);
		this.rocketDetailsOxidizer.SetTitle(UI.STARMAP.LISTTITLES.OXIDIZER);
		this.rocketDetailsOxidizer.SetIcon(this.rocketDetailsOxidizerIcon);
		this.rocketDetailsOxidizer.gameObject.name = "rocketDetailsOxidizer";
		this.rocketDetailsDupes = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.rocketDetailsContainer);
		this.rocketDetailsDupes.SetTitle(UI.STARMAP.LISTTITLES.PASSENGERS);
		this.rocketDetailsDupes.SetIcon(this.rocketDetailsDupesIcon);
		this.rocketDetailsDupes.gameObject.name = "rocketDetailsDupes";
		this.destinationDetailsAnalysis = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.destinationDetailsContainer);
		this.destinationDetailsAnalysis.SetTitle(UI.STARMAP.LISTTITLES.ANALYSIS);
		this.destinationDetailsAnalysis.SetIcon(this.destinationDetailsAnalysisIcon);
		this.destinationDetailsAnalysis.gameObject.name = "destinationDetailsAnalysis";
		this.destinationDetailsAnalysis.SetDescription(string.Format(UI.STARMAP.ANALYSIS_DESCRIPTION, 0));
		this.destinationAnalysisProgressBar = UnityEngine.Object.Instantiate<GameObject>(this.progressBarPrefab.gameObject, this.destinationDetailsContainer).GetComponent<GenericUIProgressBar>();
		this.destinationAnalysisProgressBar.SetMaxValue((float)ROCKETRY.DESTINATION_ANALYSIS.COMPLETE);
		this.destinationDetailsResearch = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.destinationDetailsContainer);
		this.destinationDetailsResearch.SetTitle(UI.STARMAP.LISTTITLES.RESEARCH);
		this.destinationDetailsResearch.SetIcon(this.destinationDetailsResearchIcon);
		this.destinationDetailsResearch.gameObject.name = "destinationDetailsResearch";
		this.destinationDetailsResearch.SetDescription(string.Format(UI.STARMAP.RESEARCH_DESCRIPTION, 0));
		this.destinationDetailsMass = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.destinationDetailsContainer);
		this.destinationDetailsMass.SetTitle(UI.STARMAP.LISTTITLES.DESTINATION_MASS);
		this.destinationDetailsMass.SetIcon(this.destinationDetailsMassIcon);
		this.destinationDetailsMass.gameObject.name = "destinationDetailsMass";
		this.destinationDetailsComposition = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.destinationDetailsContainer);
		this.destinationDetailsComposition.SetTitle(UI.STARMAP.LISTTITLES.WORLDCOMPOSITION);
		this.destinationDetailsComposition.SetIcon(this.destinationDetailsCompositionIcon);
		this.destinationDetailsComposition.gameObject.name = "destinationDetailsComposition";
		this.destinationDetailsResources = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.destinationDetailsContainer);
		this.destinationDetailsResources.SetTitle(UI.STARMAP.LISTTITLES.RESOURCES);
		this.destinationDetailsResources.SetIcon(this.destinationDetailsResourcesIcon);
		this.destinationDetailsResources.gameObject.name = "destinationDetailsResources";
		this.destinationDetailsArtifacts = UnityEngine.Object.Instantiate<BreakdownList>(this.breakdownListPrefab, this.destinationDetailsContainer);
		this.destinationDetailsArtifacts.SetTitle(UI.STARMAP.LISTTITLES.ARTIFACTS);
		this.destinationDetailsArtifacts.SetIcon(this.destinationDetailsArtifactsIcon);
		this.destinationDetailsArtifacts.gameObject.name = "destinationDetailsArtifacts";
		this.LoadPlanets();
		this.selectionUpdateHandle = Game.Instance.Subscribe(-1503271301, new Action<object>(this.OnSelectableChanged));
		this.titleBarLabel.text = UI.STARMAP.TITLE;
		this.button.onClick += delegate
		{
			ManagementMenu.Instance.ToggleStarmap();
		};
		this.launchButton.play_sound_on_click = false;
		MultiToggle multiToggle = this.launchButton;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			if (this.currentLaunchConditionManager != null && this.selectedDestination != null)
			{
				KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Click", false));
				this.LaunchRocket(this.currentLaunchConditionManager);
				return;
			}
			KFMOD.PlayUISound(GlobalAssets.GetSound("Negative", false));
		}));
		this.launchButton.ChangeState(1);
		MultiToggle multiToggle2 = this.showRocketsButton;
		multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(delegate
		{
			this.OnSelectableChanged(null);
		}));
		this.SelectDestination(null);
		SpacecraftManager.instance.Subscribe(532901469, delegate(object data)
		{
			this.RefreshAnalyzeButton();
			this.UpdateDestinationStates();
		});
	}

	// Token: 0x06006163 RID: 24931 RVA: 0x0023D4F2 File Offset: 0x0023B6F2
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.selectionUpdateHandle != -1)
		{
			Game.Instance.Unsubscribe(this.selectionUpdateHandle);
		}
		base.StopAllCoroutines();
	}

	// Token: 0x06006164 RID: 24932 RVA: 0x0023D51C File Offset: 0x0023B71C
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().MENUStarmapSnapshot);
			MusicManager.instance.PlaySong("Music_Starmap", false);
			this.UpdateDestinationStates();
			this.Refresh(null);
		}
		else
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MENUStarmapSnapshot, STOP_MODE.ALLOWFADEOUT);
			MusicManager.instance.StopSong("Music_Starmap", true, STOP_MODE.ALLOWFADEOUT);
		}
		this.OnSelectableChanged((SelectTool.Instance.selected == null) ? null : SelectTool.Instance.selected.gameObject);
		this.forceScrollDown = true;
	}

	// Token: 0x06006165 RID: 24933 RVA: 0x0023D5C0 File Offset: 0x0023B7C0
	private void UpdateDestinationStates()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 1;
		foreach (SpaceDestination spaceDestination in SpacecraftManager.instance.destinations)
		{
			num = Mathf.Max(num, spaceDestination.OneBasedDistance);
			if (spaceDestination.AnalysisState() == SpacecraftManager.DestinationAnalysisState.Complete)
			{
				num2 = Mathf.Max(num2, spaceDestination.OneBasedDistance);
			}
		}
		for (int i = num2; i < num; i++)
		{
			bool flag = false;
			using (List<SpaceDestination>.Enumerator enumerator = SpacecraftManager.instance.destinations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.distance == i)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				break;
			}
			num3++;
		}
		using (Dictionary<SpaceDestination, StarmapPlanet>.Enumerator enumerator2 = this.planetWidgets.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<SpaceDestination, StarmapPlanet> KVP = enumerator2.Current;
				SpaceDestination key = KVP.Key;
				StarmapPlanet planet = KVP.Value;
				Color color = new Color(0.25f, 0.25f, 0.25f, 0.5f);
				Color color2 = new Color(0.75f, 0.75f, 0.75f, 0.75f);
				if (KVP.Key.distance >= num2 + num3)
				{
					planet.SetUnknownBGActive(false, Color.white);
					planet.SetSprite(Assets.GetSprite("unknown_far"), color);
				}
				else
				{
					planet.SetAnalysisActive(SpacecraftManager.instance.GetStarmapAnalysisDestinationID() == KVP.Key.id);
					bool flag2 = SpacecraftManager.instance.GetDestinationAnalysisState(key) == SpacecraftManager.DestinationAnalysisState.Complete;
					SpaceDestinationType destinationType = key.GetDestinationType();
					planet.SetLabel(flag2 ? (destinationType.Name + "\n<color=#979798> " + GameUtil.GetFormattedDistance((float)KVP.Key.OneBasedDistance * 10000f * 1000f) + "</color>") : (UI.STARMAP.UNKNOWN_DESTINATION + "\n" + string.Format(UI.STARMAP.ANALYSIS_AMOUNT.text, GameUtil.GetFormattedPercent(100f * (SpacecraftManager.instance.GetDestinationAnalysisScore(KVP.Key) / (float)ROCKETRY.DESTINATION_ANALYSIS.COMPLETE), GameUtil.TimeSlice.None))));
					planet.SetSprite(flag2 ? Assets.GetSprite(destinationType.spriteName) : Assets.GetSprite("unknown"), flag2 ? Color.white : color2);
					planet.SetUnknownBGActive(SpacecraftManager.instance.GetDestinationAnalysisState(KVP.Key) != SpacecraftManager.DestinationAnalysisState.Complete, color2);
					planet.SetFillAmount(SpacecraftManager.instance.GetDestinationAnalysisScore(KVP.Key) / (float)ROCKETRY.DESTINATION_ANALYSIS.COMPLETE);
					List<int> spacecraftsForDestination = SpacecraftManager.instance.GetSpacecraftsForDestination(key);
					planet.SetRocketIcons(spacecraftsForDestination.Count, this.rocketIconPrefab);
					bool flag3 = this.currentLaunchConditionManager != null && key == SpacecraftManager.instance.GetSpacecraftDestination(this.currentLaunchConditionManager);
					planet.ShowAsCurrentRocketDestination(flag3);
					planet.SetOnClick(delegate
					{
						if (this.currentLaunchConditionManager == null)
						{
							this.SelectDestination(KVP.Key);
							return;
						}
						if (SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.currentLaunchConditionManager).state == Spacecraft.MissionState.Grounded)
						{
							this.SelectDestination(KVP.Key);
						}
					});
					planet.SetOnEnter(delegate
					{
						planet.ShowLabel(true);
					});
					planet.SetOnExit(delegate
					{
						planet.ShowLabel(false);
					});
				}
			}
		}
	}

	// Token: 0x06006166 RID: 24934 RVA: 0x0023D9DC File Offset: 0x0023BBDC
	protected override void OnActivate()
	{
		base.OnActivate();
		StarmapScreen.Instance = this;
	}

	// Token: 0x06006167 RID: 24935 RVA: 0x0023D9EA File Offset: 0x0023BBEA
	private string DisplayDistance(float distance)
	{
		return global::Util.FormatWholeNumber(distance) + " " + UI.UNITSUFFIXES.DISTANCE.KILOMETER;
	}

	// Token: 0x06006168 RID: 24936 RVA: 0x0023DA06 File Offset: 0x0023BC06
	private string DisplayDestinationMass(SpaceDestination selectedDestination)
	{
		return GameUtil.GetFormattedMass(selectedDestination.AvailableMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}");
	}

	// Token: 0x06006169 RID: 24937 RVA: 0x0023DA1C File Offset: 0x0023BC1C
	private string DisplayTotalStorageCapacity(CommandModule command)
	{
		float num = 0f;
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(command.GetComponent<AttachableBuilding>()))
		{
			CargoBay component = gameObject.GetComponent<CargoBay>();
			if (component != null)
			{
				num += component.storage.Capacity();
			}
		}
		return GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}");
	}

	// Token: 0x0600616A RID: 24938 RVA: 0x0023DAA0 File Offset: 0x0023BCA0
	private string StorageCapacityTooltip(CommandModule command, SpaceDestination dest)
	{
		string text = "";
		bool flag = dest != null && SpacecraftManager.instance.GetDestinationAnalysisState(dest) == SpacecraftManager.DestinationAnalysisState.Complete;
		if (dest != null && flag)
		{
			if (dest.AvailableMass <= ConditionHasMinimumMass.CargoCapacity(dest, command))
			{
				text = text + UI.STARMAP.LAUNCHCHECKLIST.INSUFFICENT_MASS_TOOLTIP + "\n\n";
			}
			text = text + string.Format(UI.STARMAP.LAUNCHCHECKLIST.RESOURCE_MASS_TOOLTIP, dest.GetDestinationType().Name, GameUtil.GetFormattedMass(dest.AvailableMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"), GameUtil.GetFormattedMass(ConditionHasMinimumMass.CargoCapacity(dest, command), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}")) + "\n\n";
		}
		float num = ((dest != null) ? dest.AvailableMass : 0f);
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(command.GetComponent<AttachableBuilding>()))
		{
			CargoBay component = gameObject.GetComponent<CargoBay>();
			if (component != null)
			{
				if (flag)
				{
					float availableResourcesPercentage = dest.GetAvailableResourcesPercentage(component.storageType);
					float num2 = Mathf.Min(component.storage.Capacity(), availableResourcesPercentage * num);
					num -= num2;
					text = string.Concat(new string[]
					{
						text,
						component.gameObject.GetProperName(),
						" ",
						string.Format(UI.STARMAP.STORAGESTATS.STORAGECAPACITY, GameUtil.GetFormattedMass(Mathf.Min(num2, component.storage.Capacity()), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"), GameUtil.GetFormattedMass(component.storage.Capacity(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}")),
						"\n"
					});
				}
				else
				{
					text = string.Concat(new string[]
					{
						text,
						component.gameObject.GetProperName(),
						" ",
						string.Format(UI.STARMAP.STORAGESTATS.STORAGECAPACITY, GameUtil.GetFormattedMass(0f, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"), GameUtil.GetFormattedMass(component.storage.Capacity(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}")),
						"\n"
					});
				}
			}
		}
		return text;
	}

	// Token: 0x0600616B RID: 24939 RVA: 0x0023DCD4 File Offset: 0x0023BED4
	private void LoadPlanets()
	{
		foreach (SpaceDestination spaceDestination in Game.Instance.spacecraftManager.destinations)
		{
			if ((float)spaceDestination.OneBasedDistance * 10000f > this.planetsMaxDistance)
			{
				this.planetsMaxDistance = (float)spaceDestination.OneBasedDistance * 10000f;
			}
			while (this.planetRows.Count < spaceDestination.distance + 1)
			{
				GameObject gameObject = global::Util.KInstantiateUI(this.rowPrefab, this.rowsContiner.gameObject, true);
				gameObject.rectTransform().SetAsFirstSibling();
				this.planetRows.Add(gameObject);
				gameObject.GetComponentInChildren<Image>().color = this.distanceColors[this.planetRows.Count % this.distanceColors.Length];
				gameObject.GetComponentInChildren<LocText>().text = this.DisplayDistance((float)(this.planetRows.Count + 1) * 10000f);
			}
			GameObject gameObject2 = global::Util.KInstantiateUI(this.planetPrefab.gameObject, this.planetRows[spaceDestination.distance], true);
			this.planetWidgets.Add(spaceDestination, gameObject2.GetComponent<StarmapPlanet>());
		}
		this.UpdateDestinationStates();
	}

	// Token: 0x0600616C RID: 24940 RVA: 0x0023DE3C File Offset: 0x0023C03C
	private void UnselectAllPlanets()
	{
		if (this.animateSelectedPlanetRoutine != null)
		{
			base.StopCoroutine(this.animateSelectedPlanetRoutine);
		}
		foreach (KeyValuePair<SpaceDestination, StarmapPlanet> keyValuePair in this.planetWidgets)
		{
			keyValuePair.Value.SetSelectionActive(false);
			keyValuePair.Value.ShowAsCurrentRocketDestination(false);
		}
	}

	// Token: 0x0600616D RID: 24941 RVA: 0x0023DEB8 File Offset: 0x0023C0B8
	private void SelectPlanet(StarmapPlanet planet)
	{
		planet.SetSelectionActive(true);
		if (this.animateSelectedPlanetRoutine != null)
		{
			base.StopCoroutine(this.animateSelectedPlanetRoutine);
		}
		this.animateSelectedPlanetRoutine = base.StartCoroutine(this.AnimatePlanetSelection(planet));
	}

	// Token: 0x0600616E RID: 24942 RVA: 0x0023DEE8 File Offset: 0x0023C0E8
	private IEnumerator AnimatePlanetSelection(StarmapPlanet planet)
	{
		for (;;)
		{
			planet.AnimateSelector(Time.unscaledTime);
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		yield break;
	}

	// Token: 0x0600616F RID: 24943 RVA: 0x0023DEF7 File Offset: 0x0023C0F7
	private void Update()
	{
		this.PositionPlanetWidgets();
		if (this.forceScrollDown)
		{
			this.ScrollToBottom();
			this.forceScrollDown = false;
		}
	}

	// Token: 0x06006170 RID: 24944 RVA: 0x0023DF14 File Offset: 0x0023C114
	private void ScrollToBottom()
	{
		RectTransform rectTransform = this.Map.GetComponentInChildren<VerticalLayoutGroup>().rectTransform();
		rectTransform.SetLocalPosition(new Vector3(rectTransform.localPosition.x, rectTransform.rect.height - this.Map.rect.height, rectTransform.localPosition.z));
	}

	// Token: 0x06006171 RID: 24945 RVA: 0x0023DF76 File Offset: 0x0023C176
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.CheckBlockedInput())
		{
			if (!e.Consumed)
			{
				e.Consumed = true;
				return;
			}
		}
		else
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06006172 RID: 24946 RVA: 0x0023DF98 File Offset: 0x0023C198
	private bool CheckBlockedInput()
	{
		if (UnityEngine.EventSystems.EventSystem.current != null)
		{
			GameObject currentSelectedGameObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
			if (currentSelectedGameObject != null)
			{
				foreach (KeyValuePair<Spacecraft, HierarchyReferences> keyValuePair in this.listRocketRows)
				{
					EditableTitleBar component = keyValuePair.Value.GetReference<RectTransform>("EditableTitle").GetComponent<EditableTitleBar>();
					if (currentSelectedGameObject == component.inputField.gameObject)
					{
						return true;
					}
				}
				return false;
			}
		}
		return false;
	}

	// Token: 0x06006173 RID: 24947 RVA: 0x0023E038 File Offset: 0x0023C238
	private void PositionPlanetWidgets()
	{
		float num = this.rowPrefab.GetComponent<RectTransform>().rect.height / 2f;
		foreach (KeyValuePair<SpaceDestination, StarmapPlanet> keyValuePair in this.planetWidgets)
		{
			keyValuePair.Value.rectTransform().anchoredPosition = new Vector2(keyValuePair.Value.transform.parent.rectTransform().sizeDelta.x * keyValuePair.Key.startingOrbitPercentage, -num);
		}
	}

	// Token: 0x06006174 RID: 24948 RVA: 0x0023E0E8 File Offset: 0x0023C2E8
	private void OnSelectableChanged(object data)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (this.rocketConditionEventHandler != -1)
		{
			base.Unsubscribe(this.rocketConditionEventHandler);
		}
		if (data != null)
		{
			this.currentSelectable = ((GameObject)data).GetComponent<KSelectable>();
			this.currentCommandModule = this.currentSelectable.GetComponent<CommandModule>();
			this.currentLaunchConditionManager = this.currentSelectable.GetComponent<LaunchConditionManager>();
			if (this.currentCommandModule != null && this.currentLaunchConditionManager != null)
			{
				SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(this.currentLaunchConditionManager);
				this.SelectDestination(spacecraftDestination);
				this.rocketConditionEventHandler = this.currentLaunchConditionManager.Subscribe(1655598572, new Action<object>(this.Refresh));
				this.ShowRocketDetailsPanel();
			}
			else
			{
				this.currentSelectable = null;
				this.currentCommandModule = null;
				this.currentLaunchConditionManager = null;
				this.ShowRocketListPanel();
			}
		}
		else
		{
			this.currentSelectable = null;
			this.currentCommandModule = null;
			this.currentLaunchConditionManager = null;
			this.ShowRocketListPanel();
		}
		this.Refresh(null);
	}

	// Token: 0x06006175 RID: 24949 RVA: 0x0023E1EE File Offset: 0x0023C3EE
	private void ShowRocketListPanel()
	{
		this.listPanel.SetActive(true);
		this.rocketPanel.SetActive(false);
		this.launchButton.ChangeState(1);
		this.UpdateDistanceOverlay(null);
		this.UpdateMissionOverlay(null);
	}

	// Token: 0x06006176 RID: 24950 RVA: 0x0023E222 File Offset: 0x0023C422
	private void ShowRocketDetailsPanel()
	{
		this.listPanel.SetActive(false);
		this.rocketPanel.SetActive(true);
		this.ValidateTravelAbility();
		this.UpdateDistanceOverlay(null);
		this.UpdateMissionOverlay(null);
	}

	// Token: 0x06006177 RID: 24951 RVA: 0x0023E250 File Offset: 0x0023C450
	private void LaunchRocket(LaunchConditionManager lcm)
	{
		SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(lcm);
		if (spacecraftDestination == null)
		{
			return;
		}
		lcm.Launch(spacecraftDestination);
		this.ClearRocketListPanel();
		this.FillRocketListPanel();
		this.ShowRocketListPanel();
		this.Refresh(null);
	}

	// Token: 0x06006178 RID: 24952 RVA: 0x0023E28D File Offset: 0x0023C48D
	private void OnStartedTitlebarEditing()
	{
		base.isEditing = true;
		KScreenManager.Instance.RefreshStack();
	}

	// Token: 0x06006179 RID: 24953 RVA: 0x0023E2A0 File Offset: 0x0023C4A0
	private void OnEndEditing(string data)
	{
		base.isEditing = false;
	}

	// Token: 0x0600617A RID: 24954 RVA: 0x0023E2AC File Offset: 0x0023C4AC
	private void FillRocketListPanel()
	{
		this.ClearRocketListPanel();
		List<Spacecraft> spacecraft = SpacecraftManager.instance.GetSpacecraft();
		if (spacecraft.Count == 0)
		{
			this.listHeaderStatusLabel.text = UI.STARMAP.NO_ROCKETS_TITLE;
			this.listNoRocketText.gameObject.SetActive(true);
		}
		else
		{
			this.listHeaderStatusLabel.text = string.Format(UI.STARMAP.ROCKET_COUNT, spacecraft.Count);
			this.listNoRocketText.gameObject.SetActive(false);
		}
		using (List<Spacecraft>.Enumerator enumerator = spacecraft.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				StarmapScreen.<>c__DisplayClass114_0 CS$<>8__locals1 = new StarmapScreen.<>c__DisplayClass114_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.rocket = enumerator.Current;
				HierarchyReferences hierarchyReferences = global::Util.KInstantiateUI<HierarchyReferences>(this.listRocketTemplate.gameObject, this.rocketListContainer.gameObject, true);
				BreakdownList component = hierarchyReferences.GetComponent<BreakdownList>();
				MultiToggle component2 = hierarchyReferences.GetComponent<MultiToggle>();
				EditableTitleBar component3 = hierarchyReferences.GetReference<RectTransform>("EditableTitle").GetComponent<EditableTitleBar>();
				component3.OnStartedEditing += this.OnStartedTitlebarEditing;
				component3.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditing));
				MultiToggle component4 = hierarchyReferences.GetReference<RectTransform>("LaunchRocketButton").GetComponent<MultiToggle>();
				MultiToggle component5 = hierarchyReferences.GetReference<RectTransform>("LandRocketButton").GetComponent<MultiToggle>();
				HierarchyReferences component6 = hierarchyReferences.GetReference<RectTransform>("ProgressBar").GetComponent<HierarchyReferences>();
				LaunchConditionManager launchConditionManager = CS$<>8__locals1.rocket.launchConditions;
				CommandModule component7 = launchConditionManager.GetComponent<CommandModule>();
				MinionStorage component8 = launchConditionManager.GetComponent<MinionStorage>();
				component3.SetTitle(CS$<>8__locals1.rocket.rocketName);
				component3.OnNameChanged += delegate(string newName)
				{
					CS$<>8__locals1.rocket.SetRocketName(newName);
				};
				component2.onEnter = (System.Action)Delegate.Combine(component2.onEnter, new System.Action(delegate
				{
					LaunchConditionManager launchConditions = CS$<>8__locals1.rocket.launchConditions;
					CS$<>8__locals1.<>4__this.UpdateDistanceOverlay(launchConditions);
					CS$<>8__locals1.<>4__this.UpdateMissionOverlay(launchConditions);
				}));
				component2.onExit = (System.Action)Delegate.Combine(component2.onExit, new System.Action(delegate
				{
					this.UpdateDistanceOverlay(null);
					this.UpdateMissionOverlay(null);
				}));
				component2.onClick = (System.Action)Delegate.Combine(component2.onClick, new System.Action(delegate
				{
					CS$<>8__locals1.<>4__this.OnSelectableChanged(CS$<>8__locals1.rocket.launchConditions.gameObject);
				}));
				component4.play_sound_on_click = false;
				MultiToggle multiToggle = component4;
				multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
				{
					if (launchConditionManager != null)
					{
						KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Click", false));
						CS$<>8__locals1.<>4__this.LaunchRocket(launchConditionManager);
						return;
					}
					KFMOD.PlayUISound(GlobalAssets.GetSound("Negative", false));
				}));
				if ((DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive) && SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(launchConditionManager).state != Spacecraft.MissionState.Grounded)
				{
					component5.gameObject.SetActive(true);
					component5.transform.SetAsLastSibling();
					component5.play_sound_on_click = false;
					MultiToggle multiToggle2 = component5;
					multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(delegate
					{
						if (launchConditionManager != null)
						{
							KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Click", false));
							SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(launchConditionManager).ForceComplete();
							CS$<>8__locals1.<>4__this.ClearRocketListPanel();
							CS$<>8__locals1.<>4__this.FillRocketListPanel();
							CS$<>8__locals1.<>4__this.ShowRocketListPanel();
							CS$<>8__locals1.<>4__this.Refresh(null);
							return;
						}
						KFMOD.PlayUISound(GlobalAssets.GetSound("Negative", false));
					}));
				}
				else
				{
					component5.gameObject.SetActive(false);
				}
				BreakdownListRow breakdownListRow = component.AddRow();
				string text = UI.STARMAP.MISSION_STATUS.GROUNDED;
				global::Tuple<string, BreakdownListRow.Status> textForState = StarmapScreen.GetTextForState(CS$<>8__locals1.rocket.state, CS$<>8__locals1.rocket);
				text = textForState.first;
				BreakdownListRow.Status second = textForState.second;
				breakdownListRow.ShowStatusData(UI.STARMAP.ROCKETSTATUS.STATUS, text, second);
				breakdownListRow.SetHighlighted(true);
				if (component8 != null)
				{
					List<MinionStorage.Info> storedMinionInfo = component8.GetStoredMinionInfo();
					BreakdownListRow breakdownListRow2 = component.AddRow();
					int count = storedMinionInfo.Count;
					breakdownListRow2.ShowStatusData(UI.STARMAP.LISTTITLES.PASSENGERS, count.ToString(), (count == 0) ? BreakdownListRow.Status.Red : BreakdownListRow.Status.Green);
				}
				if (CS$<>8__locals1.rocket.state == Spacecraft.MissionState.Grounded)
				{
					string text2 = "";
					List<GameObject> attachedNetwork = AttachableBuilding.GetAttachedNetwork(launchConditionManager.GetComponent<AttachableBuilding>());
					foreach (GameObject gameObject in attachedNetwork)
					{
						text2 = text2 + gameObject.GetProperName() + "\n";
					}
					BreakdownListRow breakdownListRow3 = component.AddRow();
					breakdownListRow3.ShowData(UI.STARMAP.LISTTITLES.MODULES, attachedNetwork.Count.ToString());
					breakdownListRow3.AddTooltip(text2);
					component.AddRow().ShowData(UI.STARMAP.LISTTITLES.MAXRANGE, this.DisplayDistance(component7.rocketStats.GetRocketMaxDistance()));
					BreakdownListRow breakdownListRow4 = component.AddRow();
					breakdownListRow4.ShowData(UI.STARMAP.LISTTITLES.STORAGE, this.DisplayTotalStorageCapacity(component7));
					breakdownListRow4.AddTooltip(this.StorageCapacityTooltip(component7, this.selectedDestination));
					BreakdownListRow breakdownListRow5 = component.AddRow();
					if (this.selectedDestination != null)
					{
						if (SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete)
						{
							bool flag = this.selectedDestination.AvailableMass >= ConditionHasMinimumMass.CargoCapacity(this.selectedDestination, component7);
							breakdownListRow5.ShowStatusData(UI.STARMAP.LISTTITLES.DESTINATION_MASS, this.DisplayDestinationMass(this.selectedDestination), flag ? BreakdownListRow.Status.Default : BreakdownListRow.Status.Yellow);
							breakdownListRow5.AddTooltip(this.StorageCapacityTooltip(component7, this.selectedDestination));
						}
						else
						{
							breakdownListRow5.ShowStatusData(UI.STARMAP.LISTTITLES.DESTINATION_MASS, UI.STARMAP.COMPOSITION_UNDISCOVERED_AMOUNT, BreakdownListRow.Status.Default);
						}
					}
					else
					{
						breakdownListRow5.ShowStatusData(UI.STARMAP.DESTINATIONSELECTION.NOTSELECTED, "", BreakdownListRow.Status.Red);
						breakdownListRow5.AddTooltip(UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.NOTSELECTED);
					}
					component4.GetComponent<RectTransform>().SetAsLastSibling();
					component4.gameObject.SetActive(true);
					component6.gameObject.SetActive(false);
				}
				else
				{
					float duration = CS$<>8__locals1.rocket.GetDuration();
					float timeLeft = CS$<>8__locals1.rocket.GetTimeLeft();
					float num = ((duration == 0f) ? 0f : (1f - timeLeft / duration));
					component.AddRow().ShowData(UI.STARMAP.ROCKETSTATUS.TIMEREMAINING, ((CS$<>8__locals1.rocket.controlStationBuffTimeRemaining <= 0f) ? "" : UI.STARMAP.ROCKETSTATUS.BOOSTED_TIME_MODIFIER.text) + global::Util.FormatOneDecimalPlace(timeLeft / 600f) + " / " + GameUtil.GetFormattedCycles(duration, "F1", false));
					component6.gameObject.SetActive(true);
					RectTransform reference = component6.GetReference<RectTransform>("ProgressImage");
					TMP_Text component9 = component6.GetReference<RectTransform>("ProgressText").GetComponent<LocText>();
					reference.transform.localScale = new Vector3(num, 1f, 1f);
					component9.text = GameUtil.GetFormattedPercent(num * 100f, GameUtil.TimeSlice.None);
					component6.GetComponent<RectTransform>().SetAsLastSibling();
					component4.gameObject.SetActive(false);
				}
				this.listRocketRows.Add(CS$<>8__locals1.rocket, hierarchyReferences);
			}
		}
		this.UpdateRocketRowsTravelAbility();
	}

	// Token: 0x0600617B RID: 24955 RVA: 0x0023E990 File Offset: 0x0023CB90
	public static global::Tuple<string, BreakdownListRow.Status> GetTextForState(Spacecraft.MissionState state, Spacecraft spacecraft)
	{
		switch (state)
		{
		case Spacecraft.MissionState.Grounded:
			return new global::Tuple<string, BreakdownListRow.Status>(UI.STARMAP.MISSION_STATUS.GROUNDED, BreakdownListRow.Status.Green);
		case Spacecraft.MissionState.Launching:
			return new global::Tuple<string, BreakdownListRow.Status>(UI.STARMAP.MISSION_STATUS.LAUNCHING, BreakdownListRow.Status.Yellow);
		case Spacecraft.MissionState.Underway:
			return new global::Tuple<string, BreakdownListRow.Status>((spacecraft.controlStationBuffTimeRemaining <= 0f) ? UI.STARMAP.MISSION_STATUS.UNDERWAY.text : UI.STARMAP.MISSION_STATUS.UNDERWAY_BOOSTED.text, BreakdownListRow.Status.Red);
		case Spacecraft.MissionState.WaitingToLand:
			return new global::Tuple<string, BreakdownListRow.Status>(UI.STARMAP.MISSION_STATUS.WAITING_TO_LAND, BreakdownListRow.Status.Yellow);
		case Spacecraft.MissionState.Landing:
			return new global::Tuple<string, BreakdownListRow.Status>(UI.STARMAP.MISSION_STATUS.LANDING, BreakdownListRow.Status.Yellow);
		}
		return new global::Tuple<string, BreakdownListRow.Status>(UI.STARMAP.MISSION_STATUS.DESTROYED, BreakdownListRow.Status.Red);
	}

	// Token: 0x0600617C RID: 24956 RVA: 0x0023EA3C File Offset: 0x0023CC3C
	private void ClearRocketListPanel()
	{
		this.listHeaderStatusLabel.text = UI.STARMAP.NO_ROCKETS_TITLE;
		foreach (KeyValuePair<Spacecraft, HierarchyReferences> keyValuePair in this.listRocketRows)
		{
			UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
		}
		this.listRocketRows.Clear();
	}

	// Token: 0x0600617D RID: 24957 RVA: 0x0023EABC File Offset: 0x0023CCBC
	private void FillChecklist(LaunchConditionManager launchConditionManager)
	{
		foreach (ProcessCondition processCondition in launchConditionManager.GetLaunchConditionList())
		{
			BreakdownListRow breakdownListRow = this.rocketDetailsChecklist.AddRow();
			string statusMessage = processCondition.GetStatusMessage(ProcessCondition.Status.Ready);
			ProcessCondition.Status status = processCondition.EvaluateCondition();
			BreakdownListRow.Status status2 = BreakdownListRow.Status.Green;
			if (status == ProcessCondition.Status.Failure)
			{
				status2 = BreakdownListRow.Status.Red;
			}
			else if (status == ProcessCondition.Status.Warning)
			{
				status2 = BreakdownListRow.Status.Yellow;
			}
			breakdownListRow.ShowCheckmarkData(statusMessage, "", status2);
			if (status != ProcessCondition.Status.Ready)
			{
				breakdownListRow.SetHighlighted(true);
			}
			breakdownListRow.AddTooltip(processCondition.GetStatusTooltip(status));
		}
	}

	// Token: 0x0600617E RID: 24958 RVA: 0x0023EB60 File Offset: 0x0023CD60
	private void SelectDestination(SpaceDestination destination)
	{
		this.selectedDestination = destination;
		this.UnselectAllPlanets();
		if (this.selectedDestination != null)
		{
			this.SelectPlanet(this.planetWidgets[this.selectedDestination]);
			if (this.currentLaunchConditionManager != null)
			{
				SpacecraftManager.instance.SetSpacecraftDestination(this.currentLaunchConditionManager, this.selectedDestination);
			}
			this.ShowDestinationPanel();
			this.UpdateRocketRowsTravelAbility();
		}
		else
		{
			this.ClearDestinationPanel();
		}
		if (this.rangeRowTotal != null && this.selectedDestination != null && this.currentCommandModule != null)
		{
			this.rangeRowTotal.SetStatusColor(this.currentCommandModule.conditions.reachable.CanReachSpacecraftDestination(this.selectedDestination) ? BreakdownListRow.Status.Green : BreakdownListRow.Status.Red);
		}
		this.UpdateDestinationStates();
		this.Refresh(null);
	}

	// Token: 0x0600617F RID: 24959 RVA: 0x0023EC30 File Offset: 0x0023CE30
	private void UpdateRocketRowsTravelAbility()
	{
		foreach (KeyValuePair<Spacecraft, HierarchyReferences> keyValuePair in this.listRocketRows)
		{
			Spacecraft key = keyValuePair.Key;
			LaunchConditionManager launchConditions = key.launchConditions;
			CommandModule component = launchConditions.GetComponent<CommandModule>();
			MultiToggle component2 = keyValuePair.Value.GetReference<RectTransform>("LaunchRocketButton").GetComponent<MultiToggle>();
			bool flag = key.state == Spacecraft.MissionState.Grounded;
			SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(launchConditions);
			bool flag2 = spacecraftDestination != null && component.conditions.reachable.CanReachSpacecraftDestination(spacecraftDestination);
			bool flag3 = launchConditions.CheckReadyToLaunch();
			component2.ChangeState((flag && flag2 && flag3) ? 0 : 1);
		}
	}

	// Token: 0x06006180 RID: 24960 RVA: 0x0023ED00 File Offset: 0x0023CF00
	private void RefreshAnalyzeButton()
	{
		if (this.selectedDestination == null)
		{
			this.analyzeButton.ChangeState(1);
			this.analyzeButton.onClick = null;
			this.analyzeButton.GetComponentInChildren<LocText>().text = UI.STARMAP.NO_ANALYZABLE_DESTINATION_SELECTED;
			return;
		}
		if (this.selectedDestination.AnalysisState() == SpacecraftManager.DestinationAnalysisState.Complete)
		{
			if (DebugHandler.InstantBuildMode)
			{
				this.analyzeButton.ChangeState(0);
				this.analyzeButton.onClick = delegate
				{
					this.selectedDestination.TryCompleteResearchOpportunity();
					this.ShowDestinationPanel();
				};
				this.analyzeButton.GetComponentInChildren<LocText>().text = UI.STARMAP.ANALYSIS_COMPLETE + " (debug research)";
				return;
			}
			this.analyzeButton.ChangeState(1);
			this.analyzeButton.onClick = null;
			this.analyzeButton.GetComponentInChildren<LocText>().text = UI.STARMAP.ANALYSIS_COMPLETE;
			return;
		}
		else
		{
			this.analyzeButton.ChangeState(0);
			if (this.selectedDestination.id == SpacecraftManager.instance.GetStarmapAnalysisDestinationID())
			{
				this.analyzeButton.GetComponentInChildren<LocText>().text = UI.STARMAP.SUSPEND_DESTINATION_ANALYSIS;
				this.analyzeButton.onClick = delegate
				{
					SpacecraftManager.instance.SetStarmapAnalysisDestinationID(-1);
				};
				return;
			}
			this.analyzeButton.GetComponentInChildren<LocText>().text = UI.STARMAP.ANALYZE_DESTINATION;
			this.analyzeButton.onClick = delegate
			{
				if (DebugHandler.InstantBuildMode)
				{
					SpacecraftManager.instance.SetStarmapAnalysisDestinationID(this.selectedDestination.id);
					SpacecraftManager.instance.EarnDestinationAnalysisPoints(this.selectedDestination.id, 99999f);
					this.ShowDestinationPanel();
					return;
				}
				SpacecraftManager.instance.SetStarmapAnalysisDestinationID(this.selectedDestination.id);
			};
			return;
		}
	}

	// Token: 0x06006181 RID: 24961 RVA: 0x0023EE74 File Offset: 0x0023D074
	private void Refresh(object data = null)
	{
		this.FillRocketListPanel();
		this.RefreshAnalyzeButton();
		this.ShowDestinationPanel();
		if (this.currentCommandModule != null && this.currentLaunchConditionManager != null)
		{
			this.FillRocketPanel();
			if (this.selectedDestination != null)
			{
				this.ValidateTravelAbility();
				return;
			}
		}
		else
		{
			this.ClearRocketPanel();
		}
	}

	// Token: 0x06006182 RID: 24962 RVA: 0x0023EECC File Offset: 0x0023D0CC
	private void ClearRocketPanel()
	{
		this.rocketHeaderStatusLabel.text = UI.STARMAP.ROCKETSTATUS.NONE;
		this.rocketDetailsChecklist.ClearRows();
		this.rocketDetailsMass.ClearRows();
		this.rocketDetailsRange.ClearRows();
		this.rocketThrustWidget.gameObject.SetActive(false);
		this.rocketDetailsStorage.ClearRows();
		this.rocketDetailsFuel.ClearRows();
		this.rocketDetailsOxidizer.ClearRows();
		this.rocketDetailsDupes.ClearRows();
		this.rocketDetailsStatus.ClearRows();
		this.currentRocketHasLiquidContainer = false;
		this.currentRocketHasGasContainer = false;
		this.currentRocketHasSolidContainer = false;
		this.currentRocketHasEntitiesContainer = false;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rocketDetailsContainer);
	}

	// Token: 0x06006183 RID: 24963 RVA: 0x0023EF80 File Offset: 0x0023D180
	private void FillRocketPanel()
	{
		this.ClearRocketPanel();
		this.rocketHeaderStatusLabel.text = UI.STARMAP.STATUS;
		this.UpdateDistanceOverlay(null);
		this.UpdateMissionOverlay(null);
		this.FillChecklist(this.currentLaunchConditionManager);
		this.UpdateRangeDisplay();
		this.UpdateMassDisplay();
		this.UpdateOxidizerDisplay();
		this.UpdateStorageDisplay();
		this.UpdateFuelDisplay();
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.rocketDetailsContainer);
	}

	// Token: 0x06006184 RID: 24964 RVA: 0x0023EFEC File Offset: 0x0023D1EC
	private void UpdateRangeDisplay()
	{
		this.rocketDetailsRange.AddRow().ShowData(UI.STARMAP.ROCKETSTATS.TOTAL_OXIDIZABLE_FUEL, GameUtil.GetFormattedMass(this.currentCommandModule.rocketStats.GetTotalOxidizableFuel(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		this.rocketDetailsRange.AddRow().ShowData(UI.STARMAP.ROCKETSTATS.ENGINE_EFFICIENCY, GameUtil.GetFormattedEngineEfficiency(this.currentCommandModule.rocketStats.GetEngineEfficiency()));
		this.rocketDetailsRange.AddRow().ShowData(UI.STARMAP.ROCKETSTATS.OXIDIZER_EFFICIENCY, GameUtil.GetFormattedPercent(this.currentCommandModule.rocketStats.GetAverageOxidizerEfficiency(), GameUtil.TimeSlice.None));
		float num = this.currentCommandModule.rocketStats.GetBoosterThrust() * 1000f;
		if (num != 0f)
		{
			this.rocketDetailsRange.AddRow().ShowData(UI.STARMAP.ROCKETSTATS.SOLID_BOOSTER, GameUtil.GetFormattedDistance(num));
		}
		BreakdownListRow breakdownListRow = this.rocketDetailsRange.AddRow();
		breakdownListRow.ShowStatusData(UI.STARMAP.ROCKETSTATS.TOTAL_THRUST, GameUtil.GetFormattedDistance(this.currentCommandModule.rocketStats.GetTotalThrust() * 1000f), BreakdownListRow.Status.Green);
		breakdownListRow.SetImportant(true);
		float num2 = -(this.currentCommandModule.rocketStats.GetTotalThrust() - this.currentCommandModule.rocketStats.GetRocketMaxDistance());
		this.rocketThrustWidget.gameObject.SetActive(true);
		BreakdownListRow breakdownListRow2 = this.rocketDetailsRange.AddRow();
		breakdownListRow2.ShowStatusData(UI.STARMAP.ROCKETSTATUS.WEIGHTPENALTY, this.DisplayDistance(num2), BreakdownListRow.Status.Red);
		breakdownListRow2.SetHighlighted(true);
		this.rocketDetailsRange.AddCustomRow(this.rocketThrustWidget.gameObject);
		this.rocketThrustWidget.Draw(this.currentCommandModule);
		BreakdownListRow breakdownListRow3 = this.rocketDetailsRange.AddRow();
		breakdownListRow3.ShowData(UI.STARMAP.ROCKETSTATS.TOTAL_RANGE, GameUtil.GetFormattedDistance(this.currentCommandModule.rocketStats.GetRocketMaxDistance() * 1000f));
		breakdownListRow3.SetImportant(true);
	}

	// Token: 0x06006185 RID: 24965 RVA: 0x0023F1CC File Offset: 0x0023D3CC
	private void UpdateMassDisplay()
	{
		this.rocketDetailsMass.AddRow().ShowData(UI.STARMAP.ROCKETSTATS.DRY_MASS, GameUtil.GetFormattedMass(this.currentCommandModule.rocketStats.GetDryMass(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
		this.rocketDetailsMass.AddRow().ShowData(UI.STARMAP.ROCKETSTATS.WET_MASS, GameUtil.GetFormattedMass(this.currentCommandModule.rocketStats.GetWetMass(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
		BreakdownListRow breakdownListRow = this.rocketDetailsMass.AddRow();
		breakdownListRow.ShowData(UI.STARMAP.ROCKETSTATUS.TOTAL, GameUtil.GetFormattedMass(this.currentCommandModule.rocketStats.GetTotalMass(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
		breakdownListRow.SetImportant(true);
	}

	// Token: 0x06006186 RID: 24966 RVA: 0x0023F288 File Offset: 0x0023D488
	private void UpdateFuelDisplay()
	{
		Tag engineFuelTag = this.currentCommandModule.rocketStats.GetEngineFuelTag();
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.currentCommandModule.GetComponent<AttachableBuilding>()))
		{
			IFuelTank component = gameObject.GetComponent<IFuelTank>();
			if (!component.IsNullOrDestroyed())
			{
				BreakdownListRow breakdownListRow = this.rocketDetailsFuel.AddRow();
				if (engineFuelTag.IsValid)
				{
					Element element = ElementLoader.GetElement(engineFuelTag);
					global::Debug.Assert(element != null, "fuel_element");
					breakdownListRow.ShowData(gameObject.gameObject.GetProperName() + " (" + element.name + ")", GameUtil.GetFormattedMass(component.Storage.GetAmountAvailable(engineFuelTag), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
				}
				else
				{
					breakdownListRow.ShowData(gameObject.gameObject.GetProperName(), UI.STARMAP.ROCKETSTATS.NO_ENGINE);
					breakdownListRow.SetStatusColor(BreakdownListRow.Status.Red);
				}
			}
			SolidBooster component2 = gameObject.GetComponent<SolidBooster>();
			if (component2 != null)
			{
				BreakdownListRow breakdownListRow2 = this.rocketDetailsFuel.AddRow();
				Element element2 = ElementLoader.GetElement(component2.fuelTag);
				global::Debug.Assert(element2 != null, "fuel_element");
				breakdownListRow2.ShowData(gameObject.gameObject.GetProperName() + " (" + element2.name + ")", GameUtil.GetFormattedMass(component2.fuelStorage.GetMassAvailable(component2.fuelTag), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
			}
		}
		BreakdownListRow breakdownListRow3 = this.rocketDetailsFuel.AddRow();
		breakdownListRow3.ShowData(UI.STARMAP.ROCKETSTATS.TOTAL_FUEL, GameUtil.GetFormattedMass(this.currentCommandModule.rocketStats.GetTotalFuel(true), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
		breakdownListRow3.SetImportant(true);
	}

	// Token: 0x06006187 RID: 24967 RVA: 0x0023F468 File Offset: 0x0023D668
	private void UpdateOxidizerDisplay()
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.currentCommandModule.GetComponent<AttachableBuilding>()))
		{
			OxidizerTank component = gameObject.GetComponent<OxidizerTank>();
			if (component != null)
			{
				foreach (KeyValuePair<Tag, float> keyValuePair in component.GetOxidizersAvailable())
				{
					if (keyValuePair.Value != 0f)
					{
						this.rocketDetailsOxidizer.AddRow().ShowData(gameObject.gameObject.GetProperName() + " (" + keyValuePair.Key.ProperName() + ")", GameUtil.GetFormattedMass(keyValuePair.Value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
					}
				}
			}
			SolidBooster component2 = gameObject.GetComponent<SolidBooster>();
			if (component2 != null)
			{
				this.rocketDetailsOxidizer.AddRow().ShowData(gameObject.gameObject.GetProperName() + " (" + ElementLoader.FindElementByHash(SimHashes.OxyRock).name + ")", GameUtil.GetFormattedMass(component2.fuelStorage.GetMassAvailable(ElementLoader.FindElementByHash(SimHashes.OxyRock).tag), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
			}
		}
		BreakdownListRow breakdownListRow = this.rocketDetailsOxidizer.AddRow();
		breakdownListRow.ShowData(UI.STARMAP.ROCKETSTATS.TOTAL_OXIDIZER, GameUtil.GetFormattedMass(this.currentCommandModule.rocketStats.GetTotalOxidizer(true), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
		breakdownListRow.SetImportant(true);
	}

	// Token: 0x06006188 RID: 24968 RVA: 0x0023F634 File Offset: 0x0023D834
	private void UpdateStorageDisplay()
	{
		float num = ((this.selectedDestination != null) ? this.selectedDestination.AvailableMass : 0f);
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.currentCommandModule.GetComponent<AttachableBuilding>()))
		{
			CargoBay component = gameObject.GetComponent<CargoBay>();
			if (component != null)
			{
				BreakdownListRow breakdownListRow = this.rocketDetailsStorage.AddRow();
				if (this.selectedDestination != null)
				{
					float availableResourcesPercentage = this.selectedDestination.GetAvailableResourcesPercentage(component.storageType);
					float num2 = Mathf.Min(component.storage.Capacity(), availableResourcesPercentage * num);
					num -= num2;
					breakdownListRow.ShowData(gameObject.gameObject.GetProperName(), string.Format(UI.STARMAP.STORAGESTATS.STORAGECAPACITY, GameUtil.GetFormattedMass(Mathf.Min(num2, component.storage.Capacity()), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"), GameUtil.GetFormattedMass(component.storage.Capacity(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}")));
				}
				else
				{
					breakdownListRow.ShowData(gameObject.gameObject.GetProperName(), string.Format(UI.STARMAP.STORAGESTATS.STORAGECAPACITY, GameUtil.GetFormattedMass(0f, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"), GameUtil.GetFormattedMass(component.storage.Capacity(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}")));
				}
			}
		}
	}

	// Token: 0x06006189 RID: 24969 RVA: 0x0023F7B8 File Offset: 0x0023D9B8
	private void ClearDestinationPanel()
	{
		this.destinationDetailsContainer.gameObject.SetActive(false);
		this.destinationStatusLabel.text = UI.STARMAP.ROCKETSTATUS.NONE;
	}

	// Token: 0x0600618A RID: 24970 RVA: 0x0023F7E0 File Offset: 0x0023D9E0
	private void ShowDestinationPanel()
	{
		if (this.selectedDestination == null)
		{
			return;
		}
		this.destinationStatusLabel.text = UI.STARMAP.ROCKETSTATUS.SELECTED;
		if (this.currentLaunchConditionManager != null && SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.currentLaunchConditionManager).state != Spacecraft.MissionState.Grounded)
		{
			this.destinationStatusLabel.text = UI.STARMAP.ROCKETSTATUS.LOCKEDIN;
		}
		SpaceDestinationType destinationType = this.selectedDestination.GetDestinationType();
		this.destinationNameLabel.text = ((SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete) ? destinationType.Name : UI.STARMAP.UNKNOWN_DESTINATION.text);
		this.destinationTypeValueLabel.text = ((SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete) ? destinationType.typeName : UI.STARMAP.UNKNOWN_TYPE.text);
		this.destinationDistanceValueLabel.text = this.DisplayDistance((float)this.selectedDestination.OneBasedDistance * 10000f);
		this.destinationDescriptionLabel.text = destinationType.description;
		this.destinationDetailsComposition.ClearRows();
		this.destinationDetailsResearch.ClearRows();
		this.destinationDetailsMass.ClearRows();
		this.destinationDetailsResources.ClearRows();
		this.destinationDetailsArtifacts.ClearRows();
		if (destinationType.visitable)
		{
			float num = 0f;
			if (SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete)
			{
				num = this.selectedDestination.GetTotalMass();
			}
			if (SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete)
			{
				foreach (SpaceDestination.ResearchOpportunity researchOpportunity in this.selectedDestination.researchOpportunities)
				{
					BreakdownListRow breakdownListRow = this.destinationDetailsResearch.AddRow();
					string text = ((researchOpportunity.discoveredRareResource != SimHashes.Void) ? string.Format("(!!) {0}", researchOpportunity.description) : researchOpportunity.description);
					breakdownListRow.ShowCheckmarkData(text, researchOpportunity.dataValue.ToString(), researchOpportunity.completed ? BreakdownListRow.Status.Green : BreakdownListRow.Status.Default);
				}
			}
			this.destinationAnalysisProgressBar.SetFillPercentage(SpacecraftManager.instance.GetDestinationAnalysisScore(this.selectedDestination.id) / (float)ROCKETRY.DESTINATION_ANALYSIS.COMPLETE);
			float num2 = ConditionHasMinimumMass.CargoCapacity(this.selectedDestination, this.currentCommandModule);
			if (SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete)
			{
				string formattedMass = GameUtil.GetFormattedMass(this.selectedDestination.CurrentMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}");
				string formattedMass2 = GameUtil.GetFormattedMass((float)destinationType.minimumMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}");
				BreakdownListRow breakdownListRow2 = this.destinationDetailsMass.AddRow();
				breakdownListRow2.ShowData(UI.STARMAP.CURRENT_MASS, formattedMass);
				if (this.selectedDestination.AvailableMass < num2)
				{
					breakdownListRow2.SetStatusColor(BreakdownListRow.Status.Yellow);
					breakdownListRow2.AddTooltip(string.Format(UI.STARMAP.CURRENT_MASS_TOOLTIP, GameUtil.GetFormattedMass(this.selectedDestination.AvailableMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"), GameUtil.GetFormattedMass(num2, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}")));
				}
				this.destinationDetailsMass.AddRow().ShowData(UI.STARMAP.MAXIMUM_MASS, GameUtil.GetFormattedMass((float)destinationType.maxiumMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Tonne, true, "{0:0.#}"));
				BreakdownListRow breakdownListRow3 = this.destinationDetailsMass.AddRow();
				breakdownListRow3.ShowData(UI.STARMAP.MINIMUM_MASS, formattedMass2);
				breakdownListRow3.AddTooltip(UI.STARMAP.MINIMUM_MASS_TOOLTIP);
				BreakdownListRow breakdownListRow4 = this.destinationDetailsMass.AddRow();
				breakdownListRow4.ShowData(UI.STARMAP.REPLENISH_RATE, GameUtil.GetFormattedMass(destinationType.replishmentPerCycle, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"));
				breakdownListRow4.AddTooltip(UI.STARMAP.REPLENISH_RATE_TOOLTIP);
			}
			if (SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete)
			{
				foreach (KeyValuePair<SimHashes, float> keyValuePair in this.selectedDestination.recoverableElements)
				{
					BreakdownListRow breakdownListRow5 = this.destinationDetailsComposition.AddRow();
					float num3 = this.selectedDestination.GetResourceValue(keyValuePair.Key, keyValuePair.Value) / num * 100f;
					Element element = ElementLoader.FindElementByHash(keyValuePair.Key);
					global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(element, "ui", false);
					if (num3 <= 1f)
					{
						breakdownListRow5.ShowIconData(element.name, UI.STARMAP.COMPOSITION_SMALL_AMOUNT, uisprite.first, uisprite.second);
					}
					else
					{
						breakdownListRow5.ShowIconData(element.name, GameUtil.GetFormattedPercent(num3, GameUtil.TimeSlice.None), uisprite.first, uisprite.second);
					}
					if (element.IsGas)
					{
						string properName = Assets.GetPrefab("GasCargoBay".ToTag()).GetProperName();
						if (this.currentRocketHasGasContainer)
						{
							breakdownListRow5.SetHighlighted(true);
							breakdownListRow5.AddTooltip(string.Format(UI.STARMAP.CAN_CARRY_ELEMENT, element.name, properName));
						}
						else
						{
							breakdownListRow5.SetDisabled(true);
							breakdownListRow5.AddTooltip(string.Format(UI.STARMAP.CONTAINER_REQUIRED, properName));
						}
					}
					if (element.IsLiquid)
					{
						string properName2 = Assets.GetPrefab("LiquidCargoBay".ToTag()).GetProperName();
						if (this.currentRocketHasLiquidContainer)
						{
							breakdownListRow5.SetHighlighted(true);
							breakdownListRow5.AddTooltip(string.Format(UI.STARMAP.CAN_CARRY_ELEMENT, element.name, properName2));
						}
						else
						{
							breakdownListRow5.SetDisabled(true);
							breakdownListRow5.AddTooltip(string.Format(UI.STARMAP.CONTAINER_REQUIRED, properName2));
						}
					}
					if (element.IsSolid)
					{
						string properName3 = Assets.GetPrefab("CargoBay".ToTag()).GetProperName();
						if (this.currentRocketHasSolidContainer)
						{
							breakdownListRow5.SetHighlighted(true);
							breakdownListRow5.AddTooltip(string.Format(UI.STARMAP.CAN_CARRY_ELEMENT, element.name, properName3));
						}
						else
						{
							breakdownListRow5.SetDisabled(true);
							breakdownListRow5.AddTooltip(string.Format(UI.STARMAP.CONTAINER_REQUIRED, properName3));
						}
					}
				}
				foreach (SpaceDestination.ResearchOpportunity researchOpportunity2 in this.selectedDestination.researchOpportunities)
				{
					if (!researchOpportunity2.completed && researchOpportunity2.discoveredRareResource != SimHashes.Void)
					{
						BreakdownListRow breakdownListRow6 = this.destinationDetailsComposition.AddRow();
						breakdownListRow6.ShowData(UI.STARMAP.COMPOSITION_UNDISCOVERED, UI.STARMAP.COMPOSITION_UNDISCOVERED_AMOUNT);
						breakdownListRow6.SetDisabled(true);
						breakdownListRow6.AddTooltip(UI.STARMAP.COMPOSITION_UNDISCOVERED_TOOLTIP);
					}
				}
			}
			if (SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete)
			{
				foreach (KeyValuePair<Tag, int> keyValuePair2 in this.selectedDestination.GetRecoverableEntities())
				{
					BreakdownListRow breakdownListRow7 = this.destinationDetailsResources.AddRow();
					GameObject prefab = Assets.GetPrefab(keyValuePair2.Key);
					global::Tuple<Sprite, Color> uisprite2 = Def.GetUISprite(prefab, "ui", false);
					breakdownListRow7.ShowIconData(prefab.GetProperName(), "", uisprite2.first, uisprite2.second);
					string properName4 = Assets.GetPrefab("SpecialCargoBay".ToTag()).GetProperName();
					if (this.currentRocketHasEntitiesContainer)
					{
						breakdownListRow7.SetHighlighted(true);
						breakdownListRow7.AddTooltip(string.Format(UI.STARMAP.CAN_CARRY_ELEMENT, prefab.GetProperName(), properName4));
					}
					else
					{
						breakdownListRow7.SetDisabled(true);
						breakdownListRow7.AddTooltip(string.Format(UI.STARMAP.CANT_CARRY_ELEMENT, properName4, prefab.GetProperName()));
					}
				}
			}
			if (SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete)
			{
				ArtifactDropRate artifactDropTable = this.selectedDestination.GetDestinationType().artifactDropTable;
				foreach (global::Tuple<ArtifactTier, float> tuple in artifactDropTable.rates)
				{
					this.destinationDetailsArtifacts.AddRow().ShowData(Strings.Get(tuple.first.name_key), GameUtil.GetFormattedPercent(tuple.second / artifactDropTable.totalWeight * 100f, GameUtil.TimeSlice.None));
				}
			}
			this.destinationDetailsContainer.gameObject.SetActive(true);
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.destinationDetailsContainer);
	}

	// Token: 0x0600618B RID: 24971 RVA: 0x00240080 File Offset: 0x0023E280
	private void ValidateTravelAbility()
	{
		if (this.selectedDestination != null && SpacecraftManager.instance.GetDestinationAnalysisState(this.selectedDestination) == SpacecraftManager.DestinationAnalysisState.Complete && this.currentCommandModule != null && this.currentLaunchConditionManager != null)
		{
			this.launchButton.ChangeState(this.currentLaunchConditionManager.CheckReadyToLaunch() ? 0 : 1);
		}
	}

	// Token: 0x0600618C RID: 24972 RVA: 0x002400E0 File Offset: 0x0023E2E0
	private void UpdateDistanceOverlay(LaunchConditionManager lcmToVisualize = null)
	{
		if (lcmToVisualize == null)
		{
			lcmToVisualize = this.currentLaunchConditionManager;
		}
		Spacecraft spacecraft = null;
		if (lcmToVisualize != null)
		{
			spacecraft = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(lcmToVisualize);
		}
		if (lcmToVisualize != null && spacecraft != null && spacecraft.state == Spacecraft.MissionState.Grounded)
		{
			this.distanceOverlay.gameObject.SetActive(true);
			float num = lcmToVisualize.GetComponent<CommandModule>().rocketStats.GetRocketMaxDistance();
			num = (float)((int)(num / 10000f)) * 10000f;
			Vector2 sizeDelta = this.distanceOverlay.rectTransform.sizeDelta;
			sizeDelta.x = this.rowsContiner.rect.width;
			sizeDelta.y = (1f - num / this.planetsMaxDistance) * this.rowsContiner.rect.height + (float)this.distanceOverlayYOffset + (float)this.distanceOverlayVerticalOffset;
			this.distanceOverlay.rectTransform.sizeDelta = sizeDelta;
			this.distanceOverlay.rectTransform.anchoredPosition = new Vector3(0f, (float)this.distanceOverlayVerticalOffset, 0f);
			return;
		}
		this.distanceOverlay.gameObject.SetActive(false);
	}

	// Token: 0x0600618D RID: 24973 RVA: 0x00240218 File Offset: 0x0023E418
	private void UpdateMissionOverlay(LaunchConditionManager lcmToVisualize = null)
	{
		if (lcmToVisualize == null)
		{
			lcmToVisualize = this.currentLaunchConditionManager;
		}
		Spacecraft spacecraft = null;
		if (lcmToVisualize != null)
		{
			spacecraft = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(lcmToVisualize);
		}
		if (lcmToVisualize != null && spacecraft != null)
		{
			SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(lcmToVisualize);
			if (spacecraftDestination == null)
			{
				global::Debug.Log("destination is null");
				return;
			}
			StarmapPlanet starmapPlanet = this.planetWidgets[spacecraftDestination];
			if (spacecraft == null)
			{
				global::Debug.Log("craft is null");
				return;
			}
			if (starmapPlanet == null)
			{
				global::Debug.Log("planet is null");
				return;
			}
			this.UnselectAllPlanets();
			this.SelectPlanet(starmapPlanet);
			starmapPlanet.ShowAsCurrentRocketDestination(spacecraftDestination.GetDestinationType().visitable);
			if (spacecraft.state != Spacecraft.MissionState.Grounded && spacecraftDestination.GetDestinationType().visitable)
			{
				this.visualizeRocketImage.gameObject.SetActive(true);
				this.visualizeRocketTrajectory.gameObject.SetActive(true);
				this.visualizeRocketLabel.gameObject.SetActive(true);
				this.visualizeRocketProgress.gameObject.SetActive(true);
				float duration = spacecraft.GetDuration();
				float timeLeft = spacecraft.GetTimeLeft();
				float num = ((duration == 0f) ? 0f : (1f - timeLeft / duration));
				bool flag = num > 0.5f;
				Vector2 vector = new Vector2(0f, -this.rowsContiner.rect.size.y);
				Vector3 vector2 = starmapPlanet.rectTransform().localPosition + new Vector3(starmapPlanet.rectTransform().sizeDelta.x * 0.5f, 0f, 0f);
				vector2 = starmapPlanet.transform.parent.rectTransform().localPosition + vector2;
				Vector2 vector3 = new Vector2(vector2.x, vector2.y);
				float num2 = Vector2.Distance(vector, vector3);
				Vector2 vector4 = vector3 - vector;
				float num3 = Mathf.Atan2(vector4.y, vector4.x) * 57.29578f;
				Vector2 vector5;
				if (flag)
				{
					vector5 = new Vector2(Mathf.Lerp(vector.x, vector3.x, 1f - num * 2f + 1f), Mathf.Lerp(vector.y, vector3.y, 1f - num * 2f + 1f));
				}
				else
				{
					vector5 = new Vector2(Mathf.Lerp(vector.x, vector3.x, num * 2f), Mathf.Lerp(vector.y, vector3.y, num * 2f));
				}
				this.visualizeRocketLabel.text = StarmapScreen.GetTextForState(spacecraft.state, spacecraft).first;
				this.visualizeRocketProgress.text = GameUtil.GetFormattedPercent(num * 100f, GameUtil.TimeSlice.None);
				this.visualizeRocketTrajectory.transform.SetLocalPosition(vector);
				this.visualizeRocketTrajectory.rectTransform.sizeDelta = new Vector2(num2, this.visualizeRocketTrajectory.rectTransform.sizeDelta.y);
				this.visualizeRocketTrajectory.rectTransform.localRotation = Quaternion.Euler(0f, 0f, num3);
				this.visualizeRocketImage.transform.SetLocalPosition(vector5);
				return;
			}
		}
		else
		{
			if (this.selectedDestination != null && this.planetWidgets.ContainsKey(this.selectedDestination))
			{
				this.UnselectAllPlanets();
				StarmapPlanet starmapPlanet2 = this.planetWidgets[this.selectedDestination];
				this.SelectPlanet(starmapPlanet2);
			}
			else
			{
				this.UnselectAllPlanets();
			}
			this.visualizeRocketImage.gameObject.SetActive(false);
			this.visualizeRocketTrajectory.gameObject.SetActive(false);
			this.visualizeRocketLabel.gameObject.SetActive(false);
			this.visualizeRocketProgress.gameObject.SetActive(false);
		}
	}

	// Token: 0x04004317 RID: 17175
	public GameObject listPanel;

	// Token: 0x04004318 RID: 17176
	public GameObject rocketPanel;

	// Token: 0x04004319 RID: 17177
	public LocText listHeaderLabel;

	// Token: 0x0400431A RID: 17178
	public LocText listHeaderStatusLabel;

	// Token: 0x0400431B RID: 17179
	public HierarchyReferences listRocketTemplate;

	// Token: 0x0400431C RID: 17180
	public LocText listNoRocketText;

	// Token: 0x0400431D RID: 17181
	public RectTransform rocketListContainer;

	// Token: 0x0400431E RID: 17182
	private Dictionary<Spacecraft, HierarchyReferences> listRocketRows = new Dictionary<Spacecraft, HierarchyReferences>();

	// Token: 0x0400431F RID: 17183
	[Header("Shared References")]
	public BreakdownList breakdownListPrefab;

	// Token: 0x04004320 RID: 17184
	public GameObject progressBarPrefab;

	// Token: 0x04004321 RID: 17185
	[Header("Selected Rocket References")]
	public LocText rocketHeaderLabel;

	// Token: 0x04004322 RID: 17186
	public LocText rocketHeaderStatusLabel;

	// Token: 0x04004323 RID: 17187
	private BreakdownList rocketDetailsStatus;

	// Token: 0x04004324 RID: 17188
	public Sprite rocketDetailsStatusIcon;

	// Token: 0x04004325 RID: 17189
	private BreakdownList rocketDetailsChecklist;

	// Token: 0x04004326 RID: 17190
	public Sprite rocketDetailsChecklistIcon;

	// Token: 0x04004327 RID: 17191
	private BreakdownList rocketDetailsMass;

	// Token: 0x04004328 RID: 17192
	public Sprite rocketDetailsMassIcon;

	// Token: 0x04004329 RID: 17193
	private BreakdownList rocketDetailsRange;

	// Token: 0x0400432A RID: 17194
	public Sprite rocketDetailsRangeIcon;

	// Token: 0x0400432B RID: 17195
	public RocketThrustWidget rocketThrustWidget;

	// Token: 0x0400432C RID: 17196
	private BreakdownList rocketDetailsStorage;

	// Token: 0x0400432D RID: 17197
	public Sprite rocketDetailsStorageIcon;

	// Token: 0x0400432E RID: 17198
	private BreakdownList rocketDetailsDupes;

	// Token: 0x0400432F RID: 17199
	public Sprite rocketDetailsDupesIcon;

	// Token: 0x04004330 RID: 17200
	private BreakdownList rocketDetailsFuel;

	// Token: 0x04004331 RID: 17201
	public Sprite rocketDetailsFuelIcon;

	// Token: 0x04004332 RID: 17202
	private BreakdownList rocketDetailsOxidizer;

	// Token: 0x04004333 RID: 17203
	public Sprite rocketDetailsOxidizerIcon;

	// Token: 0x04004334 RID: 17204
	public RectTransform rocketDetailsContainer;

	// Token: 0x04004335 RID: 17205
	[Header("Selected Destination References")]
	public LocText destinationHeaderLabel;

	// Token: 0x04004336 RID: 17206
	public LocText destinationStatusLabel;

	// Token: 0x04004337 RID: 17207
	public LocText destinationNameLabel;

	// Token: 0x04004338 RID: 17208
	public LocText destinationTypeNameLabel;

	// Token: 0x04004339 RID: 17209
	public LocText destinationTypeValueLabel;

	// Token: 0x0400433A RID: 17210
	public LocText destinationDistanceNameLabel;

	// Token: 0x0400433B RID: 17211
	public LocText destinationDistanceValueLabel;

	// Token: 0x0400433C RID: 17212
	public LocText destinationDescriptionLabel;

	// Token: 0x0400433D RID: 17213
	private BreakdownList destinationDetailsAnalysis;

	// Token: 0x0400433E RID: 17214
	private GenericUIProgressBar destinationAnalysisProgressBar;

	// Token: 0x0400433F RID: 17215
	public Sprite destinationDetailsAnalysisIcon;

	// Token: 0x04004340 RID: 17216
	private BreakdownList destinationDetailsResearch;

	// Token: 0x04004341 RID: 17217
	public Sprite destinationDetailsResearchIcon;

	// Token: 0x04004342 RID: 17218
	private BreakdownList destinationDetailsMass;

	// Token: 0x04004343 RID: 17219
	public Sprite destinationDetailsMassIcon;

	// Token: 0x04004344 RID: 17220
	private BreakdownList destinationDetailsComposition;

	// Token: 0x04004345 RID: 17221
	public Sprite destinationDetailsCompositionIcon;

	// Token: 0x04004346 RID: 17222
	private BreakdownList destinationDetailsResources;

	// Token: 0x04004347 RID: 17223
	public Sprite destinationDetailsResourcesIcon;

	// Token: 0x04004348 RID: 17224
	private BreakdownList destinationDetailsArtifacts;

	// Token: 0x04004349 RID: 17225
	public Sprite destinationDetailsArtifactsIcon;

	// Token: 0x0400434A RID: 17226
	public RectTransform destinationDetailsContainer;

	// Token: 0x0400434B RID: 17227
	public MultiToggle showRocketsButton;

	// Token: 0x0400434C RID: 17228
	public MultiToggle launchButton;

	// Token: 0x0400434D RID: 17229
	public MultiToggle analyzeButton;

	// Token: 0x0400434E RID: 17230
	private int rocketConditionEventHandler = -1;

	// Token: 0x0400434F RID: 17231
	[Header("Map References")]
	public RectTransform Map;

	// Token: 0x04004350 RID: 17232
	public RectTransform rowsContiner;

	// Token: 0x04004351 RID: 17233
	public GameObject rowPrefab;

	// Token: 0x04004352 RID: 17234
	public StarmapPlanet planetPrefab;

	// Token: 0x04004353 RID: 17235
	public GameObject rocketIconPrefab;

	// Token: 0x04004354 RID: 17236
	private List<GameObject> planetRows = new List<GameObject>();

	// Token: 0x04004355 RID: 17237
	private Dictionary<SpaceDestination, StarmapPlanet> planetWidgets = new Dictionary<SpaceDestination, StarmapPlanet>();

	// Token: 0x04004356 RID: 17238
	private float planetsMaxDistance = 1f;

	// Token: 0x04004357 RID: 17239
	public Image distanceOverlay;

	// Token: 0x04004358 RID: 17240
	private int distanceOverlayVerticalOffset = 500;

	// Token: 0x04004359 RID: 17241
	private int distanceOverlayYOffset = 24;

	// Token: 0x0400435A RID: 17242
	public Image visualizeRocketImage;

	// Token: 0x0400435B RID: 17243
	public Image visualizeRocketTrajectory;

	// Token: 0x0400435C RID: 17244
	public LocText visualizeRocketLabel;

	// Token: 0x0400435D RID: 17245
	public LocText visualizeRocketProgress;

	// Token: 0x0400435E RID: 17246
	public Color[] distanceColors;

	// Token: 0x0400435F RID: 17247
	public LocText titleBarLabel;

	// Token: 0x04004360 RID: 17248
	public KButton button;

	// Token: 0x04004361 RID: 17249
	private const int DESTINATION_ICON_SCALE = 2;

	// Token: 0x04004362 RID: 17250
	public static StarmapScreen Instance;

	// Token: 0x04004363 RID: 17251
	private int selectionUpdateHandle = -1;

	// Token: 0x04004364 RID: 17252
	private SpaceDestination selectedDestination;

	// Token: 0x04004365 RID: 17253
	private KSelectable currentSelectable;

	// Token: 0x04004366 RID: 17254
	private CommandModule currentCommandModule;

	// Token: 0x04004367 RID: 17255
	private LaunchConditionManager currentLaunchConditionManager;

	// Token: 0x04004368 RID: 17256
	private bool currentRocketHasGasContainer;

	// Token: 0x04004369 RID: 17257
	private bool currentRocketHasLiquidContainer;

	// Token: 0x0400436A RID: 17258
	private bool currentRocketHasSolidContainer;

	// Token: 0x0400436B RID: 17259
	private bool currentRocketHasEntitiesContainer;

	// Token: 0x0400436C RID: 17260
	private bool forceScrollDown = true;

	// Token: 0x0400436D RID: 17261
	private Coroutine animateAnalysisRoutine;

	// Token: 0x0400436E RID: 17262
	private Coroutine animateSelectedPlanetRoutine;

	// Token: 0x0400436F RID: 17263
	private BreakdownListRow rangeRowTotal;
}
