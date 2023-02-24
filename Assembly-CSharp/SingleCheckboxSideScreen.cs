using System;
using UnityEngine;

// Token: 0x02000BE1 RID: 3041
public class SingleCheckboxSideScreen : SideScreenContent
{
	// Token: 0x06005FCF RID: 24527 RVA: 0x0023132B File Offset: 0x0022F52B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06005FD0 RID: 24528 RVA: 0x00231333 File Offset: 0x0022F533
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.toggle.onValueChanged += this.OnValueChanged;
	}

	// Token: 0x06005FD1 RID: 24529 RVA: 0x00231352 File Offset: 0x0022F552
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<ICheckboxControl>() != null || target.GetSMI<ICheckboxControl>() != null;
	}

	// Token: 0x06005FD2 RID: 24530 RVA: 0x00231368 File Offset: 0x0022F568
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		if (target == null)
		{
			global::Debug.LogError("The target object provided was null");
			return;
		}
		this.target = target.GetComponent<ICheckboxControl>();
		if (this.target == null)
		{
			this.target = target.GetSMI<ICheckboxControl>();
		}
		if (this.target == null)
		{
			global::Debug.LogError("The target provided does not have an ICheckboxControl component");
			return;
		}
		this.label.text = this.target.CheckboxLabel;
		this.toggle.transform.parent.GetComponent<ToolTip>().SetSimpleTooltip(this.target.CheckboxTooltip);
		this.titleKey = this.target.CheckboxTitleKey;
		this.toggle.isOn = this.target.GetCheckboxValue();
		this.toggleCheckMark.enabled = this.toggle.isOn;
	}

	// Token: 0x06005FD3 RID: 24531 RVA: 0x0023143B File Offset: 0x0022F63B
	public override void ClearTarget()
	{
		base.ClearTarget();
		this.target = null;
	}

	// Token: 0x06005FD4 RID: 24532 RVA: 0x0023144A File Offset: 0x0022F64A
	private void OnValueChanged(bool value)
	{
		this.target.SetCheckboxValue(value);
		this.toggleCheckMark.enabled = value;
	}

	// Token: 0x040041A1 RID: 16801
	public KToggle toggle;

	// Token: 0x040041A2 RID: 16802
	public KImage toggleCheckMark;

	// Token: 0x040041A3 RID: 16803
	public LocText label;

	// Token: 0x040041A4 RID: 16804
	private ICheckboxControl target;
}
