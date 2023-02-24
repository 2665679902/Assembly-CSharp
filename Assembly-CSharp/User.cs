using System;
using UnityEngine;

// Token: 0x020003AD RID: 941
[AddComponentMenu("KMonoBehaviour/scripts/User")]
public class User : KMonoBehaviour
{
	// Token: 0x06001364 RID: 4964 RVA: 0x00066E03 File Offset: 0x00065003
	public void OnStateMachineStop(string reason, StateMachine.Status status)
	{
		if (status == StateMachine.Status.Success)
		{
			base.Trigger(58624316, null);
			return;
		}
		base.Trigger(1572098533, null);
	}
}
