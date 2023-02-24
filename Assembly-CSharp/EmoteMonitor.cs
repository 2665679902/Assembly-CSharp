using System;
using UnityEngine;

// Token: 0x0200082A RID: 2090
public class EmoteMonitor : GameStateMachine<EmoteMonitor, EmoteMonitor.Instance>
{
	// Token: 0x06003C7A RID: 15482 RVA: 0x001510D0 File Offset: 0x0014F2D0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.satisfied.ScheduleGoTo((float)UnityEngine.Random.Range(30, 90), this.ready);
		this.ready.ToggleUrge(Db.Get().Urges.Emote).EventHandler(GameHashes.BeginChore, delegate(EmoteMonitor.Instance smi, object o)
		{
			smi.OnStartChore(o);
		});
	}

	// Token: 0x04002763 RID: 10083
	public GameStateMachine<EmoteMonitor, EmoteMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x04002764 RID: 10084
	public GameStateMachine<EmoteMonitor, EmoteMonitor.Instance, IStateMachineTarget, object>.State ready;

	// Token: 0x020015A3 RID: 5539
	public new class Instance : GameStateMachine<EmoteMonitor, EmoteMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600849C RID: 33948 RVA: 0x002EA52D File Offset: 0x002E872D
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600849D RID: 33949 RVA: 0x002EA536 File Offset: 0x002E8736
		public void OnStartChore(object o)
		{
			if (((Chore)o).SatisfiesUrge(Db.Get().Urges.Emote))
			{
				this.GoTo(base.sm.satisfied);
			}
		}
	}
}
