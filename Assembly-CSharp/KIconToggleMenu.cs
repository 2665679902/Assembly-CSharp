using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AC7 RID: 2759
public class KIconToggleMenu : KScreen
{
	// Token: 0x14000024 RID: 36
	// (add) Token: 0x0600547E RID: 21630 RVA: 0x001EAD50 File Offset: 0x001E8F50
	// (remove) Token: 0x0600547F RID: 21631 RVA: 0x001EAD88 File Offset: 0x001E8F88
	public event KIconToggleMenu.OnSelect onSelect;

	// Token: 0x06005480 RID: 21632 RVA: 0x001EADBD File Offset: 0x001E8FBD
	public void Setup(IList<KIconToggleMenu.ToggleInfo> toggleInfo)
	{
		this.toggleInfo = toggleInfo;
		this.RefreshButtons();
	}

	// Token: 0x06005481 RID: 21633 RVA: 0x001EADCC File Offset: 0x001E8FCC
	protected void Setup()
	{
		this.RefreshButtons();
	}

	// Token: 0x06005482 RID: 21634 RVA: 0x001EADD4 File Offset: 0x001E8FD4
	protected virtual void RefreshButtons()
	{
		foreach (KToggle ktoggle in this.toggles)
		{
			if (ktoggle != null)
			{
				if (!this.dontDestroyToggles.Contains(ktoggle))
				{
					UnityEngine.Object.Destroy(ktoggle.gameObject);
				}
				else
				{
					ktoggle.ClearOnClick();
				}
			}
		}
		this.toggles.Clear();
		this.dontDestroyToggles.Clear();
		if (this.toggleInfo == null)
		{
			return;
		}
		Transform transform = ((this.toggleParent != null) ? this.toggleParent : base.transform);
		for (int i = 0; i < this.toggleInfo.Count; i++)
		{
			int idx = i;
			KIconToggleMenu.ToggleInfo toggleInfo = this.toggleInfo[i];
			KToggle ktoggle2;
			if (toggleInfo.instanceOverride != null)
			{
				ktoggle2 = toggleInfo.instanceOverride;
				this.dontDestroyToggles.Add(ktoggle2);
			}
			else if (toggleInfo.prefabOverride)
			{
				ktoggle2 = Util.KInstantiateUI<KToggle>(toggleInfo.prefabOverride.gameObject, transform.gameObject, true);
			}
			else
			{
				ktoggle2 = Util.KInstantiateUI<KToggle>(this.prefab.gameObject, transform.gameObject, true);
			}
			ktoggle2.Deselect();
			ktoggle2.gameObject.name = "Toggle:" + toggleInfo.text;
			ktoggle2.group = this.group;
			ktoggle2.onClick += delegate
			{
				this.OnClick(idx);
			};
			LocText componentInChildren = ktoggle2.transform.GetComponentInChildren<LocText>();
			if (componentInChildren != null)
			{
				componentInChildren.SetText(toggleInfo.text);
			}
			if (toggleInfo.getSpriteCB != null)
			{
				ktoggle2.fgImage.sprite = toggleInfo.getSpriteCB();
			}
			else if (toggleInfo.icon != null)
			{
				ktoggle2.fgImage.sprite = Assets.GetSprite(toggleInfo.icon);
			}
			toggleInfo.SetToggle(ktoggle2);
			this.toggles.Add(ktoggle2);
		}
	}

	// Token: 0x06005483 RID: 21635 RVA: 0x001EAFFC File Offset: 0x001E91FC
	public Sprite GetIcon(string name)
	{
		foreach (Sprite sprite in this.icons)
		{
			if (sprite.name == name)
			{
				return sprite;
			}
		}
		return null;
	}

	// Token: 0x06005484 RID: 21636 RVA: 0x001EB034 File Offset: 0x001E9234
	public virtual void ClearSelection()
	{
		if (this.toggles == null)
		{
			return;
		}
		foreach (KToggle ktoggle in this.toggles)
		{
			ktoggle.Deselect();
			ktoggle.ClearAnimState();
		}
		this.selected = -1;
	}

	// Token: 0x06005485 RID: 21637 RVA: 0x001EB09C File Offset: 0x001E929C
	private void OnClick(int i)
	{
		if (this.onSelect == null)
		{
			return;
		}
		this.selected = i;
		this.onSelect(this.toggleInfo[i]);
		if (!this.toggles[i].isOn)
		{
			this.selected = -1;
		}
		for (int j = 0; j < this.toggles.Count; j++)
		{
			if (j != this.selected)
			{
				this.toggles[j].isOn = false;
			}
		}
	}

