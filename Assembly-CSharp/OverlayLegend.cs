using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B52 RID: 2898
public class OverlayLegend : KScreen
{
	// Token: 0x060059FD RID: 23037 RVA: 0x00208D88 File Offset: 0x00206F88
	[ContextMenu("Set all fonts color")]
	public void SetAllFontsColor()
	{
		foreach (OverlayLegend.OverlayInfo overlayInfo in this.overlayInfoList)
		{
			for (int i = 0; i < overlayInfo.infoUnits.Count; i++)
			{
				if (overlayInfo.infoUnits[i].fontColor == Color.clear)
				{
					overlayInfo.infoUnits[i].fontColor = Color.white;
				}
			}
		}
	}

	// Token: 0x060059FE RID: 23038 RVA: 0x00208E20 File Offset: 0x00207020
	[ContextMenu("Set all tooltips")]
	public void SetAllTooltips()
	{
		foreach (OverlayLegend.OverlayInfo overlayInfo in this.overlayInfoList)
		{
			string text = overlayInfo.name;
			text = text.Replace("NAME", "");
			for (int i = 0; i < overlayInfo.infoUnits.Count; i++)
			{
				string text2 = overlayInfo.infoUnits[i].description;
				text2 = text2.Replace(text, "");
				text2 = text + "TOOLTIPS." + text2;
				overlayInfo.infoUnits[i].tooltip = text2;
			}
		}
	}

	// Token: 0x060059FF RID: 23039 RVA: 0x00208EE4 File Offset: 0x002070E4
	[ContextMenu("Set Sliced for empty icons")]
	public void SetSlicedForEmptyIcons()
	{
		foreach (OverlayLegend.OverlayInfo overlayInfo in this.overlayInfoList)
		{
			for (int i = 0; i < overlayInfo.infoUnits.Count; i++)
			{
				if (overlayInfo.infoUnits[i].icon == this.emptySprite)
				{
					overlayInfo.infoUnits[i].sliceIcon = true;
				}
			}
		}
	}

