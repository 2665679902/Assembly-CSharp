using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B96 RID: 2966
public class AutomatableSideScreen : SideScreenContent
{
	// Token: 0x06005D4C RID: 23884 RVA: 0x00221A6E File Offset: 0x0021FC6E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06005D4D RID: 23885 RVA: 0x00221A78 File Offset: 0x0021FC78
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.allowManualToggle.transform.parent.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.AUTOMATABLE_SIDE_SCREEN.ALLOWMANUALBUTTONTOOLTIP);
		this.allowManualToggle.onValueChanged += this.OnAllowManualChanged;
	}

	// Token: 0x06005D4E RID: 23886 RVA: 0x00221AC6 File Offset: 0x0021FCC6
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Automatable>() != null;
	}

	// Token: 0x06005D4F RID: 23887 RVA: 0x00221AD4 File Offset: 0x0021FCD4
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		if (target == null)
		{
			global::Debug.LogError("The target object provided was null");
			return;
		}
		this.targetAutomatable = target.GetComponent<Automatable>();
		if (this.targetAutomatable == null)
		{
			global::Debug.LogError("The target provided does not have an Automatable component");
			return;
		}
		this.allowManualToggle.isOn = !this.targetAutomatable.GetAutomationOnly();
		this.allowManualToggleCheckMark.enabled = this.allowManualToggle.isOn;
	}

	// Token: 0x06005D50 RID: 23888 RVA: 0x00221B50 File Offset: 0x0021FD50
	private void OnAllowManualChanged(bool value)
	{
		this.targetAutomatable.SetAutomationOnly(!value);
		this.allowManualToggleCheckMark.enabled = value;
	}

	// Token: 0x04003FC9 RID: 16329
	public KToggle allowManualToggle;

	// Token: 0x04003FCA RID: 16330
	public KImage allowManualToggleCheckMark;

	// Token: 0x04003FCB RID: 16331
	public GameObject content;

	// Token: 0x04003FCC RID: 16332
	private GameObject target;

	// Token: 0x04003FCD RID: 16333
	public LocText DescriptionText;

	// Token: 0x04003FCE RID: 16334
	private Automatable targetAutomatable;
}
