using System;

// Token: 0x02000050 RID: 80
public class PoweredController : GameStateMachine<PoweredController, PoweredController.Instance>
{
	// Token: 0x06000167 RID: 359 RVA: 0x0000A214 File Offset: 0x00008414
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (PoweredController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (PoweredController.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
	}

	// Token: 0x040000CD RID: 205
	public GameStateMachine<PoweredController, PoweredController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040000CE RID: 206
	public GameStateMachine<PoweredController, PoweredController.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x02000DD7 RID: 3543
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000DD8 RID: 3544
	public new class Instance : GameStateMachine<PoweredController, PoweredController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06006AF6 RID: 27382 RVA: 0x00295C8F File Offset: 0x00293E8F
		public Instance(IStateMachineTarget master, PoweredController.Def def)
			: base(master, def)
		{
		}

		// Token: 0x04005060 RID: 20576
		public bool ShowWorkingStatus;
	}
}
