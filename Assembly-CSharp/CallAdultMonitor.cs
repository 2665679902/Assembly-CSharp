using System;
using UnityEngine;

// Token: 0x020006C2 RID: 1730
public class CallAdultMonitor : GameStateMachine<CallAdultMonitor, CallAdultMonitor.Instance, IStateMachineTarget, CallAdultMonitor.Def>
{
	// Token: 0x06002F13 RID: 12051 RVA: 0x000F8E90 File Offset: 0x000F7090
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Behaviours.CallAdultBehaviour, new StateMachine<CallAdultMonitor, CallAdultMonitor.Instance, IStateMachineTarget, CallAdultMonitor.Def>.Transition.ConditionCallback(CallAdultMonitor.ShouldCallAdult), delegate(CallAdultMonitor.Instance smi)
		{
			smi.RefreshCallTime();
		});
	}

	// Token: 0x06002F14 RID: 12052 RVA: 0x000F8EE1 File Offset: 0x000F70E1
	public static bool ShouldCallAdult(CallAdultMonitor.Instance smi)
	{
		return Time.time >= smi.nextCallTime;
	}

	// Token: 0x020013A0 RID: 5024
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006132 RID: 24882
		public float callMinInterval = 120f;

		// Token: 0x04006133 RID: 24883
		public float callMaxInterval = 240f;
	}

	// Token: 0x020013A1 RID: 5025
	public new class Instance : GameStateMachine<CallAdultMonitor, CallAdultMonitor.Instance, IStateMachineTarget, CallAdultMonitor.Def>.GameInstance
	{
		// Token: 0x06007E76 RID: 32374 RVA: 0x002D949F File Offset: 0x002D769F
		public Instance(IStateMachineTarget master, CallAdultMonitor.Def def)
			: base(master, def)
		{
			this.RefreshCallTime();
		}

		// Token: 0x06007E77 RID: 32375 RVA: 0x002D94AF File Offset: 0x002D76AF
		public void RefreshCallTime()
		{
			this.nextCallTime = Time.time + UnityEngine.Random.value * (base.def.callMaxInterval - base.def.callMinInterval) + base.def.callMinInterval;
		}

		// Token: 0x04006134 RID: 24884
		public float nextCallTime;
	}
}
