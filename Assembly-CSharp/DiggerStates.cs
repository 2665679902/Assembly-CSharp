using System;

// Token: 0x020000B7 RID: 183
public class DiggerStates : GameStateMachine<DiggerStates, DiggerStates.Instance, IStateMachineTarget, DiggerStates.Def>
{
	// Token: 0x0600033D RID: 829 RVA: 0x00019B16 File Offset: 0x00017D16
	private static bool ShouldStopHiding(DiggerStates.Instance smi)
	{
		return !GameplayEventManager.Instance.IsGameplayEventRunningWithTag(GameTags.SpaceDanger);
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00019B2C File Offset: 0x00017D2C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.move;
		this.move.MoveTo((DiggerStates.Instance smi) => smi.GetTunnelCell(), this.hide, this.behaviourcomplete, false);
		this.hide.Transition(this.behaviourcomplete, new StateMachine<DiggerStates, DiggerStates.Instance, IStateMachineTarget, DiggerStates.Def>.Transition.ConditionCallback(DiggerStates.ShouldStopHiding), UpdateRate.SIM_4000ms);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Tunnel, false);
	}

	// Token: 0x0400021A RID: 538
	public GameStateMachine<DiggerStates, DiggerStates.Instance, IStateMachineTarget, DiggerStates.Def>.State move;

	// Token: 0x0400021B RID: 539
	public GameStateMachine<DiggerStates, DiggerStates.Instance, IStateMachineTarget, DiggerStates.Def>.State hide;

	// Token: 0x0400021C RID: 540
	public GameStateMachine<DiggerStates, DiggerStates.Instance, IStateMachineTarget, DiggerStates.Def>.State behaviourcomplete;

	// Token: 0x02000E42 RID: 3650
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E43 RID: 3651
	public new class Instance : GameStateMachine<DiggerStates, DiggerStates.Instance, IStateMachineTarget, DiggerStates.Def>.GameInstance
	{
		// Token: 0x06006BE8 RID: 27624 RVA: 0x0029724E File Offset: 0x0029544E
		public Instance(Chore<DiggerStates.Instance> chore, DiggerStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Tunnel);
		}

		// Token: 0x06006BE9 RID: 27625 RVA: 0x00297274 File Offset: 0x00295474
		public int GetTunnelCell()
		{
			DiggerMonitor.Instance smi = base.smi.GetSMI<DiggerMonitor.Instance>();
			if (smi != null)
			{
				return smi.lastDigCell;
			}
			return -1;
		}
	}
}
