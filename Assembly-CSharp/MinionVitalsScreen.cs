using System;
using UnityEngine;

// Token: 0x02000B31 RID: 2865
public class MinionVitalsScreen : TargetScreen
{
	// Token: 0x060058A0 RID: 22688 RVA: 0x00201FA3 File Offset: 0x002001A3
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<MinionIdentity>();
	}

	// Token: 0x060058A1 RID: 22689 RVA: 0x00201FB0 File Offset: 0x002001B0
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
	}

	// Token: 0x060058A2 RID: 22690 RVA: 0x00201FB9 File Offset: 0x002001B9
	public override void OnSelectTarget(GameObject target)
	{
		this.panel.selectedEntity = target;
		this.panel.Refresh();
	}

	// Token: 0x060058A3 RID: 22691 RVA: 0x00201FD2 File Offset: 0x002001D2
	public override void OnDeselectTarget(GameObject target)
	{
	}

	// Token: 0x060058A4 RID: 22692 RVA: 0x00201FD4 File Offset: 0x002001D4
	protected override void OnActivate()
	{
		base.OnActivate();
		if (this.panel == null)
		{
			this.panel = base.GetComponent<MinionVitalsPanel>();
		}
		this.panel.Init();
	}

	// Token: 0x04003BE2 RID: 15330
	public MinionVitalsPanel panel;
}
