using System;

// Token: 0x0200004D RID: 77
public class PoweredActiveController : GameStateMachine<PoweredActiveController, PoweredActiveController.Instance, IStateMachineTarget, PoweredActiveController.Def>
{
	// Token: 0x06000161 RID: 353 RVA: 0x00009D4C File Offset: 0x00007F4C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (PoweredActiveController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (PoweredActiveController.Instance smi) => !smi.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.working.pre, (PoweredActiveController.Instance smi) => smi.GetComponent<Operational>().IsActive);
		this.working.Enter(delegate(PoweredActiveController.Instance smi)
		{
			if (smi.def.showWorkingStatus)
			{
				smi.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.Working, null);
			}
		}).Exit(delegate(PoweredActiveController.Instance smi)
		{
			if (smi.def.showWorkingStatus)
			{
				smi.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.Working, false);
			}
		});
		this.working.pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working.loop);
		this.working.loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.working.pst, (PoweredActiveController.Instance smi) => !smi.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.working.pst, (PoweredActiveController.Instance smi) => !smi.GetComponent<Operational>().IsActive);
		this.working.pst.PlayAnim("working_pst").OnAnimQueueComplete(this.on);
	}

	// Token: 0x040000C0 RID: 192
	public GameStateMachine<PoweredActiveController, PoweredActiveController.Instance, IStateMachineTarget, PoweredActiveController.Def>.State off;

	// Token: 0x040000C1 RID: 193
	public GameStateMachine<PoweredActiveController, PoweredActiveController.Instance, IStateMachineTarget, PoweredActiveController.Def>.State on;

	// Token: 0x040000C2 RID: 194
	public PoweredActiveController.WorkingStates working;

	// Token: 0x02000DCD RID: 3533
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005047 RID: 20551
		public bool showWorkingStatus;
	}

	// Token: 0x02000DCE RID: 3534
	public class WorkingStates : GameStateMachine<PoweredActiveController, PoweredActiveController.Instance, IStateMachineTarget, PoweredActiveController.Def>.State
	{
		// Token: 0x04005048 RID: 20552
		public GameStateMachine<PoweredActiveController, PoweredActiveController.Instance, IStateMachineTarget, PoweredActiveController.Def>.State pre;

		// Token: 0x04005049 RID: 20553
		public GameStateMachine<PoweredActiveController, PoweredActiveController.Instance, IStateMachineTarget, PoweredActiveController.Def>.State loop;

		// Token: 0x0400504A RID: 20554
		public GameStateMachine<PoweredActiveController, PoweredActiveController.Instance, IStateMachineTarget, PoweredActiveController.Def>.State pst;
	}

	// Token: 0x02000DCF RID: 3535
	public new class Instance : GameStateMachine<PoweredActiveController, PoweredActiveController.Instance, IStateMachineTarget, PoweredActiveController.Def>.GameInstance
	{
		// Token: 0x06006AD9 RID: 27353 RVA: 0x00295AB0 File Offset: 0x00293CB0
		public Instance(IStateMachineTarget master, PoweredActiveController.Def def)
			: base(master, def)
		{
		}
	}
}
