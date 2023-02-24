using System;

// Token: 0x020003AC RID: 940
public abstract class Usable : KMonoBehaviour, IStateMachineTarget
{
	// Token: 0x06001360 RID: 4960
	public abstract void StartUsing(User user);

	// Token: 0x06001361 RID: 4961 RVA: 0x00066D54 File Offset: 0x00064F54
	protected void StartUsing(StateMachine.Instance smi, User user)
	{
		DebugUtil.Assert(this.smi == null);
		DebugUtil.Assert(smi != null);
		this.smi = smi;
		smi.OnStop = (Action<string, StateMachine.Status>)Delegate.Combine(smi.OnStop, new Action<string, StateMachine.Status>(user.OnStateMachineStop));
		smi.StartSM();
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x00066DA8 File Offset: 0x00064FA8
	public void StopUsing(User user)
	{
		if (this.smi != null)
		{
			StateMachine.Instance instance = this.smi;
			instance.OnStop = (Action<string, StateMachine.Status>)Delegate.Remove(instance.OnStop, new Action<string, StateMachine.Status>(user.OnStateMachineStop));
			this.smi.StopSM("Usable.StopUsing");
			this.smi = null;
		}
	}

	// Token: 0x04000A7A RID: 2682
	private StateMachine.Instance smi;
}
