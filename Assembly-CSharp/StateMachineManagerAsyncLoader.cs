using System;
using System.Collections.Generic;

// Token: 0x02000406 RID: 1030
internal class StateMachineManagerAsyncLoader : GlobalAsyncLoader<StateMachineManagerAsyncLoader>
{
	// Token: 0x0600155D RID: 5469 RVA: 0x0006EDE2 File Offset: 0x0006CFE2
	public override void Run()
	{
	}

	// Token: 0x04000BE8 RID: 3048
	public List<StateMachine> stateMachines = new List<StateMachine>();
}
