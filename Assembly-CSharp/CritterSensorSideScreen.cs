using System;
using UnityEngine;

// Token: 0x02000BAB RID: 2987
public class CritterSensorSideScreen : SideScreenContent
{
	// Token: 0x06005DFB RID: 24059 RVA: 0x00225642 File Offset: 0x00223842
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.countCrittersToggle.onClick += this.ToggleCritters;
		this.countEggsToggle.onClick += this.ToggleEggs;
	}

	// Token: 0x06005DFC RID: 24060 RVA: 0x00225678 File Offset: 0x00223878
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicCritterCountSensor>() != null;
	}

	// Token: 0x06005DFD RID: 24061 RVA: 0x00225688 File Offset: 0x00223888
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetSensor = target.GetComponent<LogicCritterCountSensor>();
		this.crittersCheckmark.enabled = this.targetSensor.countCritters;
		this.eggsCheckmark.enabled = this.targetSensor.countEggs;
	}

	// Token: 0x06005DFE RID: 24062 RVA: 0x002256D4 File Offset: 0x002238D4
	private void ToggleCritters()
	{
		this.targetSensor.countCritters = !this.targetSensor.countCritters;
		this.crittersCheckmark.enabled = this.targetSensor.countCritters;
	}

	// Token: 0x06005DFF RID: 24063 RVA: 0x00225705 File Offset: 0x00223905
	private void ToggleEggs()
	{
		this.targetSensor.countEggs = !this.targetSensor.countEggs;
		this.eggsCheckmark.enabled = this.targetSensor.countEggs;
	}

	// Token: 0x04004044 RID: 16452
	public LogicCritterCountSensor targetSensor;

	// Token: 0x04004045 RID: 16453
	public KToggle countCrittersToggle;

	// Token: 0x04004046 RID: 16454
	public KToggle countEggsToggle;

	// Token: 0x04004047 RID: 16455
	public KImage crittersCheckmark;

	// Token: 0x04004048 RID: 16456
	public KImage eggsCheckmark;
}
