using System;

// Token: 0x020003E2 RID: 994
public interface IScheduler
{
	// Token: 0x060014A5 RID: 5285
	SchedulerHandle Schedule(string name, float time, Action<object> callback, object callback_data = null, SchedulerGroup group = null);
}
