using System;
using System.Collections.Generic;

// Token: 0x02000407 RID: 1031
public class StateMachineManager : Singleton<StateMachineManager>, IScheduler
{
	// Token: 0x06001560 RID: 5472 RVA: 0x0006EE15 File Offset: 0x0006D015
	public void RegisterScheduler(Scheduler scheduler)
	{
		this.scheduler = scheduler;
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x0006EE1E File Offset: 0x0006D01E
	public SchedulerHandle Schedule(string name, float time, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, time, callback, callback_data, group);
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x0006EE32 File Offset: 0x0006D032
	public SchedulerHandle ScheduleNextFrame(string name, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, 0f, callback, callback_data, group);
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x0006EE49 File Offset: 0x0006D049
	public SchedulerGroup CreateSchedulerGroup()
	{
		return new SchedulerGroup(this.scheduler);
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x0006EE58 File Offset: 0x0006D058
	public StateMachine CreateStateMachine(Type type)
	{
		StateMachine stateMachine = null;
		if (!this.stateMachines.TryGetValue(type, out stateMachine))
		{
			stateMachine = (StateMachine)Activator.CreateInstance(type);
			stateMachine.CreateStates(stateMachine);
			stateMachine.BindStates();
			stateMachine.InitializeStateMachine();
			this.stateMachines[type] = stateMachine;
			List<Action<StateMachine>> list;
			if (this.stateMachineCreatedCBs.TryGetValue(type, out list))
			{
				foreach (Action<StateMachine> action in list)
				{
					action(stateMachine);
				}
			}
		}
		return stateMachine;
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x0006EEF4 File Offset: 0x0006D0F4
	public T CreateStateMachine<T>()
	{
		return (T)((object)this.CreateStateMachine(typeof(T)));
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x0006EF0C File Offset: 0x0006D10C
	public static void ResetParameters()
	{
		for (int i = 0; i < StateMachineManager.parameters.Length; i++)
		{
			StateMachineManager.parameters[i] = null;
		}
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x0006EF33 File Offset: 0x0006D133
	public StateMachine.Instance CreateSMIFromDef(IStateMachineTarget master, StateMachine.BaseDef def)
	{
		StateMachineManager.parameters[0] = master;
		StateMachineManager.parameters[1] = def;
		return (StateMachine.Instance)Activator.CreateInstance(Singleton<StateMachineManager>.Instance.CreateStateMachine(def.GetStateMachineType()).GetStateMachineInstanceType(), StateMachineManager.parameters);
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x0006EF69 File Offset: 0x0006D169
	public void Clear()
	{
		if (this.scheduler != null)
		{
			this.scheduler.FreeResources();
		}
		if (this.stateMachines != null)
		{
			this.stateMachines.Clear();
		}
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x0006EF94 File Offset: 0x0006D194
	public void AddStateMachineCreatedCallback(Type sm_type, Action<StateMachine> cb)
	{
		List<Action<StateMachine>> list;
		if (!this.stateMachineCreatedCBs.TryGetValue(sm_type, out list))
		{
			list = new List<Action<StateMachine>>();
			this.stateMachineCreatedCBs[sm_type] = list;
		}
		list.Add(cb);
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x0006EFCC File Offset: 0x0006D1CC
	public void RemoveStateMachineCreatedCallback(Type sm_type, Action<StateMachine> cb)
	{
		List<Action<StateMachine>> list;
		if (this.stateMachineCreatedCBs.TryGetValue(sm_type, out list))
		{
			list.Remove(cb);
		}
	}

	// Token: 0x04000BE9 RID: 3049
	private Scheduler scheduler;

	// Token: 0x04000BEA RID: 3050
	private Dictionary<Type, StateMachine> stateMachines = new Dictionary<Type, StateMachine>();

	// Token: 0x04000BEB RID: 3051
	private Dictionary<Type, List<Action<StateMachine>>> stateMachineCreatedCBs = new Dictionary<Type, List<Action<StateMachine>>>();

	// Token: 0x04000BEC RID: 3052
	private static object[] parameters = new object[2];
}
