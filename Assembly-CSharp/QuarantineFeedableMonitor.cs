using System;

// Token: 0x0200083C RID: 2108
public class QuarantineFeedableMonitor : GameStateMachine<QuarantineFeedableMonitor, QuarantineFeedableMonitor.Instance>
{
	// Token: 0x06003CDC RID: 15580 RVA: 0x001535D8 File Offset: 0x001517D8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.satisfied.EventTransition(GameHashes.AddUrge, this.hungry, (QuarantineFeedableMonitor.Instance smi) => smi.IsHungry());
		this.hungry.EventTransition(GameHashes.RemoveUrge, this.satisfied, (QuarantineFeedableMonitor.Instance smi) => !smi.IsHungry());
	}

	// Token: 0x040027B6 RID: 10166
	public GameStateMachine<QuarantineFeedableMonitor, QuarantineFeedableMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040027B7 RID: 10167
	public GameStateMachine<QuarantineFeedableMonitor, QuarantineFeedableMonitor.Instance, IStateMachineTarget, object>.State hungry;

	// Token: 0x020015CC RID: 5580
	public new class Instance : GameStateMachine<QuarantineFeedableMonitor, QuarantineFeedableMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600855A RID: 34138 RVA: 0x002EC4E4 File Offset: 0x002EA6E4
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600855B RID: 34139 RVA: 0x002EC4ED File Offset: 0x002EA6ED
		public bool IsHungry()
		{
			return base.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.Eat);
		}
	}
}
