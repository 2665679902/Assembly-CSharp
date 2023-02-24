using System;
using UnityEngine;

// Token: 0x020006FC RID: 1788
[AddComponentMenu("KMonoBehaviour/scripts/Decomposer")]
public class Decomposer : KMonoBehaviour
{
	// Token: 0x060030DE RID: 12510 RVA: 0x0010361C File Offset: 0x0010181C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		StateMachineController component = base.GetComponent<StateMachineController>();
		if (component == null)
		{
			return;
		}
		DecompositionMonitor.Instance instance = new DecompositionMonitor.Instance(this, null, 1f, false);
		component.AddStateMachineInstance(instance);
		instance.StartSM();
		instance.dirtyWaterMaxRange = 3;
	}
}
