using System;

// Token: 0x02000400 RID: 1024
public static class StateMachineExtensions
{
	// Token: 0x0600152F RID: 5423 RVA: 0x0006E564 File Offset: 0x0006C764
	public static bool IsNullOrStopped(this StateMachine.Instance smi)
	{
		return smi == null || !smi.IsRunning();
	}
}
