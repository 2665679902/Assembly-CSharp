using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000072 RID: 114
public class ToolTipScreen : KScreen
{
	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00016F0C File Offset: 0x0001510C
	// (set) Token: 0x060004A8 RID: 1192 RVA: 0x00016F13 File Offset: 0x00015113
	public static ToolTipScreen Instance { get; private set; }

	// Token: 0x060004A9 RID: 1193 RVA: 0x00016F1C File Offset: 0x0001511C
	protected override void OnActivate()
	{
		ToolTipScreen.Instance = this;
		this.toolTipWidget = Util.KInstantiate(this.ToolTipPrefab, base.gameObject, null);
		this.toolTipWidget.transform.SetParent(base.gameObject.transform, false);
		Util.Reset(this.toolTipWidget.transform);
		this.toolTipWidget.SetActive(false);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00016F7F File Offset: 0x0001517F
	protected override void OnForcedCleanUp()
	{
		ToolTipScreen.Instance = null;
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00016F87 File Offset: 0x00015187
	public void SetToolTip(ToolTip tool_tip)
	{
		this.tooltipSetting = tool_tip;
		this.multiTooltipContainer = this.toolTipWidget.transform.Find("MultitooltipContainer").gameObject;
		this.ConfigureTooltip();
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00016FB8 File Offset: 0x000151B8
	private void ConfigureTooltip()
	{
		if (this.tooltipSetting == null)
		{
			this.prevTooltip = null;
		}
		if (this.tooltipSetting != null && this.dirtyHoverTooltip != null && this.tooltipSetting == this.dirtyHoverTooltip)
		{
			this.ClearToolTip(this.dirtyHoverTooltip);
		}
		if (this.tooltipSetting != null)
		{
			this.tooltipSetting.RebuildDynamicTooltip();
			if (this.tooltipSetting.multiStringCount == 0)
			{
				this.clearMultiStringTooltip();
			}
			else if (this.prevTooltip != this.tooltipSetting || !this.multiTooltipContainer.activeInHierarchy)
			{
				this.prepareMultiStringTooltip(this.tooltipSetting);
				this.prevTooltip = this.tooltipSetting;
			}
			bool flag = this.multiTooltipContainer.transform.childCount != 0;
			this.toolTipWidget.SetActive(flag);
			if (flag)
			{
				RectTransform rectTransform;
				if (this.tooltipSetting.overrideParentObject == null)
				{
					rectTransform = this.tooltipSetting.GetComponent<RectTransform>();
				}
				else
				{
					rectTransform = this.tooltipSetting.overrideParentObject;
				}
				RectTransform component = this.toolTipWidget.GetComponent<RectTransform>();
				component.transform.SetParent(this.anchorRoot.transform);
				float num = 1f;
				if (this.scaler == null)
				{
					this.scaler = base.transform.parent.GetComponent<CanvasScaler>();
					if (this.scaler == null)
					{
						this.scaler = base.transform.parent.parent.GetComponent<CanvasScaler>();
					}
				}
				if (this.scaler != null)
				{
					num = this.scaler.scaleFactor;
				}
				if (!this.tooltipSetting.worldSpace)
				{
					this.anchorRoot.anchoredPosition = rectTransform.transform.GetPosition();
				}
				else
				{
					this.anchorRoot.anchoredPosition = base.WorldToScreen(rectTransform.transform.GetPosition()) + new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f);
				}
				this.anchorRoot.anchoredPosition = new Vector2(this.anchorRoot.anchoredPosition.x / num, this.anchorRoot.anchoredPosition.y / num);
				this.anchorRoot.anchoredPosition -= Vector2.up * (rectTransform.rectTransform().pivot.y * rectTransform.rectTransform().sizeDelta.y);
				this.anchorRoot.anchoredPosition -= Vector2.right * (rectTransform.rectTransform().pivot.x * rectTransform.rectTransform().sizeDelta.x);
				this.anchorRoot.anchoredPosition += Vector2.right * (rectTransform.sizeDelta.x * this.tooltipSetting.parentPositionAnchor.x);
				this.anchorRoot.anchoredPosition += Vector2.up * (rectTransform.sizeDelta.y * this.tooltipSetting.parentPositionAnchor.y);
				component.pivot = this.tooltipSetting.tooltipPivot;
				RectTransform rectTransform2 = component;
				RectTransform rectTransform3 = component;
				Vector2 vector = new Vector2(0f, 0f);
				rectTransform3.anchorMax = vector;
				rectTransform2.anchorMin = vector;
				component.anchoredPosition = this.tooltipSetting.tooltipPositionOffset * num;
				if (!this.tooltipSetting.worldSpace)
				{
					Rect rect = ((RectTransform)base.transform).rect;
					Vector2 vector2 = new Vector2(base.transform.GetPosition().x, base.transform.GetPosition().y) + this.ScreenEdgePadding;
					Vector2 vector3 = new Vector2(base.transform.GetPosition().x, base.transform.GetPosition().y) + rect.width * Vector2.right + rect.height * Vector2.up - this.ScreenEdgePadding * Mathf.Max(1f, num);
					vector3.x *= num;
					vector3.y *= num;
					Vector2 vector4;
					vector4.x = component.GetPosition().x - component.pivot.x * (component.sizeDelta.x * num);
					vector4.y = component.GetPosition().y - component.pivot.y * (component.sizeDelta.y * num);
					Vector2 vector5;
					vector5.x = component.GetPosition().x + (1f - component.pivot.x) * (component.sizeDelta.x * num);
					vector5.y = component.GetPosition().y + (1f - component.pivot.y) * (component.sizeDelta.y * num);
					Vector2 vector6 = Vector2.zero;
					if (vector4.x < vector2.x)
					{
						vector6.x = vector2.x - vector4.x;
					}
					if (vector5.x > vector3.x)
					{
						vector6.x = vector3.x - vector5.x;
					}
					if (vector4.y < vector2.y)
					{
						vector6.y = vector2.y - vector4.y;
					}
					if (vector5.y > vector3.y)
					{
						vector6.y = vector3.y - vector5.y;
					}
					vector6 /= num;
					component.anchoredPosition += vector6;
				}
			}
		}
		if (((RectTransform)base.transform).GetSiblingIndex() != base.transform.parent.childCount - 1)
		{
			((RectTransform)base.transform).SetAsLastSibling();
		}
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x000175C8 File Offset: 0x000157C8
	private void prepareMultiStringTooltip(ToolTip setting)
	{
		int multiStringCount = this.tooltipSetting.multiStringCount;
		this.clearMultiStringTooltip();
		for (int i = 0; i < multiStringCount; i++)
		{
			Util.KInstantiateUI(this.labelPrefab, null, true).transform.SetParent(this.multiTooltipContainer.transform);
		}
		for (int j = 0; j < this.tooltipSetting.multiStringCount; j++)
		{
			Transform child = this.multiTooltipContainer.transform.GetChild(j);
			LayoutElement component = child.GetComponent<LayoutElement>();
			TextMeshProUGUI component2 = child.GetComponent<TextMeshProUGUI>();
			component2.text = this.tooltipSetting.GetMultiString(j);
			child.GetComponent<SetTextStyleSetting>().SetStyle(this.tooltipSetting.GetStyleSetting(j));
			if (setting.SizingSetting == ToolTip.ToolTipSizeSetting.MaxWidthWrapContent)
			{
				component.minWidth = (component.preferredWidth = setting.WrapWidth);
				component.rectTransform().sizeDelta = new Vector2(setting.WrapWidth, 1000f);
				component.minHeight = (component.preferredHeight = component2.preferredHeight);
				component.minHeight = (component.preferredHeight = component2.preferredHeight);
				component.rectTransform().sizeDelta = new Vector2(setting.WrapWidth, component.minHeight);
				base.GetComponentInChildren<ContentSizeFitter>(true).horizontalFit = ContentSizeFitter.FitMode.MinSize;
				this.multiTooltipContainer.GetComponent<LayoutElement>().minWidth = setting.WrapWidth + 2f * this.ScreenEdgePadding.x;
			}
			else if (setting.SizingSetting == ToolTip.ToolTipSizeSetting.DynamicWidthNoWrap)
			{
				base.GetComponentInChildren<ContentSizeFitter>(true).horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
				Vector2 preferredValues = component2.GetPreferredValues();
				this.multiTooltipContainer.GetComponent<LayoutElement>().minWidth = (component.minWidth = (component.preferredWidth = preferredValues.x));
				component.minHeight = (component.preferredHeight = preferredValues.y);
				base.GetComponentInChildren<ContentSizeFitter>(true).SetLayoutHorizontal();
				base.GetComponentInChildren<ContentSizeFitter>(true).SetLayoutVertical();
				this.multiTooltipContainer.rectTransform().sizeDelta = new Vector2(component.minWidth, component.minHeight);
				this.multiTooltipContainer.transform.parent.rectTransform().sizeDelta = this.multiTooltipContainer.rectTransform().sizeDelta;
			}
			component2.ForceMeshUpdate();
		}
		this.tooltipIncubating = true;
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00017818 File Offset: 0x00015A18
	private void Update()
	{
		if (this.tooltipSetting != null)
		{
			this.tooltipSetting.UpdateWhileHovered();
		}
		if (this.multiTooltipContainer == null || this.anchorRoot == null)
		{
			return;
		}
		if (this.dirtyHoverTooltip != null)
		{
			ToolTip toolTip = this.dirtyHoverTooltip;
			this.MakeDirtyTooltipClean(toolTip);
			this.ClearToolTip(toolTip);
		}
		if (this.tooltipIncubating)
		{
			this.tooltipIncubating = false;
			if (this.anchorRoot.GetComponentInChildren<Image>() != null)
			{
				this.anchorRoot.GetComponentInChildren<Image>(true).enabled = false;
			}
			this.multiTooltipContainer.transform.localScale = Vector3.zero;
			this.toolTipIsBlank = true;
			for (int i = 0; i < this.multiTooltipContainer.transform.childCount; i++)
			{
				if (this.multiTooltipContainer.transform.GetChild(i).transform.localScale != Vector3.one)
				{
					this.multiTooltipContainer.transform.GetChild(i).transform.localScale = Vector3.one;
				}
				LayoutElement component = this.multiTooltipContainer.transform.GetChild(i).GetComponent<LayoutElement>();
				TextMeshProUGUI component2 = component.GetComponent<TextMeshProUGUI>();
				this.toolTipIsBlank = component2.text == "" && this.toolTipIsBlank;
				if (component.minHeight != component2.preferredHeight)
				{
					component.minHeight = component2.preferredHeight;
				}
			}
			return;
		}
		if (this.multiTooltipContainer.transform.localScale != Vector3.one && !this.toolTipIsBlank)
		{
			if (this.anchorRoot.GetComponentInChildren<Image>() != null)
			{
				this.anchorRoot.GetComponentInChildren<Image>(true).enabled = true;
			}
			this.multiTooltipContainer.transform.localScale = Vector3.one;
		}
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x000179F1 File Offset: 0x00015BF1
	public void HotSwapTooltipString(string newString, int lineIndex)
	{
		if (this.multiTooltipContainer.transform.childCount > lineIndex)
		{
			this.multiTooltipContainer.transform.GetChild(lineIndex).GetComponent<TextMeshProUGUI>().text = newString;
		}
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00017A24 File Offset: 0x00015C24
	private void clearMultiStringTooltip()
	{
		for (int i = this.multiTooltipContainer.transform.childCount - 1; i >= 0; i--)
		{
			UnityEngine.Object.DestroyImmediate(this.multiTooltipContainer.transform.GetChild(i).gameObject);
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00017A69 File Offset: 0x00015C69
	public void ClearToolTip(ToolTip tt)
	{
		if (tt == this.tooltipSetting)
		{
			this.tooltipSetting = null;
			if (this.toolTipWidget != null)
			{
				this.clearMultiStringTooltip();
				this.toolTipWidget.SetActive(false);
			}
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00017AA0 File Offset: 0x00015CA0
	public void MarkTooltipDirty(ToolTip tt)
	{
		if (tt == this.tooltipSetting)
		{
			this.dirtyHoverTooltip = tt;
		}
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00017AB7 File Offset: 0x00015CB7
	public void MakeDirtyTooltipClean(ToolTip tt)
	{
		if (tt == this.dirtyHoverTooltip)
		{
			this.dirtyHoverTooltip = null;
		}
	}

	// Token: 0x040004E7 RID: 1255
	public GameObject ToolTipPrefab;

	// Token: 0x040004E8 RID: 1256
	public RectTransform anchorRoot;

	// Token: 0x040004E9 RID: 1257
	private GameObject toolTipWidget;

	// Token: 0x040004EA RID: 1258
	private ToolTip prevTooltip;

	// Token: 0x040004EB RID: 1259
	private ToolTip tooltipSetting;

	// Token: 0x040004EC RID: 1260
	public GameObject labelPrefab;

	// Token: 0x040004ED RID: 1261
	private GameObject multiTooltipContainer;

	// Token: 0x040004EE RID: 1262
	public TextStyleSetting defaultTooltipHeaderStyle;

	// Token: 0x040004EF RID: 1263
	public TextStyleSetting defaultTooltipBodyStyle;

	// Token: 0x040004F0 RID: 1264
	private bool toolTipIsBlank;

	// Token: 0x040004F1 RID: 1265
	private Vector2 ScreenEdgePadding = new Vector2(8f, 8f);

	// Token: 0x040004F3 RID: 1267
	private ToolTip dirtyHoverTooltip;

	// Token: 0x040004F4 RID: 1268
	private bool tooltipIncubating = true;

	// Token: 0x040004F5 RID: 1269
	private CanvasScaler scaler;
}
