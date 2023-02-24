using System;
using UnityEngine;

// Token: 0x02000BCF RID: 3023
public class ProgressBarSideScreen : SideScreenContent, IRender1000ms
{
	// Token: 0x06005F14 RID: 24340 RVA: 0x0022CB83 File Offset: 0x0022AD83
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06005F15 RID: 24341 RVA: 0x0022CB8B File Offset: 0x0022AD8B
	public override int GetSideScreenSortOrder()
	{
		return -10;
	}

	// Token: 0x06005F16 RID: 24342 RVA: 0x0022CB8F File Offset: 0x0022AD8F
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IProgressBarSideScreen>() != null;
	}

	// Token: 0x06005F17 RID: 24343 RVA: 0x0022CB9A File Offset: 0x0022AD9A
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetObject = target.GetComponent<IProgressBarSideScreen>();
		this.RefreshBar();
	}

	// Token: 0x06005F18 RID: 24344 RVA: 0x0022CBB8 File Offset: 0x0022ADB8
	private void RefreshBar()
	{
		this.progressBar.SetMaxValue(this.targetObject.GetProgressBarMaxValue());
		this.progressBar.SetFillPercentage(this.targetObject.GetProgressBarFillPercentage());
		this.progressBar.label.SetText(this.targetObject.GetProgressBarLabel());
		this.label.SetText(this.targetObject.GetProgressBarTitleLabel());
		this.progressBar.GetComponentInChildren<ToolTip>().SetSimpleTooltip(this.targetObject.GetProgressBarTooltip());
	}

	// Token: 0x06005F19 RID: 24345 RVA: 0x0022CC3D File Offset: 0x0022AE3D
	public void Render1000ms(float dt)
	{
		this.RefreshBar();
	}

	// Token: 0x04004118 RID: 16664
	public LocText label;

	// Token: 0x04004119 RID: 16665
	public GenericUIProgressBar progressBar;

	// Token: 0x0400411A RID: 16666
	public IProgressBarSideScreen targetObject;
}
