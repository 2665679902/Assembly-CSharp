using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B7D RID: 2941
[AddComponentMenu("KMonoBehaviour/scripts/SchedulePaintButton")]
public class SchedulePaintButton : KMonoBehaviour
{
	// Token: 0x17000675 RID: 1653
	// (get) Token: 0x06005C7B RID: 23675 RVA: 0x0021D2C3 File Offset: 0x0021B4C3
	// (set) Token: 0x06005C7C RID: 23676 RVA: 0x0021D2CB File Offset: 0x0021B4CB
	public ScheduleGroup group { get; private set; }

	// Token: 0x06005C7D RID: 23677 RVA: 0x0021D2D4 File Offset: 0x0021B4D4
	public void SetGroup(ScheduleGroup group, Dictionary<string, ColorStyleSetting> styles, Action<SchedulePaintButton> onClick)
	{
		this.group = group;
		if (styles.ContainsKey(group.Id))
		{
			this.toggleState.SetColorStyle(styles[group.Id]);
		}
		this.label.text = group.Name;
		MultiToggle multiToggle = this.toggle;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			onClick(this);
		}));
		this.toolTip.SetSimpleTooltip(group.GetTooltip());
		base.gameObject.name = "PaintButton_" + group.Id;
	}

	// Token: 0x06005C7E RID: 23678 RVA: 0x0021D385 File Offset: 0x0021B585
	public void SetToggle(bool on)
	{
		this.toggle.ChangeState(on ? 1 : 0);
	}

	// Token: 0x04003F2D RID: 16173
	[SerializeField]
	private LocText label;

	// Token: 0x04003F2E RID: 16174
	[SerializeField]
	private ImageToggleState toggleState;

	// Token: 0x04003F2F RID: 16175
	[SerializeField]
	private MultiToggle toggle;

	// Token: 0x04003F30 RID: 16176
	[SerializeField]
	private ToolTip toolTip;
}
