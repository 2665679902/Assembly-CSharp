using System;
using System.Collections.Generic;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000A0C RID: 2572
public class ManagementMenu : KIconToggleMenu
{
	// Token: 0x06004D6E RID: 19822 RVA: 0x001B4A4D File Offset: 0x001B2C4D
	public static void DestroyInstance()
	{
		ManagementMenu.Instance = null;
	}

	// Token: 0x06004D6F RID: 19823 RVA: 0x001B4A55 File Offset: 0x001B2C55
	public override float GetSortKey()
	{
		return 21f;
	}

	// Token: 0x06004D70 RID: 19824 RVA: 0x001B4A5C File Offset: 0x001B2C5C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ManagementMenu.Instance = this;
		this.notificationDisplayer.onNotificationsChanged += this.OnNotificationsChanged;
		CodexCache.CodexCacheInit();
		ScheduledUIInstantiation component = GameScreenManager.Instance.GetComponent<ScheduledUIInstantiation>();
		this.starmapScreen = component.GetInstantiatedObject<StarmapScreen>();
		this.clusterMapScreen = component.GetInstantiatedObject<ClusterMapScreen>();
		this.skillsScreen = component.GetInstantiatedObject<SkillsScreen>();
		this.researchScreen = component.GetInstantiatedObject<ResearchScreen>();
		this.fullscreenUIs = new ManagementMenu.ManagementMenuToggleInfo[] { this.researchInfo, this.skillsInfo, this.starmapInfo, this.clusterMapInfo };
		base.Subscribe(Game.Instance.gameObject, 288942073, new Action<object>(this.OnUIClear));
		this.consumablesInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.CONSUMABLES, "OverviewUI_consumables_icon", null, global::Action.ManageConsumables, UI.TOOLTIPS.MANAGEMENTMENU_CONSUMABLES, "");
		this.AddToggleTooltip(this.consumablesInfo, null);
		this.vitalsInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.VITALS, "OverviewUI_vitals_icon", null, global::Action.ManageVitals, UI.TOOLTIPS.MANAGEMENTMENU_VITALS, "");
		this.AddToggleTooltip(this.vitalsInfo, null);
		this.researchInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.RESEARCH, "OverviewUI_research_nav_icon", null, global::Action.ManageResearch, UI.TOOLTIPS.MANAGEMENTMENU_RESEARCH, "");
		this.AddToggleTooltip(this.researchInfo, UI.TOOLTIPS.MANAGEMENTMENU_REQUIRES_RESEARCH);
		this.jobsInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.JOBS, "OverviewUI_priority_icon", null, global::Action.ManagePriorities, UI.TOOLTIPS.MANAGEMENTMENU_JOBS, "");
		this.AddToggleTooltip(this.jobsInfo, null);
		this.skillsInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.SKILLS, "OverviewUI_jobs_icon", null, global::Action.ManageSkills, UI.TOOLTIPS.MANAGEMENTMENU_SKILLS, "");
		this.AddToggleTooltip(this.skillsInfo, UI.TOOLTIPS.MANAGEMENTMENU_REQUIRES_SKILL_STATION);
		this.starmapInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.STARMAP.MANAGEMENT_BUTTON, "OverviewUI_starmap_icon", null, global::Action.ManageStarmap, UI.TOOLTIPS.MANAGEMENTMENU_STARMAP, "");
		this.AddToggleTooltip(this.starmapInfo, UI.TOOLTIPS.MANAGEMENTMENU_REQUIRES_TELESCOPE);
		this.clusterMapInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.STARMAP.MANAGEMENT_BUTTON, "OverviewUI_starmap_icon", null, global::Action.ManageStarmap, UI.TOOLTIPS.MANAGEMENTMENU_STARMAP, "");
		this.AddToggleTooltip(this.clusterMapInfo, null);
		this.scheduleInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.SCHEDULE, "OverviewUI_schedule2_icon", null, global::Action.ManageSchedule, UI.TOOLTIPS.MANAGEMENTMENU_SCHEDULE, "");
		this.AddToggleTooltip(this.scheduleInfo, null);
		this.reportsInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.REPORT, "OverviewUI_reports_icon", null, global::Action.ManageReport, UI.TOOLTIPS.MANAGEMENTMENU_DAILYREPORT, "");
		this.AddToggleTooltip(this.reportsInfo, null);
		this.reportsInfo.prefabOverride = this.smallPrefab;
		this.codexInfo = new ManagementMenu.ManagementMenuToggleInfo(UI.CODEX.MANAGEMENT_BUTTON, "OverviewUI_database_icon", null, global::Action.ManageDatabase, UI.TOOLTIPS.MANAGEMENTMENU_CODEX, "");
		this.AddToggleTooltip(this.codexInfo, null);
		this.codexInfo.prefabOverride = this.smallPrefab;
		this.ScreenInfoMatch.Add(this.consumablesInfo, new ManagementMenu.ScreenData
		{
			screen = this.consumablesScreen,
			tabIdx = 3,
			toggleInfo = this.consumablesInfo,
			cancelHandler = null
		});
		this.ScreenInfoMatch.Add(this.vitalsInfo, new ManagementMenu.ScreenData
		{
			screen = this.vitalsScreen,
			tabIdx = 2,
			toggleInfo = this.vitalsInfo,
			cancelHandler = null
		});
		this.ScreenInfoMatch.Add(this.reportsInfo, new ManagementMenu.ScreenData
		{
			screen = this.reportsScreen,
			tabIdx = 4,
			toggleInfo = this.reportsInfo,
			cancelHandler = null
		});
		this.ScreenInfoMatch.Add(this.jobsInfo, new ManagementMenu.ScreenData
		{
			screen = this.jobsScreen,
			tabIdx = 1,
			toggleInfo = this.jobsInfo,
			cancelHandler = null
		});
		this.ScreenInfoMatch.Add(this.skillsInfo, new ManagementMenu.ScreenData
		{
			screen = this.skillsScreen,
			tabIdx = 0,
			toggleInfo = this.skillsInfo,
			cancelHandler = null
		});
		this.ScreenInfoMatch.Add(this.codexInfo, new ManagementMenu.ScreenData
		{
			screen = this.codexScreen,
			tabIdx = 6,
			toggleInfo = this.codexInfo,
			cancelHandler = null
		});
		this.ScreenInfoMatch.Add(this.scheduleInfo, new ManagementMenu.ScreenData
		{
			screen = this.scheduleScreen,
			tabIdx = 7,
			toggleInfo = this.scheduleInfo,
			cancelHandler = null
		});
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			this.ScreenInfoMatch.Add(this.clusterMapInfo, new ManagementMenu.ScreenData
			{
				screen = this.clusterMapScreen,
				tabIdx = 7,
				toggleInfo = this.clusterMapInfo,
				cancelHandler = new Func<bool>(this.clusterMapScreen.TryHandleCancel)
			});
		}
		else
		{
			this.ScreenInfoMatch.Add(this.starmapInfo, new ManagementMenu.ScreenData
			{
				screen = this.starmapScreen,
				tabIdx = 7,
				toggleInfo = this.starmapInfo,
				cancelHandler = null
			});
		}
		this.ScreenInfoMatch.Add(this.researchInfo, new ManagementMenu.ScreenData
		{
			screen = this.researchScreen,
			tabIdx = 5,
			toggleInfo = this.researchInfo,
			cancelHandler = null
		});
		List<KIconToggleMenu.ToggleInfo> list = new List<KIconToggleMenu.ToggleInfo>();
		list.Add(this.vitalsInfo);
		list.Add(this.consumablesInfo);
		list.Add(this.scheduleInfo);
		list.Add(this.jobsInfo);
		list.Add(this.skillsInfo);
		list.Add(this.researchInfo);
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			list.Add(this.clusterMapInfo);
		}
		else
		{
			list.Add(this.starmapInfo);
		}
		list.Add(this.reportsInfo);
		list.Add(this.codexInfo);
		base.Setup(list);
		base.onSelect += this.OnButtonClick;
		this.PauseMenuButton.onClick += this.OnPauseMenuClicked;
		this.PauseMenuButton.transform.SetAsLastSibling();
		this.PauseMenuButton.GetComponent<ToolTip>().toolTip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_PAUSEMENU, global::Action.Escape);
		KInputManager.InputChange.AddListener(new UnityAction(this.OnInputChanged));
		Components.ResearchCenters.OnAdd += new Action<IResearchCenter>(this.CheckResearch);
		Components.ResearchCenters.OnRemove += new Action<IResearchCenter>(this.CheckResearch);
		Components.RoleStations.OnAdd += new Action<RoleStation>(this.CheckSkills);
		Components.RoleStations.OnRemove += new Action<RoleStation>(this.CheckSkills);
		Game.Instance.Subscribe(-809948329, new Action<object>(this.CheckResearch));
		Game.Instance.Subscribe(-809948329, new Action<object>(this.CheckSkills));
		Game.Instance.Subscribe(445618876, new Action<object>(this.OnResolutionChanged));
		if (!DlcManager.FeatureClusterSpaceEnabled())
		{
			Components.Telescopes.OnAdd += new Action<Telescope>(this.CheckStarmap);
			Components.Telescopes.OnRemove += new Action<Telescope>(this.CheckStarmap);
		}
		this.CheckResearch(null);
		this.CheckSkills(null);
		if (!DlcManager.FeatureClusterSpaceEnabled())
		{
			this.CheckStarmap(null);
		}
		this.researchInfo.toggle.soundPlayer.AcceptClickCondition = () => this.ResearchAvailable() || this.activeScreen == this.ScreenInfoMatch[ManagementMenu.Instance.researchInfo];
		foreach (KToggle ktoggle in this.toggles)
		{
			ktoggle.soundPlayer.toggle_widget_sound_events[0].PlaySound = false;
			ktoggle.soundPlayer.toggle_widget_sound_events[1].PlaySound = false;
		}
		this.OnResolutionChanged(null);
	}

	// Token: 0x06004D71 RID: 19825 RVA: 0x001B5294 File Offset: 0x001B3494
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.mutuallyExclusiveScreens.Add(AllResourcesScreen.Instance);
		this.mutuallyExclusiveScreens.Add(AllDiagnosticsScreen.Instance);
		this.OnNotificationsChanged();
	}

	// Token: 0x06004D72 RID: 19826 RVA: 0x001B52C2 File Offset: 0x001B34C2
	protected override void OnForcedCleanUp()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(this.OnInputChanged));
		base.OnForcedCleanUp();
	}

	// Token: 0x06004D73 RID: 19827 RVA: 0x001B52E0 File Offset: 0x001B34E0
	private void OnInputChanged()
	{
		this.PauseMenuButton.GetComponent<ToolTip>().toolTip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_PAUSEMENU, global::Action.Escape);
		this.consumablesInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_CONSUMABLES, this.consumablesInfo.hotKey);
		this.vitalsInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_VITALS, this.vitalsInfo.hotKey);
		this.researchInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_RESEARCH, this.researchInfo.hotKey);
		this.jobsInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_JOBS, this.jobsInfo.hotKey);
		this.skillsInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_SKILLS, this.skillsInfo.hotKey);
		this.starmapInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_STARMAP, this.starmapInfo.hotKey);
		this.clusterMapInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_STARMAP, this.clusterMapInfo.hotKey);
		this.scheduleInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_SCHEDULE, this.scheduleInfo.hotKey);
		this.reportsInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_DAILYREPORT, this.reportsInfo.hotKey);
		this.codexInfo.tooltip = GameUtil.ReplaceHotkeyString(UI.TOOLTIPS.MANAGEMENTMENU_CODEX, this.codexInfo.hotKey);
	}

	// Token: 0x06004D74 RID: 19828 RVA: 0x001B5480 File Offset: 0x001B3680
	private void OnResolutionChanged(object data = null)
	{
		bool flag = (float)Screen.width < 1300f;
		foreach (KToggle ktoggle in this.toggles)
		{
			HierarchyReferences component = ktoggle.GetComponent<HierarchyReferences>();
			if (!(component == null))
			{
				RectTransform reference = component.GetReference<RectTransform>("TextContainer");
				if (!(reference == null))
				{
					reference.gameObject.SetActive(!flag);
				}
			}
		}
	}

	// Token: 0x06004D75 RID: 19829 RVA: 0x001B550C File Offset: 0x001B370C
	private void OnNotificationsChanged()
	{
		foreach (KeyValuePair<ManagementMenu.ManagementMenuToggleInfo, ManagementMenu.ScreenData> keyValuePair in this.ScreenInfoMatch)
		{
			keyValuePair.Key.SetNotificationDisplay(false, false, null, this.noAlertColorStyle);
		}
	}

	// Token: 0x06004D76 RID: 19830 RVA: 0x001B5570 File Offset: 0x001B3770
	private void AddToggleTooltip(ManagementMenu.ManagementMenuToggleInfo toggleInfo, string disabledTooltip = null)
	{
		toggleInfo.getTooltipText = delegate
		{
			List<global::Tuple<string, TextStyleSetting>> list = new List<global::Tuple<string, TextStyleSetting>>();
			if (disabledTooltip != null && !toggleInfo.toggle.interactable)
			{
				list.Add(new global::Tuple<string, TextStyleSetting>(disabledTooltip, ToolTipScreen.Instance.defaultTooltipBodyStyle));
				return list;
			}
			if (toggleInfo.tooltipHeader != null)
			{
				list.Add(new global::Tuple<string, TextStyleSetting>(toggleInfo.tooltipHeader, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
			}
			list.Add(new global::Tuple<string, TextStyleSetting>(toggleInfo.tooltip, ToolTipScreen.Instance.defaultTooltipBodyStyle));
			return list;
		};
	}

	// Token: 0x06004D77 RID: 19831 RVA: 0x001B55A8 File Offset: 0x001B37A8
	public bool IsFullscreenUIActive()
	{
		if (this.activeScreen == null)
		{
			return false;
		}
		foreach (ManagementMenu.ManagementMenuToggleInfo managementMenuToggleInfo in this.fullscreenUIs)
		{
			if (this.activeScreen.toggleInfo == managementMenuToggleInfo)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004D78 RID: 19832 RVA: 0x001B55E9 File Offset: 0x001B37E9
	private void OnPauseMenuClicked()
	{
		PauseScreen.Instance.Show(true);
		this.PauseMenuButton.isOn = false;
	}

	// Token: 0x06004D79 RID: 19833 RVA: 0x001B5604 File Offset: 0x001B3804
	public void CheckResearch(object o)
	{
		if (this.researchInfo.toggle == null)
		{
			return;
		}
		bool flag = Components.ResearchCenters.Count <= 0 && !DebugHandler.InstantBuildMode;
		bool flag2 = !flag && this.activeScreen != null && this.activeScreen.toggleInfo == this.researchInfo;
		this.ConfigureToggle(this.researchInfo.toggle, flag, flag2);
	}

	// Token: 0x06004D7A RID: 19834 RVA: 0x001B5674 File Offset: 0x001B3874
	public void CheckSkills(object o = null)
	{
		if (this.skillsInfo.toggle == null)
		{
			return;
		}
		bool flag = Components.RoleStations.Count <= 0 && !DebugHandler.InstantBuildMode;
		bool flag2 = this.activeScreen != null && this.activeScreen.toggleInfo == this.skillsInfo;
		this.ConfigureToggle(this.skillsInfo.toggle, flag, flag2);
	}

	// Token: 0x06004D7B RID: 19835 RVA: 0x001B56E0 File Offset: 0x001B38E0
	public void CheckStarmap(object o = null)
	{
		if (this.starmapInfo.toggle == null)
		{
			return;
		}
		bool flag = Components.Telescopes.Count <= 0 && !DebugHandler.InstantBuildMode;
		bool flag2 = this.activeScreen != null && this.activeScreen.toggleInfo == this.starmapInfo;
		this.ConfigureToggle(this.starmapInfo.toggle, flag, flag2);
	}

	// Token: 0x06004D7C RID: 19836 RVA: 0x001B574C File Offset: 0x001B394C
	private void ConfigureToggle(KToggle toggle, bool disabled, bool active)
	{
		toggle.interactable = !disabled;
		if (disabled)
		{
			toggle.GetComponentInChildren<ImageToggleState>().SetDisabled();
			return;
		}
		toggle.GetComponentInChildren<ImageToggleState>().SetActiveState(active);
	}

	// Token: 0x06004D7D RID: 19837 RVA: 0x001B5773 File Offset: 0x001B3973
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.activeScreen != null && e.TryConsume(global::Action.Escape))
		{
			this.ToggleIfCancelUnhandled(this.activeScreen);
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06004D7E RID: 19838 RVA: 0x001B57A1 File Offset: 0x001B39A1
	public override void OnKeyUp(KButtonEvent e)
	{
		if (this.activeScreen != null && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			this.ToggleIfCancelUnhandled(this.activeScreen);
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06004D7F RID: 19839 RVA: 0x001B57D4 File Offset: 0x001B39D4
	private void ToggleIfCancelUnhandled(ManagementMenu.ScreenData screenData)
	{
		if (screenData.cancelHandler == null || !screenData.cancelHandler())
		{
			this.ToggleScreen(screenData);
		}
	}

	// Token: 0x06004D80 RID: 19840 RVA: 0x001B57F2 File Offset: 0x001B39F2
	private bool ResearchAvailable()
	{
		return Components.ResearchCenters.Count > 0 || DebugHandler.InstantBuildMode;
	}

	// Token: 0x06004D81 RID: 19841 RVA: 0x001B5808 File Offset: 0x001B3A08
	private bool SkillsAvailable()
	{
		return Components.RoleStations.Count > 0 || DebugHandler.InstantBuildMode;
	}

	// Token: 0x06004D82 RID: 19842 RVA: 0x001B581E File Offset: 0x001B3A1E
	public static bool StarmapAvailable()
	{
		return Components.Telescopes.Count > 0 || DebugHandler.InstantBuildMode;
	}

	// Token: 0x06004D83 RID: 19843 RVA: 0x001B5834 File Offset: 0x001B3A34
	public void CloseAll()
	{
		if (this.activeScreen == null)
		{
			return;
		}
		if (this.activeScreen.toggleInfo != null)
		{
			this.ToggleScreen(this.activeScreen);
		}
		this.CloseActive();
		this.ClearSelection();
	}

	// Token: 0x06004D84 RID: 19844 RVA: 0x001B5864 File Offset: 0x001B3A64
	private void OnUIClear(object data)
	{
		this.CloseAll();
	}

	// Token: 0x06004D85 RID: 19845 RVA: 0x001B586C File Offset: 0x001B3A6C
	public void ToggleScreen(ManagementMenu.ScreenData screenData)
	{
		if (screenData == null)
		{
			return;
		}
		if (screenData.toggleInfo == this.researchInfo && !this.ResearchAvailable())
		{
			this.CheckResearch(null);
			this.CloseActive();
			return;
		}
		if (screenData.toggleInfo == this.skillsInfo && !this.SkillsAvailable())
		{
			this.CheckSkills(null);
			this.CloseActive();
			return;
		}
		if (screenData.toggleInfo == this.starmapInfo && !ManagementMenu.StarmapAvailable())
		{
			this.CheckStarmap(null);
			this.CloseActive();
			return;
		}
		if (screenData.toggleInfo.toggle.gameObject.GetComponentInChildren<ImageToggleState>().IsDisabled)
		{
			return;
		}
		if (this.activeScreen != null)
		{
			this.activeScreen.toggleInfo.toggle.isOn = false;
			this.activeScreen.toggleInfo.toggle.gameObject.GetComponentInChildren<ImageToggleState>().SetInactive();
		}
		if (this.activeScreen != screenData)
		{
			OverlayScreen.Instance.ToggleOverlay(OverlayModes.None.ID, true);
			if (this.activeScreen != null)
			{
				this.activeScreen.toggleInfo.toggle.ActivateFlourish(false);
			}
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Open", false));
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().MenuOpenMigrated);
			screenData.toggleInfo.toggle.ActivateFlourish(true);
			screenData.toggleInfo.toggle.gameObject.GetComponentInChildren<ImageToggleState>().SetActive();
			this.CloseActive();
			this.activeScreen = screenData;
			if (!this.activeScreen.screen.IsActive())
			{
				this.activeScreen.screen.Activate();
			}
			this.activeScreen.screen.Show(true);
			foreach (ManagementMenuNotification managementMenuNotification in this.notificationDisplayer.GetNotificationsForAction(screenData.toggleInfo.hotKey))
			{
				if (managementMenuNotification.customClickCallback != null)
				{
					managementMenuNotification.customClickCallback(managementMenuNotification.customClickData);
					break;
				}
			}
			using (List<KScreen>.Enumerator enumerator2 = this.mutuallyExclusiveScreens.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KScreen kscreen = enumerator2.Current;
					kscreen.Show(false);
				}
				return;
			}
		}
		this.activeScreen.screen.Show(false);
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().MenuOpenMigrated, STOP_MODE.ALLOWFADEOUT);
		this.activeScreen.toggleInfo.toggle.ActivateFlourish(false);
		this.activeScreen = null;
		screenData.toggleInfo.toggle.gameObject.GetComponentInChildren<ImageToggleState>().SetInactive();
	}

	// Token: 0x06004D86 RID: 19846 RVA: 0x001B5B24 File Offset: 0x001B3D24
	public void OnButtonClick(KIconToggleMenu.ToggleInfo toggle_info)
	{
		this.ToggleScreen(this.ScreenInfoMatch[(ManagementMenu.ManagementMenuToggleInfo)toggle_info]);
	}

	// Token: 0x06004D87 RID: 19847 RVA: 0x001B5B3D File Offset: 0x001B3D3D
	private void CloseActive()
	{
		if (this.activeScreen != null)
		{
			this.activeScreen.toggleInfo.toggle.isOn = false;
			this.activeScreen.screen.Show(false);
			this.activeScreen = null;
		}
	}

	// Token: 0x06004D88 RID: 19848 RVA: 0x001B5B78 File Offset: 0x001B3D78
	public void ToggleResearch()
	{
		if ((this.ResearchAvailable() || this.activeScreen == this.ScreenInfoMatch[ManagementMenu.Instance.researchInfo]) && this.researchInfo != null)
		{
			this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.researchInfo]);
		}
	}

	// Token: 0x06004D89 RID: 19849 RVA: 0x001B5BCD File Offset: 0x001B3DCD
	public void ToggleCodex()
	{
		this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.codexInfo]);
	}

	// Token: 0x06004D8A RID: 19850 RVA: 0x001B5BEC File Offset: 0x001B3DEC
	public void OpenCodexToEntry(string id, ContentContainer targetContainer = null)
	{
		if (!this.codexScreen.gameObject.activeInHierarchy)
		{
			this.ToggleCodex();
		}
		this.codexScreen.ChangeArticle(id, false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
		this.codexScreen.FocusContainer(targetContainer);
	}

	// Token: 0x06004D8B RID: 19851 RVA: 0x001B5C34 File Offset: 0x001B3E34
	public void ToggleSkills()
	{
		if ((this.SkillsAvailable() || this.activeScreen == this.ScreenInfoMatch[ManagementMenu.Instance.skillsInfo]) && this.skillsInfo != null)
		{
			this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.skillsInfo]);
		}
	}

	// Token: 0x06004D8C RID: 19852 RVA: 0x001B5C89 File Offset: 0x001B3E89
	public void ToggleStarmap()
	{
		if (this.starmapInfo != null)
		{
			this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.starmapInfo]);
		}
	}

	// Token: 0x06004D8D RID: 19853 RVA: 0x001B5CAE File Offset: 0x001B3EAE
	public void ToggleClusterMap()
	{
		this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.clusterMapInfo]);
	}

	// Token: 0x06004D8E RID: 19854 RVA: 0x001B5CCB File Offset: 0x001B3ECB
	public void TogglePriorities()
	{
		this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.jobsInfo]);
	}

	// Token: 0x06004D8F RID: 19855 RVA: 0x001B5CE8 File Offset: 0x001B3EE8
	public void OpenReports(int day)
	{
		if (this.activeScreen != this.ScreenInfoMatch[ManagementMenu.Instance.reportsInfo])
		{
			this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.reportsInfo]);
		}
		ReportScreen.Instance.ShowReport(day);
	}

	// Token: 0x06004D90 RID: 19856 RVA: 0x001B5D38 File Offset: 0x001B3F38
	public void OpenResearch()
	{
		if (this.activeScreen != this.ScreenInfoMatch[ManagementMenu.Instance.researchInfo])
		{
			this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.researchInfo]);
		}
	}

	// Token: 0x06004D91 RID: 19857 RVA: 0x001B5D72 File Offset: 0x001B3F72
	public void OpenStarmap()
	{
		if (this.activeScreen != this.ScreenInfoMatch[ManagementMenu.Instance.starmapInfo])
		{
			this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.starmapInfo]);
		}
	}

	// Token: 0x06004D92 RID: 19858 RVA: 0x001B5DAC File Offset: 0x001B3FAC
	public void OpenClusterMap()
	{
		if (this.activeScreen != this.ScreenInfoMatch[ManagementMenu.Instance.clusterMapInfo])
		{
			this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.clusterMapInfo]);
		}
	}

	// Token: 0x06004D93 RID: 19859 RVA: 0x001B5DE6 File Offset: 0x001B3FE6
	public void CloseClusterMap()
	{
		if (this.activeScreen == this.ScreenInfoMatch[ManagementMenu.Instance.clusterMapInfo])
		{
			this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.clusterMapInfo]);
		}
	}

	// Token: 0x06004D94 RID: 19860 RVA: 0x001B5E20 File Offset: 0x001B4020
	public void OpenSkills(MinionIdentity minionIdentity)
	{
		this.skillsScreen.CurrentlySelectedMinion = minionIdentity;
		if (this.activeScreen != this.ScreenInfoMatch[ManagementMenu.Instance.skillsInfo])
		{
			this.ToggleScreen(this.ScreenInfoMatch[ManagementMenu.Instance.skillsInfo]);
		}
	}

	// Token: 0x06004D95 RID: 19861 RVA: 0x001B5E71 File Offset: 0x001B4071
	public bool IsScreenOpen(KScreen screen)
	{
		return this.activeScreen != null && this.activeScreen.screen == screen;
	}

	// Token: 0x040032FD RID: 13053
	private const float UI_WIDTH_COMPRESS_THRESHOLD = 1300f;

	// Token: 0x040032FE RID: 13054
	[MyCmpReq]
	public ManagementMenuNotificationDisplayer notificationDisplayer;

	// Token: 0x040032FF RID: 13055
	public static ManagementMenu Instance;

	// Token: 0x04003300 RID: 13056
	[Header("Management Menu Specific")]
	[SerializeField]
	private KToggle smallPrefab;

	// Token: 0x04003301 RID: 13057
	public KToggle PauseMenuButton;

	// Token: 0x04003302 RID: 13058
	[Header("Top Right Screen References")]
	public JobsTableScreen jobsScreen;

	// Token: 0x04003303 RID: 13059
	public VitalsTableScreen vitalsScreen;

	// Token: 0x04003304 RID: 13060
	public ScheduleScreen scheduleScreen;

	// Token: 0x04003305 RID: 13061
	public ReportScreen reportsScreen;

	// Token: 0x04003306 RID: 13062
	public CodexScreen codexScreen;

	// Token: 0x04003307 RID: 13063
	public ConsumablesTableScreen consumablesScreen;

	// Token: 0x04003308 RID: 13064
	private StarmapScreen starmapScreen;

	// Token: 0x04003309 RID: 13065
	private ClusterMapScreen clusterMapScreen;

	// Token: 0x0400330A RID: 13066
	private SkillsScreen skillsScreen;

	// Token: 0x0400330B RID: 13067
	private ResearchScreen researchScreen;

	// Token: 0x0400330C RID: 13068
	[Header("Notification Styles")]
	public ColorStyleSetting noAlertColorStyle;

	// Token: 0x0400330D RID: 13069
	public List<ColorStyleSetting> alertColorStyle;

	// Token: 0x0400330E RID: 13070
	public List<TextStyleSetting> alertTextStyle;

	// Token: 0x0400330F RID: 13071
	private ManagementMenu.ManagementMenuToggleInfo jobsInfo;

	// Token: 0x04003310 RID: 13072
	private ManagementMenu.ManagementMenuToggleInfo consumablesInfo;

	// Token: 0x04003311 RID: 13073
	private ManagementMenu.ManagementMenuToggleInfo scheduleInfo;

	// Token: 0x04003312 RID: 13074
	private ManagementMenu.ManagementMenuToggleInfo vitalsInfo;

	// Token: 0x04003313 RID: 13075
	private ManagementMenu.ManagementMenuToggleInfo reportsInfo;

	// Token: 0x04003314 RID: 13076
	private ManagementMenu.ManagementMenuToggleInfo researchInfo;

	// Token: 0x04003315 RID: 13077
	private ManagementMenu.ManagementMenuToggleInfo codexInfo;

	// Token: 0x04003316 RID: 13078
	private ManagementMenu.ManagementMenuToggleInfo starmapInfo;

	// Token: 0x04003317 RID: 13079
	private ManagementMenu.ManagementMenuToggleInfo clusterMapInfo;

	// Token: 0x04003318 RID: 13080
	private ManagementMenu.ManagementMenuToggleInfo skillsInfo;

	// Token: 0x04003319 RID: 13081
	private ManagementMenu.ManagementMenuToggleInfo[] fullscreenUIs;

	// Token: 0x0400331A RID: 13082
	private Dictionary<ManagementMenu.ManagementMenuToggleInfo, ManagementMenu.ScreenData> ScreenInfoMatch = new Dictionary<ManagementMenu.ManagementMenuToggleInfo, ManagementMenu.ScreenData>();

	// Token: 0x0400331B RID: 13083
	private ManagementMenu.ScreenData activeScreen;

	// Token: 0x0400331C RID: 13084
	private KButton activeButton;

	// Token: 0x0400331D RID: 13085
	private string skillsTooltip;

	// Token: 0x0400331E RID: 13086
	private string skillsTooltipDisabled;

	// Token: 0x0400331F RID: 13087
	private string researchTooltip;

	// Token: 0x04003320 RID: 13088
	private string researchTooltipDisabled;

	// Token: 0x04003321 RID: 13089
	private string starmapTooltip;

	// Token: 0x04003322 RID: 13090
	private string starmapTooltipDisabled;

	// Token: 0x04003323 RID: 13091
	private string clusterMapTooltip;

	// Token: 0x04003324 RID: 13092
	private string clusterMapTooltipDisabled;

	// Token: 0x04003325 RID: 13093
	private List<KScreen> mutuallyExclusiveScreens = new List<KScreen>();

	// Token: 0x02001820 RID: 6176
	public class ScreenData
	{
		// Token: 0x04006F39 RID: 28473
		public KScreen screen;

		// Token: 0x04006F3A RID: 28474
		public ManagementMenu.ManagementMenuToggleInfo toggleInfo;

		// Token: 0x04006F3B RID: 28475
		public Func<bool> cancelHandler;

		// Token: 0x04006F3C RID: 28476
		public int tabIdx;
	}

	// Token: 0x02001821 RID: 6177
	public class ManagementMenuToggleInfo : KIconToggleMenu.ToggleInfo
	{
		// Token: 0x06008CEF RID: 36079 RVA: 0x003044AF File Offset: 0x003026AF
		public ManagementMenuToggleInfo(string text, string icon, object user_data = null, global::Action hotkey = global::Action.NumActions, string tooltip = "", string tooltip_header = "")
			: base(text, icon, user_data, hotkey, tooltip, tooltip_header)
		{
			this.tooltip = GameUtil.ReplaceHotkeyString(this.tooltip, this.hotKey);
		}

		// Token: 0x06008CF0 RID: 36080 RVA: 0x003044D8 File Offset: 0x003026D8
		public void SetNotificationDisplay(bool showAlertImage, bool showGlow, ColorStyleSetting buttonColorStyle, ColorStyleSetting alertColorStyle)
		{
			ImageToggleState component = this.toggle.GetComponent<ImageToggleState>();
			if (component != null)
			{
				if (buttonColorStyle != null)
				{
					component.SetColorStyle(buttonColorStyle);
				}
				else
				{
					component.SetColorStyle(this.originalButtonSetting);
				}
			}
			if (this.alertImage != null)
			{
				this.alertImage.gameObject.SetActive(showAlertImage);
				this.alertImage.SetColorStyle(alertColorStyle);
			}
			if (this.glowImage != null)
			{
				this.glowImage.gameObject.SetActive(showGlow);
				if (buttonColorStyle != null)
				{
					this.glowImage.SetColorStyle(buttonColorStyle);
				}
			}
		}

		// Token: 0x06008CF1 RID: 36081 RVA: 0x00304578 File Offset: 0x00302778
		public override void SetToggle(KToggle toggle)
		{
			base.SetToggle(toggle);
			ImageToggleState component = toggle.GetComponent<ImageToggleState>();
			if (component != null)
			{
				this.originalButtonSetting = component.colorStyleSetting;
			}
			HierarchyReferences component2 = toggle.GetComponent<HierarchyReferences>();
			if (component2 != null)
			{
				this.alertImage = component2.GetReference<ImageToggleState>("AlertImage");
				this.glowImage = component2.GetReference<ImageToggleState>("GlowImage");
			}
		}

		// Token: 0x04006F3D RID: 28477
		public ImageToggleState alertImage;

		// Token: 0x04006F3E RID: 28478
		public ImageToggleState glowImage;

		// Token: 0x04006F3F RID: 28479
		private ColorStyleSetting originalButtonSetting;
	}
}
