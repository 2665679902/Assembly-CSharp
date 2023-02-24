using System;
using System.Collections;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A75 RID: 2677
public class ColonyDiagnosticScreen : KScreen, ISim1000ms
{
	// Token: 0x060051F8 RID: 20984 RVA: 0x001D9BB0 File Offset: 0x001D7DB0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ColonyDiagnosticScreen.Instance = this;
		this.RefreshSingleWorld(null);
		Game.Instance.Subscribe(1983128072, new Action<object>(this.RefreshSingleWorld));
		MultiToggle multiToggle = this.seeAllButton;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			AllDiagnosticsScreen.Instance.Show(!AllDiagnosticsScreen.Instance.gameObject.activeSelf);
		}));
	}

	// Token: 0x060051F9 RID: 20985 RVA: 0x001D9C26 File Offset: 0x001D7E26
	protected override void OnForcedCleanUp()
	{
		ColonyDiagnosticScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x060051FA RID: 20986 RVA: 0x001D9C34 File Offset: 0x001D7E34
	private void RefreshSingleWorld(object data = null)
	{
		foreach (ColonyDiagnosticScreen.DiagnosticRow diagnosticRow in this.diagnosticRows)
		{
			diagnosticRow.OnCleanUp();
			Util.KDestroyGameObject(diagnosticRow.gameObject);
		}
		this.diagnosticRows.Clear();
		this.SpawnTrackerLines(ClusterManager.Instance.activeWorldId);
	}

	// Token: 0x060051FB RID: 20987 RVA: 0x001D9CAC File Offset: 0x001D7EAC
	private void SpawnTrackerLines(int world)
	{
		this.AddDiagnostic<BreathabilityDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<FoodDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<StressDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<RadiationDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<ReactorDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<FloatingRocketDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<RocketFuelDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<RocketOxidizerDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<FarmDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<ToiletDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<BedDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<IdleDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<TrappedDuplicantDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<EntombedDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<PowerUseDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<BatteryDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		this.AddDiagnostic<RocketsInOrbitDiagnostic>(world, this.contentContainer, this.diagnosticRows);
		List<ColonyDiagnosticScreen.DiagnosticRow> list = new List<ColonyDiagnosticScreen.DiagnosticRow>();
		foreach (ColonyDiagnosticScreen.DiagnosticRow diagnosticRow in this.diagnosticRows)
		{
			list.Add(diagnosticRow);
		}
		list.Sort((ColonyDiagnosticScreen.DiagnosticRow a, ColonyDiagnosticScreen.DiagnosticRow b) => a.diagnostic.name.CompareTo(b.diagnostic.name));
		foreach (ColonyDiagnosticScreen.DiagnosticRow diagnosticRow2 in list)
		{
			diagnosticRow2.gameObject.transform.SetAsLastSibling();
		}
		list.Clear();
		this.seeAllButton.transform.SetAsLastSibling();
		this.RefreshAll();
	}

	// Token: 0x060051FC RID: 20988 RVA: 0x001D9EE0 File Offset: 0x001D80E0
	private GameObject AddDiagnostic<T>(int worldID, GameObject parent, List<ColonyDiagnosticScreen.DiagnosticRow> parentCollection) where T : ColonyDiagnostic
	{
		T diagnostic = ColonyDiagnosticUtility.Instance.GetDiagnostic<T>(worldID);
		if (diagnostic == null)
		{
			return null;
		}
		GameObject gameObject = Util.KInstantiateUI(this.linePrefab, parent, true);
		parentCollection.Add(new ColonyDiagnosticScreen.DiagnosticRow(worldID, gameObject, diagnostic));
		return gameObject;
	}

	// Token: 0x060051FD RID: 20989 RVA: 0x001D9F25 File Offset: 0x001D8125
	public static void SetIndication(ColonyDiagnostic.DiagnosticResult.Opinion opinion, GameObject indicatorGameObject)
	{
		indicatorGameObject.GetComponentInChildren<Image>().color = ColonyDiagnosticScreen.GetDiagnosticIndicationColor(opinion);
	}

	// Token: 0x060051FE RID: 20990 RVA: 0x001D9F38 File Offset: 0x001D8138
	public static Color GetDiagnosticIndicationColor(ColonyDiagnostic.DiagnosticResult.Opinion opinion)
	{
		switch (opinion)
		{
		case ColonyDiagnostic.DiagnosticResult.Opinion.DuplicantThreatening:
		case ColonyDiagnostic.DiagnosticResult.Opinion.Bad:
		case ColonyDiagnostic.DiagnosticResult.Opinion.Warning:
			return Constants.NEGATIVE_COLOR;
		case ColonyDiagnostic.DiagnosticResult.Opinion.Concern:
			return Constants.WARNING_COLOR;
		}
		return Color.white;
	}

	// Token: 0x060051FF RID: 20991 RVA: 0x001D9F75 File Offset: 0x001D8175
	public void Sim1000ms(float dt)
	{
		this.RefreshAll();
	}

	// Token: 0x06005200 RID: 20992 RVA: 0x001D9F80 File Offset: 0x001D8180
	public void RefreshAll()
	{
		string text = "";
		foreach (ColonyDiagnosticScreen.DiagnosticRow diagnosticRow in this.diagnosticRows)
		{
			if (diagnosticRow.worldID == ClusterManager.Instance.activeWorldId)
			{
				this.UpdateDiagnosticRow(diagnosticRow, text);
			}
		}
		ColonyDiagnosticScreen.SetIndication(ColonyDiagnosticUtility.Instance.GetWorldDiagnosticResult(ClusterManager.Instance.activeWorldId), this.rootIndicator);
		this.header.GetComponent<ToolTip>().enabled = !string.IsNullOrEmpty(text);
		this.header.GetComponent<ToolTip>().SetSimpleTooltip(text);
		this.seeAllButton.GetComponentInChildren<LocText>().SetText(string.Format(UI.DIAGNOSTICS_SCREEN.SEE_ALL, AllDiagnosticsScreen.Instance.GetRowCount()));
	}

	// Token: 0x06005201 RID: 20993 RVA: 0x001DA068 File Offset: 0x001D8268
	private ColonyDiagnostic.DiagnosticResult.Opinion UpdateDiagnosticRow(ColonyDiagnosticScreen.DiagnosticRow row, string tooltipString)
	{
		ColonyDiagnostic.DiagnosticResult.Opinion currentDisplayedResult = row.currentDisplayedResult;
		bool activeInHierarchy = row.gameObject.activeInHierarchy;
		if (row.diagnostic.LatestResult.opinion < ColonyDiagnostic.DiagnosticResult.Opinion.Normal)
		{
			if (!string.IsNullOrEmpty(tooltipString))
			{
				tooltipString += "\n";
			}
			tooltipString += row.diagnostic.LatestResult.Message;
		}
		if (ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(row.diagnostic.id))
		{
			this.SetRowActive(row, false);
		}
		else
		{
			switch (ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[row.worldID][row.diagnostic.id])
			{
			case ColonyDiagnosticUtility.DisplaySetting.Always:
				this.SetRowActive(row, true);
				break;
			case ColonyDiagnosticUtility.DisplaySetting.AlertOnly:
				this.SetRowActive(row, row.diagnostic.LatestResult.opinion < ColonyDiagnostic.DiagnosticResult.Opinion.Normal);
				break;
			case ColonyDiagnosticUtility.DisplaySetting.Never:
				this.SetRowActive(row, false);
				break;
			}
			if (row.gameObject.activeInHierarchy && (row.currentDisplayedResult < currentDisplayedResult || (row.currentDisplayedResult < ColonyDiagnostic.DiagnosticResult.Opinion.Normal && !activeInHierarchy)) && row.CheckAllowVisualNotification())
			{
				row.TriggerVisualNotification();
			}
		}
		return row.diagnostic.LatestResult.opinion;
	}

	// Token: 0x06005202 RID: 20994 RVA: 0x001DA197 File Offset: 0x001D8397
	private void SetRowActive(ColonyDiagnosticScreen.DiagnosticRow row, bool active)
	{
		if (row.gameObject.activeSelf != active)
		{
			row.gameObject.SetActive(active);
			row.ResolveNotificationRoutine();
		}
	}

	// Token: 0x040036FC RID: 14076
	public GameObject linePrefab;

	// Token: 0x040036FD RID: 14077
	public static ColonyDiagnosticScreen Instance;

	// Token: 0x040036FE RID: 14078
	private List<ColonyDiagnosticScreen.DiagnosticRow> diagnosticRows = new List<ColonyDiagnosticScreen.DiagnosticRow>();

	// Token: 0x040036FF RID: 14079
	public GameObject header;

	// Token: 0x04003700 RID: 14080
	public GameObject contentContainer;

	// Token: 0x04003701 RID: 14081
	public GameObject rootIndicator;

	// Token: 0x04003702 RID: 14082
	public MultiToggle seeAllButton;

	// Token: 0x04003703 RID: 14083
	public static Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, string> notificationSoundsActive = new Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, string>
	{
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.DuplicantThreatening,
			"Diagnostic_Active_DuplicantThreatening"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Bad,
			"Diagnostic_Active_Bad"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Warning,
			"Diagnostic_Active_Warning"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Concern,
			"Diagnostic_Active_Concern"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Suggestion,
			"Diagnostic_Active_Suggestion"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Tutorial,
			"Diagnostic_Active_Tutorial"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Normal,
			""
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Good,
			""
		}
	};

	// Token: 0x04003704 RID: 14084
	public static Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, string> notificationSoundsInactive = new Dictionary<ColonyDiagnostic.DiagnosticResult.Opinion, string>
	{
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.DuplicantThreatening,
			"Diagnostic_Inactive_DuplicantThreatening"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Bad,
			"Diagnostic_Inactive_Bad"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Warning,
			"Diagnostic_Inactive_Warning"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Concern,
			"Diagnostic_Inactive_Concern"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Suggestion,
			"Diagnostic_Inactive_Suggestion"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Tutorial,
			"Diagnostic_Inactive_Tutorial"
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Normal,
			""
		},
		{
			ColonyDiagnostic.DiagnosticResult.Opinion.Good,
			""
		}
	};

	// Token: 0x02001903 RID: 6403
	private class DiagnosticRow : ISim4000ms
	{
		// Token: 0x06008EF8 RID: 36600 RVA: 0x0030F274 File Offset: 0x0030D474
		public DiagnosticRow(int worldID, GameObject gameObject, ColonyDiagnostic diagnostic)
		{
			global::Debug.Assert(diagnostic != null);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			this.worldID = worldID;
			this.sparkLayer = component.GetReference<SparkLayer>("SparkLayer");
			this.diagnostic = diagnostic;
			this.titleLabel = component.GetReference<LocText>("TitleLabel");
			this.valueLabel = component.GetReference<LocText>("ValueLabel");
			this.indicator = component.GetReference<Image>("Indicator");
			this.image = component.GetReference<Image>("Image");
			this.tooltip = gameObject.GetComponent<ToolTip>();
			this.gameObject = gameObject;
			this.titleLabel.SetText(diagnostic.name);
			this.sparkLayer.colorRules.setOwnColor = false;
			if (diagnostic.tracker == null)
			{
				this.sparkLayer.transform.parent.gameObject.SetActive(false);
			}
			else
			{
				this.sparkLayer.ClearLines();
				global::Tuple<float, float>[] array = diagnostic.tracker.ChartableData(600f);
				this.sparkLayer.NewLine(array, diagnostic.name);
			}
			this.button = gameObject.GetComponent<MultiToggle>();
			MultiToggle multiToggle = this.button;
			multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
			{
				if (diagnostic.LatestResult.clickThroughTarget == null)
				{
					CameraController.Instance.ActiveWorldStarWipe(diagnostic.worldID, null);
					return;
				}
				SelectTool.Instance.SelectAndFocus(diagnostic.LatestResult.clickThroughTarget.first, (diagnostic.LatestResult.clickThroughTarget.second == null) ? null : diagnostic.LatestResult.clickThroughTarget.second.GetComponent<KSelectable>());
			}));
			this.defaultIndicatorSizeDelta = Vector2.zero;
			this.Update();
			SimAndRenderScheduler.instance.Add(this, true);
		}

		// Token: 0x06008EF9 RID: 36601 RVA: 0x0030F3FE File Offset: 0x0030D5FE
		public void OnCleanUp()
		{
			SimAndRenderScheduler.instance.Remove(this);
		}

		// Token: 0x06008EFA RID: 36602 RVA: 0x0030F40B File Offset: 0x0030D60B
		public void Sim4000ms(float dt)
		{
			this.Update();
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06008EFB RID: 36603 RVA: 0x0030F413 File Offset: 0x0030D613
		// (set) Token: 0x06008EFC RID: 36604 RVA: 0x0030F41B File Offset: 0x0030D61B
		public GameObject gameObject { get; private set; }

		// Token: 0x06008EFD RID: 36605 RVA: 0x0030F424 File Offset: 0x0030D624
		public void Update()
		{
			Color color = Color.white;
			global::Debug.Assert(this.diagnostic.LatestResult.opinion > ColonyDiagnostic.DiagnosticResult.Opinion.Unset, string.Format("{0} criteria returned no opinion. Make sure the DiagnosticResult parameters are used or an opinion result is otherwise set in all of its criteria", this.diagnostic));
			this.currentDisplayedResult = this.diagnostic.LatestResult.opinion;
			color = this.diagnostic.colors[this.diagnostic.LatestResult.opinion];
			if (this.diagnostic.tracker != null)
			{
				global::Tuple<float, float>[] array = this.diagnostic.tracker.ChartableData(600f);
				this.sparkLayer.RefreshLine(array, this.diagnostic.name);
				this.sparkLayer.SetColor(color);
			}
			this.indicator.color = this.diagnostic.colors[this.diagnostic.LatestResult.opinion];
			this.tooltip.SetSimpleTooltip((this.diagnostic.LatestResult.Message.IsNullOrWhiteSpace() ? UI.COLONY_DIAGNOSTICS.GENERIC_STATUS_NORMAL.text : this.diagnostic.LatestResult.Message) + "\n\n" + UI.COLONY_DIAGNOSTICS.MUTE_TUTORIAL.text);
			ColonyDiagnostic.PresentationSetting presentationSetting = this.diagnostic.presentationSetting;
			if (presentationSetting == ColonyDiagnostic.PresentationSetting.AverageValue || presentationSetting != ColonyDiagnostic.PresentationSetting.CurrentValue)
			{
				this.valueLabel.SetText(this.diagnostic.GetAverageValueString());
			}
			else
			{
				this.valueLabel.SetText(this.diagnostic.GetCurrentValueString());
			}
			if (!string.IsNullOrEmpty(this.diagnostic.icon))
			{
				this.image.sprite = Assets.GetSprite(this.diagnostic.icon);
			}
			if (color == Constants.NEUTRAL_COLOR)
			{
				color = Color.white;
			}
			this.titleLabel.color = color;
		}

		// Token: 0x06008EFE RID: 36606 RVA: 0x0030F5F1 File Offset: 0x0030D7F1
		public bool CheckAllowVisualNotification()
		{
			return this.timeOfLastNotification == 0f || GameClock.Instance.GetTime() >= this.timeOfLastNotification + 300f;
		}

		// Token: 0x06008EFF RID: 36607 RVA: 0x0030F620 File Offset: 0x0030D820
		public void TriggerVisualNotification()
		{
			if (DebugHandler.NotificationsDisabled)
			{
				return;
			}
			if (this.activeRoutine == null)
			{
				this.timeOfLastNotification = GameClock.Instance.GetTime();
				KFMOD.PlayUISound(GlobalAssets.GetSound(ColonyDiagnosticScreen.notificationSoundsActive[this.currentDisplayedResult], false));
				this.activeRoutine = this.gameObject.GetComponent<KMonoBehaviour>().StartCoroutine(this.VisualNotificationRoutine());
			}
		}

		// Token: 0x06008F00 RID: 36608 RVA: 0x0030F684 File Offset: 0x0030D884
		private IEnumerator VisualNotificationRoutine()
		{
			this.gameObject.GetComponentInChildren<NotificationAnimator>().Begin(false);
			RectTransform indicator = this.gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("Indicator").rectTransform;
			this.defaultIndicatorSizeDelta = Vector2.zero;
			indicator.sizeDelta = this.defaultIndicatorSizeDelta;
			float bounceDuration = 3f;
			for (float i = 0f; i < bounceDuration; i += Time.unscaledDeltaTime)
			{
				indicator.sizeDelta = this.defaultIndicatorSizeDelta + Vector2.one * (float)Mathf.RoundToInt(Mathf.Sin(6f * (3.1415927f * (i / bounceDuration))));
				yield return 0;
			}
			for (float i = 0f; i < bounceDuration; i += Time.unscaledDeltaTime)
			{
				indicator.sizeDelta = this.defaultIndicatorSizeDelta + Vector2.one * (float)Mathf.RoundToInt(Mathf.Sin(6f * (3.1415927f * (i / bounceDuration))));
				yield return 0;
			}
			for (float i = 0f; i < bounceDuration; i += Time.unscaledDeltaTime)
			{
				indicator.sizeDelta = this.defaultIndicatorSizeDelta + Vector2.one * (float)Mathf.RoundToInt(Mathf.Sin(6f * (3.1415927f * (i / bounceDuration))));
				yield return 0;
			}
			this.ResolveNotificationRoutine();
			yield break;
		}

		// Token: 0x06008F01 RID: 36609 RVA: 0x0030F694 File Offset: 0x0030D894
		public void ResolveNotificationRoutine()
		{
			this.gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("Indicator").rectTransform.sizeDelta = Vector2.zero;
			this.gameObject.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Content").localPosition = Vector2.zero;
			this.activeRoutine = null;
		}

		// Token: 0x0400730B RID: 29451
		private const float displayHistoryPeriod = 600f;

		// Token: 0x0400730C RID: 29452
		public ColonyDiagnostic diagnostic;

		// Token: 0x0400730D RID: 29453
		public SparkLayer sparkLayer;

		// Token: 0x0400730F RID: 29455
		public int worldID;

		// Token: 0x04007310 RID: 29456
		private LocText titleLabel;

		// Token: 0x04007311 RID: 29457
		private LocText valueLabel;

		// Token: 0x04007312 RID: 29458
		private Image indicator;

		// Token: 0x04007313 RID: 29459
		private ToolTip tooltip;

		// Token: 0x04007314 RID: 29460
		private MultiToggle button;

		// Token: 0x04007315 RID: 29461
		private Image image;

		// Token: 0x04007316 RID: 29462
		public ColonyDiagnostic.DiagnosticResult.Opinion currentDisplayedResult;

		// Token: 0x04007317 RID: 29463
		private Vector2 defaultIndicatorSizeDelta;

		// Token: 0x04007318 RID: 29464
		private float timeOfLastNotification;

		// Token: 0x04007319 RID: 29465
		private const float MIN_TIME_BETWEEN_NOTIFICATIONS = 300f;

		// Token: 0x0400731A RID: 29466
		private Coroutine activeRoutine;
	}
}