	// Token: 0x06005A00 RID: 23040 RVA: 0x00208F78 File Offset: 0x00207178
	protected override void OnSpawn()
	{
		base.ConsumeMouseScroll = true;
		base.OnSpawn();
		if (OverlayLegend.Instance == null)
		{
			OverlayLegend.Instance = this;
			this.activeUnitObjs = new List<GameObject>();
			this.inactiveUnitObjs = new List<GameObject>();
			foreach (OverlayLegend.OverlayInfo overlayInfo in this.overlayInfoList)
			{
				overlayInfo.name = Strings.Get(overlayInfo.name);
				for (int i = 0; i < overlayInfo.infoUnits.Count; i++)
				{
					overlayInfo.infoUnits[i].description = Strings.Get(overlayInfo.infoUnits[i].description);
					if (!string.IsNullOrEmpty(overlayInfo.infoUnits[i].tooltip))
					{
						overlayInfo.infoUnits[i].tooltip = Strings.Get(overlayInfo.infoUnits[i].tooltip);
					}
				}
			}
			base.GetComponent<LayoutElement>().minWidth = (float)(DlcManager.FeatureClusterSpaceEnabled() ? 322 : 288);
			this.ClearLegend();
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06005A01 RID: 23041 RVA: 0x002090D4 File Offset: 0x002072D4
	protected override void OnLoadLevel()
	{
		OverlayLegend.Instance = null;
		this.activeDiagrams.Clear();
		UnityEngine.Object.Destroy(base.gameObject);
		base.OnLoadLevel();
	}

	// Token: 0x06005A02 RID: 23042 RVA: 0x002090F8 File Offset: 0x002072F8
	private void SetLegend(OverlayLegend.OverlayInfo overlayInfo)
	{
		if (overlayInfo == null)
		{
			this.ClearLegend();
			return;
		}
		if (!overlayInfo.isProgrammaticallyPopulated && (overlayInfo.infoUnits == null || overlayInfo.infoUnits.Count == 0))
		{
			this.ClearLegend();
			return;
		}
		this.Show(true);
		this.title.text = overlayInfo.name;
		if (overlayInfo.isProgrammaticallyPopulated)
		{
			this.PopulateGeneratedLegend(overlayInfo, false);
			return;
		}
		this.PopulateOverlayInfoUnits(overlayInfo, false);
	}

	// Token: 0x06005A03 RID: 23043 RVA: 0x00209164 File Offset: 0x00207364
	public void SetLegend(OverlayModes.Mode mode, bool refreshing = false)
	{
		if (this.currentMode != null && this.currentMode.ViewMode() == mode.ViewMode() && !refreshing)
		{
			return;
		}
		this.ClearLegend();
		OverlayLegend.OverlayInfo overlayInfo = this.overlayInfoList.Find((OverlayLegend.OverlayInfo ol) => ol.mode == mode.ViewMode());
		this.currentMode = mode;
		this.SetLegend(overlayInfo);
	}

	// Token: 0x06005A04 RID: 23044 RVA: 0x002091D8 File Offset: 0x002073D8
	public GameObject GetFreeUnitObject()
	{
		if (this.inactiveUnitObjs.Count == 0)
		{
			this.inactiveUnitObjs.Add(Util.KInstantiateUI(this.unitPrefab, this.inactiveUnitsParent, false));
		}
		GameObject gameObject = this.inactiveUnitObjs[0];
		this.inactiveUnitObjs.RemoveAt(0);
		this.activeUnitObjs.Add(gameObject);
		return gameObject;
	}

	// Token: 0x06005A05 RID: 23045 RVA: 0x00209238 File Offset: 0x00207438
	private void RemoveActiveObjects()
	{
		while (this.activeUnitObjs.Count > 0)
		{
			this.activeUnitObjs[0].transform.Find("Icon").GetComponent<Image>().enabled = false;
			this.activeUnitObjs[0].GetComponentInChildren<LocText>().enabled = false;
			this.activeUnitObjs[0].transform.SetParent(this.inactiveUnitsParent.transform);
			this.activeUnitObjs[0].SetActive(false);
			this.inactiveUnitObjs.Add(this.activeUnitObjs[0]);
			this.activeUnitObjs.RemoveAt(0);
		}
	}

	// Token: 0x06005A06 RID: 23046 RVA: 0x002092F0 File Offset: 0x002074F0
	public void ClearLegend()
	{
		this.RemoveActiveObjects();
		for (int i = 0; i < this.activeDiagrams.Count; i++)
		{
			if (this.activeDiagrams[i] != null)
			{
				UnityEngine.Object.Destroy(this.activeDiagrams[i]);
			}
		}
		this.activeDiagrams.Clear();
		Vector2 sizeDelta = this.diagramsParent.GetComponent<RectTransform>().sizeDelta;
		sizeDelta.y = 0f;
		this.diagramsParent.GetComponent<RectTransform>().sizeDelta = sizeDelta;
		this.Show(false);
	}

	// Token: 0x06005A07 RID: 23047 RVA: 0x00209380 File Offset: 0x00207580
	public OverlayLegend.OverlayInfo GetOverlayInfo(OverlayModes.Mode mode)
	{
		for (int i = 0; i < this.overlayInfoList.Count; i++)
		{
			if (this.overlayInfoList[i].mode == mode.ViewMode())
			{
				return this.overlayInfoList[i];
			}
		}
		return null;
	}

	// Token: 0x06005A08 RID: 23048 RVA: 0x002093D0 File Offset: 0x002075D0
	private void PopulateOverlayInfoUnits(OverlayLegend.OverlayInfo overlayInfo, bool isRefresh = false)
	{
		if (overlayInfo.infoUnits != null && overlayInfo.infoUnits.Count > 0)
		{
			this.activeUnitsParent.SetActive(true);
			using (List<OverlayLegend.OverlayInfoUnit>.Enumerator enumerator = overlayInfo.infoUnits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OverlayLegend.OverlayInfoUnit overlayInfoUnit = enumerator.Current;
					GameObject freeUnitObject = this.GetFreeUnitObject();
					if (overlayInfoUnit.icon != null)
					{
						Image component = freeUnitObject.transform.Find("Icon").GetComponent<Image>();
						component.gameObject.SetActive(true);
						component.sprite = overlayInfoUnit.icon;
						component.color = overlayInfoUnit.color;
						component.enabled = true;
						component.type = (overlayInfoUnit.sliceIcon ? Image.Type.Sliced : Image.Type.Simple);
					}
					else
					{
						freeUnitObject.transform.Find("Icon").gameObject.SetActive(false);
					}
					if (!string.IsNullOrEmpty(overlayInfoUnit.description))
					{
						LocText componentInChildren = freeUnitObject.GetComponentInChildren<LocText>();
						componentInChildren.text = string.Format(overlayInfoUnit.description, overlayInfoUnit.formatData);
						componentInChildren.color = overlayInfoUnit.fontColor;
						componentInChildren.enabled = true;
					}
					ToolTip component2 = freeUnitObject.GetComponent<ToolTip>();
					if (!string.IsNullOrEmpty(overlayInfoUnit.tooltip))
					{
						component2.toolTip = string.Format(overlayInfoUnit.tooltip, overlayInfoUnit.tooltipFormatData);
						component2.enabled = true;
					}
					else
					{
						component2.enabled = false;
					}
					freeUnitObject.SetActive(true);
					freeUnitObject.transform.SetParent(this.activeUnitsParent.transform);
				}
				goto IL_180;
			}
		}
		this.activeUnitsParent.SetActive(false);
		IL_180:
		if (!isRefresh)
		{
			if (overlayInfo.diagrams != null && overlayInfo.diagrams.Count > 0)
			{
				this.diagramsParent.SetActive(true);
				using (List<GameObject>.Enumerator enumerator2 = overlayInfo.diagrams.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						GameObject gameObject = enumerator2.Current;
						GameObject gameObject2 = Util.KInstantiateUI(gameObject, this.diagramsParent, false);
						this.activeDiagrams.Add(gameObject2);
					}
					return;
				}
			}
			this.diagramsParent.SetActive(false);
		}
	}

