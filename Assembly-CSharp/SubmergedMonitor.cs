using System;

// Token: 0x020006ED RID: 1773
public class SubmergedMonitor : GameStateMachine<SubmergedMonitor, SubmergedMonitor.Instance, IStateMachineTarget, SubmergedMonitor.Def>
{
	// Token: 0x06003035 RID: 12341 RVA: 0x000FECA0 File Offset: 0x000FCEA0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.Enter("SetNavType", delegate(SubmergedMonitor.Instance smi)
		{
			smi.GetComponent<Navigator>().SetCurrentNavType(NavType.Hover);
		}).Update("SetNavType", delegate(SubmergedMonitor.Instance smi, float dt)
		{
			smi.GetComponent<Navigator>().SetCurrentNavType(NavType.Hover);
		}, UpdateRate.SIM_1000ms, false).Transition(this.submerged, (SubmergedMonitor.Instance smi) => smi.IsSubmerged(), UpdateRate.SIM_1000ms);
		this.submerged.Enter("SetNavType", delegate(SubmergedMonitor.Instance smi)
		{
			smi.GetComponent<Navigator>().SetCurrentNavType(NavType.Swim);
		}).Update("SetNavType", delegate(SubmergedMonitor.Instance smi, float dt)
		{
			smi.GetComponent<Navigator>().SetCurrentNavType(NavType.Swim);
		}, UpdateRate.SIM_1000ms, false).Transition(this.satisfied, (SubmergedMonitor.Instance smi) => !smi.IsSubmerged(), UpdateRate.SIM_1000ms)
			.ToggleTag(GameTags.Creatures.Submerged);
	}

	// Token: 0x04001D10 RID: 7440
	public GameStateMachine<SubmergedMonitor, SubmergedMonitor.Instance, IStateMachineTarget, SubmergedMonitor.Def>.State satisfied;

	// Token: 0x04001D11 RID: 7441
	public GameStateMachine<SubmergedMonitor, SubmergedMonitor.Instance, IStateMachineTarget, SubmergedMonitor.Def>.State submerged;

	// Token: 0x02001409 RID: 5129
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200140A RID: 5130
	public new class Instance : GameStateMachine<SubmergedMonitor, SubmergedMonitor.Instance, IStateMachineTarget, SubmergedMonitor.Def>.GameInstance
	{
		// Token: 0x06007FDB RID: 32731 RVA: 0x002DDF50 File Offset: 0x002DC150
		public Instance(IStateMachineTarget master, SubmergedMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007FDC RID: 32732 RVA: 0x002DDF5A File Offset: 0x002DC15A
		public bool IsSubmerged()
		{
			return Grid.IsSubstantialLiquid(Grid.PosToCell(base.transform.GetPosition()), 0.35f);
		}
	}
}
