using System;

// Token: 0x020006C8 RID: 1736
public class CritterElementMonitor : GameStateMachine<CritterElementMonitor, CritterElementMonitor.Instance, IStateMachineTarget>
{
	// Token: 0x06002F3A RID: 12090 RVA: 0x000F9A45 File Offset: 0x000F7C45
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update("UpdateInElement", delegate(CritterElementMonitor.Instance smi, float dt)
		{
			smi.UpdateCurrentElement(dt);
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x020013AD RID: 5037
	public new class Instance : GameStateMachine<CritterElementMonitor, CritterElementMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06007E9C RID: 32412 RVA: 0x002D97E0 File Offset: 0x002D79E0
		// (remove) Token: 0x06007E9D RID: 32413 RVA: 0x002D9818 File Offset: 0x002D7A18
		public event Action<float> OnUpdateEggChances;

		// Token: 0x06007E9E RID: 32414 RVA: 0x002D984D File Offset: 0x002D7A4D
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06007E9F RID: 32415 RVA: 0x002D9856 File Offset: 0x002D7A56
		public void UpdateCurrentElement(float dt)
		{
			this.OnUpdateEggChances(dt);
		}
	}
}