	// Token: 0x06005486 RID: 21638 RVA: 0x001EB11C File Offset: 0x001E931C
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.toggles == null)
		{
			return;
		}
		if (this.toggleInfo == null)
		{
			return;
		}
		for (int i = 0; i < this.toggleInfo.Count; i++)
		{
			if (this.toggles[i].isActiveAndEnabled)
			{
				global::Action hotKey = this.toggleInfo[i].hotKey;
				if (hotKey != global::Action.NumActions && e.TryConsume(hotKey))
				{
					if (this.selected != i || this.repeatKeyDownToggles)
					{
						this.toggles[i].Click();
						if (this.selected == i)
						{
							this.toggles[i].Deselect();
						}
						this.selected = i;
						return;
					}
					break;
				}
			}
		}
	}

	// Token: 0x06005487 RID: 21639 RVA: 0x001EB1CE File Offset: 0x001E93CE
	public virtual void Close()
	{
		this.ClearSelection();
		this.Show(false);
	}

	// Token: 0x04003976 RID: 14710
	[SerializeField]
	private Transform toggleParent;

	// Token: 0x04003977 RID: 14711
	[SerializeField]
	private KToggle prefab;

	// Token: 0x04003978 RID: 14712
	[SerializeField]
	private ToggleGroup group;

	// Token: 0x04003979 RID: 14713
	[SerializeField]
	private Sprite[] icons;

	// Token: 0x0400397A RID: 14714
	[SerializeField]
	public TextStyleSetting ToggleToolTipTextStyleSetting;

	// Token: 0x0400397B RID: 14715
	[SerializeField]
	public TextStyleSetting ToggleToolTipHeaderTextStyleSetting;

	// Token: 0x0400397C RID: 14716
	[SerializeField]
	protected bool repeatKeyDownToggles = true;

	// Token: 0x0400397D RID: 14717
	protected KToggle currentlySelectedToggle;

	// Token: 0x0400397F RID: 14719
	protected IList<KIconToggleMenu.ToggleInfo> toggleInfo;

	// Token: 0x04003980 RID: 14720
	protected List<KToggle> toggles = new List<KToggle>();

	// Token: 0x04003981 RID: 14721
	private List<KToggle> dontDestroyToggles = new List<KToggle>();

	// Token: 0x04003982 RID: 14722
	protected int selected = -1;

	// Token: 0x02001948 RID: 6472
	// (Invoke) Token: 0x06008FBE RID: 36798
	public delegate void OnSelect(KIconToggleMenu.ToggleInfo toggleInfo);

	// Token: 0x02001949 RID: 6473
	public class ToggleInfo
	{
		// Token: 0x06008FC1 RID: 36801 RVA: 0x00310EE0 File Offset: 0x0030F0E0
		public ToggleInfo(string text, string icon, object user_data = null, global::Action hotkey = global::Action.NumActions, string tooltip = "", string tooltip_header = "")
		{
			this.text = text;
			this.userData = user_data;
			this.icon = icon;
			this.hotKey = hotkey;
			this.tooltip = tooltip;
			this.tooltipHeader = tooltip_header;
			this.getTooltipText = new ToolTip.ComplexTooltipDelegate(this.DefaultGetTooltipText);
		}

		// Token: 0x06008FC2 RID: 36802 RVA: 0x00310F33 File Offset: 0x0030F133
		public ToggleInfo(string text, object user_data, global::Action hotkey, Func<Sprite> get_sprite_cb)
		{
			this.text = text;
			this.userData = user_data;
			this.hotKey = hotkey;
			this.getSpriteCB = get_sprite_cb;
		}

		// Token: 0x06008FC3 RID: 36803 RVA: 0x00310F58 File Offset: 0x0030F158
		public virtual void SetToggle(KToggle toggle)
		{
			this.toggle = toggle;
			toggle.GetComponent<ToolTip>().OnComplexToolTip = this.getTooltipText;
		}

		// Token: 0x06008FC4 RID: 36804 RVA: 0x00310F74 File Offset: 0x0030F174
		protected virtual List<global::Tuple<string, TextStyleSetting>> DefaultGetTooltipText()
		{
			List<global::Tuple<string, TextStyleSetting>> list = new List<global::Tuple<string, TextStyleSetting>>();
			if (this.tooltipHeader != null)
			{
				list.Add(new global::Tuple<string, TextStyleSetting>(this.tooltipHeader, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
			}
			list.Add(new global::Tuple<string, TextStyleSetting>(this.tooltip, ToolTipScreen.Instance.defaultTooltipBodyStyle));
			return list;
		}

		// Token: 0x040073E8 RID: 29672
		public string text;

		// Token: 0x040073E9 RID: 29673
		public object userData;

		// Token: 0x040073EA RID: 29674
		public string icon;

		// Token: 0x040073EB RID: 29675
		public string tooltip;

		// Token: 0x040073EC RID: 29676
		public string tooltipHeader;

		// Token: 0x040073ED RID: 29677
		public KToggle toggle;

		// Token: 0x040073EE RID: 29678
		public global::Action hotKey;

		// Token: 0x040073EF RID: 29679
		public ToolTip.ComplexTooltipDelegate getTooltipText;

		// Token: 0x040073F0 RID: 29680
		public Func<Sprite> getSpriteCB;

		// Token: 0x040073F1 RID: 29681
		public KToggle prefabOverride;

		// Token: 0x040073F2 RID: 29682
		public KToggle instanceOverride;
	}
}