	// Token: 0x06005A09 RID: 23049 RVA: 0x0020960C File Offset: 0x0020780C
	private void PopulateGeneratedLegend(OverlayLegend.OverlayInfo info, bool isRefresh = false)
	{
		if (isRefresh)
		{
			this.RemoveActiveObjects();
		}
		if (info.infoUnits != null && info.infoUnits.Count > 0)
		{
			this.PopulateOverlayInfoUnits(info, isRefresh);
		}
		List<LegendEntry> customLegendData = this.currentMode.GetCustomLegendData();
		if (customLegendData != null)
		{
			this.activeUnitsParent.SetActive(true);
			using (List<LegendEntry>.Enumerator enumerator = customLegendData.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LegendEntry legendEntry = enumerator.Current;
					GameObject freeUnitObject = this.GetFreeUnitObject();
					Image component = freeUnitObject.transform.Find("Icon").GetComponent<Image>();
					component.gameObject.SetActive(legendEntry.displaySprite);
					component.sprite = legendEntry.sprite;
					component.color = legendEntry.colour;
					component.enabled = true;
					component.type = Image.Type.Simple;
					LocText componentInChildren = freeUnitObject.GetComponentInChildren<LocText>();
					componentInChildren.text = legendEntry.name;
					componentInChildren.color = Color.white;
					componentInChildren.enabled = true;
					ToolTip component2 = freeUnitObject.GetComponent<ToolTip>();
					component2.enabled = legendEntry.desc != null || legendEntry.desc_arg != null;
					component2.toolTip = ((legendEntry.desc_arg == null) ? legendEntry.desc : string.Format(legendEntry.desc, legendEntry.desc_arg));
					freeUnitObject.SetActive(true);
					freeUnitObject.transform.SetParent(this.activeUnitsParent.transform);
				}
				goto IL_157;
			}
		}
		this.activeUnitsParent.SetActive(false);
		IL_157:
		if (!isRefresh && this.currentMode.legendFilters != null)
		{
			GameObject gameObject = Util.KInstantiateUI(this.toolParameterMenuPrefab, this.diagramsParent, false);
			this.activeDiagrams.Add(gameObject);
			this.diagramsParent.SetActive(true);
			this.filterMenu = gameObject.GetComponent<ToolParameterMenu>();
			this.filterMenu.PopulateMenu(this.currentMode.legendFilters);
			this.filterMenu.onParametersChanged += this.OnFiltersChanged;
			this.OnFiltersChanged();
		}
	}

	// Token: 0x06005A0A RID: 23050 RVA: 0x002097FC File Offset: 0x002079FC
	private void OnFiltersChanged()
	{
		this.currentMode.OnFiltersChanged();
		this.PopulateGeneratedLegend(this.GetOverlayInfo(this.currentMode), true);
		Game.Instance.ForceOverlayUpdate(false);
	}

	// Token: 0x06005A0B RID: 23051 RVA: 0x00209827 File Offset: 0x00207A27
	private void DisableOverlay()
	{
		this.filterMenu.onParametersChanged -= this.OnFiltersChanged;
		this.filterMenu.ClearMenu();
		this.filterMenu.gameObject.SetActive(false);
		this.filterMenu = null;
	}

	// Token: 0x04003CD3 RID: 15571
	public static OverlayLegend Instance;

	// Token: 0x04003CD4 RID: 15572
	[SerializeField]
	private LocText title;

	// Token: 0x04003CD5 RID: 15573
	[SerializeField]
	private Sprite emptySprite;

	// Token: 0x04003CD6 RID: 15574
	[SerializeField]
	private List<OverlayLegend.OverlayInfo> overlayInfoList;

	// Token: 0x04003CD7 RID: 15575
	[SerializeField]
	private GameObject unitPrefab;

	// Token: 0x04003CD8 RID: 15576
	[SerializeField]
	private GameObject activeUnitsParent;

	// Token: 0x04003CD9 RID: 15577
	[SerializeField]
	private GameObject diagramsParent;

	// Token: 0x04003CDA RID: 15578
	[SerializeField]
	private GameObject inactiveUnitsParent;

	// Token: 0x04003CDB RID: 15579
	[SerializeField]
	private GameObject toolParameterMenuPrefab;

	// Token: 0x04003CDC RID: 15580
	private ToolParameterMenu filterMenu;

	// Token: 0x04003CDD RID: 15581
	private OverlayModes.Mode currentMode;

	// Token: 0x04003CDE RID: 15582
	private List<GameObject> inactiveUnitObjs;

	// Token: 0x04003CDF RID: 15583
	private List<GameObject> activeUnitObjs;

	// Token: 0x04003CE0 RID: 15584
	private List<GameObject> activeDiagrams = new List<GameObject>();

	// Token: 0x020019F4 RID: 6644
	[Serializable]
	public class OverlayInfoUnit
	{
		// Token: 0x060091C1 RID: 37313 RVA: 0x00315BD7 File Offset: 0x00313DD7
		public OverlayInfoUnit(Sprite icon, string description, Color color, Color fontColor, object formatData = null, bool sliceIcon = false)
		{
			this.icon = icon;
			this.description = description;
			this.color = color;
			this.fontColor = fontColor;
			this.formatData = formatData;
			this.sliceIcon = sliceIcon;
		}

		// Token: 0x040075F0 RID: 30192
		public Sprite icon;

		// Token: 0x040075F1 RID: 30193
		public string description;

		// Token: 0x040075F2 RID: 30194
		public string tooltip;

		// Token: 0x040075F3 RID: 30195
		public Color color;

		// Token: 0x040075F4 RID: 30196
		public Color fontColor;

		// Token: 0x040075F5 RID: 30197
		public object formatData;

		// Token: 0x040075F6 RID: 30198
		public object tooltipFormatData;

		// Token: 0x040075F7 RID: 30199
		public bool sliceIcon;
	}

	// Token: 0x020019F5 RID: 6645
	[Serializable]
	public class OverlayInfo
	{
		// Token: 0x040075F8 RID: 30200
		public string name;

		// Token: 0x040075F9 RID: 30201
		public HashedString mode;

		// Token: 0x040075FA RID: 30202
		public List<OverlayLegend.OverlayInfoUnit> infoUnits;

		// Token: 0x040075FB RID: 30203
		public List<GameObject> diagrams;

		// Token: 0x040075FC RID: 30204
		public bool isProgrammaticallyPopulated;
	}
}
