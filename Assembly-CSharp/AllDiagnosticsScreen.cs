using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A2F RID: 2607
public class AllDiagnosticsScreen : KScreen, ISim4000ms, ISim1000ms
{
	// Token: 0x06004F1B RID: 20251 RVA: 0x001C25D1 File Offset: 0x001C07D1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		AllDiagnosticsScreen.Instance = this;
		this.ConfigureDebugToggle();
	}

	// Token: 0x06004F1C RID: 20252 RVA: 0x001C25E5 File Offset: 0x001C07E5
	protected override void OnForcedCleanUp()
	{
		AllDiagnosticsScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06004F1D RID: 20253 RVA: 0x001C25F4 File Offset: 0x001C07F4
	private void ConfigureDebugToggle()
	{
		Game.Instance.Subscribe(1557339983, new Action<object>(this.DebugToggleRefresh));
		MultiToggle toggle = this.debugNotificationToggleCotainer.GetComponentInChildren<MultiToggle>();
		MultiToggle toggle2 = toggle;
		toggle2.onClick = (System.Action)Delegate.Combine(toggle2.onClick, new System.Action(delegate
		{
			DebugHandler.ToggleDisableNotifications();
			toggle.ChangeState(DebugHandler.NotificationsDisabled ? 1 : 0);
		}));
		this.DebugToggleRefresh(null);
		toggle.ChangeState(DebugHandler.NotificationsDisabled ? 1 : 0);
	}

	// Token: 0x06004F1E RID: 20254 RVA: 0x001C2678 File Offset: 0x001C0878
	private void DebugToggleRefresh(object data = null)
	{
		this.debugNotificationToggleCotainer.gameObject.SetActive(DebugHandler.InstantBuildMode);
	}

	// Token: 0x06004F1F RID: 20255 RVA: 0x001C2690 File Offset: 0x001C0890
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.ConsumeMouseScroll = true;
		this.Populate(null);
		Game.Instance.Subscribe(1983128072, new Action<object>(this.Populate));
		Game.Instance.Subscribe(-1280433810, new Action<object>(this.Populate));
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		this.clearSearchButton.onClick += delegate
		{
			this.searchInputField.text = "";
		};
		this.searchInputField.onValueChanged.AddListener(delegate(string value)
		{
			this.SearchFilter(value);
		});
		KInputTextField kinputTextField = this.searchInputField;
		kinputTextField.onFocus = (System.Action)Delegate.Combine(kinputTextField.onFocus, new System.Action(delegate
		{
			base.isEditing = true;
		}));
		this.searchInputField.onEndEdit.AddListener(delegate(string value)
		{
			base.isEditing = false;
		});
		this.Show(false);
	}

	// Token: 0x06004F20 RID: 20256 RVA: 0x001C277D File Offset: 0x001C097D
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			ManagementMenu.Instance.CloseAll();
			AllResourcesScreen.Instance.Show(false);
			this.RefreshSubrows();
		}
	}

	// Token: 0x06004F21 RID: 20257 RVA: 0x001C27A4 File Offset: 0x001C09A4
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
			this.Show(false);
			e.Consumed = true;
		}
		if (base.isEditing)
		{
			e.Consumed = true;
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06004F22 RID: 20258 RVA: 0x001C27E4 File Offset: 0x001C09E4
	public int GetRowCount()
	{
		return this.diagnosticRows.Count;
	}

	// Token: 0x06004F23 RID: 20259 RVA: 0x001C27F1 File Offset: 0x001C09F1
	public override void OnKeyUp(KButtonEvent e)
	{
		if (PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
			this.Show(false);
			e.Consumed = true;
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06004F24 RID: 20260 RVA: 0x001C282E File Offset: 0x001C0A2E
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x06004F25 RID: 20261 RVA: 0x001C2838 File Offset: 0x001C0A38
	public void Populate(object data = null)
	{
		this.SpawnRows();
		foreach (string text in this.diagnosticRows.Keys)
		{
			Tag tag = text;
			this.currentlyDisplayedRows[tag] = true;
		}
		this.SearchFilter(this.searchInputField.text);
		this.RefreshRows();
	}

	// Token: 0x06004F26 RID: 20262 RVA: 0x001C28B8 File Offset: 0x001C0AB8
	private void SpawnRows()
	{
		foreach (KeyValuePair<int, Dictionary<string, ColonyDiagnosticUtility.DisplaySetting>> keyValuePair in ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings)
		{
			foreach (KeyValuePair<string, ColonyDiagnosticUtility.DisplaySetting> keyValuePair2 in ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[keyValuePair.Key])
			{
				if (!this.diagnosticRows.ContainsKey(keyValuePair2.Key))
				{
					ColonyDiagnostic diagnostic = ColonyDiagnosticUtility.Instance.GetDiagnostic(keyValuePair2.Key, keyValuePair.Key);
					if (!(diagnostic is WorkTimeDiagnostic) && !(diagnostic is ChoreGroupDiagnostic))
					{
						this.SpawnRow(diagnostic, this.rootListContainer);
					}
				}
			}
		}
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, GameObject> keyValuePair3 in this.diagnosticRows)
		{
			list.Add(keyValuePair3.Key);
		}
		list.Sort((string a, string b) => UI.StripLinkFormatting(ColonyDiagnosticUtility.Instance.GetDiagnosticName(a)).CompareTo(UI.StripLinkFormatting(ColonyDiagnosticUtility.Instance.GetDiagnosticName(b))));
		foreach (string text in list)
		{
			this.diagnosticRows[text].transform.SetAsLastSibling();
		}
	}

	// Token: 0x06004F27 RID: 20263 RVA: 0x001C2A6C File Offset: 0x001C0C6C
	private void SpawnRow(ColonyDiagnostic diagnostic, GameObject container)
	{
		if (diagnostic == null)
		{
			return;
		}
		if (!this.diagnosticRows.ContainsKey(diagnostic.id))
		{
			GameObject gameObject = Util.KInstantiateUI(this.diagnosticLinePrefab, container, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("NameLabel").SetText(diagnostic.name);
			string id2 = diagnostic.id;
			MultiToggle reference = component.GetReference<MultiToggle>("PinToggle");
			string id = diagnostic.id;
			reference.onClick = (System.Action)Delegate.Combine(reference.onClick, new System.Action(delegate
			{
				if (ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(diagnostic.id))
				{
					ColonyDiagnosticUtility.Instance.ClearDiagnosticTutorialSetting(diagnostic.id);
				}
				else
				{
					int activeWorldId = ClusterManager.Instance.activeWorldId;
					int num = ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[activeWorldId][id] - ColonyDiagnosticUtility.DisplaySetting.AlertOnly;
					if (num < 0)
					{
						num = 2;
					}
					ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[activeWorldId][id] = (ColonyDiagnosticUtility.DisplaySetting)num;
				}
				this.RefreshRows();
				ColonyDiagnosticScreen.Instance.RefreshAll();
			}));
			GraphBase component2 = component.GetReference<SparkLayer>("Chart").GetComponent<GraphBase>();
			component2.axis_x.min_value = 0f;
			component2.axis_x.max_value = 600f;
			component2.axis_x.guide_frequency = 120f;
			component2.RefreshGuides();
			this.diagnosticRows.Add(id2, gameObject);
			this.criteriaRows.Add(id2, new Dictionary<string, GameObject>());
			this.currentlyDisplayedRows.Add(id2, true);
			component.GetReference<Image>("Icon").sprite = Assets.GetSprite(diagnostic.icon);
			this.RefreshPinnedState(id2);
			RectTransform reference2 = component.GetReference<RectTransform>("SubRows");
			DiagnosticCriterion[] criteria = diagnostic.GetCriteria();
			for (int i = 0; i < criteria.Length; i++)
			{
				DiagnosticCriterion sub = criteria[i];
				GameObject gameObject2 = Util.KInstantiateUI(this.subDiagnosticLinePrefab, reference2.gameObject, true);
				gameObject2.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.DIAGNOSTICS_SCREEN.CRITERIA_TOOLTIP, diagnostic.name, sub.name));
				HierarchyReferences component3 = gameObject2.GetComponent<HierarchyReferences>();
				component3.GetReference<LocText>("Label").SetText(sub.name);
				this.criteriaRows[diagnostic.id].Add(sub.id, gameObject2);
				MultiToggle reference3 = component3.GetReference<MultiToggle>("PinToggle");
				reference3.onClick = (System.Action)Delegate.Combine(reference3.onClick, new System.Action(delegate
				{
					int activeWorldId2 = ClusterManager.Instance.activeWorldId;
					bool flag = ColonyDiagnosticUtility.Instance.IsCriteriaEnabled(activeWorldId2, diagnostic.id, sub.id);
					ColonyDiagnosticUtility.Instance.SetCriteriaEnabled(activeWorldId2, diagnostic.id, sub.id, !flag);
					this.RefreshSubrows();
				}));
			}
			this.subrowContainerOpen.Add(diagnostic.id, false);
			MultiToggle reference4 = component.GetReference<MultiToggle>("SubrowToggle");
			MultiToggle multiToggle = reference4;
			multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
			{
				this.subrowContainerOpen[diagnostic.id] = !this.subrowContainerOpen[diagnostic.id];
				this.RefreshSubrows();
			}));
			component.GetReference<MultiToggle>("MainToggle").onClick = reference4.onClick;
		}
	}

	// Token: 0x06004F28 RID: 20264 RVA: 0x001C2D86 File Offset: 0x001C0F86
	private void FilterRowBySearch(Tag tag, string filter)
	{
		this.currentlyDisplayedRows[tag] = this.PassesSearchFilter(tag, filter);
	}

	// Token: 0x06004F29 RID: 20265 RVA: 0x001C2D9C File Offset: 0x001C0F9C
	private void SearchFilter(string search)
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
		{
			this.FilterRowBySearch(keyValuePair.Key, search);
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.diagnosticRows)
		{
			this.currentlyDisplayedRows[keyValuePair2.Key] = this.PassesSearchFilter(keyValuePair2.Key, search);
		}
		this.SetRowsActive();
	}

	// Token: 0x06004F2A RID: 20266 RVA: 0x001C2E68 File Offset: 0x001C1068
	private bool PassesSearchFilter(Tag tag, string filter)
	{
		if (string.IsNullOrEmpty(filter))
		{
			return true;
		}
		filter = filter.ToUpper();
		string text = tag.ToString();
		if (ColonyDiagnosticUtility.Instance.GetDiagnosticName(text).ToUpper().Contains(filter) || tag.Name.ToUpper().Contains(filter))
		{
			return true;
		}
		ColonyDiagnostic diagnostic = ColonyDiagnosticUtility.Instance.GetDiagnostic(text, ClusterManager.Instance.activeWorldId);
		if (diagnostic == null)
		{
			return false;
		}
		DiagnosticCriterion[] criteria = diagnostic.GetCriteria();
		if (criteria == null)
		{
			return false;
		}
		DiagnosticCriterion[] array = criteria;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].name.ToUpper().Contains(filter))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004F2B RID: 20267 RVA: 0x001C2F18 File Offset: 0x001C1118
	private void RefreshPinnedState(string diagnosticID)
	{
		if (!ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[ClusterManager.Instance.activeWorldId].ContainsKey(diagnosticID))
		{
			return;
		}
		MultiToggle reference = this.diagnosticRows[diagnosticID].GetComponent<HierarchyReferences>().GetReference<MultiToggle>("PinToggle");
		if (ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(diagnosticID))
		{
			reference.ChangeState(3);
		}
		else
		{
			switch (ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[ClusterManager.Instance.activeWorldId][diagnosticID])
			{
			case ColonyDiagnosticUtility.DisplaySetting.Always:
				reference.ChangeState(2);
				break;
			case ColonyDiagnosticUtility.DisplaySetting.AlertOnly:
				reference.ChangeState(1);
				break;
			case ColonyDiagnosticUtility.DisplaySetting.Never:
				reference.ChangeState(0);
				break;
			}
		}
		string text = "";
		if (ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(diagnosticID))
		{
			text = UI.DIAGNOSTICS_SCREEN.CLICK_TOGGLE_MESSAGE.TUTORIAL_DISABLED;
		}
		else
		{
			switch (ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[ClusterManager.Instance.activeWorldId][diagnosticID])
			{
			case ColonyDiagnosticUtility.DisplaySetting.Always:
				text = UI.DIAGNOSTICS_SCREEN.CLICK_TOGGLE_MESSAGE.NEVER;
				break;
			case ColonyDiagnosticUtility.DisplaySetting.AlertOnly:
				text = UI.DIAGNOSTICS_SCREEN.CLICK_TOGGLE_MESSAGE.ALWAYS;
				break;
			case ColonyDiagnosticUtility.DisplaySetting.Never:
				text = UI.DIAGNOSTICS_SCREEN.CLICK_TOGGLE_MESSAGE.ALERT_ONLY;
				break;
			}
		}
		reference.GetComponent<ToolTip>().SetSimpleTooltip(text);
	}

	// Token: 0x06004F2C RID: 20268 RVA: 0x001C304C File Offset: 0x001C124C
	public void RefreshRows()
	{
		WorldInventory worldInventory = ClusterManager.Instance.GetWorld(ClusterManager.Instance.activeWorldId).worldInventory;
		if (this.allowRefresh)
		{
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
			{
				HierarchyReferences component = keyValuePair.Value.GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("AvailableLabel").SetText(keyValuePair.Key);
				component.GetReference<RectTransform>("SubRows").gameObject.SetActive(false);
				ColonyDiagnostic diagnostic = ColonyDiagnosticUtility.Instance.GetDiagnostic(keyValuePair.Key, ClusterManager.Instance.activeWorldId);
				if (diagnostic != null)
				{
					component.GetReference<LocText>("AvailableLabel").SetText(diagnostic.GetAverageValueString());
					component.GetReference<Image>("Indicator").color = diagnostic.colors[diagnostic.LatestResult.opinion];
					ToolTip reference = component.GetReference<ToolTip>("Tooltip");
					reference.refreshWhileHovering = true;
					reference.SetSimpleTooltip(Strings.Get(new StringKey("STRINGS.UI.COLONY_DIAGNOSTICS." + diagnostic.id.ToUpper() + ".TOOLTIP_NAME")) + "\n" + diagnostic.LatestResult.Message);
				}
				this.RefreshPinnedState(keyValuePair.Key);
			}
		}
		this.SetRowsActive();
		this.RefreshSubrows();
	}

	// Token: 0x06004F2D RID: 20269 RVA: 0x001C31DC File Offset: 0x001C13DC
	private void RefreshSubrows()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
		{
			HierarchyReferences component = keyValuePair.Value.GetComponent<HierarchyReferences>();
			component.GetReference<MultiToggle>("SubrowToggle").ChangeState((!this.subrowContainerOpen[keyValuePair.Key]) ? 0 : 1);
			component.GetReference<RectTransform>("SubRows").gameObject.SetActive(this.subrowContainerOpen[keyValuePair.Key]);
			int num = 0;
			foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.criteriaRows[keyValuePair.Key])
			{
				MultiToggle reference = keyValuePair2.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("PinToggle");
				int activeWorldId = ClusterManager.Instance.activeWorldId;
				string key = keyValuePair.Key;
				string key2 = keyValuePair2.Key;
				bool flag = ColonyDiagnosticUtility.Instance.IsCriteriaEnabled(activeWorldId, key, key2);
				reference.ChangeState(flag ? 1 : 0);
				if (flag)
				{
					num++;
				}
			}
			component.GetReference<LocText>("SubrowHeaderLabel").SetText(string.Format(UI.DIAGNOSTICS_SCREEN.CRITERIA_ENABLED_COUNT, num, this.criteriaRows[keyValuePair.Key].Count));
		}
	}

	// Token: 0x06004F2E RID: 20270 RVA: 0x001C3394 File Offset: 0x001C1594
	private void RefreshCharts()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
		{
			HierarchyReferences component = keyValuePair.Value.GetComponent<HierarchyReferences>();
			ColonyDiagnostic diagnostic = ColonyDiagnosticUtility.Instance.GetDiagnostic(keyValuePair.Key, ClusterManager.Instance.activeWorldId);
			if (diagnostic != null)
			{
				SparkLayer reference = component.GetReference<SparkLayer>("Chart");
				Tracker tracker = diagnostic.tracker;
				if (tracker != null)
				{
					float num = 3000f;
					global::Tuple<float, float>[] array = tracker.ChartableData(num);
					reference.graph.axis_x.max_value = array[array.Length - 1].first;
					reference.graph.axis_x.min_value = reference.graph.axis_x.max_value - num;
					reference.RefreshLine(array, "resourceAmount");
				}
			}
		}
	}

	// Token: 0x06004F2F RID: 20271 RVA: 0x001C3490 File Offset: 0x001C1690
	private void SetRowsActive()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
		{
			if (ColonyDiagnosticUtility.Instance.GetDiagnostic(keyValuePair.Key, ClusterManager.Instance.activeWorldId) == null)
			{
				this.currentlyDisplayedRows[keyValuePair.Key] = false;
			}
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.diagnosticRows)
		{
			if (keyValuePair2.Value.activeSelf != this.currentlyDisplayedRows[keyValuePair2.Key])
			{
				keyValuePair2.Value.SetActive(this.currentlyDisplayedRows[keyValuePair2.Key]);
			}
		}
	}

	// Token: 0x06004F30 RID: 20272 RVA: 0x001C3594 File Offset: 0x001C1794
	public void Sim4000ms(float dt)
	{
		this.RefreshCharts();
	}

	// Token: 0x06004F31 RID: 20273 RVA: 0x001C359C File Offset: 0x001C179C
	public void Sim1000ms(float dt)
	{
		this.RefreshRows();
	}

	// Token: 0x04003529 RID: 13609
	private Dictionary<string, GameObject> diagnosticRows = new Dictionary<string, GameObject>();

	// Token: 0x0400352A RID: 13610
	private Dictionary<string, Dictionary<string, GameObject>> criteriaRows = new Dictionary<string, Dictionary<string, GameObject>>();

	// Token: 0x0400352B RID: 13611
	public GameObject rootListContainer;

	// Token: 0x0400352C RID: 13612
	public GameObject diagnosticLinePrefab;

	// Token: 0x0400352D RID: 13613
	public GameObject subDiagnosticLinePrefab;

	// Token: 0x0400352E RID: 13614
	public KButton closeButton;

	// Token: 0x0400352F RID: 13615
	public bool allowRefresh = true;

	// Token: 0x04003530 RID: 13616
	[SerializeField]
	private KInputTextField searchInputField;

	// Token: 0x04003531 RID: 13617
	[SerializeField]
	private KButton clearSearchButton;

	// Token: 0x04003532 RID: 13618
	public static AllDiagnosticsScreen Instance;

	// Token: 0x04003533 RID: 13619
	public Dictionary<Tag, bool> currentlyDisplayedRows = new Dictionary<Tag, bool>();

	// Token: 0x04003534 RID: 13620
	public Dictionary<Tag, bool> subrowContainerOpen = new Dictionary<Tag, bool>();

	// Token: 0x04003535 RID: 13621
	[SerializeField]
	private RectTransform debugNotificationToggleCotainer;
}
