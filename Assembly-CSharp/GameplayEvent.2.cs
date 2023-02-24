using System;

// Token: 0x020003B0 RID: 944
public abstract class GameplayEvent<StateMachineInstanceType> : GameplayEvent where StateMachineInstanceType : StateMachine.Instance
{
	// Token: 0x06001383 RID: 4995 RVA: 0x00067543 File Offset: 0x00065743
	public GameplayEvent(string id, int priority = 0, int importance = 0)
		: base(id, priority, importance)
	{
	}
}
