using System;
using KSerialization;

// Token: 0x02000646 RID: 1606
[SerializationConfig(MemberSerialization.OptIn)]
public class SolidLogicValve : StateMachineComponent<SolidLogicValve.StatesInstance>
{
	// Token: 0x06002A9B RID: 10907 RVA: 0x000E0BE8 File Offset: 0x000DEDE8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002A9C RID: 10908 RVA: 0x000E0BF0 File Offset: 0x000DEDF0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06002A9D RID: 10909 RVA: 0x000E0C03 File Offset: 0x000DEE03
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x0400192F RID: 6447
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001930 RID: 6448
	[MyCmpReq]
	private SolidConduitBridge bridge;

	// Token: 0x020012E6 RID: 4838
	public class States : GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve>
	{
		// Token: 0x06007BEA RID: 31722 RVA: 0x002CD6C4 File Offset: 0x002CB8C4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.root.DoNothing();
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (SolidLogicValve.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational).Enter(delegate(SolidLogicValve.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			});
			this.on.DefaultState(this.on.idle).EventTransition(GameHashes.OperationalChanged, this.off, (SolidLogicValve.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational).Enter(delegate(SolidLogicValve.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
			});
			this.on.idle.PlayAnim("on").Transition(this.on.working, (SolidLogicValve.StatesInstance smi) => smi.IsDispensing(), UpdateRate.SIM_200ms);
			this.on.working.PlayAnim("on_flow", KAnim.PlayMode.Loop).Transition(this.on.idle, (SolidLogicValve.StatesInstance smi) => !smi.IsDispensing(), UpdateRate.SIM_200ms);
		}

		// Token: 0x04005EF8 RID: 24312
		public GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.State off;

		// Token: 0x04005EF9 RID: 24313
		public SolidLogicValve.States.ReadyStates on;

		// Token: 0x02001FFB RID: 8187
		public class ReadyStates : GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.State
		{
			// Token: 0x04008E93 RID: 36499
			public GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.State idle;

			// Token: 0x04008E94 RID: 36500
			public GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.State working;
		}
	}

	// Token: 0x020012E7 RID: 4839
	public class StatesInstance : GameStateMachine<SolidLogicValve.States, SolidLogicValve.StatesInstance, SolidLogicValve, object>.GameInstance
	{
		// Token: 0x06007BEC RID: 31724 RVA: 0x002CD848 File Offset: 0x002CBA48
		public StatesInstance(SolidLogicValve master)
			: base(master)
		{
		}

		// Token: 0x06007BED RID: 31725 RVA: 0x002CD851 File Offset: 0x002CBA51
		public bool IsDispensing()
		{
			return base.master.bridge.IsDispensing;
		}
	}
}
