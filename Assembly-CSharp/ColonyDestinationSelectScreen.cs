using System;
using Klei.CustomSettings;
using ProcGen;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000A74 RID: 2676
public class ColonyDestinationSelectScreen : NewGameFlowScreen
{
	// Token: 0x060051E0 RID: 20960 RVA: 0x001D9168 File Offset: 0x001D7368
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.backButton.onClick += this.BackClicked;
		this.customizeButton.onClick += this.CustomizeClicked;
		this.launchButton.onClick += this.LaunchClicked;
		this.shuffleButton.onClick += this.ShuffleClicked;
		this.storyTraitShuffleButton.onClick += this.StoryTraitShuffleClicked;
		this.storyTraitShuffleButton.gameObject.SetActive(Db.Get().Stories.Count > 3);
		this.destinationMapPanel.OnAsteroidClicked += this.OnAsteroidClicked;
		KInputTextField kinputTextField = this.coordinate;
		kinputTextField.onFocus = (System.Action)Delegate.Combine(kinputTextField.onFocus, new System.Action(this.CoordinateEditStarted));
		this.coordinate.onEndEdit.AddListener(new UnityAction<string>(this.CoordinateEditFinished));
		if (this.locationIcons != null)
		{
			bool cloudSavesAvailable = SaveLoader.GetCloudSavesAvailable();
			this.locationIcons.gameObject.SetActive(cloudSavesAvailable);
		}
		this.random = new KRandom();
	}

	// Token: 0x060051E1 RID: 20961 RVA: 0x001D929C File Offset: 0x001D749C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RefreshCloudSavePref();
		this.RefreshCloudLocalIcon();
		this.newGameSettings.Init();
		this.newGameSettings.SetCloseAction(new System.Action(this.CustomizeClose));
		this.destinationMapPanel.Init();
		CustomGameSettings.Instance.OnQualitySettingChanged += this.QualitySettingChanged;
		CustomGameSettings.Instance.OnStorySettingChanged += this.QualitySettingChanged;
		this.ShuffleClicked();
		this.RefreshMenuTabs();
		for (int i = 0; i < this.menuTabs.Length; i++)
		{
			int target = i;
			this.menuTabs[i].onClick = delegate
			{
				this.selectedMenuTabIdx = target;
				this.RefreshMenuTabs();
			};
		}
		this.ResizeLayout();
		this.storyContentPanel.Init();
		this.storyContentPanel.SelectRandomStories(3, 3, true);
		this.storyContentPanel.SelectDefault();
		this.RefreshStoryLabel();
		this.RefreshRowsAndDescriptions();
	}

	// Token: 0x060051E2 RID: 20962 RVA: 0x001D9398 File Offset: 0x001D7598
	private void ResizeLayout()
	{
		Vector2 sizeDelta = this.destinationProperties.clusterDetailsButton.rectTransform().sizeDelta;
		this.destinationProperties.clusterDetailsButton.rectTransform().sizeDelta = new Vector2(sizeDelta.x, (float)(DlcManager.FeatureClusterSpaceEnabled() ? 164 : 76));
		Vector2 sizeDelta2 = this.worldsScrollPanel.rectTransform().sizeDelta;
		Vector2 anchoredPosition = this.worldsScrollPanel.rectTransform().anchoredPosition;
		if (!DlcManager.FeatureClusterSpaceEnabled())
		{
			this.worldsScrollPanel.rectTransform().anchoredPosition = new Vector2(anchoredPosition.x, anchoredPosition.y + 88f);
		}
		float num = (float)(DlcManager.FeatureClusterSpaceEnabled() ? 436 : 524);
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.gameObject.rectTransform());
		num = Mathf.Min(num, this.destinationInfoPanel.sizeDelta.y - (float)(DlcManager.FeatureClusterSpaceEnabled() ? 164 : 76) - 22f);
		this.worldsScrollPanel.rectTransform().sizeDelta = new Vector2(sizeDelta2.x, num);
		this.storyScrollPanel.rectTransform().sizeDelta = new Vector2(sizeDelta2.x, num);
	}

	// Token: 0x060051E3 RID: 20963 RVA: 0x001D94C8 File Offset: 0x001D76C8
	protected override void OnCleanUp()
	{
		CustomGameSettings.Instance.OnQualitySettingChanged -= this.QualitySettingChanged;
		CustomGameSettings.Instance.OnStorySettingChanged -= this.QualitySettingChanged;
		this.storyContentPanel.Cleanup();
		base.OnCleanUp();
	}

	// Token: 0x060051E4 RID: 20964 RVA: 0x001D9508 File Offset: 0x001D7708
	private void RefreshCloudLocalIcon()
	{
		if (this.locationIcons == null)
		{
			return;
		}
		if (!SaveLoader.GetCloudSavesAvailable())
		{
			return;
		}
		HierarchyReferences component = this.locationIcons.GetComponent<HierarchyReferences>();
		LocText component2 = component.GetReference<RectTransform>("LocationText").GetComponent<LocText>();
		KButton component3 = component.GetReference<RectTransform>("CloudButton").GetComponent<KButton>();
		KButton component4 = component.GetReference<RectTransform>("LocalButton").GetComponent<KButton>();
		ToolTip component5 = component3.GetComponent<ToolTip>();
		ToolTip component6 = component4.GetComponent<ToolTip>();
		component5.toolTip = string.Format("{0}\n{1}", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SAVETOCLOUD.TOOLTIP, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SAVETOCLOUD.TOOLTIP_EXTRA);
		component6.toolTip = string.Format("{0}\n{1}", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SAVETOCLOUD.TOOLTIP_LOCAL, UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.SAVETOCLOUD.TOOLTIP_EXTRA);
		bool flag = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.SaveToCloud).id == "Enabled";
		component2.text = (flag ? UI.FRONTEND.LOADSCREEN.CLOUD_SAVE : UI.FRONTEND.LOADSCREEN.LOCAL_SAVE);
		component3.gameObject.SetActive(flag);
		component3.ClearOnClick();
		if (flag)
		{
			component3.onClick += delegate
			{
				CustomGameSettings.Instance.SetQualitySetting(CustomGameSettingConfigs.SaveToCloud, "Disabled");
				this.RefreshCloudLocalIcon();
			};
		}
		component4.gameObject.SetActive(!flag);
		component4.ClearOnClick();
		if (!flag)
		{
			component4.onClick += delegate
			{
				CustomGameSettings.Instance.SetQualitySetting(CustomGameSettingConfigs.SaveToCloud, "Enabled");
				this.RefreshCloudLocalIcon();
			};
		}
	}

	// Token: 0x060051E5 RID: 20965 RVA: 0x001D963C File Offset: 0x001D783C
	private void RefreshCloudSavePref()
	{
		if (!SaveLoader.GetCloudSavesAvailable())
		{
			return;
		}
		string cloudSavesDefaultPref = SaveLoader.GetCloudSavesDefaultPref();
		CustomGameSettings.Instance.SetQualitySetting(CustomGameSettingConfigs.SaveToCloud, cloudSavesDefaultPref);
	}

	// Token: 0x060051E6 RID: 20966 RVA: 0x001D9667 File Offset: 0x001D7867
	private void BackClicked()
	{
		this.newGameSettings.Cancel();
		base.NavigateBackward();
	}

	// Token: 0x060051E7 RID: 20967 RVA: 0x001D967A File Offset: 0x001D787A
	private void CustomizeClicked()
	{
		this.newGameSettings.Refresh();
		this.customSettings.SetActive(true);
	}

	// Token: 0x060051E8 RID: 20968 RVA: 0x001D9693 File Offset: 0x001D7893
	private void CustomizeClose()
	{
		this.customSettings.SetActive(false);
	}

	// Token: 0x060051E9 RID: 20969 RVA: 0x001D96A1 File Offset: 0x001D78A1
	private void LaunchClicked()
	{
		base.NavigateForward();
	}

	// Token: 0x060051EA RID: 20970 RVA: 0x001D96AC File Offset: 0x001D78AC
	private void RefreshMenuTabs()
	{
		for (int i = 0; i < this.menuTabs.Length; i++)
		{
			this.menuTabs[i].ChangeState((i == this.selectedMenuTabIdx) ? 1 : 0);
			this.menuTabs[i].GetComponentInChildren<LocText>().color = ((i == this.selectedMenuTabIdx) ? Color.white : Color.grey);
		}
		this.destinationInfoPanel.gameObject.SetActive(this.selectedMenuTabIdx == 0);
		this.storyInfoPanel.gameObject.SetActive(this.selectedMenuTabIdx == 1);
		this.destinationDetailsHeader.SetParent((this.selectedMenuTabIdx == 0) ? this.destinationDetailsParent_Asteroid : this.destinationDetailsParent_Story);
		this.destinationDetailsHeader.SetAsFirstSibling();
	}

	// Token: 0x060051EB RID: 20971 RVA: 0x001D976C File Offset: 0x001D796C
	private void ShuffleClicked()
	{
		int num = this.random.Next();
		this.newGameSettings.SetSetting(CustomGameSettingConfigs.WorldgenSeed, num.ToString());
	}

	// Token: 0x060051EC RID: 20972 RVA: 0x001D979C File Offset: 0x001D799C
	private void StoryTraitShuffleClicked()
	{
		this.storyContentPanel.SelectRandomStories(3, 3, false);
	}

	// Token: 0x060051ED RID: 20973 RVA: 0x001D97AC File Offset: 0x001D79AC
	private void CoordinateChanged(string text)
	{
		string[] array = CustomGameSettings.ParseSettingCoordinate(text);
		if (array.Length != 4 && array.Length != 5)
		{
			return;
		}
		int num;
		if (!int.TryParse(array[2], out num))
		{
			return;
		}
		ClusterLayout clusterLayout = null;
		foreach (string text2 in SettingsCache.GetClusterNames())
		{
			ClusterLayout clusterData = SettingsCache.clusterLayouts.GetClusterData(text2);
			if (clusterData.coordinatePrefix == array[1])
			{
				clusterLayout = clusterData;
			}
		}
		if (clusterLayout != null)
		{
			this.newGameSettings.SetSetting(CustomGameSettingConfigs.ClusterLayout, clusterLayout.filePath);
		}
		this.newGameSettings.SetSetting(CustomGameSettingConfigs.WorldgenSeed, array[2]);
		this.newGameSettings.ConsumeSettingsCode(array[3]);
		string text3 = ((array.Length >= 5) ? array[4] : "0");
		this.newGameSettings.ConsumeStoryTraitsCode(text3);
	}

	// Token: 0x060051EE RID: 20974 RVA: 0x001D9898 File Offset: 0x001D7A98
	private void CoordinateEditStarted()
	{
		this.isEditingCoordinate = true;
	}

	// Token: 0x060051EF RID: 20975 RVA: 0x001D98A1 File Offset: 0x001D7AA1
	private void CoordinateEditFinished(string text)
	{
		this.CoordinateChanged(text);
		this.isEditingCoordinate = false;
		this.coordinate.text = CustomGameSettings.Instance.GetSettingsCoordinate();
	}

	// Token: 0x060051F0 RID: 20976 RVA: 0x001D98C6 File Offset: 0x001D7AC6
	private void QualitySettingChanged(SettingConfig config, SettingLevel level)
	{
		if (config == CustomGameSettingConfigs.SaveToCloud)
		{
			this.RefreshCloudLocalIcon();
		}
		if (!this.isEditingCoordinate)
		{
			this.coordinate.text = CustomGameSettings.Instance.GetSettingsCoordinate();
		}
		this.RefreshRowsAndDescriptions();
	}

	// Token: 0x060051F1 RID: 20977 RVA: 0x001D98FC File Offset: 0x001D7AFC
	public void RefreshRowsAndDescriptions()
	{
		string setting = this.newGameSettings.GetSetting(CustomGameSettingConfigs.ClusterLayout);
		string setting2 = this.newGameSettings.GetSetting(CustomGameSettingConfigs.WorldgenSeed);
		this.destinationMapPanel.UpdateDisplayedClusters();
		int num;
		int.TryParse(setting2, out num);
		ColonyDestinationAsteroidBeltData cluster;
		try
		{
			cluster = this.destinationMapPanel.SelectCluster(setting, num);
		}
		catch
		{
			string defaultAsteroid = this.destinationMapPanel.GetDefaultAsteroid();
			this.newGameSettings.SetSetting(CustomGameSettingConfigs.ClusterLayout, defaultAsteroid);
			cluster = this.destinationMapPanel.SelectCluster(defaultAsteroid, num);
		}
		if (DlcManager.IsContentActive("EXPANSION1_ID"))
		{
			this.destinationProperties.EnableClusterLocationLabels(true);
			this.destinationProperties.RefreshAsteroidLines(cluster, this.selectedLocationProperties, this.storyContentPanel.GetActiveStories());
			this.destinationProperties.EnableClusterDetails(true);
			this.destinationProperties.SetClusterDetailLabels(cluster);
			this.selectedLocationProperties.headerLabel.SetText(UI.FRONTEND.COLONYDESTINATIONSCREEN.SELECTED_CLUSTER_TRAITS_HEADER);
			this.destinationProperties.clusterDetailsButton.onClick = delegate
			{
				this.destinationProperties.SelectWholeClusterDetails(cluster, this.selectedLocationProperties, this.storyContentPanel.GetActiveStories());
			};
		}
		else
		{
			this.destinationProperties.EnableClusterDetails(false);
			this.destinationProperties.EnableClusterLocationLabels(false);
			this.destinationProperties.SetParameterDescriptors(cluster.GetParamDescriptors());
			this.selectedLocationProperties.SetTraitDescriptors(cluster.GetTraitDescriptors(), this.storyContentPanel.GetActiveStories(), true);
		}
		this.RefreshStoryLabel();
	}

	// Token: 0x060051F2 RID: 20978 RVA: 0x001D9A8C File Offset: 0x001D7C8C
	public void RefreshStoryLabel()
	{
		this.storyTraitsDestinationDetailsLabel.SetText(this.storyContentPanel.GetTraitsString(false));
		this.storyTraitsDestinationDetailsLabel.GetComponent<ToolTip>().SetSimpleTooltip(this.storyContentPanel.GetTraitsString(true));
	}

	// Token: 0x060051F3 RID: 20979 RVA: 0x001D9AC1 File Offset: 0x001D7CC1
	private void OnAsteroidClicked(ColonyDestinationAsteroidBeltData cluster)
	{
		this.newGameSettings.SetSetting(CustomGameSettingConfigs.ClusterLayout, cluster.beltPath);
		this.ShuffleClicked();
	}

	// Token: 0x060051F4 RID: 20980 RVA: 0x001D9AE0 File Offset: 0x001D7CE0
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.isEditingCoordinate)
		{
			return;
		}
		if (!e.Consumed && e.TryConsume(global::Action.PanLeft))
		{
			this.destinationMapPanel.ScrollLeft();
		}
		else if (!e.Consumed && e.TryConsume(global::Action.PanRight))
		{
			this.destinationMapPanel.ScrollRight();
		}
		else if (this.customSettings.activeSelf && !e.Consumed && (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight)))
		{
			this.CustomizeClose();
		}
		base.OnKeyDown(e);
	}

	// Token: 0x040036DE RID: 14046
	[SerializeField]
	private GameObject destinationMap;

	// Token: 0x040036DF RID: 14047
	[SerializeField]
	private GameObject customSettings;

	// Token: 0x040036E0 RID: 14048
	[SerializeField]
	private MultiToggle[] menuTabs;

	// Token: 0x040036E1 RID: 14049
	private int selectedMenuTabIdx;

	// Token: 0x040036E2 RID: 14050
	[SerializeField]
	private KButton backButton;

	// Token: 0x040036E3 RID: 14051
	[SerializeField]
	private KButton customizeButton;

	// Token: 0x040036E4 RID: 14052
	[SerializeField]
	private KButton launchButton;

	// Token: 0x040036E5 RID: 14053
	[SerializeField]
	private KButton shuffleButton;

	// Token: 0x040036E6 RID: 14054
	[SerializeField]
	private KButton storyTraitShuffleButton;

	// Token: 0x040036E7 RID: 14055
	[SerializeField]
	private HierarchyReferences locationIcons;

	// Token: 0x040036E8 RID: 14056
	[SerializeField]
	private RectTransform worldsScrollPanel;

	// Token: 0x040036E9 RID: 14057
	[SerializeField]
	private RectTransform storyScrollPanel;

	// Token: 0x040036EA RID: 14058
	[SerializeField]
	private RectTransform destinationDetailsParent_Asteroid;

	// Token: 0x040036EB RID: 14059
	[SerializeField]
	private RectTransform destinationDetailsParent_Story;

	// Token: 0x040036EC RID: 14060
	[SerializeField]
	private LocText storyTraitsDestinationDetailsLabel;

	// Token: 0x040036ED RID: 14061
	private const int DESTINATION_HEADER_BUTTON_HEIGHT_CLUSTER = 164;

	// Token: 0x040036EE RID: 14062
	private const int DESTINATION_HEADER_BUTTON_HEIGHT_BASE = 76;

	// Token: 0x040036EF RID: 14063
	private const int WORLDS_SCROLL_PANEL_HEIGHT_CLUSTER = 436;

	// Token: 0x040036F0 RID: 14064
	private const int WORLDS_SCROLL_PANEL_HEIGHT_BASE = 524;

	// Token: 0x040036F1 RID: 14065
	[SerializeField]
	private AsteroidDescriptorPanel destinationProperties;

	// Token: 0x040036F2 RID: 14066
	[SerializeField]
	private AsteroidDescriptorPanel selectedLocationProperties;

	// Token: 0x040036F3 RID: 14067
	[SerializeField]
	private KInputTextField coordinate;

	// Token: 0x040036F4 RID: 14068
	[SerializeField]
	private RectTransform destinationDetailsHeader;

	// Token: 0x040036F5 RID: 14069
	[SerializeField]
	private RectTransform destinationInfoPanel;

	// Token: 0x040036F6 RID: 14070
	[SerializeField]
	private RectTransform storyInfoPanel;

	// Token: 0x040036F7 RID: 14071
	[MyCmpReq]
	public NewGameSettingsPanel newGameSettings;

	// Token: 0x040036F8 RID: 14072
	[MyCmpReq]
	private DestinationSelectPanel destinationMapPanel;

	// Token: 0x040036F9 RID: 14073
	[SerializeField]
	private StoryContentPanel storyContentPanel;

	// Token: 0x040036FA RID: 14074
	private KRandom random;

	// Token: 0x040036FB RID: 14075
	private bool isEditingCoordinate;
}
