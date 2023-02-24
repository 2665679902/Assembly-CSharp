using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000071 RID: 113
[AddComponentMenu("KMonoBehaviour/Plugins/ToolTip")]
public class ToolTip : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x1700009C RID: 156
	// (set) Token: 0x0600048F RID: 1167 RVA: 0x0001695E File Offset: 0x00014B5E
	public string toolTip
	{
		set
		{
			this.SetSimpleTooltip(value);
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06000490 RID: 1168 RVA: 0x00016967 File Offset: 0x00014B67
	public int multiStringCount
	{
		get
		{
			return this.multiStringToolTips.Count;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x06000491 RID: 1169 RVA: 0x00016974 File Offset: 0x00014B74
	// (set) Token: 0x06000492 RID: 1170 RVA: 0x0001697C File Offset: 0x00014B7C
	public Func<string> OnToolTip
	{
		get
		{
			return this._OnToolTip;
		}
		set
		{
			this._OnToolTip = value;
		}
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x00016988 File Offset: 0x00014B88
	protected override void OnPrefabInit()
	{
		if (base.gameObject.GetComponents<ToolTip>().Length > 1)
		{
			global::Debug.LogError("The object " + base.gameObject.name + " has more than one ToolTip, it conflict when displaying this tooltip.");
		}
		base.Subscribe<ToolTip>(2098165161, ToolTip.OnClickDelegate);
		if (this.UseFixedStringKey)
		{
			string text = Strings.Get(new StringKey(this.FixedStringKey));
			this.toolTip = text;
		}
		switch (this.toolTipPosition)
		{
		case ToolTip.TooltipPosition.TopLeft:
			this.tooltipPivot = new Vector2(1f, 0f);
			this.tooltipPositionOffset = new Vector2(0f, 20f);
			this.parentPositionAnchor = new Vector2(0.5f, 0.5f);
			return;
		case ToolTip.TooltipPosition.TopCenter:
			this.tooltipPivot = new Vector2(0.5f, 0f);
			this.tooltipPositionOffset = new Vector2(0f, 20f);
			this.parentPositionAnchor = new Vector2(0.5f, 0.5f);
			return;
		case ToolTip.TooltipPosition.TopRight:
			this.tooltipPivot = new Vector2(0f, 0f);
			this.tooltipPositionOffset = new Vector2(0f, 20f);
			this.parentPositionAnchor = new Vector2(0.5f, 0.5f);
			return;
		case ToolTip.TooltipPosition.BottomLeft:
			this.tooltipPivot = new Vector2(1f, 1f);
			this.tooltipPositionOffset = new Vector2(0f, -25f);
			this.parentPositionAnchor = new Vector2(0.5f, 0.5f);
			return;
		case ToolTip.TooltipPosition.BottomCenter:
			this.tooltipPivot = new Vector2(0.5f, 1f);
			this.tooltipPositionOffset = new Vector2(0f, -25f);
			this.parentPositionAnchor = new Vector2(0.5f, 0.5f);
			return;
		case ToolTip.TooltipPosition.BottomRight:
			this.tooltipPivot = new Vector2(0f, 1f);
			this.tooltipPositionOffset = new Vector2(0f, -25f);
			this.parentPositionAnchor = new Vector2(0.5f, 0.5f);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00016BA0 File Offset: 0x00014DA0
	protected override void OnSpawn()
	{
		if (!this.worldSpace)
		{
			Canvas componentInParent = base.gameObject.GetComponentInParent<Canvas>();
			this.worldSpace = componentInParent != null && componentInParent.worldCamera != null;
		}
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00016BDF File Offset: 0x00014DDF
	public void SetSimpleTooltip(string message)
	{
		this.ClearMultiStringTooltip();
		this.AddMultiStringTooltip(message, PluginAssets.Instance.defaultTextStyleSetting);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00016BF8 File Offset: 0x00014DF8
	public void AddMultiStringTooltip(string newString, TextStyleSetting styleSetting)
	{
		this.multiStringToolTips.Add(newString);
		this.styleSettings.Add(styleSetting);
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00016C12 File Offset: 0x00014E12
	public void ClearMultiStringTooltip()
	{
		this.multiStringToolTips.Clear();
		this.styleSettings.Clear();
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00016C2A File Offset: 0x00014E2A
	public string GetMultiString(int idx)
	{
		return this.multiStringToolTips[idx];
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00016C38 File Offset: 0x00014E38
	public TextStyleSetting GetStyleSetting(int idx)
	{
		return this.styleSettings[idx];
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x00016C48 File Offset: 0x00014E48
	public void SetFixedStringKey(string newKey)
	{
		this.FixedStringKey = newKey;
		string text = Strings.Get(new StringKey(this.FixedStringKey));
		this.toolTip = text;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00016C7C File Offset: 0x00014E7C
	public void RebuildDynamicTooltip()
	{
		if (this.OnToolTip != null)
		{
			this.ClearMultiStringTooltip();
			string text = this.OnToolTip();
			if (!string.IsNullOrEmpty(text))
			{
				this.AddMultiStringTooltip(text, PluginAssets.Instance.defaultTextStyleSetting);
				return;
			}
		}
		else if (this.OnComplexToolTip != null)
		{
			this.ClearMultiStringTooltip();
			foreach (global::Tuple<string, TextStyleSetting> tuple in this.OnComplexToolTip())
			{
				this.AddMultiStringTooltip(tuple.first, tuple.second);
			}
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00016D24 File Offset: 0x00014F24
	public void OnPointerEnter(PointerEventData data)
	{
		this.OnHoverStateChanged(true);
		this.isHovering = true;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00016D34 File Offset: 0x00014F34
	public void OnPointerExit(PointerEventData data)
	{
		this.OnHoverStateChanged(false);
		this.isHovering = false;
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00016D44 File Offset: 0x00014F44
	private void OnClick(object data)
	{
		ToolTipScreen.Instance.ClearToolTip(this);
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00016D51 File Offset: 0x00014F51
	private void OnDisable()
	{
		if (ToolTipScreen.Instance)
		{
			ToolTipScreen.Instance.MarkTooltipDirty(this);
		}
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00016D6A File Offset: 0x00014F6A
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (ToolTipScreen.Instance)
		{
			ToolTipScreen.Instance.MarkTooltipDirty(this);
		}
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00016D89 File Offset: 0x00014F89
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (ToolTipScreen.Instance)
		{
			ToolTipScreen.Instance.MakeDirtyTooltipClean(this);
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00016DA8 File Offset: 0x00014FA8
	private void OnHoverStateChanged(bool is_over)
	{
		if (ToolTipScreen.Instance == null)
		{
			return;
		}
		if (is_over)
		{
			ToolTipScreen.Instance.SetToolTip(this);
			return;
		}
		ToolTipScreen.Instance.ClearToolTip(this);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00016DD2 File Offset: 0x00014FD2
	protected override void OnCleanUp()
	{
		if (ToolTipScreen.Instance != null)
		{
			ToolTipScreen.Instance.ClearToolTip(this);
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x00016DEC File Offset: 0x00014FEC
	public void UpdateWhileHovered()
	{
		if (!this.forceRefresh && !this.refreshWhileHovering)
		{
			return;
		}
		if (Time.unscaledTime - this.lastUpdateTime > 0.2f)
		{
			this.lastUpdateTime = Time.unscaledTime;
			if (this.isHovering)
			{
				this.RebuildDynamicTooltip();
				for (int i = 0; i < this.multiStringToolTips.Count; i++)
				{
					ToolTipScreen.Instance.HotSwapTooltipString(this.multiStringToolTips[i], i);
				}
			}
		}
	}

	// Token: 0x040004D4 RID: 1236
	public bool UseFixedStringKey;

	// Token: 0x040004D5 RID: 1237
	public string FixedStringKey = "";

	// Token: 0x040004D6 RID: 1238
	private List<string> multiStringToolTips = new List<string>();

	// Token: 0x040004D7 RID: 1239
	private List<TextStyleSetting> styleSettings = new List<TextStyleSetting>();

	// Token: 0x040004D8 RID: 1240
	public bool worldSpace;

	// Token: 0x040004D9 RID: 1241
	public bool forceRefresh;

	// Token: 0x040004DA RID: 1242
	public bool refreshWhileHovering;

	// Token: 0x040004DB RID: 1243
	private bool isHovering;

	// Token: 0x040004DC RID: 1244
	private float lastUpdateTime;

	// Token: 0x040004DD RID: 1245
	public ToolTip.TooltipPosition toolTipPosition = ToolTip.TooltipPosition.BottomCenter;

	// Token: 0x040004DE RID: 1246
	public Vector2 tooltipPivot = new Vector2(0f, 1f);

	// Token: 0x040004DF RID: 1247
	public Vector2 tooltipPositionOffset = new Vector2(0f, -25f);

	// Token: 0x040004E0 RID: 1248
	public Vector2 parentPositionAnchor = new Vector2(0.5f, 0.5f);

	// Token: 0x040004E1 RID: 1249
	public RectTransform overrideParentObject;

	// Token: 0x040004E2 RID: 1250
	public ToolTip.ToolTipSizeSetting SizingSetting = ToolTip.ToolTipSizeSetting.DynamicWidthNoWrap;

	// Token: 0x040004E3 RID: 1251
	public float WrapWidth = 256f;

	// Token: 0x040004E4 RID: 1252
	private Func<string> _OnToolTip;

	// Token: 0x040004E5 RID: 1253
	public ToolTip.ComplexTooltipDelegate OnComplexToolTip;

	// Token: 0x040004E6 RID: 1254
	private static readonly global::EventSystem.IntraObjectHandler<ToolTip> OnClickDelegate = new global::EventSystem.IntraObjectHandler<ToolTip>(delegate(ToolTip component, object data)
	{
		component.OnClick(data);
	});

	// Token: 0x020009C1 RID: 2497
	public enum TooltipPosition
	{
		// Token: 0x040021CC RID: 8652
		TopLeft,
		// Token: 0x040021CD RID: 8653
		TopCenter,
		// Token: 0x040021CE RID: 8654
		TopRight,
		// Token: 0x040021CF RID: 8655
		BottomLeft,
		// Token: 0x040021D0 RID: 8656
		BottomCenter,
		// Token: 0x040021D1 RID: 8657
		BottomRight,
		// Token: 0x040021D2 RID: 8658
		Custom
	}

	// Token: 0x020009C2 RID: 2498
	public enum ToolTipSizeSetting
	{
		// Token: 0x040021D4 RID: 8660
		MaxWidthWrapContent,
		// Token: 0x040021D5 RID: 8661
		DynamicWidthNoWrap
	}

	// Token: 0x020009C3 RID: 2499
	// (Invoke) Token: 0x06005371 RID: 21361
	public delegate List<global::Tuple<string, TextStyleSetting>> ComplexTooltipDelegate();
}
