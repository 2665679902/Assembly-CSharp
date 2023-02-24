using System;
using KSerialization;

// Token: 0x02000401 RID: 1025
[SerializationConfig(MemberSerialization.OptIn)]
public abstract class StateMachineComponent : KMonoBehaviour, ISaveLoadable, IStateMachineTarget
{
	// Token: 0x06001530 RID: 5424
	public abstract StateMachine.Instance GetSMI();

	// Token: 0x04000BDF RID: 3039
	[MyCmpAdd]
	protected StateMachineController stateMachineController;
}
