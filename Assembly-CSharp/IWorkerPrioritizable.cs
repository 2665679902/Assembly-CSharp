using System;

// Token: 0x02000379 RID: 889
public interface IWorkerPrioritizable
{
	// Token: 0x0600122C RID: 4652
	bool GetWorkerPriority(Worker worker, out int priority);
}
