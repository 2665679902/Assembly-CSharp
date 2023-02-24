using System;
using UnityEngine;

// Token: 0x02000B5F RID: 2911
[AddComponentMenu("KMonoBehaviour/scripts/PriorityButton")]
public class PriorityButton : KMonoBehaviour
{
	// Token: 0x1700066D RID: 1645
	// (get) Token: 0x06005ABF RID: 23231 RVA: 0x0020EFD0 File Offset: 0x0020D1D0
	// (set) Token: 0x06005AC0 RID: 23232 RVA: 0x0020EFD8 File Offset: 0x0020D1D8
	public PrioritySetting priority
	{
		get
		{
			return this._priority;
		}
		set
		{
			this._priority = value;
			if (this.its != null)
			{
				if (this.priority.priority_class == PriorityScreen.PriorityClass.high)
				{
					this.its.colorStyleSetting = this.highStyle;
				}
				else
				{
					this.its.colorStyleSetting = this.normalStyle;
				}
				this.its.RefreshColorStyle();
				this.its.ResetColor();
			}
		}
	}

	// Token: 0x06005AC1 RID: 23233 RVA: 0x0020F042 File Offset: 0x0020D242
	protected override void OnPrefabInit()
	{
		this.toggle.onClick += this.OnClick;
	}

	// Token: 0x06005AC2 RID: 23234 RVA: 0x0020F05B File Offset: 0x0020D25B
	private void OnClick()
	{
		if (this.playSelectionSound)
		{
			PriorityScreen.PlayPriorityConfirmSound(this.priority);
		}
		if (this.onClick != null)
		{
			this.onClick(this.priority);
		}
	}

	// Token: 0x04003D75 RID: 15733
	public KToggle toggle;

	// Token: 0x04003D76 RID: 15734
	public LocText text;

	// Token: 0x04003D77 RID: 15735
	public ToolTip tooltip;

	// Token: 0x04003D78 RID: 15736
	[MyCmpGet]
	private ImageToggleState its;

	// Token: 0x04003D79 RID: 15737
	public ColorStyleSetting normalStyle;

	// Token: 0x04003D7A RID: 15738
	public ColorStyleSetting highStyle;

	// Token: 0x04003D7B RID: 15739
	public bool playSelectionSound = true;

	// Token: 0x04003D7C RID: 15740
	public Action<PrioritySetting> onClick;

	// Token: 0x04003D7D RID: 15741
	private PrioritySetting _priority;
}
