using System;
using Klei.AI;

// Token: 0x02000838 RID: 2104
public class MournMonitor : GameStateMachine<MournMonitor, MournMonitor.Instance>
{
	// Token: 0x06003CB3 RID: 15539 RVA: 0x00152CA0 File Offset: 0x00150EA0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.EventHandler(GameHashes.EffectAdded, new GameStateMachine<MournMonitor, MournMonitor.Instance, IStateMachineTarget, object>.GameEvent.Callback(this.OnEffectAdded)).Enter(delegate(MournMonitor.Instance smi)
		{
			if (this.ShouldMourn(smi))
			{
				smi.GoTo(this.needsToMourn);
			}
		});
		this.needsToMourn.ToggleChore((MournMonitor.Instance smi) => new MournChore(smi.master), this.idle);
	}

	// Token: 0x06003CB4 RID: 15540 RVA: 0x00152D14 File Offset: 0x00150F14
	private bool ShouldMourn(MournMonitor.Instance smi)
	{
		Effect effect = Db.Get().effects.Get("Mourning");
		return smi.master.GetComponent<Effects>().HasEffect(effect);
	}

	// Token: 0x06003CB5 RID: 15541 RVA: 0x00152D47 File Offset: 0x00150F47
	private void OnEffectAdded(MournMonitor.Instance smi, object data)
	{
		if (this.ShouldMourn(smi))
		{
			smi.GoTo(this.needsToMourn);
		}
	}

	// Token: 0x040027A5 RID: 10149
	private GameStateMachine<MournMonitor, MournMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x040027A6 RID: 10150
	private GameStateMachine<MournMonitor, MournMonitor.Instance, IStateMachineTarget, object>.State needsToMourn;

	// Token: 0x020015C5 RID: 5573
	public new class Instance : GameStateMachine<MournMonitor, MournMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008546 RID: 34118 RVA: 0x002EC313 File Offset: 0x002EA513
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}
	}
}
