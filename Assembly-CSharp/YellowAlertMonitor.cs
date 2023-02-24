using System;

// Token: 0x02000856 RID: 2134
public class YellowAlertMonitor : GameStateMachine<YellowAlertMonitor, YellowAlertMonitor.Instance>
{
	// Token: 0x06003D4C RID: 15692 RVA: 0x00156DA4 File Offset: 0x00154FA4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.off.EventTransition(GameHashes.EnteredYellowAlert, (YellowAlertMonitor.Instance smi) => Game.Instance, this.on, (YellowAlertMonitor.Instance smi) => YellowAlertManager.Instance.Get().IsOn());
		this.on.EventTransition(GameHashes.ExitedYellowAlert, (YellowAlertMonitor.Instance smi) => Game.Instance, this.off, (YellowAlertMonitor.Instance smi) => !YellowAlertManager.Instance.Get().IsOn()).Enter("EnableYellowAlert", delegate(YellowAlertMonitor.Instance smi)
		{
			smi.EnableYellowAlert();
		});
	}

	// Token: 0x0400282A RID: 10282
	public GameStateMachine<YellowAlertMonitor, YellowAlertMonitor.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x0400282B RID: 10283
	public GameStateMachine<YellowAlertMonitor, YellowAlertMonitor.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x0200160D RID: 5645
	public new class Instance : GameStateMachine<YellowAlertMonitor, YellowAlertMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060086A1 RID: 34465 RVA: 0x002EF894 File Offset: 0x002EDA94
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x060086A2 RID: 34466 RVA: 0x002EF89D File Offset: 0x002EDA9D
		public void EnableYellowAlert()
		{
		}
	}
}
