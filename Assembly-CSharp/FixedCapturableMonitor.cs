using System;

// Token: 0x02000468 RID: 1128
public class FixedCapturableMonitor : GameStateMachine<FixedCapturableMonitor, FixedCapturableMonitor.Instance, IStateMachineTarget, FixedCapturableMonitor.Def>
{
	// Token: 0x06001903 RID: 6403 RVA: 0x00085A8C File Offset: 0x00083C8C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToGetCaptured, (FixedCapturableMonitor.Instance smi) => smi.ShouldGoGetCaptured(), null).Enter(delegate(FixedCapturableMonitor.Instance smi)
		{
			Components.FixedCapturableMonitors.Add(smi);
		}).Exit(delegate(FixedCapturableMonitor.Instance smi)
		{
			Components.FixedCapturableMonitors.Remove(smi);
		});
	}

	// Token: 0x0200109C RID: 4252
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200109D RID: 4253
	public new class Instance : GameStateMachine<FixedCapturableMonitor, FixedCapturableMonitor.Instance, IStateMachineTarget, FixedCapturableMonitor.Def>.GameInstance
	{
		// Token: 0x060073BE RID: 29630 RVA: 0x002B16EA File Offset: 0x002AF8EA
		public Instance(IStateMachineTarget master, FixedCapturableMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x002B16F4 File Offset: 0x002AF8F4
		public bool ShouldGoGetCaptured()
		{
			return this.targetCapturePoint != null && this.targetCapturePoint.IsRunning() && this.targetCapturePoint.shouldCreatureGoGetCaptured;
		}

		// Token: 0x04005832 RID: 22578
		public FixedCapturePoint.Instance targetCapturePoint;
	}
}
