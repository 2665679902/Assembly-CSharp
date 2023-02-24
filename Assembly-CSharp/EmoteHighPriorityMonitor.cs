using System;

// Token: 0x02000829 RID: 2089
public class EmoteHighPriorityMonitor : GameStateMachine<EmoteHighPriorityMonitor, EmoteHighPriorityMonitor.Instance>
{
	// Token: 0x06003C78 RID: 15480 RVA: 0x00151054 File Offset: 0x0014F254
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.ready;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.ready.ToggleUrge(Db.Get().Urges.EmoteHighPriority).EventHandler(GameHashes.BeginChore, delegate(EmoteHighPriorityMonitor.Instance smi, object o)
		{
			smi.OnStartChore(o);
		});
		this.resetting.GoTo(this.ready);
	}

	// Token: 0x04002761 RID: 10081
	public GameStateMachine<EmoteHighPriorityMonitor, EmoteHighPriorityMonitor.Instance, IStateMachineTarget, object>.State ready;

	// Token: 0x04002762 RID: 10082
	public GameStateMachine<EmoteHighPriorityMonitor, EmoteHighPriorityMonitor.Instance, IStateMachineTarget, object>.State resetting;

	// Token: 0x020015A1 RID: 5537
	public new class Instance : GameStateMachine<EmoteHighPriorityMonitor, EmoteHighPriorityMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008497 RID: 33943 RVA: 0x002EA4D8 File Offset: 0x002E86D8
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06008498 RID: 33944 RVA: 0x002EA4E1 File Offset: 0x002E86E1
		public void OnStartChore(object o)
		{
			if (((Chore)o).SatisfiesUrge(Db.Get().Urges.EmoteHighPriority))
			{
				this.GoTo(base.sm.resetting);
			}
		}
	}
}
